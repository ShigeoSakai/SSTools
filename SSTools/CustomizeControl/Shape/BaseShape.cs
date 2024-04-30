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
    /// 図形基本クラス
    /// </summary>
    public class BaseShape
    {
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
        public virtual void Draw(Graphics g)
        {
            Pen pen = new Pen(Color, LineWidth) { 
                DashStyle = this.DashStyle,
            };
            g.DrawLine(pen, new PointF(Point.X - MarkerSize / 2.0F, Point.Y - MarkerSize / 2.0F),
                new PointF(Point.X + MarkerSize / 2.0F, Point.Y + MarkerSize / 2.0F));
            g.DrawLine(pen, new PointF(Point.X + MarkerSize / 2.0F, Point.Y - MarkerSize / 2.0F),
                new PointF(Point.X - MarkerSize / 2.0F, Point.Y + MarkerSize / 2.0F));
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
    }
}
