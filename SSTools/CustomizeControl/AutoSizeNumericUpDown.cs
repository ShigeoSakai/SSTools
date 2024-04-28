using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSTools
{
	/// <summary>
	/// AutoSizeに対応したNumericUpDown
	/// </summary>
	public partial class AutoSizeNumericUpDown : NumericUpDown
	{
		/// <summary>
		/// コンポーネント
		/// </summary>
		private IContainer components;

		/// <summary>
		/// Dispose I/F
		/// </summary>
		/// <param name="disposing"></param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
		/// <summary>
		/// コンポーネントの初期化
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.SuspendLayout();
			this.ResumeLayout(false);
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public AutoSizeNumericUpDown()
		{
			InitializeComponent();
			// 文字位置右
			TextAlign = HorizontalAlignment.Right;
			// ローカルに設定
			_decimalPlaces = base.DecimalPlaces;
			_hexadecimal = base.Hexadecimal;
			_maximum = base.Maximum;
			_minimum = base.Minimum;
		}
		/// <summary>
		/// 更新中止中
		/// </summary>
		private bool UpdatePending = false;
		/// <summary>
		/// 初期化開始
		/// </summary>
		public new void BeginInit()
		{
			// 更新中止
			UpdatePending = true;
			base.BeginInit();
		}
		/// <summary>
		/// 初期化終了
		/// </summary>
		public new void EndInit()
		{
			// 更新再開
			UpdatePending = false;
			// サイズを計算して設定
			SetSize();
			base.EndInit();
		}
		/// <summary>
		/// 幅の計算
		/// </summary>
		/// <returns>計算されたサイズ</returns>
		/// <remarks>
		/// AutoSize=Trueの場合は、最大値(もしくは最小値）の文字数分'8'もしくは'D'を描画したサイズ＋
		/// 　　　　　　　　　　　　スクロールバー幅(上下ボタン分)＋２
		///          Falseの場合は、自身の幅
		/// </remarks>
		private Size CalcSize()
		{
			if (AutoSize)
			{
				string maxStr, minStr;
				char c;
				if (_hexadecimal)
				{
					maxStr = ((int)_maximum).ToString("X");
					minStr = ((int)_minimum).ToString("X");
					c = 'D';
				}
				else
				{
					string fmt = string.Format("F{0}", _decimalPlaces);
					// 最大値、最小値をフォーマットに従って文字列変換
					maxStr = _maximum.ToString(fmt);
					minStr = _minimum.ToString(fmt);
					c = '8';
				}
				// 長い方を採用
				int maxLen = (maxStr.Length > minStr.Length) ? maxStr.Length : minStr.Length;
				// 'D'を文字長さ分生成
				string calcStr = new string(c, maxLen);
				// 描画サイズを求める
				Size size = TextRenderer.MeasureText(calcStr, Font);
				return new Size(size.Width + SystemInformation.VerticalScrollBarWidth + 2,size.Height + 2);
			}
			else
			{	// 自身のサイズを返す
				return Size;
			}
		}
		/// <summary>
		/// サイズ変更中フラグ
		/// </summary>
		private bool IsSizeChanging = false;
		/// <summary>
		/// サイズ設定
		/// </summary>
		/// <remarks>
		/// 更新中止中以外で、AutoSizeがtrue時のみサイズを設定
		/// </remarks>
		private void SetSize()
		{
			if ((UpdatePending == false) && (AutoSize))
			{
				bool before = SetChanging(ref IsSizeChanging);
				Size = CalcSize();
				RestoreChanging(ref IsSizeChanging, before);
			}
		}
		/// <summary>
		/// サイズが変更された
		/// </summary>
		/// <param name="e"></param>
		protected override void OnSizeChanged(EventArgs e)
		{
			if (IsChanging(ref IsSizeChanging) == false)
			{   // 本クラス以外から設定された=> 再計算
				SetSize();
			}
			base.OnSizeChanged(e);
		}
		/// <summary>
		/// 変更中フラグ設定
		/// </summary>
		/// <param name="flag">変更中フラグ</param>
		/// <returns>設定前の状態</returns>
		private bool SetChanging(ref bool flag)
		{
			bool before = flag;
			flag |= true;
			return before;
		}
		/// <summary>
		/// 変更中フラグを戻す
		/// </summary>
		/// <param name="flag">変更中フラグ</param>
		/// <param name="value">以前の状態</param>
		private void RestoreChanging(ref bool flag,bool value) => flag = value;
		/// <summary>
		/// 変更中かどうか
		/// </summary>
		/// <param name="flag">変更中フラグ</param>
		/// <returns>true:変更中</returns>
		private bool IsChanging(ref bool flag) => flag;

		/// <summary>
		/// 小数点位置
		/// </summary>
		private int _decimalPlaces;
		/// <summary>
		/// 小数点位置(プロパティ)
		/// </summary>
		/// <remarks>
		/// 変更イベントがない為、newで上書き
		/// </remarks>
		public new int DecimalPlaces
		{
			get => _decimalPlaces;
			set
			{
				if (_decimalPlaces != value)
				{
					_decimalPlaces = value;
					base.DecimalPlaces = _decimalPlaces;
					OnDecimalPlacesChange(new EventArgs());
				}
			}
		}
		/// <summary>
		/// 小数点位置変更イベント
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnDecimalPlacesChange(EventArgs e)
		{
			// サイズを計算して設定
			SetSize();
		}
		/// <summary>
		/// 16進数表示
		/// </summary>
		private bool _hexadecimal;
		/// <summary>
		/// 16進数表示(プロパティ)
		/// </summary>
		/// <remarks>
		/// 変更イベントがない為、newで上書き
		/// </remarks>
		public new bool Hexadecimal
		{
			get => _hexadecimal;
			set
			{
				if (_hexadecimal != value)
				{
					_hexadecimal = value;
					base.Hexadecimal = _hexadecimal;
					OnHexadecimalChange(new EventArgs());
				}
			}
		}
		/// <summary>
		/// 16進数表示変更イベント
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnHexadecimalChange(EventArgs e)
		{
			// サイズを計算して設定
			SetSize();
		}
		/// <summary>
		/// 最大値
		/// </summary>
		private decimal _maximum;
		/// <summary>
		/// 最大値(プロパティ)
		/// </summary>
		/// <remarks>
		/// 変更イベントがない為、newで上書き
		/// </remarks>
		public new decimal Maximum
		{
			get => _maximum;
			set
			{
				if (_maximum != value)
				{
					_maximum = value;
					base.Maximum = _maximum;
					OnMaximumChange(new EventArgs());
				}
			}
		}
		/// <summary>
		/// 最大値変更イベント
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMaximumChange(EventArgs e)
		{
			// サイズを計算して設定
			SetSize();
		}
		/// <summary>
		/// 最小値
		/// </summary>
		private decimal _minimum;
		/// <summary>
		/// 最小値(プロパティ)
		/// </summary>
		/// <remarks>
		/// 変更イベントがない為、newで上書き
		/// </remarks>
		public new decimal Minimum
		{
			get => _minimum;
			set
			{
				if (_minimum != value)
				{
					_minimum = value;
					base.Minimum = _minimum;
					OnMinimumChange(new EventArgs());
				}
			}
		}
		/// <summary>
		/// 最小値変更イベント
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMinimumChange(EventArgs e)
		{
			// サイズを計算して設定
			SetSize();
		}
		/// <summary>
		/// AutoSizeが変更された
		/// </summary>
		/// <param name="e"></param>
		protected override void OnAutoSizeChanged(EventArgs e)
		{
			// サイズを計算して設定
			SetSize();

			base.OnAutoSizeChanged(e);
		}
		/// <summary>
		/// フォントが変更された
		/// </summary>
		/// <param name="e"></param>
		protected override void OnFontChanged(EventArgs e)
		{
			// サイズを計算して設定
			SetSize();

			base.OnFontChanged(e);
		}
	}
}
