using SSTools;
using SSTools.Shape;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using static SSTools.ZoomPictureBox;
using System.Reflection;
using static SSTools.Shape.BaseShape;
using static System.Net.Mime.MediaTypeNames;


namespace SSTools
{
    /// <summary>
    /// 図形管理
    /// </summary>
    public class ShapeManager
    {
        /// <summary>
        /// 図形辞書
        /// </summary>
        private Dictionary<string,BaseShape> shapeDictionary = new Dictionary<string, BaseShape> ();
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ShapeManager() { }
        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="src">コピー元</param>
        public ShapeManager(ShapeManager src)
        {
            foreach(KeyValuePair<string,BaseShape> pair in src.shapeDictionary)
                shapeDictionary.Add(pair.Key, pair.Value.Clone());
        }
        /// <summary>
        /// クローンコピー
        /// </summary>
        /// <returns>コピーされたオブジェクト</returns>
        public ShapeManager Clone()
        { 
            return new ShapeManager(this); 
        }
        /// <summary>
        /// 登録件数
        /// </summary>
        public int Count { get=> shapeDictionary.Count; }

        /// <summary>
        /// 図形の一覧(Enumerator)
        /// </summary>
        /// <returns>図形</returns>
        public IEnumerable<BaseShape> EnumerableShape()
        {
            foreach (BaseShape shape in shapeDictionary.Values)
                yield return shape;
        }
        /// <summary>
        /// 図形の追加
        /// </summary>
        /// <param name="shape">図形</param>
        public void AddShape(BaseShape shape)
        {
            if (shapeDictionary.ContainsKey(shape.Name))
            {   // 同じ名前は削除
                shapeDictionary.Remove(shape.Name);
            }
            // 辞書に追加
            shapeDictionary.Add(shape.Name, shape);
        }
        /// <summary>
        /// 図形の追加
        /// </summary>
        /// <param name="shapes">図形</param>
        public void AddShape(IEnumerable<BaseShape> shapes)
        {
            if ((shapes != null) && (shapes.Count() > 0))
            {
                foreach (BaseShape shape in shapes)
                    AddShape(shape);
            }
        }
        /// <summary>
        /// 図形の追加
        /// </summary>
        /// <param name="src">図形管理マネージャー</param>
        public void AddShape(ShapeManager src)
        {
            if ((src != null) && (src.Count > 0))
                foreach(BaseShape shape in src.EnumerableShape())
                    shapeDictionary.Add(shape.Name, shape.Clone());
        }


