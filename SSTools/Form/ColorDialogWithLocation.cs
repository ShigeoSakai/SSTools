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
	public class ColorDialogWithLocation : ColorDialog
	{
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern bool GetWindowRect(IntPtr hWnd, [In, Out] ref WndMsgAnalysis.RECT lpRect);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern bool MoveWindow(IntPtr hWnd, int X, int Y,
			int nWidth, int nHeight, bool bRepaint);

		public Point Location { get; set; } = new Point(0, 0);

		public FormStartPosition StartPosition { get; set; } = FormStartPosition.CenterScreen;

		public Control Parent { get; set; } = null;
		private Form GetParentForm(Control ctrl)
		{
			if (ctrl == null)
				return null;
			if (ctrl is Form frm)
				return frm;
			return GetParentForm(ctrl.Parent);
		}
		private Rectangle GetScreenBounds(IntPtr hWnd)
		{
			if (Parent == null)
				return Screen.FromHandle(hWnd).Bounds;
			return Screen.FromControl(Parent).Bounds; ;
		}

		private readonly WndMsgAnalysis analyzer = new WndMsgAnalysis(ANALYSIS_MODE.ALL | ANALYSIS_MODE.SHOW_DESCRIPTION);

		private void SetWndFunction()
		{
			analyzer.SetFunc<WM_DEFAULT>(WND_MSG_ENUM.WM_INITDIALOG, WmInitDialog);
			//analyzer.SetFunc<WPARAM_SIZE>(WND_MSG_ENUM.WM_SIZE, WmSize);
			analyzer.SetFunc<WM_DEFAULT>(WND_MSG_ENUM.WM_SIZE, WmSize);
		}
		private bool WmInitDialog(IntPtr hWnd, WND_MSG_ENUM msgID, WM_DEFAULT param, string description)
		{
			RECT r = new RECT();

			// ダイアログボックスの位置とサイズを取得する
			GetWindowRect(hWnd, ref r);

			Rectangle screenRect = GetScreenBounds(hWnd);
			Size dialogSize = new Size(r.Width, r.Height);
			switch (StartPosition)
			{
				case FormStartPosition.Manual:
					break;
				case FormStartPosition.CenterScreen:
					Location = new Point(
						screenRect.Left + (screenRect.Width - dialogSize.Width) / 2,
						screenRect.Top + (screenRect.Height - dialogSize.Height) / 2);
					break;
				case FormStartPosition.WindowsDefaultLocation:
				case FormStartPosition.WindowsDefaultBounds:
					Location = new Point(r.Left, r.Top);
					break;
				case FormStartPosition.CenterParent:
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
		private bool WmSize(IntPtr hWnd, WND_MSG_ENUM msgID, WM_DEFAULT param, string description)
		{
			RECT r = new RECT();
			// ダイアログボックスの位置とサイズを取得する
			GetWindowRect(hWnd, ref r);

			MoveWindow(hWnd, Location.X - (r.Width) + 275, Location.Y, r.Width, r.Height, true);

			return true;
		}

		public ColorDialogWithLocation() : base() 
		{
			SetWndFunction();
		}
		public ColorDialogWithLocation(Control parent) : this() { Parent = parent; }

		protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			if (analyzer.Analysis(hWnd,msg,wparam,lparam))
			{
				return IntPtr.Zero;
			}
#if false
			if (msg == 0x110) // WM_INITDIALOG
			{
				WM_RECT r = new WM_RECT();

				// ダイアログボックスの位置とサイズを取得する
				GetWindowRect(hWnd, ref r);

				Rectangle screenRect = GetScreenBounds(hWnd);
				Size dialogSize = new Size(r.Width, r.Height);
				switch (StartPosition)
				{
					case FormStartPosition.Manual:
						break;
					case FormStartPosition.CenterScreen:
						Location = new Point(
							screenRect.Left + (screenRect.Width - dialogSize.Width) / 2,
							screenRect.Top + (screenRect.Height - dialogSize.Height) / 2);
						break;
					case FormStartPosition.WindowsDefaultLocation:
					case FormStartPosition.WindowsDefaultBounds:
						Location = new Point(r.Left, r.Top);
						break;
					case FormStartPosition.CenterParent:
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

				return IntPtr.Zero; // HookProc メソッドでメッセージを処理済みにする		}
			}
			if (msg == 0x0005) //  WM_SIZE
			{
				WM_RECT r = new WM_RECT();
				// ダイアログボックスの位置とサイズを取得する
				GetWindowRect(hWnd, ref r);

				MoveWindow(hWnd, Location.X - (r.Width) + 275, Location.Y, r.Width, r.Height, true);
				return IntPtr.Zero; // HookProc メソッドでメッセージを処理済みにする
			}
#endif

			// WM_INIDIALOG 以外のメッセージに対しては元のコントロールにまかせる
			return base.HookProc(hWnd, msg, wparam, lparam);
		}
	}
}
