using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSTools.Shape
{
    public class PolygonShape : BaseShape
    {
        /// <summary>
        /// 点群
        /// </summary>
        private readonly List<Point> points = new List<Point>();

        /// <summary>
        /// 閉じた図形か？
        /// </summary>
        public bool IsClose { get; set; } = true;
        /// <summary>
        /// 編集中か？
        /// </summary>
        public bool IsEditing { get; set; } = false;
        /// <summary>
        /// 編集中の最後の点座標
        /// </summary>
        public Point? ExtraPoint { get; set; } = null;
        /// <summary>
        /// 塗つぶすかどうか
        /// </summary>
        public bool Fill { get; set; } = false;
        /// <summary>
        /// 開始点
        /// </summary>
        public Point StartPoint
        {
            get
            {
                if (points.Count > 0)
                    return points[0];
                return new Point();
            }
        }
        /// <summary>
        /// 点座標の取得
        /// </summary>
        public List<Point> Points {  get { return points; } }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        /// <param name="name">名前</param>
        public PolygonShape(string name) : base(name) { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="pointList">点群</param>
        /// <param name="isClose">閉じた図形か</param>
        /// <param name="color">線色</param>
        /// <param name="lineWidth">線幅</param>
        /// <param name="dashStyle">線種</param>
        /// <param name="isFill">塗りつぶすか</param>
        /// <param name="fillColor">塗りつぶし色</param>
        public PolygonShape(string name,IEnumerable<Point> pointList,bool isClose = true ,Color? color = null,float? lineWidth = null,
            DashStyle? dashStyle = null,bool isFill = false,Color? fillColor = null) :
            base(name, color, lineWidth, dashStyle, null)
        {
            if (pointList != null)
                points.AddRange(pointList);
            IsClose = isClose;

            Fill = isFill;
            if (fillColor.HasValue) this.FillColor = fillColor.Value;
        }
        /// <summary>
        /// コンストラクタ(編集図形)
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="startPoint">開始座標</param>
        /// <param name="color">線色</param>
        /// <param name="lineWidth">線幅</param>
        /// <param name="dashStyle">線種</param>
        /// <param name="markerSize">マーカーサイズ</param>
        public PolygonShape(string name,Point startPoint, Color? color = null, float? lineWidth = null,
            DashStyle? dashStyle = null, float? markerSize = null):
            base(name, color, lineWidth, dashStyle , markerSize)
        {
            points.Add(startPoint);
            IsClose = false;
            IsEditing = true;
        }

        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="src"></param>
        public PolygonShape(PolygonShape src) : base(src) 
        {
            points = new List<Point>(src.Points);
            IsClose = src.IsClose ;
            IsEditing = src.IsEditing ;
            ExtraPoint = src.ExtraPoint ;
            Fill = src.Fill ;
        }
        /// <summary>
        /// クローンコピー
        /// </summary>
        public override BaseShape Clone()
        {
            return new PolygonShape(this);
        }

        /// <summary>
        /// 座標の追加
        /// </summary>
        /// <param name="point">追加点座標</param>
        /// <returns>座標点数・編集モード以外の時は-1</returns>
        public int AddPoint(Point point)
        {
            if (IsEditing)
            {
                points.Add(point);
                return points.Count;
            }
            return -1;
        }
        /// <summary>
        /// 外接矩形の取得
        /// </summary>
        /// <returns>外接矩形</returns>
        private Rectangle GetBoundingRect()
        {
            Point topLeft = new Point(int.MaxValue, int.MaxValue);
            Point bottomRight = new Point(0, 0);
            foreach (Point p in points)
            {
                if (topLeft.X > p.X) topLeft.X = p.X;
                if (topLeft.Y > p.Y) topLeft.Y = p.Y;
                if (bottomRight.X < p.X) bottomRight.X = p.X;
                if (bottomRight.Y < p.Y) bottomRight.Y = p.Y;
            }
            return new Rectangle(topLeft.X,topLeft.Y,bottomRight.X - topLeft.X,bottomRight.Y - topLeft.Y);
        }
        /// <summary>
        /// 領域に含まれるか？
        /// </summary>
        /// <param name="rect">領域</param>
        /// <returns>true;含まれる</returns>
        public override bool IsContain(RectangleF rect)
        {
            // 各点が領域に含まれるか？
            foreach (Point p in points)
            {
                if (rect.Contains(p)) return true;
                if (IsEditing)
                {
                    if (RectContains(rect,new Rectangle((int)(p.X - MarkerSize),(int)(p.Y - MarkerSize),(int)(MarkerSize * 2),(int)(MarkerSize * 2))))
                        return true;
                }
            }
            // 外接矩形が領域に含まれるか？
            Rectangle boundingRect = GetBoundingRect();
            return RectContains(rect, boundingRect);
        }
        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="g">グラフィックス</param>
        public override void Draw(Graphics g, SizeF? size)
        {
            if (Visible)
            {
                if ((points != null) && (points.Count > 0))
                {
                    // 塗りつぶし指定
                    if (Fill)
                    {
                        if (points.Count > 2)
                        {
                            SolidBrush brush = new SolidBrush(GetDrawColor(COLOR_SELECT.FILL_COLOR));
                            g.FillPolygon(brush, points.ToArray());
                            brush.Dispose();
                        }
                    }
                    // 線分を描画
                    Pen pen = new Pen(GetDrawColor(COLOR_SELECT.NORMAL_COLOR), LineWidth)
                    {
                        DashStyle = this.DashStyle,
                    };
                    Point before = points[0];
                    if (IsEditing)
                        g.DrawRectangle(pen, before.X - MarkerSize, before.Y - MarkerSize, MarkerSize * 2.0F, MarkerSize * 2.0F);
                    for (int i = 1; i < points.Count; i++)
                    {
                        g.DrawLine(pen, before, points[i]);
                        if (IsEditing)
                            g.DrawRectangle(pen, points[i].X - MarkerSize, points[i].Y - MarkerSize, MarkerSize * 2.0F, MarkerSize * 2.0F);
                        before = points[i];
                    }
                    if ((IsEditing) && (ExtraPoint.HasValue))
                    {
                        g.DrawLine(pen, before, ExtraPoint.Value);
                    }
                    // 閉じた図形か？
                    if (IsClose)
                        g.DrawLine(pen, points.Last(), points[0]);
                    pen.Dispose();
                }
            }
        }
        public override Rectangle GetDrawSize()
        {
            Rectangle rect = GetBoundingRect();
            return new Rectangle(
                (int)(rect.X - LineWidth),
                (int)(rect.Y - LineWidth),
                (int)(rect.Width + LineWidth * 2.0),
                (int)(rect.Height + LineWidth * 2.0));
        }
    }
}
