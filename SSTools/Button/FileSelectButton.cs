using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using System.Reflection;
using System.Security.Cryptography;

namespace SSTools
{
    /// <summary>
    /// ファイル選択ボタン
    /// </summary>
    public class FileSelectButton : Button
    {
        /// <summary>
        /// フォルタプリセット
        /// </summary>
        public enum FILTER_PRESET
        {
            NONE = 0,
            [Description("画像ファイル")]
            IMAGE = 1,
			[Description("テキストファイル")]
			TEXT = 2,
			[Description("動画ファイル")]
			MOVIE = 3,
			[Description("実行ファイル")]
			EXE = 16,
			[Description("システムファイル")]
			SYSTEM = 17,
        }

        /// <summary>
        /// フィルタプリセットクラス
        /// </summary>
		private class FILTER_PRESET_CLASS
        {
            /// <summary>
            /// 拡張子辞書
            /// </summary>
            private Dictionary<FILTER_PRESET,string> PresetDict = new Dictionary<FILTER_PRESET,string>();
            /// <summary>
            /// コンストラクタ
            /// </summary>
            public FILTER_PRESET_CLASS() 
            {
                PresetDict.Clear();
                // 画像ファイル
                PresetDict.Add(FILTER_PRESET.IMAGE, "画像ファイル|*.bmp;*.png;*.jpg;*.jpeg;*.tif;*.tiff;*.gif;*.svg;*.webp|" +
                    "ビットマップファイル|*.bmp|" +
                    "PNGファイル|*.png|" +
                    "JPEGファイル|*.jpg;*.jpeg|" +
                    "TIFFファイル|*.tif;*.tiff|" +
					"GIFファイル|*.gif|" +
					"SVGファイル|*.svg|" +
					"WebPファイル|*.webp|" +
					"全てのファイル|*.*");
                // テキストファイル
                PresetDict.Add(FILTER_PRESET.TEXT, "テキストファイル|*.txt;*.csv;*.log;*.ini|" +
					"プレーンテキストファイル|*.txt|" +
					"CSVファイル|*.csv|" +
                    "ログファイル|*.log|" +
					"INIファイル|*.ini|" +
					"全てのファイル|*.*");
                PresetDict.Add(FILTER_PRESET.MOVIE, "動画ファイル|*.mp4;*.mpeg;*.mpg;*.ts;*.m2t;*.avi;*.webm;*.wmv;*.mov;*.mkv" +
                    "MPEG-4ファイル|*.mp4|" +
                    "MPEG-2ファイル|*.mpeg;*.mpg;*.ts;*.m2t|" +
                    "AVIファイル|*.avi|" +
                    "WebMファイル|*.webm|" +
                    "Windows Mediaファイル|*.wmv|" +
                    "MOVファイル|*.mov|" +
					"MKVファイル|*.mkv|" +
					"全てのファイル|*.*");
				// 実行可能ファイル
				PresetDict.Add(FILTER_PRESET.EXE, "実行可能ファイル|*.exe;*.com;*.bat|" +
                    "EXEファイル|*.exe|" +
                    "COMファイル|*.com|" +
                    "BATファイル|*.bat|" +
                    "全てのファイル|*.*");
				PresetDict.Add(FILTER_PRESET.SYSTEM,"システムファイル|*.exe;*.com;*.dll;*.bat;*.sys|" +
					"EXEファイル|*.exe|" +
					"COMファイル|*.com|" +
                    "DLLファイル|*.dll|" +
					"BATファイル|*.bat|" +
					"SYSファイル|*.sys|" +
					"全てのファイル|*.*");
			}
            /// <summary>
            /// プリセットフィルターの取得
            /// </summary>
            /// <param name="filter"></param>
            /// <returns></returns>
            public string GetPresetFilter(FILTER_PRESET filter)
            {
                if (PresetDict.ContainsKey(filter)) return PresetDict[filter];
                return "全てのファイル|*.*";
            }
		}
        /// <summary>
        /// プリセットフィルタ
        /// </summary>
        private FILTER_PRESET_CLASS PresetFilter = new FILTER_PRESET_CLASS();

		/// <summary>
		/// ファイル選択イベント
		/// </summary>
		[Category("Action"),Description("ファイル選択イベント")]
        public event EventHandler FileSelect;

