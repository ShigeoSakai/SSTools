using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SSTools.Win32FileInfo;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace SSTools
{
	/// <summary>
	/// ファイル一覧View
	/// </summary>
	public partial class FileListView : UserControl
	{
		/// <summary>
		/// 表示方法
		/// </summary>
		public enum FILE_VIEW
		{
			LargeIcon = System.Windows.Forms.View.LargeIcon,	//!< 大きいアイコン表示
			Details = System.Windows.Forms.View.Details,		//!< 詳細表示
			SmallIcon = System.Windows.Forms.View.SmallIcon,	//!< 小さいアイコン表示
			List = System.Windows.Forms.View.List,				//!< リスト表示
			Tile = System.Windows.Forms.View.Tile,				//!< タイル表示
			ExtraLargeIcon,										//!< 特大アイコン表示
			JumboIcon,											//!< ジャンボアイコン表示
			TileLarge,											//!< 大きいタイル表示
		}
		/// <summary>
		/// ディレクトリ変更イベントハンドラ
		/// </summary>
		/// <param name="sender">送信元</param>
		/// <param name="path">変更ディレクトリ</param>
		/// <param name="topNode">Top Node</param>
		public delegate void ChangeDirectoryEventHandler(object sender, string path, FolderTreeNode topNode);
		/// <summary>
		/// ディレクトリ変更イベント
		/// </summary>
		public event ChangeDirectoryEventHandler ChangeDirectoryEvent;
		/// <summary>
		/// ディレクトリ変更イベント発行
		/// </summary>
		/// <param name="path">変更ディレクトリ</param>
		/// <param name="topNode">Top Node</param>
		protected virtual void OnChangeDirectoryEvent(string path, FolderTreeNode topNode) => ChangeDirectoryEvent?.Invoke(this, path, topNode);

		/// <summary>
		/// ファイル選択イベントハンドラ
		/// </summary>
		/// <param name="sender">送信元</param>
		/// <param name="paths">選択されたパス</param>
		public delegate void FileSelectedEventHandler(object sender, string[] paths, bool IsDoubleClick);
		/// <summary>
		/// ファイル選択イベント
		/// </summary>
		public event FileSelectedEventHandler FileSelectedEvent;
		/// <summary>
		/// ファイル選択イベント発行
		/// </summary>
		/// <param name="paths">選択されたパス</param>
		protected virtual void OnFileSelectedEvent(string[] paths, bool IsDoubleClick) => FileSelectedEvent?.Invoke(this, paths,IsDoubleClick);
		/// <summary>
		/// ファイル選択イベント発行
		/// </summary>
		/// <param name="paths">選択されたパス</param>
		protected virtual void OnFileSelectedEvent(string path, bool IsDoubleClick) => FileSelectedEvent?.Invoke(this, new string[] { path },IsDoubleClick);

        /// <summary>
        /// フォルダ選択イベントハンドラ
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="paths">選択されたパス</param>
        public delegate void FolderSelectedEventHandler(object sender, string[] paths);
        /// <summary>
        /// フォルダ選択イベント
        /// </summary>
        public event FolderSelectedEventHandler FolderSelectedEvent;
        /// <summary>
        /// フォルダ選択イベント発行
        /// </summary>
        /// <param name="paths">選択されたパス</param>
        protected virtual void OnFolderSelectedEvent(string[] paths) => FolderSelectedEvent?.Invoke(this, paths);
        /// <summary>
        /// フォルダ選択イベント発行
        /// </summary>
        /// <param name="paths">選択されたパス</param>
        protected virtual void OnFolderSelectedEvent(string path) => FolderSelectedEvent?.Invoke(this, new string[] { path });


        /// <summary>
        /// 並び替えアイコン(空白)
        /// </summary>
        private const string BLANK_IMG_KEY = "Blank";
		/// <summary>
		/// 並び替えアイコン(上矢印)
		/// </summary>
		private const string UPARROW_IMG_KEY = "UpArrow";
		/// <summary>
		/// 並び替えアイコン(下矢印)
		/// </summary>
		private const string DOWNARROW_IMG_KEY = "DownArrow";

		/// <summary>
		/// 最小のタイル幅
		/// </summary>
		private const int TILE_MIN_WIDTH = 220;

		/// <summary>
		/// タイル表示の幅を計算
		/// </summary>
		/// <returns>タイル表示の幅</returns>
		/// <remarks>
		/// (クライアントサイズ幅/220)で分割したサイズ
		/// </remarks>
		private int CalcTileWidth()
		{
			int result = ListViewFile.ClientSize.Width / TILE_MIN_WIDTH;
			if (result == 0)
				return TILE_MIN_WIDTH;
			return ListViewFile.ClientSize.Width / result;
		}
		/// <summary>
		/// ListViewのカラム自動サイズ
		/// </summary>
		private void AutoSizeColumns()
		{
			// ファイル名は中身で
			ListViewFile.Columns[CHeaderFileName.Index].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
			// 種別は中身で
			ListViewFile.Columns[CHeaderType.Index].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
			// サイズはヘッダで
			ListViewFile.Columns[CHeaderSize.Index].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
			// 日付はヘッダで
			ListViewFile.Columns[CHeaderCreateDate.Index].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
			ListViewFile.Columns[CHeaderUpdateDate.Index].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
			ListViewFile.Columns[CHeaderAccessDate.Index].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
		}
        /// <summary>
        /// 表示(View)
        /// </summary>
        private FILE_VIEW m_view = FILE_VIEW.LargeIcon;
		/// <summary>
		/// 表示(View)プロパティ
		/// </summary>
		public FILE_VIEW View
		{
			get => m_view;
			set
			{
				if (m_view != value)
				{
					ListViewFile.BeginUpdate();
					switch (value)
					{
						case FILE_VIEW.SmallIcon:
						case FILE_VIEW.List:
							ListViewFile.View = (System.Windows.Forms.View)value;
							break;
						case FILE_VIEW.Details:
							ListViewFile.View = (System.Windows.Forms.View)value;
							// カラムサイズ設定
							AutoSizeColumns();
							break;
						case FILE_VIEW.Tile:
							ListViewFile.TileSize = new Size(CalcTileWidth(), LargeIconList.ImageSize.Height + 4);
							ListViewFile.LargeImageList = LargeIconList;
							ListViewFile.View = (System.Windows.Forms.View)value;
							break;
						case FILE_VIEW.TileLarge:
							ListViewFile.TileSize = new Size(CalcTileWidth(), ExtraLargeIconList.ImageSize.Height + 16);
							ListViewFile.LargeImageList = ExtraLargeIconList;
							ListViewFile.View = System.Windows.Forms.View.Tile;
							break;
						case FILE_VIEW.LargeIcon:
							ListViewFile.LargeImageList = LargeIconList;
							ListViewFile.View = System.Windows.Forms.View.LargeIcon;
							break;
						case FILE_VIEW.ExtraLargeIcon:
							ListViewFile.LargeImageList = ExtraLargeIconList;
							ListViewFile.View = System.Windows.Forms.View.LargeIcon;
							break;
						case FILE_VIEW.JumboIcon:
							ListViewFile.LargeImageList = JumboIconList;
							ListViewFile.View = System.Windows.Forms.View.LargeIcon;
							break;
					}
					m_view = value;

					ComboItem<FILE_VIEW>.Set(CbView, m_view);
					SetViewRadioButton(m_view);

					ListViewFile.EndUpdate();
					// 項目に合わせて自動調整
					ListViewFile.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

                }
			}
		}
		/// <summary>
		/// 複数選択可能か？
		/// </summary>
		public bool MultiSelect
		{
			get =>ListViewFile.MultiSelect;
			set => ListViewFile.MultiSelect = value;
		}

		/// <summary>
		/// ListViewの並び替え用クラス
		/// </summary>
		private class ListItemSorter : IComparer
		{
			/// <summary>
			/// 並び替えの方法
			/// </summary>
			public enum SORT_TYPE
			{
				NONE = 0,
				TEXT = 1,
				SIZE = 2,
				DATE = 3
			}
			/// <summary>
			/// 対象のカラム
			/// </summary>
			public int TargetColumn { get; private set; }
			/// <summary>
			/// 並び替え順
			/// </summary> 
			public SortOrder SortOrder { get; set; } = SortOrder.None;
			/// <summary>
			/// 並び替えの方向
			/// </summary>
			public SORT_TYPE SortType { get; private set; } 
			/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="targetColumn">対象カラム</param>
			/// <param name="sortOrder">並び替え順</param>
			/// <param name="sortType">並び替え方法</param>
			public ListItemSorter(int targetColumn, SortOrder sortOrder, SORT_TYPE sortType)
			{
				TargetColumn = targetColumn;
				SortOrder = sortOrder;
				SortType = sortType;
			}
			/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="targetColumn">対象カラム</param>
			/// <param name="sortType">並び替え方法</param>
			public ListItemSorter(int targetColumn, SORT_TYPE sortType)
			{
				TargetColumn = targetColumn;
				SortType = sortType;
			}
			/// <summary>
			/// ファイル属性を数値に変換
			/// </summary>
			/// <param name="attr">ファイル属性文字列</param>
			/// <returns>変換した値</returns>
			/// <remarks>
			///   Directory < Device < Normal の順にする
			/// </remarks>
			private UInt32 GetAttrValue(string attr)
			{
				UInt32 value = Convert.ToUInt32(attr, 16);
				// Normal -> Dirctory -> Deviceの順
				UInt32 deviceValue = (value & 0x00F0) >> 4;
				//  Archive属性のみはNormalと同じにする
				if (deviceValue == 0x02) deviceValue = 0x08;
				UInt32 devBits = ((deviceValue & 0x04) >> 2) |
					((deviceValue & 0x01) << 1) |
					(UInt32)(((deviceValue & 0x05) == 0) ? 0x04 : 0x00) |
					((deviceValue & 0x08));

				return (devBits << 4) | (15 - (value & 0x0F));
			}

			/// <summary>
			/// 比較
			/// </summary>
			/// <param name="x"></param>
			/// <param name="y"></param>
			/// <returns> x < y ... 負値,x = y ... 0 , x > y : 正値</y></returns>
			public int Compare(object x, object y)
			{
				int result = 0;
				if ((x is ListViewItem itemX) && (y is ListViewItem itemY))
				{
					if (SortOrder != SortOrder.None)
					{
						UInt32 attrX = GetAttrValue(itemX.SubItems[6].Text);
						UInt32 attrY = GetAttrValue(itemY.SubItems[6].Text);
						switch(SortType)
						{
							case SORT_TYPE.TEXT:
								if (attrX == attrY)
								{
									result = String.Compare(
										itemX.SubItems[TargetColumn].Text,
										itemY.SubItems[TargetColumn].Text);
									if (SortOrder == SortOrder.Descending)
										result = -result;
								}
								else
									result = (int)(attrX - attrY);
								break;
							case SORT_TYPE.DATE:
								DateTime xDate = DateTime.Parse(itemX.SubItems[TargetColumn].Text);
								DateTime yDate = DateTime.Parse(itemY.SubItems[TargetColumn].Text);
								result = DateTime.Compare(xDate, yDate);
								if (SortOrder == SortOrder.Descending)
									result = -result;
								break;
							case SORT_TYPE.SIZE:
								if (attrX == attrY)
								{
									long xSize = FileListView.StringToFileSize(itemX.SubItems[TargetColumn].Text);
									long ySize = FileListView.StringToFileSize(itemY.SubItems[TargetColumn].Text);
									result = (xSize < ySize) ? -1 : (xSize == ySize) ? 0 : 1;
									if (SortOrder == SortOrder.Descending)
										result = -result;
								}
								else
									result = (int)(attrX - attrY);
								break;
						}

					}
				}
				return result;
			}
		}


		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FileListView()
		{
			InitializeComponent();

			// システムアイコンの取得
			SystemIconManager.Init(SystemIconManager.ICON_SIZE.SMALL |
				SystemIconManager.ICON_SIZE.LARGE |
				SystemIconManager.ICON_SIZE.EXTRA_LARGE |
				SystemIconManager.ICON_SIZE.JUMBO);
			// アイコンの初期化
			SystemIconManager.InitImageList(ref SmallIconList, SystemIconManager.ICON_SIZE.SMALL);
			SystemIconManager.InitImageList(ref LargeIconList, SystemIconManager.ICON_SIZE.LARGE);
			SystemIconManager.InitImageList(ref ExtraLargeIconList, SystemIconManager.ICON_SIZE.EXTRA_LARGE);
			SystemIconManager.InitImageList(ref JumboIconList, SystemIconManager.ICON_SIZE.JUMBO);
			// 上下アイコン追加
			AddUpDownIcon();

			// Viewの初期設定
			ListViewFile.View = System.Windows.Forms.View.LargeIcon;

			// Radio Button Iconの設定
			RbSmall.Image = new Icon(IconResource.Small,new Size(16,16)).ToBitmap();
			RbLarge.Image = new Icon(IconResource.Large, new Size(16, 16)).ToBitmap();
			RbExtraLarge.Image = new Icon(IconResource.ExtraLarge, new Size(16, 16)).ToBitmap();
			RbJumbo.Image = new Icon(IconResource.Jumbo, new Size(16, 16)).ToBitmap();
			RbDetail.Image = new Icon(IconResource.Details, new Size(16, 16)).ToBitmap();
			RbList.Image = new Icon(IconResource.List, new Size(16, 16)).ToBitmap();
			RbTile.Image = new Icon(IconResource.Tile, new Size(16, 16)).ToBitmap();
			RbLargeTile.Image = new Icon(IconResource.TileLarge, new Size(16, 16)).ToBitmap();

			CHeaderFileName.ImageKey = "UpArrow";

			// コンボボックスの初期化
			ComboItem<FILE_VIEW>.MakeCombo(Enum.GetValues(typeof(FILE_VIEW)), CbView);
			ComboItem<FILE_VIEW>.Set(CbView,View);
			// ラジオボタンの設定
			SetViewRadioButton(View);

			// カラムのタグに並び替えの設定
			CHeaderFileName.Tag = new ListItemSorter(CHeaderFileName.Index, ListItemSorter.SORT_TYPE.TEXT);
			CHeaderType.Tag = new ListItemSorter(CHeaderType.Index, ListItemSorter.SORT_TYPE.TEXT);
			CHeaderSize.Tag = new ListItemSorter(CHeaderSize.Index, ListItemSorter.SORT_TYPE.SIZE);
			CHeaderCreateDate.Tag = new ListItemSorter(CHeaderCreateDate.Index, ListItemSorter.SORT_TYPE.DATE);
			CHeaderUpdateDate.Tag = new ListItemSorter(CHeaderUpdateDate.Index, ListItemSorter.SORT_TYPE.DATE);
			CHeaderAccessDate.Tag = new ListItemSorter(CHeaderAccessDate.Index, ListItemSorter.SORT_TYPE.DATE);
			// デフォルトの並び替えを設定
			ListItemSorter func = (ListItemSorter)CHeaderFileName.Tag;
			func.SortOrder = ListViewFile.Sorting;
			ListViewFile.ListViewItemSorter = func;

			// 並び替えの設定
			SetSort(CHeaderFileName.Index, ListViewFile.Sorting);
		}
		/// <summary>
		/// 伸縮ボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtShrink_Click(object sender, EventArgs e)
		{
			if (SplitPanel.Panel2Collapsed)
			{
				SplitPanel.Panel2Collapsed = false;
				BtShrink.Text = ">>";
			}
			else
			{
				SplitPanel.Panel2Collapsed = true;
				BtShrink.Text = "<<";
			}
		}
		/// <summary>
		/// Viewラジオボタンの設定
		/// </summary>
		/// <param name="view"></param>
		private void SetViewRadioButton(FILE_VIEW view)
		{
			switch(view)
			{
				case FILE_VIEW.LargeIcon:
					RbLarge.Checked = true; break;
				case FILE_VIEW.SmallIcon:
					RbSmall.Checked = true; break;
				case FILE_VIEW.ExtraLargeIcon:
					RbExtraLarge.Checked = true; break;
				case FILE_VIEW.JumboIcon:
					RbJumbo.Checked = true; break;
				case FILE_VIEW.Details:
					RbDetail.Checked = true; break;
				case FILE_VIEW.List:
					RbList.Checked = true; break;
				case FILE_VIEW.Tile:
					RbTile.Checked = true; break;
				case FILE_VIEW.TileLarge:
					RbLargeTile.Checked = true; break;
			}
		}
		/// <summary>
		/// 上下アイコン追加
		/// </summary>
		private void AddUpDownIcon()
		{
			SmallIconList.Images.Add("UpArrow",IconResource.Up);
			LargeIconList.Images.Add("UpArrow", IconResource.Up);
			ExtraLargeIconList.Images.Add("UpArrow", IconResource.Up);
			JumboIconList.Images.Add("UpArrow", IconResource.Up);

			SmallIconList.Images.Add("DownArrow", IconResource.Down);
			LargeIconList.Images.Add("DownArrow", IconResource.Down);
			ExtraLargeIconList.Images.Add("DownArrow", IconResource.Down);
			JumboIconList.Images.Add("DownArrow", IconResource.Down);

			SmallIconList.Images.Add("Blank", IconResource.Blank);
			LargeIconList.Images.Add("Blank", IconResource.Blank);
			ExtraLargeIconList.Images.Add("Blank", IconResource.Blank);
			JumboIconList.Images.Add("Blank", IconResource.Blank);

		}

		/// <summary>
		/// コンボボックスからの変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CbView_SelectionChangeCommitted(object sender, EventArgs e)
		{
			View = ComboItem<FILE_VIEW>.Get(CbView, FILE_VIEW.LargeIcon);
		}
		/// <summary>
		/// Viewラジオボタンの変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RbView_CheckedChanged(object sender, EventArgs e)
		{
			FILE_VIEW view = FILE_VIEW.LargeIcon;
			if (RbSmall.Checked)
				view = FILE_VIEW.SmallIcon;
			else if (RbLarge.Checked)
				view = FILE_VIEW.LargeIcon;
			else if (RbExtraLarge.Checked)
				view = FILE_VIEW.ExtraLargeIcon;
			else if (RbJumbo.Checked)
				view = FILE_VIEW.JumboIcon;
			else if (RbList.Checked)
				view = FILE_VIEW.List;
			else if (RbDetail.Checked)
				view = FILE_VIEW.Details;
			else if (RbTile.Checked)
				view = FILE_VIEW.Tile;
			else if (RbLargeTile.Checked)
				view = FILE_VIEW.TileLarge;
			View = view;
		}
		/// <summary>
		/// 現在のディレクトリ
		/// </summary>
		private string m_Path;
		/// <summary>
		/// 現在のディレクトリ(プロパティ)
		/// </summary>
		public string Path
		{
			get => m_Path;
			set => SetPath(value);
		}
		/// <summary>
		/// 現在の最上位ノード
		/// </summary>
		private FolderTreeNode m_TopNode = null;

		/// <summary>
		/// アイコンのindexを取得
		/// </summary>
		/// <param name="icon_index">アイコンのIndex</param>
		/// <returns>ImageListのIndex</returns>
		private int SetIconImage(int icon_index)
		{
			// 小アイコンのインデックスを採用
			int result_s = SystemIconManager.StoreImageList(ref SmallIconList, icon_index, SystemIconManager.ICON_SIZE.SMALL);
			SystemIconManager.StoreImageList(ref LargeIconList, icon_index, SystemIconManager.ICON_SIZE.LARGE);
			SystemIconManager.StoreImageList(ref ExtraLargeIconList, icon_index, SystemIconManager.ICON_SIZE.EXTRA_LARGE);
			SystemIconManager.StoreImageList(ref JumboIconList, icon_index, SystemIconManager.ICON_SIZE.JUMBO);

			return result_s;
		}

		/// <summary>
		/// ワイルドカードマッチング
		/// </summary>
		/// <param name="text"></param>
		/// <param name="pattern"></param>
		/// <returns></returns>
		private bool IsWildcardMatch(string text, string pattern)
		{
			string regex_pattern = string.Empty;
			foreach (char c in text)
			{
				if (c == '*') regex_pattern += ".*";
				else if (c == '?') regex_pattern += ".";
				else regex_pattern += Regex.Escape(c.ToString());
			}
			return Regex.IsMatch(text, regex_pattern);
		}
		/// <summary>
		/// 拡張子によるフィルタ
		/// </summary>
		/// <param name="fileInfo"></param>
		/// <param name="ext_lists"></param>
		/// <returns></returns>
		private bool FilterFilename(FileInfo fileInfo, List<string> ext_lists)
		{
			if ((fileInfo != null) && (ext_lists != null) && (ext_lists.Count > 0))
			{
				// ディレクトリは常に表示許可
				if (fileInfo.Attributes.HasFlag(FileAttributes.Directory))
					return true;
				// ファイルの場合
				string ext = System.IO.Path.GetExtension(fileInfo.FullName);
				// 拡張子に入っていたらOK
				if (ext_lists.Contains(ext)) return true;

				// ワイルドカードマッチング
				foreach (string item in ext_lists)
				{
					if ((string.IsNullOrEmpty(item) == false) &&
						((item.IndexOf('*') > 0) || (item.IndexOf('?') > 0)))
					{
						bool result = IsWildcardMatch(ext, item);
						if (result) return true;
                    }
				}
				return false;
			}
			// 表示許可
			return true;
		}


		/// <summary>
		/// パス設定
		/// </summary>
		/// <param name="path">ディレクトリ</param>
		/// <param name="topNode">最上位ノード</param>
		public void SetPath(string path, FolderTreeNode topNode = null,List<string> ext_lists = null)
		{
			ListViewFile.BeginUpdate();

			// 詳細情報をクリア
			ClearFileDetailInfo();
			// リストビューをクリア
			ListViewFile.Items.Clear();
			if ((path != null) && (Directory.Exists(path)))
			{
				// 取得するフラグ
				SHGFI shFlags = SHGFI.SHGFI_DISPLAYNAME |
					SHGFI.SHGFI_SMALLICON |
					SHGFI.SHGFI_TYPENAME |
					SHGFI.SHGFI_SYSICONINDEX;

				foreach (string filename in Directory.EnumerateFileSystemEntries(path))
				{
					SHFILEINFO f_info = new SHFILEINFO();
					SHGetFileInfo(filename, 0, out f_info, (uint)Marshal.SizeOf(f_info), shFlags);

					string file_size = null;

					FileInfo fileInfo = new FileInfo(filename);
					if (fileInfo.Exists)
					{
						file_size = FileSizeToString(fileInfo.Length);
					}
					if (fileInfo != null)
                    {
                        if (FilterFilename(fileInfo, ext_lists))
                        {
                            // ListViewに追加
                            ListViewFile.Items.Add(new ListViewItem(new string[]
							{
								f_info.szDisplayName,
								f_info.szTypeName,
								file_size,
								fileInfo.CreationTime.ToString("yyyy/MM/dd HH:mm:ss"),
								fileInfo.LastWriteTime.ToString("yyyy/MM/dd HH:mm:ss"),
								fileInfo.LastAccessTime.ToString("yyyy/MM/dd HH:mm:ss"),
								((UInt32)fileInfo.Attributes).ToString("X8"),

							}, SetIconImage(f_info.iIcon)));
						}
					}
				}
			}
			// 現在の状態を保存
			m_Path = path;
			if (topNode != null)
				m_TopNode = topNode;

			if (_selectRequest.Count > 0)
			{
                foreach (ListViewItem item in ListViewFile.Items)
                {
                    bool check = (_selectRequest.Contains(item.SubItems[CHeaderFileName.Index].Text));
                    item.Selected = check;
                }
				_selectRequest.Clear();
            }
            // 項目に合わせて自動調整
            ListViewFile.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            ListViewFile.EndUpdate();

            ListViewFile.Refresh();
        }
		/// <summary>
		/// ファイルサイズの単位
		/// </summary>
		private static readonly List<string> UnitList = new List<string>()
		{
			"","K","M","G"
		};
		/// <summary>
		/// ファイルサイズを文字列に変換
		/// </summary>
		/// <param name="size">ファイルサイズ</param>
		/// <returns>変換した文字列</returns>
		private string FileSizeToString(long size)
		{
			double exp = 1.0;
			double result = (double)size;
			string unit = UnitList[0];
			for (int i = 0; i < UnitList.Count; i++)
			{
				if (result < (exp / 2.0))
					break;
				result /= exp;
				exp *= 1024.0;
				unit = UnitList[i];
			}
			return string.Format("{0:#,0.0}{1}", result, unit);
		}
		/// <summary>
		/// 単位が入ったファイルサイズ文字列からlong値に変換する
		/// </summary>
		/// <param name="str">サイズ文字列</param>
		/// <returns>変換した値</returns>
		private static long StringToFileSize(string str)
		{
			if ((str == null) || (str.Length == 0)) return -1;
			int exp = 0;
			if (UnitList.Contains(str.Last().ToString()))
			{	// 単位が入っている場合
				exp = UnitList.IndexOf(str.Last().ToString());
				str = str.Substring(0,str.Length - 1);
			}
			if (Double.TryParse(str,out double value))
			{
				value = value * Math.Pow(1024.0, exp);
				return (long)value;
			}
			return -1;
		}

		/// <summary>
		/// ファイル属性を文字に変換
		/// </summary>
		/// <param name="attr">ファイル属性</param>
		/// <returns>変換した文字列</returns>
		private string AttributeToString(FileAttributes attr)
		{
			string result = string.Empty;
			if (attr.HasFlag(FileAttributes.ReadOnly))
				result += "RO";
			if (attr.HasFlag(FileAttributes.Hidden))
				result += ((result.Length != 0) ? "," : "") + "H";
			if (attr.HasFlag(FileAttributes.System))
				result += ((result.Length != 0) ? "," : "") + "Sy";
			if (attr.HasFlag(FileAttributes.Directory))
				result += ((result.Length != 0) ? "," : "") + "D";
			if (attr.HasFlag(FileAttributes.Archive))
				result += ((result.Length != 0) ? "," : "") + "A";
			if (attr.HasFlag(FileAttributes.Device))
				result += ((result.Length != 0) ? "," : "") + "Dv";
			if (attr.HasFlag(FileAttributes.Temporary))
				result += ((result.Length != 0) ? "," : "") + "T";
			if (attr.HasFlag(FileAttributes.SparseFile))
				result += ((result.Length != 0) ? "," : "") + "Sp";
			if (attr.HasFlag(FileAttributes.ReparsePoint))
				result += ((result.Length != 0) ? "," : "") + "RP";
			if (attr.HasFlag(FileAttributes.Compressed))
				result += ((result.Length != 0) ? "," : "") + "C";
			if (attr.HasFlag(FileAttributes.Offline))
				result += ((result.Length != 0) ? "," : "") + "OL";
			if (attr.HasFlag(FileAttributes.NotContentIndexed))
				result += ((result.Length != 0) ? "," : "") + "NC";
			if (attr.HasFlag(FileAttributes.Encrypted))
				result += ((result.Length != 0) ? "," : "") + "E";
			if (attr.HasFlag(FileAttributes.IntegrityStream))
				result += ((result.Length != 0) ? "," : "") + "IS";
			if (attr.HasFlag(FileAttributes.NoScrubData))
				result += ((result.Length != 0) ? "," : "") + "NS";

			return result;
		}
		/// <summary>
		/// ListViewのアイテムが選択された
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ListViewFile_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ListViewFile.SelectedItems.Count > 0)
			{
				// 詳細を表示する(先頭のみ)
				ListViewItem item = ListViewFile.SelectedItems[0];
				SetFileDetailInfo(
					item.SubItems[CHeaderFileName.Index].Text,
					item.SubItems[CHeaderType.Index].Text,
					item.SubItems[CHeaderCreateDate.Index].Text,
					item.SubItems[CHeaderUpdateDate.Index].Text,
					item.SubItems[CHeaderAccessDate.Index].Text,
					item.ImageIndex,
					(FileAttributes)Convert.ToUInt32(item.SubItems[CHeaderAttr.Index].Text, 16)
				);
                List<string> select_path = new List<string>();
                List<string> folder_path = new List<string>();

                // ディレクトリが選択されていた場合は、そのパスを開いてもらう
                for (int index = 0; index < ListViewFile.SelectedItems.Count; index++)
                {
                    ListViewItem lv_item = ListViewFile.SelectedItems[index];
                    FileAttributes attr = (FileAttributes)Convert.ToUInt32(lv_item.SubItems[CHeaderAttr.Index].Text, 16);
                    if (attr.HasFlag(FileAttributes.Directory) == false)
                    {   // リストに追加
						select_path.Add(System.IO.Path.Combine(m_Path, lv_item.SubItems[CHeaderFileName.Index].Text));
                    }
					else
					{   // フォルダリストに追加
                        folder_path.Add(System.IO.Path.Combine(m_Path, lv_item.SubItems[CHeaderFileName.Index].Text));

                    }
                }
				// ファイルの選択通知
                if (select_path.Count > 0)
                {	// 全て通知
                    OnFileSelectedEvent(select_path.ToArray(),false);
                }
				if (folder_path.Count > 0)
				{	// 全て通知
					OnFolderSelectedEvent(folder_path.ToArray());
				}
            }
		}
		/// <summary>
		/// 画像ファイル拡張子
		/// </summary>
		private string[] ImageExtensions = new string[]
		{
			".jpg",".jpeg",".bmp",".png",".tif",".tiff"
		};
		/// <summary>
		/// 詳細情報を設定
		/// </summary>
		/// <param name="filename">ファイル名</param>
		/// <param name="filekind">ファイル種別</param>
		/// <param name="createData">作成日時</param>
		/// <param name="updateDate">更新日時</param>
		/// <param name="accessDate">最終アクセス日時</param>
		/// <param name="iconIndex">アイコンIndex</param>
		/// <param name="attr">ファイル属性</param>
		private void SetFileDetailInfo(string filename,string filekind,string createData,string updateDate,string accessDate,int iconIndex,FileAttributes attr)
		{
			LbFileName.Text = filename;
			LbKind.Text = filekind;
			LbCreateDate.Text = createData;
			LbUpdateDate.Text = updateDate;
			LbAccessDate.Text = accessDate;
			LbAttribute.Text = AttributeToString(attr);
			string fullpath = System.IO.Path.Combine(m_Path,filename);
			Bitmap bmp = null;
			if ((File.Exists(fullpath)) && (ImageExtensions.Contains(System.IO.Path.GetExtension(fullpath)))) 
			{   // 画像ファイルなので読み込んで表示する
				try
				{
					using (FileStream fs = new FileStream(fullpath, FileMode.Open, FileAccess.Read))
					{
						bmp = new Bitmap(fs);
					}
				}
				catch { bmp = null; }
			}
			// 画像を設定
			if (PbFileImage.Image != null)
			{
				PbFileImage.Image.Dispose();
				PbFileImage.Image = null;
			}
			if (bmp != null)
				PbFileImage.Image = bmp;
			else
				PbFileImage.Image = JumboIconList.Images[iconIndex];
		}
		/// <summary>
		/// 詳細情報をクリアする
		/// </summary>
		private void ClearFileDetailInfo()
		{
			if (PbFileImage.Image != null)
			{
				PbFileImage.Image.Dispose();
				PbFileImage.Image = null;
			}
			LbFileName.Text = "";
			LbKind.Text = "";
			LbCreateDate.Text = "";
			LbUpdateDate.Text = "";
			LbAccessDate.Text = "";
			LbAttribute.Text = "";
		}


		/// <summary>
		/// ListViewのサイズが変わった
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ListViewFile_ClientSizeChanged(object sender, EventArgs e)
		{
			if (ListViewFile.View == System.Windows.Forms.View.Tile)
			{   // タイル表示の場合のみ
				ListViewFile.BeginUpdate();
				if (View == FILE_VIEW.TileLarge)
				{
					ListViewFile.TileSize = new Size(CalcTileWidth(), ExtraLargeIconList.ImageSize.Height + 16);
				}
				else if (View == FILE_VIEW.Tile)
				{
					ListViewFile.TileSize = new Size(CalcTileWidth(), LargeIconList.ImageSize.Height + 4);
				}
				ListViewFile.EndUpdate();
			}
		}
		/// <summary>
		/// カラムをクリックされた
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ListViewFile_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if ((e.Column >= 0) && (e.Column < 6))
			{   // 対象のカラム
				if (ListViewFile.Columns[e.Column].ImageKey == BLANK_IMG_KEY)
				{	// 昇順に設定
					SetSort(e.Column,SortOrder.Ascending);
				}
				else if (ListViewFile.Columns[e.Column].ImageKey == DOWNARROW_IMG_KEY)
				{	// 降順に設定
					SetSort(e.Column, SortOrder.Descending);
				}
				else if (ListViewFile.Columns[e.Column].ImageKey == UPARROW_IMG_KEY)
				{	// 昇順に設定
					SetSort(e.Column, SortOrder.Ascending);
				}
			}
		}
		/// <summary>
		/// 並び替え方法を設定する
		/// </summary>
		/// <param name="index">設定するカラムインデックス</param>
		/// <param name="order">並び替え順</param>
		private  void SetSort(int index,SortOrder order)
		{
			ListViewFile.BeginUpdate();
			for (int i = 0; i < CHeaderAttr.Index; i ++)
			{
				if (i != index)
					ListViewFile.Columns[i].ImageKey = BLANK_IMG_KEY;
				else if (order == SortOrder.Ascending)
					ListViewFile.Columns[i].ImageKey = DOWNARROW_IMG_KEY;
				else if (order == SortOrder.Descending)
					ListViewFile.Columns[i].ImageKey = UPARROW_IMG_KEY;
				else
					ListViewFile.Columns[i].ImageKey = BLANK_IMG_KEY;
			}
			if (ListViewFile.Columns[index].Tag is ListItemSorter sorter)
			{
				sorter.SortOrder = order;
				ListViewFile.ListViewItemSorter = sorter;
				// 強制的に並び替え(オブジェクトが変わっていない事があるので)
				ListViewFile.Sort();
			}
			ListViewFile.EndUpdate();
		}
		/// <summary>
		/// ダブルクリックされた
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ListViewFile_DoubleClick(object sender, EventArgs e)
		{
			if (ListViewFile.SelectedItems.Count > 0)
			{
				List<string> select_path = new List<string>();

				// ディレクトリが選択されていた場合は、そのパスを開いてもらう
				for(int index = 0; index < ListViewFile.SelectedItems.Count; index ++)
				{
					ListViewItem item = ListViewFile.SelectedItems[index];
					FileAttributes attr = (FileAttributes)Convert.ToUInt32(item.SubItems[CHeaderAttr.Index].Text, 16);
					if (attr.HasFlag(FileAttributes.Directory))
					{	// イベントを発行
						OnChangeDirectoryEvent(System.IO.Path.Combine(m_Path, item.SubItems[CHeaderFileName.Index].Text), m_TopNode);
						// 抜ける
						return;
					}
					select_path.Add(System.IO.Path.Combine(m_Path, item.SubItems[CHeaderFileName.Index].Text));
				}
				if (select_path.Count > 0)
				{
					// 全て通知
					OnFileSelectedEvent(select_path.ToArray(),true);
				}
			}
		}

		/// <summary>
		/// 選択要求リスト
		/// </summary>
		private List<string> _selectRequest = new List<string>();

		/// <summary>
		/// 指定されたファイルを選択状態にする
		/// </summary>
		/// <param name="filenames"></param>
		/// <returns></returns>
		public bool SetSelect(string[] filenames)
		{
			// 選択要求リストをクリア
            _selectRequest.Clear();

            foreach (string filename in filenames)
			{
                _selectRequest.Add(System.IO.Path.GetFileName(filename));
			}
			// この段階で一旦Select状態にする
			bool result = false;
			foreach(ListViewItem item in ListViewFile.Items)
			{
				bool check = (_selectRequest.Contains(item.SubItems[CHeaderFileName.Index].Text));
				item.Selected = check;
				result |= check;
			}
			return result;
		}
		/// <summary>
		/// 選択対象フラグ
		/// </summary>
		[Flags]
		public enum SELECT_FLAGS
		{
			FILE = 1,
			FOLDER = 2,
			ALL = 3
		}
		/// <summary>
		/// 選択されているアイテムを取得する
		/// </summary>
		/// <returns></returns>
		public string[] GetSelected(SELECT_FLAGS flags = SELECT_FLAGS.ALL)
		{
			if (ListViewFile.SelectedItems.Count > 0)
			{
				List<string> selected_item = new List<string>();
				foreach(ListViewItem item in ListViewFile.SelectedItems)
				{
					if (item.Selected)
					{
                        FileAttributes attr = (FileAttributes)Convert.ToUInt32(item.SubItems[CHeaderAttr.Index].Text, 16);
						if ((attr.HasFlag(FileAttributes.Directory)) && (flags.HasFlag(SELECT_FLAGS.FOLDER)))
							selected_item.Add(System.IO.Path.Combine(m_Path, item.SubItems[CHeaderFileName.Index].Text));
						else if (flags.HasFlag(SELECT_FLAGS.FILE))
                            selected_item.Add(System.IO.Path.Combine(m_Path, item.SubItems[CHeaderFileName.Index].Text));
                    }
                }
				if (selected_item.Count > 0) 
					return selected_item.ToArray();
			}
			return null;
		}



		/// <summary>
		/// 新規フォルダボタンの表示・非表示
		/// </summary>
		public bool ShowNewFolderButton
		{
			get => BtNewFolder.Visible;
			set => BtNewFolder.Visible = value;
		}
		/// <summary>
		/// 名前の末尾にあるSuffixを取得する
		/// </summary>
		/// <param name="text"></param>
		/// <param name="suffix"></param>
		/// <returns></returns>
		private bool GetSuffixNum(string text,out int suffix)
		{
			suffix = -1;
            Match match = Regex.Match(text, @".+_(\((\d+)\)|(\d+))$");
            if ((match != null) && (match.Success) && (match.Groups.Count >= 4))
            {
                string num_text = (match.Groups[2].Success) ? match.Groups[2].Value :
                    (match.Groups[3].Success) ? match.Groups[3].Value : null;
				if ((string.IsNullOrEmpty(num_text) == false) &&
					(int.TryParse(num_text, out int value)))
				{
					suffix = value;
					return true;
				}
            }
            return false;
        }
		/// <summary>
		/// Suffixを追加した名前を取得
		/// </summary>
		/// <param name="names"></param>
		/// <param name="default_name"></param>
		/// <returns></returns>
		private string GetSuffixName(string[] names,string default_name)
		{
			if (names.Length > 0)
			{
                Array.Sort(names);
                int? suffix = null;
                for (int index = names.Length - 1; index >= 0; index--)
                {
                    string name = System.IO.Path.GetFileNameWithoutExtension(names[index]);
                    if (name.StartsWith(default_name))
                    {
                        if (GetSuffixNum(name, out int suffix_value))
                        {
                            suffix = suffix_value;
                            break;
                        }
                        else if (name == default_name)
                        {
                            suffix = 0;
                            break;
                        }
                    }
                }
                if (suffix.HasValue)
                {
                    default_name = string.Format("{0}_({1})", default_name, suffix.Value + 1);
                }
            }
            return default_name;
		}


        /// <summary>
        /// 新規フォルダを作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtNewFolder_Click(object sender, EventArgs e)
        {
			string new_foldername = "新規フォルダ";
			// 同じ名前のフォルダがあったら、番号を末尾に付ける
			string[] now_dirs = Directory.GetDirectories(m_Path,new_foldername + "*");
			new_foldername = GetSuffixName(now_dirs,new_foldername);

            try
			{
				// ディレクトリ作成
				Directory.CreateDirectory(System.IO.Path.Combine(m_Path, new_foldername));
				// 再表示
				SetPath(m_Path);

				// ラベル編集可能にする
				ListViewFile.LabelEdit = true;
				// 追加したフォルダを編集可能状態にする
				bool isEdit = false;
				foreach (ListViewItem item in ListViewFile.Items)
                {
                    if (item.Text == new_foldername)
					{
						item.BeginEdit();
						isEdit = true;
						break;
					}
                }
				if (isEdit == false)
				{   // 見つからない...ラベル編集不可
                    ListViewFile.LabelEdit = false;
                }
            }
            catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(),"ディレクトリ作成エラー",MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }

		/// <summary>
		/// 編集しているIndex
		/// </summary>
		int? _editting_index = null;
		/// <summary>
		/// 編集前の内容
		/// </summary>
		string _beforeText = null;
        /// <summary>
        /// ラベル編集開始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewFile_BeforeLabelEdit(object sender, LabelEditEventArgs e)
		{
			if ((e.Item >= 0) && (e.Item < ListViewFile.Items.Count))
			{
				_editting_index = e.Item;
				_beforeText = ListViewFile.Items[e.Item].SubItems[CHeaderFileName.Index].Text;
			}
        }
        /// <summary>
        /// ラベル編集終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewFile_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
			if (_editting_index.HasValue) 
			{
				if ((_beforeText != null) && (e.Label != null) && (_beforeText != e.Label))
				{   // 入力されて名前が変わった

                    FileAttributes attr = (FileAttributes)Convert.ToUInt32(ListViewFile.Items[e.Item].SubItems[CHeaderAttr.Index].Text, 16);
					if (attr.HasFlag(FileAttributes.System) == false)
					{

						if (attr.HasFlag(FileAttributes.Directory))
						{   // ディレクトリ名の変更
							Directory.Move(
								System.IO.Path.Combine(m_Path, _beforeText),
								System.IO.Path.Combine(m_Path, e.Label));
						}
						else
						{   // ファイル名の変更
							File.Move(
								System.IO.Path.Combine(m_Path, _beforeText),
								System.IO.Path.Combine(m_Path, e.Label));
						}
						ListViewItem item = ListViewFile.Items[e.Item];
                        // ListViewの中身も変える
                        item.SubItems[CHeaderFileName.Index].Text = e.Label;
						// 詳細を更新
                        SetFileDetailInfo(
							item.SubItems[CHeaderFileName.Index].Text,
							item.SubItems[CHeaderType.Index].Text,
							item.SubItems[CHeaderCreateDate.Index].Text,
							item.SubItems[CHeaderUpdateDate.Index].Text,
							item.SubItems[CHeaderAccessDate.Index].Text,
							item.ImageIndex,
							(FileAttributes)Convert.ToUInt32(item.SubItems[CHeaderAttr.Index].Text, 16)
						);

                    }
                    else
					{	// 編集をキャンセルする
						e.CancelEdit = true;
					}
				}
				_editting_index = null;
				_beforeText = null;
			}
            // ラベル編集不可
            ListViewFile.LabelEdit = false;
        }
		/// <summary>
		/// メニューから新規ファイルを作成
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void ToolStripMenuItemNewFile_Click(object sender, EventArgs e)
        {
            string new_filename = "新規ファイル";
            // 同じ名前のフォルダがあったら、番号を末尾に付ける
            string[] now_dirs = Directory.GetFiles(m_Path, new_filename + "*");
            new_filename = GetSuffixName(now_dirs, new_filename) + ".txt";

			try
			{	// ファイルを作成
				FileStream fs = File.Create(System.IO.Path.Combine(m_Path,new_filename));
				fs.Close();
                // 再表示
                SetPath(m_Path);

                // ラベル編集可能にする
                ListViewFile.LabelEdit = true;
                // 追加したフォルダを編集可能状態にする
                bool isEdit = false;
                foreach (ListViewItem item in ListViewFile.Items)
                {
                    if (item.Text == new_filename)
                    {
                        item.BeginEdit();
                        isEdit = true;
                        break;
                    }
                }
                if (isEdit == false)
                {   // 見つからない...ラベル編集不可
                    ListViewFile.LabelEdit = false;
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

        }
        /// <summary>
        /// メニューから新規フォルダを作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemNewFolder_Click(object sender, EventArgs e)
        {
			// 新規フォルダ作成ボタンを呼び出す
			BtNewFolder_Click(sender, e);
        }
		/// <summary>
		/// メニューから名前を変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void ToolStripMenuItemRename_Click(object sender, EventArgs e)
        {
			// 選択されているアイテムがあるかチェック
			if (ListViewFile.SelectedItems.Count > 0)
			{
				// 最初のもののみ編集可能
				ListViewItem item = ListViewFile.SelectedItems[0];
                if (item != null)
                {
                    // ラベル編集可能にする
                    ListViewFile.LabelEdit = true;
					// 選択されたものの編集開始
					item.BeginEdit();
                }
            }
        }
		/// <summary>
		/// メニューから削除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
			throw new NotImplementedException();
        }
    }
}
