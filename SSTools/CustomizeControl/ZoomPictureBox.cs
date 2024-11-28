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

        public class PictureBoxInfo
        {
            public int X { get; private set; }
            public int Y { get; private set; }
            public int ImageX { get; private set; }
            public int ImageY { get; private set; }

            public PictureBoxInfo(int x, int y, int imgX, int imgY)
            {
                X = x;
                Y = y;
                ImageX = imgX;
                ImageY = imgY;
            }
        }
        public delegate void ZoomPictureBox_InfoEventHandler(object sender, PictureBoxInfo info);
        public event ZoomPictureBox_InfoEventHandler InfoEvent;
        protected void OnInfoEvent(int x, int y, int imgX, int imgY)
        {
            InfoEvent?.Invoke(this, new PictureBoxInfo(x, y, imgX, imgY));
        }
        public event ZoomPictureBox_InfoEventHandler ClickInfoEvent;
        protected void OnClickInfoEvent(int x, int y, int imgX, int imgY)
        {
            ClickInfoEvent?.Invoke(this, new PictureBoxInfo(x, y, imgX, imgY));
        }

        /// <summary>
        /// アフィン行列
        /// </summary>
        private readonly ZoomPictureBoxAffineMatrix affineMatrix_ = new ZoomPictureBoxAffineMatrix();

        private bool showImage_ = true;
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

        private bool shapeShow_ = true;
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
        /// X軸比率
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
        /// Y軸比率
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
        /// SizeMode
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

        public enum PictureBoxDrawMode
        {
            NORMAL = 0,
            AREA_SELECT,
            DRAW,
        }
        public Color AreaSelectColor { get; set; } = Color.YellowGreen;

        private PictureBoxDrawMode pictureBoxDrawMode_ = PictureBoxDrawMode.NORMAL;
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
        /// <param name="pe"></param>
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
                        g.DrawImage(maskImage_, 0, 0);
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
        /// <param name="e"></param>
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
        /// <param name="e"></param>
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
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            // ドラッグ中
            isMouseDown = true;
            // 座標値を保存
            beforeMousePosition = e.Location;
        }

        public delegate bool AreaConfirmEventHandler(object sender, List<Point> pointList);
        public event AreaConfirmEventHandler AreaConfirmEvent;
        protected virtual bool OnAreaConfirmEvent(List<Point> pointList)
        {
            if (AreaConfirmEvent != null)
                return AreaConfirmEvent(this, pointList);
            return true;
        }


        /// <summary>
        /// マウスアップイベント
        /// </summary>
        /// <param name="e"></param>
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
        /// <param name="e"></param>
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

        public IEnumerable<Shape.BaseShape> EnumerableShape()
        {
            return _Shapes.EnumerableShape();
        }
        /// <summary>
        /// 指定図形の一覧を取得(Enumrator)
        /// </summary>
        /// <typeparam name="T">図形</typeparam>
        /// <returns></returns>
        public IEnumerable<T> EnumerableShapes<T>() where T : BaseShape
        {
            return _Shapes.EnumerableShapes<T>();
        }
        /// <summary>
        /// 指定図形の一覧を取得(Enumrator)
        /// </summary>
        /// <param name="pattern">取得する図形名の正規表現</param>
        /// <returns></returns>
        public IEnumerable<BaseShape> EnumerableShapes(string pattern)
        {
            return _Shapes.EnumerableShapes(pattern);
        }

        public void AddShape(Shape.BaseShape shape)
        {
            _Shapes.AddShape(shape);
        }
        public void AddShape(IEnumerable<Shape.BaseShape> shapes)
        {
            _Shapes.AddShape(shapes);
        }
        public void AddShape(ShapeManager shapes)
        {
            _Shapes.AddShape(shapes);
        }

        /// <summary>
        /// 図形の名前一覧の取得
        /// </summary>
        /// <returns></returns>
        public List<string> GetShapeNames()
        {
            return _Shapes.GetShapeNames();
        }
        public bool ChangeColor(string name,Color color)
        {
            return _Shapes.SetShapeProperty(name,ShapeManager.SHAPE_PROPERTY.Color,color);
        }
        public bool ChangeVisible(string name, bool visible)
        {
            return (_Shapes.SetShapeProperty(name, ShapeManager.SHAPE_PROPERTY.Visible,visible));
        }

        public bool Remove(string name)
        {
           return _Shapes.Remove(name);
        }
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
        /// <param name="filename"></param>
        /// <param name="drawShape"></param>
        /// <param name="drawMask"></param>
        /// <returns></returns>

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
        /// <param name="drawShape"></param>
        /// <param name="drawMask"></param>
        /// <returns></returns>
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
        /// <param name="drawShape"></param>
        /// <param name="drawMask"></param>
        /// <returns></returns>
        private Bitmap GetImageToBitmap(bool drawShape = true, bool drawMask = true)
        {
            if (base.Image != null)
            {
                Bitmap bmp = new Bitmap(base.Image.Width, base.Image.Height, PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bmp);
                g.DrawImage(base.Image, 0, 0);

                if ((drawMask) && (maskImage_ != null))
                {
                    g.DrawImage(maskImage_, 0, 0);
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
        /// <param name="filename"></param>
        /// <param name="rect"></param>
        /// <param name="drawShape"></param>
        /// <param name="drawMask"></param>
        /// <param name="legendImage"></param>
        /// <returns></returns>
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
        /// <param name="filename"></param>
        /// <param name="option"></param>
        /// <param name="drawShape"></param>
        /// <param name="drawMask"></param>
        /// <param name="legendImage"></param>
        /// <returns></returns>
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
        /// <param name="visibleOnly"></param>
        /// <returns></returns>
        public List<LegendData> GetLegend(bool visibleOnly = false)
        {
            return _Shapes.GetLegend(visibleOnly);
        }

    }
}
