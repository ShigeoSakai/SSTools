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
    /// 円,楕円クラス
    /// </summary>
    public class CircleShape : BaseShape
    {
        /// <summary>
        /// 塗りつぶすかどうか
        /// </summary>
        public bool Fill { get; set; } = false;
        /// <summary>
        /// 中心点を表示するか
        /// </summary>
        public bool ShowCenter { get; set; } = false;

        /// <summary>
        /// 外接矩形
        /// </summary>
        private RectangleF _enclosingRectangle = new RectangleF();
        /// <summary>
        /// 外接矩形(Rectangle)(プロパティ)
        /// </summary>
        public Rectangle EnclosingRectangle 
        {
            get {
                return new Rectangle((int)(_enclosingRectangle.X),
                    (int)(_enclosingRectangle.Y),
                    (int)(_enclosingRectangle.Width),
                    (int)(_enclosingRectangle.Height));
            }
            set {
                _enclosingRectangle.X = value.X;
                _enclosingRectangle.Y = value.Y;
                _enclosingRectangle.Width = value.Width;
                _enclosingRectangle.Height = value.Height;
                Point = new Point((int)(_enclosingRectangle.X + value.Width /2),
                    (int)(_enclosingRectangle.Y + value.Height / 2));
            }
        }
        /// <summary>
        /// 外接矩形(RectangleF)(プロパティ)
        /// </summary>
        public RectangleF EnclosingRectangleF
        {
            get => _enclosingRectangle;
            set
            {
                _enclosingRectangle = value;
                Point = new Point((int)(_enclosingRectangle.X + value.Width / 2.0F),
                    (int)(_enclosingRectangle.Y + value.Height / 2.0F));
            }
        }

        /// <summary>
        /// 横方向の半径
        /// </summary>
        public float RadiusX
        {
            get
            {
                if (EnclosingRectangle.IsEmpty == false)
                    return _enclosingRectangle.Width / 2.0F;
                else
                    return 0;
            }
            set
            {
                EnclosingRectangleF = new RectangleF(
                    Point.X - value / 2.0F, _enclosingRectangle.Y,
                    value, _enclosingRectangle.Height);
            }
        }
        /// <summary>
        /// 縦方向の半径
        /// </summary>
        public float RadiusY
        {
            get
            {
                if (_enclosingRectangle.IsEmpty == false)
                    return _enclosingRectangle.Height / 2.0F;
                else
                    return 0;
            }
            set
            {
                EnclosingRectangleF = new RectangleF(
                    _enclosingRectangle.X, Point.Y - value / 2.0F,
                    _enclosingRectangle.Width , value);
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">図形名</param>
        public CircleShape(string name) : base(name) { }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">図形名</param>
        /// <param name="color">色</param>
        /// <param name="lineWidth">線幅</param>
        /// <param name="dashStyle">線種</param>
        /// <param name="markerSize">マーカーサイズ</param>
        public CircleShape(string name, Color? color = null, float? lineWidth = null, DashStyle? dashStyle = null, float? markerSize = null)
            : base(name, color, lineWidth, dashStyle, markerSize) { }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">図形名</param>
        /// <param name="point">中心座標</param>
        /// <param name="color">色</param>
        /// <param name="lineWidth">線幅</param>
        /// <param name="dashStyle">線種</param>
        /// <param name="markerSize">マーカーサイズ</param>
        public CircleShape(string name, Point point, Color? color = null, float? lineWidth = null, DashStyle? dashStyle = null, float? markerSize = null)
            : base(name, point, color, lineWidth, dashStyle, markerSize) { }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">図形名</param>
        /// <param name="rect">外接矩形</param>
        /// <param name="color">色</param>
        /// <param name="lineWidth">線幅</param>
        /// <param name="dashStyle">線種</param>
        /// <param name="markerSize">マーカーサイズ</param>
        public CircleShape(string name, Rectangle rect, Color? color = null, float? lineWidth = null, DashStyle? dashStyle = null, float? markerSize = null)
            : base(name,  color, lineWidth, dashStyle, markerSize) 
        {
            EnclosingRectangle= rect;
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">図形名</param>
        /// <param name="rect">外接矩形</param>
        /// <param name="color">色</param>
        /// <param name="lineWidth">線幅</param>
        /// <param name="dashStyle">線種</param>
        /// <param name="markerSize">マーカーサイズ</param>
        public CircleShape(string name, RectangleF rect, Color? color = null, float? lineWidth = null, DashStyle? dashStyle = null, float? markerSize = null)
            : base(name, color, lineWidth, dashStyle, markerSize)
        {
            EnclosingRectangleF = rect;
        }
        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="src">コピー元</param>
        public CircleShape(CircleShape src) : base(src)
        {
            Fill = src.Fill;
            ShowCenter = src.ShowCenter;
            _enclosingRectangle = src._enclosingRectangle;
        }
        /// <summary>
        /// クローンコピー
        /// </summary>
        /// <returns>コピーされたオブジェクト</returns>
        public override BaseShape Clone()
        {
            return new CircleShape(this);
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
            return true;
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="g">グラフィックス</param>
        /// <param name="size">表示サイズ</param>
        public override void Draw(Graphics g, SizeF? size)
        {
            if (Visible)
            {
                if (_enclosingRectangle.IsEmpty == false)
                {
                    if (Fill)
                    {   // 塗りつぶし
                        SolidBrush brush = new SolidBrush(GetDrawColor(COLOR_SELECT.FILL_COLOR));
                        g.FillEllipse(brush, _enclosingRectangle);
                        brush.Dispose();
                    }
                    // 外形を描画
                    Pen pen = new Pen(GetDrawColor(COLOR_SELECT.NORMAL_COLOR), LineWidth)
                    {
                        DashStyle = this.DashStyle
                    };

                    g.DrawEllipse(pen, _enclosingRectangle);
                    pen.Dispose();
                }
                if (ShowCenter)
                {
                    DrawPoint(g, size, dashStyle: DashStyle.Solid);
                }
            }
        }
        /// <summary>
        /// 描画領域サイズを取得
        /// </summary>
        /// <returns>描画領域サイズ</returns>
        public override Rectangle GetDrawSize()
        {
            return new Rectangle(
                (int)(_enclosingRectangle.X - LineWidth),
                (int)(_enclosingRectangle.Y - LineWidth),
                (int)(_enclosingRectangle.Width + LineWidth * 2.0),
                (int)(_enclosingRectangle.Height + LineWidth * 2.0));
        }
    }
}
