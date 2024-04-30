using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTools.Shape
{
    public class PointShape : BaseShape
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name"></param>
        public PointShape(string name) : base(name) { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="point">中心座標</param>
        /// <param name="color">色</param>
        /// <param name="lineWidth">線幅</param>
        /// <param name="dashStyle">線種</param>
        /// <param name="markerSize">マーカーサイズ</param>
        public PointShape(string name, Point point, Color? color = null, float? lineWidth = null, DashStyle? dashStyle = null,
            float? markerSize = null) : base(name,point, color, lineWidth, dashStyle, markerSize) 
        {
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="color">色</param>
        /// <param name="lineWidth">線幅</param>
        /// <param name="dashStyle">線種</param>
        /// <param name="markerSize">マーカーサイズ</param>
        public PointShape(string name, Color? color = null, float? lineWidth = null, DashStyle? dashStyle = null,
            float? markerSize = null) : base(name,color,lineWidth,dashStyle,markerSize)
        {
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
        public PointShape(string name, PointF point, Color? color = null, float? lineWidth = null, DashStyle? dashStyle = null,
            float? markerSize = null) : this(name, new Point((int)point.X,(int)point.Y), color, lineWidth, dashStyle, markerSize)
        {
        }
    }
}
