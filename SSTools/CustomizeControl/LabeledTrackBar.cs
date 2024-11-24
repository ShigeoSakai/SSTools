using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace SSTools
{
	/// <summary>
	/// カスタムトラックバー
	/// Minimum,Maximum,中央値のラベル表示と、説明ラベル表示を追加
	/// </summary>
	public class LabeledTrackBar : Control,ISupportInitialize
	{
		#region [定数]
		/// <summary>
		/// コントロール左マージン
		/// </summary>
		private const int LEFT_MARGIN = 3;
		/// <summary>
		/// コントロール右マージン
		/// </summary>
		private const int RIGHT_MARGIN = 3;
		/// <summary>
		/// コントロール上マージン
		/// </summary>
		private const int TOP_MARGIN = 3;
		/// <summary>
		/// コントロール下マージン
		/// </summary>
		private const int BOTTOM_MARGIN = 3;
		/// <summary>
		/// アイテム間のマージン
		/// </summary>
		private const int ITEM_MARGIN = 2;

		/// <summary>
		/// 目盛り線の幅
		/// </summary>
		private const int TICK_HEIGHT = 4;
		/// <summary>
		/// 目盛り線の高さ
		/// </summary>
		private const int TICK_WIDTH = 4;
		/// <summary>
		/// トラックバー幅
		/// </summary>
		private const int TRACKBAR_WIDTH = 4;
		#endregion [定数]

		/// <summary>
		/// 値変更イベント
		/// </summary>
		public event EventHandler ValueChanged;

		#region [プロパティ]
		/// <summary>
		/// 最大値
		/// </summary>
		private int m_Maximun = 100;
		/// <summary>
		/// スライダー位置の最大値
		/// </summary>
		[Category("動作"),RefreshProperties(RefreshProperties.All),
			Description("トラックバー上のスライダー位置の最大値です。")]
		public int Maximum
		{
			get { return m_Maximun; }
			set
			{
				// 現在の値を保存
				int old_value = m_Value;
				// 最大値を設定
				m_Maximun = value;
				// 値が変わらないように内部値を設定
				int conv_value = ChangeInnerValue(old_value);
				if (conv_value != old_value)
                {   // でも値が変わってしまった
					m_Value = conv_value;
					// イベント発行
					OnValueChanged(new EventArgs());
				}

				// 保存画像をクリア
				StringImageSetNull(ref maxValueStringImage);
				StringImageSetNull(ref centerValueStringImage);

				// 一括初期化待ちか？
				if (m_PendingInit == false)
				{
					// 目盛りの分割単位計算
					CalcTickUnit();
					// トラックバーの再計算
					CalculateTrackBarSize();
				}
			}
		}
		/// <summary>
		/// 最小値
		/// </summary>
		private int m_Minimum = 0;
		/// <summary>
		/// スライダー位置の最小値
		/// </summary>
		[Category("動作"),Description("トラックバー上のスライダー位置の最小値です。")]
		public int Minimum
		{
			get { return m_Minimum; }
			set
			{
				m_Minimum = value;

				// 保存画像をクリア
				StringImageSetNull(ref minValueStringImage);
				StringImageSetNull(ref centerValueStringImage);

				// 一括初期化待ちか？
				if (m_PendingInit == false)
				{
					// 目盛りの分割単位計算
					CalcTickUnit();
					// トラックバーの再計算
					CalculateTrackBarSize();
				}
			}
		}
		/// <summary>
		/// 現在値
		/// </summary>
		private int m_Value = 0;
		/// <summary>
		/// 現在値(スライダーの位置)
		/// </summary>
		/// <remarks>
		/// Inverseプロパティ=true もしくは OrientationプロパティがVerticalの場合、<br>
		/// 最大値 - 内部の値となる。<br>
		/// (Inverseプロパティ=false で OrientationプロパティがVerticalの場合は内部の値は変換しない)
		/// </remarks>
		[Category("動作"),Description("スライダーの位置です。")]
		public int Value
		{
			get 
			{	// 内部の値から変換して返す 
				return ChangeInnerValue(m_Value); 
			}
			set
			{
				// 設定値を内部の値に変換する
				int conv_value = ChangeInnerValue(value);
				if (m_Value != conv_value)
                {   // 設定値が変わった
					m_Value = conv_value;
					// イベントを発行
					OnValueChanged(new EventArgs());
				}
				// 一括初期化待ちか？
				if (m_PendingInit == false)
				{
					// トラックバーの再計算
					CalculateTrackBarSize();
				}
			}
		}

		/// <summary>
		/// マウスクリック or PAGE_UP/DOWNでの移動量
		/// </summary>
		[Category("動作"),
			Description("マウスのクリックや、PAGE UPおよびPAGE DOWNキーを押したときに、スライダーが移動するポジション数です。")]
		public int LargeChange { get; set; } = 5;
		/// <summary>
		/// カーソルキーでの移動量
		/// </summary>
		[Category("動作"),
			Description("キーボード入力(方向キー)に対して、スライダーが移動するポジション数です。")]
		public int SmallChange { get; set; } = 1;

		/// <summary>
		/// 目盛りの単位
		/// </summary>
		private int m_TickFrequency = 1;
		/// <summary>
		/// 目盛りマークの単位
		/// </summary>
		[Category("表示"),Description("目盛りマーク間の位置の数です。")]
		public int TickFrequency
		{
			get { return m_TickFrequency; }
			set
			{
				m_TickFrequency = value;
				// 一括初期化待ちか？
				if (m_PendingInit == false)
				{
					// 目盛りの分割単位計算
					CalcTickUnit();
					// トラックバーの再計算
					CalculateTrackBarSize();
				}
			}
		}
		/// <summary>
		/// 目盛り線の位置
		/// </summary>
		private TickStyle m_TickStyle = TickStyle.BottomRight;
		/// <summary>
		/// 目盛り線の表示位置
		/// </summary>
		/// <remarks>
		/// 目盛り線の表示位置により、スライダーの形状も変更する。
		/// </remarks>
		[Category("表示"),Description("目盛りをトラックバーのどこに表示するか示します。"),
			DefaultValue(typeof(TickStyle), "BottomRight")]
		public TickStyle TickStyle
		{
			get { return m_TickStyle; }
			set
			{ 
				m_TickStyle = value;
				// 一括初期化待ちか？
				if (m_PendingInit == false)
				{
					// トラックバーの再計算
					CalculateTrackBarSize();
				}
			}
		}
		/// <summary>
		/// トラックバーの方向
		/// </summary>
		private Orientation m_Orientation = Orientation.Horizontal;
		/// <summary>
		/// トラックバーの方向
		/// </summary>
		/// <remarks>
		/// Horizontal ... 左側が小さい、右側が大きい<br>
		/// Verical ... 下側が小さい、上側が大きい<br>
		/// (Inverseプロパティがtrueの場合はで反転する)
		/// </remarks>
		[Category("表示"),Description("コントロールの向きです。"),DefaultValue(typeof(Orientation), "Horizontal")]
		public Orientation Orientation
		{
			get { return m_Orientation; }
			set
			{
				m_Orientation = value;

				// 保存画像をクリア
				StringImageSetNull(ref minValueStringImage);
				StringImageSetNull(ref maxValueStringImage);
				StringImageSetNull(ref centerValueStringImage);
				StringImageSetNull(ref descriptionMinImage);
				StringImageSetNull(ref descriptionMaxImage);

				// 一括初期化待ちか？
				if (m_PendingInit == false)
				{
					// トラックバーの再計算
					CalculateTrackBarSize();
				}
			}
		}
		/// <summary>
		/// 反転(上下or左右)表示するか
		/// </summary>
		private bool m_Inverse = false;
		/// <summary>
		/// 反転(上下or左右)表示するか
		/// </summary>
		[Category("表示"), DisplayName("反転表示"),DefaultValue(false), Description(
			"反転表示するかどうか\r\n" +
			"水平表示の場合、true:左側を小さい値にする false:右側を小さい値にする\r\n" +
			"垂直表示の場合、true:下側を小さい値にする false:上側を小さい値にする"),
			RefreshProperties(RefreshProperties.All)]
		public bool Inverse
		{
			get { return m_Inverse; }
			set
			{
				if (m_Inverse != value)
				{
					// 現在の値を保存
					int old_value = m_Value;
					// 反転設定
					m_Inverse = value;
					// 値が変わらないように変換する
					int conv_value = ChangeInnerValue(old_value);
					if (conv_value != old_value)
                    {   // でも値が変わってしまった
						m_Value = conv_value;
						// イベント発行
						OnValueChanged(new EventArgs());
                    }
				}
				// 一括初期化待ちか？
				if (m_PendingInit == false)
				{
					// トラックバーの再計算
					CalculateTrackBarSize();
				}
			}
		}

		/// <summary>
		/// 数値ラベルのフォーマット
		/// </summary>
		private string m_LabelFormat;
		/// <summary>
		/// 数値ラベルのフォーマット
		/// </summary>
		/// <remarks>
		/// ラベルフォーマットを指定する。<br>
		/// ns,us,μs,ms,sが末尾の場合は、値がその単位として変換を行う<br>
		/// {SI}が含まれる場合は、K,M,Gの単位を付ける。
		/// </remarks>
		[Category("数値ラベル"), DisplayName("フォーマット"), Description(
			"ラベルフォーマットを指定する。\r\n" +
			"\"ns\",\"us\",\"μs\",\"ms\",\"s\"が末尾の場合は、値がその単位として変換を行う\r\n" +
			"\"{SI}\"が含まれる場合は、K,M,Gの単位を付ける。")]
		public string LabelFormat
		{
			get { return m_LabelFormat; }
			set
			{
				m_LabelFormat = value;
				// 保存画像をクリア
				StringImageSetNull(ref minValueStringImage);
				StringImageSetNull(ref maxValueStringImage);
				StringImageSetNull(ref centerValueStringImage);

				// 一括初期化待ちか？
				if (m_PendingInit == false)
				{
					// トラックバーの再計算
					CalculateTrackBarSize();
				}
			}
		}

		/// <summary>
		/// 数値ラベルの表示有無
		/// </summary>
		private bool m_ShowLabel = true;
		/// <summary>
		/// 数値ラベルの表示有無
		/// </summary>
		[Category("数値ラベル"), DisplayName("ラベル表示"),DefaultValue(true),Description("ラベルを表示するかどうか")]
		public bool DisplayLabel
		{
			get { return m_ShowLabel; }
			set
			{
				m_ShowLabel = value;
				// 一括初期化待ちか？
				if (m_PendingInit == false)
				{
					// トラックバーの再計算
					CalculateTrackBarSize();
				}
			}
		}
		/// <summary>
		/// 中央のラベル表示
		/// </summary>
		private bool m_ShowCenterLabel = true;
		/// <summary>
		/// 中央のラベル表示有無
		/// </summary>
		[Category("数値ラベル"), DisplayName("中央値"), DefaultValue(true), Description("中央値を表示するかどうか")]
		public bool ShowCenterLabel
		{
			get { return m_ShowCenterLabel; }
			set
			{
				m_ShowCenterLabel = value;
				// 一括初期化待ちか？
				if (m_PendingInit == false)
				{
					// トラックバーの再計算
					CalculateTrackBarSize();
				}
			}
		}

		/// <summary>
		/// 数値ラベルのフォント
		/// </summary>
		private Font m_LabelFont = new Font("MS UI Gothic", 9.0F, FontStyle.Regular);
		/// <summary>
		/// 数値ラベルのフォント
		/// </summary>
		[Category("数値ラベル"), DisplayName("フォント"), Description("ラベルのフォント指定")]
		public new Font Font
		{
			get { return m_LabelFont; }
			set
			{
				m_LabelFont = value;
				// 保存画像をクリア
				StringImageSetNull(ref minValueStringImage);
				StringImageSetNull(ref maxValueStringImage);
				StringImageSetNull(ref centerValueStringImage);

				// 一括初期化待ちか？
				if (m_PendingInit == false)
				{
					// トラックバーの再計算
					CalculateTrackBarSize();
				}
			}
		}
		/// <summary>
		/// 数値ラベルの色
		/// </summary>
		private Color m_LabelColor = Color.Black;
		/// <summary>
		/// 数値ラベルの色
		/// </summary>
		[Category("数値ラベル"), DisplayName("文字色"), Description("ラベルの文字色指定"), DefaultValue(typeof(Color), "Black")]
		public Color LabelColor
		{
			get { return m_LabelColor; }
			set
			{
				m_LabelColor = value;
				// 保存画像をクリア
				StringImageSetNull(ref minValueStringImage);
				StringImageSetNull(ref maxValueStringImage);
				StringImageSetNull(ref centerValueStringImage);

				// 一括初期化待ちか？
				if (m_PendingInit == false)
					Invalidate();
			}
		}
		/// <summary>
		/// 数値ラベルを回転
		/// </summary>
		private bool m_LabelRotate = false;
		/// <summary>
		/// 数値ラベルを回転させるかどうか
		/// </summary>
		/// <remarks>
		/// trueの場合、-90度回転した数値ラベルを表示する
		/// </remarks>
		[Category("数値ラベル"), DisplayName("90度回転表示"),DefaultValue(false),
			Description("トラックバーが垂直表示の場合、数値ラベルを90度回転させて表示させるかどうか")]
		public bool LabelRotate
		{
			get { return m_LabelRotate; }
			set
			{
				m_LabelRotate = value;
				// 保存画像をクリア
				StringImageSetNull(ref minValueStringImage);
				StringImageSetNull(ref maxValueStringImage);
				StringImageSetNull(ref centerValueStringImage);

				// 一括初期化待ちか？
				if (m_PendingInit == false)
				{
					// トラックバーの再計算
					CalculateTrackBarSize();
				}
			}
		}

		/// <summary>
		/// 説明文の表示有無
		/// </summary>
		private bool m_ShowDescription = false;
		/// <summary>
		/// 説明文の表示有無
		/// </summary>
		[Category("説明"), DisplayName("説明表示"),DefaultValue(false), Description("説明を表示するかどうか")]
		public bool ShowDescription
		{
			get { return m_ShowDescription; }
			set
			{
				m_ShowDescription = value;
				// 一括初期化待ちか？
				if (m_PendingInit == false)
				{
					// トラックバーの再計算
					CalculateTrackBarSize();
				}
			}
		}
		/// <summary>
		/// 説明文のフォント
		/// </summary>
		private Font m_DescriptionFont = new Font("MS UI Gothic", 9.0F, FontStyle.Regular);
		/// <summary>
		/// 説明文のフォント
		/// </summary>
		[Category("説明"), DisplayName("フォント"), Description("説明文字列のフォント指定")]
		public Font DescriptionFont
		{
			get { return m_DescriptionFont; }
			set
			{
				m_DescriptionFont = value;
				// 保持画像を削除
				StringImageSetNull(ref descriptionMaxImage);
				StringImageSetNull(ref descriptionMinImage);

				// 一括初期化待ちか？
				if (m_PendingInit == false)
				{
					// トラックバーの再計算
					CalculateTrackBarSize();
				}
			}
		}
		/// <summary>
		/// 説明文(最小側)
		/// </summary>
		private string m_DescriptionMin;
		/// <summary>
		/// 最小値側の説明文
		/// </summary>
		[Category("説明"), DisplayName("最小値"), Description("最小値側の説明文")]
		public string DescriptionMin
		{
			get { return m_DescriptionMin; }
			set
			{
				// 値を設定
				m_DescriptionMin = value;
				// 保持画像を削除
				StringImageSetNull(ref descriptionMinImage);

				// 一括初期化待ちか？
				if (m_PendingInit == false)
				{
					// トラックバーの再計算
					CalculateTrackBarSize();
				}
			}
		}
		/// <summary>
		/// 説明文(最大側)
		/// </summary>
		private string m_DescriptionMax;
		/// <summary>
		/// 最大値側の説明文
		/// </summary>
		[Category("説明"), DisplayName("最大値"), Description("最大値側の説明文")]
		public string DescriptionMax
		{
			get { return m_DescriptionMax; }
			set
			{
				// 値を設定
				m_DescriptionMax = value;
				// 保持画像を削除
				StringImageSetNull(ref descriptionMaxImage);

				// 一括初期化待ちか？
				if (m_PendingInit == false)
				{
					// トラックバーの再計算
					CalculateTrackBarSize();
				}
			}
		}
		/// <summary>
		/// 説明文を回転
		/// </summary>
		private bool m_DescriptionRotate = false;
		/// <summary>
		/// 説明文を回転させるかどうか
		/// </summary>
		/// <remarks>
		/// trueの場合、-90度回転した説明文を表示する
		/// </remarks>
		[Category("説明"), DisplayName("90度回転表示"), DefaultValue(false),
			Description("トラックバーが垂直表示の場合、説明文を90度回転させて表示させるかどうか")]
		public bool DescriptionRotate
		{
			get { return m_DescriptionRotate; }
			set
			{
				// 値を設定
				m_DescriptionRotate = value;
				// 保持画像を削除
				StringImageSetNull(ref descriptionMaxImage);
				StringImageSetNull(ref descriptionMinImage);

				// 一括初期化待ちか？
				if (m_PendingInit == false)
				{
					// トラックバーの再計算
					CalculateTrackBarSize();
				}
			}
		}

		/// <summary>
		/// 説明文の色
		/// </summary>
		private Color m_DescriptionColor = Color.Black;
		/// <summary>
		/// 説明文の色
		/// </summary>
		[Category("説明"), DisplayName("文字色"), Description("説明文の文字色指定"), DefaultValue(typeof(Color), "Black")]
		public Color DescriptionColor
		{
			get { return m_DescriptionColor; }
			set
			{
				m_DescriptionColor = value;
				// 保持画像を削除
				StringImageSetNull(ref descriptionMaxImage);
				StringImageSetNull(ref descriptionMinImage);

				// 一括初期化待ちか？
				if (m_PendingInit == false)
					Invalidate();
			}
		}
		/// <summary>
		/// マウス長押しタイマー値
		/// </summary>
		/// <remarks>
		/// マウスの長押し判定をする時間(ms)
		/// </remarks>
		[Category("動作"),Description("マウスボタン長押し判定の時間(ms)"),DefaultValue(250)]
		public int LongPress { get; set; } = 250;

		/// <summary>
		/// 自動サイズ設定
		/// </summary>
		private bool m_AutoSize = false;
		/// <summary>
		/// 自動サイズ調整
		/// </summary>
		/// <remarks>
		/// trueの場合、設定された内容でサイズの自動調整を行う。
		/// デフォルトは「false」。
		/// </remarks>
		[Category("配置"),DefaultValue(false),Description("サイズの自動調整を行います。"),Browsable(true),
			DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),EditorBrowsable(EditorBrowsableState.Always),
			RefreshProperties(RefreshProperties.All)]

		public override bool AutoSize
        {
			get { return m_AutoSize; }
			set
            {
				if (m_AutoSize != value)
				{
					m_AutoSize = value;
					// 一括初期化待ちか？
					if ((m_AutoSize) && (m_PendingInit == false))
					{
						// トラックバーの再計算
						CalculateTrackBarSize();
					}
					// イベント発行
					OnAutoSizeChanged(new EventArgs());
				}
			}
		}

		/// <summary>
		/// デバッグ表示
		/// </summary>
		private bool m_ShowDebug = false;
		/// <summary>
		/// デバッグ表示をするかどうか
		/// </summary>
		/// <remarks>
		/// trueの場合、コントロールの領域を赤枠で表示する。
		/// </remarks>
		[DefaultValue(false)]
		public bool ShowDebug
		{
			get { return m_ShowDebug; }
			set
			{
				m_ShowDebug = value;
				Invalidate();
			}
		}

		#endregion [プロパティ]

		/// <summary>
		/// バー部分の領域
		/// </summary>
		private Rectangle trackRectangle = new Rectangle();
		/// <summary>
		/// メモリ部分の領域（上・左）
		/// </summary>
		private Rectangle ticksRectangleUpLeft = new Rectangle();
		/// <summary>
		/// メモリ部分の領域（下・、右）
		/// </summary>
		private Rectangle ticksRectangleBottomRight = new Rectangle();

		/// <summary>
		/// 最小値の説明表示領域
		/// </summary>
		private Rectangle descriptionMin = new Rectangle();
		/// <summary>
		/// 最小値の説明表示描画イメージ
		/// </summary>
		private Bitmap descriptionMinImage = null;

		/// <summary>
		/// 最大値の説明表示領域
		/// </summary>
		private Rectangle descriptionMax = new Rectangle();
		/// <summary>
		/// 最大値の説明表示描画イメージ
		/// </summary>
		private Bitmap descriptionMaxImage = null;

		/// <summary>
		/// 最小値のラベル領域
		/// </summary>
		private Rectangle minValueString = new Rectangle();
		/// <summary>
		/// 最小値のラベル表示描画イメージ
		/// </summary>
		private Bitmap minValueStringImage = null;

		/// <summary>
		/// 中央値のラベル領域
		/// </summary>
		private Rectangle centerValueString = new Rectangle();
		/// <summary>
		/// 中央値のラベル表示描画イメージ
		/// </summary>
		private Bitmap centerValueStringImage = null;

		/// <summary>
		/// 最大値のラベル領域
		/// </summary>
		private Rectangle maxValueString = new Rectangle();
		/// <summary>
		/// 最大値のラベル表示描画イメージ
		/// </summary>
		private Bitmap maxValueStringImage = null;

		/// <summary>
		/// 文字列画像用ロックオブジェクト
		/// </summary>
		private readonly object StringImageLockObj = new object();

		/// <summary>
		/// スライダー部分の領域
		/// </summary>
		private readonly ThumbClass SliderBar = new ThumbClass();
		/// <summary>
		/// スライダークリック中か？
		/// </summary>
		private bool thumbClicked = false;
		/// <summary>
		/// スライダーの状態
		/// </summary>
		private TrackBarThumbState thumbState = TrackBarThumbState.Normal;
		/// <summary>
		/// 目盛りの分割数
		/// </summary>
		private int m_TickUnit = 1;
		/// <summary>
		/// 初期化終了待ちか？
		/// </summary>
		private bool m_PendingInit = false;
		/// <summary>
		/// 目盛りの分割数の算出
		/// </summary>
		/// <remarks>
		/// 目盛り分割数の算出を行う。<br>
		/// (注)ここでは実際の目盛り描画のサイズが不明のため、単純に求めるのみとする。<br>
		/// 　　描画時に分割数の補正を行う。
		/// </remarks>
		private void CalcTickUnit()
		{
			m_TickUnit = (m_TickFrequency == 0) ? 2 : (m_Maximun - m_Minimum + m_TickFrequency) / m_TickFrequency;
		}
		/// <summary>
		/// 内部制御値との変換
		/// </summary>
		/// <param name="value">値</param>
		/// <returns>変換後の値</returns>
		/// <remarks>
		/// トラックバーの方向と、反転あり・なしで、内部の値と、外部の値の相互変換を行う。<br>
		/// ・方向が水平で、反転あり ... 反転(最大値-値)した値を返す<br>
		/// ・方向は垂直で、反転なし ... 反転(最大値-値)した値を返す<br>
		/// 上記以外は、そのままの値を返す
		/// </remarks>
		private int ChangeInnerValue(int value)
		{
			// 方向が水平で、反転あり ... 反転
			// 方向は垂直で、反転なし ... 反転
			if (m_Inverse == (m_Orientation == Orientation.Horizontal))
				return (m_Maximun < value) ? 0 : m_Maximun - value; // 反転
			return value;
		}
		/// <summary>
		/// 文字列画像のクリア(ロック付き)
		/// </summary>
		/// <param name="image">文字列画像</param>
		/// <remarks>
		/// 文字列画像がnull以外の場合、排他して文字列画像をクリアする。<br>
		/// 排他...OnPaint描画中に中身がnullにならないように...<br>
		/// </remarks>
		private void StringImageSetNull(ref Bitmap image)
        {
			if (image != null)
			{	// 何か入っている
				lock (StringImageLockObj)
				{ 
					try
					{   // ビットマップを解放
						image.Dispose();
					}
					finally
					{   // nullに設定
						image = null;
					}
				}
            }
        }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public LabeledTrackBar()
		{
			// ダブルバッファを有効にする(描画で点滅するので)
			DoubleBuffered = true;
			// 長押しタイマー初期化
			LongPressTimerInit();
		}
		/// <summary>
		/// 初期化開始
		/// </summary>
		/// <remarks>
		/// プロパティの値変更等で、座標値等の計算を行わないようにする。<br>
		/// EndInit()呼び出しで、一括再計算させる。
		/// </remarks>
		public void BeginInit()
		{
			// 設定都度、計算をさせない
			m_PendingInit = true;
		}
		/// <summary>
		/// 初期化終了
		/// </summary>
		/// <remarks>
		/// トラックバーの分割数、各座標値を計算する。
		/// </remarks>
		public void EndInit()
		{
			if (m_PendingInit)
            {   // 一気に再計算する
				// 目盛りの分割単位計算
				CalcTickUnit();
				// トラックバーの再計算
				CalculateTrackBarSize();

				m_PendingInit = false;
            }
		}
		/// <summary>
		/// リソースの解放
		/// </summary>
		/// <param name="disposing">true:リソース解放中</param>
		/// <remarks>
		/// 文字列画像を解放する。
		/// </remarks>
        protected override void Dispose(bool disposing)
        {
			if (disposing)
			{
				// 文字列描画イメージを解放する
				try
				{
					if (minValueStringImage != null)
					{
						minValueStringImage.Dispose();
						minValueStringImage = null;
					}
					if (maxValueStringImage != null)
					{
						maxValueStringImage.Dispose();
						maxValueStringImage = null;
					}
					if (centerValueStringImage != null)
					{
						centerValueStringImage.Dispose();
						centerValueStringImage = null;
					}
					if (descriptionMaxImage != null)
					{
						descriptionMaxImage.Dispose();
						descriptionMaxImage = null;
					}
					if (descriptionMinImage != null)
					{
						descriptionMinImage.Dispose();
						descriptionMinImage = null;
					}
				}
				catch (Exception) { }
			}

			base.Dispose(disposing);
        }

        /// <summary>
        /// 値の範囲を設定
        /// </summary>
        /// <param name="minValue">最小値</param>
        /// <param name="maxValue">最大値</param>
		/// <remarks>
		/// トラックバーの最小値と最大値を設定する。
		/// </remarks>
        public void SetRange(int minValue, int maxValue)
        {
			m_Minimum = minValue;
			m_Maximun = maxValue;
			CalcTickUnit();
			// トラックバーの再計算
			CalculateTrackBarSize();
		}

        #region [イベント]
        /// <summary>
        /// クライアントサイズ変更
        /// </summary>
        /// <param name="e">イベント引数</param>
		/// <remarks>
		/// コントロール描画範囲の変更に伴う各座標値の再計算を行う。
		/// </remarks>
        protected override void OnClientSizeChanged(EventArgs e)
		{
			base.OnClientSizeChanged(e);
			if (m_PendingInit == false)
			{
				// トラックバーの再計算
				CalculateTrackBarSize();
			}
		}
		/// <summary>
		/// フォーカスが来た
		/// </summary>
		/// <param name="e">イベント引数</param>
		/// <remarks>
		/// フォーカスを取得した場合、描画処理(OnPaint)に点線の枠を描画するように指示する。
		/// </remarks>
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			// 枠線を描画させる
			Invalidate();
		}
		/// <summary>
		/// フォーカスが離れた
		/// </summary>
		/// <param name="e">イベント引数</param>
		/// <remarks>
		/// フォーカスが離れた場合、描画処理(OnPaint)に点線の枠を描画しないよう指示する。
		/// </remarks>
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			// 枠線を描画さない
			Invalidate();
		}
		#endregion [イベント]

		/// <summary>
		/// 全てのサイズで一番大きい幅と高さを求める
		/// </summary>
		/// <param name="result">結果</param>
		/// <param name="items">サイズ</param>
		/// <returns>結果</returns>
		/// <remarks>
		/// 複数のSize構造体から一番大きい幅と高さを求める。
		/// </remarks>
		private Size MergeSize(ref Size result,params Size[] items)
        {
			foreach(Size sz in items)
            {
				if (result.Width < sz.Width)
					result.Width = sz.Width;
				if (result.Height < sz.Height)
					result.Height = sz.Height;
            }
			return result;
        }
		#region [描画座標計算]

		/// <summary>
		/// 縦横サイズを入れ替える
		/// </summary>
		/// <param name="size">サイズ</param>
		/// <remarks>
		/// 縦と横の値を入れ替える。
		/// </remarks>
		private void SwapSize(ref Size size) =>(size.Height, size.Width) = (size.Width, size.Height);

        /// <summary>
        /// トラックバーの再計算
        /// </summary>
        /// <remarks>
        /// 説明ラベル、最小・最大・中央値ラベル、目盛り線、トラックバーおよびスライダーの
        /// 描画位置を算出する。<br>
        /// 計算後に、コントロールの全エリアを無効(OnPaintによる再描画対象)にする。<br>
        /// Orientationにより、計算方法を変える。
        /// </remarks>
        private void CalculateTrackBarSize()
		{
			// トラックバーの描画が使えるか？
			if (!TrackBarRenderer.IsSupported)
				return;
			// 描画グラフィックを取得
			using (Graphics g = CreateGraphics())
			{
				// 数値ラベルの有無
				Size labelSize = new Size(0, 0);
				if (m_ShowLabel)
				{   // 最小値文字列の描画サイズ
					Size minLabelSize = TextRenderer.MeasureText(g, GetString(m_Minimum), m_LabelFont, new Size(1000, 1000), TextFormatFlags.NoPadding);
					// 最大値文字列の描画サイズ
					Size maxLabelSize = TextRenderer.MeasureText(g, GetString(m_Maximun), m_LabelFont, new Size(1000, 1000), TextFormatFlags.NoPadding);
					// 中央値文字列の描画サイズ
					Size centerLabelSize = TextRenderer.MeasureText(g, GetString((int)((m_Maximun - m_Minimum) / 2.0 + 0.5)),
						 m_LabelFont, new Size(1000, 1000), TextFormatFlags.NoPadding);

					if (m_LabelRotate)
                    {   // 90度回転なら縦横を入れ替える
						SwapSize(ref minLabelSize);
						SwapSize(ref maxLabelSize);
						SwapSize(ref centerLabelSize);
					}

					// 大きさを保存
					minValueString.Size = minLabelSize;
					maxValueString.Size = maxLabelSize;
					centerValueString.Size = centerLabelSize;
					// 一番大きい幅と高さを計算上のサイズにする
					MergeSize(ref labelSize, minLabelSize, maxLabelSize, centerLabelSize);
				} 
				// 詳細表示の有無
				Size descriptionSize = new Size(0, 0);
				if (m_ShowDescription)
				{   // 文字列の描画サイズを求める
					Size descriptionMinSize = TextRenderer.MeasureText(g, DescriptionMin ?? "■",
						m_DescriptionFont, new Size(1000, 1000), TextFormatFlags.NoPadding);
					Size descriptionMaxSize = TextRenderer.MeasureText(g, DescriptionMax ?? "■",
						m_DescriptionFont, new Size(1000, 1000), TextFormatFlags.NoPadding);

					if (m_DescriptionRotate)
					{   // 90度回転なら縦横を入れ替える
						SwapSize(ref descriptionMinSize);
						SwapSize(ref descriptionMaxSize);
					}

					// 大きさを保存
					descriptionMin.Size = descriptionMinSize;
					descriptionMax.Size = descriptionMaxSize;
					// 一番大きい幅と高さを計算上のサイズにする
					MergeSize(ref descriptionSize, descriptionMinSize, descriptionMaxSize);
				}
				// スライドバーのサイズを設定
				SliderBar.SetSize(g, m_Orientation, m_TickStyle);

				if (m_Orientation == Orientation.Horizontal)
				{	
					// 水平方向 .... 上から順番に配置する
					int posY = ClientRectangle.Y + TOP_MARGIN + Padding.Top;
					// 説明文
					if (m_ShowDescription)
					{
						if (m_Inverse == false)
						{   // 左が小さい値
							//  最小値ラベル
							descriptionMin.Location = new Point(ClientRectangle.Left + LEFT_MARGIN + Padding.Left, posY);
							//  最大値ラベル
							descriptionMax.Location = new Point(
								ClientRectangle.Right - descriptionMax.Width - LEFT_MARGIN - RIGHT_MARGIN - Padding.Horizontal, posY);
						}
						else
						{   // 右側が小さい値
							//  最小値ラベル
							descriptionMin.Location = new Point(
								ClientRectangle.Right - descriptionMin.Width - LEFT_MARGIN - RIGHT_MARGIN - Padding.Horizontal, posY);
							//  最大値ラベル
							descriptionMax.Location = new Point(ClientRectangle.Left + LEFT_MARGIN + Padding.Left, posY);
						}
						// 垂直位置を更新
						posY += descriptionSize.Height;
					}

					// 目盛線 上 TopLeft=1,Both=3 なので
					if (((int)m_TickStyle & 0x01) != 0)
					{
						// マージンを加算
						posY += ITEM_MARGIN;
						// 目盛り線上
						ticksRectangleUpLeft.X = ClientRectangle.X + LEFT_MARGIN + Padding.Left + SliderBar.Size.Width / 2;
						ticksRectangleUpLeft.Y = posY;
						ticksRectangleUpLeft.Width = ClientRectangle.Width - SliderBar.Size.Width - LEFT_MARGIN - RIGHT_MARGIN - Padding.Horizontal;
						ticksRectangleUpLeft.Height = TICK_HEIGHT;
						// 垂直位置を更新
						posY += TICK_HEIGHT;
					}
					// マージンを加算
					posY += ITEM_MARGIN;

					// スライダーの設定
					SliderBar.SetArea( 
						new Point(ClientRectangle.X + LEFT_MARGIN + Padding.Left, posY), m_Minimum, m_Maximun,
						ClientRectangle.Width - LEFT_MARGIN - RIGHT_MARGIN - Padding.Horizontal);
					// スライダー位置
					SliderBar.Value = m_Value;

					// トラックバー ... スライドバーの真ん中に描画
					trackRectangle.X = ClientRectangle.X + LEFT_MARGIN + Padding.Left + SliderBar.Size.Width / 2;
					trackRectangle.Y = posY + (SliderBar.Size.Height - TRACKBAR_WIDTH) / 2;
					trackRectangle.Width = ClientRectangle.Width - SliderBar.Size.Width - LEFT_MARGIN - RIGHT_MARGIN - Padding.Horizontal;
					trackRectangle.Height = TRACKBAR_WIDTH;

					// 垂直位置を更新
					posY += SliderBar.Size.Height;

					// 目盛線 下 BottomRight=2,Both=3なので
					if (((int)m_TickStyle & 0x02) != 0)
					{
						// マージンを加算
						posY += ITEM_MARGIN;
						// 目盛り線下
						ticksRectangleBottomRight.X = ClientRectangle.X + LEFT_MARGIN + Padding.Left + SliderBar.Size.Width / 2;
						ticksRectangleBottomRight.Y = posY;
						ticksRectangleBottomRight.Width = ClientRectangle.Width - SliderBar.Size.Width - LEFT_MARGIN - RIGHT_MARGIN - Padding.Horizontal;
						ticksRectangleBottomRight.Height = TICK_HEIGHT;

						// 垂直位置を更新
						posY += TICK_HEIGHT;
					}
					// 数値ラベル
					if (m_ShowLabel)
					{
						// マージンを加算
						posY += ITEM_MARGIN;

						if (m_Inverse == false)
						{   // 左が小さい値
							minValueString.Location = new Point(ClientRectangle.X + LEFT_MARGIN + Padding.Left, posY);
							maxValueString.Location = new Point(
								ClientRectangle.Right - maxValueString.Width - LEFT_MARGIN - RIGHT_MARGIN - Padding.Horizontal, posY);
						}
						else
						{   // 右が小さい値
							minValueString.Location = new Point(
								ClientRectangle.Right - minValueString.Width - LEFT_MARGIN - RIGHT_MARGIN - Padding.Horizontal, posY);
							maxValueString.Location = new Point(ClientRectangle.X + LEFT_MARGIN + Padding.Left, posY);
						}
						// 中央値の表示
						if (m_ShowCenterLabel)
						{
							centerValueString.Location = new Point(
								ClientRectangle.X + LEFT_MARGIN + Padding.Left + (ClientRectangle.Width - centerValueString.Width - LEFT_MARGIN - RIGHT_MARGIN - Padding.Horizontal) / 2,
								posY);
						}
						// 垂直位置を更新
						posY += labelSize.Height;
					}

					// 垂直位置を更新
					posY += BOTTOM_MARGIN + Padding.Bottom;
					// 自動サイズ調整かどうか
					if (m_AutoSize)
					{
						Size = new Size(Size.Width, posY - ClientRectangle.Y);
						// イベント発行
						OnSizeChanged(new EventArgs());
					}
				}
				else
                {   // 垂直方向 ... 左から順に配置する
					int posX = ClientRectangle.X + LEFT_MARGIN + Padding.Left;
					// 説明文
					if (m_ShowDescription)
                    {
						if (m_Inverse == false)
                        {   // 下が小さい値
							// 最小値ラベル
							descriptionMin.Location = new Point(
								posX + descriptionSize.Width - descriptionMin.Width, 
								ClientRectangle.Bottom - descriptionMin.Height - TOP_MARGIN - BOTTOM_MARGIN - Padding.Vertical);
							// 最大値ラベル
							descriptionMax.Location = new Point(
								posX + descriptionSize.Width - descriptionMax.Width,
								ClientRectangle.Top + TOP_MARGIN + Padding.Top);
						}
						else
                        {   // 上が小さい値
							// 最小値ラベル
							descriptionMin.Location = new Point(
								posX + descriptionSize.Width - descriptionMin.Width,
								ClientRectangle.Top + TOP_MARGIN + Padding.Top);
							// 最大値ラベル
							descriptionMax.Location = new Point(
								posX + descriptionSize.Width - descriptionMax.Width,
								ClientRectangle.Bottom - descriptionMax.Height - TOP_MARGIN - BOTTOM_MARGIN - Padding.Vertical);
						}
						// 水平位置を更新
						posX += descriptionSize.Width;
					}
					// 目盛線 左 TopLeft=1,Both=3 なので
					if (((int)m_TickStyle & 0x01) != 0)
					{
						// マージンを加算
						posX += ITEM_MARGIN;
						// 目盛り線左
						ticksRectangleUpLeft.X = posX;
						ticksRectangleUpLeft.Y = ClientRectangle.Y + TOP_MARGIN + Padding.Top + SliderBar.Size.Height / 2;
						ticksRectangleUpLeft.Width = TICK_WIDTH;
						ticksRectangleUpLeft.Height = ClientRectangle.Height - SliderBar.Size.Height - TOP_MARGIN - BOTTOM_MARGIN - Padding.Vertical;
						// 水平位置を更新
						posX += TICK_WIDTH;
					}
					// マージンを加算
					posX += ITEM_MARGIN;

					// スライダーの設定
					SliderBar.SetArea(
						new Point(posX , ClientRectangle.Y + TOP_MARGIN + Padding.Top ), m_Minimum, m_Maximun,
						ClientRectangle.Height - TOP_MARGIN - BOTTOM_MARGIN - Padding.Vertical);
					// スライダー位置
					SliderBar.Value = m_Value;

					// トラックバー ... スライドバーの真ん中に描画
					trackRectangle.X = posX + (SliderBar.Size.Width - TRACKBAR_WIDTH) / 2; 
					trackRectangle.Y = ClientRectangle.Y + TOP_MARGIN + Padding.Top + SliderBar.Size.Height / 2;
					trackRectangle.Width = TRACKBAR_WIDTH; 
					trackRectangle.Height = ClientRectangle.Height - SliderBar.Size.Height - TOP_MARGIN - BOTTOM_MARGIN - Padding.Vertical;

					// 水平位置を更新
					posX += SliderBar.Size.Width;
					
					// 目盛線 右 BottomRight=2,Both=3なので
					if (((int)m_TickStyle & 0x02) != 0)
					{
						// マージンを加算
						posX += ITEM_MARGIN;
						// 目盛り線右
						ticksRectangleBottomRight.X = posX;
						ticksRectangleBottomRight.Y = ClientRectangle.Y + TOP_MARGIN + Padding.Top + SliderBar.Size.Height / 2;
						ticksRectangleBottomRight.Width = TICK_HEIGHT; 
						ticksRectangleBottomRight.Height = ClientRectangle.Height - SliderBar.Size.Height - TOP_MARGIN - BOTTOM_MARGIN - Padding.Vertical;

						// 水平位置を更新
						posX += TICK_HEIGHT;
					}
					// 数値ラベル
					if (m_ShowLabel)
					{
						// マージンを加算
						posX += ITEM_MARGIN;

						if (m_Inverse == false)
						{   // 下が小さい値
							minValueString.Location = new Point(
								posX + labelSize.Width - minValueString.Width,
								ClientRectangle.Bottom - minValueString.Height - TOP_MARGIN - BOTTOM_MARGIN - Padding.Vertical);
							maxValueString.Location = new Point(
								posX + labelSize.Width - maxValueString.Width,
								ClientRectangle.Top + TOP_MARGIN + Padding.Top);
						}
						else
						{   // 上が小さい値
							minValueString.Location = new Point(
								posX + labelSize.Width - minValueString.Width,
								ClientRectangle.Top + TOP_MARGIN + Padding.Top);
							maxValueString.Location = new Point(
								posX + labelSize.Width - maxValueString.Width,
								ClientRectangle.Bottom - maxValueString.Height - TOP_MARGIN - BOTTOM_MARGIN -Padding.Vertical);
						}
						// 中央値の表示
						if (m_ShowCenterLabel)
						{
							centerValueString.Location = new Point(
								posX + labelSize.Width - centerValueString.Width,
								ClientRectangle.Y + TOP_MARGIN + Padding.Top + (ClientRectangle.Height - centerValueString.Height - TOP_MARGIN - BOTTOM_MARGIN - Padding.Vertical) / 2);
						}
						// 水平位置を更新
						posX += labelSize.Width;
					}
					// 水平位置を更新
					posX += RIGHT_MARGIN + Padding.Right;
					// 自動サイズ調整かどうか
					if (m_AutoSize)
					{
						Size = new Size(posX - ClientRectangle.X, Size.Height);
						// イベント発行
						OnSizeChanged(new EventArgs());
					}
				}
			}
			Invalidate();
		}
		/// <summary>
		/// パディングが変更になった
		/// </summary>
		/// <param name="e"></param>
        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);
			// トラックバーの再計算
			CalculateTrackBarSize();
		}


        #endregion [描画座標計算]

        #region [スライダー管理クラス]
        /// <summary>
        /// スライダー管理クラス
        /// </summary>
        private class ThumbClass
		{
			/// <summary>
			/// スライダーサイズ
			/// </summary>
			public Size Size { get; private set; }

			/// <summary>
			/// 中心座標
			/// </summary>
			private Point m_Center = new Point();
			public Point Center
			{
				get { return m_Center; }
				set
				{
					m_Center = value;
					// 中心から左上座標を求める
					CalcTopLeft();
					// 値を算出
					CalcValue();
				}
			}
			/// <summary>
			/// 左上座標
			/// </summary>
			private Point m_TopLeft = new Point();
			/// <summary>
			/// 左上座標
			/// </summary>
			public Point TopLeft { get { return m_TopLeft; } }
			/// <summary>
			/// 開始座標
			/// </summary>
			private Point m_Start = new Point();
			/// <summary>
			/// 開始座標
			/// </summary>
			public Point Start { get { return m_Start; } }
			/// <summary>
			/// 最小値
			/// </summary>
			private int m_MinValue = 0;
			/// <summary>
			/// 最大値
			/// </summary>
			private int m_MaxValue = 0;
			/// <summary>
			/// 可動幅(Pixel)
			/// </summary>
			private int m_Width = 0;
			/// <summary>
			/// 現在の値
			/// </summary>
			private int m_Value = 0;
			/// <summary>
			/// 現在の値
			/// </summary>
			public int Value { 
				get { return m_Value; }
				set
                {
					m_Value = value;
					// 座標計算
					CalcPoint();
                }
            }
			/// <summary>
			/// スライダーの領域
			/// </summary>
			public Rectangle Rectangle
			{
				get { return new Rectangle(TopLeft, Size); }
			}
			/// <summary>
			/// コントロール全体(トラックバーも含めた)
			/// </summary>
			private Rectangle m_AllRectangle = new Rectangle();
			/// <summary>
			/// コントロール全体(トラックバーも含めた)
			/// </summary>
			public Rectangle AllRectangle
            {
				get
                {
					return m_AllRectangle;
				}
            }

			/// <summary>
			/// 1単位あたりのPixel数
			/// </summary>
			private float m_Ratio;
			/// <summary>
			/// トラックバーの向き
			/// </summary>
			private Orientation m_Orientation;

			/// <summary>
			/// 設定済みか？
			/// </summary>
			private bool isSet = false;
			/// <summary>
			/// コンストラクタ
			/// </summary>
			public ThumbClass()
			{
			}
			/// <summary>
			/// 大きさの算出
			/// </summary>
			/// <param name="g">グラフィック</param>
			/// <param name="orientation">トラックバーの方向</param>
			/// <param name="tickStyle">スライダーの形</param>
			/// <remarks>
			/// スライダーの描画サイズを求める
			/// </remarks>
			public void SetSize(Graphics g, Orientation orientation, TickStyle tickStyle)
            {
				// 向きを保存
				m_Orientation = orientation;

				// スライダーのサイズを求める
				if (m_Orientation == Orientation.Horizontal)
					switch (tickStyle)
					{
						case TickStyle.TopLeft:
							Size = TrackBarRenderer.GetTopPointingThumbSize(g, TrackBarThumbState.Normal);
							break;
						case TickStyle.BottomRight:
							Size = TrackBarRenderer.GetBottomPointingThumbSize(g, TrackBarThumbState.Normal);
							break;
						case TickStyle.Both:
						default:
							Size top = TrackBarRenderer.GetTopPointingThumbSize(g, TrackBarThumbState.Normal);
							Size bottom = TrackBarRenderer.GetBottomPointingThumbSize(g, TrackBarThumbState.Normal);
							Size = new Size(
								(top.Width > bottom.Width) ? top.Width : bottom.Width,
								(top.Height > bottom.Height) ? top.Height : bottom.Height);
							break;
					}
				else
					switch (tickStyle)
					{
						case TickStyle.TopLeft:
							Size = TrackBarRenderer.GetLeftPointingThumbSize(g, TrackBarThumbState.Normal);
							break;
						case TickStyle.BottomRight:
							Size = TrackBarRenderer.GetRightPointingThumbSize(g, TrackBarThumbState.Normal);
							break;
						case TickStyle.Both:
						default:
							Size left = TrackBarRenderer.GetLeftPointingThumbSize(g, TrackBarThumbState.Normal);
							Size right = TrackBarRenderer.GetRightPointingThumbSize(g, TrackBarThumbState.Normal);
							Size = new Size(
								(left.Width > right.Width) ? left.Width : right.Width,
								(left.Height > right.Height) ? left.Height : right.Height);
							break;
					}
			}
			/// <summary>
			/// パラメータの設定
			/// </summary>
			/// <param name="start">開始座標</param>
			/// <param name="minValue">最小値</param>
			/// <param name="maxValue">最大値</param>
			/// <param name="width">トラックバー幅</param>
			/// <remarks>
			/// スライダーの動作可能範囲、1単位あたりのPixel数を計算する。
			/// </remarks>
			public void SetArea(Point start,int minValue,int maxValue,int width)
			{
				// コントロール全体を保存
				m_AllRectangle.Location = start;
				if (m_Orientation == Orientation.Horizontal)
                {
					m_AllRectangle.Width = width;
					m_AllRectangle.Height = Size.Height;
				}
				else
                {
					m_AllRectangle.Width = Size.Width;
					m_AllRectangle.Height = width;
                }

				// 開始座標(コントロールの左上座標なので、中心座標にする)
				m_Start.X = start.X + Size.Width / 2;
				m_Start.Y = start.Y + Size.Height / 2;
				// 最小値
				m_MinValue = minValue;
				// 最大値
				m_MaxValue = maxValue;
				// 移動幅(コントロール全体の幅なので、大きさ分削る)
				m_Width = width - ((m_Orientation == Orientation.Horizontal)? Size.Width : Size.Height);
				// 1単位あたりのPixel数を計算
				m_Ratio = (float)m_Width / (m_MaxValue - m_MinValue);
				// 最小値で中心座標を算出 = 開始点
				m_Center.X = m_Start.X;
				m_Center.Y = m_Start.Y;

				// 設定済みにする
				isSet = true;

				// 左上座標を算出
				CalcTopLeft();
			}
			/// <summary>
			/// スライダー中心座標から左上座標を求める
			/// </summary>
			private void CalcTopLeft()
            {
				m_TopLeft.X = m_Center.X - Size.Width / 2;
				m_TopLeft.Y = m_Center.Y - Size.Height / 2;
            }
			/// <summary>
			/// 値から座標を計算
			/// </summary>
			/// <remarks>
			/// トラックバーの値から、開始点座標、1単位あたりのPixel数を使って<br>
			/// スライダー座標値を算出する。
			/// </remarks>
			private void CalcPoint()
            {
				if (isSet)
				{
					// 最大値、最小値の丸め
					if (m_Value < m_MinValue)
						m_Value = m_MinValue;
					else if (m_Value > m_MaxValue)
						m_Value = m_MaxValue;
					// Pixcel数に変換
					int value = (int)Math.Round((m_Value - m_MinValue) * m_Ratio);

					if (m_Orientation == Orientation.Horizontal)
					{   // 水平バー
						m_Center.X = m_Start.X + value;
					}
					else
					{   // 垂直バー
						m_Center.Y = m_Start.Y + value;
					}
					// 左上座標を算出
					CalcTopLeft();
				}
			}
			/// <summary>
			/// 中心座標から値を計算
			/// </summary>
			/// <remarks>
			/// 指定された座標から、開始点座標、1単位あたりのPixel数を使って<br>
			/// トラックバーの値を算出する。
			/// </remarks>
			private void CalcValue()
            {
				if (isSet)
				{
					// 値を設定（現在の中心の値）
					m_Value = GetValue(m_Center);
				}
            }
			/// <summary>
			/// 指定座標がスライダーの領域内か？
			/// </summary>
			/// <param name="point">座標</param>
			/// <returns>true:座標はスライダーの領域内</returns>
			public bool Contains(Point point)
			{
				if (isSet)
					return ((TopLeft.X <= point.X) && (point.X <= TopLeft.X + Size.Width) &&
						(TopLeft.Y <= point.Y) && (point.Y <= TopLeft.Y + Size.Height));
				return false;
			}
			/// <summary>
			/// 大きく移動の範囲か？
			/// </summary>
			/// <param name="point">座標</param>
			/// <param name="direction">方向</param>
			/// <returns>true;指定座標はトラックバー上</returns>
			/// <remarks>
			/// 現在のスライダー位置から指定座標が＋方向 ... direction = 1<br>
			/// 現在のスライダー位置から指定座標が－方向 ... direction = -1<br>
			/// </remarks>
			public bool ContainsLarge(Point point ,out int direction)
            {
				if (isSet)
                {
					if (m_Orientation == Orientation.Horizontal)
					{
						if ((m_Start.X - Size.Width / 2 <= point.X) && (point.X <= m_Start.X + m_Width + Size.Width / 2) &&
							(m_Start.Y - Size.Height / 2 <= point.Y) && (point.Y <= m_Start.Y + Size.Height / 2))
						{
							if (point.X < m_Center.X)
								direction = -1;     // 前へ
							else
								direction = 1;		// 後ろへ
							return true;
						}
					}
					else
					{
						if ((m_Start.X - Size.Width / 2 <= point.X) && (point.X <= m_Start.X + Size.Width / 2) &&
							(m_Start.Y - Size.Height / 2 <= point.Y) && (point.Y <= m_Start.Y + m_Width + Size.Height / 2))
						{
							if (point.Y < m_Center.Y)
								direction = -1;      // 前へ
							else
								direction = 1;      // 後ろへ
							return true;
						}
					}

				}
				direction = 0;
				return false;
            }
			/// <summary>
			/// 指定座標の値を算出
			/// </summary>
			/// <param name="point">座標</param>
			/// <returns>値</returns>
			/// <remarks>
			/// 指定された座標から、開始点座標、1単位あたりのPixel数を使って<br>
			/// トラックバーの値を算出する。
			/// </remarks>
			public int GetValue(Point point)
            {
				if (isSet)
				{
					// 開始点からのPixel数
					int pixel;
					if (m_Orientation == Orientation.Horizontal)
						pixel = point.X - m_Start.X;
					else
						pixel = point.Y - m_Start.Y;
					// 値を算出
					int value = (int)Math.Round((float)pixel / m_Ratio) + m_MinValue;
					// 最大値、最小値に丸め
					if (value < m_MinValue)
						value = m_MinValue;
					else if (value > m_MaxValue)
						value = m_MaxValue;

					return value;
				}
				return 0;
			}
		}
		#endregion [スライダー管理クラス]

		#region [OnPaint]
		/// <summary>
		/// 文字列画像が生成されていなかったら生成する
		/// </summary>
		/// <param name="image">文字列画像</param>
		/// <param name="text">描画する文字列</param>
		/// <param name="isRotate">回転するか</param>
		/// <param name="size">描画サイズ</param>
		/// <param name="font">描画フォント</param>
		/// <param name="color">描画色</param>
		/// <remarks>
		/// 文字列画像が生成されていなかった(=null)ら、生成(isRotateがtureの場合は、-90度回転。falseの場合は回転なし)する。<br>
		/// 文字列画像が生成されいる(!=null)の場合は何もしない。
		/// </remarks>
		private void MakeStringImage(ref Bitmap image,string text,bool isRotate, Size size,ref Font font,Color color)
        {
			// 文字列画像を生成するか？
			if (image == null)
			{
				// サイズは回転後の値
				image = new Bitmap(size.Width, size.Height);
				Graphics g = Graphics.FromImage(image);
				if (isRotate)
				{
					// 回転させる設定
					g.RotateTransform(-90.0F);
					// 平行移動の設定
					g.TranslateTransform(0, size.Height, MatrixOrder.Append);
				}
				// 文字列描画
				g.DrawString(text, font, new SolidBrush(color), new Point(0, 0),StringFormat.GenericTypographic);
				// リソースを解放
				g.Dispose();
			}
		}
		/// <summary>
		/// ある領域が別の領域に含まれる(重なる部分がある)かチェック
		/// </summary>
		/// <param name="baseRect">領域1</param>
		/// <param name="rect">領域2</param>
		/// <returns>true:重なり合う部分がある</returns>
		/// <remarks>
		/// 矩形領域1と矩形領域2が重なる部分があるかチェックする。
		/// </remarks>
		private bool PartContains(Rectangle baseRect,Rectangle rect)
        {
			// rectの頂点がbaseRectに含まれていたらtrue
			if ((baseRect.Contains(rect.Left, rect.Top)) ||
				(baseRect.Contains(rect.Left, rect.Bottom)) ||
				(baseRect.Contains(rect.Right, rect.Top)) ||
				(baseRect.Contains(rect.Right, rect.Bottom)))
				return true;
			// baseRectの頂点がrectに含まれていたらtrue
			if ((rect.Contains(baseRect.Left, baseRect.Top)) ||
				(rect.Contains(baseRect.Left, baseRect.Bottom)) ||
				(rect.Contains(baseRect.Right, baseRect.Top)) ||
				(rect.Contains(baseRect.Right, baseRect.Bottom)))
				return true;
			return false;
		}

		/// <summary>
		/// OnPaint ... トラックバーの描画処理
		/// </summary>
		/// <param name="e">描画イベントパラメータ</param>
		/// <remarks>
		/// コントロール全体・一部の描画処理を行う。<br>
		/// ・フォーカスがある場合は、点線でコントロールを囲む四角形を描画する。(条件なし)<br>
		/// ・説明文字列の描画(指定された領域に説明文字列領域が含まれる場合のみ)<br>
		/// ・最大値・最小値・中央値のラベルの描画(指定された領域に各文字列領域が含まれる場合のみ)<br>
		/// ・目盛り線の描画(指定された領域に目盛り線領域が含まれる場合のみ)<br>
		/// ・トラックバーとスライダーの描画(指定された領域に各領域が含まれる場合のみ)<br>
		/// 各描画座標値は、あらかじめ算出されているはず(calculateTrackBarSize()で)。<br>
		/// 文字列は画像として保存されていない場合は、文字列画像を生成して描画。保存されている場合は、その画像を描画する。<br>
		/// 目盛り線の分割数は、分割数 >= 目盛り線の幅/2 の場合、目盛り線の幅/2として描画する。<br>
		/// （分割数>目盛り線の幅の場合、TrackBarRenderer.DrawHorizontalTicks()が無限ループ状態になるため... ＋<br>
		/// 　目盛り線自体が2pixelあるので、余計な描画をさせないようにするため）
		/// </remarks>
		protected override void OnPaint(PaintEventArgs e)
		{
			if (!TrackBarRenderer.IsSupported)
			{
				return;
			}
			// Graphicsを取得
			Graphics g = e.Graphics;

			if (Focused)
			{   // フォーカスがある
				Pen dashPen = new Pen(Color.Black) { DashStyle = DashStyle.Dot };
				g.DrawRectangle(dashPen, e.ClipRectangle.Location.X,e.ClipRectangle.Location.Y,e.ClipRectangle.Width-1,e.ClipRectangle.Height-1);
				dashPen.Dispose();
			}

			// 説明文字列
			if (m_ShowDescription)
			{   // 最小値側
				if (DescriptionMin != null)
				{
					// 描画対象か？
					if ((PartContains(e.ClipRectangle, descriptionMin)) || (descriptionMinImage == null))
					{
						lock (StringImageLockObj)
						{
							// 文字列が生成されていなかったら生成する
							MakeStringImage(ref descriptionMinImage, DescriptionMin, m_DescriptionRotate,
							descriptionMin.Size, ref m_DescriptionFont, m_DescriptionColor);
							// 文字列を描画
							g.DrawImage(descriptionMinImage, descriptionMin.Location);
						}
					}
				}
				// 最大値側
				if (DescriptionMax != null)
				{
					// 描画対象か？
					if ((PartContains(e.ClipRectangle, descriptionMax)) || (descriptionMaxImage == null))
					{
						lock (StringImageLockObj)
						{
							// 文字列が生成されていなかったら生成する
							MakeStringImage(ref descriptionMaxImage, DescriptionMax, m_DescriptionRotate,
							descriptionMax.Size, ref m_DescriptionFont, m_DescriptionColor);
							// 文字列を描画
							g.DrawImage(descriptionMaxImage, descriptionMax.Location);
						}
					}
				}
			}
			// 最大値,最小値
			if (m_ShowLabel)
			{
				string min_str = GetString(m_Minimum);
				string max_str = GetString(m_Maximun);
				string ave_str = GetString((int)((m_Maximun - m_Minimum) / 2.0 + 0.5));

				// 最小値
				if ((PartContains(e.ClipRectangle, minValueString)) || (minValueStringImage == null))
                {
					lock (StringImageLockObj)
                    {
						// 文字列が生成されていなかったら生成する
						MakeStringImage(ref minValueStringImage, min_str, m_LabelRotate,
						minValueString.Size, ref m_LabelFont, m_LabelColor);
						// 文字列を描画
						g.DrawImage(minValueStringImage, minValueString.Location);
					}
				}

				// 最大値
				if ((PartContains(e.ClipRectangle, maxValueString)) || (maxValueStringImage == null))
				{
					lock (StringImageLockObj)
					{
						// 文字列が生成されていなかったら生成する
						MakeStringImage(ref maxValueStringImage, max_str, m_LabelRotate,
							maxValueString.Size, ref m_LabelFont, m_LabelColor);
						// 文字列を描画
						g.DrawImage(maxValueStringImage, maxValueString.Location);
					}
				}
				// 中央値
				if (m_ShowCenterLabel)
				{
					if ((PartContains(e.ClipRectangle, centerValueString)) || (centerValueStringImage == null))
					{
						lock (StringImageLockObj)
						{
							// 文字列が生成されていなかったら生成する
							MakeStringImage(ref centerValueStringImage, ave_str, m_LabelRotate,
									centerValueString.Size, ref m_LabelFont, m_LabelColor);
							// 文字列を描画
							g.DrawImage(centerValueStringImage, centerValueString.Location);
						}
					}
				}
			}
			// メモリ線 上
			if ((((int)m_TickStyle & 0x01) != 0) && (PartContains(e.ClipRectangle, ticksRectangleUpLeft)))
			{
				// m_TickUnitが描画幅の半分より大きい場合は、TickUnitを描画幅にする
				if (m_Orientation == Orientation.Horizontal)
					TrackBarRenderer.DrawHorizontalTicks(g, ticksRectangleUpLeft,
						(m_TickUnit > (ticksRectangleUpLeft.Width/2)) ? (ticksRectangleUpLeft.Width/2) : m_TickUnit,
						EdgeStyle.Raised);
				else
					TrackBarRenderer.DrawVerticalTicks(g, ticksRectangleUpLeft,
						(m_TickUnit > (ticksRectangleUpLeft.Height/2)) ? (ticksRectangleUpLeft.Height/2) : m_TickUnit,
						EdgeStyle.Raised);
			}
			// メモリ線下
			if ((((int)m_TickStyle & 0x02) != 0) && (PartContains(e.ClipRectangle, ticksRectangleBottomRight)))
			{
				// m_TickUnitが描画幅の半分より大きい場合は、TickUnitを描画幅にする
				if (m_Orientation == Orientation.Horizontal)
					TrackBarRenderer.DrawHorizontalTicks(g, ticksRectangleBottomRight,
						(m_TickUnit > (ticksRectangleBottomRight.Width/2)) ? (ticksRectangleBottomRight.Width/2) : m_TickUnit,
						EdgeStyle.Raised);
				else
					TrackBarRenderer.DrawVerticalTicks(g, ticksRectangleBottomRight,
						(m_TickUnit > (ticksRectangleBottomRight.Height/2)) ? (ticksRectangleBottomRight.Height/2) : m_TickUnit,
						EdgeStyle.Raised);
			}

			if (PartContains(e.ClipRectangle, SliderBar.AllRectangle))
			{

				// トラックバーの部分
				if (m_Orientation == Orientation.Horizontal)
					TrackBarRenderer.DrawHorizontalTrack(g, trackRectangle);
				else
					TrackBarRenderer.DrawVerticalTrack(g, trackRectangle);

				// スライダー部分
				if (m_Orientation == Orientation.Horizontal)
				{   // 水平方向
					//  メモリ位置によって書き分ける
					switch (m_TickStyle)
					{
						case TickStyle.TopLeft:
							TrackBarRenderer.DrawTopPointingThumb(g, SliderBar.Rectangle, thumbState);
							break;
						case TickStyle.BottomRight:
							TrackBarRenderer.DrawBottomPointingThumb(g, SliderBar.Rectangle, thumbState);
							break;
						case TickStyle.Both:
						default:
							TrackBarRenderer.DrawHorizontalThumb(g, SliderBar.Rectangle, thumbState);
							break;
					}
				}
				else
				{   // 垂直位置
					//  メモリ位置によって書き分ける
					switch (m_TickStyle)
					{
						case TickStyle.TopLeft:
							TrackBarRenderer.DrawLeftPointingThumb(g, SliderBar.Rectangle, thumbState);
							break;
						case TickStyle.BottomRight:
							TrackBarRenderer.DrawRightPointingThumb(g, SliderBar.Rectangle, thumbState);
							break;
						case TickStyle.Both:
						default:
							TrackBarRenderer.DrawVerticalThumb(g, SliderBar.Rectangle, thumbState);
							break;
					}
				}
			}
			// for Debug
			if (ShowDebug)
			{
				g.DrawRectangles(new Pen(Color.Red), new Rectangle[]
				{
					minValueString, maxValueString, centerValueString, descriptionMin, descriptionMax,
					ticksRectangleUpLeft, ticksRectangleBottomRight, trackRectangle,SliderBar.Rectangle
				});
				Console.WriteLine(" minValueString:{0}\r\n centerValueString:{1}\r\n maxValueString:{2}", 
					minValueString, centerValueString, maxValueString);
				Console.WriteLine(" descriptionMin:{0}\r\n descriptionMax:{1}", descriptionMin, descriptionMax);
				Console.WriteLine(" ticksRectangleUpLeft:{0}\r\n ticksRectangleBottomRight:{1}", ticksRectangleUpLeft, ticksRectangleBottomRight);
				Console.WriteLine(" trackRectangle:{0}\r\n SliderBar:{1}", trackRectangle, SliderBar.Rectangle);
			}
		}
