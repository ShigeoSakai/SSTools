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
		/// 最小サイズ
		/// </summary>
		private Size _minSize = new Size(158, 25);
		/// <summary>
		/// デフォルトサイズ
		/// </summary>
		protected override Size DefaultSize => _minSize;
		/// <summary>
		/// TrackBarが縦の時のデフォルトサイズ
		/// </summary>
		private readonly Size TrackBarDefaultSize = new Size(45,104);
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public TrackBarAndValue()
		{
			InitializeComponent();

			// 初期設定
			_size = DefaultSize;
			_orientation = Orientation.Horizontal;
			// マージン
			UpDownValue.Margin = new Padding(3, 3, 0, 3);
			TrackBarValue.Margin = new Padding(0, 3, 3, 3);

			// レイアウト実行
			ExecLayout(_size);
		}
		/// <summary>
		/// 設定フラグのセット
		/// </summary>
		/// <param name="flag">設定フラグ</param>
		/// <returns>以前の状態</returns>
		private bool SetChanging(ref bool flag)
		{
			bool result = flag;
			flag |= true;
			return result; ;
		}
		/// <summary>
		/// 設定フラグを以前の状態にする
		/// </summary>
		/// <param name="flag">設定フラグ</param>
		/// <param name="value">以前の状態</param>
		private void RestoreChanging(ref bool flag, bool value) => flag = value;
		/// <summary>
		/// 設定中か？
		/// </summary>
		/// <param name="flag">設定フラグ</param>
		/// <returns>true:設定中</returns>
		private bool IsChanging(ref bool flag) => flag;

		#region [イベント]
		/// <summary>
		/// 値変更イベント
		/// </summary>
		public event EventHandler ValueChanged;
		/// <summary>
		/// 値変更イベント発行
		/// </summary>
		protected virtual void OnValueChanged()
		{
			ValueChanged?.Invoke(this, EventArgs.Empty);
		}
		/// <summary>
		/// スクロールイベント
		/// </summary>
		public new event EventHandler Scroll;
		/// <summary>
		/// スクロールイベント発行
		/// </summary>
		protected virtual void OnScroll()
		{
			Scroll?.Invoke(this, EventArgs.Empty);
		}
		#endregion [イベント]

		#region [プロパティ]
		/// <summary>
		/// 読み取り専用か？
		/// </summary>
		[Category("動作"),
			Description("読み取り専用の場合、true"), DefaultValue(false)]
		public bool ReadOnly
		{
			get => UpDownValue.ReadOnly;
			set
			{
				UpDownValue.ReadOnly = value;
				TrackBarValue.Enabled = !value;
			}
		}
		[Category("動作"),
			DefaultValue(HorizontalAlignment.Left),
			Description("スピンボックス (アップダウンコントロール) でのテキストの配置")]
		public HorizontalAlignment TextAlign
		{
			get => UpDownValue.TextAlign;
			set => UpDownValue.TextAlign = value;
		}
		[Category("動作"),
			DefaultValue(LeftRightAlignment.Right),
			Description("スピンボックス (アップダウンコントロール) の上向きの矢印ボタンと下向きの矢印ボタンの配置")]
		public LeftRightAlignment UpDownAlign
		{
			get => UpDownValue.UpDownAlign;
			set => UpDownValue.UpDownAlign = value;

		}
		[Category("動作"),
			DefaultValue(0),
			Description("スピンボックス (アップダウンコントロール) に表示する小数部の桁数")]
		public int DecimalPlaces
		{
			get => UpDownValue.DecimalPlaces;
			set
			{
				if (UpDownValue.DecimalPlaces != value)
				{
					// トラックバーの値を変更
					TrackBarValue.BeginInit();
					TrackBarValue.Minimum = CalcValue(UpDownValue.Minimum, value);
					TrackBarValue.Maximum = CalcValue(UpDownValue.Maximum, value);
					TrackBarValue.Value = SetTrackBarValue(UpDownValue.Value, _reverse, value);
					TrackBarValue.SmallChange = CalcValue(UpDownValue.Increment, value);
					TrackBarValue.LargeChange = CalcValue(ToDecimal(TrackBarValue.LargeChange, DecimalPlaces), value);
					TrackBarValue.TickFrequency = CalcValue(ToDecimal(TrackBarValue.TickFrequency, DecimalPlaces), value);
					TrackBarValue.EndInit();
					UpDownValue.DecimalPlaces = value;
				}
			}

		}
		[Category("動作"),
			DefaultValue(false),
			Description("スピン ボックス (アップダウンコントロール) に値を16進形式で表示するかどうかを示す値")]
		public bool Hexadecimal
		{
			get => UpDownValue.Hexadecimal;
			set => UpDownValue.Hexadecimal = value;
		}
		[Category("動作"),
			DefaultValue(1),
			Description("上向きまたは下向きの矢印ボタンがクリックされたときに、スピンボックス (アップダウンコントロール) で増分または減分する値")]
		public decimal Increment
		{
			get => UpDownValue.Increment;
			set
			{
				UpDownValue.Increment = value;
				TrackBarValue.SmallChange = CalcValue(value, DecimalPlaces);
			}
		}
		[Category("動作"),
			DefaultValue(1),
			Description("わずかに移動したときにTrackBar.Valueプロパティに対して加算または減算される値")]
		public decimal SmallChange
		{
			get => UpDownValue.Increment;
			set => Increment = value;
		}
		[Category("動作"),
			DefaultValue(10),
			Description("大きく移動したときにTrackBar.Valueプロパティに対して加算または減算される値")]
		public decimal LargeChange
		{
			get => ToDecimal(TrackBarValue.LargeChange, DecimalPlaces);
			set => TrackBarValue.LargeChange = CalcValue(value, DecimalPlaces);
		}
		[Category("動作"),
			DefaultValue(10),
			Description("コントロールに描画された目盛り間のデルタを指定する値")]
		public decimal TickFrequency
		{
			get => ToDecimal(TrackBarValue.TickFrequency, DecimalPlaces);
			set => TrackBarValue.TickFrequency = CalcValue(value, DecimalPlaces);
		}
		/// <summary>
		/// 最大値
		/// </summary>
		[Category("動作"), RefreshProperties(RefreshProperties.All),
			Description("最大値を取得または設定します。"), DefaultValue(typeof(decimal), "100")]
		public decimal Maximum
		{
			get => UpDownValue.Maximum;
			set
			{
				UpDownValue.Maximum = value;
				TrackBarValue.Maximum = CalcValue(value, DecimalPlaces);
				if (Value > UpDownValue.Maximum)
					Value = UpDownValue.Maximum;
			}
		}
		/// <summary>
		/// 最小値
		/// </summary>
		[Category("動作"), RefreshProperties(RefreshProperties.All),
			Description("最小値を取得または設定します。"), DefaultValue(typeof(decimal), "0")]
		public decimal Minimum
		{
			get => UpDownValue.Minimum;
			set
			{
				UpDownValue.Minimum = value;
				TrackBarValue.Minimum = CalcValue(value, DecimalPlaces);
				if (Value < UpDownValue.Minimum)
					Value = UpDownValue.Minimum;
			}
		}
		/// <summary>
		/// 値
		/// </summary>
		[Category("動作"), RefreshProperties(RefreshProperties.All),
			Description("コントロールに表示する数値。"), DefaultValue(typeof(decimal), "0")]
		public decimal Value
		{
			get => UpDownValue.Value;
			set
			{
				if (value < Minimum)
					value = Minimum;
				else if (value > Maximum)
					value = Maximum;
				UpDownValue.Value = value;
				TrackBarValue.Value = SetTrackBarValue(value, _reverse, DecimalPlaces);
			}
		}
		/// <summary>
		/// 軸の反転
		/// </summary>
		private bool _reverse = false;
		[Category("動作"), 
			Description("軸を反転します。"), DefaultValue(false)]
		public bool Reverse
		{
			get => _reverse;
			set
			{
				if (_reverse != value)
				{
					decimal oldValue = GetTrackBarValue(_reverse, DecimalPlaces);
					_reverse = value;
					TrackBarValue.Value = SetTrackBarValue(oldValue, _reverse, DecimalPlaces);
				}
			}
		}
		/// <summary>
		/// UpDownコントロールのAutoSize
		/// </summary>
		[Category("動作"),
			Description("UpDownコントロールのAutoSize"), DefaultValue(true)]
		public bool UpDownControlAutoSize
		{
			get => UpDownValue.AutoSize;
			set => UpDownValue.AutoSize = value;
		}
		/// <summary>
		/// UpDownコントロールの固定サイズ
		/// </summary>
		[Category("動作"),
			Description("UpDownコントロールのAutoSizeがfalseの時の横幅"), DefaultValue(56)]
		public int UpDownControlWidth
		{
			get => UpDownValue.Width;
			set
			{
				if (UpDownControlAutoSize == false)
					UpDownValue.Width = value;
			}
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
				if ((_orientation != value) && 
					((Dock == DockStyle.None) || (Dock == DockStyle.Fill)))
				{   // 向きを変更
					Orientation oldValue = _orientation;
					_orientation = value;
					OnOrientationChange(new OrientationChangeEventArg(_orientation,oldValue));
				}
			}
		}
		#endregion [プロパティ]

		/// <summary>
		/// 10^expを計算
		/// </summary>
		/// <param name="exp">指数</param>
		/// <returns>10^expの値</returns>
		private decimal Pow10(int exp)
		{
			decimal pow10 = 1;
			for (int i = 0; i < exp; i++)
			{
				pow10 *= 10;
			}
			return pow10;
		}
		/// <summary>
		/// Decimal値をintに変換
		/// </summary>
		/// <param name="value">decimal値</param>
		/// <param name="DecimalPlaces">小数点位置</param>
		/// <returns>変換したint値</returns>
		private int CalcValue(decimal value, int DecimalPlaces)
		{
			return decimal.ToInt32(value * Pow10(DecimalPlaces));
		}
		/// <summary>
		/// int値をdecimal値に変換
		/// </summary>
		/// <param name="value">int値</param>
		/// <param name="DecimalPlaces">小数点位置</param>
		/// <returns>変換したdecimal値</returns>
		private decimal ToDecimal(int value, int DecimalPlaces)
		{
			return ((decimal)value / Pow10(DecimalPlaces));
		}
		/// <summary>
		/// TrackBarの値をdecimal値に変換
		/// </summary>
		/// <param name="isReverse">true:軸反転</param>
		/// <param name="DecimalPlaces">小数点位置</param>
		/// <returns>取得したdecimal値</returns>
		private decimal GetTrackBarValue(bool isReverse, int DecimalPlaces)
		{
			int value = TrackBarValue.Value;
			if (isReverse)
			{
				value = TrackBarValue.Maximum - value + TrackBarValue.Minimum;
			}
			return ToDecimal(value, DecimalPlaces);
		}
		/// <summary>
		/// decimal値をTrackBar値に変換
		/// </summary>
		/// <param name="value">decimal値</param>
		/// <param name="isReverse">true:軸反転</param>
		/// <param name="DecimalPlaces">小数点位置</param>
		/// <returns>TrackBar設定値</returns>
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
		private void UpDownValue_ValueChanged(object sender, EventArgs e)
		{
			// トラックバーに設定
			TrackBarValue.Value = SetTrackBarValue(UpDownValue.Value, _reverse, DecimalPlaces);
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
			UpDownValue.Value = GetTrackBarValue(_reverse, DecimalPlaces);
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
			UpDownValue.Value = GetTrackBarValue(_reverse, DecimalPlaces);
			// スクロールイベント発行
			OnScroll();
		}

		/// <summary>
		/// サイズ設定中フラグ
		/// </summary>
		private bool SizeChanging = false;

		/// <summary>
		/// コントロールのサイズ
		/// </summary>
		private Size _size;
		/// <summary>
		/// コントロールのサイズ（プロパティ）
		/// </summary>
		public new Size Size
		{
			get => _size;
			set
			{
				if ((_size != value) || (base.Size != value))
				{
					// 値を設定
					_size = value;
					base.Size = value;
				}
				// レイアウトを実行
				ExecLayout(_size);
			}
		}
		/// <summary>
		/// サイズを設定
		/// </summary>
		/// <param name="size">設定するサイズ</param>
		private void SetSize(Size size)
		{
			bool before = SetChanging(ref SizeChanging);
			Size = size;
			RestoreChanging(ref SizeChanging, before);
		}

		/// <summary>
		/// サイズ変更
		/// </summary>
		/// <param name="e"></param>
		protected override void OnSizeChanged(EventArgs e)
		{
			if (IsChanging(ref SizeChanging) == false)
			{	// このコントロール以外から設定された
				Size newSize = CalcSize(base.Size);
				SetSize(newSize);
			}
			base.OnSizeChanged(e);
		}
		/// <summary>
		/// UpDownコントロールのサイズ変更イベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UpDownValue_SizeChanged(object sender, EventArgs e)
		{
			Size newSize = CalcSize(base.Size);
			SetSize(newSize);
		}
		/// <summary>
		/// 向きが変わった時のイベント通知内容
		/// </summary>
		public class OrientationChangeEventArg
		{
			/// <summary>
			/// 変更後の値
			/// </summary>
			public Orientation NewValue { get; private set; }
			/// <summary>
			/// 変更前の値
			/// </summary>
			public Orientation OldValue { get; private set; }
			/// <summary>
			/// コンストラクション
			/// </summary>
			/// <param name="newValue">変更後の値</param>
			/// <param name="oldValue">変更前の値</param>
			public OrientationChangeEventArg(Orientation newValue, Orientation oldValue)
			{
				NewValue = newValue;
				OldValue = oldValue;
			}
		}
		/// <summary>
		/// 向きの変更
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnOrientationChange(OrientationChangeEventArg e)
		{
			if (e.NewValue != e.OldValue)
			{   // 向きが変わった
				// マージン
				UpDownValue.Margin = new Padding(UpDownValue.Margin.Left, UpDownValue.Margin.Top, UpDownValue.Margin.Bottom, UpDownValue.Margin.Right);
				TrackBarValue.Margin = new Padding(TrackBarValue.Margin.Top, TrackBarValue.Margin.Left, TrackBarValue.Margin.Right, TrackBarValue.Margin.Bottom);
				if (e.NewValue == Orientation.Horizontal)
				{   // 横向きに変わった
					TrackBarValue.Orientation = Orientation.Horizontal;
					TrackBarValue.Size = new Size(TrackBarValue.Height,UpDownValue.Height);
					TrackBarValue.TickStyle = TickStyle.BottomRight;
				}
				else
				{   // 縦向きに変わった
					TrackBarValue.Orientation = Orientation.Vertical;
					TrackBarValue.Size = new Size(TrackBarDefaultSize.Width, TrackBarValue.Width);
					TrackBarValue.TickStyle = TickStyle.Both;
				}
				// サイズを変更してレイアウトを実行
				SetSize(CalcSize());
			}
		}
		/// <summary>
		/// AutoSizeが変わった
		/// </summary>
		/// <param name="e"></param>
		protected override void OnAutoSizeChanged(EventArgs e)
		{
			// サイズを変更
			SetSize(CalcSize());
			base.OnAutoSizeChanged(e);
		}
		/// <summary>
		/// Dockが変わった
		/// </summary>
		/// <param name="e"></param>
		protected override void OnDockChanged(EventArgs e)
		{
			Orientation oldValue = Orientation;
			if ((Dock == DockStyle.Left) || (Dock == DockStyle.Right))
			{
				_orientation = Orientation.Vertical;
				OnOrientationChange(new OrientationChangeEventArg(_orientation, oldValue));
			}
			else if ((Dock == DockStyle.Top) || (Dock == DockStyle.Bottom))
			{
				_orientation = Orientation.Horizontal;
				OnOrientationChange(new OrientationChangeEventArg(_orientation, oldValue));
			}

			base.OnDockChanged(e);
		}
		/// <summary>
		/// 最小のサイズを求める
		/// </summary>
		/// <param name="size">サイズ指定</param>
		/// <returns>最小のサイズ</returns>
		private Size CalcMinimunSize(Size? size = null)
		{
			// TrackBarのサイズ
			Size trackBarSize = TrackBarValue.Size;
			// 各マージン
			Size upDownMargin = new Size(UpDownValue.Margin.Horizontal,UpDownValue.Margin.Vertical);
			Size trackBarMargin = new Size(TrackBarValue.Margin.Horizontal,TrackBarValue.Margin.Vertical);
			// 求める幅と高さ
			int width, height;
			if (_orientation == Orientation.Horizontal)
			{	// 横向き
				if (size.HasValue)
				{   // TrackBarのサイズを再計算
					trackBarSize = new Size(
						size.Value.Width - upDownMargin.Width - trackBarMargin.Width - UpDownValue.Width,
						size.Value.Height - trackBarMargin.Height
						);
				}
				// TrackBarの幅制限
				if (trackBarSize.Width < TrackBarDefaultSize.Height / 2)
					trackBarSize.Width = TrackBarDefaultSize.Height / 2;
				// TrackBarの高さ制限
				trackBarSize.Height = UpDownValue.Height;
				// 横幅
				width = upDownMargin.Width + UpDownValue.Width + trackBarMargin.Width + trackBarSize.Width;
				// 縦幅
				height = upDownMargin.Height + UpDownValue.Height;
			}
			else
			{   // 縦向き
				if (size.HasValue)
				{   // TrackBarのサイズを再計算
					trackBarSize = new Size(
						size.Value.Width - trackBarMargin.Width,
						size.Value.Height - upDownMargin.Height - UpDownValue.Height - trackBarMargin.Height
						);
				}
				// TrackBarの高さ制限
				if (trackBarSize.Height < TrackBarDefaultSize.Height / 2)
					trackBarSize.Height = TrackBarDefaultSize.Height / 2;
				// TrackBarの幅制限
				trackBarSize.Width = TrackBarDefaultSize.Width;
				// 横幅
				width = upDownMargin.Width + UpDownValue.Width;
				// 横幅制限
				if (width < TrackBarDefaultSize.Width + trackBarMargin.Width)
					width = TrackBarDefaultSize.Width + trackBarMargin.Width;
				// 縦幅
				height = upDownMargin.Height + UpDownValue.Height + trackBarMargin.Height + trackBarSize.Height;
			}
			return new Size(width, height);
		}
		/// <summary>
		/// サイズを算出
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		private Size CalcSize(Size? size = null)
		{
			// 最小のサイズを求める
			Size minSize = CalcMinimunSize(size);

			// 最小サイズを更新
			_minSize = minSize;

			if ((Dock != DockStyle.None) && (Parent != null))
			{	// Dock指定ありは、AutoSize無視でサイズを算出
				switch(Dock)
				{
					case DockStyle.Fill:
						return Parent.ClientSize;
					case DockStyle.Top:
					case DockStyle.Bottom:
						return new Size(Parent.ClientSize.Width,minSize.Height);
					case DockStyle.Left:
					case DockStyle.Right:
						return new Size(minSize.Width, Parent.ClientSize.Height);
				}
			}

			// AutoSizeの場合は、最小サイズを返す
			if (AutoSize)
				return minSize;
			else
			{	// 全体のサイズを変更
				Size newSize = (size.HasValue) ? size.Value : Size;
				if (newSize.Width < minSize.Width)
					newSize.Width = minSize.Width;
				if (newSize.Height < minSize.Height)
					newSize.Height = minSize.Height;
				return newSize;
			}
		}
		/// <summary>
		/// レイアウト実行
		/// </summary>
		/// <param name="size"></param>
		private void ExecLayout(Size? size)
		{
			// TrackBarのサイズ
			Size trackBarSize = TrackBarValue.Size;
			// 各マージン
			Size upDownMargin = new Size(UpDownValue.Margin.Horizontal, UpDownValue.Margin.Vertical);
			Size trackBarMargin = new Size(TrackBarValue.Margin.Horizontal, TrackBarValue.Margin.Vertical);

			SuspendLayout();
			if (_orientation == Orientation.Horizontal)
			{   // 横向き
				int height = (size.HasValue) ? size.Value.Height : Height;
				if (size.HasValue)
				{   // TrackBarのサイズを再計算
					trackBarSize = new Size(
						size.Value.Width - upDownMargin.Width - trackBarMargin.Width - UpDownValue.Width,
						size.Value.Height - trackBarMargin.Height
						);
				}
				// TrackBarの幅制限
				if (trackBarSize.Width < TrackBarDefaultSize.Height / 2)
					trackBarSize.Width = TrackBarDefaultSize.Height / 2;
				// TrackBarの高さ制限
				trackBarSize.Height = UpDownValue.Height;
				// UpDownの位置
				UpDownValue.Location = new Point(
					UpDownValue.Margin.Left,
					(height - UpDownValue.Height) / 2
					);

				// TrackBarのサイズ
				TrackBarValue.Size = trackBarSize;
				// TrackBarの位置
				TrackBarValue.Location = new Point(
					upDownMargin.Width + UpDownValue.Width + TrackBarValue.Margin.Left,
					(height - TrackBarValue.Height) /2
					);
			}
			else
			{   // 縦向き
				int width = (size.HasValue) ? size.Value.Width : Width;
				if (size.HasValue)
				{   // TrackBarのサイズを再計算
					trackBarSize = new Size(
						size.Value.Width - trackBarMargin.Width,
						size.Value.Height - upDownMargin.Height - UpDownValue.Height - trackBarMargin.Height
						);
					// TrackBarサイズの幅制限
				}
				// TrackBarの高さ制限
				if (trackBarSize.Height < TrackBarDefaultSize.Height / 2)
					trackBarSize.Height = TrackBarDefaultSize.Height / 2;
				// TrackBarの幅制限
				trackBarSize.Width = TrackBarDefaultSize.Width;

				// UpDownの位置
				UpDownValue.Location = new Point(
					(width - UpDownValue.Width)/2,
					UpDownValue.Margin.Top
					);
				// TrackBarのサイズ
				TrackBarValue.Size = trackBarSize;
				// TrackBarの位置
				TrackBarValue.Location = new Point(
					(width - TrackBarValue.Width) / 2,
					upDownMargin.Height + UpDownValue.Height + TrackBarValue.Margin.Top
					);
			}
			ResumeLayout();
		}
	}
}
