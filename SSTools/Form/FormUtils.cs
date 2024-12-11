using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSTools
{
    /// <summary>
    /// フォームユーティリティ
    /// </summary>
    public class FormUtils
    {
        /// <summary>
        /// フォームの表示位置を算出する
        /// </summary>
        /// <param name="baseControl">基準になるコントロール</param>
        /// <param name="formSize">フォームのサイズ</param>
        /// <returns>フォーム表示位置</returns>
        /// <remarks>
        ///  フォームが、画面からはみ出ないて、基準コントロールの周辺に表示できる
        ///  位置を求める
        ///       ┌─────┐       ┌─────┐
        ///       │3         │       │5         │
        /// ┌─────┐    │       │    ┌─────┐
        /// │1         │──┘       └──│7         │
        /// ┌─────┐┏━┓       ┏━┓┌─────┐
        /// │2         │┃  ┃       ┃  ┃│8         │
        /// │          │┃  ┃       ┃  ┃│          │
        /// └─────┘┗━┛       ┗━┛└─────┘
        ///       ┌─────┐       ┌─────┐
        ///       │4         │       │6         │
        ///       │          │       │          │
        ///       └─────┘       └─────┘
        /// </remarks>
        public static Point CalcLocation(Control baseControl , Size formSize)
        {
            // フォームが属するスクリーンの使用可能サイズ
            Rectangle screenRect = Screen.GetWorkingArea(baseControl);
            // コントロールの画面上の位置
            Point ctrlLocation = baseControl.PointToScreen(new Point(0, 0));

            LocatePositon pos = LocatePositon.NONE;
            
            // 左右方向
            if (ctrlLocation.X - formSize.Width >= screenRect.Left)
            {   // 1,2,3,4が可能
                pos = LocatePositon.POS_LEFT | LocatePositon.POS_LEFT_MAX;
            }
            else if (ctrlLocation.X + baseControl.Width - formSize.Width >= screenRect.Left)
            {   // 3,4が可能
                pos = LocatePositon.POS_LEFT;
            }
            if (ctrlLocation.X + baseControl.Width + formSize.Width < screenRect.Right) 
            {   // 5,6,7,8が可能 
                pos |= LocatePositon.POS_RIGHT | LocatePositon.POS_RIGHT_MAX;
            }
            else if (ctrlLocation.X + formSize.Width < screenRect.Right)
            {   // 5,6が可能
                pos |= LocatePositon.POS_RIGHT;
            }


            // 上下方向
            if (ctrlLocation.Y - formSize.Height >= screenRect.Top)
            {   // 1,3,5,7が可能
                pos |= LocatePositon.POS_TOP | LocatePositon.POS_TOP_MAX;
            }
            else if (ctrlLocation.Y + baseControl.Height - formSize.Height >= screenRect.Top)
            {   // 1,7が可能
                pos |= LocatePositon.POS_TOP;
            }
            if (ctrlLocation.Y + baseControl.Height + formSize.Height < screenRect.Bottom)
            {   // 2,4,6,8が可能
                pos |= LocatePositon.POS_BOTTOM | LocatePositon.POS_BOTTOM_MAX;
            }
            else if (ctrlLocation.Y + formSize.Height < screenRect.Bottom)
            {   // 2,8が可能
                pos |= LocatePositon.POS_BOTTOM;
            }

            // 位置が見つからない場合...
            if (pos == LocatePositon.NONE)
            {   // 4の位置
                pos = LocatePositon.POS_LEFT | LocatePositon.POS_BOTTOM_MAX;
            }

            if (pos.HasFlag(LocatePositon.POS_LEFT) && pos.HasFlag(LocatePositon.POS_BOTTOM_MAX))
            {   // POS_4
                return new Point(
                    ctrlLocation.X + baseControl.Width - formSize.Width,
                    ctrlLocation.Y + baseControl.Height);
            }
            else if (pos.HasFlag(LocatePositon.POS_RIGHT) && pos.HasFlag(LocatePositon.POS_BOTTOM_MAX))
            {   // POS_6
                return new Point(
                    ctrlLocation.X,
                    ctrlLocation.Y + baseControl.Height);
            }
            else if (pos.HasFlag(LocatePositon.POS_LEFT) && pos.HasFlag(LocatePositon.POS_TOP_MAX))
            {   // POS_3
                return new Point(
                    ctrlLocation.X + baseControl.Width - formSize.Width,
                    ctrlLocation.Y - formSize.Height);
            }
            else if (pos.HasFlag(LocatePositon.POS_RIGHT) && pos.HasFlag(LocatePositon.POS_TOP_MAX))
            {   // POS_5
                return new Point(
                    ctrlLocation.X,
                    ctrlLocation.Y - formSize.Height);
            }
            else if (pos.HasFlag(LocatePositon.POS_LEFT_MAX) && pos.HasFlag(LocatePositon.POS_TOP))
            {   // POS_1
                return new Point(
                    ctrlLocation.X - formSize.Width,
                    ctrlLocation.Y + baseControl.Height - formSize.Height);
            }
            else if (pos.HasFlag(LocatePositon.POS_LEFT_MAX) && pos.HasFlag(LocatePositon.POS_BOTTOM))
            {   // POS_2
                return new Point(
                    ctrlLocation.X - formSize.Width,
                    ctrlLocation.Y);
            }
            else if (pos.HasFlag(LocatePositon.POS_RIGHT_MAX) && pos.HasFlag(LocatePositon.POS_TOP))
            {   // POS_7
                return new Point(
                    ctrlLocation.X + baseControl.Width,
                    ctrlLocation.Y + baseControl.Height - formSize.Height);
            }
            else if (pos.HasFlag(LocatePositon.POS_RIGHT_MAX) && pos.HasFlag(LocatePositon.POS_BOTTOM))
            {   // POS_8
                return new Point(
                    ctrlLocation.X + baseControl.Width,
                    ctrlLocation.Y);
            }
            else
            {   // それ以外...画面中央
                return new Point(
                    screenRect.X + (screenRect.Width - formSize.Width)/2, 
                    screenRect.Y + (screenRect.Height - formSize.Height)/2);
            }
        }
        /// <summary>
        /// 表示位置Enum
        /// </summary>
        /// <remarks>
        /// POS_1 = POS_LEFT_MAX | POS_TOP,
        /// POS_2 = POS_LEFT_MAX | POS_BOTTOM,
        /// POS_3 = POS_LEFT | POS_TOP_MAX,
        /// POS_4 = POS_LEFT | POS_BOTTOM_MAX,
        ///
        /// POS_5 = POS_RIGHT | POS_TOP_MAX,
        /// POS_6 = POS_RIGHT | POS_BOTTOM_MAX,
        /// POS_7 = POS_RIGHT_MAX | POS_TOP,
        /// POS_8 = POS_RIGHT_MAX | POS_BOTTOM,
        /// </remarks>
        [Flags]
        private enum LocatePositon
        {
            POS_LEFT_MAX = 0x0001,          //!< 横方向:左最大
            POS_LEFT = 0x0002,              //!< 横方向:左
            POS_RIGHT = 0x0004,             //!< 横方向:右
            POS_RIGHT_MAX = 0x0008,         //!< 横方向:右最大

            POS_TOP_MAX = 0x0010,           //!< 縦方向:上最大
            POS_TOP = 0x0020,               //!< 縦方向:上
            POS_BOTTOM = 0x0040,            //!< 縦方向:下
            POS_BOTTOM_MAX = 0x0080,        //!< 縦方向:下最大

            NONE = 0,                       //!< 未指定
        }
    }
}