        /// <summary>
        /// 図形の名前一覧の取得
        /// </summary>
        /// <returns>図形の名前一覧</returns>
        public List<string> GetShapeNames()
        {
            List<string> names = new List<string>();
            foreach (string key in shapeDictionary.Keys)
                names.Add(key);
            return names;
        }
        /// <summary>
        /// 図形の削除
        /// </summary>
        /// <param name="name">名前</param>
        /// <returns>true:削除された</returns>
        public bool Remove(string name)
        {
            if (shapeDictionary.ContainsKey(name))
            {
                return shapeDictionary.Remove(name);
            }
            return false;
        }
        /// <summary>
        /// 図形のクリア
        /// </summary>
        public void ClearShape()
        {
            shapeDictionary.Clear();
        }
        /// <summary>
        /// 描画(float)
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="imageSize">画像サイズ</param>
        /// <param name="rect">描画領域</param>
        /// <returns>true:図形を描画した</returns>
        public bool Draw(Graphics g ,SizeF? imageSize = null, RectangleF? rect = null)
        {
            // 描画領域
            bool isDraw = false;
            foreach (BaseShape shape in shapeDictionary.Values)
            {
                if ((rect.HasValue == false) || (shape.IsContain(rect.Value)))
                {
                    isDraw = true;
                    shape.Draw(g, imageSize);
                }
            }
            return isDraw;
        }
        /// <summary>
        /// 描画(int)
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="imageSize">画像サイズ</param>
        /// <param name="rect">描画領域</param>
        /// <returns>true:図形を描画した</returns>
        public bool Draw(Graphics g, Size? imageSize = null, Rectangle? rect = null)
        {
            return Draw(g,
                (imageSize.HasValue) ? new SizeF(imageSize.Value.Width, imageSize.Value.Height) : (SizeF?)null,
                (rect.HasValue) ? new RectangleF(rect.Value.X, rect.Value.Y, rect.Value.Width, rect.Value.Height) : (RectangleF?)null);
        }
        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="bmp">元画像</param>
        /// <returns>描画後の画像</returns>
        public Bitmap Draw(Bitmap bmp)
        {
            if (bmp != null)
            {
                Graphics g = Graphics.FromImage(bmp);
                Draw(g,bmp.Size);
                g.Dispose();
                return bmp;
            }
            return null;
        }
        /// <summary>
        /// クリッピングした画像を取得
        /// </summary>
        /// <param name="bmp">元画像</param>
        /// <param name="rect">クリッピング領域</param>
        /// <param name="mask">マスク画像</param>
        /// <param name="legendImage">凡例画像</param>
        /// <returns>クリッピングした画像</returns>
        public Bitmap GetClip(System.Drawing.Image bmp,  Rectangle rect, System.Drawing.Image mask = null, Bitmap legendImage = null)
        {
            int imgWidth = rect.Width;
            int imgHeight = rect.Height;
            if (legendImage != null)
            {
                imgHeight += legendImage.Height;
                if (imgWidth < legendImage.Width)
                    imgWidth = legendImage.Width;
            }
            Bitmap resultBmp = new Bitmap(imgWidth, imgHeight, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(resultBmp);
            Matrix affineMatrix = new Matrix(1.0F, 0.0F, 0.0F, 1.0F, (float)(-rect.X), (float)(-rect.Y));
            g.Transform = affineMatrix;
            
            // 元画像
            g.DrawImage(bmp, 0, 0);
            // マスク
            if (mask != null)
                g.DrawImage(mask, 0, 0);

            // 描画
            if (shapeDictionary.Count > 0)
                Draw(g,resultBmp.Size);

            // 凡例
            if (legendImage != null)
            {
                g.DrawImage(legendImage, rect.X + imgWidth - legendImage.Width, rect.Bottom);
            }
            g.Dispose();
            return resultBmp;
        }

        /// <summary>
        /// 図形がある領域のクリッピングした画像を取得
        /// </summary>
        /// <param name="bmp">元画像</param>
        /// <param name="option">クリップする時のオプション</param>
        /// <param name="visible_only">表示図形のみか</param>
        /// <param name="mask">マスク画像</param>
        /// <param name="legendImage">凡例画像</param>
        /// <param name="clip_margin">マージン</param>
        /// <returns>クリッピングした画像</returns>
        public Bitmap GetClip(System.Drawing.Image bmp, CLIP_OPTION option = CLIP_OPTION.NONE,
            bool visible_only = true,
            System.Drawing.Image mask = null,
            Bitmap legendImage = null, int clip_margin = 50)
        {
            if (shapeDictionary.Count > 0)
            {
                int minX = int.MaxValue, minY = int.MaxValue;
                int maxX = -1, maxY = -1;

                foreach (BaseShape shape in shapeDictionary.Values)
                {
                    if ((visible_only == false) ||
                        (shape.Visible))
                    {
                        Rectangle rect = shape.GetDrawSize();
                        if (rect.Left < minX) minX = rect.Left;
                        if (rect.Top < minY) minY = rect.Top;
                        if (rect.Right > maxX) maxX = rect.Right;
                        if (rect.Bottom > maxY) maxY = rect.Bottom;
                    }
                }
                if ((minX < int.MaxValue) && (minY < int.MaxValue) &&
                    (maxX >= 0) && (maxY >= 0) &&
                    (maxX - minX > 0) && (maxY - minY > 0))
                {   // マージン
                    minX -= clip_margin; if (minX < 0) minX = 0;
                    maxX += clip_margin; if (maxX > bmp.Width) maxX = bmp.Width;
                    minY -= clip_margin; if (minY < 0) minY = 0;
                    maxY += clip_margin; if (maxY > bmp.Height) maxY = bmp.Height;
                    if (option == CLIP_OPTION.ORIGINAL_WIDTH)
                    {
                        minX = 0; maxX = bmp.Width;
                    }
                    else if (option == CLIP_OPTION.ORIGINAL_HEIGHT)
                    {
                        minY = 0; maxY = bmp.Height;
                    }

                    return GetClip(bmp, new Rectangle(minX, minY, maxX - minX, maxY - minY),mask, legendImage);
                }
            }
            if (bmp is Bitmap bitmap)
                return bitmap;
            return new Bitmap(bmp);
        }

        /// <summary>
        /// 登録図形から凡例用データを作成する
        /// </summary>
        /// <param name="visibleOnly">表示図形のみか</param>
        /// <returns>凡例用データリスト</returns>
        public List<LegendData> GetLegend(bool visibleOnly = false)
        {
            if (shapeDictionary.Count > 0)
            {
                List<LegendData> list = new List<LegendData>();
                foreach (KeyValuePair<string, BaseShape> pair in shapeDictionary)
                {
                    if ((visibleOnly == false) || (pair.Value.Visible))
                    {
                        list.Add(new LegendData(pair.Key, pair.Value.Color,pair.Value.Text, pair.Value.GetType()));
                    }
                }
                if (list.Count > 0)
                {
                    list.Sort((a, b) => string.Compare(a.Name, b.Name));
                    return list;
                }
            }
            return null;
        }
        /// <summary>
        /// 指定図形の一覧を取得(Enumrator)
        /// </summary>
        /// <typeparam name="T">図形</typeparam>
        /// <returns>図形</returns>
        public IEnumerable<T> EnumerableShapes<T>() where T:BaseShape
        {
            foreach (BaseShape shape in shapeDictionary.Values)
            {
                if (typeof(T) == shape.GetType())
                    yield return (T)shape;
            }
        }
        /// <summary>
        /// 指定図形の一覧を取得(Enumrator)
        /// </summary>
        /// <param name="pattern">取得する図形名の正規表現</param>
        /// <returns>図形</returns>
        public IEnumerable<BaseShape> EnumerableShapes(string pattern)
        {
            Regex regex = new Regex(pattern);
            foreach (BaseShape shape in shapeDictionary.Values)
            {
                if (regex.IsMatch(shape.Name))
                    yield return shape;
            }
        }
        /// <summary>
        /// パターンを指定して削除
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns>true:図形が削除された</returns>
        public bool RemovePattern(string pattern)
        {
            bool isDel = false;
            Regex regex = new Regex(pattern);
            foreach (KeyValuePair<string, BaseShape> shape in shapeDictionary)
            {
                if ((regex.IsMatch(shape.Key)) || (regex.IsMatch(shape.Value.Name)))
                {
                    shapeDictionary.Remove(shape.Key);
                    isDel |= true;
                }
            }
            return isDel;
        }

        /// <summary>
        /// 指定図形のプロパティを設定する
        /// </summary>
        /// <param name="pattern">取得する図形名の正規表現</param>
        /// <param name="property">プロパティ名</param>
        /// <param name="value">値</param>
        /// <returns>true:設定した</returns>
        public bool SetShapeProperty(string pattern,string property,object value)
        {
            bool result = false;
            Regex regex = new Regex(pattern);
            foreach (BaseShape shape in shapeDictionary.Values)
            {
                if (regex.IsMatch(shape.Name))
                {
                    PropertyInfo propInfo = shape.GetType().GetProperty(property);
                    if ((propInfo != null) && (propInfo.PropertyType == value.GetType()))
                    {
                        propInfo.SetValue(shape, value);
                        result = true;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 基本図形のプロパティ名
        /// </summary>
        public enum SHAPE_PROPERTY
        {
            Color,              //!< 色
            FillColor,          //!< 塗りつぶし色
            LineWidth,          //!< 線幅
            MarkerSize,         //!< マーカーサイズ
            MarkerType,         //!< マーカー種類
            DashStyle,          //!< 線種
            ShowLable,          //!< ラベル表示有無
            Text,               //!< ラベルテキスト
            LabelPosition,      //!< ラベル表示位置
            LabelFont,          //!< ラベルフォント
            LabelColor,         //!< ラベル色
            LabelFill,          //!< ラベルを塗りつぶすか
            LabelFillColor,     //!< ラベル塗りつぶし色
            LabelBorder,        //!< ラベルの枠表示 
            LabelBorderColor,   //!< ラベルの枠色 
            Visible,            //!< 表示有無
        }
        /// <summary>
        /// 基本図形のプロパティ情報
        /// </summary>
        private class SHAPE_PROPERTY_DEF
        {
            /// <summary>
            /// プロパティ
            /// </summary>
            public SHAPE_PROPERTY Property { get;private set; }
            /// <summary>
            /// プロパティ名
            /// </summary>
            public string PropertyName { get; private set; }
            /// <summary>
            /// 型
            /// </summary>
            public Type Type { get; private set; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="property">プロパティ</param>
            /// <param name="type">型</param>
            public SHAPE_PROPERTY_DEF(SHAPE_PROPERTY property, Type type)
            {
                Property = property;
                PropertyName = property.ToString();
                Type = type;
            }
        }
        /// <summary>
        /// 基本図形のプロパティ情報定義
        /// </summary>
        private static SHAPE_PROPERTY_DEF[] SHAPE_PROPERTY_DEFS = new SHAPE_PROPERTY_DEF[]
        {
            new SHAPE_PROPERTY_DEF(SHAPE_PROPERTY.Color,typeof(Color)),
            new SHAPE_PROPERTY_DEF(SHAPE_PROPERTY.FillColor,typeof(Color?)),
            new SHAPE_PROPERTY_DEF(SHAPE_PROPERTY.LineWidth,typeof(float)),
            new SHAPE_PROPERTY_DEF(SHAPE_PROPERTY.MarkerSize,typeof(float)),
            new SHAPE_PROPERTY_DEF(SHAPE_PROPERTY.MarkerType,typeof(MARKER_TYPE)),
            new SHAPE_PROPERTY_DEF(SHAPE_PROPERTY.DashStyle,typeof(DashStyle)),
            new SHAPE_PROPERTY_DEF(SHAPE_PROPERTY.ShowLable,typeof(bool)),
            new SHAPE_PROPERTY_DEF(SHAPE_PROPERTY.Text,typeof(string)),
            new SHAPE_PROPERTY_DEF(SHAPE_PROPERTY.LabelPosition,typeof(LABEL_POSITION)),
            new SHAPE_PROPERTY_DEF(SHAPE_PROPERTY.LabelFont,typeof(Font)),
            new SHAPE_PROPERTY_DEF(SHAPE_PROPERTY.LabelColor,typeof(Color?)),
            new SHAPE_PROPERTY_DEF(SHAPE_PROPERTY.LabelFill,typeof(bool)),
            new SHAPE_PROPERTY_DEF(SHAPE_PROPERTY.LabelFillColor,typeof(Color?)),
            new SHAPE_PROPERTY_DEF(SHAPE_PROPERTY.LabelBorder,typeof(bool)),
            new SHAPE_PROPERTY_DEF(SHAPE_PROPERTY.LabelBorderColor,typeof(Color?)),
            new SHAPE_PROPERTY_DEF(SHAPE_PROPERTY.Visible,typeof(bool)),
        };
        /// <summary>
        /// 指定図形のプロパティを設定する
        /// </summary>
        /// <param name="pattern">取得する図形名の正規表現</param>
        /// <param name="property">プロパティ名</param>
        /// <param name="value">値</param>
        /// <returns>true:設定した</returns>
        public bool SetShapeProperty(string pattern, SHAPE_PROPERTY property, object value)
        {
            SHAPE_PROPERTY_DEF def = SHAPE_PROPERTY_DEFS.First((a) => a.Property == property);
            if ((def != null) && (def.Type == value.GetType())) 
            {
                return SetShapeProperty(pattern, def.PropertyName, value);
            }
            return false;
        }
    }
}
