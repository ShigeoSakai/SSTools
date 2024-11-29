using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTools.ColorMap
{
    public class GrayColorMap : ColorMapClass
    {
        public override Color Get(int index)
        {
            return Color.FromArgb(index,index,index);
        }
    }
}
