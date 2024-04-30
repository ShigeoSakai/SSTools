using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SSTools
{
    public partial class ZoomPictureBox
    {
        /// <summary>
        /// 画素単位の拡大コピー
        /// </summary>
        /// <param name="dst">コピー先</param>
        /// <param name="src">コピー元</param>
        /// <param name="dstRect">コピー先領域</param>
        /// <param name="srcRect">コピー元領域</param>
        /// <param name="onePixcelSize">1ピクセルのサイズ(拡大倍率)</param>
        private void CopyImage(Bitmap dst, Bitmap src, Rectangle dstRect, Rectangle srcRect, int onePixcelSize, ref Point startOffset)
        {
            // 書き込みオフセット
            int copyOffsetX = 0;
            int copyOffsetY = 0;

            // 書き込みビットマップデータをロック
            BitmapData w_bd = dst.LockBits(
                       dstRect,
                       ImageLockMode.WriteOnly,
                       dst.PixelFormat);
            // 1画素のサイズ
            int dstPixelByte = GetPixelFormatSize(dst.PixelFormat);
            // カラーパレット
            ColorPalette dstPalette = dst.Palette;

            // 読み込みビットマップデータをロック
            BitmapData r_bd = src.LockBits(
                       srcRect,
                       ImageLockMode.ReadOnly,
                       src.PixelFormat);
            // 1画素のサイズ
            int srcPixelByte = GetPixelFormatSize(src.PixelFormat);
            // カラーパレット
            ColorPalette srcPalette = src.Palette;

            if (srcPixelByte != 0)
            {
                // 1ライン分の元データ
                byte[] pixelData = new byte[srcPixelByte * r_bd.Width];

                // 書き込みバイト数
                int dstLength = r_bd.Width * GetPixelFormatSize(dst.PixelFormat) * onePixcelSize;
                int dstLengthTmp = dstLength;
                if (dstLength > w_bd.Width * GetPixelFormatSize(dst.PixelFormat))
                {
                    dstLength = w_bd.Width * GetPixelFormatSize(dst.PixelFormat);
                    if ((srcRect.X + srcRect.Width) >= src.Width)
                    {   // オフセットを調整して、逆からにする
                        copyOffsetX = dstLengthTmp - dstLength;
                    }
                }
                // 書き込み行数
                int dstHeight = r_bd.Height * onePixcelSize;
                int dstHeightTmp = dstHeight;
                if (dstHeight > w_bd.Height)
                {
                    dstHeight = w_bd.Height;
                    if ((srcRect.Y + srcRect.Height) >= src.Height)
                    {   // オフセットを調整して、逆からにする
                        copyOffsetY = dstHeightTmp - dstHeight;
                    }
                }
                int copyOffsetYTmp = copyOffsetY;
                for (int y = 0; y < r_bd.Height; y++)
                {
                    // 読み込み元アドレス
                    IntPtr readAddr = r_bd.Scan0 + y * r_bd.Stride;
                    // バイト配列にコピー
                    Marshal.Copy(readAddr, pixelData, 0, srcPixelByte * r_bd.Width);

                    // コピー先形式に変換
                    byte[] dstData = ConvertDstFormat(pixelData, w_bd.Width, dst.PixelFormat,
                        r_bd.Width, src.PixelFormat, srcPalette.Entries, onePixcelSize);

                    for (int row = copyOffsetYTmp; (row < onePixcelSize) && (y * onePixcelSize + row - copyOffsetY < w_bd.Height); row++)
                    {
                        // コピー先のアドレス
                        IntPtr writeAddr = w_bd.Scan0 + (y * onePixcelSize + row - copyOffsetY) * w_bd.Stride;
                        Marshal.Copy(dstData, copyOffsetX, writeAddr, dstLength);
                    }
                    // 先頭に戻す
                    copyOffsetYTmp = 0;
                }
            }

            // ビットマップデータをロック解除
            dst.UnlockBits(w_bd);
            src.UnlockBits(r_bd);

            startOffset.X = onePixcelSize - (int)(copyOffsetX / GetPixelFormatSize(dst.PixelFormat));
            startOffset.Y = onePixcelSize - copyOffsetY;
        }
        /// <summary>
        /// コピー先のフォーマットに変換
        /// </summary>
        /// <param name="srcData">コピー元データ</param>
        /// <param name="dstWidth">コピー先画像幅</param>
        /// <param name="dstFormat">コピー先フォーマット</param>
        /// <param name="srcWidth">コピー元画像幅</param>
        /// <param name="srcFormat">コピー元フォーマット</param>
        /// <param name="srcPalette">コピー元パレット</param>
        /// <param name="onePixcelSize">拡大サイズ(1pixelの大きさ)</param>
        /// <returns></returns>
        private byte[] ConvertDstFormat(byte[] srcData, int dstWidth, PixelFormat dstFormat,
            int srcWidth, PixelFormat srcFormat, Color[] srcPalette, int onePixcelSize)
        {
            // コピー先の必要バイト数
            int dstLength = srcWidth * GetPixelFormatSize(dstFormat) * onePixcelSize;
            // 格納先を確保
            byte[] dstData = new byte[dstLength];
            // 読み込み元をColorに変換
            Color[] oneLineColor = ConvertColor(srcData, srcWidth, srcFormat, srcPalette);
            int w_index = 0;
            // コピー先に書き込み
            for (int index = 0; index < oneLineColor.Length; index++)
            {
                for (int cpnum = 0; (cpnum < onePixcelSize) && (w_index < dstLength); cpnum++)
                {
                    dstData[w_index] = oneLineColor[index].B;
                    w_index++;
                    if (w_index < dstLength)
                    {
                        dstData[w_index] = oneLineColor[index].G;
                        w_index++;
                    }
                    if (w_index < dstLength)
                    {
                        dstData[w_index] = oneLineColor[index].R;
                        w_index++;
                    }
                    if ((w_index < dstLength) &&
                        ((dstFormat == PixelFormat.Format32bppArgb) || (dstFormat == PixelFormat.Format32bppPArgb)))
                    {
                        dstData[w_index] = oneLineColor[index].A;
                        w_index++;
                    }
                    if ((w_index < dstLength) &&
                        (dstFormat == PixelFormat.Format32bppRgb))
                    {
                        w_index++;
                    }
                }
            }

            return dstData;
        }
        /// <summary>
        /// コピー元データからColor配列を生成する
        /// </summary>
        /// <param name="srcData">コピー元データ</param>
        /// <param name="srcWidth">コピー元画像幅</param>
        /// <param name="srcFormat">コピー元フォーマット</param>
        /// <param name="srcPalette">コピー元パレット</param>
        /// <returns></returns>
        private Color[] ConvertColor(byte[] srcData, int srcWidth, PixelFormat srcFormat, Color[] srcPalette)
        {
            Color[] result = new Color[srcWidth];
            int w_index = 0;
            int r_index = 0;
            while ((w_index < srcWidth) && (r_index < srcData.Length))
            {
                if (srcFormat == PixelFormat.Format8bppIndexed)
                {   // 8bit Indexed Format
                    result[w_index] = srcPalette[srcData[r_index]];
                    w_index++;
                    r_index++;
                }
                else if (srcFormat == PixelFormat.Format24bppRgb)
                {   // 24bit RGB
                    result[w_index] = Color.FromArgb(srcData[r_index + 2], srcData[r_index + 1], srcData[r_index]);
                    w_index++;
                    r_index += 3;
                }
                else if ((srcFormat == PixelFormat.Format32bppArgb) ||
                    (srcFormat == PixelFormat.Format32bppPArgb) ||
                    (srcFormat == PixelFormat.Format32bppRgb))
                {   // 32bit ARGB
                    result[w_index] = Color.FromArgb(srcData[r_index + 3], srcData[r_index + 2], srcData[r_index + 1], srcData[r_index]);
                    w_index++;
                    r_index += 4;
                }
                else if (srcFormat == PixelFormat.Format16bppGrayScale)
                {   // 16bit TIFF
                    r_index++;
                    result[w_index] = Color.FromArgb(srcData[r_index], srcData[r_index], srcData[r_index]);
                    w_index++;
                    r_index++;
                }
            }

            return result;
        }

        /// <summary>
        /// PixelFormatから使用するBYTEサイズの算出
        /// </summary>
        /// <param name="format">PixelFormat</param>
        /// <returns>バイトサイズ</returns>
        private int GetPixelFormatSize(PixelFormat format)
        {
            switch (format)
            {
                case PixelFormat.Format16bppArgb1555:
                case PixelFormat.Format16bppGrayScale:
                case PixelFormat.Format16bppRgb555:
                case PixelFormat.Format16bppRgb565:
                    return 2;
                case PixelFormat.Format24bppRgb:
                    return 3;
                case PixelFormat.Format32bppArgb:
                case PixelFormat.Format32bppPArgb:
                case PixelFormat.Format32bppRgb:
                    return 4;
                case PixelFormat.Format48bppRgb:
                    return 6;
                case PixelFormat.Format64bppArgb:
                case PixelFormat.Format64bppPArgb:
                    return 8;
                case PixelFormat.Format8bppIndexed:
                    return 1;
                // 以降はbit単位なので無視
                case PixelFormat.Format4bppIndexed:
                case PixelFormat.Format1bppIndexed:
                default:
                    return 0;
            }
        }
    }
}
