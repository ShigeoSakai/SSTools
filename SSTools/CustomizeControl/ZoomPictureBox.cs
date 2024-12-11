using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SSTools.Shape;
using static SSTools.ShapeManager;

namespace SSTools
{
    /// <summary>
    /// ズームできるPictureBox
    /// </summary>
    public partial class ZoomPictureBox : PictureBox
    {
        /// <summary>
        /// 画素情報クラス
        /// </summary>
        public class PictureBoxInfo
        {
            /// <summary>
            /// X座標
            /// </summary>
            public int X { get; private set; }
            /// <summary>
            /// Y座標
            /// </summary>
            public int Y { get; private set; }
            /// <summary>
            /// 画像上でのX座標
            /// </summary>
            public int ImageX { get; private set; }
            /// <summary>
            /// 画像上でのY座標
            /// </summary>
            public int ImageY { get; private set; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="x">X座標</param>
            /// <param name="y">Y座標</param>
            /// <param name="imgX">画像上のX座標</param>
            /// <param name="imgY">画像上のY座標</param>
            public PictureBoxInfo(int x, int y, int imgX, int imgY)
            {
                X = x;
                Y = y;
                ImageX = imgX;
                ImageY = imgY;
            }
        }
        /// <summary>
        ///画素情報取得イベントハンドラ
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="info">画素情報</param>
        public delegate void ZoomPictureBox_InfoEventHandler(object sender, PictureBoxInfo info);
        /// <summary>
        /// 画素情報取得イベント
        /// </summary>
        public event ZoomPictureBox_InfoEventHandler InfoEvent;
        /// <summary>
        /// 画素情報取得イベント発行
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <param name="imgX">画像上のX座標</param>
        /// <param name="imgY">画像上のY座標</param>
        protected void OnInfoEvent(int x, int y, int imgX, int imgY)
        {
            InfoEvent?.Invoke(this, new PictureBoxInfo(x, y, imgX, imgY));
        }
        /// <summary>
        /// 画像クリックイベント
        /// </summary>
        public event ZoomPictureBox_InfoEventHandler ClickInfoEvent;
        /// <summary>
        /// 画像イベント発行
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <param name="imgX">画像上のX座標</param>
        /// <param name="imgY">画像上のY座標</param>
        protected void OnClickInfoEvent(int x, int y, int imgX, int imgY)
        {
            ClickInfoEvent?.Invoke(this, new PictureBoxInfo(x, y, imgX, imgY));
        }

        /// <summary>
        /// アフィン行列
        /// </summary>
        private readonly ZoomPictureBoxAffineMatrix affineMatrix_ = new ZoomPictureBoxAffineMatrix();

        /// <summary>
        /// 画像表示をするか
        /// </summary>
        private bool showImage_ = true;
        /// <summary>
        /// 画像表示をするかどうか(プロパティ)
        /// </summary>
        public bool ShowImage { get { return showImage_; }
            set
            {
                if (showImage_ != value)
                {
                    showImage_ = value;
                    Refresh();
                }
            }

        }
        /// <summary>
        /// マスク用カラーマトリックス
        /// </summary>
        private ColorMatrix maskColorMatrix = new ColorMatrix();
        /// <summary>
        /// マスク用画像アトリビュート
        /// </summary>
        private ImageAttributes maskImageAttribute = new ImageAttributes();
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ZoomPictureBox() :base()
        {
            //ColorMatrixの行列の値を変更して、アルファ値が0.5に変更されるようにする
            maskColorMatrix.Matrix00 = maskColorMatrix.Matrix11 = maskColorMatrix.Matrix22 = maskColorMatrix.Matrix44 = 1;
            maskColorMatrix.Matrix33 = 0.5F;
            //ColorMatrixを設定する
            maskImageAttribute.SetColorMatrix(maskColorMatrix);
        }
        /// <summary>
        /// マスク画像のAlfa値
        /// </summary>
        public float MaskAlfa
        {
            get => maskColorMatrix.Matrix33;
            set
            {
                maskColorMatrix.Matrix33 = value;
                //ColorMatrixを設定する
                maskImageAttribute.SetColorMatrix(maskColorMatrix);
            }
        }

        /// <summary>
        /// Imageを自動削除するかどうか(プロパティ)
        /// </summary>
        public bool AutoImageDispose { get; set; } = true;

