using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTools.Shape
{
    /// <summary>
    /// 矩形図形クラス
    /// </summary>
    public class RectangleShape : BaseShape
    {
        /// <summary>
        /// 矩形
        /// </summary>
        public RectangleF Rectangle { get; set; }
        /// <summary>
        /// 塗りつぶすかどうか
        /// </summary>
        public bool Fill { get; set; } = false;

        public bool ShowCenter { get; set; } = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">名前</param>
        public RectangleShape(string name) :base(name) { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="rectangle">矩形</param>
        /// <param name="isFill">塗りつぶすかどうか</param>
        /// <param name="color">線色</param>
        /// <param name="lineWidth">線幅</param>
        /// <param name="dashStyle">線種</param>
        /// <param name="fillColor">塗りつぶし色</param>
        public RectangleShape(string name, Rectangle rectangle,bool isFill = false, 
            Color? color = null, float? lineWidth = null,DashStyle? dashStyle = null, Color? fillColor = null ) :
            base(name, new Point((rectangle.Left + rectangle.Right)/2, (rectangle.Top + rectangle.Bottom)/2),
                color, lineWidth, dashStyle)
        {
            Rectangle = new RectangleF(rectangle.X,rectangle.Y,rectangle.Width,rectangle.Height);
            Fill = isFill;
            if (fillColor.HasValue)
                FillColor = fillColor.Value;
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="rectangle">矩形</param>
        /// <param name="isFill">塗りつぶすかどうか</param>
        /// <param name="color">線色</param>
        /// <param name="lineWidth">線幅</param>
        /// <param name="dashStyle">線種</param>
        /// <param name="fillColor">塗りつぶし色</param>
        public RectangleShape(string name, RectangleF rectangle, bool isFill = false,
            Color? color = null, float? lineWidth = null, DashStyle? dashStyle = null, Color? fillColor = null) :
            base(name, new Point((int)((rectangle.Left + rectangle.Right) / 2.0F),(int)((rectangle.Top + rectangle.Bottom) / 2.0F)),
                color, lineWidth, dashStyle)
        {
            Rectangle = new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            Fill = isFill;
            if (fillColor.HasValue)
                FillColor = fillColor.Value;
        }
        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="src"></param>
        public RectangleShape(RectangleShape src) : base(src) 
        {
            Rectangle = src.Rectangle;
            Fill = src.Fill;
            ShowCenter = src.ShowCenter;
        }
        /// <summary>
        /// クローンコピー
        /// </summary>
        public override BaseShape Clone()
        {
            return new RectangleShape(this);
        }

        /// <summary>
        /// 領域に図形が含まれるか
        /// </summary>
        /// <param name="rect">領域</param>
        /// <returns>true:含まれる</returns>
        /// <remarks>
        /// 座標系は画像座標系
        /// </remarks>
        public override bool IsContain(RectangleF rect)
        {
            return RectContains(rect, Rectangle);
        }
        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="g">グラフィックス</param>
        public override void Draw(Graphics g, SizeF? size)
        {
            if (Visible)
            {
                if (Fill)
                {   // 塗りつぶし
                    SolidBrush brush = new SolidBrush(GetDrawColor(COLOR_SELECT.FILL_COLOR));
                    g.FillRectangle(brush, Rectangle);
                    brush.Dispose();
                }
                // 外形を描画
                Pen pen = new Pen(GetDrawColor(COLOR_SELECT.NORMAL_COLOR), LineWidth)
                {
                    DashStyle = this.DashStyle
                };
                g.DrawRectangle(pen, Rectangle.X,Rectangle.Y,Rectangle.Width,Rectangle.Height);
                pen.Dispose();

                if (ShowCenter)
                {
                    DrawPoint(g, size, dashStyle: DashStyle.Solid);
                }
                if (ShowLable)
                {
                    DrawText(g, size, new PointF(Point.X, Point.Y));
                }
            }
        }
        /// <summary>
        /// テキスト表示位置の算出
        /// </summary>
        /// <param name="textSize">文字列表示サイズ</param>
        /// <param name="size">表示サイズ</param>
        /// <param name="pts">頂点座標リスト</param>
        /// <returns>テキスト表示領域</returns>

        protected override RectangleF CalcTextPosition(SizeF textSize, SizeF? size, PointF center)
        {
            // 表示位置とオフセットを計算
            CalcTextOffset(textSize, out int leftRight, out int topBottom, out int upDown, out float offsetX, out float offsetY);

            PointF pt = new PointF();
            // 横方向
            if (leftRight == 1)
                pt.X = Rectangle.Left + offsetX;    // 左端
            else if (leftRight == 2)
                pt.X = Rectangle.X + Rectangle.Width / 2.0F + offsetX;  // 中央
            else
                pt.X = Rectangle.Right + offsetX;   // 右端
            // 縦方向
            if (topBottom == 1)
                pt.Y = Rectangle.Top + offsetY;     // 上側
            else if (topBottom == 3)
                pt.Y = Rectangle.Bottom - offsetY;  // 下側(基準線上側が実際は矩形の外側となるので符号が反転)
            else
                pt.Y = Rectangle.Top + Rectangle.Height / 2.0F + offsetY;   // 中央

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



        public override Rectangle GetDrawSize()
        {
            return new Rectangle(
                (int)(Rectangle.X - LineWidth),
                (int)(Rectangle.Y - LineWidth),
                (int)(Rectangle.Width + LineWidth * 2.0),
                (int)(Rectangle.Height + LineWidth * 2.0));
        }

    }
}