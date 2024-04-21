using System;
using System.Runtime.InteropServices;

namespace SSTools
{
	public partial class WndMsgAnalysis
	{
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
				string.Format("({0},{1}) center:({2},{3}) flags:{4:X8}", X, Y, Cx, Cy, Flags);
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
			public WINDOWPOS_PARAMS LpPos;

			/// <summary>
			/// 文字列変換
			/// </summary>
			/// <returns></returns>
			public override string ToString() =>
				string.Format("RECT0:{0}\r\nRECT[1]:{1}\r\nRECT[2]:{2}\r\n:LPPos:{3}",
					Rect0.ToString(), Rect1.ToString(), Rect2.ToString(), LpPos.ToString());
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
	}
}