        /// <summary>
        /// 画像
        /// </summary>
        public new Image Image
        {
            get { return base.Image; }
            set
            {
                if ((base.Image != null) && (AutoImageDispose))
                {
                    base.Image.Dispose();
                    base.Image = null;
                }
                base.Image = value;
                // Matrixの計算
                CalcMatrix(true);
                // 再描画
                Refresh();
            }
        }
        /// <summary>
        /// 表示領域の指定
        /// </summary>
        /// <param name="rect">表示する領域</param>
        public void SetShowRectAngle(Rectangle rect)
        {
            // Matrixの計算
            affineMatrix_.SetShowRectAngle(rect);
            // 再描画
            Refresh();
        }


        /// <summary>
        /// マスク画像(ローカル)
        /// </summary>
        private Image maskImage_ = null;
        /// <summary>
        /// マスク画像
        /// </summary>
        public Image MaskImage
        {
            get { return maskImage_; }
            set
            {
                if (maskImage_ != null)
                {
                    maskImage_.Dispose();
                    maskImage_ = null;
                }
                maskImage_ = value;
                if (maskShow_)
                {   // 再描画
                    Refresh();
                }
            }
        }
        /// <summary>
        /// マスク表示
        /// </summary>
        private bool maskShow_ = true;
        /// <summary>
        /// マスク画像の表示有無(プロパティ)
        /// </summary>
        public bool MaskShow
        {
            get { return maskShow_; }
            set
            {
                if (maskShow_ != value)
                {
                    maskShow_ = value;
                    // 再描画
                    Refresh();
                }
            }
        }
        /// <summary>
        /// 図形表示有無
        /// </summary>
        private bool shapeShow_ = true;
        /// <summary>
        /// 図形表示有無(プロパティ)
        /// </summary>
        public bool ShapeShow
        {
            get { return shapeShow_; }
            set
            {
                if (shapeShow_ != value)
                {
                    shapeShow_ = value;
                    // 再描画
                    Refresh();

                }
            }
        }


        /// <summary>
        /// X軸比率
        /// </summary>
        private float xRatio_ = 1.0F;
        /// <summary>
        /// Y軸比率
        /// </summary>
        private float yRatio_ = 1.0F;

        /// <summary>
        /// X軸比率(プロパティ)
        /// </summary>
        public float XRatio
        {
            get { return xRatio_; }
            set
            {
                if (xRatio_ != value)
                {
                    xRatio_ = value;
                    CalcMatrix();
                    Refresh();
                }
            }
        }
        /// <summary>
        /// Y軸比率(プロパティ)
        /// </summary>
        public float YRatio
        {
            get { return yRatio_; }
            set
            {
                if (yRatio_ != value)
                {
                    yRatio_ = value;
                    CalcMatrix();
                    Refresh();
                }
            }
        }
        /// <summary>
        /// SizeMode(プロパティ)
        /// </summary>
        public new PictureBoxSizeMode SizeMode
        {
            get { return base.SizeMode; }
            set
            {
                base.SizeMode = value;
                CalcMatrix();
                Refresh();
            }
        }
        /// <summary>
        /// PictureBox描画モード
        /// </summary>
        public enum PictureBoxDrawMode
        {
            NORMAL = 0,     //!< 通常
            AREA_SELECT,    //!< エリア選択中
            DRAW,           //!< 描画中
        }
        /// <summary>
        /// エリア選択時の色
        /// </summary>
        public Color AreaSelectColor { get; set; } = Color.YellowGreen;
        /// <summary>
        /// PictureBox描画モード指定
        /// </summary>
        private PictureBoxDrawMode pictureBoxDrawMode_ = PictureBoxDrawMode.NORMAL;
        /// <summary>
        /// PictureBox描画モード指定(プロパティ)
        /// </summary>
        public PictureBoxDrawMode PictureBoxMode
        {
            get { return pictureBoxDrawMode_; }
            set
            {
                if (pictureBoxDrawMode_ != value)
                {
                    if ((pictureBoxDrawMode_ == PictureBoxDrawMode.AREA_SELECT) && (areaSelectShape_ != null))
                    {   // エリア選択中は今の状態で確定させる
                        OnAreaConfirmEvent(areaSelectShape_.Points);
                    }

                    pictureBoxDrawMode_ = value;
                    // モード切替でエリア選択図形をクリア
                    areaSelectShape_ = null;
                    // 再描画
                    Refresh();
                }
            }
        }
        /// <summary>
        /// エリア選択図形
        /// </summary>
        private PolygonShape areaSelectShape_ = null;

