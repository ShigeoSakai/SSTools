using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Reflection.Emit;

namespace SSTools
{
    /// <summary>
    /// フォルダ選択ダイアログ
    /// </summary>
    public partial class FolderSelectDialog : System.Windows.Forms.Form
    {
        /// <summary>
        /// 存在しないパスをユーザーが指定したときに、ダイアログ ボックスに警告を
        /// 表示するかどうかを示す値
        ///  既定値は true
        /// </summary>
        [Category("ダイアログ"), DefaultValue(true),
            Description("存在しないパスをユーザーが指定したときに、ダイアログボックスに警告を" +
            "表示するかどうかを示す値。既定値は true")]
        public bool CheckPathExists { get; set; } = true;

        /// <summary>
        /// ダイアログ ボックスの上に表示される説明テキスト
        /// </summary>
        [Category("ダイアログ"), DefaultValue(""),
            Description("ダイアログボックスの上に表示される説明テキスト")]
        public string Description
        {
            get => this.Text;
            set => this.Text = value;
        }

        /// <summary>
        /// 参照の開始位置とするルート フォルダー
        /// </summary>
        [Category("ダイアログ"), DefaultValue(typeof(Environment.SpecialFolder), "Desktop"),
            Description("参照の開始位置とするルートフォルダー")]
        public Environment.SpecialFolder RootFolder { get; set; } = Environment.SpecialFolder.Desktop;

        /// <summary>
        /// ユーザーが選択したパス
        /// </summary>
        private string _selectPath = null;
        /// <summary>
        /// ユーザーが選択したパス
        /// </summary>
        [Category("ダイアログ"), DefaultValue(""),
            Description("ユーザーが選択したパス")]
        public string SelectedPath
        {
            get
            {
                if (string.IsNullOrEmpty(TbPath.Text))
                {
                    // 直近のパスから
                    _selectPath = FileSelectDialog.GetIntialDirectory(_selectPath);
                    TbPath.Text = _selectPath;
                }
                else if ((string.IsNullOrEmpty(_selectPath)== false) &&
                    (_selectPath != TbPath.Text.Trim()) &&
                    (Directory.Exists(TbPath.Text)))
                {
                    _selectPath = TbPath.Text.Trim();
                }
                else if (string.IsNullOrEmpty(_selectPath))
                {
                    // 直近のパスから取得
                    _selectPath = FileSelectDialog.GetIntialDirectory(_selectPath);
                    TbPath.Text = _selectPath;
                }

                return _selectPath;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {   // 直近のパスから取得
                    _selectPath = FileSelectDialog.GetIntialDirectory(_selectPath);
                    TbPath.Text = _selectPath;
                }
                else if ((CheckPathExists) && (Directory.Exists(value) == false))
                {   // 存在しないパスを指定された
                    MessageBox.Show(string.Format("指定されたディレクトリ:\"{0}\"は存在しません", value), "ディレクトリが存在しない",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    _selectPath = value;
                    TbPath.Text = value;
                }
                // パスを表示
                ShowSelectTree(_selectPath);
            }
        }
        /// <summary>
        /// フォルダー参照ダイアログボックスに [新しいフォルダー] ボタンを表示するかどうかを示す値
        /// </summary>
        [Category("ダイアログ"), DefaultValue(true),
            Description("フォルダー参照ダイアログボックスに [新しいフォルダー] ボタンを表示するかどうかを示す値")]
        public bool ShowNewFolderButton
        {
            get => FileView.ShowNewFolderButton;
            set => FileView.ShowNewFolderButton = value;
        }

        /// <summary>
        /// 隠しフォルダを表示するか
        /// </summary>
        [Category("ダイアログ"), DefaultValue(false),
            Description("隠しフォルダを表示するか。" +
            "既定値は false")]
        public bool ShowHiddenFolder
        {
            get => FolderTree.ShowHiddenFolder;
            set => FolderTree.ShowHiddenFolder = value;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FolderSelectDialog()
        {
            InitializeComponent();
            // Viewを設定
            FileView.View = FileSelectDialog.RecentView;
        }
        /// <summary>
        /// 表示された
        /// </summary>
        /// <param name="e">イベント引数</param>

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (string.IsNullOrEmpty(_selectPath))
            {
                // 直近のパスから取得
                _selectPath = FileSelectDialog.GetIntialDirectory(_selectPath);
                TbPath.Text = _selectPath;
                // パスを表示
                ShowSelectTree(_selectPath);
            }

        }

        /// <summary>
        /// 選択されたパスを表示
        /// </summary>
        /// <param name="path">パス</param>
        private void ShowSelectTree(string path)
        {
            FolderTree.SelectFolder(path);
        }
        /// <summary>
        /// フォルダノードが選択された
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="path">パス</param>
        /// <param name="fullpath">フルパス</param>
        /// <param name="topNode">Top Node</param>
        private void FolderTree_SelectNodeEvent(object sender, string path, string fullpath, FolderTreeNode topNode)
        {
            // パスの更新
            TbPath.Text = fullpath;

            // ファイルViewへ設定
            FileView.SetPath(fullpath, topNode, null);
        }
        /// <summary>
        /// FileViewからディレクトリ変更イベント
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="path">パス</param>
        /// <param name="topNode">Top Node</param>
        private void FileView_ChangeDirectoryEvent(object sender, string path, FolderTreeNode topNode)
        {
            FolderTree.SelectFolder(path, topNode);
        }
        /// <summary>
        /// フォルダのチェック
        /// </summary>
        /// <returns>true:チェックOK</returns>
        private bool CheckFolder()
        {
            string check_path = null;
            if ((TbPath.Text.Trim().Length > 0) && (Directory.Exists(TbPath.Text)))
            {
                check_path = TbPath.Text.Trim();
            }
            // FileViewで選択されているフォルダがあれば、そっちを優先
            string[] selected_list = FileView.GetSelected(FileListView.SELECT_FLAGS.FOLDER);
            if ((selected_list != null) && (selected_list.Length > 0))
                check_path += selected_list[0];

            if (string.IsNullOrEmpty(check_path) == false)
            {
                // プロパティに設定
                SelectedPath = check_path;
                return true;
            }
            return false;
        }

        /// <summary>
        /// OKボタン
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント引数</param>
        private void BtOK_Click(object sender, EventArgs e)
        {
            // ファイルの選択チェック
            if (CheckFolder() == false)
                return;
            // Viewの選択を保存
            FileSelectDialog.RecentView = FileView.View;
            DialogResult = DialogResult.OK;
            this.Close();

        }
        /// <summary>
        /// キャンセルボタン
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント引数</param>
        private void BtCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// フォルダをテキストボックスの内容に変更
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント引数</param>
        private void BtOpen_Click(object sender, EventArgs e)
        {
            if ((CheckPathExists == false) || (Directory.Exists(TbPath.Text)))
            {
                // パスを表示
                ShowSelectTree(TbPath.Text);
            }
            else
            {   // 存在しないパスを指定された
                MessageBox.Show(string.Format("指定されたディレクトリ:\"{0}\"は存在しません", TbPath.Text.Trim()), "ディレクトリが存在しない",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// フォルダ選択イベント
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="paths">パス</param>
        private void FileView_FolderSelectedEvent(object sender, string[] paths)
        {

        }
    }
}