#endregion [OnPaint]


		#region [マウス制御]
		/// <summary>
		/// マウス座標とスライダー中心とのオフセット
		/// </summary>
		private Point moveOffset = new Point();

		/// <summary>
		/// 長押しタイマー
		/// </summary>
		private readonly Timer m_LongPressTimer = new Timer();

		/// <summary>
		/// 長押しタイマーの初期化
		/// </summary>
		private void LongPressTimerInit()
        {
			// イベント登録
			m_LongPressTimer.Tick += LongPressTimer_Tick;
		}
		/// <summary>
		/// 長押しタイマー開始
		/// </summary>
		private void LongPressTimerStart()
        {
			// タイマー停止
			LongPressTimerStop();
			// インターバルを設定し、タイマー開始
			m_LongPressTimer.Interval = LongPress;
			m_LongPressTimer.Start();
        }
		/// <summary>
		/// 長押しタイマー停止
		/// </summary>
		private void LongPressTimerStop()
        {
			if (m_LongPressTimer.Enabled)
			{   // 動いていたら止める
				m_LongPressTimer.Stop();
			}
		}

		/// <summary>
		/// マウスボタン押された時の処理
		/// </summary>
		/// <param name="e">マウスイベントデータ</param>
		/// <remarks>
		/// マウスボタンの押された座標が、スライダー領域内の場合は、
		/// スライダー中心とのオフセットを算出し、スライダークリック状態にする。<br>
		/// また、マウスボタンの押された座標が、トラックバー上での場合は、
		/// 大きく移動(LargeChange)の処理を行う。
		/// </remarks>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (!TrackBarRenderer.IsSupported)
				return;

			if (SliderBar.Contains(e.Location))
			{
				thumbClicked = true;
				thumbState = TrackBarThumbState.Pressed;
				// スライダー中心とのオフセットを保存
				moveOffset.X = SliderBar.Center.X - e.X;
				moveOffset.Y = SliderBar.Center.Y - e.Y;

				// スライドバーのみ再描画
				Invalidate(SliderBar.AllRectangle);
			}
			else if (SliderBar.ContainsLarge(e.Location,out int direction))
            {	// トラックバーの範囲
				Point pos;
				if (m_Orientation == Orientation.Horizontal)
					pos = new Point(e.Location.X, SliderBar.Center.Y);
				else
					pos = new Point(SliderBar.Center.X, e.Location.Y);
				// この位置での値
				int value = SliderBar.GetValue(pos);
				// 現在値との差がLargeChangeより大きいか？
				if (Math.Abs(value - m_Value) >= LargeChange)
                {	// 大きく移動
					ValueChangeFromKey(direction, true);
					// 長押しタイマー開始
					LongPressTimerStart();
				} 
				else if (value != m_Value)
				{
                    // スライダー位置更新
                    SliderBar.Value = value;

					m_Value = value;
                    // 描画
                    Invalidate(SliderBar.AllRectangle);
					// イベント発行
					OnValueChanged(new EventArgs());
				}
			}
		}
		/// <summary>
		/// マウスボタン長押し判定
		/// </summary>
		/// <param name="sender">送信元</param>
		/// <param name="e">イベント引数</param>
		/// <remarks>
		/// 長押しタイマーTick処理<br>
		/// トラックバーの範囲内であれば、現在値と差があればLargeChange分移動
		/// それ以外は、タイマーを静止する。
		/// </remarks>
		private void LongPressTimer_Tick(object sender, EventArgs e)
		{
			// マウス座標
			Point m_pos = PointToClient(MousePosition);

			// トラックバーの範囲か？
			if (SliderBar.ContainsLarge(m_pos, out int direction))
			{
				Point pos;
				if (m_Orientation == Orientation.Horizontal)
					pos = new Point(m_pos.X, SliderBar.Center.Y);
				else
					pos = new Point(SliderBar.Center.X, m_pos.Y);
				// この位置での値
				int value = SliderBar.GetValue(pos);
				if (Math.Abs(value - m_Value) >= LargeChange)
				{   // 大きく移動
					ValueChangeFromKey(direction, true);
					// タイマー継続
					return;
				}
				else if (value != m_Value)
				{
                    // スライダー位置更新
                    SliderBar.Value = value;

					m_Value = value;
                    // 描画
                    Invalidate(SliderBar.AllRectangle);
					// イベント発行
					OnValueChanged(new EventArgs());
				}
			}
			// タイマーを停止
			LongPressTimerStop();
		}
		/// <summary>
		/// マウスボタンが離れた時の処理
		/// </summary>
		/// <param name="e">マウスイベントデータ</param>
		/// <remarks>
		/// スライダークリック状態の場合は、現在のマウス座標値から、
		/// トラックバーの値を計算し、値の更新をする。
		/// </remarks>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (!TrackBarRenderer.IsSupported)
				return;

			if (thumbClicked == true)
			{
				// スライダーの中心座標を算出
				Point thumbCenter = new Point(e.X + moveOffset.X, e.Y + moveOffset.Y);
				// 中心座標での値を算出
				int value = SliderBar.GetValue(thumbCenter);
				if (value != m_Value)
				{
					m_Value = value;
					// イベント発行
					OnValueChanged(new EventArgs());
				}
				thumbState = TrackBarThumbState.Hot;
				thumbClicked = false;
				// スライドバーのみ再描画
				Invalidate(SliderBar.AllRectangle);
			}
			else
            {   // 長押しタイマーが動作していたら停止
				LongPressTimerStop();
            }
		}
		/// <summary>
		/// 値変更イベント発行
		/// </summary>
		/// <param name="e">イベントデータ</param>
		/// <remarks>値更新イベントを発行し、要求元に値の変更を通知する。</remarks>
		protected virtual void OnValueChanged(EventArgs e)
		{
			// イベント発行
			ValueChanged?.Invoke(this, new EventArgs());
		}

		/// <summary>
		/// マウス移動中処理
		/// </summary>
		/// <param name="e">マウスイベントデータ</param>
		/// <remarks>
		/// スライダークリック状態の場合は、マウス座標から値を算出し、更新されている場合は、
		/// 値変更のイベントを発行する。<br>
		/// また、トラックバー上を通過している場合は、スライドバーの表示状態を変更する。
		/// </remarks>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (!TrackBarRenderer.IsSupported)
				return;

			// ドラッグ中か？
			if (thumbClicked == true)
			{
				// スライダーの中心座標を算出
				Point thumbCenter = new Point(e.X + moveOffset.X, e.Y + moveOffset.Y);
				// 中心座標での値を算出
				int value = SliderBar.GetValue(thumbCenter);
				if (value != m_Value)
				{
					// スライダー位置更新
					SliderBar.Value = value;
					// 結果を保存
					m_Value = value;
					// イベント発行
					OnValueChanged(new EventArgs());
				}
			}
			else
			{	// 長押し判定中でタイマーが動作していたら止める
				LongPressTimerStop();
				// マウスがトラックバーの上を通過中か？
				thumbState = SliderBar.Contains(e.Location) ?
					TrackBarThumbState.Hot : TrackBarThumbState.Normal;
			}
			// スライドバーのみ再描画
			Invalidate(SliderBar.AllRectangle);
		}
		#endregion [マウス制御]


		#region [キー動作]
		/// <summary>
		/// カーソルキーをキャッチする
		/// </summary>
		/// <param name="e">キーイベントデータ</param>
		/// <remarks>
		/// カーソルキーの押下を補足するため、<br>
		/// ・垂直バーの場合<br>
		/// 　　上下カーソルキーを動作を無効=OnKeyDown()イベントで処理<br>
		/// ・水平バーの場合<br>
		/// 　　左右カーソルキーを動作を無効=OnKeyDown()イベントで処理<br>
		/// </remarks>
		protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
			switch(e.KeyCode)
            {
				case Keys.Up:
				case Keys.Down:
					// 垂直バーならカーソルキーを無効にする
					if (m_Orientation == Orientation.Vertical)
						e.IsInputKey = true;
					break;
				case Keys.Left:
				case Keys.Right:
					// 水平バーならカーソルキーを無効にする
					if (m_Orientation == Orientation.Horizontal)
						e.IsInputKey = true;
					break;
            }
			base.OnPreviewKeyDown(e);
		}
		/// <summary>
		/// キーボード処理
		/// </summary>
		/// <param name="e">キーイベントデータ</param>
		/// <remarks>
		/// カーソルキーの場合、小さく移動(SmallChange)<br>
		/// PageUp/Downキーの場合は、大きく移動(LargeChage)させる。
		/// </remarks>
		protected override void OnKeyDown(KeyEventArgs e)
        {
			switch(e.KeyCode)
            {
				case Keys.Up:
				case Keys.Left:
					// 小さく前へ
					ValueChangeFromKey(-1, false);
					e.Handled = true;
					break;
				case Keys.Down:
				case Keys.Right:
					// 小さく後ろへ
					ValueChangeFromKey(1, false);
					e.Handled = true;
					break;
				case Keys.PageUp:
					// 大きく後ろへ
					if (m_Orientation == Orientation.Horizontal)
						ValueChangeFromKey(1, true);
					else
						ValueChangeFromKey(-1, true);
					e.Handled = true;
					break;
				case Keys.PageDown:
					// 大きく前へ
					if (m_Orientation == Orientation.Horizontal)
						ValueChangeFromKey(-1, true);
					else
						ValueChangeFromKey(1, true);
					e.Handled = true;
					break;

			}
			base.OnKeyDown(e);
		}
		/// <summary>
		/// キー操作による移動
		/// </summary>
		/// <param name="num">移動カウント</param>
		/// <param name="isLarge">true:大きく/false:小さく</param>
		/// <remarks>
		/// num > 0 の場合は、値を増加させる。<br>
		/// num < 0 の場合は、値を減少させる。<br>
		/// isLargeがtrueの場合、増減量は、num * LargeChange<br>
		/// isLargeがfalseの場合、増減量は、num * SmallChange<br>
		/// 値が変更になった場合は、値変更イベントを発行する。
		/// </remarks>
		private void ValueChangeFromKey(int num,bool isLarge)
        {
			int stepValue = SmallChange;
			if (isLarge)
				stepValue = LargeChange;
			int value = m_Value + num * stepValue;
			if (value < m_Minimum)
				value = m_Minimum;
			if (value > m_Maximun)
				value = m_Maximun;
			if (value != m_Value)
            {
				// スライダー位置更新
				SliderBar.Value = value;
				// 値を更新
				m_Value = value;
				// イベント発行
				OnValueChanged(new EventArgs());
				// 描画
				Invalidate(SliderBar.AllRectangle);
			}
        }
		#endregion [キー動作]


		#region [フォーマット変換]
		/// <summary>
		/// 特殊なフォーマットで文字列変換
		/// </summary>
		/// <param name="value">値</param>
		/// <returns>フォーマット変換後の文字列</returns>
		/// <remarks>
		///  LabelFormatの末尾が、"秒","s"の場合は、指定SI単位の値として表示する<br>
		///  　単位例)ms,us,μs,ns,m秒...<br>
		///  　下位2桁が0の場合、1つ上のSI単位で表示する<br>
		///  　　例) 値:1000 フォーマット:ms → 1s  値:2500 フォーマット:us → 2.5ms<br>
		///  LabelFormatに"{SI}"が含まれる場合は、SI単位を付けて表示する<br>
		///  　例) 値:1000 フォーマット:{SI} → 1K 値:2500 フォーマット:{SI}m → 2.5Km<br> 
		/// </remarks>
		private string GetString(int value)
		{
			if ((m_LabelFormat != null) && (m_LabelFormat.Trim().Length > 0))
			{
				try
				{
					string format = m_LabelFormat.Trim();
					// 時刻指定かどうか
					if ((format.ToLower().EndsWith("s")) || (format.EndsWith("秒")))
					{   // 時刻単位指定
						// 単位テーブル
						string[] table = new string[] { "n", "μ", "m", "" };
						string lastUnit = format.Last().ToString();
						format = format.Substring(0, format.Length - 1);
						string unit_format = "#,0";
						int start_index = 3;
						if (format.Length > 0)
						{
							switch (format.ToLower().Last())
							{
								case 'n':
									start_index = 0;
									format = format.Substring(0, format.Length - 1);
									break;
								case 'u':
								case 'μ':
									start_index = 1;
									format = format.Substring(0, format.Length - 1);
									break;
								case 'm':
									start_index = 2;
									format = format.Substring(0, format.Length - 1);
									break;
							}
						}
						if (format.Length > 0)
							unit_format = format;
						// 小数点位置のチェックと小数点ありフォーマットの作成
						string decimal_point_format = unit_format;
						if (decimal_point_format.IndexOf('.') < 0)
							decimal_point_format += ".0";

						while ((value >= 1000) && (start_index < table.Length - 1))
						{
							if (value % 100 != 0)
								return value.ToString(unit_format) + table[start_index] + lastUnit;
							if (value % 1000 != 0)
								return ((double)(value / 1000.0)).ToString(decimal_point_format) + table[start_index + 1] + lastUnit;
							value /= 1000;
							start_index++;
						}
						// 1つ上の単位が使えるか？
						if ((value % 100 == 0) && (start_index < table.Length - 1))
							return ((double)(value / 1000.0)).ToString(decimal_point_format) + table[start_index + 1] + lastUnit;
						// 終わったところで表示
						return value.ToString(unit_format) + table[start_index] + lastUnit;
					}
					// SI単位
					if (format.ToUpper().IndexOf("{SI}") >= 0)
					{   // SI単位指定
						int pos = format.ToUpper().IndexOf("{SI}");
						string before_si = ((pos > 0) ? format.Substring(0, pos) : "#,0");
						string after_si = ((pos + 4 < format.Length) ? format.Substring(pos + 4) : "");
						// 小数点位置のチェックと小数点ありフォーマットの作成
						string decimal_point_format = before_si;
						if (decimal_point_format.IndexOf('.') < 0)
							decimal_point_format += ".0";
						// SI単位テーブル
						string[] table = new string[] { "", "K", "M", "G" };
						int unit_index = 0;
						while ((value >= 1000) && (unit_index < table.Length - 1))
						{
							if (value % 100 != 0)
								return value.ToString(before_si) + table[unit_index] + after_si;
							if (value % 1000 != 0)
								return ((double)(value / 1000.0)).ToString(decimal_point_format) + table[unit_index + 1] + after_si;
							value /= 1000;
							unit_index++;
						}
						// 1つ上の単位が使えるか？
						if ((value % 100 == 0) && (unit_index < table.Length - 1))
							return ((double)(value / 1000.0)).ToString(decimal_point_format) + table[unit_index + 1] + after_si;
						// 終わったところで表示
						return value.ToString(before_si) + table[unit_index] + after_si;
					}
					// 上記以外はそのまま変換してみる
					return value.ToString(format);
				}
				catch (Exception) { }
			}
			// 空もしくは変換エラーならデフォルト
			return value.ToString("#,0");
		}

		#endregion [フォーマット変換]

    }
}
