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
        public Rectangle Rectangle { get; set; }
        /// <summary>
        /// 塗りつぶすかどうか
        /// </summary>
        public bool Fill { get; set; } = false;
        /// <summary>
        /// 塗りつぶし色
        /// </summary>
        public Color FillColor { get; set; } = Color.White;

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
            Rectangle = rectangle;
            Fill = isFill;
            if (fillColor.HasValue)
                FillColor = fillColor.Value;
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
        public override void Draw(Graphics g)
        {
            if (Fill)
            {   // 塗りつぶし
                SolidBrush brush = new SolidBrush(FillColor);
                g.FillRectangle(brush, Rectangle);
                brush.Dispose();
            }
            // 外形を描画
            Pen pen = new Pen(Color, LineWidth) { 
                DashStyle = this.DashStyle
            };
            g.DrawRectangle(pen, Rectangle);
            pen.Dispose();
        }
    }
}