        /// <summary>
        /// 描画イベント
        /// </summary>
        /// <param name="pe">Paintイベント引数</param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            if (base.Image != null)
            {
                Graphics g = pe.Graphics;
                // Affine行列を設定
                g.Transform = affineMatrix_.Matrix;

                try
                {
                    if (showImage_)
                    {
                        // 画像を描画
                        g.DrawImage(base.Image, 0, 0);
                    }
                    else
                    {
                        g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, base.Image.Width, base.Image.Height));
                    }
                    // マスク画像
                    if ((maskImage_ != null) && (maskShow_))
                    {
                        //g.DrawImage(maskImage_, 0, 0);
                        g.DrawImage (maskImage_,new Rectangle(0, 0, base.Image.Width, base.Image.Height),
                            0, 0, base.Image.Width, base.Image.Height, GraphicsUnit.Pixel, maskImageAttribute);
                    }
                    // 図形
                    if ((_Shapes.Count > 0) && (shapeShow_))
                    {
                        DrawShape(g);
                    }
                    // 領域選択
                    if ((pictureBoxDrawMode_ == PictureBoxDrawMode.AREA_SELECT) && (areaSelectShape_ != null))
                        areaSelectShape_.Draw(g, affineMatrix_.ImageSizeF);

                }
                catch { }
            }
        }
        /// <summary>
        /// アフィン行列の計算
        /// </summary>
        /// <param name="isReset">true;倍率リセット</param>
        /// <param name="rect">表示指定領域</param>
        private void CalcMatrix(bool isReset = false, Rectangle? rect = null)
        {
            if (base.Image != null)
            {
                // アスペクト比を計算
                double aspect = xRatio_ / yRatio_;
                // アフィン行列に設定
                affineMatrix_.Reset(base.Image.Size, ClientSize, SizeMode, aspect, isReset, rect);
            }
        }
        /// <summary>
        /// コントロールのサイズ変更
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (base.Image != null)
            {
                // アフィン行列の計算
                affineMatrix_.ChangeControlSize(ClientSize);
                // 再描画
                Refresh();
            }
        }
        /// <summary>
        /// マウスホィール
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta != 0)
            {   // 拡大・縮小設定
                affineMatrix_.Zoom(e.Delta, e.Location);
                // 再描画
                Refresh();
            }
        }

        /// <summary>
        /// マウスドラッグ中か？
        /// </summary>
        private bool isMouseDown = false;
        /// <summary>
        /// 前回のマウス位置
        /// </summary>
        private Point beforeMousePosition = new Point(0, 0);

        /// <summary>
        /// マウスダウンイベント
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            // ドラッグ中
            isMouseDown = true;
            // 座標値を保存
            beforeMousePosition = e.Location;
        }
        /// <summary>
        /// 表示エリア確定イベントハンドラ
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="pointList">座標リスト</param>
        /// <returns>true:確定する</returns>
        public delegate bool AreaConfirmEventHandler(object sender, List<Point> pointList);
        /// <summary>
        /// 表示エリア確定イベント
        /// </summary>
        public event AreaConfirmEventHandler AreaConfirmEvent;
        /// <summary>
        /// 表示エリア確定イベント発行
        /// </summary>
        /// <param name="pointList">座標リスト</param>
        /// <returns>true:確定する</returns>
        protected virtual bool OnAreaConfirmEvent(List<Point> pointList)
        {
            if (AreaConfirmEvent != null)
                return AreaConfirmEvent(this, pointList);
            return true;
        }


        /// <summary>
        /// マウスアップイベント
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (isMouseDown)
            {   // ドラッグ中解除
                isMouseDown = false;
                if ((pictureBoxDrawMode_ == PictureBoxDrawMode.AREA_SELECT) &&
                    (Math.Abs(beforeMousePosition.X - e.X) < 2.0) &&
                    (Math.Abs(beforeMousePosition.Y - e.Y) < 2.0))
                {   // 2pixel以内ならクリックと判断
                    Point imageLocation = affineMatrix_.GetImageLocaltion(e.Location);

                    if ((imageLocation.X >= 0) && (imageLocation.X < affineMatrix_.ImageSize.Width) &&
                        (imageLocation.Y >= 0) && (imageLocation.Y < affineMatrix_.ImageSize.Height))
                    {
                        if (areaSelectShape_ == null)
                            areaSelectShape_ = new PolygonShape("AreaSelect", imageLocation, AreaSelectColor);
                        else
                        {
                            if ((Math.Abs(imageLocation.X - areaSelectShape_.StartPoint.X) <= areaSelectShape_.MarkerSize) &&
                                (Math.Abs(imageLocation.Y - areaSelectShape_.StartPoint.Y) <= areaSelectShape_.MarkerSize))
                            {   // 編集の終了
                                // イベント発行
                                if (OnAreaConfirmEvent(areaSelectShape_.Points))
                                {
                                    areaSelectShape_ = null;
                                    pictureBoxDrawMode_ = PictureBoxDrawMode.NORMAL;
                                }
                            }
                            else
                                areaSelectShape_.AddPoint(imageLocation);

                        }
                        Refresh();
                    }
                }
            }
            if (ClickInfoEvent != null)
            {
                Point imgPt = affineMatrix_.GetImageLocaltion(e.Location);
                OnClickInfoEvent(e.X, e.Y, imgPt.X, imgPt.Y);
            }

        }
        /// <summary>
        /// ダブルクリック時の処理
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnDoubleClick(EventArgs e)
        {
            if ((pictureBoxDrawMode_ == PictureBoxDrawMode.AREA_SELECT) && (areaSelectShape_ != null))
            {
                // イベント発行
                if (OnAreaConfirmEvent(areaSelectShape_.Points))
                {
                    areaSelectShape_ = null;
                    pictureBoxDrawMode_ = PictureBoxDrawMode.NORMAL;
                }
                Refresh();
            }
            base.OnDoubleClick(e);
        }


        /// <summary>
        /// マウス移動イベント
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if ((isMouseDown) && (e.Button == MouseButtons.Left))
            {   // ドラッグ中
                //  移動量を計算
                int deltaX = e.Location.X - beforeMousePosition.X;
                int deltaY = e.Location.Y - beforeMousePosition.Y;
                // 移動
                if (affineMatrix_.Shift(deltaX, deltaY))
                {   // 再描画
                    Refresh();
                }
                // 座標値を保存
                beforeMousePosition = e.Location;
            }
            else if ((pictureBoxDrawMode_ == PictureBoxDrawMode.AREA_SELECT) && (areaSelectShape_ != null))
            {
                Point imageLocation = affineMatrix_.GetImageLocaltion(e.Location);
                if ((imageLocation.X >= 0) && (imageLocation.X < affineMatrix_.ImageSize.Width) &&
                    (imageLocation.Y >= 0) && (imageLocation.Y < affineMatrix_.ImageSize.Height))
                {
                    areaSelectShape_.ExtraPoint = imageLocation;
                    Refresh();
                }
            }
            if (InfoEvent != null)
            {
                Point imgPt = affineMatrix_.GetImageLocaltion(e.Location);
                OnInfoEvent(e.X, e.Y, imgPt.X, imgPt.Y);
            }

        }
        /// <summary>
        /// アフィン行列のリセット
        /// </summary>
        /// <param name="x_ratio">X軸比率</param>
        /// <param name="y_ratio">Y軸比率</param>
        /// <param name="sizeMode">SizeMode</param>
        public void Reset(float x_ratio, float y_ratio, PictureBoxSizeMode sizeMode)
        {
            // アフィン行列リセット
            affineMatrix_.Reset(base.Image.Size, ClientSize, sizeMode, (double)(x_ratio / y_ratio), true);
        }

        /// <summary>
        /// 図形辞書
        /// </summary>
        //private readonly Dictionary<string, Shape.BaseShape> shapeDictionary = new Dictionary<string, Shape.BaseShape>();
        private ShapeManager _Shapes = new ShapeManager();
        /// <summary>
        /// 登録されている図形の一覧を取得する(Enumrator)
        /// </summary>
        /// <returns>登録されている図形</returns>
        public IEnumerable<Shape.BaseShape> EnumerableShape()
        {
            return _Shapes.EnumerableShape();
        }
        /// <summary>
        /// 指定図形の一覧を取得(Enumrator)
        /// </summary>
        /// <typeparam name="T">図形</typeparam>
        /// <returns>指定図形の一覧</returns>
        public IEnumerable<T> EnumerableShapes<T>() where T : BaseShape
        {
            return _Shapes.EnumerableShapes<T>();
        }
        /// <summary>
        /// 指定図形の一覧を取得(Enumrator)
        /// </summary>
        /// <param name="pattern">取得する図形名の正規表現</param>
        /// <returns>指定図形の一覧</returns>
        public IEnumerable<BaseShape> EnumerableShapes(string pattern)
        {
            return _Shapes.EnumerableShapes(pattern);
        }
        /// <summary>
        /// 図形の追加
        /// </summary>
        /// <param name="shape">図形</param>
        public void AddShape(Shape.BaseShape shape)
        {
            _Shapes.AddShape(shape);
        }
        /// <summary>
        /// 複数図形の追加
        /// </summary>
        /// <param name="shapes">図形</param>
        public void AddShape(IEnumerable<Shape.BaseShape> shapes)
        {
            _Shapes.AddShape(shapes);
        }
        /// <summary>
        /// 図形の追加
        /// </summary>
        /// <param name="shapes">図形マネージャ</param>
        public void AddShape(ShapeManager shapes)
        {
            _Shapes.AddShape(shapes);
        }

        /// <summary>
        /// 図形の名前一覧の取得
        /// </summary>
        /// <returns>図形の名前一覧</returns>
        public List<string> GetShapeNames()
        {
            return _Shapes.GetShapeNames();
        }
        /// <summary>
        /// 指定図形の色を設定
        /// </summary>
        /// <param name="name">図形名</param>
        /// <param name="color">色</param>
        /// <returns>true;設定OK</returns>
        public bool ChangeColor(string name,Color color)
        {
            return _Shapes.SetShapeProperty(name,ShapeManager.SHAPE_PROPERTY.Color,color);
        }
        /// <summary>
        /// 指定図形の表示有無を設定
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="visible">表示有無</param>
        /// <returns>true;設定OK</returns>
        public bool ChangeVisible(string name, bool visible)
        {
            return (_Shapes.SetShapeProperty(name, ShapeManager.SHAPE_PROPERTY.Visible,visible));
        }
        /// <summary>
        /// 指定図形を削除
        /// </summary>
        /// <param name="name">名前</param>
        /// <returns>true:削除OK</returns>
        public bool Remove(string name)
        {
           return _Shapes.Remove(name);
        }
        /// <summary>
        /// パターンで指定された図形を削除
        /// </summary>
        /// <param name="pattern">名前のパターン(正規表現)</param>
        /// <returns>true:削除OK</returns>
        public bool RemovePattern(string pattern)
        {
            return _Shapes.RemovePattern(pattern);
        }


        /// <summary>
        /// 図形の全クリア
        /// </summary>
        public void ClearShape()
        {
            _Shapes.ClearShape();
        }

        /// <summary>
        /// 指定図形のプロパティを設定する
        /// </summary>
        /// <param name="pattern">取得する図形名の正規表現</param>
        /// <param name="property">プロパティ名</param>
        /// <param name="value">値</param>
        /// <returns>true:設定した</returns>
        public bool SetShapeProperty(string pattern, string property, object value)
        {
            return _Shapes.SetShapeProperty(pattern,property,value);
        }
        /// <summary>
        /// 指定図形のプロパティを設定する
        /// </summary>
        /// <param name="pattern">取得する図形名の正規表現</param>
        /// <param name="property">プロパティ名</param>
        /// <param name="value">値</param>
        /// <returns>true:設定した</returns>
        public bool SetShapeProperty(string pattern, SHAPE_PROPERTY property, object value)
        {
            return _Shapes.SetShapeProperty(pattern,property,value);
        }


        /// <summary>
        /// 図形の描画
        /// </summary>
        /// <param name="g">グラフィックス</param>
        /// <returns>true:図形を描画した</returns>
        private bool DrawShape(Graphics g)
        {
            // 描画領域
            PointF startP = affineMatrix_.GetImageLocaltionF(new Point(0, 0));
            PointF endP = affineMatrix_.GetImageLocaltionF(new Point(ClientSize.Width, ClientSize.Height));
            RectangleF rect = new RectangleF(startP.X, startP.Y, endP.X - startP.X, endP.Y - startP.Y);

            return _Shapes.Draw(g, affineMatrix_.ImageSizeF, rect);
        }
        /// <summary>
        /// 画像を保存
        /// </summary>
        /// <param name="filename">ファイル名</param>
        /// <param name="drawShape">図形描画有無</param>
        /// <param name="drawMask">マスク描画有無</param>
        /// <returns>true:保存OK</returns>
        public bool SaveImage(string filename, bool drawShape = true, bool drawMask = true)
        {
            bool result = false;
            if ((!string.IsNullOrEmpty(filename)) && (base.Image != null))
            {
                Bitmap bmp = GetImageToBitmap(drawShape, drawMask);
                if (bmp != null)
                {
                    try
                    {
                        bmp.Save(filename);
                        result = true;
                    }
                    catch { }
                    finally
                    {
                        bmp.Dispose();
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// クリップボードに画像をコピー
        /// </summary>
        /// <param name="drawShape">図形描画有無</param>
        /// <param name="drawMask">マスク描画有無</param>
        /// <returns>true;クリップボードにコピーOK</returns>
        public bool ToClipBoard(bool drawShape = true, bool drawMask = true)
        {
            bool result = false;
            if (base.Image != null)
            {
                Bitmap bmp = GetImageToBitmap(drawShape, drawMask);
                if (bmp != null)
                {
                    Clipboard.SetImage(bmp);
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// 画像をBitmapにコピーする
        /// </summary>
        /// <param name="drawShape">図形描画有無</param>
        /// <param name="drawMask">マスク描画有無</param>
        /// <returns>画像のビットマップ</returns>
        private Bitmap GetImageToBitmap(bool drawShape = true, bool drawMask = true)
        {
            if (base.Image != null)
            {
                Bitmap bmp = new Bitmap(base.Image.Width, base.Image.Height, PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bmp);
                g.DrawImage(base.Image, 0, 0);

                if ((drawMask) && (maskImage_ != null))
                {
					g.DrawImage(maskImage_, new Rectangle(0, 0, base.Image.Width, base.Image.Height),
							0, 0, base.Image.Width, base.Image.Height, GraphicsUnit.Pixel, maskImageAttribute);
				}
                if ((drawShape) && (_Shapes.Count > 0))
                {
                    DrawShape(g);
                }
                g.Dispose();
                return bmp;
            }
            return null;
        }
        /// <summary>
        /// 指定された矩形で画像を切り取り、ファイルに保存する
        /// </summary>
        /// <param name="filename">ファイル名</param>
        /// <param name="rect">指定領域</param>
        /// <param name="drawShape">図形描画有無</param>
        /// <param name="drawMask">マスク描画有無</param>
        /// <param name="legendImage">凡例画像</param>
        /// <returns>true:ファイルに保存OK</returns>
        public bool SaveClipImage(string filename, Rectangle rect, bool drawShape = true, bool drawMask = true, Bitmap legendImage = null)
        {
            if (base.Image != null)
            {
                Bitmap bmp = _Shapes.GetClip(base.Image, rect, 
                    ((drawMask) && (maskImage_ != null)) ? maskImage_ : null, 
                    legendImage);
                if (bmp != null)
                {
                    try
                    {
                        bmp.Save(filename);
                        return true;
                    }
                    catch { }
                    finally
                    {
                        bmp.Dispose();
                        bmp = null;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 図形が入る領域で画像を切り取り、ファイルに保存する
        /// </summary>
        /// <param name="filename">ファイル名</param>
        /// <param name="option">切り取りオプション</param>
        /// <param name="drawShape">図形描画有無</param>
        /// <param name="drawMask">マスク描画有無</param>
        /// <param name="legendImage">凡例画像</param>
        /// <returns>true:ファイルに保存OK</returns>
        public bool SaveClipImage(string filename, CLIP_OPTION option = CLIP_OPTION.NONE, bool drawShape = true, bool drawMask = true, 
            Bitmap legendImage = null,int clip_margin = 50)
        {
            if (base.Image != null)
            {
                Bitmap bmp = _Shapes.GetClip(base.Image, option, false,
                    ((drawMask) && (maskImage_ != null)) ? maskImage_ : null,
                    legendImage);
                if (bmp != null)
                {
                    try
                    {
                        bmp.Save(filename);
                        return true;
                    }
                    catch { }
                    finally
                    {
                        bmp.Dispose();
                        bmp = null;
                    }
                }
            }
            return false;

        }
        /// <summary>
        /// 登録図形から凡例用データを作成する
        /// </summary>
        /// <param name="visibleOnly">見えている図形のみか</param>
        /// <returns>凡例データリスト</returns>
        public List<LegendData> GetLegend(bool visibleOnly = false)
        {
            return _Shapes.GetLegend(visibleOnly);
        }

    }
}
