using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTools.ColorMap
{
    /// <summary>
    /// カラーマップクラス
    /// </summary>
    public class ColorMapClass
    {
        /// <summary>
        /// カラーマップ
        /// </summary>
        protected Color[] colorMap;

        /// <summary>
        /// 色の取得
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <returns>指定インデックスの色</returns>
        public virtual Color Get(int index)
        {
            if ((colorMap != null) && (index >= 0) && (index < colorMap.Length))
                return colorMap[index];
            return Color.FromArgb(0, 0, 0);
        }
    }
}
