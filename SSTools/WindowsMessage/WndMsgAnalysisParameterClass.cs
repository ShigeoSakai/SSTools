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
            /// <summary>
            /// WParam値
            /// </summary>
			public HI_LO wParam;
            /// <summary>
            /// LParam構造体値
            /// </summary>
			public T lParam; 
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
			/// <remarks>
			/// WParam ... そのままの値
			/// LParam ... 構造体Tのポインタとして格納
			/// </remarks>
            public ONLY_LPARAM(IntPtr wParam, IntPtr lParam)
			{
				this.wParam.ValuePtr = wParam;
				this.lParam = (T)Marshal.PtrToStructure(lParam, typeof(T));
			}
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString() { return lParam.ToString();}
		}
		/// <summary>
		/// 指定クラスのIntPtrを引数とするコンストラクタを呼び出し、クラスを生成する
		/// </summary>
		/// <typeparam name="T">クラス</typeparam>
		/// <param name="value">アドレス(IntPtr)</param>
		/// <returns>生成されたクラス</returns>
		private static T Construct<T>(IntPtr value)
		{
			return (T)typeof(T).GetConstructor(new Type[] { typeof(IntPtr) }).Invoke(new object[] { value });
		}

        /// <summary>
        /// LParamのみクラスに適用
        /// </summary>
        /// <typeparam name="T">クラス</typeparam>
        public class ONLY_LPARAM_CLASS<T> where T : class
		{
            /// <summary>
            /// WParam値
            /// </summary>
			public HI_LO wParam; 
            /// <summary>
            /// LParamが示すアドレスのクラス
            /// </summary>
            public T lParam; 
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParam ... そのままの値
            /// LParam ... クラスTのポインタとして格納
            /// </remarks>
            public ONLY_LPARAM_CLASS(IntPtr wParam, IntPtr lParam)
			{
				this.wParam.ValuePtr = wParam;
				this.lParam = Construct<T>(lParam);
			}
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString(){ return lParam.ToString();}
		}
        /// <summary>
        /// WParamがFalseの場合、T0構造体を適用
        /// WParamがTrueの場合、T1構造体を適用
        /// </summary>
        /// <typeparam name="T0">WParam=False時の構造体</typeparam>
        /// <typeparam name="T1">WParam=True時の構造体</typeparam>
        public class WPARAM_IF_LPARAM<T0, T1> where T0 : struct where T1 : struct
        {
            /// <summary>
            /// LParamが示すアドレスの"T0"構造体
            /// </summary>
            public T0 Param0; 
            /// <summary>
            /// LParamが示すアドレスの"T1"構造体
            /// </summary>
            public T1 Param1; 
            /// <summary>
            /// WParamの状態 
            /// </summary>
            public uint Condition;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParam ... 条件判定に使用
            /// LParam ... WParam=0時は、T0構造体のポインタ。WParam!=0時は、T1構造体のポインタとする
            /// </remarks>
			public WPARAM_IF_LPARAM(IntPtr wParam, IntPtr lParam)
            {
                Condition = (uint)wParam;
                if (Condition == 0)
                    Param0 = (T0)Marshal.PtrToStructure(lParam, typeof(T0));
                else
                    Param1 = (T1)Marshal.PtrToStructure(lParam, typeof(T1));
            }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString()
			{ return string.Format("Cond:{0} {1}", Condition, (Condition == 0) ? Param0.ToString() : Param1.ToString());}
		}
        /// <summary>
        /// WParam/LParamとも直値のクラス
        /// </summary>
        /// <typeparam name="T">構造体</typeparam>
        public class WPARAM_LPARAM_IS_VALUE<T> where T : struct, ISetValue
        {
            /// <summary>
            /// 構造体Tの値
            /// </summary>
            public T Param;
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParam/WParamとも構造体Tの値(ポインタではない)
            /// </remarks>
            public WPARAM_LPARAM_IS_VALUE(IntPtr wParam, IntPtr lParam) { Param.Set(wParam, lParam); }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString() { return Param.ToString(); }
		}
		/// <summary>
		/// パラメータ未使用
		/// </summary>
		public class PARAMETER_NOT_USE
		{
            /// <summary>
            /// パラメータ未使用コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            public PARAMETER_NOT_USE(IntPtr wParam, IntPtr lParam) { }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString() {return ""; }
		}
		/// <summary>
		/// WParamがハンドルのクラス
		/// </summary>
		public class WPARAMETER_IS_HANDLE
		{
            /// <summary>
            /// ハンドル
            /// </summary>
			public IntPtr Handle;
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParamがハンドル。LParamは未使用
            /// </remarks>
            public WPARAMETER_IS_HANDLE(IntPtr wParam, IntPtr lParam) { Handle = wParam; }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString() { return string.Format("Handle:0x{0:X8}", (uint)Handle); }
		}
		/// <summary>
		/// LParamがハンドルのクラス
		/// </summary>
		public class LPARAMETER_IS_HANDLE
		{
            /// <summary>
            /// ハンドル
            /// </summary>
			public IntPtr Handle;
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// LParamがハンドル。WParamは未使用
            /// </remarks>
            public LPARAMETER_IS_HANDLE(IntPtr wParam, IntPtr lParam) { Handle = wParam; }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString() { return string.Format("Handle:0x{0:X8}", (uint)Handle); }
		}
		/// <summary>
		/// WParamが論理値のクラス
		/// </summary>
		public class WPARAM_IS_BOOL
		{
            /// <summary>
            /// 論理値(WParam値)
            /// </summary>
			public bool Value;
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParamが論理値(0...False/0以外...True)
            /// LParamは未使用
            /// </remarks>
			public WPARAM_IS_BOOL(IntPtr wParam, IntPtr lParam) { Value = (wParam != (IntPtr)0); }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString() { return Value.ToString(); }
		}
		/// <summary>
		/// WParam/LParamともハンドルのクラス
		/// </summary>
		private class WM_HANDLES : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParam/LParamともハンドル値
            /// </remarks>
			public WM_HANDLES(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString() { return string.Format("Device Context:0x{0:X8} Control Handle:0x{1:X8}", Param.W.ValueU, Param.L.ValueU); }
		}
	}
}
