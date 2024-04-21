using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTools
{
	public partial class WndMsgAnalysis
	{
		/// <summary>
		/// デフォルトクラス(WParam/LParam保持のみ)
		/// </summary>
		public class WM_DEFAULT : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
			public WM_DEFAULT(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
			public override string ToString() => string.Format("WParam:0x{0:X8} LParam:0x{1:X8}", Param.W.ValueU, Param.L.ValueU);
		}
		/// <summary>
		/// WPARAM XY
		/// </summary>
		public class WPARAM_XY : WPARAM_LPARAM_IS_VALUE<XY_PARAMS>
		{
			int X { get => Param.X; }
			int Y { get => Param.Y; }
			public WPARAM_XY(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
			public override string ToString() => string.Format("X:{0} Y:{1}", X, Y);
		}

		/// <summary>
		/// WPARAM SIZE
		/// </summary>
		public class WPARAM_SIZE : WPARAM_LPARAM_IS_VALUE<SIZE_PARAMS>
		{
			int Width { get => Param.Width; }
			int Height { get => Param.Height; }
			public WPARAM_SIZE(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
			public override string ToString() => string.Format("Width:{0} Height:{1}", Width, Height);
		}
		/// <summary>
		/// UI_STATE用クラス
		/// </summary>
		public class WM_UISTATE : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
			public enum WM_UISTATE_ACTION
			{
				UIS_CLEAR = 2,      // 上位ワードで指定された UI 状態フラグをクリアする必要があります。
				UIS_INITIALIZE = 3, // 上位ワードで指定された UI 状態フラグは、最後の入力イベントに基づいて変更する必要があります。
				UIS_SET = 1,        // 上位ワードで指定された UI 状態フラグを設定する必要があります。
			}
			[Flags]
			public enum WM_UISTATE_STYLE
			{
				UISF_ACTIVE = 0x4,      // コントロールは、アクティブなコントロールに使用されるスタイルで描画する必要があります。
				UISF_HIDEACCEL = 0x2,   // キーボード アクセラレータ。
				UISF_HIDEFOCUS = 0x01,  // フォーカス インジケーター。
			}
			public WM_UISTATE_ACTION Action { get => (WM_UISTATE_ACTION)Param.W.Lo; }
			public WM_UISTATE_STYLE Style { get => (WM_UISTATE_STYLE)Param.W.Hi; }
			public WM_UISTATE(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
			public override string ToString() =>
				string.Format("Action:{0} Style:{1} 0x{2:X8}", Action, Style, Param.W.ValueU);
		}
		/// <summary>
		/// GetICON用クラス
		/// </summary>
		public class WM_GETICON : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
			public enum ICON_KIND
			{   
				ICON_BIG = 1,       // ウィンドウの大きなアイコンを取得します。
				ICON_SMALL = 0,     // ウィンドウの小さいアイコンを取得します。
				ICON_SMALL2 = 2,    // アプリケーションによって提供される小さなアイコンを取得します。
									// アプリケーションで指定されていない場合、システムはそのウィンドウにシステムによって生成されたアイコンを使用します。
			}
			public ICON_KIND Kind { get => (ICON_KIND)Param.W.Lo; }
			public WM_GETICON(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
			public override string ToString() => string.Format("Kind:{0} 0x{1:X8}", Kind, Param.W.ValueU);
		}
		/// <summary>
		/// WM_ACTIVE用クラス
		/// </summary>
		public class WM_ACTIVE : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
			public enum ACTIVE_STATE
			{
				WA_ACTIVE = 1,		// マウスのクリック以外の何らかの方法 (たとえば、SetActiveWindow関数の呼び出し、
									// またはキーボードインターフェイスを使用してウィンドウを選択すること) によってアクティブ化されます。
				WA_CLICKACTIVE = 2, // マウスをクリックするとアクティブになります。
				WA_INACTIVE = 0,    // 無効化されました。
			}
			public ACTIVE_STATE State { get => (ACTIVE_STATE)Param.W.Lo; }
			public WM_ACTIVE(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
			public override string ToString() =>
				string.Format("State:{0} wParamHi:{1} WinHandle:0x{2:X8}", State, Param.W.Hi, Param.L.ValueU);
		}
		/// <summary>
		/// WM_COMMAND用クラス
		/// </summary>
		public class WM_COMMAND : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
			public enum SOURCE
			{   
				MENU = 0,           // メニュー
				ACCELERATOR = 1,    // Accelerator
				CONTROL = 2,        // コントロール
			}
			public SOURCE Source { get => (Param.W.Hi <= 1) ? (SOURCE)Param.W.Hi : SOURCE.CONTROL; }
			public WM_COMMAND(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
			public override string ToString()
			{
				if (Source == SOURCE.CONTROL)
					return string.Format("Source:{0} Code:0x{1:X4} ID:0x{2:X4} Handle:0x{3:X8}", Source, Param.W.Hi, Param.W.Lo, Param.L.ValueU);
				return string.Format("Source:{0} ID:0x{1:X4} Handle:0x{2:X8}", Source, Param.W.Lo, Param.L.ValueU);
			}
		}
		/// <summary>
		/// WM_SHOWWINDOW用クラス
		/// </summary>
		public class WM_SHOWWINDOW : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
			public enum WINDOW_STATE
			{
				SW_OTHERUNZOOM = 4,     // 最大化ウィンドウが復元または最小化されたため、ウィンドウが見つかりませんでした。
				SW_OTHERZOOM = 2,       // ウィンドウは、最大化された別のウィンドウで覆われています。
				SW_PARENTCLOSING = 1,   // ウィンドウの所有者ウィンドウが最小化されています。
				SW_PARENTOPENING = 3,   // ウィンドウの所有者ウィンドウが復元されています。
			}
			public WINDOW_STATE State { get => (WINDOW_STATE)Param.L.ValuePtr; }
			public WM_SHOWWINDOW(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
			public override string ToString() =>
				string.Format("{0} State:{1}", (Param.W.ValuePtr == (IntPtr)0) ? "Inactive" : "Active", State);
		}
		/// <summary>
		/// WM_SYSCOMMAND用クラス
		/// </summary>
		public class WM_SYSCOMMAND : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
			public enum CMD
			{
				SC_CLOSE = 0xF060,              // ウィンドウを閉じます。
				SC_CONTEXTHELP = 0xF180,        // ポインターを使用して、カーソルを疑問符に変更します。 ユーザーがダイアログ ボックスでコントロールをクリックすると、コントロールは WM_HELP メッセージを受け取ります。
				SC_DEFAULT = 0xF160,            // 既定の項目を選択します。ユーザーがウィンドウ メニューをダブルクリックしました。
				SC_HOTKEY = 0xF150,             // アプリケーション指定のホット キーに関連付けられているウィンドウをアクティブにします。 lParam パラメーターは、アクティブにするウィンドウを識別します。
				SC_HSCROLL = 0xF080,            // 水平方向にスクロールします。
				SCF_ISSECURE = 0x00000001,      // スクリーン セーバーが安全かどうかを示します。
				SC_KEYMENU = 0xF100,            // キーストロークの結果としてウィンドウ メニューを取得します。 詳細については、「解説」を参照してください。
				SC_MAXIMIZE = 0xF030,           // ウィンドウを最大化します。
				SC_MINIMIZE = 0xF020,           // ウィンドウを最小化します。
				SC_MONITORPOWER = 0xF170,       // 表示の状態を設定します。 このコマンドは、バッテリ駆動のパーソナル コンピューターなど、省電力機能を備えたデバイスをサポートします。
												//  lParam パラメーターには、次の値を指定できます。
												//   -1 (ディスプレイの電源が入ります)
												//    1 (ディスプレイは低電力になります)
												//    2 (ディスプレイがシャットダウン中)
				SC_MOUSEMENU = 0xF090,          // マウス クリックの結果としてウィンドウ メニューを取得します。
				SC_MOVE = 0xF010,               // ウィンドウを移動します。
				SC_NEXTWINDOW = 0xF040,         // 次のウィンドウに移動します。
				SC_PREVWINDOW = 0xF050,         // 前のウィンドウに移動します。
				SC_RESTORE = 0xF120,            // ウィンドウを通常の位置とサイズに戻します。
				SC_SCREENSAVE = 0xF140,         // System.ini ファイルの[boot] セクションで指定されたスクリーン セーバー アプリケーションを実行します。
				SC_SIZE = 0xF000,               // ウィンドウのサイズを設定します。
				SC_TASKLIST = 0xF130,           // [スタート] メニューをアクティブにします。
				SC_VSCROLL = 0xF070,            // 垂直方向にスクロールします。
			}
			public CMD Cmd { get => (CMD)Param.W.ValueU; }
			public int X { get => Param.L.Lo; }
			public int Y { get => Param.L.Hi; }
			public WM_SYSCOMMAND(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
			public override string ToString() =>
				string.Format("CMD:{0} X:{1} Y:{2}", Cmd, X, Y);
		}
		/// <summary>
		/// マウス用共通クラス
		/// </summary>
		public class WM_MOUSE : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
			[Flags]
			public enum STATE
			{
				MK_CONTROL = 0x0008,        // CTRL キーが押されています。
				MK_LBUTTON = 0x0001,        // マウスの左ボタンが押されています。
				MK_MBUTTON = 0x0010,        // マウスの中央ボタンが押されています。
				MK_RBUTTON = 0x0002,        // マウスの右ボタンが押されています。
				MK_SHIFT = 0x0004,          // Shift キーが押されています。
				MK_XBUTTON1 = 0x0020,       // 最初の X ボタンが押されています。
				MK_XBUTTON2 = 0x0040,       // 2 つ目の X ボタンが押されています。
			}
			public STATE State { get => (STATE)Param.W.Lo; }
			public int X { get => Param.L.Lo; }
			public int Y { get => Param.L.Hi; }
			public WM_MOUSE(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
			public override string ToString() =>string.Format("State:{0} X:{1} Y:{2}", State, X, Y);
		}
		/// <summary>
		/// WM_MOUSE_WHEEL用クラス
		/// </summary>
		public class WM_MOUSE_WHEEL : WM_MOUSE
		{
			public int Wheel { get => Param.W.HiI; }
			public WM_MOUSE_WHEEL(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
			public override string ToString() =>string.Format("State:{0} X:{1} Y:{2} Wheel:{2}", State, X, Y, Wheel);
		}
		/// <summary>
		/// Xボタン用クラス
		/// </summary>
		public class WM_X_MOUSE : WM_MOUSE
		{
			public enum XBUTTON
			{
				XBUTTON1 = 0x0001,      // 1つ目の [X] ボタンがダブルクリックされました。
				XBUTTON2 = 0x0002,      // 2つ目の [X] ボタンがダブルクリックされました。
			}
			public XBUTTON XButton { get => (XBUTTON)Param.L.Hi; }
			public WM_X_MOUSE(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
			public override string ToString() =>string.Format("State:{0} XBUTTON:{3} X:{1} Y:{2}", State, X, Y, XButton);
		}
		/// <summary>
		/// 値判定状態
		/// </summary>
		public enum HIT_STATE
		{
			HTBORDER = 18,          // サイズ変更の境界線がないウィンドウの境界線内。
			HTBOTTOM = 15,          // サイズ変更可能なウィンドウの下部の水平方向の境界線内 (ユーザーはマウスをクリックして垂直方向にウィンドウのサイズを変更できます)。
			HTBOTTOMLEFT = 16,      // サイズ変更可能なウィンドウの境界線の左下隅(ユーザーはマウスをクリックして斜め方向にウィンドウのサイズを変更できます)。
			HTBOTTOMRIGHT = 17,     // サイズ変更可能なウィンドウの境界線の右下隅(ユーザーはマウスをクリックしてウィンドウの斜め方向にサイズを変更できます)。
			HTCAPTION = 2,          // タイトル バー内。
			HTCLIENT = 1,           // クライアント領域内。
			HTCLOSE = 20,           // [閉じる] ボタン内。
			HTERROR = -2,           // 画面の背景またはウィンドウ間の分割線上(HTNOWHERE と同じですが、DefWindowProc 関数によってエラーを示すシステム ビープ音が生成される点が異なります)。
			HTGROWBOX = 4,          // サイズ ボックス内(HTSIZE と同じ)。
			HTHELP = 21,            // [ヘルプ] ボタン内。
			HTHSCROLL = 6,          // 水平スクロール バー内。
			HTLEFT = 10,            // サイズ変更可能なウィンドウの左の境界線内(ユーザーはマウスをクリックして水平方向にウィンドウのサイズを変更できます)。
			HTMENU = 5,             // メニュー内。
			HTMAXBUTTON = 9,        // [最大化] ボタン内。
			HTMINBUTTON = 8,        // [最小化] ボタン内。
			HTNOWHERE = 0,          // 画面の背景またはウィンドウ間の分割線上。
			HTREDUCE = 8,           // [最小化] ボタン内。
			HTRIGHT = 11,           // サイズ変更可能なウィンドウの右の境界線内(ユーザーはマウスをクリックして水平方向にウィンドウのサイズを変更できます)。
			HTSIZE = 4,             // サイズ ボックス内(HTSIZE と同じ)。
			HTSYSMENU = 3,          // ウィンドウ メニュー内または子ウィンドウの[閉じる] ボタン内。
			HTTOP = 12,             // ウィンドウの水平方向の上部境界線内。
			HTTOPLEFT = 13,         // ウィンドウの境界線の左上隅。
			HTTOPRIGHT = 14,        // ウィンドウの境界線の右上隅。
			HTTRANSPARENT = -1,     // 現在同じスレッド内の別のウィンドウでカバーされているウィンドウ内(いずれかのウィンドウが HTTRANSPARENT ではないコードを返すまで、メッセージは同じスレッド内の基になるウィンドウに送信されます)。
			HTVSCROLL = 7,          // 垂直スクロール バー内。
			HTZOOM = 9,             // [最大化] ボタン内。
		}
		/// <summary>
		/// WM_NCMOUSE用クラス
		/// </summary>
		public class WM_NCMOUSE : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
			public HIT_STATE State { get => (HIT_STATE)Param.W.Value; }
			public int X { get => Param.L.Lo; }
			public int Y { get => Param.L.Hi; }
			public WM_NCMOUSE(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
			public override string ToString() =>string.Format("HitTest:{0} X:{1},Y:{2}", State, X, Y);
		}
		/// <summary>
		/// WM_SETCURSOR用クラス
		/// </summary>
		public class WM_SETCURSOR : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
			public HIT_STATE State { get => (HIT_STATE)Param.L.LoI; }
			public IntPtr WinHndl { get => Param.W.ValuePtr; }
			public WND_MSG_ENUM MsgID { get => (WND_MSG_ENUM)Param.L.Hi; }
			public WM_SETCURSOR(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
			public override string ToString() =>
				string.Format("Window:0x{0:X8} State:{1} MsgID:{2}(0x{3:X4})", (uint)WinHndl, State,MsgID.ToString(), (ushort)MsgID);
		}
		/// <summary>
		/// WM_COLOR_CHANGE用クラス
		/// </summary>
		public class WM_COLOR_CHANGE : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
			Color Color { get => Color.FromArgb(Param.W.Value); }
			bool IsBlend { get => (Param.L.Value != 0); }
			public WM_COLOR_CHANGE(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
			public override string ToString() =>string.Format("Color:0x{0:X8} IsBlend:{1}", Color.ToArgb(), IsBlend);
		}
		/// <summary>
		/// WM_NOTIFY_FORMAT用クラス
		/// </summary>
		public class WM_NOTIFY_FORMAT : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
			public enum QUERY_KIND
			{
				NFR_ANSI = 1,
				NFR_UNICODE = 2,
				NF_QUERY = 3,
				NF_REQUERY = 4,
			}
			public IntPtr Handle { get => Param.W.ValuePtr; }
			public QUERY_KIND Kind { get => (QUERY_KIND)Param.L.Value; }
			public WM_NOTIFY_FORMAT(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
			public override string ToString() =>string.Format("Handle:0x{0:X8} Query:{1}", (uint)Handle, Kind);
		}
		/// <summary>
		/// WM_PRINT_CLIENT用クラス
		/// </summary>
		public class WM_PRINT_CLIENT : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
			[Flags]
			public enum PRINT_FLAG
			{
				PRF_CHECKVISIBLE = 0x00000001,
				PRF_NONCLIENT = 0x00000002,
				PRF_CLIENT = 0x00000004,
				PRF_ERASEBKGND = 0x00000008,
				PRF_CHILDREN = 0x00000010,
				PRF_OWNED = 0x00000020,
			}

			IntPtr Handle { get => Param.W.ValuePtr; }
			PRINT_FLAG Flags { get => (PRINT_FLAG)Param.L.Value; }
			public WM_PRINT_CLIENT(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
			public override string ToString() => string.Format("Handle:0x{0:X8} Flags:{1}", Handle, Flags);
		}
		/// <summary>
		/// IME_NOTIFY種類
		/// </summary>
		public enum WM_IME_NOTIFY_KIND
		{
			IMN_CLOSESTATUSWINDOW = 0x0001,		// LParam:未使用
			IMN_OPENSTATUSWINDOW = 0x0002,		// LParam:未使用
			IMN_CHANGECANDIDATE = 0x0003,		// LParam:候補リストフラグ。 各ビットは候補リスト(ビット0から最初のリスト、
												//    ビット1から2番目のリストなど) に対応します。 指定したビットが1の場合、
												//    対応する候補ウィンドウが変更されようとしています。
			IMN_CLOSECANDIDATE = 0x0004,		// LParam:候補リストフラグ。
			IMN_OPENCANDIDATE = 0x0005,			// LParam:候補リストフラグ。
			IMN_SETCONVERSIONMODE = 0x0006,		// LParam:未使用
			IMN_SETSENTENCEMODE = 0x0007,		// LParam:未使用
			IMN_SETOPENSTATUS = 0x0008,			// LParam:未使用
			IMN_SETCANDIDATEPOS = 0x0009,		// LParam:候補リストフラグ。
			IMN_SETCOMPOSITIONFONT = 0x000A,	// LParam:未使用
			IMN_SETCOMPOSITIONWINDOW = 0x000B,	// LParam:未使用
			IMN_SETSTATUSWINDOWPOS = 0x000C,	// LParam:未使用
			IMN_GUIDELINE = 0x000D,				// LParam:未使用
			IMN_PRIVATE = 0x000E,				// ???
		}
		/// <summary>
		/// IME_NOTIFY用クラス
		/// </summary>
		public class IME_NOTIFY : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
			WM_IME_NOTIFY_KIND Kind { get=> (WM_IME_NOTIFY_KIND)Param.W.Value; }
			public IME_NOTIFY(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
			public override string ToString()
			{
				if ((Kind == WM_IME_NOTIFY_KIND.IMN_CHANGECANDIDATE) || (Kind == WM_IME_NOTIFY_KIND.IMN_CLOSECANDIDATE) ||
					(Kind == WM_IME_NOTIFY_KIND.IMN_OPENCANDIDATE) || (Kind == WM_IME_NOTIFY_KIND.IMN_SETCANDIDATEPOS))
					return string.Format("{0} ListFlag:0x{1:X8}", Kind, Param.L.ValueU);
				return Kind.ToString();
			}
		}
		/// <summary>
		/// IME_SETCONTEXT用クラス
		/// </summary>
		public class IME_SETCONTEXT : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
			[Flags]
			public enum ISC_SHOWUI
			{
				ISC_SHOWUICANDIDATEWINDOW = 0x00000001,
				ISC_SHOWUICANDIDATEWINDOW1 = 0x00000002,
				ISC_SHOWUICANDIDATEWINDOW2 = 0x00000004,
				ISC_SHOWUICANDIDATEWINDOW3 = 0x00000008,
				ISC_SHOWUICOMPOSITIONWINDOW = -2147483648, // 0x80000000
				ISC_SHOWUIGUIDELINE = 0x40000000,
				ISC_SHOWUIALLCANDIDATEWINDOW = ISC_SHOWUICANDIDATEWINDOW | ISC_SHOWUICANDIDATEWINDOW1 | ISC_SHOWUICANDIDATEWINDOW2 | ISC_SHOWUICANDIDATEWINDOW3,
				ISC_SHOWUIALL = ISC_SHOWUIALLCANDIDATEWINDOW | ISC_SHOWUICOMPOSITIONWINDOW | ISC_SHOWUIGUIDELINE,
			}
			public bool IsWindowActive { get => (Param.W.Value != 0); }
			public ISC_SHOWUI ShowUI { get => (ISC_SHOWUI)Param.L.Value; }
			public IME_SETCONTEXT(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
			public override string ToString() =>string.Format("WindowActive:{0} ShowUI:{1}", IsWindowActive, ShowUI);
		}
	}
}
