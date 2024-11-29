using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTools.ColorMap
{
    public class ColorMapClass
    {
        protected Color[] colorMap;

        public virtual Color Get(int index)
        {
            if ((colorMap != null) && (index >= 0) && (index < colorMap.Length))
                return colorMap[index];
            return Color.FromArgb(0, 0, 0);
        }
    }
}
