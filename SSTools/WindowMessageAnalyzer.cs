﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTools
{
	public class WindowMessageAnalyzer
	{
		private class WndProcMsg
		{
			public int ID { get; private set; }
			public string Message { get; private set; }
			public string Description { get; private set; }
			public string WParam { get; private set; }
			public string LParam { get; private set; }
			public string Return { get; private set; }
			public WndProcMsg(int id, string message, string description, string w_param, string l_param, string ret)
			{
				ID = id;
				Message = message;
				Description = description;
				WParam = w_param;
				LParam = l_param;
				Return = ret; ;
			}
		}
		private static readonly List<WndProcMsg> wndProcMsgs = new List<WndProcMsg>()
		{
			new WndProcMsg(0x0000,"WM_NULL","特に意味はありません。特定のウインドウにこのメッセージを投げてタイムアウトするかどうかで生存確認を行う事ができます。","","",""),
			new WndProcMsg(0x0001,"WM_CREATE","ウインドウが作成されていることを示します。","","",""),
			new WndProcMsg(0x0002,"WM_DESTROY","ウインドウが破棄されようとしていることを示します。","","",""),
			new WndProcMsg(0x0003,"WM_MOVE","ウインドウの位置が変更されたことを示します。","","",""),
			new WndProcMsg(0x0005,"WM_SIZE","ウインドウのサイズが変更されていることを示します。","","",""),
			new WndProcMsg(0x0006,"WM_ACTIVATE","アクティブ状態が変更されていることを示します。","","",""),
			new WndProcMsg(0x0007,"WM_SETFOCUS","ウインドウがキーボード・フォーカスを取得したことを示します。","","",""),
			new WndProcMsg(0x0008,"WM_KILLFOCUS","ウインドウがキーボード・フォーカスを失っていることを示します。","","",""),
			new WndProcMsg(0x000A,"WM_ENABLE","ウインドウの有効または無効の状態が変更されていることを示します。","","",""),
			new WndProcMsg(0x000B,"WM_SETREDRAW","ウインドウ内の再描画を許可または禁止します。","","",""),
			new WndProcMsg(0x000C,"WM_SETTEXT","ウインドウのテキストを設定します。","","",""),
			new WndProcMsg(0x000D,"WM_GETTEXT","ウインドウに対応するテキストを取得します。","","",""),
			new WndProcMsg(0x000E,"WM_GETTEXTLENGTH","ウインドウに関連付けられているテキストの長さを取得します。","","",""),
			new WndProcMsg(0x000F,"WM_PAINT","ウインドウのクライアント領域を描画する必要があることを示します。","","",""),
			new WndProcMsg(0x0010,"WM_CLOSE","コントロール・メニューの[クローズ]コマンドが選ばれました。","","",""),
			new WndProcMsg(0x0011,"WM_QUERYENDSESSION","Windowsセッションを終了するよう要求します。","","",""),
			new WndProcMsg(0x0012,"WM_QUIT","アプリケーションを強制終了するよう要求します。","","",""),
			new WndProcMsg(0x0013,"WM_QUERYOPEN","アイコン化ウインドウを復元するよう要求します。","","",""),
			new WndProcMsg(0x0014,"WM_ERASEBKGND","ウインドウの背景を消去する必要があることを示します。","","",""),
			new WndProcMsg(0x0015,"WM_SYSCOLORCHANGE","システム・カラーの値が変更されたことを示します。","","",""),
			new WndProcMsg(0x0016,"WM_ENDSESSION","Windowsセッションが終了することを示します。","","",""),
			new WndProcMsg(0x0017,"WM_SYSTEMERROR","(Win32ではもはや用いられません)","","",""),
			new WndProcMsg(0x0018,"WM_SHOWWINDOW","ウインドウの表示または非表示の状態が変更されようとしていることを示します。","","",""),
			new WndProcMsg(0x0019,"WM_CTLCOLOR","子コントロールが描画される直前であることを示します。","","",""),
			new WndProcMsg(0x001A,"WM_WININICHANGE","WIN.INIが変更されたことをアプリケーションに通知します。Windowsの設定が変更されたことをアプリケーションに通知します。","","",""),
			new WndProcMsg(0x001B,"WM_DEVMODECHANGE","デバイス モードの設定が変更されたことを示します。","","",""),
			new WndProcMsg(0x001C,"WM_ACTIVATEAPP","新しいタスクがアクティブになるタイミングをアプリケーションに通知します。","","",""),
			new WndProcMsg(0x001D,"WM_FONTCHANGE","フォント リソース プールが変更されていることを示します。","","",""),
			new WndProcMsg(0x001E,"WM_TIMECHANGE","システム時刻が設定されたことを示します。","","",""),
			new WndProcMsg(0x001F,"WM_CANCELMODE","内部モードをキャンセルするようウインドウに通知します。","","",""),
			new WndProcMsg(0x0020,"WM_SETCURSOR","マウス カーソルの形状を設定するようウインドウに促します。","","",""),
			new WndProcMsg(0x0021,"WM_MOUSEACTIVATE","非アクティブ ウインドウ内でマウスがクリックされたことを示します。","","",""),
			new WndProcMsg(0x0022,"WM_CHILDACTIVATE","子ウインドウにアクティブであることを通知します。","","",""),
			new WndProcMsg(0x0023,"WM_QUEUESYNC","CBTメッセージを区切ります。","","",""),
			new WndProcMsg(0x0024,"WM_GETMINMAXINFO","アイコン表示時および最大表示時のサイズ情報を取得します。","","",""),
			new WndProcMsg(0x0026,"WM_PAINTICON","アイコンが描画されようとしています。","","",""),
			new WndProcMsg(0x0027,"WM_ICONERASEBKGND","アイコンの背景を塗りつぶすようアイコン化ウインドウに通知します。","","",""),
			new WndProcMsg(0x0028,"WM_NEXTDLGCTL","フォーカスを別のダイアログ ボックス コントロールに設定します。","","",""),
			new WndProcMsg(0x002A,"WM_SPOOLERSTATUS","印刷ジョブが追加または削除されたことを示します。(XP 以降ではサポートされません)","","",""),
			new WndProcMsg(0x002B,"WM_DRAWITEM","オーナー描画コントロールまたはオーナー描画メニューを再描画する必要があることを示します。","","",""),
			new WndProcMsg(0x002C,"WM_MEASUREITEM","オーナー描画のコントロールまたは項目の寸法を要求します。","","",""),
			new WndProcMsg(0x002D,"WM_DELETEITEM","ほかのオーナー描画項目またはオーナー描画コントロールに代わったことを示します。","","",""),
			new WndProcMsg(0x002E,"WM_VKEYTOITEM","リスト ボックスのキーストロークをそのオーナー ウインドウに提供します。","","",""),
			new WndProcMsg(0x002F,"WM_CHARTOITEM","リスト ボックスのキーストロークをそのオーナー ウインドウに提供します。","","",""),
			new WndProcMsg(0x0030,"WM_SETFONT","コントロールで使われるフォントを設定します。","","",""),
			new WndProcMsg(0x0031,"WM_GETFONT","コントロールで使われているフォントを取得します。","","",""),
			new WndProcMsg(0x0032,"WM_SETHOTKEY","ウインドウにホット キーを関連付けます。","","",""),
			new WndProcMsg(0x0033,"WM_GETHOTKEY","ウインドウのホット キーの仮想キー コードを取得します。","","",""),
			new WndProcMsg(0x0037,"WM_QUERYDRAGICON","アイコン化ウインドウに対してマウス カーソルのハンドルを要求します。","","",""),
			new WndProcMsg(0x0039,"WM_COMPAREITEM","コンボ ボックスまたはリスト ボックスの項目位置を判断します。","","",""),
			new WndProcMsg(0x003D,"WM_GETOBJECT","Active Accessibility は、WM_GETOBJECT メッセージを送信して、サーバー アプリケーションに含まれるアクセシビリティ対応オブジェクトに関する情報を取得します。アプリケーションがこのメッセージを直接送信することはありません。これは、 AccessibleObjectFromPoint、AccessibleObjectFromEvent、またはAccessibleObjectFromWindowへの呼び出しに応答して Active Accessibility によってのみ送信されます。ただし、サーバー アプリケーションはこのメッセージを処理します。","","",""),
			new WndProcMsg(0x0041,"WM_COMPACTING","メモリ不足状態であることを示します。","","",""),
			new WndProcMsg(0x0044,"WM_COMMNOTIFY","(Win32 ではもはや用いられません)","","",""),
			new WndProcMsg(0x0046,"WM_WINDOWPOSCHANGING","ウインドウに新しいサイズまたは位置を通知します。","","",""),
			new WndProcMsg(0x0047,"WM_WINDOWPOSCHANGED","ウインドウにサイズまたは位置の変更を通知します。","","",""),
			new WndProcMsg(0x0048,"WM_POWER","システムが中断モードに入っていることを示します。","","",""),
			new WndProcMsg(0x004A,"WM_COPYDATA","ほかのアプリケーションにデータを渡します。","","",""),
			new WndProcMsg(0x004B,"WM_CANCELJOURNAL","ユーザーがジャーナル モードをキャンセルしました。","","",""),
			new WndProcMsg(0x004E,"WM_NOTIFY","イベントが発生したとき、またはコントロールが何らかの情報を必要としたときに、共通コントロールによってその親ウィンドウに送信されます。","","",""),
			new WndProcMsg(0x0050,"WM_INPUTLANGCHANGEREQUEST","WM_INPUTLANGCHANGEREQUEST メッセージは、ユーザーがホットキー (キーボード コントロール パネル アプリケーションで指定) またはシステム タスク バーのインジケーターを使用して新しい入力言語を選択すると、フォーカスのあるウィンドウにポストされます。アプリケーションは、メッセージをDefWindowProc関数に渡すことで変更を受け入れるか、すぐに戻ることで変更を拒否 (および変更の発生を阻止) できます。","","",""),
			new WndProcMsg(0x0051,"WM_INPUTLANGCHANGE","WM_INPUTLANGCHANGE メッセージは、アプリケーションの入力言語が変更された後、影響を受ける最上位のウィンドウに送信されます。アプリケーション固有の設定を行って、メッセージをDefWindowProc関数に渡す必要があります。この関数は、メッセージをすべての第 1 レベルの子ウィンドウに渡します。これらの子ウィンドウは、メッセージをDefWindowProcに渡して、子ウィンドウにメッセージを渡すようにすることができます。","","",""),
			new WndProcMsg(0x0052,"WM_TCARD","Microsoft Windows ヘルプを使用してトレーニング カードを開始したアプリケーションに送信されます。ユーザーが承認可能なボタンをクリックすると、メッセージによってアプリケーションに通知されます。アプリケーションは、 WinHelp関数の呼び出しで HELP_TCARD コマンドを指定することにより、トレーニング カードを開始します。","","",""),
			new WndProcMsg(0x0053,"WM_HELP","ユーザーが F1 キーを押したことを示します。 F1 を押したときにメニューがアクティブな場合、WM_HELP がそのメニューに関連付けられたウィンドウに送信されます。それ以外の場合は、WM_HELP がキーボード フォーカスのあるウィンドウに送信されます。キーボード フォーカスを持つウィンドウがない場合は、WM_HELP が現在アクティブなウィンドウに送信されます。","","",""),
			new WndProcMsg(0x0054,"WM_USERCHANGED","ユーザがログオン/ログオフしたことを示します。","","",""),
			new WndProcMsg(0x0055,"WM_NOTIFYFORMAT","ウィンドウが WM_NOTIFY 通知メッセージで ANSI 構造または Unicode 構造を受け入れるかどうかを決定します。 WM_NOTIFYFORMAT メッセージは、コモン コントロールからその親ウィンドウに、また親ウィンドウからコモン コントロールに送信されます。","","",""),
			new WndProcMsg(0x007B,"WM_CONTEXTMENU","WM_CONTEXTMENU メッセージは、ユーザーがウィンドウ内でマウスの右ボタンをクリックした (右クリックした) ことをウィンドウに通知します。","","",""),
			new WndProcMsg(0x007C,"WM_STYLECHANGING","SetWindowLong() によってウインドウのスタイルが変更されようとしています。","","",""),
			new WndProcMsg(0x007D,"WM_STYLECHANGED","SetWindowLong() によってウインドウのスタイルが変更されました。","","",""),
			new WndProcMsg(0x007E,"WM_DISPLAYCHANGE","ディスプレイの解像度が変更されたことを示します。","","",""),
			new WndProcMsg(0x007F,"WM_GETICON","WM_GETICON メッセージは、ウィンドウに関連付けられた大きいアイコンまたは小さいアイコンへのハンドルを取得するためにウィンドウに送信されます。 ALT+TAB ダイアログには大きなアイコンが表示され、ウィンドウのキャプションには小さなアイコンが表示されます。","","",""),
			new WndProcMsg(0x0080,"WM_SETICON","アプリケーションは WM_SETICON メッセージを送信して、新しい大きいアイコンまたは小さいアイコンをウィンドウに関連付けます。 ALT+TAB ダイアログ ボックスには大きなアイコンが表示され、ウィンドウ キャプションには小さなアイコンが表示されます。","","",""),
			new WndProcMsg(0x0081,"WM_NCCREATE","ウインドウの非クライアント領域が作成されていることを示します。","","",""),
			new WndProcMsg(0x0082,"WM_NCDESTROY","ウインドウの非クライアント領域が破棄されていることを示します。","","",""),
			new WndProcMsg(0x0083,"WM_NCCALCSIZE","ウインドウのクライアント領域のサイズを計算します。","","",""),
			new WndProcMsg(0x0084,"WM_NCHITTEST","マウス カーソルが移動したことを示します。","","",""),
			new WndProcMsg(0x0085,"WM_NCPAINT","ウインドウの枠を描画する必要があることを示します。","","",""),
			new WndProcMsg(0x0086,"WM_NCACTIVATE","非クライアント領域のアクティブ状態を変更します。","","",""),
			new WndProcMsg(0x0087,"WM_GETDLGCODE","ダイアログ プロシージャがコントロール入力を処理できるようにします。","","",""),
			new WndProcMsg(0x0088,"WM_SYNCPAINT","WM_SYNCPAINT メッセージは、独立した GUI スレッドのリンクを回避しながらペイントを同期するために使用されます。","","",""),
			new WndProcMsg(0x00A0,"WM_NCMOUSEMOVE","非クライアント領域でマウス カーソルが移動したことを示します。","","",""),
			new WndProcMsg(0x00A1,"WM_NCLBUTTONDOWN","非クライアント領域でマウスの左ボタンが押されたことを示します。","","",""),
			new WndProcMsg(0x00A2,"WM_NCLBUTTONUP","非クライアント領域でマウスの左ボタンが離されたことを示します。","","",""),
			new WndProcMsg(0x00A3,"WM_NCLBUTTONDBLCLK","非クライアント領域でマウスの左ボタンをダブルクリックしたことを示します。","","",""),
			new WndProcMsg(0x00A4,"WM_NCRBUTTONDOWN","非クライアント領域でマウスの右ボタンが押されたことを示します。","","",""),
			new WndProcMsg(0x00A5,"WM_NCRBUTTONUP","非クライアント領域でマウスの右ボタンが離されたことを示します。","","",""),
			new WndProcMsg(0x00A6,"WM_NCRBUTTONDBLCLK","非クライアント領域でマウスの右ボタンをダブルクリックしたことを示します。","","",""),
			new WndProcMsg(0x00A7,"WM_NCMBUTTONDOWN","非クライアント領域でマウスの中央ボタンが押されたことを示します。","","",""),
			new WndProcMsg(0x00A8,"WM_NCMBUTTONUP","非クライアント領域でマウスの中央ボタンが離されたことを示します。","","",""),
			new WndProcMsg(0x00A9,"WM_NCMBUTTONDBLCLK","非クライアント領域でマウスの中央ボタンをダブルクリックしたことを示します。","","",""),
			new WndProcMsg(0x00AB,"WM_NCXBUTTONDOWN","非クライアント領域でマウスの 4 つ目以降のボタンが押されたことを示します。","","",""),
			new WndProcMsg(0x00AC,"WM_NCXBUTTONUP","非クライアント領域でマウスの 4 つ目以降のボタンが離されたことを示します。","","",""),
			new WndProcMsg(0x00AD,"WM_NCXBUTTONDBLCLK","非クライアント領域でマウスの 4 つ目以降のボタンをダブルクリックしたことを示します。","","",""),
			new WndProcMsg(0x00AE,"WM_NCUAHDRAWCAPTION","WM_NCUAHDRAWCAPTION メッセージは、テーマに関連する文書化されていないメッセージです。 WM_NCPAINT を処理するときは、このメッセージも処理する必要があります。","","",""),
			new WndProcMsg(0x00AF,"WM_NCUAHDRAWFRAME","WM_NCUAHDRAWFRAME メッセージは、テーマに関連する文書化されていないメッセージです。 WM_NCPAINT を処理するときは、このメッセージも処理する必要があります。","","",""),
			new WndProcMsg(0x00FE,"WM_INPUT_DEVICE_CHANGE","","","",""),
			new WndProcMsg(0x00FF,"WM_INPUT","RAW Input Device (キーボード/マウス/リモコン等) からの入力があったことを示します。","","",""),
			new WndProcMsg(0x0100,"WM_KEYFIRST","WM_KEYDOWN メッセージは、非システム キーが押されたときに、キーボード フォーカスのあるウィンドウにポストされます。非システム キーは、ALT キーが押されていないときに押されるキーです。","","",""),
			new WndProcMsg(0x0100,"WM_KEYDOWN","非システム キーが押されたことを示します。","","",""),
			new WndProcMsg(0x0101,"WM_KEYUP","非システム キーが離されたことを示します。","","",""),
			new WndProcMsg(0x0102,"WM_CHAR","ユーザーが文字キーを押したことを示します。","","",""),
			new WndProcMsg(0x0103,"WM_DEADCHAR","ユーザーがデッド キーを押したことを示します。","","",""),
			new WndProcMsg(0x0104,"WM_SYSKEYDOWN","Alt+任意のキーが押されたことを示します。","","",""),
			new WndProcMsg(0x0105,"WM_SYSKEYUP","Alt+任意のキーが離されたことを示します。","","",""),
			new WndProcMsg(0x0106,"WM_SYSCHAR","コントロール メニュー キーが押されたことを示します。","","",""),
			new WndProcMsg(0x0107,"WM_SYSDEADCHAR","システム デッド キーが押されたを示します。","","",""),
			new WndProcMsg(0x0108,"WM_KEYLAST","このメッセージはキーボード メッセージをフィルタリングします。","","",""),
			new WndProcMsg(0x0109,"WM_UNICHAR","","","",""),
			new WndProcMsg(0x010D,"WM_IME_STARTCOMPOSITION","キーストロークの結果として IME が合成文字列を生成する直前に送信されます。ウィンドウは、WindowProc関数を通じてこのメッセージを受け取ります。","","",""),
			new WndProcMsg(0x010E,"WM_IME_ENDCOMPOSITION","IME が合成を終了したときにアプリケーションに送信されます。ウィンドウは、WindowProc関数を通じてこのメッセージを受け取ります。","","",""),
			new WndProcMsg(0x010F,"WM_IME_COMPOSITION","キーストロークの結果として IME が合成ステータスを変更したときにアプリケーションに送信されます。ウィンドウは、WindowProc関数を通じてこのメッセージを受け取ります。","","",""),
			new WndProcMsg(0x010F,"WM_IME_KEYLAST","定義が必要です","","",""),
			new WndProcMsg(0x0110,"WM_INITDIALOG","ダイアログ ボックスを初期化します。","","",""),
			new WndProcMsg(0x0111,"WM_COMMAND","コマンド メッセージを指定します。","","",""),
			new WndProcMsg(0x0112,"WM_SYSCOMMAND","システム コマンドが要求されたことを示します。","","",""),
			new WndProcMsg(0x0113,"WM_TIMER","タイマのタイムアウト時間が経過したことを示します。","","",""),
			new WndProcMsg(0x0114,"WM_HSCROLL","水平スクロール バーがクリックされたことを示します。","","",""),
			new WndProcMsg(0x0115,"WM_VSCROLL","垂直スクロール バーがクリックされたことを示します。","","",""),
			new WndProcMsg(0x0116,"WM_INITMENU","メニューがアクティブ化されようとしていることを示します。","","",""),
			new WndProcMsg(0x0117,"WM_INITMENUPOPUP","ポップアップ メニューが作成されていることを示します。","","",""),
			new WndProcMsg(0x0119,"WM_GESTURE","ジェスチャに関する情報を渡します。","","",""),
			new WndProcMsg(0x011A,"WM_GESTURENOTIFY","ジェスチャ構成を設定できます。","","",""),
			new WndProcMsg(0x011F,"WM_MENUSELECT","ユーザーがメニュー項目を選択したことを示します。","","",""),
			new WndProcMsg(0x0120,"WM_MENUCHAR","未知のメニュー ニーモニックが押されたを示します。","","",""),
			new WndProcMsg(0x0121,"WM_ENTERIDLE","モーダル ダイアログ ボックスまたはメニューがアイドルであることを示します。","","",""),
			new WndProcMsg(0x0122,"WM_MENURBUTTONUP","メニュー項目にカーソルがある状態でマウスの右ボタンが離されたことを示します。","","",""),
			new WndProcMsg(0x0123,"WM_MENUDRAG","WM_MENUDRAG メッセージは、ユーザーがメニュー項目をドラッグすると、ドラッグ アンド ドロップ メニューの所有者に送信されます。","","",""),
			new WndProcMsg(0x0124,"WM_MENUGETOBJECT","WM_MENUGETOBJECT メッセージは、マウス カーソルがメニュー項目に入ったとき、または項目の中央から項目の上部または下部に移動したときに、ドラッグ アンド ドロップ メニューの所有者に送信されます。","","",""),
			new WndProcMsg(0x0125,"WM_UNINITMENUPOPUP","WM_UNINITMENUPOPUP メッセージは、ドロップダウン メニューまたはサブメニューが破棄されたときに送信されます。","","",""),
			new WndProcMsg(0x0126,"WM_MENUCOMMAND","WM_MENUCOMMAND メッセージは、ユーザーがメニューから選択を行うときに送信されます。","","",""),
			new WndProcMsg(0x0127,"WM_CHANGEUISTATE","アプリケーションは WM_CHANGEUISTATE メッセージを送信して、ユーザー インターフェイス (UI) の状態を変更する必要があることを示します。","","",""),
			new WndProcMsg(0x0128,"WM_UPDATEUISTATE","","","",""),
			new WndProcMsg(0x0129,"WM_QUERYUISTATE","","","",""),
			new WndProcMsg(0x0132,"WM_CTLCOLORMSGBOX","メッセージ ボックスが描画されようとしています。","","",""),
			new WndProcMsg(0x0133,"WM_CTLCOLOREDIT","エディット コントロールが描画されようとしています。","","",""),
			new WndProcMsg(0x0134,"WM_CTLCOLORLISTBOX","リスト ボックスが描画されようとしています。","","",""),
			new WndProcMsg(0x0135,"WM_CTLCOLORBTN","ボタンが描画されようとしています。","","",""),
			new WndProcMsg(0x0136,"WM_CTLCOLORDLG","ダイアログ ボックスが描画されようとしています。","","",""),
			new WndProcMsg(0x0137,"WM_CTLCOLORSCROLLBAR","スクロール バーが描画されようとしていることを示します。","","",""),
			new WndProcMsg(0x0138,"WM_CTLCOLORSTATIC","スタティック コントロールが描画されようとしています。","","",""),
			new WndProcMsg(0x0200,"WM_MOUSEFIRST","最初のマウス メッセージを指定するには、WM_MOUSEFIRST を使用します。 PeekMessage() 関数を使用します。","","",""),
			new WndProcMsg(0x0200,"WM_MOUSEMOVE","マウス カーソルが移動したことを示します。","","",""),
			new WndProcMsg(0x0201,"WM_LBUTTONDOWN","左のマウス ボタンがいつ押されたかを示します。","","",""),
			new WndProcMsg(0x0202,"WM_LBUTTONUP","左のマウス ボタンがいつ離されたかを示します。","","",""),
			new WndProcMsg(0x0203,"WM_LBUTTONDBLCLK","マウスの左ボタンをダブルクリックしたことを示します。","","",""),
			new WndProcMsg(0x0204,"WM_RBUTTONDOWN","マウスの右ボタンがいつ押されたかを示します。","","",""),
			new WndProcMsg(0x0205,"WM_RBUTTONUP","マウスの右ボタンがいつ離されたかを示します。","","",""),
			new WndProcMsg(0x0206,"WM_RBUTTONDBLCLK","マウスの右ボタンをダブルクリックしたことを示します。","","",""),
			new WndProcMsg(0x0207,"WM_MBUTTONDOWN","中央のマウス ボタンがいつ押されたかを示します。","","",""),
			new WndProcMsg(0x0208,"WM_MBUTTONUP","中央のマウス ボタンがいつ離されたかを示します。","","",""),
			new WndProcMsg(0x0209,"WM_MBUTTONDBLCLK","マウスの中央ボタンをダブルクリックしたことを示します。","","",""),
			new WndProcMsg(0x020A,"WM_MOUSEWHEEL","マウス ホイールが回転した事を示します。","","",""),
			new WndProcMsg(0x020B,"WM_XBUTTONDOWN","マウスの 4 つ目以降のボタンがいつ押されたかを示します。","","",""),
			new WndProcMsg(0x020C,"WM_XBUTTONUP","マウスの 4 つ目以降のボタンがいつ離されたかを示します。","","",""),
			new WndProcMsg(0x020D,"WM_XBUTTONDBLCLK","マウスの 4 つ目以降のボタンをダブルクリックしたことを示します。","","",""),
			new WndProcMsg(0x020E,"WM_MOUSEHWHEEL","マウス ホイールが回転した事を示します。","","",""),
			new WndProcMsg(0x020E,"WM_MOUSELAST","","","",""),
			new WndProcMsg(0x0210,"WM_PARENTNOTIFY","親ウインドウに子ウインドウのアクティブ状態を通知します。","","",""),
			new WndProcMsg(0x0211,"WM_ENTERMENULOOP","メニューのモーダル ループを開始します。","","",""),
			new WndProcMsg(0x0212,"WM_EXITMENULOOP","メニューのモーダル ループを終了します。","","",""),
			new WndProcMsg(0x0213,"WM_NEXTMENU","WM_NEXTMENU メッセージは、右矢印キーまたは左矢印キーを使用してメニュー バーとシステム メニューを切り替えるときにアプリケーションに送信されます。","","",""),
			new WndProcMsg(0x0214,"WM_SIZING","WM_SIZING メッセージは、ユーザーがサイズ変更しているウィンドウに送信されます。このメッセージを処理することにより、アプリケーションはドラッグ四角形のサイズと位置を監視し、必要に応じてそのサイズまたは位置を変更できます。","","",""),
			new WndProcMsg(0x0215,"WM_CAPTURECHANGED","WM_CAPTURECHANGED メッセージは、マウス キャプチャを失ったウィンドウに送信されます。","","",""),
			new WndProcMsg(0x0216,"WM_MOVING","WM_MOVING メッセージは、ユーザーが移動しているウィンドウに送信されます。このメッセージを処理することにより、アプリケーションはドラッグ四角形の位置を監視し、必要に応じてその位置を変更できます。","","",""),
			new WndProcMsg(0x0218,"WM_POWERBROADCAST","電源管理イベントが発生したことをアプリケーションに通知します。","","",""),
			new WndProcMsg(0x0219,"WM_DEVICECHANGE","デバイスまたはコンピューターのハードウェア構成の変更をアプリケーションに通知します。","","",""),
			new WndProcMsg(0x0220,"WM_MDICREATE","子ウインドウを作成するようMDIクライアントに促します。","","",""),
			new WndProcMsg(0x0221,"WM_MDIDESTROY","MDI子ウインドウをクローズします。","","",""),
			new WndProcMsg(0x0222,"WM_MDIACTIVATE","MDI子ウインドウをアクティブ化します。","","",""),
			new WndProcMsg(0x0223,"WM_MDIRESTORE","子ウインドウを復元するようMDIクライアントに促します。","","",""),
			new WndProcMsg(0x0224,"WM_MDINEXT","次のMDI子ウインドウをアクティブ化します。","","",""),
			new WndProcMsg(0x0225,"WM_MDIMAXIMIZE","MDI子ウインドウを最大化します。","","",""),
			new WndProcMsg(0x0226,"WM_MDITILE","MDI子ウインドウを並べて整列させます。","","",""),
			new WndProcMsg(0x0227,"WM_MDICASCADE","MDI子ウインドウを重ねて整列させます。","","",""),
			new WndProcMsg(0x0228,"WM_MDIICONARRANGE","アイコン化されたMDI子ウインドウを整列します。","","",""),
			new WndProcMsg(0x0229,"WM_MDIGETACTIVE","アクティブなMDI子ウインドウに関するデータを取得します。","","",""),
			new WndProcMsg(0x0230,"WM_MDISETMENU","MDIフレーム ウインドウのメニューを置き換えます。","","",""),
			new WndProcMsg(0x0231,"WM_ENTERSIZEMOVE","ウインドウのサイズ変更/移動が行われる前に通知されます。","","",""),
			new WndProcMsg(0x0232,"WM_EXITSIZEMOVE","ウインドウのサイズ変更/移動が行われた後に通知されます。","","",""),
			new WndProcMsg(0x0233,"WM_DROPFILES","ファイルがドロップされたことを示します。","","",""),
			new WndProcMsg(0x0234,"WM_MDIREFRESHMENU","MDIフレーム ウインドウのメニューを最新表示します。","","",""),
			new WndProcMsg(0x0240,"WM_TOUCH","1 つ以上の接触点 (指やペンなど) がタッチセンサー式デジタイザーの表面に触れたときにウインドウに通知します。","","",""),
			new WndProcMsg(0x0281,"WM_IME_SETCONTEXT","ウィンドウがアクティブ化されたときにアプリケーションに送信されます。ウィンドウは、WindowProc関数を通じてこのメッセージを受け取ります。","","",""),
			new WndProcMsg(0x0282,"WM_IME_NOTIFY","IME ウィンドウへの変更を通知するためにアプリケーションに送信されます。ウィンドウは、WindowProc関数を通じてこのメッセージを受け取ります。","","",""),
			new WndProcMsg(0x0283,"WM_IME_CONTROL","要求されたコマンドを実行するように IME ウィンドウに指示するためにアプリケーションによって送信されます。アプリケーションはこのメッセージを使用して、作成した IME ウィンドウを制御します。このメッセージを送信するには、アプリケーションは次のパラメーターを指定してSendMessage関数を呼び出します。","","",""),
			new WndProcMsg(0x0284,"WM_IME_COMPOSITIONFULL","IME ウィンドウがコンポジション ウィンドウの領域を拡張するスペースを見つけられない場合に、アプリケーションに送信されます。ウィンドウは、WindowProc関数を通じてこのメッセージを受け取ります。","","",""),
			new WndProcMsg(0x0285,"WM_IME_SELECT","オペレーティング システムが現在の IME を変更しようとしているときにアプリケーションに送信されます。ウィンドウは、WindowProc関数を通じてこのメッセージを受け取ります。","","",""),
			new WndProcMsg(0x0286,"WM_IME_CHAR","IME が変換結果の文字を取得したときにアプリケーションに送信されます。ウィンドウは、WindowProc関数を通じてこのメッセージを受け取ります。","","",""),
			new WndProcMsg(0x0288,"WM_IME_REQUEST","コマンドを提供し、情報を要求するためにアプリケーションに送信されます。ウィンドウは、WindowProc関数を通じてこのメッセージを受け取ります。","","",""),
			new WndProcMsg(0x0290,"WM_IME_KEYDOWN","キーが押されたことをアプリケーションに通知し、メッセージの順序を維持するために、IME によってアプリケーションに送信されます。ウィンドウは、WindowProc関数を通じてこのメッセージを受け取ります。","","",""),
			new WndProcMsg(0x0291,"WM_IME_KEYUP","アプリケーションにキーの解放を通知し、メッセージの順序を維持するために、IME によってアプリケーションに送信されます。ウィンドウは、WindowProc関数を通じてこのメッセージを受け取ります。","","",""),
			new WndProcMsg(0x02A0,"WM_NCMOUSEHOVER","TrackMouseEvent の前回の呼び出しで指定されている時間のあいだカーソルがウインドウの非クライアント領域に置かれていました。","","",""),
			new WndProcMsg(0x02A1,"WM_MOUSEHOVER","マウスがウインドウのクライアントエリア上でホバリングしてから、TrackMouseEvent 関数への呼び出しであらかじめ指定された時間が経過しました。","","",""),
			new WndProcMsg(0x02A2,"WM_NCMOUSELEAVE","マウスが、TrackMouseEvent の前回の呼び出しで指定されている時間のあいだカーソルがウインドウの非クライアント領域から出ていました。","","",""),
			new WndProcMsg(0x02A3,"WM_MOUSELEAVE","マウスが、TrackMouseEvent 関数への呼び出しであらかじめ指定されたウインドウのクライアントエリアを離れました。","","",""),
			new WndProcMsg(0x02B1,"WM_WTSSESSION_CHANGE","ユーザーの簡易切り替えが行われました。","","",""),
			new WndProcMsg(0x02C0,"WM_TABLET_DEFBASE","","","",""),
			new WndProcMsg(0x02C8,"WM_TABLET_ADDED","","","",""),
			new WndProcMsg(0x02C9,"WM_TABLET_DELETED","","","",""),
			new WndProcMsg(0x02CB,"WM_TABLET_FLICK","フリック入力があったことを示します。","","",""),
			new WndProcMsg(0x02CC,"WM_TABLET_QUERYSYSTEMGESTURESTATUS","","","",""),
			new WndProcMsg(0x0300,"WM_CUT","選択項目を削除し、 クリップボードにコピーします。","","",""),
			new WndProcMsg(0x0301,"WM_COPY","クリップボードに選択項目をコピーします。","","",""),
			new WndProcMsg(0x0302,"WM_PASTE","クリップボード データをエディット コントロールに挿入します。","","",""),
			new WndProcMsg(0x0303,"WM_CLEAR","エディット コントロールをクリアします。","","",""),
			new WndProcMsg(0x0304,"WM_UNDO","エディット コントロール内での直前の操作を取り消します。","","",""),
			new WndProcMsg(0x0305,"WM_RENDERFORMAT","クリップボード データをレンダするようオーナーに通知します。","","",""),
			new WndProcMsg(0x0306,"WM_RENDERALLFORMATS","すべてのクリップボード形式をレンダするようオーナーに通知します。","","",""),
			new WndProcMsg(0x0307,"WM_DESTROYCLIPBOARD","クリップボードが空になったことをオーナーに通知します。","","",""),
			new WndProcMsg(0x0308,"WM_DRAWCLIPBOARD","クリップボードの内容が変更されたことを示します。","","",""),
			new WndProcMsg(0x0309,"WM_PAINTCLIPBOARD","クリップボードの内容を表示するようオーナーに促します。","","",""),
			new WndProcMsg(0x030A,"WM_VSCROLLCLIPBOARD","クリップボードの内容をスクロールするようオーナーに促します。","","",""),
			new WndProcMsg(0x030B,"WM_SIZECLIPBOARD","クリップボードのサイズが変更されていることを示します。","","",""),
			new WndProcMsg(0x030C,"WM_ASKCBFORMATNAME","新しいタスクがアクティブになるタイミングをアプリケーションに通知します。","","",""),
			new WndProcMsg(0x030D,"WM_CHANGECBCHAIN","クリップボード ビューアのチェインからの除去を通知します。","","",""),
			new WndProcMsg(0x030E,"WM_HSCROLLCLIPBOARD","クリップボードの内容をスクロールするようオーナーに促します。","","",""),
			new WndProcMsg(0x030F,"WM_QUERYNEWPALETTE","ウインドウがその論理パレットを実現できるようにします。","","",""),
			new WndProcMsg(0x0310,"WM_PALETTEISCHANGING","パレットが変更されていることを各ウインドウに通知します。","","",""),
			new WndProcMsg(0x0311,"WM_PALETTECHANGED","フォーカス ウインドウがそのパレットを実現したことを示します。","","",""),
			new WndProcMsg(0x0312,"WM_HOTKEY","ホット キーが検出されています。","","",""),
			new WndProcMsg(0x0317,"WM_PRINT","WM_PRINT メッセージはウィンドウに送信され、指定されたデバイス コンテキスト (最も一般的にはプリンター デバイス コンテキスト) でウィンドウ自体を描画するよう要求します。","","",""),
			new WndProcMsg(0x0318,"WM_PRINTCLIENT","WM_PRINTCLIENT メッセージはウィンドウに送信され、指定されたデバイス コンテキスト (最も一般的にはプリンター デバイス コンテキスト) でクライアント領域を描画するよう要求します。","","",""),
			new WndProcMsg(0x0319,"WM_APPCOMMAND","アプリケーション コマンドが要求されたことを示します。","","",""),
			new WndProcMsg(0x031A,"WM_THEMECHANGED","Windows のテーマが変更された事を示します。","","",""),
			new WndProcMsg(0x031D,"WM_CLIPBOARDUPDATE","WM_CLIPBOARDUPDATE メッセージは、クリップボードの内容が変更されたことを示します。このメッセージをリッスンするには、AddClipboardFormatListener() API を使用してクリップボード フォーマット リスナーを作成します。これは古い SetClipboardViewer() API に置き換わるもので、クリップボードの変更を監視するためのよりシンプルで効率的な方法です。","","",""),
			new WndProcMsg(0x0358,"WM_HANDHELDFIRST","定義が必要です","","",""),
			//new WndProcMsg(0x0359,"WM_HANDHELDLAST","","","",""),
			new WndProcMsg(0x35F,"WM_HANDHELDLAST","定義が必要です","","",""),
			new WndProcMsg(0x360,"WM_AFXLAST","WM_AFXFIRST は、最初の afx メッセージを指定します。","","",""),
			new WndProcMsg(0x37F,"WM_AFXFIRST","WM_AFXFIRST は最後の afx メッセージを指定します。","","",""),
			new WndProcMsg(0x0380,"WM_PENWINFIRST","定義が必要です","","",""),
			new WndProcMsg(0x038F,"WM_PENWINLAST","定義が必要です","","",""),
			new WndProcMsg(0x0390,"WM_COALESCE_FIRST","","","",""),
			new WndProcMsg(0x039F,"WM_COALESCE_LAST","","","",""),
			new WndProcMsg(0x03E0,"WM_DDE_FIRST","","","",""),
			new WndProcMsg(0x03E0,"WM_DDE_INITIATE","DDE対話を開始します。","","",""),
			new WndProcMsg(0x03E1,"WM_DDE_TERMINATE","DDE対話を終了します。","","",""),
			new WndProcMsg(0x03E2,"WM_DDE_ADVISE","DDEデータ変更の更新を要求します。","","",""),
			new WndProcMsg(0x03E3,"WM_DDE_UNADVISE","DDEデータの更新要求を停止させます。","","",""),
			new WndProcMsg(0x03E4,"WM_DDE_ACK","DDEメッセージに対して受領通知をします。","","",""),
			new WndProcMsg(0x03E5,"WM_DDE_DATA","データををDDEクライアントに送ります。","","",""),
			new WndProcMsg(0x03E6,"WM_DDE_REQUEST","DDEサーバーからデータを要求します。","","",""),
			new WndProcMsg(0x03E7,"WM_DDE_POKE","未要求のデータをサーバーに送ります。","","",""),
			new WndProcMsg(0x03E8,"WM_DDE_EXECUTE","文字列をDDEサーバーに送ります。","","",""),
			new WndProcMsg(0x031E,"WM_DWMCOMPOSITIONCHANGED","DWM 合成の設定が変更された事を示します。","","",""),
			new WndProcMsg(0x031F,"WM_DWMNCRENDERINGCHANGED","DWM レンダリングがクライアント領域外で変更された事を示します。","","",""),
			new WndProcMsg(0x0320,"WM_DWMCOLORIZATIONCOLORCHANGED","DWM 合成の基準となる配色が変更された事を示します。","","",""),
			new WndProcMsg(0x0321,"WM_DWMWINDOWMAXIMIZEDCHANGE","DWM 合成ウインドウが最大化または最大化解除された事を示します。","","",""),
			new WndProcMsg(0x0323,"WM_DWMSENDICONICTHUMBNAIL","","","",""),
			new WndProcMsg(0x0326,"WM_DWMSENDICONICLIVEPREVIEWBITMAP","","","",""),
			new WndProcMsg(0x033F,"WM_GETTITLEBARINFOEX","","","",""),
			new WndProcMsg(0x0400,"WM_USER","メッセージ値の範囲を示します。 0x0000..WM_USER-1 はシステム予約です。WM_USER..WM_APP-1 は Windows で利用されます。","","",""),
			new WndProcMsg(0x0401,"WM_CHOOSEFONT_GETLOGFONT","[フォントの指定]ダイアログ ボックスのLOGFONT構造体を取得します。","","",""),
			new WndProcMsg(0x8000,"WM_APP","WM_APP..WM_APP + 0x3FFF はアプリケーションで自由に定義できます。","","",""),
			new WndProcMsg(0xCCCD,"WM_RASDIALEVENT","RAS接続状態が変更されたことを通知します。","","",""),

		};
	}
}
