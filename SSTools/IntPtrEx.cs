﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SSTools
{
    /// <summary>
    /// IntPtrを使いやすくするクラス
    /// </summary>
    public struct IntPtrEx : IFormattable, IComparable
	{
		/// <summary>
		/// IntPtrを色々な数値型に変換する構造体
		/// </summary>
		[StructLayout(LayoutKind.Explicit)]
		public struct HI_LO
		{
			[FieldOffset(0)]
			public ushort LO_WORD;		//!< word(16bit) low
			[FieldOffset(0)]
			public short LO_SHORT;      //!< short(16bit) low
            [FieldOffset(2)]
			public ushort HI_WORD;      //!< word(16bit) hi
            [FieldOffset(2)]
			public short HI_SHORT;      //!< short(16bit) hi
            [FieldOffset(0)]
			public uint LO_UINT;        //!< uint(32bit) low
            [FieldOffset(0)]
			public int LO_INT;			//!< int(32bit) low

            [FieldOffset(0)]
			public IntPtr Ptr;			//!< IntPtr

#if !WIN32
			[FieldOffset(0)]
			public ulong ULONG;			//!< ulong(64bit)
			[FieldOffset(0)]
			public long LONG;           //!< long(64bit)
            [FieldOffset(4)]
			public uint HI_UINT;        //!< uint(32bit) hi
            [FieldOffset(4)]
			public int HI_INT;          //!< int(32bit) hi
#endif
            /// <summary>
            /// 明示的な値の設定
            /// </summary>
            /// <param name="value">IntPtr値</param>
            public void Set(IntPtr value)
			{
				Ptr = value;
#if !WIN32
				// 32bitプロセスで起動された場合、上位32bitを符号拡張する
				if (Environment.Is64BitProcess == false)
					if ((LO_UINT & 0x80000000) != 0)
						HI_UINT = 0xFFFFFFFF;
					else
						HI_UINT = 0;
#endif
			}
#if !WIN32
			/// <summary>
			/// 符号なし値の取得
			/// </summary>
			/// <returns>ulong値</returns>
			public ulong GetUnsigned() => ULONG;
			/// <summary>
			/// 符号あり値の取得
			/// </summary>
			/// <returns>long値</returns>
			public long GetSigned() => LONG;
#else
            /// <summary>
            /// 符号なし値の取得
            /// </summary>
            /// <returns>uint値</returns>
            public uint GetUnsigned() => LO_UINT;
            /// <summary>
            /// 符号あり値の取得
            /// </summary>
            /// <returns>int値</returns>
            public int  GetSigned() => LO_INT;
#endif

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="value">IntPtr値</param>
            /// <remarks>
            /// 規定のコンストラクタthis()を明示的に呼び出して、構造体メンバーを初期化する必要がある
            /// </remarks>
            public HI_LO(IntPtr value) : this() { Set(value); }
			/// <summary>
			/// コピーコンストラクタ
			/// </summary>
			/// <param name="value">値</param>
			/// <remarks>
			/// 規定のコンストラクタthis()を明示的に呼び出して、構造体メンバーを初期化する必要がある
			/// </remarks>
			public HI_LO(HI_LO value) : this() { Set(value.Ptr); }
		}
		/// <summary>
		/// 値(実体)
		/// </summary>
		public HI_LO Value { get; private set; }
		/// <summary>
		/// 値(プロパティ)
		/// </summary>
		public IntPtr IntPtr { get => Value.Ptr; set => Value = new HI_LO(value); }

		/// <summary>
		/// コピーコンストラクタ
		/// </summary>
		/// <param name="value">IntPtrEx値</param>
		/// <remarks>
		/// 規定のコンストラクタthis()を明示的に呼び出して、構造体メンバーを初期化する必要がある
		/// </remarks>
		public IntPtrEx(IntPtrEx value) : this() { IntPtr = value.IntPtr; }
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="value">IntPtr値</param>
		/// <remarks>
		/// 規定のコンストラクタthis()を明示的に呼び出して、構造体メンバーを初期化する必要がある
		/// </remarks>
		public IntPtrEx(IntPtr value) : this() { IntPtr = value; }

        /// <summary>
        /// インスタンス生成
        /// </summary>
        /// <param name="value">IntPtr値</param>
        /// <returns>インスタンス</returns>
        public static IntPtrEx Instance(IntPtr value) => new IntPtrEx(value);

		/// <summary>
		/// 文字列変換
		/// </summary>
		/// <returns>変換した文字列</returns>
		public override string ToString() => ToString(null, null);
		/// <summary>
		/// 文字列変換
		/// </summary>
		/// <param name="format">フォーマット</param>
		/// <returns>変換した文字列</returns>
		public string ToString(string format) => ToString(format, null);

		/// <summary>
		/// 文字列変換
		/// </summary>
		/// <param name="format">フォーマット</param>
		/// <param name="formatProvider">フォーマットプロバイダ</param>
		/// <returns>変換した文字列</returns>
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (string.IsNullOrEmpty(format)) format = "PTR";
			if (formatProvider == null) formatProvider = CultureInfo.CurrentCulture;

			string fmt_large = format.ToUpperInvariant();
			if (fmt_large.StartsWith("PTR"))
			{
#if !WIN32
				int digit = Environment.Is64BitProcess ? 16 : 8;
#else
                int digit = 8;
#endif
				int max_digit = digit;
				if ((fmt_large.Length > 3) && (int.TryParse(fmt_large.Substring(3), out digit)))
				{
					if (digit > max_digit) digit = max_digit;
				}
				string data_str = Value.GetUnsigned().ToString("X");
				int conv_len = data_str.Length;
				data_str = data_str.PadLeft(max_digit, '0');
				if (max_digit > 8)
					data_str = data_str.Substring(0, 8) + "-" + data_str.Substring(8, 8);

				if (format.StartsWith("PTR") == false)
					data_str = data_str.ToLower();

				if (digit < conv_len) digit = conv_len;
				if ((max_digit > 8) && (digit > 8)) digit++;

				return data_str.Substring(data_str.Length - digit, digit);
			}
			else
			{
				return Value.GetUnsigned().ToString(format, formatProvider);
			}
		}
        /// <summary>
        /// 指定フィールドに値が入った時のイベントハンドラ
        /// </summary>
        /// <param name="name">フィールド名</param>
        /// <param name="type">フィールド型</param>
        /// <param name="value">値</param>
        public delegate void PtrEventHandler(string name, Type type, object value);
		/// <summary>
		/// 値の示す番地から構造体を生成
		/// </summary>
		/// <typeparam name="T">構造体の型</typeparam>
		/// <param name="func">イベント関数</param>
		/// <returns>構造体</returns>
		public T GetStructure<T>(PtrEventHandler func = null) where T : struct
		{
			if (Value.Ptr != (IntPtr)0)
			{
				T result = Marshal.PtrToStructure<T>(Value.Ptr);
				if (func != null)
				{
					FieldInfo[] fields = typeof(T).GetFields();
					foreach (FieldInfo field in fields)
					{
						if (field.Attributes.HasFlag(FieldAttributes.Static) == false)
						{
							if (field.FieldType == typeof(IntPtrEx))
							{
								IntPtrEx obj = (IntPtrEx)field.GetValue(result);
								func?.Invoke(field.Name, field.FieldType, obj);
							}
							if (field.FieldType == typeof(IntPtr))
							{
								IntPtr obj = (IntPtr)field.GetValue(result);
								func?.Invoke(field.Name, field.FieldType, obj);
							}
						}
					}
				}
				return result;
			}
			return default;
		}
		/// <summary>
		/// IntPtrを文字列変換
		/// </summary>
		/// <returns>変換された文字列</returns>
		public string GetString()
		{
			if (Value.Ptr != (IntPtr)0)
				return Marshal.PtrToStringAuto(Value.Ptr);
			return default;
		}
        /// <summary>
        /// 指定構造体の配列として取り出す
        /// </summary>
        /// <typeparam name="T">構造体に型</typeparam>
        /// <param name="value_size">構造体のサイズ</param>
        /// <param name="capacity">配列のサイズ</param>
        /// <returns>指定構造体の配列</returns>
        public T[] GetArray<T>(int value_size, int capacity) where T : struct
		{
			if (Value.Ptr != (IntPtr)0)
			{
				T[] result = new T[capacity];
				IntPtr ptr = Value.Ptr;
				for (int index = 0; index < capacity; index++, ptr += value_size)
					result[index] = Marshal.PtrToStructure<T>(ptr);
				return result;
			}
			return default;
		}

		/// <summary>
		/// 値の比較
		/// </summary>
		/// <param name="obj">比較する値</param>
		/// <returns>負値:小さい,0:同じ,正値:大きい</returns>
		/// <exception cref="ArgumentException">比較できない型(intPtrEx2)と比較した</exception>
		public int CompareTo(object obj)
		{
			if (obj == null) return 1;
			if (obj is IntPtrEx ptr_st) return (int)(Value.GetUnsigned() - ptr_st.Value.GetUnsigned());
#if WIN32
            if (obj is IntPtr ptr) return (int)(Value.GetUnsigned() - (uint)ptr);
#else
			if (obj is IntPtr ptr) return (int)(Value.GetUnsigned() - (ulong)ptr);
#endif
			if (obj is ulong ulValue) return (int)(Value.GetUnsigned() - ulValue);
			if (obj is long lValue) return (int)(Value.GetSigned() - lValue);
			if (obj is uint uiValue) return (int)(Value.GetUnsigned() - uiValue);
			if (obj is int iValue) return (int)(Value.GetSigned() - iValue);
			throw new ArgumentException(string.Format("IntPtrEx2型は\"{0}\"型と比較できません", obj.GetType().Name));
		}
        /// <summary>
        /// 値の比較(一致)
        /// </summary>
        /// <param name="obj">比較する値</param>
        /// <returns>true:一致</returns>
        public override bool Equals(object obj) => CompareTo(obj) == 0;
		/// <summary>
		/// ハッシュ値の取得
		/// </summary>
		/// <returns>ハッシュ値</returns>
		public override int GetHashCode() =>Value.GetUnsigned().GetHashCode();
        /// <summary>
        /// オペレーター「＋」
        /// </summary>
        /// <param name="left">左側値(IntPtrEx)</param>
        /// <param name="right">右側値(IntPtrEx)</param>
        /// <returns>演算結果</returns>
        public static IntPtrEx operator +(IntPtrEx left, IntPtrEx right) => Instance((IntPtr)(left.Value.GetUnsigned() + right.Value.GetUnsigned()));
#if WIN32
		/// <summary>
		/// オペレーター「＋」
		/// </summary>
		/// <param name="left">左側値(IntPtrEx)</param>
		/// <param name="right">右側値(IntPtr)</param>
		/// <returns>演算結果</returns>
		public static IntPtrEx operator +(IntPtrEx left, IntPtr right) => Instance((IntPtr)(left.Value.GetUnsigned() + (uint)right));
		/// <summary>
		/// オペレーター「＋」
		/// </summary>
		/// <param name="left">左側値(IntPtr)</param>
		/// <param name="right">右側値(IntPtrEx)</param>
		/// <returns>演算結果</returns>
		public static IntPtrEx operator +(IntPtr left, IntPtrEx right) => Instance((IntPtr)((uint)left + right.Value.GetUnsigned()));
#else
        /// <summary>
        /// オペレーター「＋」
        /// </summary>
        /// <param name="left">左側値(IntPtrEx)</param>
        /// <param name="right">右側値(IntPtr)</param>
        /// <returns>演算結果</returns>
        public static IntPtrEx operator +(IntPtrEx left, IntPtr right) => Instance((IntPtr)(left.Value.GetUnsigned() + (ulong)right));
        /// <summary>
        /// オペレーター「＋」
        /// </summary>
        /// <param name="left">左側値(IntPtr)</param>
        /// <param name="right">右側値(IntPtrEx)</param>
        /// <returns>演算結果</returns>
        public static IntPtrEx operator +(IntPtr left, IntPtrEx right) => Instance((IntPtr)((ulong)left + right.Value.GetUnsigned()));
        /// <summary>
        /// オペレーター「＋」
        /// </summary>
        /// <param name="left">左側値(IntPtrEx)</param>
        /// <param name="right">右側値(ulong)</param>
        /// <returns>演算結果</returns>
        public static IntPtrEx operator +(IntPtrEx left, ulong right) => Instance((IntPtr)(left.Value.GetUnsigned() + right));
        /// <summary>
        /// オペレーター「＋」
        /// </summary>
        /// <param name="left">左側値(ulong)</param>
        /// <param name="right">右側値(IntPtrEx)</param>
        /// <returns>演算結果</returns>
        public static IntPtrEx operator +(ulong left, IntPtrEx right) => Instance((IntPtr)(left + right.Value.GetUnsigned()));
#endif
        /// <summary>
        /// オペレーター「＋」
        /// </summary>
        /// <param name="left">左側値(IntPtrEx)</param>
        /// <param name="right">右側値(uint)</param>
        /// <returns>演算結果</returns>
        public static IntPtrEx operator +(IntPtrEx left, uint right) => Instance((IntPtr)(left.Value.GetUnsigned() + right));
        /// <summary>
        /// オペレーター「＋」
        /// </summary>
        /// <param name="left">左側値(uint)</param>
        /// <param name="right">右側値(IntPtrEx)</param>
        /// <returns>演算結果</returns>
        public static IntPtrEx operator +(uint left, IntPtrEx right) => Instance((IntPtr)(left + right.Value.GetUnsigned()));
        /// <summary>
        /// オペレーター「＋」
        /// </summary>
        /// <param name="left">左側値(IntPtrEx)</param>
        /// <param name="right">右側値(int)</param>
        /// <returns>演算結果</returns>
        public static IntPtrEx operator +(IntPtrEx left, int right) => Instance((IntPtr)(left.Value.GetSigned() + right));
        /// <summary>
        /// オペレーター「＋」
        /// </summary>
        /// <param name="left">左側値(int)</param>
        /// <param name="right">右側値(IntPtrEx)</param>
        /// <returns>演算結果</returns>
        public static IntPtrEx operator +(int left, IntPtrEx right) => Instance((IntPtr)(left + right.Value.GetSigned()));
        /// <summary>
        /// オペレーター「＋＋」
        /// </summary>
        /// <param name="value">値(IntPtrEx)</param>
        /// <returns>演算結果</returns>
        public static IntPtrEx operator ++(IntPtrEx value) => Instance((IntPtr)(value.Value.GetUnsigned() + 1));


        /// <summary>
        /// オペレーター「ー」
        /// </summary>
        /// <param name="left">左側値(IntPtrEx)</param>
        /// <param name="right">右側値(IntPtrEx)</param>
        /// <returns>演算結果</returns>
        public static IntPtrEx operator -(IntPtrEx left, IntPtrEx right) => Instance((IntPtr)(left.Value.GetUnsigned() - right.Value.GetUnsigned()));
#if WIN32
        /// <summary>
        /// オペレーター「ー」
        /// </summary>
        /// <param name="left">左側値(IntPtrEx)</param>
        /// <param name="right">右側値(IntPtr)</param>
        /// <returns>演算結果</returns>
		public static IntPtrEx operator -(IntPtrEx left, IntPtr right) => Instance((IntPtr)(left.Value.GetUnsigned() - (uint)right));
        /// <summary>
        /// オペレーター「ー」
        /// </summary>
        /// <param name="left">左側値(IntPtr)</param>
        /// <param name="right">右側値(IntPtrEx)</param>
        /// <returns>演算結果</returns>
		public static IntPtrEx operator -(IntPtr left, IntPtrEx right) => Instance((IntPtr)((uint)left - right.Value.GetUnsigned()));
#else
        /// <summary>
        /// オペレーター「ー」
        /// </summary>
        /// <param name="left">左側値(IntPtrEx)</param>
        /// <param name="right">右側値(IntPtr)</param>
        /// <returns>演算結果</returns>
        public static IntPtrEx operator -(IntPtrEx left, IntPtr right) => Instance((IntPtr)(left.Value.GetUnsigned() - (ulong)right));
        /// <summary>
        /// オペレーター「ー」
        /// </summary>
        /// <param name="left">左側値(IntPtr)</param>
        /// <param name="right">右側値(IntPtrEx)</param>
        /// <returns>演算結果</returns>
		public static IntPtrEx operator -(IntPtr left, IntPtrEx right) => Instance((IntPtr)((ulong)left - right.Value.GetUnsigned()));
        /// <summary>
        /// オペレーター「ー」
        /// </summary>
        /// <param name="left">左側値(IntPtrEx)</param>
        /// <param name="right">右側値(ulong)</param>
        /// <returns>演算結果</returns>
		public static IntPtrEx operator -(IntPtrEx left, ulong right) => Instance((IntPtr)(left.Value.GetUnsigned() - right));
        /// <summary>
        /// オペレーター「ー」
        /// </summary>
        /// <param name="left">左側値(ulong)</param>
        /// <param name="right">右側値(IntPtrEx)</param>
        /// <returns>演算結果</returns>
		public static IntPtrEx operator -(ulong left, IntPtrEx right) => Instance((IntPtr)(left - right.Value.GetUnsigned()));
#endif
        /// <summary>
        /// オペレーター「ー」
        /// </summary>
        /// <param name="left">左側値(IntPtrEx)</param>
        /// <param name="right">右側値(uint)</param>
        /// <returns>演算結果</returns>
		public static IntPtrEx operator -(IntPtrEx left, uint right) => Instance((IntPtr)(left.Value.GetUnsigned() - right));
        /// <summary>
        /// オペレーター「ー」
        /// </summary>
        /// <param name="left">左側値(uint)</param>
        /// <param name="right">右側値(IntPtrEx)</param>
        /// <returns>演算結果</returns>
		public static IntPtrEx operator -(uint left, IntPtrEx right) => Instance((IntPtr)(left - right.Value.GetUnsigned()));
        /// <summary>
        /// オペレーター「ー」
        /// </summary>
        /// <param name="left">左側値(IntPtrEx)</param>
        /// <param name="right">右側値(int)</param>
        /// <returns>演算結果</returns>
		public static IntPtrEx operator -(IntPtrEx left, int right) => Instance((IntPtr)(left.Value.GetSigned() - right));
        /// <summary>
        /// オペレーター「ー」
        /// </summary>
        /// <param name="left">左側値(int)</param>
        /// <param name="right">右側値(IntPtrEx)</param>
        /// <returns>演算結果</returns>
		public static IntPtrEx operator -(int left, IntPtrEx right) => Instance((IntPtr)(left - right.Value.GetSigned()));
        /// <summary>
        /// オペレーター「ーー」
        /// </summary>
        /// <param name="value">値(IntPtrEx)</param>
        /// <returns>演算結果</returns>
		public static IntPtrEx operator --(IntPtrEx value) => Instance((IntPtr)(value.Value.GetUnsigned() - 1));

        /// <summary>
        /// オペレーター「==」
        /// </summary>
        /// <param name="left">左側値(IntPtrEx)</param>
        /// <param name="right">右側値(object)</param>
        /// <returns>演算結果</returns>
        public static bool operator ==(IntPtrEx left, object right) => left.Equals(right);
        /// <summary>
        /// オペレーター「!=」
        /// </summary>
        /// <param name="left">左側値(IntPtrEx)</param>
        /// <param name="right">右側値(object)</param>
        /// <returns>演算結果</returns>
		public static bool operator !=(IntPtrEx left, object right) => left.Equals(right);
        /// <summary>
        /// オペレーター「<」
        /// </summary>
        /// <param name="left">左側値(IntPtrEx)</param>
        /// <param name="right">右側値(object)</param>
        /// <returns>演算結果</returns>
        public static bool operator <(IntPtrEx left, object right) => left.CompareTo(right) < 0;
        /// <summary>
        /// オペレーター「>」
        /// </summary>
        /// <param name="left">左側値(IntPtrEx)</param>
        /// <param name="right">右側値(object)</param>
        /// <returns>演算結果</returns>
		public static bool operator >(IntPtrEx left, object right) => left.CompareTo(right) > 0;
        /// <summary>
        /// オペレーター「<=」
        /// </summary>
        /// <param name="left">左側値(IntPtrEx)</param>
        /// <param name="right">右側値(object)</param>
        /// <returns>演算結果</returns>
		public static bool operator <=(IntPtrEx left, object right) => left.CompareTo(right) <= 0;
        /// <summary>
        /// オペレーター「>=」
        /// </summary>
        /// <param name="left">左側値(IntPtrEx)</param>
        /// <param name="right">右側値(object)</param>
        /// <returns>演算結果</returns>
		public static bool operator >=(IntPtrEx left, object right) => left.CompareTo(right) >= 0;
	}
}
