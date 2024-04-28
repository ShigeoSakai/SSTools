using System;
using System.Collections.Generic;
using System.Reflection;

namespace SSTools
{
	public partial class WndMsgAnalysis
	{
		/// <summary>
		/// メッセージ解析I/F
		/// </summary>
		private interface IMakeAndCall
		{
			/// <summary>
			/// Windowメッセージ
			/// </summary>
			WND_MSG_ENUM MsgID { get; }
			/// <summary>
			/// 説明
			/// </summary>
			string Description { get; }
			/// <summary>
			/// 解析モード
			/// </summary>
			ANALYSIS_MODE AnalysisMode { get; }
			/// <summary>
			/// パラメータ型
			/// </summary>
			Type Type { get; }
			/// <summary>
			/// 関数定義があるか
			/// </summary>
			bool IsFunc { get; }
			/// <summary>
			/// 処理関数設定
			/// </summary>
			/// <typeparam name="T">パラメータ型</typeparam>
			/// <param name="func">処理関数</param>
			/// <returns>true:登録OK/false:型が違う</returns>
			bool SetFunc<T>(Func<IntPtr, WND_MSG_ENUM, T, string, bool> func) where T :class;
			/// <summary>
			/// 処理関数クリア
			/// </summary>
			void ClearFunc();
			/// <summary>
			/// パラメータの生成
			/// </summary>
			/// <param name="wparam">WParam</param>
			/// <param name="lparam">LParam</param>
			/// <returns>指定パラメータ型で生成した結果</returns>
			object MakeParameter(IntPtr wparam, IntPtr lparam);
			/// <summary>
			/// 処理関数呼び出し
			/// </summary>
			/// <param name="hdl">Windowハンドル</param>
			/// <param name="param">パラメータ</param>
			/// <param name="UseDescription">説明を表示するか？</param>
			/// <returns>呼び出し結果 true:このメッセージの処理済み/false:未処理</returns>
			bool Call(IntPtr hdl, object param, bool UseDescription);
		}
		/// <summary>
		/// Windowメッセージ定義
		/// </summary>
		/// <typeparam name="T">パラメータ型</typeparam>
		private class WindowsMessageDefine<T> :IMakeAndCall where T :class
		{
			/// <summary>
			/// Windowメッセージ
			/// </summary>
			public WND_MSG_ENUM MsgID { get; private set; }
			/// <summary>
			/// 説明
			/// </summary>
			public string Description { get; private set; }
			/// <summary>
			/// 解析モード
			/// </summary>
			public ANALYSIS_MODE AnalysisMode { get; private set; }
			/// <summary>
			/// 処理関数
			/// </summary>
			public Func<IntPtr, WND_MSG_ENUM, T, string, bool> Func { get; set; }
			/// <summary>
			/// 関数が登録されているか
			/// </summary>
			public bool IsFunc { get => (Func != null); }
			/// <summary>
			/// パラメータ型
			/// </summary>
			public Type Type { get => typeof(T); }

