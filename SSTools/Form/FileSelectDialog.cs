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
using static SSTools.FileListView;

namespace SSTools
{
    /// <summary>
    /// ファイル選択ダイアログ
    /// </summary>
    public partial class FileSelectDialog : System.Windows.Forms.Form
    {
        /// <summary>
        /// 存在しないファイルの名前をユーザーが指定した場合に、ダイアログ ボックスが
        /// 警告を表示するかどうかを示す値
        ///  既定値は true
        /// </summary>
        [Category("ダイアログ"), DefaultValue(true),
            Description("存在しないファイルの名前をユーザーが指定した場合に、ダイアログボックスが" +
            "警告を表示するかどうかを示す値。既定値は true")]
        public bool CheckFileExists { get; set; } = true;
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
        /// 選択されたファイル名
        /// </summary>
        private string _fileName;
        /// <summary>
        /// ファイル ダイアログ ボックスで選択されたファイル名を含む文字列
        ///  既定値は空の文字列 ("") 
        /// </summary>
        [Category("ダイアログ"), DefaultValue(""),
            Description("ファイルダイアログボックスで選択されたファイル名を含む文字列。" +
            "既定値は空の文字列 (\"\")")]
        public string FileName 
        { 
            get => _fileName;
            set 
            {
                if (FileView.SetSelect(new string[] { value }))
                {
                    _fileName = value;
                }
            } 
        }

        /// <summary>
        /// 選択されたファイル名(フルパス)
        /// </summary>
        private string[] _fileNames;
        /// <summary>
        /// 選択されたファイル名(フルパス)
        /// </summary>
        [Category("ダイアログ"), DefaultValue(""),
            Description("ファイルダイアログボックスで選択されたファイル名を含む文字列。")]
        public string[] FileNames 
        { 
            get=> _fileNames;
            set 
            {
                if (FileView.SetSelect(value))
                {
                    _fileNames = value;
                }
            }
        }

