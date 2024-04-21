using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace SSTools
{
    /// <summary>
    /// フォルダ選択ボタン
    /// </summary>
    public class FolderSelectButton : Button
    {
        /// <summary>
        /// フォルダ選択イベント
        /// </summary>
        [Category("Action"), Description("フォルダ選択イベント")]
        public event EventHandler FolderSelect;

        /// <summary>
        /// フォルダ選択イベント発行
        /// </summary>
        protected virtual void OnFolderSelect()
        {
            FolderSelect?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FolderSelectButton() 
        {
            base.Text = "...";
            AutoSize = true;
        }

        /// <summary>
        /// 表示テキスト
        /// （設定できなくする）
        /// </summary>
        [Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
        public new string Text
        {
            get => base.Text;
            set { }
        }

        /// <summary>
        /// リンクするコントロール
        /// </summary>
        [Category("ダイアログ"),
            Description("ここに指定されたコントロールのTextプロパティを、ダイアログのパスに設定/結果を格納する。")]
        public Control LinkControl { get; set; } = null;

        /// <summary>
        /// ダイアログ ボックスの上に表示される説明テキスト
        /// </summary>
        [Category("ダイアログ"),DefaultValue(""),
            Description("ダイアログボックスの上に表示される説明テキスト")]
        public string Description { get; set; } = "";

        /// <summary>
        /// 参照の開始位置とするルート フォルダー
        /// </summary>
        [Category("ダイアログ"), DefaultValue(typeof(Environment.SpecialFolder),"Desktop"),
            Description("参照の開始位置とするルートフォルダー")]
        public Environment.SpecialFolder RootFolder { get; set; } = Environment.SpecialFolder.Desktop;

        /// <summary>
        /// ユーザーが選択したパス
        /// </summary>
        [Category("ダイアログ"), DefaultValue(""),
            Description("ユーザーが選択したパス")]
        public string SelectedPath { get; set; } = "";

        /// <summary>
        /// フォルダー参照ダイアログボックスに [新しいフォルダー] ボタンを表示するかどうかを示す値
        /// </summary>
        [Category("ダイアログ"), DefaultValue(true),
            Description("フォルダー参照ダイアログボックスに [新しいフォルダー] ボタンを表示するかどうかを示す値")]
        public bool ShowNewFolderButton { get; set; } = true;

        /// <summary>
        /// フォルダ選択ダイアログを開く
        /// </summary>
        private void OpenFolderDialog()
        {
            //　初期フォルダの設定
            string folder = SelectedPath;
            if (LinkControl != null)
            {
                folder = LinkControl.Text;
            }
            if ((folder == null) ||(folder.Trim().Length == 0) || 
                (Directory.Exists(folder)== false))
            {
                folder = Directory.GetCurrentDirectory();
            }
            // フォルダ選択ダイアログ生成
            FolderBrowserDialog dialog = new FolderBrowserDialog()
            { 
                Description = Description,
                RootFolder = RootFolder,
                SelectedPath = folder,
                ShowNewFolderButton = ShowNewFolderButton,
            };
            // フォルダ選択ダイアログを開く
            DialogResult = dialog.ShowDialog();

            if (DialogResult == DialogResult.OK)
            {
                if (LinkControl != null)
                    LinkControl.Text = dialog.SelectedPath;
                SelectedPath = dialog.SelectedPath;
            }
            // ダイアログ解放
            dialog.Dispose();
        }
        /// <summary>
        /// クリックイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            // ダイアログを開く
            OpenFolderDialog();
            // フォルダ選択イベント発行
            if (DialogResult == DialogResult.OK)
                OnFolderSelect();
            // クリックイベント発行
            base.OnClick(e);
        }
    }
}
