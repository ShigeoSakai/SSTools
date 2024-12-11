using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTools.ColorMap
{
    /// <summary>
    /// グレースケールカラーマップ
    /// </summary>
    public class GrayColorMap : ColorMapClass
    {
        /// <summary>
        /// 色の取得
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <returns>色</returns>
        public override Color Get(int index)
        {
            return Color.FromArgb(index,index,index);
        }
    }
}
