using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParam/LParamとも値として保存
            /// </remarks>
			public WM_DEFAULT(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString() { return string.Format("WParam:0x{0:X8} LParam:0x{1:X8}", Param.W.ValueU, Param.L.ValueU); }
		}
		/// <summary>
		/// WPARAM XYクラス
		/// </summary>
		public class WPARAM_XY : WPARAM_LPARAM_IS_VALUE<XY_PARAMS>
		{
            /// <summary>
            /// X値
            /// </summary>
			int X { get => Param.X; }
            /// <summary>
            /// Y値
            /// </summary>
            int Y { get => Param.Y; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParam...上位16bitがY値、下位16bitがX値
            /// LParam.. 未使用
            /// </remarks>
			public WPARAM_XY(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString() { return string.Format("X:{0} Y:{1}", X, Y); }
		}

		/// <summary>
		/// WPARAM SIZEクラス
		/// </summary>
		public class WPARAM_SIZE : WPARAM_LPARAM_IS_VALUE<SIZE_PARAMS>
		{
            /// <summary>
            /// 幅
            /// </summary>
			int Width { get => Param.Width; } 
            /// <summary>
            /// 高さ
            /// </summary>
			int Height { get => Param.Height; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParam...上位16bitが高さ、下位16bitが幅
            /// LParam.. 未使用
            /// </remarks>
			public WPARAM_SIZE(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString() { return string.Format("Width:{0} Height:{1}", Width, Height); }
		}
		/// <summary>
		/// UI_STATE用クラス
		/// </summary>
		public class WM_UISTATE : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
            /// <summary>
            /// 実行されるアクションの定義
            /// </summary>
			public enum WM_UISTATE_ACTION
			{
				UIS_CLEAR = 2,      //!< 上位ワードで指定された UI 状態フラグをクリアする必要があります。
                UIS_INITIALIZE = 3, //!< 上位ワードで指定された UI 状態フラグは、最後の入力イベントに基づいて変更する必要があります。
                UIS_SET = 1,        //!< 上位ワードで指定された UI 状態フラグを設定する必要があります。
            }
            /// <summary>
            /// 影響を受けるUI状態要素またはコントロールのスタイル定義
            /// </summary>
			[Flags]
			public enum WM_UISTATE_STYLE
			{
				UISF_ACTIVE = 0x4,      //!< コントロールは、アクティブなコントロールに使用されるスタイルで描画する必要があります。
                UISF_HIDEACCEL = 0x2,   //!< キーボード アクセラレータ。
                UISF_HIDEFOCUS = 0x01,  //!< フォーカス インジケーター。
            }
            /// <summary>
            /// 実行されるアクション
            /// </summary>
			public WM_UISTATE_ACTION Action { get => (WM_UISTATE_ACTION)Param.W.Lo; }
            /// <summary>
            /// 影響を受けるUI状態要素またはコントロールのスタイル
            /// </summary>
			public WM_UISTATE_STYLE Style { get => (WM_UISTATE_STYLE)Param.W.Hi; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParam...上位16bitがスタイル、下位16bitがアクション
            /// LParam.. 未使用
            /// </remarks>
			public WM_UISTATE(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString()
			{ return string.Format("Action:{0} Style:{1} 0x{2:X8}", Action, Style, Param.W.ValueU); }
		}
		/// <summary>
		/// GetICON用クラス
		/// </summary>
		public class WM_GETICON : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
            /// <summary>
            /// アイコンの種類定義
            /// </summary>
			public enum ICON_KIND
			{   
				ICON_BIG = 1,       //!< ウィンドウの大きなアイコンを取得します。
                ICON_SMALL = 0,     //!< ウィンドウの小さいアイコンを取得します。
                ICON_SMALL2 = 2,    //!< アプリケーションによって提供される小さなアイコンを取得します。
                                    //!< アプリケーションで指定されていない場合、システムはそのウィンドウにシステムによって生成されたアイコンを使用します。
            }
            /// <summary>
            /// アイコンの種類
            /// </summary>
            public ICON_KIND Kind { get => (ICON_KIND)Param.W.Lo; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParam...下位16bitがアイコンの種類
            /// LParam.. 未使用
            /// </remarks>
			public WM_GETICON(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString() { return string.Format("Kind:{0} 0x{1:X8}", Kind, Param.W.ValueU); }
		}
		/// <summary>
		/// WM_ACTIVE用クラス
		/// </summary>
		public class WM_ACTIVE : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
            /// <summary>
            /// ウィンドウがアクティブ化されるか非アクティブ化されるかを指定
            /// </summary>
			public enum ACTIVE_STATE
			{
				WA_ACTIVE = 1,      //!< マウスのクリック以外の何らかの方法 (たとえば、SetActiveWindow関数の呼び出し、
                                    //!< またはキーボードインターフェイスを使用してウィンドウを選択すること) によってアクティブ化されます。
                WA_CLICKACTIVE = 2, //!< マウスをクリックするとアクティブになります。
                WA_INACTIVE = 0,    //!< 無効化されました。
            }
            /// <summary>
            /// ウィンドウの状態
            /// </summary>
			public ACTIVE_STATE State { get => (ACTIVE_STATE)Param.W.Lo; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParam...下位16bitがウィンドウの状態
            /// LParam.. 未使用
            /// </remarks>
			public WM_ACTIVE(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString() 
			{ return string.Format("State:{0} wParamHi:{1} WinHandle:0x{2:X8}", State, Param.W.Hi, Param.L.ValueU); }
		}
		/// <summary>
		/// WM_COMMAND用クラス
		/// </summary>
		public class WM_COMMAND : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
            /// <summary>
            /// メッセージソース定義
            /// </summary>
			public enum SOURCE
			{   
				MENU = 0,           //!< メニュー
				ACCELERATOR = 1,    //!< Accelerator
                CONTROL = 2,        //!< コントロール
            }
            /// <summary>
            /// メッセージソース
            /// </summary>
			public SOURCE Source { get => (Param.W.Hi <= 1) ? (SOURCE)Param.W.Hi : SOURCE.CONTROL; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParam...メッセージソース
            /// LParam.. 未使用
            /// </remarks>
            public WM_COMMAND(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
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
            /// <summary>
            /// 表示されているウィンドウの状態
            /// </summary>
			public enum WINDOW_STATE
			{
				SW_OTHERUNZOOM = 4,     //!< 最大化ウィンドウが復元または最小化されたため、ウィンドウが見つかりませんでした。
                SW_OTHERZOOM = 2,       //!< ウィンドウは、最大化された別のウィンドウで覆われています。
                SW_PARENTCLOSING = 1,   //!< ウィンドウの所有者ウィンドウが最小化されています。
                SW_PARENTOPENING = 3,   //!< ウィンドウの所有者ウィンドウが復元されています。
            }
            /// <summary>
            /// ウィンドウの状態
            /// </summary>
			public WINDOW_STATE State { get => (WINDOW_STATE)Param.L.ValuePtr; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParam...未使用
            /// LParam.. ウィンドウの状態
            /// </remarks>
			public WM_SHOWWINDOW(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString()
			{ return string.Format("{0} State:{1}", (Param.W.ValuePtr == (IntPtr)0) ? "Inactive" : "Active", State);}
		}
		/// <summary>
		/// WM_SYSCOMMAND用クラス
		/// </summary>
		public class WM_SYSCOMMAND : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
            /// <summary>
            /// 要求されたシステム コマンドの種類
            /// </summary>
			public enum CMD
			{
				SC_CLOSE = 0xF060,              //!< ウィンドウを閉じます。
                SC_CONTEXTHELP = 0xF180,        //!< ポインターを使用して、カーソルを疑問符に変更します。 ユーザーがダイアログ ボックスでコントロールをクリックすると、コントロールは WM_HELP メッセージを受け取ります。
                SC_DEFAULT = 0xF160,            //!< 既定の項目を選択します。ユーザーがウィンドウ メニューをダブルクリックしました。
                SC_HOTKEY = 0xF150,             //!< アプリケーション指定のホット キーに関連付けられているウィンドウをアクティブにします。 lParam パラメーターは、アクティブにするウィンドウを識別します。
                SC_HSCROLL = 0xF080,            //!< 水平方向にスクロールします。
                SCF_ISSECURE = 0x00000001,      //!< スクリーン セーバーが安全かどうかを示します。
                SC_KEYMENU = 0xF100,            //!< キーストロークの結果としてウィンドウ メニューを取得します。 詳細については、「解説」を参照してください。
                SC_MAXIMIZE = 0xF030,           //!< ウィンドウを最大化します。
                SC_MINIMIZE = 0xF020,           //!< ウィンドウを最小化します。
                SC_MONITORPOWER = 0xF170,       //!< 表示の状態を設定します。 このコマンドは、バッテリ駆動のパーソナル コンピューターなど、省電力機能を備えたデバイスをサポートします。
                                                //!<  lParam パラメーターには、次の値を指定できます。
                                                //!<   -1 (ディスプレイの電源が入ります)
                                                //!<    1 (ディスプレイは低電力になります)
                                                //!<    2 (ディスプレイがシャットダウン中)
                SC_MOUSEMENU = 0xF090,          //!< マウス クリックの結果としてウィンドウ メニューを取得します。
                SC_MOVE = 0xF010,               //!< ウィンドウを移動します。
                SC_NEXTWINDOW = 0xF040,         //!< 次のウィンドウに移動します。
                SC_PREVWINDOW = 0xF050,         //!< 前のウィンドウに移動します。
                SC_RESTORE = 0xF120,            //!< ウィンドウを通常の位置とサイズに戻します。
                SC_SCREENSAVE = 0xF140,         //!< System.ini ファイルの[boot] セクションで指定されたスクリーン セーバー アプリケーションを実行します。
                SC_SIZE = 0xF000,               //!< ウィンドウのサイズを設定します。
                SC_TASKLIST = 0xF130,           //!< [スタート] メニューをアクティブにします。
                SC_VSCROLL = 0xF070,            //!< 垂直方向にスクロールします。
            }
            /// <summary>
            /// 要求されたシステムコマンド
            /// </summary>
			public CMD Cmd { get => (CMD)Param.W.ValueU; }
            /// <summary>
            /// X座標
            /// </summary>
			public int X { get => Param.L.Lo; }
            /// <summary>
            /// Y座標
            /// </summary>
			public int Y { get => Param.L.Hi; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParam...システムコマンド
            /// LParam.. 上位16bitがY座標、下位16bitがX座標
            /// </remarks>
			public WM_SYSCOMMAND(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString() { return string.Format("CMD:{0} X:{1} Y:{2}", Cmd, X, Y); }
		}
		/// <summary>
		/// マウス用共通クラス
		/// </summary>
		public class WM_MOUSE : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
            /// <summary>
            /// マウスボタンの定義
            /// </summary>
			[Flags]
			public enum STATE
			{
				MK_CONTROL = 0x0008,        //!< CTRL キーが押されています。
                MK_LBUTTON = 0x0001,        //!< マウスの左ボタンが押されています。
                MK_MBUTTON = 0x0010,        //!< マウスの中央ボタンが押されています。
                MK_RBUTTON = 0x0002,        //!< マウスの右ボタンが押されています。
                MK_SHIFT = 0x0004,          //!< Shift キーが押されています。
                MK_XBUTTON1 = 0x0020,       //!< 最初の X ボタンが押されています。
                MK_XBUTTON2 = 0x0040,       //!< 2 つ目の X ボタンが押されています。
            }
            /// <summary>
            /// マウスボタンの状態
            /// </summary>
			public STATE State { get => (STATE)Param.W.Lo; }
            /// <summary>
            /// X座標
            /// </summary>
			public int X { get => Param.L.Lo; }
            /// <summary>
            /// Y座標
            /// </summary>
			public int Y { get => Param.L.Hi; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParam.. 下位16bit マウスボタンの状態
            /// LParam.. 上位16bitがY座標、下位16bitがX座標
            /// </remarks>
			public WM_MOUSE(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString() { return string.Format("State:{0} X:{1} Y:{2}", State, X, Y); }
		}
		/// <summary>
		/// WM_MOUSE_WHEEL用クラス
		/// </summary>
		public class WM_MOUSE_WHEEL : WM_MOUSE
		{
            /// <summary>
            /// マウスホィール値
            /// </summary>
			public int Wheel { get => Param.W.HiI; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParamの上位16bit マウスホィール値
            /// それ以外は、WM_MOUSEと同じ
            /// </remarks>
			public WM_MOUSE_WHEEL(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString() { return string.Format("State:{0} X:{1} Y:{2} Wheel:{2}", State, X, Y, Wheel); }
		}
		/// <summary>
		/// Xボタン用クラス
		/// </summary>
		public class WM_X_MOUSE : WM_MOUSE
		{
            /// <summary>
            /// Xボタンの状態定義
            /// </summary>
			public enum XBUTTON
			{
				XBUTTON1 = 0x0001,      //!< 1つ目の [X] ボタンがダブルクリックされました。
                XBUTTON2 = 0x0002,      //!< 2つ目の [X] ボタンがダブルクリックされました。
            }
            /// <summary>
            /// Xボタンの状態
            /// </summary>
			public XBUTTON XButton { get => (XBUTTON)Param.L.Hi; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParamの上位16bit Xボタンの状態
            /// それ以外は、WM_MOUSEと同じ
            /// </remarks>
            public WM_X_MOUSE(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString() { return string.Format("State:{0} XBUTTON:{3} X:{1} Y:{2}", State, X, Y, XButton); }
		}
		/// <summary>
		/// 当たり判定状態
		/// </summary>
		public enum HIT_STATE
		{
			HTBORDER = 18,          //!< サイズ変更の境界線がないウィンドウの境界線内。
            HTBOTTOM = 15,          //!< サイズ変更可能なウィンドウの下部の水平方向の境界線内 (ユーザーはマウスをクリックして垂直方向にウィンドウのサイズを変更できます)。
            HTBOTTOMLEFT = 16,      //!< サイズ変更可能なウィンドウの境界線の左下隅(ユーザーはマウスをクリックして斜め方向にウィンドウのサイズを変更できます)。
            HTBOTTOMRIGHT = 17,     //!< サイズ変更可能なウィンドウの境界線の右下隅(ユーザーはマウスをクリックしてウィンドウの斜め方向にサイズを変更できます)。
            HTCAPTION = 2,          //!< タイトル バー内。
            HTCLIENT = 1,           //!< クライアント領域内。
            HTCLOSE = 20,           //!< [閉じる] ボタン内。
            HTERROR = -2,           //!< 画面の背景またはウィンドウ間の分割線上(HTNOWHERE と同じですが、DefWindowProc 関数によってエラーを示すシステム ビープ音が生成される点が異なります)。
            HTGROWBOX = 4,          //!< サイズ ボックス内(HTSIZE と同じ)。
            HTHELP = 21,            //!< [ヘルプ] ボタン内。
            HTHSCROLL = 6,          //!< 水平スクロール バー内。
            HTLEFT = 10,            //!< サイズ変更可能なウィンドウの左の境界線内(ユーザーはマウスをクリックして水平方向にウィンドウのサイズを変更できます)。
            HTMENU = 5,             //!< メニュー内。
            HTMAXBUTTON = 9,        //!< [最大化] ボタン内。
            HTMINBUTTON = 8,        //!< [最小化] ボタン内。
            HTNOWHERE = 0,          //!< 画面の背景またはウィンドウ間の分割線上。
            HTREDUCE = 8,           //!< [最小化] ボタン内。
            HTRIGHT = 11,           //!< サイズ変更可能なウィンドウの右の境界線内(ユーザーはマウスをクリックして水平方向にウィンドウのサイズを変更できます)。
            HTSIZE = 4,             //!< サイズ ボックス内(HTSIZE と同じ)。
            HTSYSMENU = 3,          //!< ウィンドウ メニュー内または子ウィンドウの[閉じる] ボタン内。
            HTTOP = 12,             //!< ウィンドウの水平方向の上部境界線内。
            HTTOPLEFT = 13,         //!< ウィンドウの境界線の左上隅。
            HTTOPRIGHT = 14,        //!< ウィンドウの境界線の右上隅。
            HTTRANSPARENT = -1,     //!< 現在同じスレッド内の別のウィンドウでカバーされているウィンドウ内(いずれかのウィンドウが HTTRANSPARENT ではないコードを返すまで、メッセージは同じスレッド内の基になるウィンドウに送信されます)。
            HTVSCROLL = 7,          //!< 垂直スクロール バー内。
            HTZOOM = 9,             //!< [最大化] ボタン内。
        }
		/// <summary>
		/// WM_NCMOUSE用クラス
		/// </summary>
		public class WM_NCMOUSE : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
            /// <summary>
            /// 当たり判定
            /// </summary>
			public HIT_STATE State { get => (HIT_STATE)Param.W.Value; }
            /// <summary>
            /// X座標
            /// </summary>
			public int X { get => Param.L.Lo; }
            /// <summary>
            /// Y座標
            /// </summary>
			public int Y { get => Param.L.Hi; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParam.. 当たり判定
            /// LParam.. 上位16bitがY座標、下位16bitがX座標
            /// </remarks>
            public WM_NCMOUSE(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString() { return string.Format("HitTest:{0} X:{1},Y:{2}", State, X, Y); }
		}
		/// <summary>
		/// WM_SETCURSOR用クラス
		/// </summary>
		public class WM_SETCURSOR : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
            /// <summary>
            /// 当たり判定
            /// </summary>
			public HIT_STATE State { get => (HIT_STATE)Param.L.LoI; }
            /// <summary>
            /// ウィンドウハンドル
            /// </summary>
			public IntPtr WinHndl { get => Param.W.ValuePtr; }
            /// <summary>
            /// メッセージID
            /// </summary>
			public WND_MSG_ENUM MsgID { get => (WND_MSG_ENUM)Param.L.Hi; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParam.. ウィンドウハンドル
            /// LParam.. 上位16bitがメッセージID、下位16bitが当たり判定
            /// </remarks>
            public WM_SETCURSOR(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString() 
            { return string.Format("Window:0x{0:X8} State:{1} MsgID:{2}(0x{3:X4})", (uint)WinHndl, State, MsgID.ToString(), (ushort)MsgID); }
		}
		/// <summary>
		/// WM_COLOR_CHANGE用クラス
		/// </summary>
		public class WM_COLOR_CHANGE : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
            /// <summary>
            /// 新しい色
            /// </summary>
			Color Color { get => Color.FromArgb(Param.W.Value); }
            /// <summary>
            /// 新しい色を不透明度とブレンドするかどうかを指定
            /// </summary>
            bool IsBlend { get => (Param.L.Value != 0); }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParam.. 新しい色
            /// LParam.. ブレンド指定
            /// </remarks>
			public WM_COLOR_CHANGE(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString() { return string.Format("Color:0x{0:X8} IsBlend:{1}", Color.ToArgb(), IsBlend); }
		}
		/// <summary>
		/// WM_NOTIFY_FORMAT用クラス
		/// </summary>
		public class WM_NOTIFY_FORMAT : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
            /// <summary>
            /// WM_NOTIFYFORMATメッセージの性質を指定するコマンド値、および戻り値
            /// </summary>
			public enum QUERY_KIND
			{
				NFR_ANSI = 1,       //!< ANSI構造体
                NFR_UNICODE = 2,    //!< Unicode構造体
                NF_QUERY = 3,       //!< ANSI構造体と Unicode構造体のどちらを使用するかを判断するためのクエリ
                NF_REQUERY = 4,     //!< NF_QUERY形式を親ウィンドウに送信するコントロールの要求
            }
            /// <summary>
            /// ハンドル
            /// </summary>
			public IntPtr Handle { get => Param.W.ValuePtr; }
            /// <summary>
            /// 要求種別
            /// </summary>
			public QUERY_KIND Kind { get => (QUERY_KIND)Param.L.Value; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParam.. ハンドル
            /// LParam.. 要求種別
            /// </remarks>
			public WM_NOTIFY_FORMAT(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString() { return string.Format("Handle:0x{0:X8} Query:{1}", (uint)Handle, Kind); }
		}
		/// <summary>
		/// WM_PRINT_CLIENT用クラス
		/// </summary>
		public class WM_PRINT_CLIENT : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
            /// <summary>
            /// 描画オプション定義
            /// </summary>
			[Flags]
			public enum PRINT_FLAG
			{
				PRF_CHECKVISIBLE = 0x00000001,  //!< ウィンドウが表示されている場合にのみ、ウィンドウを描画します。
                PRF_NONCLIENT = 0x00000002,     //!< 表示されているすべての子ウィンドウを描画します。
                PRF_CLIENT = 0x00000004,        //!< ウィンドウのクライアント領域を描画します。
                PRF_ERASEBKGND = 0x00000008,    //!< ウィンドウを描画する前に背景を消去します。
                PRF_CHILDREN = 0x00000010,      //!< ウィンドウの非クライアント領域を描画します。
                PRF_OWNED = 0x00000020,         //!< 所有されているすべてのウィンドウを描画します。
            }
            /// <summary>
            /// ハンドル
            /// </summary>
			IntPtr Handle { get => Param.W.ValuePtr; }
            /// <summary>
            /// 描画オプション
            /// </summary>
			PRINT_FLAG Flags { get => (PRINT_FLAG)Param.L.Value; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParam.. ハンドル
            /// LParam.. 描画オプション
            /// </remarks>
			public WM_PRINT_CLIENT(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString() { return string.Format("Handle:0x{0:X8} Flags:{1}", Handle, Flags); }
		}
		/// <summary>
		/// IME_NOTIFY種類
		/// </summary>
		public enum WM_IME_NOTIFY_KIND
		{
			IMN_CLOSESTATUSWINDOW = 0x0001,     //!< LParam:未使用
            IMN_OPENSTATUSWINDOW = 0x0002,      //!< LParam:未使用
            IMN_CHANGECANDIDATE = 0x0003,       //!< LParam:候補リストフラグ。 各ビットは候補リスト(ビット0から最初のリスト、
                                                //!<    ビット1から2番目のリストなど) に対応します。 指定したビットが1の場合、
                                                //!<    対応する候補ウィンドウが変更されようとしています。
            IMN_CLOSECANDIDATE = 0x0004,        //!< LParam:候補リストフラグ。
            IMN_OPENCANDIDATE = 0x0005,         //!< LParam:候補リストフラグ。
            IMN_SETCONVERSIONMODE = 0x0006,     //!< LParam:未使用
            IMN_SETSENTENCEMODE = 0x0007,       //!< LParam:未使用
            IMN_SETOPENSTATUS = 0x0008,         //!< LParam:未使用
            IMN_SETCANDIDATEPOS = 0x0009,       //!< LParam:候補リストフラグ。
            IMN_SETCOMPOSITIONFONT = 0x000A,    //!< LParam:未使用
            IMN_SETCOMPOSITIONWINDOW = 0x000B,  //!< LParam:未使用
            IMN_SETSTATUSWINDOWPOS = 0x000C,    //!< LParam:未使用
            IMN_GUIDELINE = 0x000D,             //!< LParam:未使用
            IMN_PRIVATE = 0x000E,               //!< ???
        }
		/// <summary>
		/// IME_NOTIFY用クラス
		/// </summary>
		public class IME_NOTIFY : WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM>
		{
            /// <summary>
            /// NOTIFY種別
            /// </summary>
			WM_IME_NOTIFY_KIND Kind { get=> (WM_IME_NOTIFY_KIND)Param.W.Value; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParam.. NOTIFY種別 
            /// LParam.. 未使用
            /// </remarks>
            public IME_NOTIFY(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
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
            /// <summary>
            /// IME UI表示方法定義
            /// </summary>
			[Flags]
			public enum ISC_SHOWUI
			{
				ISC_SHOWUICANDIDATEWINDOW = 0x00000001,     //!< ユーザーインターフェイスウィンドウでインデックス 0 の候補ウィンドウを表示します。
                ISC_SHOWUICANDIDATEWINDOW1 = 0x00000002,    //!< ユーザーインターフェイスウィンドウでインデックス 1 の候補ウィンドウを表示します。
                ISC_SHOWUICANDIDATEWINDOW2 = 0x00000004,    //!< ユーザーインターフェイスウィンドウでインデックス 2 の候補ウィンドウを表示します。
                ISC_SHOWUICANDIDATEWINDOW3 = 0x00000008,    //!< ユーザーインターフェイスウィンドウでインデックス 3 の候補ウィンドウを表示します。
                ISC_SHOWUICOMPOSITIONWINDOW = -2147483648,  //!< ユーザーインターフェイスウィンドウでコンポジション ウィンドウを表示します。(=0x80000000)
                ISC_SHOWUIGUIDELINE = 0x40000000,           //!< ユーザーインターフェイスウィンドウでガイド ウィンドウを表示します。
                ISC_SHOWUIALLCANDIDATEWINDOW = ISC_SHOWUICANDIDATEWINDOW | ISC_SHOWUICANDIDATEWINDOW1 | ISC_SHOWUICANDIDATEWINDOW2 | ISC_SHOWUICANDIDATEWINDOW3,	//!< 
                ISC_SHOWUIALL = ISC_SHOWUIALLCANDIDATEWINDOW | ISC_SHOWUICOMPOSITIONWINDOW | ISC_SHOWUIGUIDELINE,   //!<
            }
            /// <summary>
            /// ウィンドウがアクティブな場合は TRUE、それ以外の場合は FALSE
            /// </summary>
			public bool IsWindowActive { get => (Param.W.Value != 0); }
            /// <summary>
            /// 表示オプション
            /// </summary>
			public ISC_SHOWUI ShowUI { get => (ISC_SHOWUI)Param.L.Value; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParam.. ウィンドウアクティブ状態 
            /// LParam.. 表示オプション
            /// </remarks>
			public IME_SETCONTEXT(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) { }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString() { return string.Format("WindowActive:{0} ShowUI:{1}", IsWindowActive, ShowUI); }
		}
        /// <summary>
        /// ウィンドウ スタイル
        /// ウィンドウに適用できるスタイル定義
        /// </summary>
        [Flags]
        public enum WINDOW_STYLE : long
        {
            WS_BORDER = 0x00800000L,    //!< ウィンドウに細い線の罫線がある
            WS_CAPTION = 0x00C00000L,   //!< ウィンドウにはタイトル バーがあります (WS_BORDER スタイルが含まれます)。
            WS_CHILD = 0x40000000L, //!< ウィンドウは子ウィンドウです。 このスタイルのウィンドウにメニュー バーを設定することはできません。 このスタイルは 、WS_POPUP スタイルでは使用できません。
            WS_CHILDWINDOW = 0x40000000L,   //!< WS_CHILD スタイルと同じです。
            WS_CLIPCHILDREN = 0x02000000L,  //!< 親ウィンドウ内で描画が行われるときに、子ウィンドウが占有する領域を除外します。 このスタイルは、親ウィンドウを作成するときに使用されます。
            WS_CLIPSIBLINGS = 0x04000000L,  //!< 互いに相対的に子ウィンドウをクリップします。つまり、特定の子ウィンドウが WM_PAINT メッセージを受信すると、 WS_CLIPSIBLINGS スタイルは、更新する子ウィンドウの領域から他のすべての重複する子ウィンドウをクリップします。 WS_CLIPSIBLINGSが指定されておらず、子ウィンドウが重複している場合は、子ウィンドウのクライアント領域内で描画するときに、隣接する子ウィンドウのクライアント領域内に描画できます。
            WS_DISABLED = 0x08000000L,  //!< ウィンドウは最初は無効になっています。 無効なウィンドウは、ユーザーからの入力を受信できません。 ウィンドウの作成後にこれを変更するには、 EnableWindow 関数を使用します。
            WS_DLGFRAME = 0x00400000L,  //!< ウィンドウには、通常ダイアログ ボックスで使用されるスタイルの境界線があります。 このスタイルのウィンドウにタイトル バーを設定することはできません。
            Ws_group = 0x00020000L, //!< ウィンドウは、コントロールのグループの最初のコントロールです。 グループは、この最初のコントロールと、
                                    //!< その後に定義されたすべてのコントロールから、 WS_GROUP スタイルを持つ次のコントロールまでで構成されます。
                                    //!< 各グループの最初のコントロールには通常、 ユーザー がグループからグループに移動できるように、
                                    //!< WS_TABSTOP スタイルがあります。 ユーザーは、その後、方向キーを使用して、グループ内の 1 つのコントロールから
                                    //!< グループ内の次のコントロールにキーボード フォーカスを変更できます。
                                    //!< このスタイルのオンとオフを切り替えて、ダイアログ ボックスのナビゲーションを変更できます。 ウィンドウの作成後にこのスタイルを変更するには、 SetWindowLong 関数を使用します。
            WS_HSCROLL = 0x00100000L,   //!< ウィンドウには水平スクロール バーがあります。
            WS_ICONIC = 0x20000000L,    //!< ウィンドウは最初は最小化されます。 WS_MINIMIZEスタイルと同じです。
            WS_MAXIMIZE = 0x01000000L,  //!< ウィンドウは最初は最大化されます。
            WS_MAXIMIZEBOX = 0x00010000L,   //!< ウィンドウには最大化ボタンがあります。 WS_EX_CONTEXTHELP スタイルと組み合わせることはできません。 WS_SYSMENU スタイルも指定する必要があります。
            WS_MINIMIZE = 0x20000000L,  //!< ウィンドウは最初は最小化されます。 WS_ICONICスタイルと同じです。
            WS_MINIMIZEBOX = 0x00020000L,   //!< ウィンドウには最小化ボタンがあります。 WS_EX_CONTEXTHELP スタイルと組み合わせることはできません。 WS_SYSMENU スタイルも指定する必要があります。
            WS_OVERLAPPED = 0x00000000L,    //!< ウィンドウは重なり合ったウィンドウです。 オーバーラップ ウィンドウには、タイトル バーと境界線があります。 WS_TILEDスタイルと同じです。
            WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX),  //!< ウィンドウは重なり合ったウィンドウです。 WS_TILEDWINDOW スタイルと同じです。
            WS_POPUP = 0x80000000L, //!< ウィンドウはポップアップ ウィンドウです。 このスタイルは 、WS_CHILD スタイルでは使用できません。
            WS_POPUPWINDOW = (WS_POPUP | WS_BORDER | WS_SYSMENU),  //!< ウィンドウはポップアップ ウィンドウです。 ウィンドウ メニューを表示するには、WS_CAPTIONスタイルとWS_POPUPWINDOWスタイルを組み合わせる必要があります。
            WS_SIZEBOX = 0x00040000L,   //!< ウィンドウにはサイズ設定の境界線があります。 WS_THICKFRAME スタイルと同じです。
            WS_SYSMENU = 0x00080000L,   //!< ウィンドウには、タイトル バーにウィンドウ メニューがあります。 WS_CAPTION スタイルも指定する必要があります。
            WS_TABSTOP = 0x00010000L,   //!< ウィンドウは、ユーザーが Tab キーを押したときにキーボード フォーカスを受け取ることができるコントロールです。
                                        //!< Tab キーを押すと、キーボードフォーカスが WS_TABSTOP スタイルの次のコントロールに変更されます。
                                        //!< このスタイルのオンとオフを切り替えて、ダイアログ ボックスのナビゲーションを変更できます。
                                        //!< ウィンドウの作成後にこのスタイルを変更するには、 SetWindowLong 関数を使用します。 ユーザーが作成したウィンドウとモードレス ダイアログがタブ位置を操作するには、メッセージ ループを変更して IsDialogMessage 関数を呼び出します。
            WS_THICKFRAME = 0x00040000L,    //!< ウィンドウにはサイズ設定の境界線があります。 WS_SIZEBOXスタイルと同じです。
            WS_TILED = 0x00000000L, //!< ウィンドウは重なり合ったウィンドウです。 オーバーラップ ウィンドウには、タイトル バーと境界線があります。 WS_OVERLAPPED スタイルと同じです。
            WS_TILEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX),  //!< ウィンドウは重なり合ったウィンドウです。 WS_OVERLAPPEDWINDOWスタイルと同じです。
            WS_VISIBLE = 0x10000000L,   //!< ウィンドウは最初に表示されます。
                                        //!< このスタイルは、 ShowWindow または SetWindowPos 関数を使用してオンとオフを切 り 替えることができます。
            WS_VSCROLL = 0x00200000L,	//!< ウィンドウには垂直スクロール バーがあります
        }
        /// <summary>
        /// 拡張ウィンドウスタイル定義
        /// </summary>
        [Flags]
        public enum EXTEND_WINDOW_STYLE : long
        {
            WS_EX_ACCEPTFILES = 0x00000010L,    //!< ウィンドウは、ドラッグ アンド ドロップ ファイルを受け入れます。
            WS_EX_APPWINDOW = 0x00040000L,  //!< ウィンドウが表示されているときに、タスク バーに最上位のウィンドウを強制的に配置します。
            WS_EX_CLIENTEDGE = 0x00000200L, //!< ウィンドウには、縁がくぼんだ境界線があります。
            WS_EX_COMPOSITED = 0x02000000L, //!< ダブルバッファリングを使用して、ウィンドウのすべての子孫を下から上に塗りつぶします。 下から上への描画順序を使用すると、子孫ウィンドウに半透明 (アルファ) 効果と透明度(カラー キー) 効果を適用できますが、子孫ウィンドウにもWS_EX_TRANSPARENTビットが設定されている場合に限ります。 ダブルバッファリングを使用すると、ウィンドウとその子孫をちらつきなく塗りつぶすことができます。 ウィンドウの クラス スタイル が CS_OWNDCまたは CS_CLASSDC の場合、これは使用できません。
                                            //!< Windows 2000: このスタイルはサポートされていません。
            WS_EX_CONTEXTHELP = 0x00000400L,    //!< ウィンドウのタイトル バーには疑問符が含まれています。 ユーザーがこの疑問符をクリックすると、カーソルは、疑問符付きのポインターに変化します。 ユーザーが子ウィンドウをクリックすると、子は WM_HELP メッセージを受け取ります。 子ウィンドウは、HELP_WM_HELP コマンドを使用して WinHelp 関数を呼び出す必要がある親ウィンドウ プロシージャにメッセージを渡す必要があります。 ヘルプ アプリケーションには、通常、子ウィンドウのヘルプを含むポップアップ ウィンドウが表示されます。
                                                //!< WS_EX_CONTEXTHELP は、 WS_MAXIMIZEBOX または WS_MINIMIZEBOX スタイルでは使用できません。
            WS_EX_CONTROLPARENT = 0x00010000L,  //!< ウィンドウ自体には、ダイアログ ボックスのナビゲーションに参加する必要がある子ウィンドウが含まれています。 このスタイルを指定すると、Tab キー、方向キー、キーボードニーモニックの処理などのナビゲーション操作を実行するときに、ダイアログ マネージャーはこのウィンドウの子に再帰されます。
            WS_EX_DLGMODALFRAME = 0x00000001L,  //!< ウィンドウには二重の境界線があります。必要に応じて、dwStyle パラメーターでWS_CAPTION スタイルを指定することで、ウィンドウをタイトル バーで作成できます。
            WS_EX_LAYERED = 0x00080000L,    //!< ウィンドウは レイヤーウィンドウです。 ウィンドウのクラス スタイルが CS_OWNDC または CS_CLASSDC の場合、このスタイルは使用できません。
                                            //!< Windows 8:WS_EX_LAYERED スタイルは、最上位のウィンドウと子ウィンドウでサポートされています。 以前のバージョンの Windows では、最上位のウィンドウに対してのみ WS_EX_LAYERED がサポートされています。
            WS_EX_LAYOUTRTL = 0x00400000L,  //!< シェル言語がヘブライ語、アラビア語、または読み取り順序の配置をサポートする別の言語の場合、ウィンドウの水平方向の原点は右端にあります。 水平方向の値を増やすと、左に進みます。
            WS_EX_LEFT = 0x00000000L,   //!< ウィンドウには、一般的な左揃えプロパティがあります。 既定値です。
            WS_EX_LEFTSCROLLBAR = 0x00004000L,  //!< シェル言語がヘブライ語、アラビア語、または読み取り順序の配置をサポートする別の言語の場合、垂直スクロール バー(存在する場合) はクライアント領域の左側にあります。 他の言語の場合、スタイルは無視されます。
            WS_EX_LTRREADING = 0x00000000L, //!< ウィンドウ テキストは、左から右の読み取り順序プロパティを使用して表示されます。 既定値です。
            WS_EX_MDICHILD = 0x00000040L,   //!< ウィンドウは MDI 子ウィンドウです。
            WS_EX_NOACTIVATE = 0x08000000L, //!< このスタイルで作成されたトップレベル ウィンドウは、ユーザーがクリックしても前景ウィンドウになりません。 ユーザーがフォアグラウンド ウィンドウを最小化または閉じると、システムはこのウィンドウをフォアグラウンドに移動しません。
                                            //!< プログラムによるアクセスや、ナレーターなどのアクセス可能なテクノロジによるキーボード ナビゲーションを使用して、ウィンドウをアクティブ化しないでください。
                                            //!< ウィンドウをアクティブにするには、 SetActiveWindow または SetForegroundWindow 関数を 使用します。
                                            //!< 既定では、タスク バーにウィンドウは表示されません。 タスク バーにウィンドウを強制的に表示するには、 WS_EX_APPWINDOW スタイルを使用します。
            WS_EX_NOINHERITLAYOUT = 0x00100000L,    //!< ウィンドウは、そのウィンドウ レイアウトを子ウィンドウに渡しません。
            WS_EX_NOPARENTNOTIFY = 0x00000004L, //!< このスタイルで作成された子ウィンドウは、作成時または破棄時に WM_PARENTNOTIFY メッセージを親ウィンドウに送信しません。
            WS_EX_NOREDIRECTIONBITMAP = 0x00200000L,    //!< ウィンドウはリダイレクト 画面にレンダリングされません。 これは、目に見えるコンテンツがないウィンドウ、またはサーフェス以外のメカニズムを使用してビジュアルを提供するウィンドウ用です。
            WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE), //!< ウィンドウは重なり合ったウィンドウです。
            WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST),    //!< ウィンドウはパレット ウィンドウで、コマンドの配列を表示するモードレス ダイアログ ボックスです。
            WS_EX_RIGHT = 0x00001000L,  //!< ウィンドウには、汎用の "右揃え" プロパティがあります。 これはウィンドウ クラスに依存します。 このスタイルは、シェル言語がヘブライ語、アラビア語、または読み取り順序の配置をサポートする別の言語である場合にのみ有効です。それ以外の場合、スタイルは無視されます。
                                        //!< 静的コントロールまたは編集コントロールに WS_EX_RIGHT スタイルを使用すると、 それぞれ SS_RIGHT または ES_RIGHT スタイルを使用する場合と同じ効果があります。 ボタン コントロールでこのスタイルを使用すると、 BS_RIGHT スタイルや BS_RIGHTBUTTON スタイルを使用する場合と同じ効果があります。
            WS_EX_RIGHTSCROLLBAR = 0x00000000L, //!< 垂直スクロール バー(存在する場合) は、クライアント領域の右側にあります。 既定値です。
            WS_EX_RTLREADING = 0x00002000L, //!< シェル言語がヘブライ語、アラビア語、または読み取り順序の配置をサポートする別の言語である場合、ウィンドウ テキストは右から左への読み取り順序プロパティを使用して表示されます。 他の言語の場合、スタイルは無視されます。
            WS_EX_STATICEDGE = 0x00020000L, //!< ウィンドウには、ユーザー入力を受け入れられないアイテムに使用することを目的とした 3 次元の罫線スタイルがあります。
            WS_EX_TOOLWINDOW = 0x00000080L, //!< ウィンドウは、フローティング ツールバーとして使用することを目的としています。 ツール ウィンドウには、通常のタイトル バーより短いタイトル バーがあり、ウィンドウ タイトルは小さいフォントを使用して描画されます。 ツール ウィンドウは、タスク バーや、ユーザーが Alt キーを押しながら Tab キーを押したときに表示されるダイアログには表示されません。 ツール ウィンドウにシステム メニューがある場合、そのアイコンはタイトル バーに表示されません。 ただし、右クリックするか、Alt + SPACE キーを押して、システム メニューを表示できます。
            WS_EX_TOPMOST = 0x00000008L,    //!< ウィンドウは、最上位以外のすべてのウィンドウの上に配置し、ウィンドウが非アクティブ化されている場合でも、その上に配置する必要があります。 このスタイルを追加または削除するには、 SetWindowPos 関数を使用します。
            WS_EX_TRANSPARENT = 0x00000020L,    //!< ウィンドウの下の兄弟(同じスレッドによって作成された) が描画されるまで、ウィンドウを描画しないでください。 基になる兄弟ウィンドウのビットが既に塗りつぶされているため、ウィンドウは透明に表示されます。
                                                //!< これらの制限なしで透過性を実現するには、 SetWindowRgn 関数を 使用します。
            WS_EX_WINDOWEDGE = 0x00000100L, //!< ウィンドウには、エッジが上がった罫線があります。
        }
        /// <summary>
        /// アプリケーションのウィンドウ プロシージャに渡される初期化パラメータークラス
        /// CREATESTRUCT構造体のクラス形式
        /// </summary>
        public class WM_CREATE : ONLY_LPARAM<CREATESTRUCTA>
		{
            /// <summary>
            /// ウィンドウの作成に使用できる追加のデータ
            /// </summary>
            public IntPtr CreateParams { get => lParam.lpCreateParams; }
            /// <summary>
            /// 新しいウィンドウを所有するモジュールのハンドル
            /// </summary>
            public IntPtr Instance { get => lParam.hInstance; }
            /// <summary>
            /// 新しいウィンドウで使用するメニューのハンドル
            /// </summary>
            public IntPtr Menu { get => lParam.hMenu; }
            /// <summary>
            /// ウィンドウが子ウィンドウの場合は、親ウィンドウへのハンドル
            /// ウィンドウが所有されている場合、このメンバーは所有者ウィンドウを識別します。
            /// ウィンドウが子ウィンドウまたは所有ウィンドウでない場合、このメンバーは NULL
            /// </summary>
            public IntPtr HwndParent { get => lParam.hwndParent; }
            /// <summary>
            /// 新しいウィンドウの高さ (ピクセル単位)
            /// </summary>
            public int Cy { get => lParam.cy; }
            /// <summary>
            /// 新しいウィンドウの幅 (ピクセル単位)
            /// </summary>
            public int Cx { get => lParam.cx; }
            /// <summary>
            /// 新しいウィンドウの左上隅の y 座標
            /// </summary>
            public int Y { get => lParam.y; }
            /// <summary>
            /// 新しいウィンドウの左上隅の x 座標
            /// </summary>
            public int X { get => lParam.x; }
            /// <summary>
            /// 新しいウィンドウの名前
            /// </summary>
            public readonly string Name;
            /// <summary>
            /// 新しいウィンドウのクラス名
            /// </summary>
			public readonly string ClassName;
            /// <summary>
            /// 新しいウィンドウのスタイル
            /// </summary>
			public WINDOW_STYLE Style { get => (WINDOW_STYLE)lParam.style; }
            /// <summary>
            /// 新しいウィンドウの拡張ウィンドウ スタイル
            /// </summary>
			public EXTEND_WINDOW_STYLE ExtStyle {  get => (EXTEND_WINDOW_STYLE)lParam.dwExStyle; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="wParam">WParam</param>
            /// <param name="lParam">LParam</param>
            /// <remarks>
            /// WParam.. 未使用 
            /// LParam.. CREATESTRUCT構造体
            /// </remarks>
            public WM_CREATE(IntPtr wParam, IntPtr lParam) : base(wParam, lParam) 
			{
				if (this.lParam.lpszName != (IntPtr)0)
					Name = Marshal.PtrToStringAuto(this.lParam.lpszName);
				if (this.lParam.lpszClass != (IntPtr)0)
					ClassName = Marshal.PtrToStringAuto(this.lParam.lpszClass);
			}
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換された文字列</returns>
            public override string ToString()
            {
                return string.Format("CreateParams:0x{0:X16} Instance:0x{1:X16} Menu:0x{2:X16} Parent:0x{3:X16} X:{4},Y:{5} Cx:{6} Cy:{7} Style:{8}" +
                    " Name:{9} Class:{10} ExtStyle:{11}",
                    (ulong)CreateParams, (ulong)Instance, (ulong)Menu, (ulong)HwndParent,
                    X, Y, Cx, Cy, Style, Name, ClassName, ExtStyle);
            }
        }

    }
}
