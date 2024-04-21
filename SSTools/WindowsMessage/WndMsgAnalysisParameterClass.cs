using System;
using System.Runtime.InteropServices;

namespace SSTools
{
	public partial class WndMsgAnalysis
	{
		/// <summary>
		/// LParamのみ構造体に適用
		/// </summary>
		/// <typeparam name="T">構造体</typeparam>
		public class ONLY_LPARAM<T> where T : struct
		{
			public HI_LO wParam;
			public T lParam;
			public ONLY_LPARAM(IntPtr wParam, IntPtr lParam)
			{
				this.wParam.ValuePtr = wParam;
				this.lParam = (T)Marshal.PtrToStructure(lParam, typeof(T));
			}
			public override string ToString() => lParam.ToString();
		}
		/// <summary>
		/// WParamがFalseの場合、T0構造体を適用
		/// WParamがFalseの場合、T1構造体を適用
		/// </summary>
		/// <typeparam name="T0"></typeparam>
		/// <typeparam name="T1"></typeparam>
		public class WPARAM_IF_LPARAM<T0, T1> where T0 : struct where T1 : struct
		{
			public T0 Param0;
			public T1 Param1;
			public uint Condition;

			public WPARAM_IF_LPARAM(IntPtr wParam, IntPtr lParam)
			{
				Condition = (uint)wParam;
				if (Condition == 0)
					Param0 = (T0)Marshal.PtrToStructure(lParam, typeof(T0));
				else
					Param1 = (T1)Marshal.PtrToStructure(lParam, typeof(T1));
			}
			public override string ToString() =>
				string.Format("Cond:{0} {1}", Condition, (Condition == 0) ? Param0.ToString() : Param1.ToString());
		}
		/// <summary>
		/// WParam/LParamとも直値のクラス
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public class WPARAM_LPARAM_IS_VALUE<T> where T : struct, ISetValue
		{
			public T Param;
			public WPARAM_LPARAM_IS_VALUE(IntPtr wParam, IntPtr lParam) => Param.Set(wParam, lParam);
			public override string ToString() => Param.ToString();
		}
		/// <summary>
		/// パラメータ未使用
		/// </summary>
		public class PARAMETER_NOT_USE
		{
			public PARAMETER_NOT_USE(IntPtr wParam, IntPtr lParam) { }
			public override string ToString() => "";
		}
		/// <summary>
		/// WParamがハンドル
		/// </summary>
		public class WPARAMETER_IS_HANDLE
		{
			public IntPtr Handle;
			public WPARAMETER_IS_HANDLE(IntPtr wParam, IntPtr lParam) => Handle = wParam;
			public override string ToString() => string.Format("Handle:0x{0:X8}", (uint)Handle);
		}
		/// <summary>
		/// LParamがハンドル
		/// </summary>
		public class LPARAMETER_IS_HANDLE
		{
			public IntPtr Handle;
			public LPARAMETER_IS_HANDLE(IntPtr wParam, IntPtr lParam) => Handle = wParam;
			public override string ToString() => string.Format("Handle:0x{0:X8}", (uint)Handle);
		}
		/// <summary>
		/// WParamが論理値
		/// </summary>
		public class WPARAM_IS_BOOL
		{
			public bool Value;
			public WPARAM_IS_BOOL(IntPtr wParam, IntPtr lParam) => Value = (wParam != (IntPtr)0);
			public override string ToString() => Value.ToString();
		}
		/// <summary>
		/// WParam/LParamともハンドル
		/// </summary>
		private class WM_HANDLES : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
			public WM_HANDLES(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
			public override string ToString() => string.Format("Device Context:0x{0:X8} Control Handle:0x{1:X8}", Param.W.ValueU, Param.L.ValueU);
		}
	}
}