			/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="msgID">Windowメッセージ</param>
			/// <param name="analysisMode">解析モード</param>
			/// <param name="func">処理関数</param>
			/// <param name="description">説明</param>
			public WindowsMessageDefine(WND_MSG_ENUM msgID, ANALYSIS_MODE analysisMode,
				Func<IntPtr, WND_MSG_ENUM, T, string, bool> func = null, string description = null)
			{
				MsgID = msgID;
				Description = description;
				AnalysisMode = analysisMode;
				Func = func;
			}
			/// <summary>
			/// 処理関数登録
			/// </summary>
			/// <param name="func">処理関数</param>
			public void SetFunc(Func<IntPtr, WND_MSG_ENUM, T, string, bool> func) => Func = func;
			/// <summary>
			/// 処理関数クリア
			/// </summary>
			public void ClearFunc() => Func = null;
			/// <summary>
			/// パラメータの生成
			/// </summary>
			/// <param name="wparam">WParam</param>
			/// <param name="lparam">LParam</param>
			/// <returns>指定パラメータ型で生成した結果</returns>
			public object MakeParameter(IntPtr wParam,IntPtr lParam)
			{
				ConstructorInfo ctor = typeof(T).GetConstructor(new Type[] { typeof(IntPtr), typeof(IntPtr) });
				if (ctor != null)
					return ctor.Invoke(new object[] {wParam, lParam });
				return null;
			}
			/// <summary>
			/// 処理関数呼び出し
			/// </summary>
			/// <param name="hdl">Windowハンドル</param>
			/// <param name="param">パラメータ</param>
			/// <param name="UseDescription">説明を表示するか？</param>
			/// <returns>呼び出し結果 true:このメッセージの処理済み/false:未処理</returns>
			public bool Call(IntPtr hdl, object obj, bool UseDescription)
			{
				if (obj is T param)
					return Func(hdl, MsgID, param, (UseDescription) ? Description : null);
				return false;
			}
			/// <summary>
			/// 処理関数設定
			/// </summary>
			/// <typeparam name="T1">パラメータ型</typeparam>
			/// <param name="func">処理関数</param>
			/// <returns>true:登録OK/false:型が違う</returns>
			public bool SetFunc<T1>(Func<IntPtr, WND_MSG_ENUM, T1, string, bool> func) where T1 : class
			{
				if (Type == typeof(T1))
				{
					Func = (Func<IntPtr, WND_MSG_ENUM, T, string, bool>)func;
					return true;
				}
				return false;
			}
		}
		/// <summary>
		/// Windowメッセージ定義(Factory)
		/// </summary>
		private class WMFactory
		{
			/// <summary>
			/// Windowメッセージ定義
			/// </summary>
			public IMakeAndCall Define { get; private set; }
			/// <summary>
			/// Windowメッセージ定義インスタンスの生成
			/// </summary>
			/// <typeparam name="T">パラメータ型</typeparam>
			/// <param name="msgID">Windowメッセージ</param>
			/// <param name="analysisMode">解析モード</param>
			/// <param name="func">処理関数</param>
			/// <param name="description">説明</param>
			/// <returns>Windowメッセージ定義インスタンス</returns>
			public static WMFactory New<T>(WND_MSG_ENUM msgID, ANALYSIS_MODE analysisMode,
				Func<IntPtr, WND_MSG_ENUM, T, string, bool> func = null, string description = null) where T : class
				 => new WMFactory(new WindowsMessageDefine<T>(msgID, analysisMode, func, description));
			/// <summary>
			/// Windowメッセージ
			/// </summary>
			public WND_MSG_ENUM MsgID { get => (Define != null) ? Define.MsgID : WND_MSG_ENUM.WM_NULL; }
			/// <summary>
			/// 説明
			/// </summary>
			public string Description { get => Define?.Description; }
			/// <summary>
			/// 解析モード
			/// </summary>
			public ANALYSIS_MODE AnalysisMode { get => (Define != null) ? Define.AnalysisMode : ANALYSIS_MODE.NONE; }
			/// <summary>
			/// 関数が登録されているか
			/// </summary>
			public bool IsFunc { get => (Define != null) && Define.IsFunc; }
			/// <summary>
			/// パラメータ型
			/// </summary>
			public Type Type { get => Define?.Type; }

