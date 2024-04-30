﻿using System;
using System.Runtime.InteropServices;
using static SSTools.WndMsgAnalysis;

namespace SSTools
{
	public partial class WndMsgAnalysis
	{
        //       C++                   C#
        //  型名      64bit  32bit 
        //  char        1      1     sbyte
        //  byte        1      1     byte
        //  short       2      2     short/Int16
        //  ushort      2      2     usort/UInt16
        //  int         4      4     int/Int32
        //  uint        4      4     uint/UInt32
        //  long        4      4     int/Int32
        //  ulong       4      4     uint/UInt32
        //  long long   8      8     long/Int64
        //  float       4      4     float
        //  double      8      8     double
        //  FLOAT128   16     16
        //  bool        1      1     bool/Boolean
        //  BOOL        4      4     int/Int32
        //  BOOLEAN     1      1     bool/Boolean
        //  HWND        8      4     IntPtr
        //  ポインタ    8      4     IntPtr
        //  WPARAM      8      4     IntPtr
        //  LPARAM      8      4     IntPtr

        // C++ での結果 64bit
        //  char=1 CHAR=1 BYTE=1
        //	short=2 SHORT=2 USHORT=2 WORD=2
        //	int=4 INT=4 UINT=4
        //	long=4 LONG=4 ULONG=4
        //	long long=8 LONGLON=8 ULONGLONG=8
        //	INT8=1 INT16=2 INT32=4 INT64=8
        //	DWORD=4 DWORD32=4 DWORD64=8 DWORDLONG=8
        //	float=4 FLOAT=4 FLOAT128=16 double=8
        //	bool=1 BOOL=4 BOOLEAN=1
        //	HWND:8 WPARAM=8 LPARAM=8
        //	WINDOWPOS=40 RECT=16 NCCALCSIZE_PARAMS=56
        //
        // C++ での結果 32bit差分のみ
        //	HWND:8 WPARAM=8 LPARAM=8
        //	WINDOWPOS=40 RECT=16 NCCALCSIZE_PARAMS=56


		/// <summary>
		/// WParam/LParam値設定I/F
		/// </summary>
		public interface ISetValue
		{
			/// <summary>
			/// WParam/LParam値設定I
			/// </summary>
			/// <param name="wParam">wParam</param>
			/// <param name="lParam">lParam</param>
			void Set(IntPtr wParam, IntPtr lParam);
		}
		/// <summary>
		/// HiLo分割構造体
		/// </summary>
		[StructLayout(LayoutKind.Explicit)]
		public struct HI_LO
		{
			[FieldOffset(0)]
			public ushort Lo;
			[FieldOffset(0)]
			public short LoI;
			[FieldOffset(2)]
			public ushort Hi;
			[FieldOffset(2)]
			public short HiI;
			[FieldOffset(0)]
			public IntPtr ValuePtr;
			[FieldOffset(0)]
			public Int32 Value;
			[FieldOffset(0)]
			public UInt32 ValueU;
		}
		/// <summary>
		/// WParam/LParam値保持構造体
		/// </summary>
		public struct WPRAM_LPARAM : ISetValue
		{
			/// <summary>
			/// WParam
			/// </summary>
			public HI_LO W;
			/// <summary>
			/// LParam
			/// </summary>
			public HI_LO L;
			/// <summary>
			/// 文字列変換
			/// </summary>
			/// <returns></returns>
			public override string ToString() =>
				string.Format("wParam:0x{0:X8},lParam:0x{1:X8}", W.ValueU, L.ValueU);
			/// <summary>
			/// 値の設定
			/// </summary>
			/// <param name="wParam">WParam</param>
			/// <param name="lParam">LParam</param>
			public void Set(IntPtr wParam, IntPtr lParam)
			{
				this.W.ValuePtr = wParam;
				this.L.ValuePtr = lParam;
			}
		}
		/// <summary>
		/// XY
		/// </summary>
		[StructLayout(LayoutKind.Explicit)]
		public struct XY_PARAMS : ISetValue
		{
			/// <summary>
			/// 幅
			/// </summary>
			[FieldOffset(0)]
			public Int16 X;
			/// <summary>
			/// 高さ
			/// </summary>
			[FieldOffset(2)]
			public Int16 Y;
			/// <summary>
			/// 値
			/// </summary>
			[FieldOffset(0)]
			public UInt32 Value;

			/// <summary>
			/// 文字列変換
			/// </summary>
			/// <returns></returns>
			public override string ToString() =>string.Format("X:{0},Y:{1}", X, Y);
			/// <summary>
			/// 値の設定
			/// </summary>
			/// <param name="wParam">wParam</param>
			/// <param name="lParam">lParam</param>
			public void Set(IntPtr wParam, IntPtr lParam) =>this.Value = (UInt32)wParam;
		}
		/// <summary>
		/// WM_SIZE
		/// </summary>
		[StructLayout(LayoutKind.Explicit)]
		public struct SIZE_PARAMS : ISetValue
		{
			/// <summary>
			/// 幅
			/// </summary>
			[FieldOffset(0)]
			public UInt16 Width;
			/// <summary>
			/// 高さ
			/// </summary>
			[FieldOffset(2)]
			public UInt16 Height;
			/// <summary>
			/// 値
			/// </summary>
			[FieldOffset(0)]
			public UInt32 Value;

