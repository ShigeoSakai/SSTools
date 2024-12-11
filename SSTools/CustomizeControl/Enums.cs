using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTools
{
    /// <summary>
    /// ラベル表示位置
    /// </summary>
    public enum LABEL_POSITION
    {
        CENTER = 0x022,                 //!< 中央
        TOP_LEFT = 0x011,               //!< 左上外側
        TOP_LEFT_INNER = 0x111,         //!< 左上内側
        TOP_CENTER = 0x012,             //!< 上中央外側
        TOP_CENTER_INNER = 0x112,       //!< 上中央内側
        TOP_RIGHT = 0x013,              //!< 右上外側
        TOP_RIGHT_INNER = 0x113,        //!< 右上内側
        BOTTOM_LEFT = 0x131,            //!< 左下外側
        BOTTOM_LEFT_INNER = 0x031,      //!< 左下内側
        BOTTOM_CENTER = 0x132,          //!< 下中央外側
        BOTTOM_CENTER_INNER = 0x032,    //!< 下中央内側
        BOTTOM_RIGHT = 0x133,           //!< 右下外側
        BOTTOM_RIGHT_INNER = 0x033,     //!< 右下内側
    }
    /// <summary>
    /// 切り取りオプション
    /// </summary>
    public enum CLIP_OPTION
    {
        NONE = 0,               //!< 全ての図形が入るサイズにする
        ORIGINAL_WIDTH = 1,     //!< 元の画像幅を維持
        ORIGINAL_HEIGHT = 2     //!< 元の画像高さを維持
    }

}