			/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="define">Windowメッセージ定義</param>
			private WMFactory(IMakeAndCall define) => Define = define;
			/// <summary>
			/// 処理関数呼び出し
			/// </summary>
			/// <param name="hdl">Windowハンドル</param>
			/// <param name="wParam">WParaam</param>
			/// <param name="lParam">WParaam</param>
			/// <param name="UseDescription">説明を表示するか？</param>
			/// <returns>呼び出し結果 true:このメッセージの処理済み/false:未処理 or 処理関数定義なし</returns>
			/// <remarks>
			/// 処理関数がtrueを返す .... WndProc()/HookProc()を呼び出さない(このメッセージは処理済み)
			/// 処理関数がfalseを返す、もしくはメッセージ処理未定義 ... WndProc()/HookProc()を呼び出す。
			/// </remarks>
			public bool Call(IntPtr hdl,IntPtr wParam,IntPtr lParam , bool UseDescription)
			{
				if ((Define != null) && (Define.IsFunc))
				{	// 処理関数定義はある時のみ
					object parameter = Define.MakeParameter(wParam, lParam);
					return Define.Call(hdl, parameter, UseDescription);
				}
				return false;
			}
			/// <summary>
			/// 処理関数の登録
			/// </summary>
			/// <typeparam name="T">引数の処理型</typeparam>
			/// <param name="func">処理関数</param>
			public bool SetFunc<T>(Func<IntPtr, WND_MSG_ENUM, T, string, bool> func) where T : class
			{
				if (Define != null)
					return Define.SetFunc<T>(func);
				return false;
			}
		}
		/// <summary>
		/// Windowメッセージ定義リスト
		/// </summary>
		private static List<WMFactory> windowsMessageDefines = new List<WMFactory>()
		{
			// Window関連
			// 00
			//  WM_NULL
			//  WM_CREATE
			WMFactory.New<PARAMETER_NOT_USE>(WND_MSG_ENUM.WM_DESTROY, ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction, "ウインドウが破棄されようとしている"),
			WMFactory.New<WPARAM_XY>(WND_MSG_ENUM.WM_MOVE, ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"ウィンドウが移動された"),
			WMFactory.New<WPARAM_SIZE>(WND_MSG_ENUM.WM_SIZE, ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"ウインドウのサイズが変更"),
			WMFactory.New<WM_ACTIVE>(WND_MSG_ENUM.WM_ACTIVATE, ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"アクティブ状態が変更"),
			WMFactory.New<WPARAMETER_IS_HANDLE>(WND_MSG_ENUM.WM_SETFOCUS, ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"ウインドウがキーボードフォーカスを取得した"),
			WMFactory.New<WPARAMETER_IS_HANDLE>(WND_MSG_ENUM.WM_KILLFOCUS, ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"ウインドウがキーボードフォーカスを失った"),
			//  WM_ENABLE
			//  WM_SETREDRAW
			//  WM_SETTEXT
			//  WM_GETTEXT
			//  WM_GETTEXTLENGTH
			WMFactory.New<PARAMETER_NOT_USE>(WND_MSG_ENUM.WM_PAINT, ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"ウインドウのクライアント領域を描画する必要があり"),

			// 10
			WMFactory.New<PARAMETER_NOT_USE>(WND_MSG_ENUM.WM_CLOSE,ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"コントロールメニューの[クローズ]コマンドが選択"),
			//  WM_QUERYENDSESSION
			//  WM_QUIT
			//  WM_QUERYOPEN
			WMFactory.New<WPARAMETER_IS_HANDLE>(WND_MSG_ENUM.WM_ERASEBKGND,ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"ウインドウの背景を消去する必要があり"),
			//  WM_SYSCOLORCHANGE
			//  WM_ENDSESSION
			//  WM_SYSTEMERROR
			WMFactory.New<WM_SHOWWINDOW>(WND_MSG_ENUM.WM_SHOWWINDOW,ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"ウインドウの表示または非表示の状態が変更"),
			//  WM_CTLCOLOR
			//  WM_WININICHANGE
			//  WM_DEVMODECHANGE = 0x001B,
			//  WM_ACTIVATEAPP = 0x001C,
			//  WM_FONTCHANGE = 0x001D,
			//  WM_TIMECHANGE = 0x001E,
			//  WM_CANCELMODE = 0x001F,
			// 20
			//  WM_SETCURSOR = 0x0020,
			//  WM_MOUSEACTIVATE = 0x0021,
			//  WM_CHILDACTIVATE = 0x0022,
			//  WM_QUEUESYNC = 0x0023,
			//  WM_GETMINMAXINFO = 0x0024,
			//  WM_PAINTICON = 0x0026,
			//  WM_ICONERASEBKGND = 0x0027,
			//  WM_NEXTDLGCTL = 0x0028,
			//  WM_SPOOLERSTATUS = 0x002A,
			//  WM_DRAWITEM = 0x002B,
			//  WM_MEASUREITEM = 0x002C,
			//  WM_DELETEITEM = 0x002D,
			//  WM_VKEYTOITEM = 0x002E,
			//  WM_CHARTOITEM = 0x002F,
			// 30
			WMFactory.New<WM_DEFAULT>(WND_MSG_ENUM.WM_SETFONT,ANALYSIS_MODE.WINDOW_MSG | ANALYSIS_MODE.CONTROL_MSG, DefaultDebugFunction,"コントロールで使われるフォントを設定します。"),
			//  WM_GETFONT = 0x0031,
			//  WM_SETHOTKEY = 0x0032,
			//  WM_GETHOTKEY = 0x0033,
			//  WM_QUERYDRAGICON = 0x0037,
			//  WM_COMPAREITEM = 0x0039,
			//  WM_GETOBJECT = 0x003D,
			// 40
			//  WM_COMPACTING = 0x0041,
			//  WM_COMMNOTIFY = 0x0044,
			WMFactory.New<ONLY_LPARAM<WINDOWPOS_PARAMS>>(WND_MSG_ENUM.WM_WINDOWPOSCHANGING,ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"ウインドウに新しいサイズまたは位置を通知"),
			WMFactory.New<ONLY_LPARAM<WINDOWPOS_PARAMS>>(WND_MSG_ENUM.WM_WINDOWPOSCHANGED,ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"ウインドウにサイズまたは位置の変更を通知"),
			//  WM_POWER = 0x0048,
			//  WM_COPYDATA = 0x004A,
			//  WM_CANCELJOURNAL = 0x004B,
			WMFactory.New<ONLY_LPARAM<NMHDR>>(WND_MSG_ENUM.WM_NOTIFY,ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"イベントが発生、またはコントロールに何らかの情報が必要"),
			// 50
			//  WM_INPUTLANGCHANGEREQUEST = 0x0050,
			//  WM_INPUTLANGCHANGE = 0x0051,
			//  WM_TCARD = 0x0052,
			//  WM_HELP = 0x0053,
			//  WM_USERCHANGED = 0x0054,
			WMFactory.New<WM_NOTIFY_FORMAT>(WND_MSG_ENUM.WM_NOTIFYFORMAT,ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"ANSIまたはUnicodeの構造体を受け入れるかどうかを決定"),
			// 70
			//  WM_CONTEXTMENU = 0x007B,
			//  WM_STYLECHANGING = 0x007C,
			//  WM_STYLECHANGED = 0x007D,
			//  WM_DISPLAYCHANGE = 0x007E,
			WMFactory.New<WM_GETICON>(WND_MSG_ENUM.WM_GETICON,ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"ウィンドウに関連付けられているアイコンを取得"),
			// 80
			//  WM_SETICON = 0x0080,
			//  WM_NCCREATE = 0x0081,
			WMFactory.New<PARAMETER_NOT_USE>(WND_MSG_ENUM.WM_NCDESTROY,ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"ウインドウの非クライアント領域が破棄されている"),
			//WMFactory.New<ONLY_LPARAM<NCCALCSIZE_PARAMS>>(WND_MSG_ENUM.WM_NCCALCSIZE,ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"ウインドウのクライアント領域のサイズを計算"),
			WMFactory.New<ONLY_LPARAM_CLASS<NCCALCSIZE_PARAMS_CLASS>>(WND_MSG_ENUM.WM_NCCALCSIZE,ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"ウインドウのクライアント領域のサイズを計算"),
			WMFactory.New<WPARAMETER_IS_HANDLE>(WND_MSG_ENUM.WM_NCPAINT,ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"ウインドウの枠を描画する必要があり"),
			WMFactory.New<WM_DEFAULT>(WND_MSG_ENUM.WM_NCACTIVATE,ANALYSIS_MODE.WINDOW_MSG, DebugNcActivate,"非クライアント領域のアクティブ状態を変更"),
			//  WM_GETDLGCODE = 0x0087,
			//  WM_SYNCPAINT = 0x88,
			// F0
			//	WM_INPUT_DEVICE_CHANGE = 0x00FE,
			//	WM_INPUT = 0x00FF,
			// 100
			//	WM_KEYDOWN = 0x0100,
			//	WM_KEYUP = 0x0101,
			//	WM_CHAR = 0x0102,
			//	WM_DEADCHAR = 0x0103,
			//	WM_SYSKEYDOWN = 0x0104,
			//	WM_SYSKEYUP = 0x0105,
			//	WM_SYSCHAR = 0x0106,
			//	WM_SYSDEADCHAR = 0x0107,
			//	WM_UNICHAR = 0x0109,
			//	WM_IME_STARTCOMPOSITION = 0x010D,
			//	WM_IME_ENDCOMPOSITION = 0x010E,
			//	WM_IME_COMPOSITION = 0x010F,
			// 110 
			WMFactory.New<WM_DEFAULT>(WND_MSG_ENUM.WM_INITDIALOG,ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"ダイアログボックスを初期化"),
			WMFactory.New<WM_COMMAND>(WND_MSG_ENUM.WM_COMMAND,ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"コマンドメッセージを指定"),
			WMFactory.New<WM_SYSCOMMAND>(WND_MSG_ENUM.WM_SYSCOMMAND,ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"システムコマンドを要求"),
			//	WM_TIMER = 0x0113,
			//	WM_HSCROLL = 0x0114,
			//	WM_VSCROLL = 0x0115,
			//	WM_INITMENU = 0x0116,
			//	WM_INITMENUPOPUP = 0x0117,
			//	WM_GESTURE = 0x0119,
			//	WM_GESTURENOTIFY = 0x011A,
			//	WM_MENUSELECT = 0x011F,
			// 120
			//	WM_MENUCHAR = 0x0120,
			//	WM_ENTERIDLE = 0x0121,
			//	WM_MENURBUTTONUP = 0x0122,
			//	WM_MENUDRAG = 0x0123,
			//	WM_MENUGETOBJECT = 0x0124,
			//	WM_UNINITMENUPOPUP = 0x0125,
			//	WM_MENUCOMMAND = 0x0126,
			WMFactory.New<WM_UISTATE>(WND_MSG_ENUM.WM_CHANGEUISTATE,ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"UI の状態を変更する必要がある"),
			WMFactory.New<WM_UISTATE>(WND_MSG_ENUM.WM_UPDATEUISTATE,ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"指定したウィンドウとそのすべての子ウィンドウの UI 状態を変更"),
			WMFactory.New<PARAMETER_NOT_USE>(WND_MSG_ENUM.WM_QUERYUISTATE,ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"ウィンドウの UI 状態を取得"),
			WMFactory.New<ONLY_LPARAM<RECT>>(WND_MSG_ENUM.WM_MOVING,ANALYSIS_MODE.WINDOW_MSG, DefaultDebugFunction,"移動しているウィンドウに送信。ドラッグ四角形の位置を監視し、必要に応じてその位置を変更"),
			
			// マウス関連
			// 20
			WMFactory.New<WM_SETCURSOR>(WND_MSG_ENUM.WM_SETCURSOR,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"マウス カーソルの形状を設定"),
			// 80
			WMFactory.New<WPARAM_XY>(WND_MSG_ENUM.WM_NCHITTEST,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"マウス座標でのヒットテスト"),
			// A0
			WMFactory.New<WM_NCMOUSE>(WND_MSG_ENUM.WM_NCMOUSEMOVE,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"非クライアント領域でマウス カーソルが移動"),
			WMFactory.New<WM_NCMOUSE>(WND_MSG_ENUM.WM_NCLBUTTONDOWN,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"非クライアント領域でマウスの左ボタンが押された"),
			WMFactory.New<WM_NCMOUSE>(WND_MSG_ENUM.WM_NCLBUTTONUP,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"非クライアント領域でマウスの左ボタンが離された"),
			WMFactory.New<WM_NCMOUSE>(WND_MSG_ENUM.WM_NCLBUTTONDBLCLK,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"非クライアント領域でマウスの左ボタンをダブルクリック"),
			WMFactory.New<WM_NCMOUSE>(WND_MSG_ENUM.WM_NCRBUTTONDOWN,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"非クライアント領域でマウスの右ボタンが押された"),
			WMFactory.New<WM_NCMOUSE>(WND_MSG_ENUM.WM_NCRBUTTONUP,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"非クライアント領域でマウスの右ボタンが離された"),
			WMFactory.New<WM_NCMOUSE>(WND_MSG_ENUM.WM_NCRBUTTONDBLCLK,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"非クライアント領域でマウスの右ボタンをダブルクリック"),
			WMFactory.New<WM_NCMOUSE>(WND_MSG_ENUM.WM_NCMBUTTONDOWN,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"非クライアント領域でマウスの中央ボタンが押された"),
			WMFactory.New<WM_NCMOUSE>(WND_MSG_ENUM.WM_NCMBUTTONUP,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"非クライアント領域でマウスの中央ボタンが離された"),
			WMFactory.New<WM_NCMOUSE>(WND_MSG_ENUM.WM_NCMBUTTONDBLCLK,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"非クライアント領域でマウスの中央ボタンをダブルクリック"),
			WMFactory.New<WM_NCMOUSE>(WND_MSG_ENUM.WM_NCXBUTTONDOWN,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"非クライアント領域でマウスの4つ目以降のボタンが押された"),
			WMFactory.New<WM_NCMOUSE>(WND_MSG_ENUM.WM_NCXBUTTONUP,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"非クライアント領域でマウスの4つ目以降のボタンが離された"),
			WMFactory.New<WM_NCMOUSE>(WND_MSG_ENUM.WM_NCXBUTTONDBLCLK,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"非クライアント領域でマウスの4つ目以降のボタンをダブルクリック"),
			// 200
			WMFactory.New<WM_MOUSE>(WND_MSG_ENUM.WM_MOUSEMOVE,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"マウスカーソルが移動"),
			WMFactory.New<WM_MOUSE>(WND_MSG_ENUM.WM_LBUTTONDOWN,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"マウスの左ボタンが押された"),
			WMFactory.New<WM_MOUSE>(WND_MSG_ENUM.WM_LBUTTONUP,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"マウスの左ボタンが離された"),
			WMFactory.New<WM_MOUSE>(WND_MSG_ENUM.WM_LBUTTONDBLCLK,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"マウスの左ボタンをダブルクリック"),
			WMFactory.New<WM_MOUSE>(WND_MSG_ENUM.WM_RBUTTONDOWN,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"マウスの右ボタンが押された"),
			WMFactory.New<WM_MOUSE>(WND_MSG_ENUM.WM_RBUTTONUP,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"マウスの右ボタンが離された"),
			WMFactory.New<WM_MOUSE>(WND_MSG_ENUM.WM_RBUTTONDBLCLK,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"マウスの右ボタンをダブルクリック"),
			WMFactory.New<WM_MOUSE>(WND_MSG_ENUM.WM_MBUTTONDOWN,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"マウスの中央ボタンが押された"),
			WMFactory.New<WM_MOUSE>(WND_MSG_ENUM.WM_MBUTTONUP,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"マウスの中央ボタンが離された"),
			WMFactory.New<WM_MOUSE>(WND_MSG_ENUM.WM_MBUTTONDBLCLK,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"マウスの中央ボタンをダブルクリック"),
			WMFactory.New<WM_MOUSE_WHEEL>(WND_MSG_ENUM.WM_MOUSEWHEEL,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"マウスホイールが回転"),
			WMFactory.New<WM_X_MOUSE>(WND_MSG_ENUM.WM_XBUTTONDOWN,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"マウスの4つ目以降のボタンが押されたか"),
			WMFactory.New<WM_X_MOUSE>(WND_MSG_ENUM.WM_XBUTTONUP,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"マウスの4つ目以降のボタンが離された"),
			WMFactory.New<WM_X_MOUSE>(WND_MSG_ENUM.WM_XBUTTONDBLCLK,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"マウスの4つ目以降のボタンをダブルクリック"),
			WMFactory.New<WM_MOUSE_WHEEL>(WND_MSG_ENUM.WM_MOUSEHWHEEL,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"マウス水平ホイールが回転した"),
			// 210
			WMFactory.New<LPARAMETER_IS_HANDLE>(WND_MSG_ENUM.WM_CAPTURECHANGED,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"マウスキャプチャを失うウィンドウに送信"),
			// 2A0
			WMFactory.New<WM_NCMOUSE>(WND_MSG_ENUM.WM_NCMOUSEHOVER,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"非クライアントリア領域上でホバリング"),
			WMFactory.New<WM_MOUSE>(WND_MSG_ENUM.WM_MOUSEHOVER,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"マウスがウインドウのクライアントエリア上でホバリング"),
			WMFactory.New<PARAMETER_NOT_USE>(WND_MSG_ENUM.WM_NCMOUSELEAVE,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"非クライアント領域を離れた。"),
			WMFactory.New<PARAMETER_NOT_USE>(WND_MSG_ENUM.WM_MOUSELEAVE,ANALYSIS_MODE.MOUSE_MSG,DefaultDebugFunction,"マウスがウインドウのクライアントエリアを離れた。"),

			// Control関連 
			WMFactory.New<WM_HANDLES>(WND_MSG_ENUM.WM_CTLCOLORMSGBOX,ANALYSIS_MODE.CONTROL_MSG,DefaultDebugFunction,"メッセージ ボックスを描画"),
			WMFactory.New<WM_HANDLES>(WND_MSG_ENUM.WM_CTLCOLOREDIT,ANALYSIS_MODE.CONTROL_MSG,DefaultDebugFunction,"エディットコントロールを描画"),
			WMFactory.New<WM_HANDLES>(WND_MSG_ENUM.WM_CTLCOLORLISTBOX,ANALYSIS_MODE.CONTROL_MSG,DefaultDebugFunction,"リストボックスを描画"),
			WMFactory.New<WM_HANDLES>(WND_MSG_ENUM.WM_CTLCOLORBTN,ANALYSIS_MODE.CONTROL_MSG,DefaultDebugFunction,"ボタンを描画"),
			WMFactory.New<WM_HANDLES>(WND_MSG_ENUM.WM_CTLCOLORDLG,ANALYSIS_MODE.CONTROL_MSG,DefaultDebugFunction,"ダイアログボックスを描画"),
			WMFactory.New<WM_HANDLES>(WND_MSG_ENUM.WM_CTLCOLORSCROLLBAR,ANALYSIS_MODE.CONTROL_MSG,DefaultDebugFunction,"スクロールバーを描画"),
			WMFactory.New<WM_HANDLES>(WND_MSG_ENUM.WM_CTLCOLORSTATIC,ANALYSIS_MODE.CONTROL_MSG,DefaultDebugFunction,"スタティックコントロールを描画"),
			// DWM(ウィンドウマネージャ)関連
			WMFactory.New<PARAMETER_NOT_USE>(WND_MSG_ENUM.WM_DWMCOMPOSITIONCHANGED,ANALYSIS_MODE.WINDOW_MGR_MSG,DefaultDebugFunction,"DWMコンポジションが有効または無効の通知"),
			WMFactory.New<WPARAM_IS_BOOL>(WND_MSG_ENUM.WM_DWMNCRENDERINGCHANGED,ANALYSIS_MODE.WINDOW_MGR_MSG,DefaultDebugFunction,"クライアント領域以外のレンダリングポリシーの変更通知"),
			WMFactory.New<WM_COLOR_CHANGE>(WND_MSG_ENUM.WM_DWMCOLORIZATIONCOLORCHANGED,ANALYSIS_MODE.WINDOW_MGR_MSG,DefaultDebugFunction,"色分け色が変更通知"),
			WMFactory.New<WPARAM_IS_BOOL>(WND_MSG_ENUM.WM_DWMWINDOWMAXIMIZEDCHANGE,ANALYSIS_MODE.WINDOW_MGR_MSG,DefaultDebugFunction,"DWM構成ウィンドウが最大化"),
			WMFactory.New<PARAMETER_NOT_USE>(WND_MSG_ENUM.WM_DWMSENDICONICTHUMBNAIL,ANALYSIS_MODE.WINDOW_MGR_MSG,DefaultDebugFunction,"ウィンドウのサムネイルとして使用する静的ビットマップを要求"),
			WMFactory.New<PARAMETER_NOT_USE>(WND_MSG_ENUM.WM_DWMSENDICONICLIVEPREVIEWBITMAP,ANALYSIS_MODE.WINDOW_MGR_MSG,DefaultDebugFunction,"ウィンドウのライブプレビューとして使用する静的ビットマップを要求"),

            // IME関連
			WMFactory.New<IME_SETCONTEXT>(WND_MSG_ENUM.WM_IME_SETCONTEXT,ANALYSIS_MODE.IME_MSG,DefaultDebugFunction,"ウィンドウがアクティブ化された"),
			WMFactory.New<IME_NOTIFY>(WND_MSG_ENUM.WM_IME_NOTIFY,ANALYSIS_MODE.IME_MSG,DefaultDebugFunction,"IMEウィンドウへの変更を通知"),
			// その他
			WMFactory.New<WM_PRINT_CLIENT>(WND_MSG_ENUM.WM_PRINTCLIENT,ANALYSIS_MODE.OTHER_MSG,DefaultDebugFunction,"指定されたデバイスコンテキスト(プリンタ)でクライアント領域の描画要求"),

		};
		/// <summary>
		/// デフォルト処理関数
		/// </summary>
		/// <typeparam name="T">パラメータ型</typeparam>
		/// <param name="hWnd">Windowハンドル</param>
		/// <param name="msgID">Windowメッセージ</param>
		/// <param name="param">パラメータ</param>
		/// <param name="description">説明</param>
		/// <returns>常にfalse</returns>
		private static bool DefaultDebugFunction<T>(IntPtr hWnd, WND_MSG_ENUM msgID, T param, string description) where T : class
		{
			Console.WriteLine("{0}(0x{1:X4}) {2} :{3}", msgID.ToString(), (ushort)msgID , param.ToString(), description);
			return false;
		}
		/// <summary>
		/// NcActivate用処理関数
		/// </summary>
		/// <param name="hWnd">Windowハンドル</param>
		/// <param name="msgID">Windowメッセージ</param>
		/// <param name="param">パラメータ</param>
		/// <param name="description">説明</param>
		/// <returns>常にfalse</returns>
		private static bool DebugNcActivate(IntPtr hWnd, WND_MSG_ENUM msgID, WPARAM_LPARAM_IS_VALUE<WPRAM_LPARAM> param, string description)
		{
			Console.WriteLine("{0}(0x{1:X4}):{2} Handle:0x{3:X8}", msgID.ToString(), (ushort)msgID, (param.Param.W.ValuePtr != (IntPtr)0), param.Param.L.ValuePtr);
			return false;
		}
	}
}
