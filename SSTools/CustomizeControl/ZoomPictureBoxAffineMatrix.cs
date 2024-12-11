using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSTools
{
    /// <summary>
    /// アフィン行列クラス
    /// </summary>
    internal class ZoomPictureBoxAffineMatrix
    {
        /// <summary>
        /// デフォルトのMatrix
        /// </summary>
        private Matrix defaultMatrix_ = new Matrix();

        /// <summary>
        /// 現在のMatrix
        /// </summary>
        private Matrix matrix_ = new Matrix();

        /// <summary>
        /// 逆行列
        /// </summary>
        private Matrix invMatrix_;

        /// <summary>
        /// 逆行列の算出
        /// </summary>
        /// <param name="matrix">アフィン行列</param>
        /// <returns>逆行列</returns>
        private Matrix CalcInvMatrix(Matrix matrix)
        {
            if ((matrix != null) && (matrix.IsInvertible))
            {   // 算出できる場合は算出
                Matrix inv = matrix.Clone();
                inv.Invert();
                return inv;
            }
            else
            {   // 無効な場合は単位行列
                return new Matrix();
            }
        }

        /// <summary>
        /// アフィン行列
        /// </summary>
        public Matrix Matrix
        {
            get { return matrix_; }
            private set
            {
                matrix_ = value;
                invMatrix_ = CalcInvMatrix(matrix_);
            }
        }
        /// <summary>
        /// 逆行列
        /// </summary>
        public Matrix InvMatrix { get { return invMatrix_; } }

        /// <summary>
        /// アスペクト比(ローカル)
        /// </summary>
        private double aspect_ = 1.0;
        /// <summary>
        /// アスペクト比
        /// </summary>
        public double Aspect
        {
            get { return aspect_; }
            private set { aspect_ = value; }
        }

        #region [座標変換]
        /// <summary>
        /// マウス座標から画像座標への変換
        /// </summary>
        /// <param name="mouseLocation">マウス座標</param>
        /// <returns>画像座標</returns>
        public Point GetImageLocaltion(Point mouseLocation)
        {
            if (invMatrix_ != null)
            {   // 逆行列を使って変換
                Point[] pts = new Point[] { mouseLocation };
                invMatrix_.TransformPoints(pts);
                return pts[0];
            }
            else
            {   // そのまま返す
                return mouseLocation;
            }
        }
        /// <summary>
        /// マウス座標から画像座標への変換
        /// </summary>
        /// <param name="mouseLocation">マウス座標</param>
        /// <returns>画像座標</returns>
        public PointF GetImageLocaltionF(Point mouseLocation)
        {
            if (invMatrix_ != null)
            {   // 逆行列を使って変換
                PointF[] pts = new PointF[] { new PointF(mouseLocation.X,mouseLocation.Y) };
                invMatrix_.TransformPoints(pts);
                return pts[0];
            }
            else
            {   // そのまま返す
                return new PointF(mouseLocation.X, mouseLocation.Y);
            }
        }
        /// <summary>
        /// マウス座標から画像座標への変換
        /// </summary>
        /// <param name="mouseLocation">マウス座標</param>
        /// <returns>画像座標</returns>
        public PointF GetImageLocaltionF(PointF mouseLocation)
        {
            if (invMatrix_ != null)
            {   // 逆行列を使って変換
                PointF[] pts = new PointF[] { mouseLocation };
                invMatrix_.TransformPoints(pts);
                return pts[0];
            }
            else
            {   // そのまま返す
                return mouseLocation;
            }
        }
        /// <summary>
        /// コントロールサイズから実画像サイズを求める
        /// </summary>
        /// <param name="size">サイズ</param>
        /// <returns>実画像サイズ</returns>
        public Size GetImageSize(Size size)
        {
            if (invMatrix_ != null)
            {   // 逆行列を使って変換
                Point[] pts = new Point[] { new Point(size.Width, size.Height) };
                invMatrix_.TransformVectors(pts);
                return new Size(pts[0].X, pts[0].Y);
            }
            else
            {   // そのまま返す
                return size;
            }
        }
        /// <summary>
        /// コントロールサイズから実画像サイズを求める
        /// </summary>
        /// <param name="size">サイズ</param>
        /// <returns>実画像サイズ</returns>
        public SizeF GetImageSizeF(Size size)
        {
            if (invMatrix_ != null)
            {   // 逆行列を使って変換
                PointF[] pts = new PointF[] { new PointF(size.Width, size.Height) };
                invMatrix_.TransformVectors(pts);
                return new SizeF(pts[0].X, pts[0].Y);
            }
            else
            {   // そのまま返す
                return new SizeF(size.Width,size.Height);
            }
        }
        /// <summary>
        /// コントロールサイズから実画像サイズを求める
        /// </summary>
        /// <param name="size">サイズ</param>
        /// <returns>実画像サイズ</returns>
        public SizeF GetImageSizeF(SizeF size)
        {
            if (invMatrix_ != null)
            {   // 逆行列を使って変換
                PointF[] pts = new PointF[] { new PointF(size.Width, size.Height) };
                invMatrix_.TransformVectors(pts);
                return new SizeF(pts[0].X, pts[0].Y);
            }
            else
            {   // そのまま返す
                return size;
            }
        }
        /// <summary>
        /// コントロール座標系差分から画像座標系差分を求める
        /// </summary>
        /// <param name="delta">コントロール座標系差分</param>
        /// <returns>画像座標系差分</returns>
        public PointF GetImageDelta(PointF delta)
        {
            if (invMatrix_ != null)
            {   // 逆行列を使って変換
                PointF[] pts = new PointF[] { delta };
                invMatrix_.TransformVectors(pts);
                return pts[0];
            }
            else
            {   // そのまま返す
                return delta;
            }
        }


        /// <summary>
        /// 画像座標からコントロール座標に変換する
        /// </summary>
        /// <param name="location">画像座標</param>
        /// <returns>コントロール座標</returns>
        public Point GetMouseLocation(Point location)
        {
            if (matrix_ != null)
            {
                Point[] pts = new Point[] { location };
                matrix_.TransformPoints(pts);
                return pts[0];
            }
            else
            {   // そのまま返す
                return location;
            }
        }
        /// <summary>
        /// 画像座標からコントロール座標に変換する
        /// </summary>
        /// <param name="location">画像座標</param>
        /// <returns>コントロール座標</returns>
        public PointF GetMouseLocationF(PointF location)
        {
            if (matrix_ != null)
            {
                PointF[] pts = new PointF[] { location };
                matrix_.TransformPoints(pts);
                return pts[0];
            }
            else
            {   // そのまま返す
                return location;
            }
        }
        /// <summary>
        /// 画像座標からコントロール座標に変換する
        /// </summary>
        /// <param name="location">画像座標</param>
        /// <returns>コントロール座標</returns>
        public PointF GetMouseLocationF(Point location)
        {
            if (matrix_ != null)
            {
                PointF[] pts = new PointF[] { new PointF(location.X,location.Y) };
                matrix_.TransformPoints(pts);
                return pts[0];
            }
            else
            {   // そのまま返す
                return new PointF(location.X, location.Y);
            }
        }

        /// <summary>
        /// 画像座標系の大きさをコントロール座標系に変換する
        /// </summary>
        /// <param name="size">画像座標系のサイズ</param>
        /// <returns>コントロール座標系のサイズ</returns>
        public Size GetControlSize(Size size)
        {
            if (matrix_ != null)
            {
                Point[] pts = new Point[] { new Point(size.Width,size.Height) };
                matrix_.TransformVectors(pts);
                return new Size(pts[0].X, pts[0].Y);
            }
            else
            {   // そのまま返す
                return size;
            }
        }
        /// <summary>
        /// 画像座標系の大きさをコントロール座標系に変換する
        /// </summary>
        /// <param name="size">画像座標系のサイズ</param>
        /// <returns>コントロール座標系のサイズ</returns>
        public SizeF GetControlSizeF(SizeF size)
        {
            if (matrix_ != null)
            {
                PointF[] pts = new PointF[] { new PointF(size.Width, size.Height) };
                matrix_.TransformVectors(pts);
                return new SizeF(pts[0].X, pts[0].Y);
            }
            else
            {   // そのまま返す
                return size;
            }
        }
        /// <summary>
        /// 画像座標系の大きさをコントロール座標系に変換する
        /// </summary>
        /// <param name="size">画像座標系のサイズ</param>
        /// <returns>コントロール座標系のサイズ</returns>
        public SizeF GetControlSizeF(Size size)
        {
            if (matrix_ != null)
            {
                PointF[] pts = new PointF[] { new PointF(size.Width, size.Height) };
                matrix_.TransformVectors(pts);
                return new SizeF(pts[0].X, pts[0].Y);
            }
            else
            {   // そのまま返す
                return new SizeF(size.Width, size.Height) ;
            }
        }
        #endregion [座標変換]

        /// <summary>
        /// アスペクト比 X
        /// </summary>
        private double xRatio_ = 1.0;
        /// <summary>
        /// アスペクト比 Y
        /// </summary>
        private double yRatio_ = 1.0;
        /// <summary>
        /// アスペクト比の設定
        /// </summary>
        /// <param name="xRatio">Xの比率</param>
        /// <param name="yRatio">Yの比率</param>
        /// <returns>アスペクト比</returns>
        public double SetAspect(float xRatio,float yRatio)
        {
            this.xRatio_ = xRatio;
            this.yRatio_ = yRatio;

            float min = (xRatio < yRatio) ? xRatio : yRatio;
            if (min < 1.0)
            {   // 最小値を1以上にする
                double inv = 1.0 / min;
                this.xRatio_ *= inv;
                this.yRatio_ *= inv;
            }

            aspect_ = this.xRatio_ / this.yRatio_;
            return aspect_;
        }

        /// <summary>
        /// 画像サイズ
        /// </summary>
        public Size ImageSize { get; private set; } = new Size();
        /// <summary>
        /// 画像サイズ(SizeF)
        /// </summary>
        public SizeF ImageSizeF { get { return new SizeF(ImageSize.Width, ImageSize.Height); } }
        /// <summary>
        /// コントロールのサイズ
        /// </summary>
        public Size ControlSize { get; private set; } = new Size();

        /// <summary>
        /// 倍率
        /// </summary>
        private readonly float[] zoomFactor =
        {
                0.03125F,   // 0
                0.0625F,    // 1
                0.125F,     // 2
                0.25F,      // 3
                0.3F,       // 4
                0.4F,       // 5
                0.5F,       // 6
                0.65F,      // 7
                0.8F,       // 8
                1.0F,       // 9
                1.25F,      // 10
                1.5F,       // 11
                2.0F,       // 12
                2.5F,       // 13
                3.0F,       // 14
                4.0F,       // 15
                5.0F,       // 16
                6.0F,       // 17
                8.0F,       // 18
                10.0F,      // 19
                15.0F,      // 20 
                20.0F,      // 21
                25.0F,      // 22
                30.0F,      // 23
            };
        /// <summary>
        /// Zoom倍率のインデックス
        /// </summary>
        private int zoomIndex_ = 9;
        /// <summary>
        /// Zoom倍率
        /// </summary>
        private int ZoomIndex
        {
            get { return zoomIndex_; }
            set
            {
                if ((value >= minZoomIndex) && (value < zoomFactor.Length))
                    zoomIndex_ = value;
            }
        }
        /// <summary>
        /// 最小倍率
        /// </summary>
        private int minZoomIndex = 0;

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public ZoomPictureBoxAffineMatrix() { }
        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="src">コピー元</param>
        public ZoomPictureBoxAffineMatrix(ZoomPictureBoxAffineMatrix src) 
        {
            defaultMatrix_ = src.defaultMatrix_.Clone();
            matrix_ = src.matrix_.Clone();
            invMatrix_ = CalcInvMatrix(matrix_);
            aspect_ = src.aspect_;
            ZoomIndex = src.ZoomIndex;
            ImageSize = src.ImageSize;
            ControlSize = src.ControlSize;
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="imageSize">画像サイズ</param>
        /// <param name="contorlSize">コントロールサイズ</param>
        /// <param name="sizeMode">サイズモード</param>
        /// <param name="aspect">アスペクト比</param>
        public ZoomPictureBoxAffineMatrix(Size imageSize,Size contorlSize,PictureBoxSizeMode sizeMode,double aspect=1.0)
        {
            // リセット処理を呼ぶ
            Reset(imageSize,contorlSize, sizeMode, aspect,true);
        }
        /// <summary>
        /// 最小倍率の計算
        /// </summary>
        /// <param name="imageSize">画像サイズ</param>
        /// <param name="contorlSize">コントロールサイズ</param>
        /// <param name="x_scale">X方向の倍率</param>
        /// <param name="y_scale">Y方向の倍率</param>
        /// <returns></returns>
        private int CalcMinZoomIndex(Size imageSize, Size controlSize, float x_scale, float y_scale)
        {
            int imgX = (int)(imageSize.Width * x_scale);
            int imgY = (int)(imageSize.Height * y_scale);

            float x_ratio = (float)controlSize.Width / imgX;
            float y_ratio = (float)controlSize.Height / imgY;
          
            // 小さい方を採用
            float ratio = (x_ratio < y_ratio) ? x_ratio : y_ratio;

            // テーブルを逆検索
            int index = zoomFactor.Length - 1;
            for (; (index >= 0) && (zoomFactor[index] > ratio) ; index--) { }

            return index;
        }

        /// <summary>
        /// アフィン行列のリセット
        /// </summary>
        /// <param name="imageSize">画像サイズ</param>
        /// <param name="contorlSize">コントロールのサイズ</param>
        /// <param name="sizeMode">SizeMode</param>
        /// <param name="aspect">アスペクト比</param>
        /// <param name="zoomReset">倍率をリセットするか</param>
        /// <param name="rect">表示指定領域</param>
        public void Reset(Size imageSize, Size contorlSize, PictureBoxSizeMode sizeMode, double aspect = 1.0, bool zoomReset = false,
            Rectangle? rect = null)
        {
            // 大きさを保存
            ImageSize = imageSize;
            ControlSize = contorlSize;

            // 倍率のリセット
            if (zoomReset)
            {   // 倍率を1.0
                ZoomIndex = 9;
                // 逆行列をリセット
                invMatrix_ = new Matrix();
            }

            Point? rectCenter = null;
            // 基準サイズ
            Size baseSize = contorlSize;
            if (rect.HasValue)
            {
                baseSize = rect.Value.Size;
                rectCenter = new Point((rect.Value.Left + rect.Value.Right) / 2, (rect.Value.Top + rect.Value.Bottom) / 2);
            }

            // SizeModeでデフォルトMatrixを作成
            switch (sizeMode)
            {
                case PictureBoxSizeMode.StretchImage:
                    // アスペクト比を再算出
                    float x_scale = (float)baseSize.Width / (float)(imageSize.Width * aspect);
                    float y_scale = (float)baseSize.Height / (float)(imageSize.Height);
                    aspect_= aspect;
                    Matrix tmpSt = new Matrix((float)(x_scale * aspect), 0.0F, 0.0F, y_scale, 0.0F, 0.0F);
                    defaultMatrix_ = tmpSt;
                    // 最小倍率の設定(等倍が最小)
                    minZoomIndex = 9;
                    break;
                case PictureBoxSizeMode.Zoom:
                    // 収まるように拡大率を算出
                    float x_scale_zoom = (float)baseSize.Width / (float)(imageSize.Width * aspect);
                    float y_scale_zoom = (float)baseSize.Height / (float)(imageSize.Height);
                    // 小さい方を採用
                    float scale_zoom = (x_scale_zoom < y_scale_zoom) ? x_scale_zoom : y_scale_zoom;
                    aspect_ = aspect;
                    Matrix tmpZ = new Matrix((float)(scale_zoom * aspect), 0.0F, 0.0F, scale_zoom, 0.0F, 0.0F);
                    defaultMatrix_ = tmpZ;
                    // 最小倍率の設定(等倍が最小)
                    minZoomIndex = 9;
                    break;
                case PictureBoxSizeMode.CenterImage:
                    // アスペクト比を加味したMatrix
                    aspect_ = aspect;
                    // 最低での1.0の値になるように調整
                    float scale_center = 1.0F;
                    if (aspect_ < 1.0)
                        scale_center = 1.0F / (float)aspect_;
                    Matrix tmpCI = new Matrix((float)aspect * scale_center, 0.0F, 0.0F, 1.0F * scale_center, 0.0F, 0.0F);
                    // 中心の差分分平行移動
                    tmpCI.Translate((float)(baseSize.Width - imageSize.Width * aspect * scale_center) / 2.0F,
                        (baseSize.Height - imageSize.Height * scale_center) / 2.0F, MatrixOrder.Append);
                    // 最小倍率の計算
                    minZoomIndex = CalcMinZoomIndex(imageSize, baseSize, defaultMatrix_.Elements[0], defaultMatrix_.Elements[3]);
                    break;
                case PictureBoxSizeMode.Normal:
                case PictureBoxSizeMode.AutoSize:
                    // アスペクト比を加味したMatrix
                    aspect_ = aspect;
                    // 最低での1.0の値になるように調整
                    float scale = 1.0F;
                    if (aspect_ < 1.0)
                        scale = 1.0F / (float)aspect_;
                    Matrix tmpAS = new Matrix((float)aspect * scale, 0.0F, 0.0F, 1.0F * scale, 0.0F, 0.0F);
                    defaultMatrix_ = tmpAS;
                    // 最小倍率の計算
                    minZoomIndex = CalcMinZoomIndex(imageSize, baseSize, defaultMatrix_.Elements[0], defaultMatrix_.Elements[3]);
                    break;
            }
            // アスペクト比によるスケールの設定
            if (aspect_ < 1.0)
            {
                xRatio_ = 1.0;
                yRatio_ = 1.0 / aspect_;
            }
            else
            {
                xRatio_ = aspect_;
                yRatio_ = 1.0;
            }

            // Zoom倍率を考慮したMatrixの生成
            CalcZoomInit();
        }
        /// <summary>
        /// 倍率の取得
        /// </summary>
        /// <returns>ズーム倍率</returns>
        private float GetZoomFactor()
        {
            return zoomFactor[ZoomIndex];
        }

        /// <summary>
        /// Zoom倍率を考慮したMatrixの生成(初期化・リセット時)
        /// </summary>
        public void CalcZoomInit()
        {
            // デフォルトMatrixの逆行列
            Matrix invDefault = CalcInvMatrix(defaultMatrix_);

            // 倍率
            float zoom = GetZoomFactor();
            float offsetX, offsetY;

            // コントロールのサイズ→実画像サイズ変換
            PointF[] pts_size = new PointF[] { new PointF(ControlSize.Width, ControlSize.Height) };
            invDefault.TransformVectors(pts_size);

            // Offset計算
            offsetX = defaultMatrix_.Elements[4];
            if (offsetX < (pts_size[0].X - ImageSize.Width) * defaultMatrix_.Elements[0] * zoom)
                offsetX = (pts_size[0].X - ImageSize.Width) * defaultMatrix_.Elements[0] * zoom;
            if (offsetX > 0.0F)
                offsetX = 0.0F;

            offsetY = defaultMatrix_.Elements[5]; ;
            if (offsetY < (pts_size[0].Y - ImageSize.Height) * defaultMatrix_.Elements[3] * zoom)
                offsetY = (pts_size[0].Y - ImageSize.Height) * defaultMatrix_.Elements[3] * zoom;
            if (offsetY > 0.0F)
                offsetY = 0.0F;

            // アフィン行列作成
            matrix_ = new Matrix(defaultMatrix_.Elements[0] * zoom, 0.0F, 0.0F, defaultMatrix_.Elements[3] * zoom,
                offsetX, offsetY);

            // 逆行列を算出
            invMatrix_ =  CalcInvMatrix(matrix_);
        }
        /// <summary>
        /// 表示領域の設定
        /// </summary>
        /// <param name="rect">表示領域</param>
        public void SetShowRectAngle(Rectangle rect)
        {
            // 現在のコントロールサイズ内での倍率
            float x_ratio = ControlSize.Width / (rect.Width * 1.2F);
            float y_ratio = ControlSize.Height / (rect.Height * 1.2F);
            // 小さい方を採用
            float ratio = (x_ratio < y_ratio) ? x_ratio : y_ratio;
            // 現在の倍率にかける
            float zoom = GetZoomFactor() * ratio;
            // テーブルを逆検索
            int zoomIndex = zoomFactor.Length - 1;
            for (; (zoomIndex >= 0) && (zoomFactor[zoomIndex] > ratio); zoomIndex--) { }

            // 指定倍率計算
            CalcZoom(zoomIndex);

            // 現在の表示されているサイズ
            SizeF dispImgSize = GetImageSizeF(ControlSize);
            RectangleF expRect = new RectangleF(
                rect.X - (dispImgSize.Width - rect.Width * 1.2F) / 2.0F,
                rect.Y - (dispImgSize.Height - rect.Height * 1.2F) / 2.0F,
                dispImgSize.Width,
                dispImgSize.Height);
            zoom = GetZoomFactor();

            // アフィン行列作成
            matrix_ = new Matrix(defaultMatrix_.Elements[0] * zoom, 0.0F, 0.0F, defaultMatrix_.Elements[3] * zoom,
                -expRect.X * zoom, -expRect.Y * zoom);

            // 逆行列を算出
            invMatrix_ = CalcInvMatrix(matrix_);

        }

        /// <summary>
        /// コントロールサイズ変更
        /// </summary>
        /// <param name="controlSize">コントロールのサイズ</param>
        public void ChangeControlSize(Size controlSize)
        {
            // コントロールのサイズ設定
            ControlSize = controlSize;

            // 最小倍率の計算
            int minZoomIndex = CalcMinZoomIndex(ImageSize, controlSize, defaultMatrix_.Elements[0], defaultMatrix_.Elements[3]);
            if (ZoomIndex < minZoomIndex)
            {   // 最小倍率を更新
                this.minZoomIndex = minZoomIndex;
                // 最小倍率で表示
                CalcZoom(minZoomIndex);
                return;
            }
            
            // 現在の表示されているサイズ
            SizeF dispImgSize = GetImageSizeF(ControlSize);

            // 倍率
            float zoom = GetZoomFactor();

            // オフセット
            float offsetX = matrix_.Elements[4];
            float offsetY = matrix_.Elements[5];

            // X方向 変更後のOffset計算
            if (dispImgSize.Width > ImageSize.Width)
                offsetX = 0.0F;

            if (offsetX < (dispImgSize.Width - ImageSize.Width) * defaultMatrix_.Elements[0] * zoom)
                offsetX = (dispImgSize.Width - ImageSize.Width) * defaultMatrix_.Elements[0] * zoom;
            if (offsetX > 0.0F)
                offsetX = 0.0F;

            // Y方向 変更後のOffset計算
            if (dispImgSize.Height > ImageSize.Height)
                offsetY = 0.0F;

            if (offsetY < (dispImgSize.Height - ImageSize.Height) * defaultMatrix_.Elements[3] * zoom)
                offsetY = (dispImgSize.Height - ImageSize.Height) * defaultMatrix_.Elements[3] * zoom;
            if (offsetY > 0.0F)
                offsetY = 0.0F;

            // アフィン行列作成
            matrix_ = new Matrix(defaultMatrix_.Elements[0] * zoom, 0.0F, 0.0F, defaultMatrix_.Elements[3] * zoom,
                offsetX, offsetY);

            // 逆行列を算出
            invMatrix_ = CalcInvMatrix(matrix_);
        }


        /// <summary>
        /// 倍率を加味してアフィン行列を更新
        /// </summary>
        /// <param name="localtion">拡大・縮小中心座標。nullの場合は原点</param>
        /// <param name="newZoomIndex">新しい拡大率</param>
        private void CalcZoom(int newZoomIndex, Point? localtion = null)
        {
            if (newZoomIndex < ZoomIndex)
            {
                // 現在のアフィン行列で表示サイズを計算
                SizeF dispSizeArea = GetControlSizeF(ImageSize);

                if ((ControlSize.Width > dispSizeArea.Width) && (ControlSize.Height > dispSizeArea.Height))
                {   // 画像が小さくなりすぎなので、これ以上は行わない
                    return;
                }
            }
            // 変更前の倍率
            float zoomBefore = GetZoomFactor();
            // 変更後の倍率
            ZoomIndex = newZoomIndex;
            float zoom = GetZoomFactor();

            // 倍率変更後のサイズ
            SizeF dispImgSize = new SizeF(
                    ControlSize.Width / (matrix_.Elements[0] * zoom),
                    ControlSize.Height/ (matrix_.Elements[3] * zoom) );


            // オフセット
            float elm4 = matrix_.Elements[4];
            float elm5 = matrix_.Elements[5];

            // 拡大中心
            PointF mousePts = new PointF(0.0F,0.0F);
            if (localtion.HasValue)
            {
                mousePts.X = localtion.Value.X;
                mousePts.Y = localtion.Value.Y;
            }
            // マウス座標の画像上の座標を求める
            PointF zoomCenter = GetImageLocaltionF(mousePts);

            // 変更後のOffset計算
            float offsetX = (zoomBefore - zoom) * defaultMatrix_.Elements[0] * zoomCenter.X + elm4;

            if (dispImgSize.Width > ImageSize.Width)
                offsetX = 0.0F;

            if (offsetX < (dispImgSize.Width - ImageSize.Width) * defaultMatrix_.Elements[0] * zoom)
                offsetX = (dispImgSize.Width - ImageSize.Width) * defaultMatrix_.Elements[0] * zoom;
            if (offsetX > 0.0F)
                offsetX = 0.0F;

            float offsetY = (zoomBefore - zoom) * defaultMatrix_.Elements[3] * zoomCenter.Y + elm5;

            if (dispImgSize.Height > ImageSize.Height)
                offsetY = 0.0F;

            if (offsetY < (dispImgSize.Height - ImageSize.Height) * defaultMatrix_.Elements[3] * zoom)
                offsetY = (dispImgSize.Height - ImageSize.Height) * defaultMatrix_.Elements[3] * zoom;
            if (offsetY > 0.0F)
                offsetY = 0.0F;

            // アフィン行列作成
            matrix_ = new Matrix(defaultMatrix_.Elements[0] * zoom, 0.0F, 0.0F, defaultMatrix_.Elements[3] * zoom,
                offsetX, offsetY);

            // 逆行列を算出
            invMatrix_ = CalcInvMatrix(matrix_);
        }
        /// <summary>
        /// 拡大
        /// </summary>
        /// <param name="localtion">中心座標</param>
        /// <returns>true:ズームOK</returns>
        public bool ZoomIn(Point? localtion = null)
        {
            return Zoom(1, localtion);
        }
        /// <summary>
        /// 縮小
        /// </summary>
        /// <param name="localtion">中心座標</param>
        /// <returns>true:ズームOK</returns>
        public bool ZoomOut(Point? localtion = null)
        {
            return Zoom(-1, localtion);
        }
        /// <summary>
        /// 拡大・縮小
        /// </summary>
        /// <param name="tick">負の場合は縮小。正の場合は拡大</param>
        /// <param name="localtion">中心座標</param>
        /// <returns>true:ズームOK</returns>
        public bool Zoom(int tick, Point? localtion = null)
        {
            int add_value = (tick < 0) ? -1 : 1;
            if ((ZoomIndex + add_value >= 0) && (ZoomIndex + add_value < zoomFactor.Length))
            {   // 倍率を更新
                CalcZoom(ZoomIndex + add_value, localtion);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 移動
        /// </summary>
        /// <param name="deltaX">X移動量</param>
        /// <param name="deltaY">Y移動量</param>
        /// <returns>true:移動した</returns>
        public bool Shift(int deltaX, int deltaY)
        {
            // マウス座標から実座標に変換
            PointF delta = GetImageDelta(new PointF(deltaX,deltaY));
            SizeF dispArea = GetImageSizeF(ControlSize);

            float transOffsetX = matrix_.Elements[4];
            float transOffsetY = matrix_.Elements[5];

            bool result = false;
            if ((matrix_.Elements[4] + delta.X >= (dispArea.Width - ImageSize.Width) * matrix_.Elements[0]) && (matrix_.Elements[4] + delta.X < 0))
            {   // X移動可
                transOffsetX += delta.X;
                result = true;
            }
            if ((matrix_.Elements[5] + delta.Y >= (dispArea.Height - ImageSize.Height) * matrix_.Elements[3]) && (matrix_.Elements[5] + delta.Y < 0))
            {   // Y移動可
                transOffsetY += delta.Y;
                result = true;
            }
            if (result)
            {
                matrix_ = new Matrix(matrix_.Elements[0], matrix_.Elements[1], matrix_.Elements[2], matrix_.Elements[3],
                    transOffsetX, transOffsetY);
                // 逆行列を算出
                invMatrix_ = CalcInvMatrix(matrix_);
            }
            return result;
        }
        /// <summary>
        /// 文字列変換
        /// </summary>
        /// <returns>各Matrixを文字列に変換</returns>
        public override string ToString()
        {
            return string.Format("default ... [0]={0:#.000} [3]={1:#.000} [4]={2:#.000} [5]={3:#.000}\r\n",
                defaultMatrix_.Elements[0], defaultMatrix_.Elements[3], defaultMatrix_.Elements[4], defaultMatrix_.Elements[5]) +
                string.Format("matrix ... [0]={0:#.000} [3]={1:#.000} [4]={2:#.000} [5]={3:#.000}\r\n",
                matrix_.Elements[0], matrix_.Elements[3], matrix_.Elements[4], matrix_.Elements[5]) +
                string.Format("inv mat ... [0]={0:#.000} [3]={1:#.000} [4]={2:#.000} [5]={3:#.000}\r\n",
                invMatrix_.Elements[0], invMatrix_.Elements[3], invMatrix_.Elements[4], invMatrix_.Elements[5]);
        }

    }
}

