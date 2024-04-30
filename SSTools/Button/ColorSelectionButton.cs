using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSTools
{
	/// <summary>
	/// 色選択ボタン
	/// </summary>
	public class ColorSelectionButton :Button
	{
		/// <summary>
		/// ToolTip
		/// </summary>
		private ToolTip ColorToolTip;
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
			this.ColorToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// ColorChooser
			// 
			this.ResumeLayout(false);
		}

		/// <summary>
		/// テキストに表示する文字列の候補
		/// </summary>
		[Flags]
		public enum SHOW_COLOR_NAME
		{
			[Description("色名表示しない")]
			NONE = 0,
			[Description("システムカラーのみ")]
			SYSTEM = 0x01,
			[Description("定義済み色のみ")]
			KNOWN_COLOR = 0x02,
			[Description("名前のある色")]
			ALL = SYSTEM | KNOWN_COLOR,
		}
		/// <summary>
		/// システム・規定色の一覧
		/// </summary>
		private readonly List<Color> _systemColors;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ColorSelectionButton()
		{
			// コンポーネントの初期化
			InitializeComponent();
			// ボタンをフラットに
			FlatStyle = FlatStyle.Flat;

			// システムカラーリスト取得
			_systemColors = typeof(Color).GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)
				.Select(p => Color.FromName(p.Name))
				.ToList();
			foreach (KnownColor c in Enum.GetValues(typeof(KnownColor)))
				_systemColors.Add(Color.FromKnownColor(c));
		}
		/// <summary>
		/// 色
		/// </summary>
		[Category("色指定"), Description("色"), RefreshProperties(RefreshProperties.All)]
		public Color Color
		{
			get => BackColor;
			set => BackColor = value;
		}
		/// <summary>
		/// テキストに表示する文字列
		/// </summary>
		private SHOW_COLOR_NAME _showColorName = SHOW_COLOR_NAME.KNOWN_COLOR;

		/// <summary>
		/// テキストに表示する文字列(プロパティ)
		/// </summary>
		[Category("色指定"), Description("テキストに表示する文字列"), DefaultValue(typeof(SHOW_COLOR_NAME), "ALL"),
			RefreshProperties(RefreshProperties.All)]
		public SHOW_COLOR_NAME ShowColorName
		{
			get => _showColorName;
			set
			{
				_showColorName = value;
				if (_showColorName != SHOW_COLOR_NAME.NONE)
				{   // テキストを変更する
					isBackColorChanging = true;
					Text = ToColorName(Color);
					isBackColorChanging = false;
				}
			}
		}

		/// <summary>
		/// 使用可能なすべての色を基本色セットとしてダイアログボックスに表示
		/// </summary>
		[Category("色指定ダイアログ"), DefaultValue(false),
			Description("使用可能なすべての色を基本色セットとしてダイアログボックスに表示する場合は true。それ以外の場合は false。")]
		public bool AnyColor { get; set; } = false;

		/// <summary>
		/// カスタムカラーの作成用コントロールを表示
		/// </summary>
		[Category("色指定ダイアログ"), DefaultValue(false),
			Description("ダイアログボックスが開かれたときに、カスタムカラーの作成用コントロールを表示する場合は true。それ以外の場合は false。")]
		public bool FullOpen { get; set; } = false;

		/// <summary>
		/// カスタムカラーを定義できる
		/// </summary>
		[Category("色指定ダイアログ"), DefaultValue(true),
			Description("ユーザーがカスタムカラーを定義できる場合は true。それ以外の場合は false")]
		public bool AllowFullOpen { get; set; } = true;

		/// <summary>
		/// カスタムカラー
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int[] CustomColors { get; set; } = null;

		/// <summary>
		/// ヘルプボタン
		/// </summary>
		[Category("色指定ダイアログ"), DefaultValue(false),
			Description("ダイアログ ボックスに [?] ボタンを表示する場合は true。それ以外の場合は false。")]
		public bool ShowHelp { get; set; } = false;

		/// <summary>
		/// 純色のみ選択
		/// </summary>
		[Category("色指定ダイアログ"), DefaultValue(false),
			Description("ユーザーが純色のみ選択できる場合は true。それ以外の場合は false。")]
		public bool SolidColorOnly { get; set; } = false;

        /// <summary>
        /// ダイアログ表示の開始位置
        /// </summary>
        [Category("色指定ダイアログ"), DefaultValue(typeof(FormStartPosition), "CenterScreen"),
            Description("ダイアログの表示開始位置指定。")]
        public FormStartPosition StartPosition { get; set; } = FormStartPosition.CenterScreen;

		/// <summary>
		/// ダイアログ表示位置
		/// </summary>
        [Category("色指定ダイアログ"), DefaultValue(typeof(Point),"0,0"),
			Description("StartPostionが\"Manual\"時のダイアログ表示座標。")]
        public Point WindowLocation { get; set; } = new Point(0, 0);


		/// <summary>
		/// ボタンクリック時の処理
		/// </summary>
		/// <param name="e"></param>
		protected override void OnClick(EventArgs e)
		{
			// ダイアログ表示位置指定
			FormStartPosition startPosition = StartPosition;
			// ウィンドウ表示位置
			Point location = WindowLocation;

			// 色選択ダイアログを生成
			ColorDialogWithLocation dlg = new ColorDialogWithLocation(this)
			{
				Color = BackColor,
				AnyColor = AnyColor,
				FullOpen = FullOpen,
				AllowFullOpen = AllowFullOpen,
				ShowHelp = ShowHelp,
				SolidColorOnly = SolidColorOnly,
				CustomColors = CustomColors,
				StartPosition = startPosition,
				Location = location,
			};
			// 色選択ダイアログを表示
			if (dlg.ShowDialog() == DialogResult.OK)
			{   // OKが押された
				//   色を設定
				Color = dlg.Color;
				if (dlg.CustomColors != null)
				{   // カスタムカラーを保存
					CustomColors = dlg.CustomColors;
				}
			}
			dlg.Dispose();
			// 元のクリック処理を呼び出し
			base.OnClick(e);
		}
		/// <summary>
		/// 色変更中か？
		/// </summary>
		private bool isBackColorChanging = false;

		/// <summary>
		/// 背景色
		/// </summary>
		[RefreshProperties(RefreshProperties.All)]
		public override Color BackColor
		{
			get => base.BackColor;
			set
			{
				base.BackColor = value;
				// 補色を前景色に設定
				ForeColor = InvertColor(value);
				// ツールチップの設定
				ColorToolTip.RemoveAll();
				ColorToolTip.SetToolTip(this, ToString(value));
				// テキストに設定する必要があるか？（本クラス以外からの設定か？）
				if ((isBackColorChanging == false) && (_showColorName != SHOW_COLOR_NAME.NONE))
				{   // テキストを更新
					isBackColorChanging = true;
					Text = ToColorName(value);
					isBackColorChanging = false;
				}
			}
		}
		/// <summary>
		/// 反転色を求める
		/// </summary>
		/// <param name="color">色</param>
		/// <returns>反転色</returns>
		private Color InvertColor(Color color)
		{
			if (IsGray(color))
			{   // グレーは補色や反転色でうまく求められないので...
				if (color.R + color.G + color.B <= 384)
					return Color.White;
				else
					return Color.Black;
			}
			// 補色を返す
			return ComplementaryColor(color);
		}
		/// <summary>
		/// 補色を求める
		/// </summary>
		/// <param name="color">色</param>
		/// <returns>補色</returns>
		/// <remarks>
		/// https://helpx.adobe.com/jp/illustrator/using/adjusting-colors.html
		/// 補色
		/// カラーの各構成要素を、選択したカラーの最大のRGB値と最小のRGB値の合計を元にして、新しい値に変更します。
		/// 現在のカラーのRGB値のうち最大と最小の値が合計され、その値から各構成要素の値を引いて、新しいRGB値が生成されます。
		/// 例えば、RGB値がレッド 102、グリーン 153、ブルー 51 であるカラーを選択したとします。
		/// この場合、まず最大値である 153 と最小値である 51 を合計して 204 という値が算出されます。
		/// この値から既存のカラーの RGB 値がそれぞれ差し引かれます。
		/// つまり、新しいレッドの値は 204 - 102（現在のレッドの値）= 102、
		/// グリーンの値は 204 - 153（現在のグリーンの値）= 51、
		/// ブルーの値は 204 - 51（現在のブルーの値）= 153 となり、新しい補色の RGB 値が生成されます。
		/// </remarks>
		private Color ComplementaryColor(Color color)
		{
			int max_value = ((color.R >= color.G) && (color.R >= color.B)) ? color.R :
				((color.G >= color.R) && (color.G >= color.B)) ? color.G : color.B;
			int min_value = ((color.R <= color.G) && (color.R <= color.B)) ? color.R :
				((color.G <= color.R) && (color.G <= color.B)) ? color.G : color.B;
			return Color.FromArgb(max_value + min_value - color.R,
				max_value + min_value - color.G,
				max_value + min_value - color.B);
		}
		/// <summary>
		/// グレー色かどうか
		/// </summary>
		/// <param name="color">色</param>
		/// <returns>true:グレー色</returns>
		/// <remarks>
		/// 各色の輝度値の差が±4未満の場合は、グレーとする
		/// </remarks>
		private bool IsGray(Color color)
		{
			return (Math.Abs(color.R - color.G) < 4) &&
				(Math.Abs(color.G - color.B) < 4);
		}

		/// <summary>
		/// テキストの変更
		/// </summary>
		/// <param name="e"></param>
		protected override void OnTextChanged(EventArgs e)
		{
			// このクラス内からの変更か？
			if ((isBackColorChanging == false) && (_showColorName != SHOW_COLOR_NAME.NONE))
			{   // テキストから色を求める
				Color? color = ToColor(Text);
				if (color.HasValue)
				{   // 色が有効なら設定
					isBackColorChanging = true;
					BackColor = color.Value;
					isBackColorChanging = false;
				}
			}
			// 元の処理を呼び出す
			base.OnTextChanged(e);
		}
		/// <summary>
		/// 色→色文字列変換
		/// </summary>
		/// <param name="color">色</param>
		/// <param name="alfa_check">Alfa値のチェックをするか</param>
		/// <returns>色文字列変換</returns>
		private string ToColorName(Color color, bool alfa_check = true)
		{
			if ((_showColorName.HasFlag(SHOW_COLOR_NAME.KNOWN_COLOR)) ||
				(_showColorName.HasFlag(SHOW_COLOR_NAME.SYSTEM)))
			{   // リストから検索
				foreach (Color tbl_color in _systemColors)
				{
					if ((tbl_color.R == color.R) && (tbl_color.G == color.G) &&
						(tbl_color.B == color.B) &&
						(((alfa_check) && (tbl_color.A == color.A)) || (alfa_check == false)))
					{
						if ((_showColorName.HasFlag(SHOW_COLOR_NAME.SYSTEM)) &&
							(tbl_color.IsSystemColor))
							return tbl_color.Name;
						if ((_showColorName.HasFlag(SHOW_COLOR_NAME.KNOWN_COLOR)) &&
							(tbl_color.IsSystemColor == false))
							return tbl_color.Name;
					}
				}
			}
			return string.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
		}
		/// <summary>
		/// 色文字列→色変換
		/// </summary>
		/// <param name="text">色文字列</param>
		/// <param name="alfa_check">Alfa値のチェックをするか</param>
		/// <returns>色(変換できた場合)もしくはnull</returns>
		private Color? ToColor(string text, bool alfa_check = false)
		{
			if ((text != null) && (text.Trim().Length > 0))
			{
				if ((_showColorName.HasFlag(SHOW_COLOR_NAME.KNOWN_COLOR)) ||
					(_showColorName.HasFlag(SHOW_COLOR_NAME.SYSTEM)))
				{   // リストから検索
					foreach (Color tbl_color in _systemColors)
					{
						if (tbl_color.Name.ToLower() == text.ToLower())
						{
							if ((tbl_color.IsSystemColor) && (_showColorName.HasFlag(SHOW_COLOR_NAME.SYSTEM)))
								return Color.FromArgb((alfa_check) ? tbl_color.A : 255, tbl_color);
							if ((tbl_color.IsKnownColor) && (_showColorName.HasFlag(SHOW_COLOR_NAME.KNOWN_COLOR)))
								return Color.FromArgb((alfa_check) ? tbl_color.A : 255, tbl_color);
						}
					}
				}
			}
			return null;
		}
		/// <summary>
		/// 色→色文字列変換
		/// </summary>
		/// <param name="color">色</param>
		/// <returns>色名 (#rrggbb)の形式</returns>
		public string ToString(Color color)
		{
			foreach (Color tbl_color in _systemColors)
			{
				if ((tbl_color.R == color.R) && (tbl_color.G == color.G) &&
					(tbl_color.B == color.B))
					return tbl_color.Name + string.Format(" (#{0:X2}{1:X2}{2:X2})", color.R, color.G, color.B);
			}
			return string.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
		}
		/// <summary>
		/// 文字列変換
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return ToString(Color);
		}

	}
}
