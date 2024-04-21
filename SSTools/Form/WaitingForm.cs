using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SSTools
{
	/// <summary>
	/// 進捗表示付き待ちフォーム
	/// </summary>
	public partial class WaitingForm : Form
	{
		/// <summary>
		/// キャンセルイベント
		/// </summary>
		public event EventHandler CancelEvent;
		/// <summary>
		/// キャンセルイベント発行
		/// </summary>
		protected virtual void OnCancelEvent()
		{
			CancelEvent?.Invoke(this, EventArgs.Empty);
		}
		/// <summary>
		/// 表示する文字列
		/// </summary>
		private string _caption;
		/// <summary>
		/// 表示する文字列(プロパティ)
		/// </summary>
		public string Caption
		{
			get => _caption;
			set => SetCaption(value);
		}
		/// <summary>
		/// 表示する文字列を設定
		/// </summary>
		/// <param name="caption">表示する文字列</param>
		private void SetCaption(string caption)
		{
			if (InvokeRequired)
			{
				Invoke((MethodInvoker)delegate { SetCaption(caption); });
				return;
			}
			_caption = caption;
			LbCaption.Text = caption;
		}
		/// <summary>
		/// 合計数
		/// </summary>
		private int _total;
		/// <summary>
		/// 合計数(プロパティ)
		/// </summary>
		public int Total
		{
			get => _total;
			set => SetTotal(value);
		}
		/// <summary>
		/// 合計数を設定
		/// </summary>
		/// <param name="total">合計数</param>
		private void SetTotal(int total)
		{
			if (InvokeRequired)
			{
				Invoke((MethodInvoker)delegate { SetTotal(total); });
				return;
			}
			_total = total;
			PBarExec.Maximum = total;
		}
		/// <summary>
		/// ウィンドウタイトル
		/// </summary>
		private string _title;
		/// <summary>
		/// ウィンドウタイトル(プロパティ)
		/// </summary>
		public string Title
		{
			get => _title;
			set => this.Text = value;
		}
		/// <summary>
		/// ウィンドウタイトルの設定
		/// </summary>
		/// <param name="title">タイトル</param>
		private void SetTitle(string title)
		{
			if (InvokeRequired)
			{
				Invoke((MethodInvoker)delegate { SetTitle(title); });
				return;
			}
			_title = title;
			this.Text = title;
		}
		/// <summary>
		/// キャンセルされたかどうか
		/// </summary>
		public bool IsCancel { get; private set; } = false;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public WaitingForm()
		{
			InitializeComponent();

			LbCaption.Text = "";
			LbProgress.Text = "(/)";
			// ローカル変数へコピー
			_caption = LbCaption.Text;
			_total = PBarExec.Maximum;
			_value = PBarExec.Value;
			_title = this.Text;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="caption">表示する文字列</param>
		/// <param name="total">合計数</param>
		public WaitingForm(string caption,int total) :this()
		{
			Caption = caption;
			Total = total;
		}
		/// <summary>
		/// 進捗値
		/// </summary>
		private int _value;
		/// <summary>
		/// 進捗値プロパティ
		/// </summary>
		public int Value
		{
			get => _value;
			set { Set(value) ; }
		}

		/// <summary>
		/// 進捗値を設定
		/// </summary>
		/// <param name="value">進捗値</param>
		public void Set(int value)
		{
			if (InvokeRequired)
			{
				Invoke((MethodInvoker)delegate { Set(value); });
				return;
			}
			PBarExec.Value = value;
			LbProgress.Text = string.Format("({0}/{1})", value, Total);
			_value = value;
			Refresh();
		}
		/// <summary>
		/// 中止ボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtCancel_Click(object sender, EventArgs e)
		{
			IsCancel = true;
			OnCancelEvent();
		}
	}
}
