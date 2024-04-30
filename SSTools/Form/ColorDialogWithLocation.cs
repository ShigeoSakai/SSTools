using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SSTools.WndMsgAnalysis;

namespace SSTools
{
    /// <summary>
    /// 位置指定可能なColorDialog
    /// </summary>
    /// <remarks>
    /// StartPositionを、WindowsDefaultBoundsに設定した場合のみ、
	/// 親コントロールが隠れない位置にDialogを表示します。
    /// </remarks>
    public class ColorDialogWithLocation : ColorDialog
	{
		/// <summary>
		/// Win32API ウィンドウの位置、サイズを取得
		/// </summary>
		/// <param name="hWnd">ウィンドウハンドル</param>
		/// <param name="lpRect">結果の領域</param>
		/// <returns>true;取得OK</returns>
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern bool GetWindowRect(IntPtr hWnd, [In, Out] ref WndMsgAnalysis.RECT lpRect);

		/// <summary>
		/// Win32API ウィンドウの移動
		/// </summary>
		/// <param name="hWnd">ウィンドウハンドル</param>
		/// <param name="X">X座標</param>
		/// <param name="Y">Y座標</param>
		/// <param name="nWidth">ウィンドウ幅</param>
		/// <param name="nHeight">ウィンドウ高さ</param>
		/// <param name="bRepaint">再描画</param>
		/// <returns>true:設定OK</returns>
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern bool MoveWindow(IntPtr hWnd, int X, int Y,
			int nWidth, int nHeight, bool bRepaint);

        /// <summary>
        /// ウィンドウ表示座標
        /// </summary>
        /// <remarks>
        /// Manual時のみ有効
        /// </remarks>
        public Point Location { get; set; } = new Point(0, 0);
        /// <summary>
        /// ウィンドウ表示位置
        /// </summary>
        public FormStartPosition StartPosition { get; set; } = FormStartPosition.CenterScreen;
		/// <summary>
		/// 親コントロール
		/// </summary>
		public Control Parent { get; set; } = null;
		/// <summary>
		/// コントロールを所有している親Formを求める
		/// </summary>
		/// <param name="ctrl">コントロール</param>
		/// <returns>親Form。見つからない場合はnull</returns>
		private Form GetParentForm(Control ctrl)
		{
			if (ctrl == null)
				return null;
			if (ctrl is Form frm)
				return frm;
			return GetParentForm(ctrl.Parent);
		}
		/// <summary>
		/// 親コントロールの表示されているスクリーン領域を求める
		/// </summary>
		/// <param name="hWnd">ウィンドウハンドル</param>
		/// <returns>スクリーン領域</returns>
		private Rectangle GetScreenBounds(IntPtr hWnd)
		{
			if (Parent == null)
				return Screen.FromHandle(hWnd).WorkingArea;
			return Screen.FromControl(Parent).WorkingArea; ;
		}
		/// <summary>
		/// Windowメッセージ解析モジュール
		/// </summary>
		private readonly WndMsgAnalysis analyzer = new WndMsgAnalysis(ANALYSIS_MODE.ALL | ANALYSIS_MODE.SHOW_DESCRIPTION);
        /// <summary>
        /// Windowメッセージ解析モジュール初期化
        /// </summary>
        private void SetWndFunction()
		{
            // WM_INITDIALOG
            analyzer.SetFunc<WM_DEFAULT>(WND_MSG_ENUM.WM_INITDIALOG, WmInitDialog);
			// WM_SIZE
			analyzer.SetFunc<WM_DEFAULT>(WND_MSG_ENUM.WM_SIZE, WmSize);
		}
        /// <summary>
        /// ダイアログ初期表示(WM_INITDIALOG)
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル</param>
        /// <param name="msgID">メッセージID=WM_INITDIALOG</param>
        /// <param name="param">パラメータ</param>
        /// <param name="description">説明文字列</param>
        /// <returns>常にtrue(処理済)</returns>
        private bool WmInitDialog(IntPtr hWnd, WND_MSG_ENUM msgID, WM_DEFAULT param, string description)
		{
			RECT r = new RECT();

			// ダイアログボックスの位置とサイズを取得する
			GetWindowRect(hWnd, ref r);
			// スクリーンの表示領域
			Rectangle screenRect = GetScreenBounds(hWnd);
			// ダイアログのサイズ
			Size dialogSize = new Size(r.Width, r.Height);
			switch (StartPosition)
			{
				case FormStartPosition.Manual:
					// Location指定
					break;
				case FormStartPosition.CenterScreen:
					// スクリーン中央
					Location = new Point(
						screenRect.Left + (screenRect.Width - dialogSize.Width) / 2,
						screenRect.Top + (screenRect.Height - dialogSize.Height) / 2);
					break;
				case FormStartPosition.WindowsDefaultLocation:
					// デフォルト位置
                    Location = new Point(r.Left, r.Top);
                    break;
                case FormStartPosition.WindowsDefaultBounds:
					// 親コントロールが隠れない位置
					if (Parent != null)
					{
						Location = FormUtils.CalcLocation(Parent,dialogSize);
					}
					else
					{
                        Location = new Point(r.Left, r.Top);
                    }
                    break;

				case FormStartPosition.CenterParent:
					// 親Formの中央
					Form parent = GetParentForm(Parent);
					if (parent != null)
					{
						Location = new Point(parent.Location.X + (parent.Size.Width - dialogSize.Width) / 2,
							parent.Location.Y + (parent.Size.Height - dialogSize.Height) / 2);
					}
					else
					{
						Location = new Point(r.Left, r.Top);
					}
					break;
			}
			// ダイアログボックスの位置を変更する
			MoveWindow(hWnd, Location.X, Location.Y, r.Width, r.Height, true);

			return true;
		}
        /// <summary>
        /// ダイアログサイズ変更(WM_SIZE)
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル</param>
        /// <param name="msgID">メッセージID=WM_SIZE</param>
        /// <param name="param">パラメータ</param>
        /// <param name="description">説明文字列</param>
        /// <param name="description"></param>
        /// <returns>常にtrue(処理済)</returns>
        private bool WmSize(IntPtr hWnd, WND_MSG_ENUM msgID, WM_DEFAULT param, string description)
		{
			RECT r = new RECT();
			// ダイアログボックスの位置とサイズを取得する
			GetWindowRect(hWnd, ref r);
			if (StartPosition == FormStartPosition.WindowsDefaultBounds)
			{	// 親コントロールの隠れない位置を求める
                Size dialogSize = new Size(r.Width, r.Height);
                if (Parent != null)
                {
                    Location = FormUtils.CalcLocation(Parent, dialogSize);
                }
            }
			// ウィンドウを移動
            MoveWindow(hWnd, Location.X, Location.Y, r.Width, r.Height, true);

			return true;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ColorDialogWithLocation() : base() 
		{
			SetWndFunction();
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="parent">親コントロール</param>
		public ColorDialogWithLocation(Control parent) : this() { Parent = parent; }
		/// <summary>
		/// Windowメッセージフック
		/// </summary>
		/// <param name="hWnd">ウィンドウハンドル</param>
		/// <param name="msg">メッセージ</param>
		/// <param name="wparam">wParam</param>
		/// <param name="lparam">lParam</param>
		/// <returns></returns>
		protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			if (analyzer.Analysis(hWnd,msg,wparam,lparam))
			{	// 処理したメッセージの場合、抜ける
				return IntPtr.Zero;
			}
			// WM_INIDIALOG/WM_SIZE以外のメッセージに対しては元のコントロールにまかせる
			return base.HookProc(hWnd, msg, wparam, lparam);
		}
	}
}
