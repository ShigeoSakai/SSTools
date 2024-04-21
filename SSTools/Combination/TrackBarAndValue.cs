using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSTools
{
	public partial class TrackBarAndValue : UserControl
	{
		/// <summary>
		/// デフォルトサイズ
		/// </summary>
		protected override Size DefaultSize => new Size(180, 28);
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public TrackBarAndValue()
		{
			InitializeComponent();
			// 基準になるTextBox(NumericUpDown)のサイズ(不変)
			Size textBoxSize = new Size(TbValue.Width + TbValue.Margin.Top + TbValue.Margin.Bottom,
				TbValue.Height + TbValue.Margin.Right + TbValue.Margin.Left);
			// 最小サイズを設定
			MinimumSize = textBoxSize;
			// レイアウト実行
			LayoutExec(DefaultSize);

			// トラックバーの値を一致させる
			TrackBarValue.BeginInit();
			TrackBarValue.Minimum = CalcValue(TbValue.Minimum, DecimalPlaces);
			TrackBarValue.Maximum = CalcValue(TbValue.Maximum, DecimalPlaces);
			TrackBarValue.Value = SetTrackBarValue(TbValue.Value, _reverse, DecimalPlaces);
			TrackBarValue.SmallChange = CalcValue(TbValue.Increment, DecimalPlaces);
			TrackBarValue.EndInit();


		}
		/// <summary>
		/// コントロールの向き
		/// </summary>
		private Orientation _orientation = Orientation.Horizontal;
		/// <summary>
		/// コントロールの向き(プロパティ)
		/// </summary>
		public Orientation Orientation
		{
			get => _orientation;
			set
			{
				if (_orientation != value)
				{	// 向きを変更
					ChangeOrientation(value,Size);
				}
			}
		}
		/// <summary>
		/// コントロールの向きとサイズを変更
		/// </summary>
		/// <param name="orientation">向き</param>
		/// <param name="size">サイズ</param>
		private void ChangeOrientation(Orientation orientation,Size size)
		{
			// 基準になるTextBox(NumericUpDown)のサイズ
			Size textBoxSize = new Size(TbValue.Width + TbValue.Margin.Top + TbValue.Margin.Bottom,
				TbValue.Height + TbValue.Margin.Right + TbValue.Margin.Left);
			//　結果のサイズ
			Size result = new Size();
			if (_orientation != orientation)
			{	// 向きが変わった
				result.Width = textBoxSize.Width + ((orientation == Orientation.Horizontal) ? size.Height - textBoxSize.Height : 0);
				result.Height = textBoxSize.Height + ((orientation == Orientation.Vertical) ? size.Width - textBoxSize.Width : 0);
				_orientation = orientation;
			}
			else
			{	// 向きは同じ
				result = Size;
			}
			// レイアウト実行
			LayoutExec(result);
		}
		/// <summary>
		/// レイアウト実行
		/// </summary>
		/// <param name="size">コントロールのサイズ</param>
		private void LayoutExec(Size size)
		{
			SuspendLayout();
			// 基準になるTextBox(NumericUpDown)のサイズ
			Size textBoxSize = new Size(TbValue.Width + TbValue.Margin.Top + TbValue.Margin.Bottom,
				TbValue.Height + TbValue.Margin.Right + TbValue.Margin.Left);

			if (_orientation == Orientation.Horizontal)
			{   // 横方向
				// TextBox(NumericUpDown)の位置
				TbValue.Location = new Point(TbValue.Margin.Left, TbValue.Margin.Top + 3);
				// トラックバーの方向
				TrackBarValue.Orientation = Orientation.Horizontal;
				// トラックバー目盛り位置
				TrackBarValue.TickStyle = TickStyle.BottomRight;
				// トラックバーサイズ
				TrackBarValue.Size = new Size(
					size.Width - textBoxSize.Width - TrackBarValue.Margin.Right,
					textBoxSize.Height + 3 - TrackBarValue.Margin.Top - TrackBarValue.Margin.Bottom);
				// トラックバー位置
				TrackBarValue.Location = new Point(
					textBoxSize.Width, TrackBarValue.Margin.Top);
				// コントロールのサイズ
				Size = new Size(textBoxSize.Width + TrackBarValue.Size.Width + TrackBarValue.Margin.Right,
					textBoxSize.Height + 3);
			}
			else
			{   // 縦方向
				// TextBox(NumericUpDown)の位置
				TbValue.Location = new Point(TbValue.Margin.Left, TbValue.Margin.Top + 3);
				// トラックバーの方向
				TrackBarValue.Orientation = Orientation.Vertical;
				// トラックバー目盛り位置
				TrackBarValue.TickStyle = TickStyle.Both;
				// トラックバーサイズ
				TrackBarValue.Size = new Size(
					46,
					size.Height - textBoxSize.Height - TrackBarValue.Margin.Bottom);
				// トラックバー位置
				TrackBarValue.Location = new Point(
					(textBoxSize.Width - TrackBarValue.Width)/2,
					textBoxSize.Height);
				// コントロールのサイズ
				Size = new Size(textBoxSize.Width,
					textBoxSize.Height + TrackBarValue.Size.Height + TrackBarValue.Margin.Bottom);
			}
			ResumeLayout();
		}
		/// <summary>
		/// サイズ変更
		/// </summary>
		/// <param name="e"></param>
		protected override void OnSizeChanged(EventArgs e)
		{
			// レイアウトを実行
			LayoutExec(Size);
			// 元の処理を呼ぶ
			base.OnSizeChanged(e);
		}
		/// <summary>
		/// 読み取り専用か？
		/// </summary>
		[Category("動作"),
			Description("読み取り専用の場合、true"), DefaultValue(false)]
		public bool ReadOnly
		{
			get => TbValue.ReadOnly;
			set
			{
				TbValue.ReadOnly = value;
				TrackBarValue.Enabled = !value;
			}
		}
		[Category("動作"),
			DefaultValue(HorizontalAlignment.Left),
			Description("スピンボックス (アップダウンコントロール) でのテキストの配置")]
		public HorizontalAlignment TextAlign
		{
			get => TbValue.TextAlign;
			set => TbValue.TextAlign = value;
		}
		[Category("動作"),
			DefaultValue(LeftRightAlignment.Right),
			Description("スピンボックス (アップダウンコントロール) の上向きの矢印ボタンと下向きの矢印ボタンの配置")]
		public LeftRightAlignment UpDownAlign
		{
			get => TbValue.UpDownAlign;
			set => TbValue.UpDownAlign = value;

		}
		[Category("動作"),
			DefaultValue(0),
			Description("スピンボックス (アップダウンコントロール) に表示する小数部の桁数")]
		public int DecimalPlaces
		{
			get => TbValue.DecimalPlaces;
			set
			{
				if (TbValue.DecimalPlaces != value)
				{
					// トラックバーの値を変更
					TrackBarValue.BeginInit();
					TrackBarValue.Minimum = CalcValue(TbValue.Minimum, value);
					TrackBarValue.Maximum = CalcValue(TbValue.Maximum, value);
					TrackBarValue.Value = SetTrackBarValue(TbValue.Value, _reverse, value);
					TrackBarValue.SmallChange = CalcValue(TbValue.Increment, value);
					TrackBarValue.LargeChange = CalcValue(ToDecimal(TrackBarValue.LargeChange,DecimalPlaces), value);
					TrackBarValue.TickFrequency = CalcValue(ToDecimal(TrackBarValue.TickFrequency,DecimalPlaces), value);
					TrackBarValue.EndInit();
					TbValue.DecimalPlaces = value;
				}
			}

		}
		[Category("動作"),
			DefaultValue(false), 
			Description("スピン ボックス (アップダウンコントロール) に値を16進形式で表示するかどうかを示す値")]
		public bool Hexadecimal
		{ 
			get => TbValue.Hexadecimal;
			set => TbValue.Hexadecimal = value;
		}
		[Category("動作"),
			DefaultValue(1), 
			Description("上向きまたは下向きの矢印ボタンがクリックされたときに、スピンボックス (アップダウンコントロール) で増分または減分する値")]
		public decimal Increment
		{
			get => TbValue.Increment;
			set
			{
				TbValue.Increment = value;
				TrackBarValue.SmallChange = CalcValue(value, DecimalPlaces);
			}
		}
		[Category("動作"),
			DefaultValue(1),
			Description("わずかに移動したときにTrackBar.Valueプロパティに対して加算または減算される値")]
		public decimal SmallChange
		{
			get => TbValue.Increment;
			set => Increment = value;
		}
		[Category("動作"),
			DefaultValue(10),
			Description("大きく移動したときにTrackBar.Valueプロパティに対して加算または減算される値")]
		public decimal LargeChange
		{
			get => ToDecimal(TrackBarValue.LargeChange,DecimalPlaces);
			set => TrackBarValue.LargeChange = CalcValue(value,DecimalPlaces);
		}
		[Category("動作"),
			DefaultValue(10),
			Description("コントロールに描画された目盛り間のデルタを指定する値")]
		public decimal TickFrequency 
		{
			get => ToDecimal(TrackBarValue.TickFrequency,DecimalPlaces);
			set => TrackBarValue.TickFrequency = CalcValue(value,DecimalPlaces);
		}
		/// <summary>
		/// 最大値
		/// </summary>
		[Category("動作") ,RefreshProperties(RefreshProperties.All),
			Description("最大値を取得または設定します。"),DefaultValue(typeof(decimal),"100")]
		public decimal Maximum
		{
			get => TbValue.Maximum;
			set
			{
				TbValue.Maximum = value;
				TrackBarValue.Maximum = CalcValue(value, DecimalPlaces);
				if (Value > TbValue.Maximum)
					Value = TbValue.Maximum;
			}
		}
		/// <summary>
		/// 最小値
		/// </summary>
		[Category("動作"), RefreshProperties(RefreshProperties.All),
			Description("最小値を取得または設定します。"), DefaultValue(typeof(decimal), "0")]
		public decimal Minimum
		{
			get => TbValue.Minimum;
			set
			{
				TbValue.Minimum = value;
				TrackBarValue.Minimum = CalcValue(value,DecimalPlaces);
				if (Value < TbValue.Minimum)
					Value = TbValue.Minimum;
			}
		}
		/// <summary>
		/// 値
		/// </summary>
		[Category("動作"), RefreshProperties(RefreshProperties.All),
			Description("コントロールに表示する数値。"), DefaultValue(typeof(decimal), "0")]
		public decimal Value
		{
			get => TbValue.Value;
			set
			{
				if (value < Minimum)
					value = Minimum;
				else if (value > Maximum)
					value = Maximum;
				TbValue.Value = value;
				TrackBarValue.Value = SetTrackBarValue(value, _reverse, DecimalPlaces);
			}
		}
		/// <summary>
		/// 軸の反転
		/// </summary>
		private bool _reverse = false;
		public bool Reverse
		{
			get => _reverse;
			set
			{
				if (_reverse != value)
				{
					decimal oldValue = GetTrackBarValue(_reverse,DecimalPlaces);
					_reverse = value;
					TrackBarValue.Value = SetTrackBarValue(oldValue, _reverse, DecimalPlaces);
				}
			}
		}

		private decimal Pow10(int exp)
		{
			decimal pow10 = 1;
			for (int i = 0; i < exp; i++)
			{
				pow10 *= 10;
			}
			return pow10;
		}

		private int CalcValue(decimal value, int DecimalPlaces)
		{
			return decimal.ToInt32(value * Pow10(DecimalPlaces));
		}
		private decimal ToDecimal(int value, int DecimalPlaces)
		{
			return ((decimal)value / Pow10(DecimalPlaces));
		}
		private decimal GetTrackBarValue(bool isReverse, int DecimalPlaces)
		{
			int value = TrackBarValue.Value;
			if (isReverse) 
			{
				value = TrackBarValue.Maximum - value + TrackBarValue.Minimum;
			}
			return ToDecimal(value, DecimalPlaces);
		}
		private int SetTrackBarValue(decimal value, bool isReverse, int DecimalPlaces) 
		{
			int trackValue = CalcValue(value, DecimalPlaces);
			if (isReverse)
			{
				trackValue = TrackBarValue.Maximum - trackValue + TrackBarValue.Minimum;
			}
			return trackValue;
		}
		/// <summary>
		/// アップダウンコントロールの値変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TbValue_ValueChanged(object sender, EventArgs e)
		{
			// トラックバーに設定
			TrackBarValue.Value = SetTrackBarValue(TbValue.Value, _reverse, DecimalPlaces);
			// 値変更イベント発行
			OnValueChanged();
		}
		/// <summary>
		/// トラックバーの値変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TrackBarValue_ValueChanged(object sender, EventArgs e)
		{
			TbValue.Value = GetTrackBarValue(_reverse,DecimalPlaces);
			// 値変更イベント発行
			OnValueChanged();
		}
		/// <summary>
		/// トラックバースクロール
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TrackBarValue_Scroll(object sender, EventArgs e)
		{
			TbValue.Value = GetTrackBarValue(_reverse, DecimalPlaces);
			// スクロールイベント発行
			OnScroll();
		}

		public event EventHandler ValueChanged;
		protected virtual void OnValueChanged()
		{
			ValueChanged?.Invoke(this, EventArgs.Empty);
		}
		public new event EventHandler Scroll;
		protected virtual void OnScroll()
		{
			Scroll?.Invoke(this, EventArgs.Empty);
		}

	}
}