        //
        // 概要:
        //     ダイアログ ボックスで選択されたファイルのファイル名と拡張子を取得します。 ファイル名にはパスは含まれません。
        //
        // 戻り値:
        //     ダイアログ ボックスで選択されたファイルのファイル名と拡張子。 ファイル名にはパスは含まれません。 既定値は空の文字列です。
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("ダイアログ"), DefaultValue(""),
            Description("ダイアログ ボックスで選択されたファイルのファイル名と拡張子を取得します。 ファイル名にはパスは含まれません。")]
        public string SafeFileName { get; private set; }
        //
        // 概要:
        //     ダイアログ ボックスで選択されたすべてのファイルのファイル名と拡張子の配列を取得します。 ファイル名にはパスは含まれません。
        //
        // 戻り値:
        //     ダイアログ ボックスで選択されたすべてのファイルのファイル名と拡張子の配列。 ファイル名にはパスは含まれません。 ファイルが 1 つも選択されていない場合は、空の配列が返されます。
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("ダイアログ"), DefaultValue(""),
            Description("ダイアログ ボックスで選択されたすべてのファイルのファイル名と拡張子の配列を取得します。 ファイル名にはパスは含まれません。")]
        public string[] SafeFileNames { get; private set; }

        /// <summary>
        /// フィルタリスト
        /// </summary>
        private List<FIlterListClass> _filterList;
        /// <summary>
        /// ダイアログ ボックスの [ファイルの種類] ボックスに表示される選択肢
        /// </summary>
        [Category("ダイアログ"), DefaultValue("全てのファイル|*.*"),
            Description("ダイアログボックスの [ファイルの種類]ボックスに表示される選択肢")]
        public string Filter 
        { 
            get
            {
                string result = FIlterListClass.ToString(_filterList);
                if (string.IsNullOrEmpty(result))
                    return "全てのファイル|*.*";
                return result;
            }
            set
            {
                _filterList = FIlterListClass.MakeList(value);
                // コンボボックスに設定
                FIlterListClass.MakeComboBox(CbFilter, _filterList);
            }
        }
        /// <summary>
        /// ファイル ダイアログ ボックスで現在選択されているフィルターのインデックス
        /// </summary>
        [Category("ダイアログ"), DefaultValue(1),
            Description("ファイルダイアログボックスで現在選択されているフィルターのインデックス")]
        public int FilterIndex 
        { 
            get
            {
                if (CbFilter.Items.Count > 0)
                    return CbFilter.Items.Count;
                return 0;
            }
            set
            {
                if ((_filterList != null) && (_filterList.Count > 0) &&
                    (value < CbFilter.Items.Count) && (value >= 0))
                    CbFilter.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 初期表示フォルダ
        /// </summary>
        private string _initialDirectory = null;
        /// <summary>
        /// ファイル ダイアログ ボックスに表示される起動ディレクトリ
        /// </summary>
        [Category("ダイアログ"), DefaultValue(""),
            Description("ファイルダイアログボックスに表示される起動ディレクトリ")]
        public string InitialDirectory 
        { 
            get
            {
                if (string.IsNullOrEmpty(TbPath.Text))
                {
                    // 直近のパスから取得
                    _initialDirectory = GetIntialDirectory(_initialDirectory);
                    TbPath.Text = _initialDirectory;
                }
                else if ((string.IsNullOrEmpty(_initialDirectory) == false) &&
                    (_initialDirectory != TbPath.Text.Trim()) && 
                    (Directory.Exists(TbPath.Text)))
                {
                    _initialDirectory = TbPath.Text.Trim();
                }
                else if (string.IsNullOrEmpty(_initialDirectory))
                {
                    // 直近のパスから取得
                    _initialDirectory = GetIntialDirectory(_initialDirectory);
                    TbPath.Text = _initialDirectory;
                }
                return _initialDirectory;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {   // 直近のパスから取得
                    _initialDirectory = GetIntialDirectory(_initialDirectory);
                    TbPath.Text = _initialDirectory;
                }
                else if ((CheckPathExists) && (Directory.Exists(value) == false))
                {   // 存在しないパスを指定された
                    MessageBox.Show(string.Format("指定されたディレクトリ:\"{0}\"は存在しません", value), "ディレクトリが存在しない",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    _initialDirectory = value;
                    TbPath.Text = value;
                }
                // パスを表示
                ShowSelectTree(_initialDirectory);
            }
        }
        /// <summary>
        /// ファイル ダイアログ ボックスのタイトル
        /// </summary>
        [Category("ダイアログ"), DefaultValue(""),
            Description("ファイルダイアログ ボックスのタイトル")]
        public string Title 
        {
            get => this.Text;
            set => this.Text = value; 
        }
        /// <summary>
        /// ダイアログ ボックスで複数のファイルを選択できるかどうかを示す値
        ///  既定値は false
        /// </summary>
        [Category("ダイアログ"), DefaultValue(false),
            Description("ダイアログボックスで複数のファイルを選択できるかどうかを示す値。" +
            "既定値は false")]
        public bool Multiselect 
        { 
            get => FileView.MultiSelect;
            set => FileView.MultiSelect = value; 
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
        /// フィルタクラス
        /// </summary>
        private class FIlterListClass
        {
            /// <summary>
            /// 名前
            /// </summary>
            public string Name { get; private set; }
            /// <summary>
            /// 表示名
            /// </summary>
            public string DisplayName { get; private set; }
            /// <summary>
            /// 拡張子(文字列)
            /// </summary>
            public string Ext { get; private set; }
            /// <summary>
            /// 拡張子リスト
            /// </summary>
            public List<string> ExtList { get; private set; } = new List<string>();
            /// <summary>
            /// オリジナルの文字列
            /// </summary>
            public string OrigText { get; private set; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="name"></param>
            /// <param name="display_name"></param>
            /// <param name="ext"></param>
            /// <param name="orig_text"></param>
            public FIlterListClass(string name,string display_name, string ext,string orig_text)
            {
                Name = name;
                Ext = ext;
                DisplayName = display_name;
                OrigText = orig_text;
                // 拡張子を".拡張子"のリストにする
                MatchCollection ext_matches = Regex.Matches(ext, @"(\.[^;]+)");
                if (ext_matches.Count > 0)
                {
                    foreach (Match ext_match in ext_matches)
                    {
                        if (ext_match.Success)
                        {
                            if (ext_match.Value != ".*")
                                ExtList.Add(ext_match.Value);
                        }
                    }
                }
            }
            /// <summary>
            /// 文字列変換(表示名を返す)
            /// </summary>
            /// <returns>変換した文字列</returns>
            public override string ToString()
            {
                return DisplayName;
            }
            /// <summary>
            /// 拡張子リストを生成
            /// </summary>
            /// <param name="text">拡張子文字列</param>
            /// <returns>拡張子のリスト</returns>
            public static List<FIlterListClass> MakeList(string text)
            {
                List<FIlterListClass> list = new List<FIlterListClass>();
                // |種別|*.拡張子|を分ける
                MatchCollection matches = Regex.Matches(text, @"([^\|]+)\|([^\|]+)");
                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                    {
                        if ((match.Success) && (match.Groups.Count >= 3))
                        {
                            string name = match.Groups[1].Value;
                            string ext = match.Groups[2].Value;
                            string display_name = name;
                            string orig_text = match.Groups[0].Value;

                            // 名前に既に拡張子が入っているか？
                            if (Regex.IsMatch(name, @"\((\*\.[^;]+;{0,1})+\)") == false)
                            {
                                display_name += "(" + ext + ")";
                            }
                            // リストに追加
                            list.Add(new FIlterListClass(name,display_name, ext,orig_text));
                        }
                    }
                }
                return list;
            }
            /// <summary>
            /// 拡張子リストをコンボボックスに設定
            /// </summary>
            /// <param name="comboBox">コンボボックス</param>
            /// <param name="list">拡張子リスト</param>
            /// <param name="select_index">選択するインデックス</param>
            public static void MakeComboBox(ComboBox comboBox, List<FIlterListClass> list,int select_index = 0)
            {
                comboBox.Items.Clear();
                foreach (FIlterListClass item in list)
                {
                    comboBox.Items.Add(item);
                }
                if ((select_index >= 0) && (select_index < comboBox.Items.Count))
                    comboBox.SelectedIndex = select_index;
            }
            /// <summary>
            /// 拡張子リストを文字列に変換する
            /// </summary>
            /// <param name="list">拡張子リスト</param>
            /// <returns>変換した文字列</returns>
            public static string ToString(List<FIlterListClass> list)
            {
                string result = string.Empty;
                if (list != null)
                {
                    foreach (FIlterListClass item in list)
                    {
                        result += ((result.Length > 0) ? "|" : "") + item.OrigText;
                    }
                }
                return result;
            }
        }
        /// <summary>
        /// 直近の選択パス
        /// </summary>
        public static string RecentPath = null;
        /// <summary>
        /// 直近のパスと新規パスから、参照できるパスを取得
        /// </summary>
        /// <param name="init_folder">初期フォルダ</param>
        /// <returns>参照できるパス</returns>
        public static string GetIntialDirectory(string init_folder)
        {
            if (string.IsNullOrEmpty(init_folder))
                if (string.IsNullOrEmpty(RecentPath))
                    return Directory.GetCurrentDirectory();
                else
                    return RecentPath;
            else 
                return init_folder;
        }
        /// <summary>
        /// 最後に選択されたView
        /// </summary>
        public static FileListView.FILE_VIEW RecentView = FileListView.FILE_VIEW.LargeIcon;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FileSelectDialog()
        {
            InitializeComponent();
            // リストViewを設定
            FileView.View = RecentView;

            // デフォルトのフィルタリストを作成
            _filterList = FIlterListClass.MakeList(Filter);
            // コンボボックスに設定
            FIlterListClass.MakeComboBox(CbFilter, _filterList);

        }
        /// <summary>
        /// 表示された
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (string.IsNullOrEmpty(_initialDirectory))
            {
                // 直近のパスから取得
                _initialDirectory = GetIntialDirectory(_initialDirectory);
                TbPath.Text = _initialDirectory;
                // パスを表示
                ShowSelectTree(_initialDirectory);
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

            // ファイル一覧
            if ((CbFilter.Items.Count > 0) && (CbFilter.SelectedItem != null) &&
                (CbFilter.SelectedItem is FIlterListClass item))
            {
                // ファイルViewへ設定
                FileView.SetPath(fullpath, topNode,item.ExtList);
            }
            else
            {   // ファイルViewへ設定
                FileView.SetPath(fullpath, topNode, null);
            }
        }
        /// <summary>
        /// フィルタの選択が変わった
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント引数</param>
        private void CbFilter_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((CbFilter.Items.Count > 0) && (CbFilter.SelectedItem != null) &&
                (CbFilter.SelectedItem is FIlterListClass item))
            {
                // ファイルViewへ設定
                FileView.SetPath(FileView.Path, null, item.ExtList);
            }
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
        /// ファイルが選択された
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="paths">パス(配列)</param>
        /// <param name="isDoubleClick">true:DoubleClickされた</param>
        private void FileView_SelectedEvent(object sender, string[] paths,bool isDoubleClick)
        {
            if (paths.Length > 0) 
            {
                if (paths.Length == 1)
                    TbSelectFiles.Text = Path.GetFileName(paths[0]);
                else
                {
                    string result = string.Empty;
                    foreach (string path in paths)
                        result += ((result.Length > 0) ? "," : "") + "\"" + Path.GetFileName(path) + "\"";
                    TbSelectFiles.Text = result;
                }
                if (isDoubleClick)
                {   // ダブルクリック => 即OK
                    BtOK_Click(sender, new EventArgs());
                }
            }
        }
        /// <summary>
        /// テキストボックスからファイル一覧を取得
        /// </summary>
        /// <param name="textBox">テキストボックス</param>
        /// <returns>ファイル一覧</returns>
        private string[] GetFilenamesFromTextbox(TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text) == false)
            {
                string text = textBox.Text;
                if (text.IndexOf("\",\"") > 0)
                {   // 複数カラム
                    string cut_text = text.Trim().Trim('\"');
                    string[] files = cut_text.Split(new string[] { "\",\"" }, StringSplitOptions.RemoveEmptyEntries);
                    return files;
                }
                else
                    return new string[] { text };
            }
            return null;
        }
        /// <summary>
        /// ファイルの存在チェックとプロパティへの格納
        /// </summary>
        /// <returns>true:ファイルが存在</returns>
        private bool CheckAndStoreFileNames()
        {
            string[] files = GetFilenamesFromTextbox(TbSelectFiles);
            if ((files != null) && (files.Length > 0))
            {
                string[] full_paths = new string[files.Length];
                
                for(int index = 0; index < files.Length; index ++)
                {
                    string full_path = Path.Combine(FileView.Path, files[index]);
                    if ((File.Exists(full_path) == false) && (CheckFileExists))
                    {   // 存在しないファイルが選択された
                        MessageBox.Show(string.Format("ファイル:\"{0}\"が存在しません。",files[index]),"ファイルが存在しません",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else
                    {   // 格納
                        full_paths[index] = full_path;
                    }
                }
                if (Multiselect)
                {
                    SafeFileNames = files;
                    _fileNames = full_paths;
                }
                else
                {   // 単一選択
                    SafeFileNames = new string[] { files[0] };
                    _fileNames = new string[] { full_paths[0] };
                }
                SafeFileName = files[0];
                _fileName = full_paths[0];

                // 直近のパスに設定
                RecentPath = Path.GetDirectoryName(_fileName);

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
            if (CheckAndStoreFileNames() == false)
                return;

            // 現在のViewの選択を保存
            RecentView = FileView.View;
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
    }
}