			/// <summary>
			/// 文字列変換
			/// </summary>
			/// <returns></returns>
			public override string ToString() => string.Format("W:{0},H:{1}", Width, Height);
			/// <summary>
			/// 値の設定
			/// </summary>
			/// <param name="wParam">wParam</param>
			/// <param name="lParam">lParam</param>
			public void Set(IntPtr wParam, IntPtr lParam) => this.Value = (UInt32)wParam;
		}
		/// <summary>
		/// RECT構造体
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			/// <summary>
			/// 左
			/// </summary>
			public int Left;
			/// <summary>
			/// 上
			/// </summary>
			public int Top;
			/// <summary>
			/// 右
			/// </summary>
			public int Right;
			/// <summary>
			/// 下
			/// </summary>
			public int Bottom;
			/// <summary>
			/// 文字列変換
			/// </summary>
			/// <returns></returns>
			public override string ToString() =>
				string.Format("({0},{1}) - ({2},{3}) W:{4} H:{5}", Left, Top, Right, Bottom, Right - Left, Bottom - Top);
			/// <summary>
			/// 幅
			/// </summary>
			public int Width { get => Right - Left; }
			/// <summary>
			/// 高さ
			/// </summary>
			public int Height { get => Bottom - Top; }
		}


		/// <summary>
		/// WINDOWPOS
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct WINDOWPOS_PARAMS
		{
			public IntPtr Hwnd;
			public IntPtr HwndInsertAfter;
			public int X;
			public int Y;
			public int Cx;
			public int Cy;
			public uint Flags;

			/// <summary>
			/// 文字列変換
			/// </summary>
			/// <returns></returns>
			public override string ToString() =>
				string.Format("({0},{1}) W:{2},H:{3} flags:{4:X8}", X, Y, Cx, Cy, Flags);
		}

		/// <summary>
		/// WM_NCCALCSIZE
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct NCCALCSIZE_PARAMS
		{
			public RECT Rect0;
			public RECT Rect1;
			public RECT Rect2;
			public IntPtr LpPos;    // WINDOWPOS_PARAMS

			/// <summary>
			/// 文字列変換
			/// </summary>
			/// <returns></returns>
			public override string ToString() =>
				string.Format("RECT0:{0} RECT[1]:{1} RECT[2]:{2} LPPos:{3}",
					Rect0.ToString(), Rect1.ToString(), Rect2.ToString(), LpPos.ToString());
		}

		public class NCCALCSIZE_PARAMS_CLASS
		{
			public NCCALCSIZE_PARAMS NcCalcSize;
			public WINDOWPOS_PARAMS WindowPos;

			public NCCALCSIZE_PARAMS_CLASS(IntPtr address)
			{
				NcCalcSize = IntPtrEx.Instance(address).GetStructure<NCCALCSIZE_PARAMS>(PtrFunc);
			}
			private void PtrFunc(string name,Type type,object value)
			{
				if (name == "LpPos")
				{
					if ((type == typeof(IntPtr)) && (value is IntPtr addr))
						WindowPos = IntPtrEx.Instance(addr).GetStructure<WINDOWPOS_PARAMS>();
					else if ((type == typeof(IntPtrEx)) && (value is IntPtrEx ptr))
						WindowPos = ptr.GetStructure<WINDOWPOS_PARAMS>();
				}
					
			}

			public override string ToString() =>
				string.Format("RECT0:{0} RECT[1]:{1} RECT[2]:{2} LPPos:{3}" +
					" Hwnd:{4} HwndInsertAfter:{5} X:{6} Y:{7} W:{8} H:{9} Flags:{10}",
					NcCalcSize.Rect0.ToString(), NcCalcSize.Rect1.ToString(), NcCalcSize.Rect2.ToString(),IntPtrEx.Instance(NcCalcSize.LpPos),
					IntPtrEx.Instance(WindowPos.Hwnd), IntPtrEx.Instance(WindowPos.HwndInsertAfter),
					WindowPos.X, WindowPos.Y, WindowPos.Cx,WindowPos.Cy,WindowPos.Flags);
		}

		/// <summary>
		/// NMHDR構造体
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct NMHDR
		{
			public IntPtr HwndFrom;
			public UInt32 IdFrom;
			public UInt32 Code;
			public override string ToString() =>
				string.Format("HwndFrom:0x{0:X8} ID From:0x{1:X8} Code:0x{2:X8}",(uint)HwndFrom, IdFrom, Code);
		}

        /// <summary>
        /// CREATESTRUCTA構造体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]

        public struct CREATESTRUCTA
        {
            public IntPtr lpCreateParams;
            public IntPtr hInstance;
            public IntPtr hMenu;
            public IntPtr hwndParent;
            public int cy;
            public int cx;
            public int y;
            public int x;
            public UInt32 style;
            public IntPtr lpszName;
            public IntPtr lpszClass;
            public UInt32 dwExStyle;

			public override string ToString() =>
				string.Format("CreateParams:0x{0:X8} Instance:0x{1:X8} Menu:0x{2:X8} Parent:0x{3:X8} X:{4},Y:{5} Cx:{6} Cy:{7} Style:{8}" +
					" Name:0x{9:X8} Class:0x{10:X8} ExtStyle:{11}",
					(uint)lpCreateParams, (uint)hInstance, (uint)hMenu, (uint)hwndParent, 
					x, y, cx, cy, (WINDOW_STYLE)style,(uint)lpszName,(uint)lpszClass,(EXTEND_WINDOW_STYLE)dwExStyle);
        }
    }
}
