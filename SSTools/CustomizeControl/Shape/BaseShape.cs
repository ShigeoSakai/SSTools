using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSTools.Shape
{
    /// <summary>
    /// 図形基本クラス
    /// </summary>
    public class BaseShape
    {
        public enum MARKER_TYPE
        {
            POINT = 0,
            PLUS = 1,
            CROSS = 2,
            CIRCLE = 3,
        }


        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 中心座標
        /// </summary>
        public Point Point { get; set; }
        /// <summary>
        /// 色
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// 塗りつぶし色
        /// </summary>
        public Color? FillColor { get; set; } = Color.White;

        /// <summary>
        /// 線幅
        /// </summary>
        public float LineWidth { get; set; } = 1.0F;
        /// <summary>
        /// マーカーサイズ
        /// </summary>
        public float MarkerSize { get; set; } = 10.0F;
        /// <summary>
        /// 線種
        /// </summary>
        public DashStyle DashStyle { get; set; } = DashStyle.Solid;
        /// <summary>
        /// ラベル表示
        /// </summary>
        public bool ShowLable { get; set; } = false;
        /// <summary>
        /// 表示文字列
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// ラベル表示位置
        /// </summary>
        public LABEL_POSITION LabelPosition { get; set; } = LABEL_POSITION.TOP_LEFT;
        /// <summary>
        /// ラベルフォント
        /// </summary>
        public Font LabelFont { get; set; } = new Font(FontFamily.GenericSansSerif, 10.0F);
        /// <summary>
        /// ラベル文字色
        /// </summary>
        public Color? LabelColor { get; set; }
        /// <summary>
        /// ラベル塗りつぶし
        /// </summary>
        public bool LabelFill { get; set; } = false;
        /// <summary>
        /// ラベル塗りつぶし色
        /// </summary>
        public Color? LabelFillColor { get; set; }
        /// <summary>
        /// ラベル枠
        /// </summary>
        public bool LabelBorder { get; set; } = false;
        /// <summary>
        /// ラベル枠色
        /// </summary>
        public Color? LabelBorderColor { get; set; }

        public bool Visible { get; set; } = true;

        public MARKER_TYPE MarkerType { get; set; } = MARKER_TYPE.CROSS;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">名前</param>
        public BaseShape(string name) 
        {
            Name = name;
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="point">中心座標</param>
        /// <param name="color">色</param>
        /// <param name="lineWidth">線幅</param>
        /// <param name="dashStyle">線種</param>
        /// <param name="markerSize">マーカーサイズ</param>
        public BaseShape(string name, Point point, Color? color = null,float? lineWidth = null,DashStyle? dashStyle = null,
            float? markerSize = null) : this(name ,color, lineWidth, dashStyle, markerSize)
        {
            Point = point;
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="color">色</param>
        /// <param name="lineWidth">線幅</param>
        /// <param name="dashStyle">線種</param>
        /// <param name="markerSize">マーカーサイズ</param>
        public BaseShape(string name, Color? color = null, float? lineWidth = null, DashStyle? dashStyle = null,
            float? markerSize = null) : this(name)
        {
            if (color.HasValue)
                Color = color.Value;
            if (lineWidth.HasValue)
                LineWidth = lineWidth.Value;
            if (dashStyle.HasValue)
                DashStyle = dashStyle.Value;
            if (markerSize.HasValue)
                MarkerSize = markerSize.Value;
        }
        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="src"></param>
        public BaseShape(BaseShape src)
        {
            Name = src.Name;
            Point = src.Point;
            Color = src.Color;
            FillColor = src.FillColor;
            LineWidth = src.LineWidth;
            MarkerSize = src.MarkerSize;
            MarkerType = src.MarkerType;
            DashStyle = src.DashStyle;
            ShowLable = src.ShowLable;
            Text = src.Text;
            LabelPosition = src.LabelPosition;
            LabelFont = src.LabelFont;
            LabelColor = src.LabelColor;
            LabelFill = src.LabelFill;
            LabelFillColor = src.LabelFillColor;
            LabelBorder = src.LabelBorder;
            LabelBorderColor = src.LabelBorderColor;
            Visible = src.Visible;
        }
        /// <summary>
        /// クローンコピー
        /// </summary>
        /// <returns></returns>
        public virtual BaseShape Clone()
        {
            return new BaseShape(this);
        }

        /// <summary>
        /// 領域に図形が含まれるか
        /// </summary>
        /// <param name="rect">領域</param>
        /// <returns>true:含まれる</returns>
        /// <remarks>
        /// 座標系は画像座標系
        /// </remarks>
        public virtual bool IsContain(RectangleF rect)
        {
            return rect.Contains(Point.X, Point.Y);
        }
        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="g">グラフィックス</param>
        public virtual void Draw(Graphics g, SizeF? size)
        {
            if (Visible)
            {
                Pen pen = new Pen(GetDrawColor(COLOR_SELECT.NORMAL_COLOR), LineWidth)
                {
                    DashStyle = this.DashStyle,
                };
                SolidBrush brush = new SolidBrush(GetDrawColor(COLOR_SELECT.NORMAL_COLOR));

                switch (MarkerType)
                {
                    case MARKER_TYPE.PLUS:
                        g.DrawLine(pen, new PointF(Point.X - MarkerSize / 2.0F, Point.Y),
                            new PointF(Point.X + MarkerSize / 2.0F, Point.Y));
                        g.DrawLine(pen, new PointF(Point.X, Point.Y - MarkerSize / 2.0F),
                            new PointF(Point.X, Point.Y + MarkerSize / 2.0F));
                        break;
                    case MARKER_TYPE.CROSS:
                        g.DrawLine(pen, new PointF(Point.X - MarkerSize / 2.0F, Point.Y - MarkerSize / 2.0F),
                            new PointF(Point.X + MarkerSize / 2.0F, Point.Y + MarkerSize / 2.0F));
                        g.DrawLine(pen, new PointF(Point.X + MarkerSize / 2.0F, Point.Y - MarkerSize / 2.0F),
                            new PointF(Point.X - MarkerSize / 2.0F, Point.Y + MarkerSize / 2.0F));
                        break;
                    case MARKER_TYPE.CIRCLE:
                        g.FillEllipse(brush, new RectangleF(Point.X - MarkerSize / 2.0F, Point.Y - MarkerSize / 2.0F, MarkerSize, MarkerSize));
                        break;
                    case MARKER_TYPE.POINT:
                    default:
                        g.FillEllipse(brush, new RectangleF(Point.X - 1.0F, Point.Y - 1.0F, 2.0F, 2.0F));
                        break;
                }
                brush.Dispose();


                if (ShowLable)
                {
                    // ラベル文字列の描画
                    DrawText(g, size, new PointF(Point.X, Point.Y));
                }

                pen.Dispose();
            }
        }

        protected void DrawPoint(Graphics g, SizeF? size,
            PointF? point = null, Color? color = null, float? lineWidth = null,
            DashStyle? dashStyle = null,float? markerSize = null,MARKER_TYPE? markerType = null)
        {
            PointF pts = point.HasValue ? point.Value : new PointF(this.Point.X, this.Point.Y);
            Color penColor = color.HasValue ? color.Value : GetDrawColor(COLOR_SELECT.NORMAL_COLOR);
            float lWidth = lineWidth.HasValue ? lineWidth.Value : this.LineWidth;
            DashStyle dash = dashStyle.HasValue ? dashStyle.Value : this.DashStyle;
            float mSize = markerSize.HasValue ? markerSize.Value : this.MarkerSize;
            MARKER_TYPE mType = markerType.HasValue ? markerType.Value : this.MarkerType;


            Pen pen = new Pen(penColor, lWidth)
            {
                DashStyle = dash,
            };
            SolidBrush brush = new SolidBrush(penColor);

            switch (mType)
            {
                case MARKER_TYPE.PLUS:
                    g.DrawLine(pen, new PointF(pts.X - mSize / 2.0F, pts.Y),
                        new PointF(pts.X + mSize / 2.0F, pts.Y));
                    g.DrawLine(pen, new PointF(pts.X, pts.Y - mSize / 2.0F),
                        new PointF(pts.X, pts.Y + mSize / 2.0F));
                    break;
                case MARKER_TYPE.CROSS:
                    g.DrawLine(pen, new PointF(pts.X - mSize / 2.0F, pts.Y - mSize / 2.0F),
                        new PointF(pts.X + mSize / 2.0F, pts.Y + mSize / 2.0F));
                    g.DrawLine(pen, new PointF(pts.X + mSize / 2.0F, pts.Y - mSize / 2.0F),
                        new PointF(pts.X - mSize / 2.0F, pts.Y + mSize / 2.0F));
                    break;
                case MARKER_TYPE.CIRCLE:
                    g.FillEllipse(brush, new RectangleF(pts.X - mSize / 2.0F, pts.Y - mSize / 2.0F, mSize, mSize));
                    break;
                case MARKER_TYPE.POINT:
                default:
                    g.FillEllipse(brush, new RectangleF(pts.X - 1.0F, pts.Y - 1.0F, 2.0F, 2.0F));
                    break;
            }
            brush.Dispose();
            pen.Dispose();
        }


        /// <summary>
        /// 矩形が領域に含まれるか
        /// </summary>
        /// <param name="targetRect">領域</param>
        /// <param name="baseRect">矩形</param>
        /// <returns>true:含まれる</returns>

        protected bool RectContains(RectangleF targetRect,Rectangle baseRect)
        {
            if ((targetRect.Contains(baseRect.Left, baseRect.Top)) ||
                (targetRect.Contains(baseRect.Right, baseRect.Top)) ||
                (targetRect.Contains(baseRect.Left, baseRect.Bottom)) ||
                (targetRect.Contains(baseRect.Right, baseRect.Bottom)))
                return true;
            if ((baseRect.Contains((int)targetRect.Left, (int)targetRect.Top)) ||
                (baseRect.Contains((int)targetRect.Right, (int)targetRect.Top)) ||
                (baseRect.Contains((int)targetRect.Left, (int)targetRect.Bottom)) ||
                (baseRect.Contains((int)targetRect.Right, (int)targetRect.Bottom)))
                return true;
            if (((targetRect.Top <= baseRect.Top) && (baseRect.Top <= targetRect.Bottom)) ||
                ((targetRect.Bottom <= baseRect.Top) && (baseRect.Bottom <= targetRect.Bottom)) ||
                ((targetRect.Left <= baseRect.Left) && (baseRect.Left <= targetRect.Right)) ||
                ((targetRect.Left <= baseRect.Right) && (baseRect.Right <= targetRect.Right)))
                return true;
            return false;

        }
        /// <summary>
        /// 色の選択
        /// </summary>
        protected enum COLOR_SELECT
        {
            NORMAL_COLOR,
            FILL_COLOR,
            TEXT_COLOR,
            TEXT_FILL_COLOR,
            TEXT_BORDER_COLOR
        }

        /// <summary>
        /// 描画色を取得
        /// </summary>
        /// <returns></returns>
        protected Color GetDrawColor(COLOR_SELECT colorSelect)
        {
            Color drawColor;

            switch (colorSelect)
            {
                case COLOR_SELECT.FILL_COLOR:
                    if (FillColor.HasValue)
                        drawColor = FillColor.Value;
                    else
                        drawColor = Color.FromArgb(127, Color);
                    break;
                case COLOR_SELECT.TEXT_COLOR:
                    if (LabelColor.HasValue)
                        drawColor = LabelColor.Value;
                    else if (LabelBorderColor.HasValue)
                        drawColor = LabelBorderColor.Value;
                    else if (LabelFillColor.HasValue)
                        drawColor = Color.FromArgb((byte)(~LabelFillColor.Value.R), (byte)(~LabelFillColor.Value.G), (byte)(~LabelFillColor.Value.B));
                    else
                        drawColor = Color;
                    break;
                case COLOR_SELECT.TEXT_FILL_COLOR:
                    if (LabelFillColor.HasValue)
                        drawColor = LabelFillColor.Value;
                    else if (FillColor.HasValue)
                        drawColor = FillColor.Value;
                    else if (LabelColor.HasValue)
                        drawColor = Color.FromArgb((byte)(~LabelColor.Value.R), (byte)(~LabelColor.Value.G), (byte)(~LabelColor.Value.B));
                    else if (LabelBorderColor.HasValue)
                        drawColor = Color.FromArgb((byte)(~LabelBorderColor.Value.R), (byte)(~LabelBorderColor.Value.G), (byte)(~LabelBorderColor.Value.B));
                    else
                        drawColor = Color.FromArgb(127, Color);
                    break;
                case COLOR_SELECT.TEXT_BORDER_COLOR:
                    if (LabelBorderColor.HasValue)
                        drawColor = LabelBorderColor.Value;
                    else if (LabelColor.HasValue)
                        drawColor = LabelColor.Value;
                    else if (LabelFillColor.HasValue)
                        drawColor = Color.FromArgb((byte)(~LabelFillColor.Value.R), (byte)(~LabelFillColor.Value.G), (byte)(~LabelFillColor.Value.B));
                    else
                        drawColor = Color;
                    break;
                default:
                    drawColor = Color;
                    break;
            }
            return drawColor;
        }


        /// <summary>
        /// 文字列表示位置からオフセットを算出
        /// </summary>
        /// <param name="textSize">文字列表示サイズ</param>
        /// <param name="leftRight">1:左,2:中央,3:右</param>
        /// <param name="topBottom">1:上,2;中央,3:下</param>
        /// <param name="upDown">0:基準線より上,1:基準線より下</param>
        /// <param name="offsetX">オフセットX</param>
        /// <param name="offsetY">オフセットY</param>
        protected void CalcTextOffset(SizeF textSize, out int leftRight, out int topBottom, out int upDown, out float offsetX, out float offsetY)
        {
            // 文字列表示位置の分解
            leftRight = (int)LabelPosition & 0x00F;
            topBottom = ((int)LabelPosition & 0x0F0) >> 4;
            upDown = ((int)LabelPosition & 0xF00) >> 16;

            if (leftRight == 1)
                offsetX = 0;
            else if (leftRight == 2)
                offsetX = -textSize.Width / 2.0F;
            else
                offsetX = -textSize.Width;

            if (topBottom == 2)
                offsetY = -textSize.Height / 2.0F;
            else if (upDown == 0)
                offsetY = -textSize.Height;
            else
                offsetY = 0;
        }

        /// <summary>
        /// テキスト表示位置の算出
        /// </summary>
        /// <param name="textSize">文字列表示サイズ</param>
        /// <param name="size">表示サイズ</param>
        /// <param name="pts">頂点座標リスト</param>
        /// <returns>テキスト表示領域</returns>
        protected virtual RectangleF CalcTextPosition(SizeF textSize, SizeF? size, PointF center)
        {
            // 表示位置とオフセットを計算
            CalcTextOffset(textSize, out int leftRight, out int topBottom, out int upDown, out float offsetX, out float offsetY);

            // リストの最後が中心点
            PointF pt = new PointF(center.X + offsetX, center.Y + offsetY);

            if (topBottom == 1)
                pt.Y = pt.Y - MarkerSize / 2.0F;
            else if (topBottom == 3)
                pt.Y = pt.Y + MarkerSize / 2.0F;


            // はみ出す場合の処理
            if ((size.HasValue) && (pt.X + offsetX >= size.Value.Width))
                pt.X = size.Value.Width - textSize.Width;
            if (pt.X + offsetX < 0.0F)
                pt.X = 0;
            if ((size.HasValue) && (pt.Y + offsetY >= size.Value.Height))
                pt.Y = size.Value.Height - textSize.Height;
            if (pt.Y + offsetY < 0.0F)
                pt.Y = 0;

            return new RectangleF(pt, textSize);
        }
        /// <summary>
        /// 文字列の描画
        /// </summary>
        /// <param name="graphics">グラフィック</param>
        /// <param name="size">表示サイズ</param>
        /// <param name="pts">頂点座標リスト</param>
        protected void DrawText(Graphics graphics, SizeF? size, PointF center)
        {
            if ((ShowLable) && (Text != null))
            {
                // 文字列の描画サイズ
                SizeF textSize = TextRenderer.MeasureText(graphics, Text, LabelFont);
                // 描画サイズ
                RectangleF textRect = CalcTextPosition(textSize, size, center);

                if ((textRect.X >= 0) || ((size.HasValue) && (textRect.X < size.Value.Width)) ||
                    (textRect.Y >= 0) || ((size.HasValue) && (textRect.Y < size.Value.Height)) ||
                    (textRect.X + textRect.Width >= 0) || ((size.HasValue) && (textRect.X + textRect.Width < size.Value.Width)) ||
                    (textRect.Y + textRect.Height >= 0) || ((size.HasValue) && (textRect.Y + textRect.Height < size.Value.Width)))
                {
                    if (LabelFill)
                    {   // 塗りつぶし
                        SolidBrush brush = new SolidBrush(GetDrawColor(COLOR_SELECT.TEXT_FILL_COLOR));
                        graphics.FillRectangle(brush, textRect);
                        brush.Dispose();
                    }
                    if (LabelBorder)
                    {   // 枠線
                        Pen pen = new Pen(GetDrawColor(COLOR_SELECT.TEXT_BORDER_COLOR), 1.0F);
                        graphics.DrawRectangle(pen, textRect.X, textRect.Y, textRect.Width, textRect.Height);
                        pen.Dispose();
                    }
                    // 文字列の描画
                    SolidBrush textBrush = new SolidBrush(GetDrawColor(COLOR_SELECT.TEXT_COLOR));
                    graphics.DrawString(Text, LabelFont, textBrush, textRect.X, textRect.Y);
                    textBrush.Dispose();
                }
            }
        }
        public virtual Rectangle GetDrawSize()
        {
            if (MarkerType == MARKER_TYPE.POINT)
                return new Rectangle(Point.X, Point.Y, (int)(LineWidth * 2.0F), (int)(LineWidth * 2.0F));
            else
                return new Rectangle(
                    (int)(Point.X - MarkerSize / 2.0F - LineWidth),
                    (int)(Point.Y - MarkerSize / 2.0F - LineWidth),
                    (int)(MarkerSize + LineWidth * 2.0),
                    (int)(MarkerSize + LineWidth * 2.0));
        }

    }
}