        /// <summary>
        /// ファイル選択イベント発行
        /// </summary>
        protected virtual void OnFileSelect()
        {
            FileSelect?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FileSelectButton()
        {
            base.Text = "...";
            AutoSize = true;
        }

        /// <summary>
        /// 表示テキスト
        /// （設定できなくする）
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new string Text
        {
            get => base.Text;
            set { }
        }
        /// <summary>
        /// ユーザーが拡張子を指定しない場合、ダイアログ ボックスがファイル名に
        /// 自動的に拡張子を付けるかどうかを示す値
        ///  既定値は true
        /// </summary>
        [Category("ダイアログ"),DefaultValue(true),
            Description("ユーザーが拡張子を指定しない場合、ダイアログ ボックスがファイル名に" +
            "自動的に拡張子を付けるかどうかを示す値。既定値は true")]
        public bool AddExtension { get; set; } = true;
        /// <summary>
        /// この FileDialog インスタンスが Windows Vista で実行されているときに
        /// 外観と動作を自動的にアップグレードする必要がある
        ///  既定値は true
        /// </summary>
        [Category("ダイアログ"), DefaultValue(true),
            Description("このFileDialogインスタンスが Windows Vistaで実行されているときに" +
            "外観と動作を自動的にアップグレードする必要があるかどうか。既定値は true")]
        public bool AutoUpgradeEnabled { get; set; } = true;
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
        /// 既定のファイル名の拡張子
        ///  既定値は空の文字列 ("") 
        /// </summary>
        [Category("ダイアログ"), DefaultValue(""),
            Description("既定のファイル名の拡張子。既定値は空の文字列 (\"\")")]
        public string DefaultExt { get; set; } = "";
        /// <summary>
        /// ダイアログ ボックスが、ショートカットで参照されたファイルの場所を返すかどうか、
        /// またはショートカットの場所 (.lnk) を返すかどうかを指定する値
        ///  既定値は true
        /// </summary>
        [Category("ダイアログ"), DefaultValue(true),
            Description("ダイアログボックスが、ショートカットで参照されたファイルの場所を返すかどうか、" +
            "またはショートカットの場所 (.lnk) を返すかどうかを指定する値。既定値は true")]
        public bool DereferenceLinks { get; set; } = true;
        /// <summary>
        /// ファイル ダイアログ ボックスで選択されたファイル名を含む文字列
        ///  既定値は空の文字列 ("") 
        /// </summary>
        [Category("ダイアログ"), DefaultValue(""),
            Description("ファイルダイアログボックスで選択されたファイル名を含む文字列。" +
            "既定値は空の文字列 (\"\")")]
        public string FileName { get; set; } = "";
        /// <summary>
        /// ダイアログ ボックスの [ファイルの種類] ボックスに表示される選択肢
        /// </summary>
        [Category("ダイアログ"), DefaultValue("全てのファイル|*.*"),
            Description("ダイアログボックスの [ファイルの種類]ボックスに表示される選択肢")]
        public string Filter { get; set; } = "全てのファイル|*.*";
        /// <summary>
        /// ファイル ダイアログ ボックスで現在選択されているフィルターのインデックス
        /// </summary>
        [Category("ダイアログ"), DefaultValue(1),
            Description("ファイルダイアログボックスで現在選択されているフィルターのインデックス")]
        public int FilterIndex { get; set; } = 1;
        /// <summary>
        /// ファイル ダイアログ ボックスに表示される起動ディレクトリ
        /// </summary>
        [Category("ダイアログ"), DefaultValue(""),
            Description("ファイルダイアログボックスに表示される起動ディレクトリ")]
        public string InitialDirectory { get; set; } = "";
        /// <summary>
        /// ダイアログ ボックスを閉じる前に、ディレクトリを以前選択されていた
        /// ディレクトリに復元するかどうかを示す値
        ///  既定値は false
        /// </summary>
        [Category("ダイアログ"), DefaultValue(false),
            Description("ダイアログボックスを閉じる前に、ディレクトリを以前選択されていた" +
            "ディレクトリに復元するかどうかを示す値。既定値は false")]
        public bool RestoreDirectory { get; set; } = false;
        /// <summary>
        /// ファイル ダイアログ ボックスに [ヘルプ] ボタンを表示するかどうかを示す値
        ///  既定値は false
        /// </summary>
        [Category("ダイアログ"), DefaultValue(true),
            Description("ファイルダイアログボックスに [ヘルプ] ボタンを表示するかどうかを示す値。" +
            "既定値は false")]
        public bool ShowHelp { get; set; } = false;
        /// <summary>
        /// 複数のファイル名拡張子を持つファイルの表示をダイアログボックスが
        /// サポートするかどうかを示す値
        ///  既定値は false
        /// </summary>
        [Category("ダイアログ"), DefaultValue(true),
            Description("複数のファイル名拡張子を持つファイルの表示をダイアログボックスが" +
            "サポートするかどうかを示す値。既定値は false")]
        public bool SupportMultiDottedExtensions { get; set; } = false;
        /// <summary>
        /// ファイル ダイアログ ボックスのタイトル
        /// </summary>
        [Category("ダイアログ"), DefaultValue(""),
            Description("ファイルダイアログ ボックスのタイトル")]
        public string Title { get; set; } = "";
        /// <summary>
        /// ダイアログ ボックスが有効な Win32 ファイル名だけを受け入れるかどうかを示す値
        ///  既定値は true
        /// </summary>
        [Category("ダイアログ"), DefaultValue(true),
            Description("ダイアログボックスが有効なWin32ファイル名だけを受け入れるかどうかを示す値。" +
            "既定値は true")]
        public bool ValidateNames { get; set; } = true;
        /// <summary>
        /// ダイアログ ボックスで複数のファイルを選択できるかどうかを示す値
        ///  既定値は false
        /// </summary>
        [Category("ダイアログ"), DefaultValue(false),
            Description("ダイアログボックスで複数のファイルを選択できるかどうかを示す値。" +
            "既定値は false")]
        public bool Multiselect { get; set; } = false;
        /// <summary>
        /// 読み取り専用チェック ボックスがオンかオフかを示す値
        ///  既定値は false
        /// </summary>
        [Category("ダイアログ"), DefaultValue(false),
            Description("読み取り専用チェックボックスがオンかオフかを示す値。" +
            "既定値は false")]
        public bool ReadOnlyChecked { get; set; } = false;
        /// <summary>
        /// ダイアログ ボックスに読み取り専用チェック ボックスが表示されているかどうかを示す値
        ///  既定値は false
        /// </summary>
        [Category("ダイアログ"), DefaultValue(false),
            Description("ダイアログボックスに読み取り専用チェックボックスが表示されているかどうかを示す値。" +
            "既定値は false")]
        public bool ShowReadOnly { get; set; } = false;

        /// <summary>
        /// リンクするコントロール
        /// </summary>
        [Category("ダイアログ"),
            Description("ここに指定されたコントロールのTextプロパティを、ダイアログのパスに設定/結果を格納する。")]
        public Control LinkControl { get; set; } = null;

        /// <summary>
        /// フィルタプリセット
        /// </summary>
        private FILTER_PRESET _presetFilter = FILTER_PRESET.NONE;
		/// <summary>
		/// フィルタプリセット
		/// </summary>
		[Category("ダイアログ"),
			Description("自動設定するフィルタを指定")]
		public FILTER_PRESET Preset
        {
            get => _presetFilter;
            set
            {
                if (value != _presetFilter)
                {
                    if (value != FILTER_PRESET.NONE)
                        Filter = PresetFilter.GetPresetFilter(value);
                }
            }
        }
		/// <summary>
		/// カスタムファイルオープンダイアログを使用
		/// </summary>
		[Category("ダイアログ"),
			Description("カスタムファイルオープンダイアログを使用")]
		public bool UserCustomDialog { get; set; } = false;

		/// <summary>
		/// Clickイベントを送信するコントロール
		/// </summary>
		[Category("ダイアログ"),
			Description("Clickイベントを送信するコントロール")]
		public Control SendClickControl { get; set; } = null;

		/// <summary>
		/// ダイアログ ボックスで選択されたすべてのファイルの名前
		/// </summary>
		[Browsable(false)]
        public string[] FileNames { get; private set; } = null;

        /// <summary>
        /// クリックイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            // ファイル選択ダイアログを開く
            OpenFileSelectDialog();

            // ファイル選択イベント発行
            if (DialogResult == DialogResult.OK)
                OnFileSelect();

            // クリックイベントは発行しない
            // base.OnClick(e);
        }
        /// <summary>
        /// ファイル選択ダイアログを開く
        /// </summary>
        private void OpenFileSelectDialog()
        {
            // フォルダとファイル名初期値
            string folder = InitialDirectory;
            string filename = FileName;
            
            if (LinkControl != null)
            {   // リンクしているコントロールがある場合、Textからパスを取得
                string path = LinkControl.Text;
                if ((path != null) && (path.Trim().Length > 0)) 
                {
                    if (Directory.Exists(path))
                    {   // ディレクトリの場合
                        folder = path;
                    }
                    else
                    {   // ファイルの場合
                        folder = Path.GetDirectoryName(path);
                        filename = Path.GetFileName(path);
                    }
                }
            }
            // フォルダ未設定の場合は、カレントディレクトリ
            if ((folder == null) || (folder.Trim().Length == 0) || (Directory.Exists(folder) == false))
                folder = Directory.GetCurrentDirectory();
            // ファイル名未設定の場合は、"default.txt"
            if ((filename == null) || (filename.Trim().Length == 0))
                filename = "default.txt";
            // フィルタの設定
            string filter = Filter;
            if ((filter == null) || (filter.Trim().Length == 0))
                filter = "全てのファイル|*.*";

            DialogResult dialog_result;
            if (UserCustomDialog == false)
            {   // 通常のダイアログ
                // ファイル選択ダイアログを生成
                OpenFileDialog ofd = new OpenFileDialog()
                {
                    AddExtension = AddExtension,
                    AutoUpgradeEnabled = AutoUpgradeEnabled,
                    CheckFileExists = CheckFileExists,
                    CheckPathExists = CheckPathExists,
                    DefaultExt = DefaultExt,
                    DereferenceLinks = DereferenceLinks,
                    FileName = filename,
                    Filter = filter,
                    FilterIndex = FilterIndex,
                    InitialDirectory = folder,
                    RestoreDirectory = RestoreDirectory,
                    ShowHelp = ShowHelp,
                    SupportMultiDottedExtensions = SupportMultiDottedExtensions,
                    Title = Title,
                    ValidateNames = ValidateNames,
                    Multiselect = Multiselect,
                    ReadOnlyChecked = ReadOnlyChecked,
                    ShowReadOnly = ShowReadOnly,
                };
                // ダイアログを表示して結果を取得
                dialog_result = ofd.ShowDialog();
                if (dialog_result == DialogResult.OK)
                {
                    //  リンクコントロールのTextにファイル名を設定
                    if (LinkControl != null)
                        LinkControl.Text = ofd.FileName;
                    // その他を設定
                    FileName = ofd.FileName;
                    FileNames = ofd.FileNames;
                    InitialDirectory = Path.GetDirectoryName(ofd.FileName);
                    DefaultExt = ofd.DefaultExt;
                }
                ofd.Dispose();
                ofd = null;
            }
            else
            {
                // カスタムダイアログを開く
                FileSelectDialog fileSelectDialog = new FileSelectDialog()
                {
                    CheckFileExists = CheckFileExists,
                    CheckPathExists = CheckPathExists,
                    FileName = filename,
                    Filter = filter,
                    FilterIndex = FilterIndex,
                    InitialDirectory = folder,
                    Title = Title,
                    Multiselect = Multiselect,
                };
                // ダイアログを表示して結果を取得
                dialog_result = fileSelectDialog.ShowDialog();
                if (dialog_result == DialogResult.OK)
                {
                    //  リンクコントロールのTextにファイル名を設定
                    if (LinkControl != null)
                        LinkControl.Text = fileSelectDialog.FileName;
                    // その他を設定
                    FileName = fileSelectDialog.FileName;
                    FileNames = fileSelectDialog.FileNames;
                    InitialDirectory = Path.GetDirectoryName(fileSelectDialog.FileName);
                }
                fileSelectDialog.Dispose();
                fileSelectDialog = null;
            }

            if (dialog_result == DialogResult.OK)
            {   // ダイアログがOKで終了
                // クリックイベント発行
                if (SendClickControl != null)
                {
                    MethodInfo info = SendClickControl.GetType().GetMethod("PerformClick", BindingFlags.Public);
                    if (info != null)
					{   // PerformClick呼び出し
						info.Invoke(SendClickControl, null);
                    }
                    else
                    {   // 強引にOnCLickを呼び出す
						SendClickControl.GetType().InvokeMember("OnClick",
	                        BindingFlags.InvokeMethod |BindingFlags.NonPublic |BindingFlags.Instance,
	                        null,
	                        SendClickControl,
	                        new object[] { EventArgs.Empty });
					}
                }
            }
        }
    }
}
