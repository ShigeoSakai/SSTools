using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSTools
{
	/// <summary>
	/// フォルダTree Nodeクラス
	/// </summary>
	public class FolderTreeNode : TreeNode, IDisposable
	{
		/// <summary>
		/// Shell Item
		/// </summary>
		public ShellItem ShellItem { get; private set; } = null;
		/// <summary>
		/// サブフォルダはダミー登録か？
		/// </summary>
		public bool SubFolderIsDummy { get; set; } = false;

		/// <summary>
		/// DeskTop Shell Item
		/// </summary>
		private ShellItem m_shDesktop = null;
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="imageList">画像リスト(アイコン表示用)</param>
		/// <param name="isShowHidenFolder">隠しフォルダの表示・非表示</param>
		/// <param name="folder">ルートフォルダ</param>
		public FolderTreeNode(ref ImageList imageList, bool isShowHidenFolder,
			Environment.SpecialFolder folder = Environment.SpecialFolder.Desktop) : base()
		{
			// Create the root shell item.
			m_shDesktop = new ShellItem(folder);
			CreateNode(m_shDesktop, ref imageList);

			// Now we need to add any children to the root node.
			ArrayList arrChildren = m_shDesktop.GetSubFolders(isShowHidenFolder);
			foreach (ShellItem shChild in arrChildren)
			{
				FolderTreeNode childNode = new FolderTreeNode(shChild, ref imageList);
				// If this is a folder item and has children then add a place holder node.
				if (shChild.IsFolder && shChild.HasSubFolder)
				{
					childNode.SubFolderIsDummy = true;
					childNode.Nodes.Add("PH");
				}
				Nodes.Add(childNode);
			}
		}
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="shellItem">Shell Item</param>
        /// <param name="imageList">画像リスト(アイコン表示用)</param>
        public FolderTreeNode(ShellItem shellItem, ref ImageList imageList) :base() { CreateNode(shellItem, ref imageList); }


        /// <summary>
        /// Nodeの生成
        /// </summary>
        /// <param name="shellItem">Shell Item</param>
        /// <param name="imgList">画像リスト(アイコン表示用)</param>
        private void CreateNode(ShellItem shellItem, ref ImageList imageList)
		{
			if (shellItem != null)
			{
				Text = shellItem.DisplayName;
				ShellItem = shellItem;
				// アイコンを取得
				int icon = SystemIconManager.StoreImageList(ref imageList, ShellItem.IconIndex);
				int open_icon = icon;
				if (ShellItem.OpenIcon != IntPtr.Zero)
					open_icon = SystemIconManager.StoreOpenIconImageList(ref imageList, ShellItem.OpenIcon, ShellItem.OpenIconIndex);
				// アイコンを登録
				ImageIndex = icon;
				SelectedImageIndex = open_icon;
			}
		}

		/// <summary>
		/// デストラクタ
		/// </summary>
		public void Dispose()
		{
			// サブノードのクリア
			if (Nodes.Count > 0)
			{
				foreach(TreeNode node in Nodes)
				{
                    if (node is FolderTreeNode treeNode)
						treeNode.Dispose();
                }
				Nodes.Clear();
			}
		}
		/// <summary>
		/// Root Shell Itemを解放する
		/// </summary>
		public void DisposeRoot()
		{
            if (m_shDesktop != null)
            {
                m_shDesktop.Dispose();
                m_shDesktop = null;
            }
            // ShellItemのクリア
            if (ShellItem != null)
            {
                ShellItem.Dispose();
                ShellItem = null;
            }
        }

        /// <summary>
        /// サブフォルダの展開
        /// </summary>
        /// <param name="imgList">画像リスト(アイコン表示用)</param>
        /// <param name="isShowHidenFolder">隠しフォルダの表示・非表示</param>
        public void ExpandSubFolder(ref ImageList imgList, bool isShowHidenFolder = false)
		{
			if (SubFolderIsDummy)
			{
				Nodes.Clear();
				ShellItem shellItem = ShellItem;
				if (shellItem != null)
				{
					ArrayList arrSub = shellItem.GetSubFolders(isShowHidenFolder);
					foreach (ShellItem shChild in arrSub)
					{
						FolderTreeNode childNode = new FolderTreeNode(shChild, ref imgList);
						// If this is a folder item and has children then add a place holder node.
						if (shChild.IsFolder && shChild.HasSubFolder)
						{
							childNode.SubFolderIsDummy = true;
							childNode.Nodes.Add("PH");
						}
						Nodes.Add(childNode);
					}
				}
				SubFolderIsDummy = false;
			}
		}
		/// <summary>
		/// Top Nodeの取得
		/// </summary>
		/// <param name="root">現在のNode</param>
		/// <returns>親Node</returns>
		/// <remarks>
		///	　再帰呼び出しをする
		/// </remarks>
		public FolderTreeNode GetTopOfNode(FolderTreeNode root)
		{
			if ((Parent == null) || (Parent.Equals(root)))
				return this;
			if (Parent is FolderTreeNode parentNode)
				return parentNode.GetTopOfNode(root);
			return null;

		}
		/// <summary>
		/// 親Nodeから全て転換する
		/// </summary>
		public void ExpandToTopNode()
		{
			TreeNode node = this;
			while (node.Parent != null)
			{
				node.Parent.Expand();
				node = node.Parent;
			}
		}
        /// <summary>
        /// 指定パスのNodeを検索する
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="imageList">画像リスト(アイコン表示用)</param>
        /// <param name="topNode">Top Node</param>
        /// <param name="isShowHidden">隠しフォルダの表示・非表示</param>
        /// <returns>指定パスのNode。見つからない場合はnull</returns>
        public FolderTreeNode FindPath(string path, ref ImageList imageList, FolderTreeNode topNode = null, bool isShowHidden = false)
		{
			FolderTreeNode op_node = topNode ?? this;
			CHECK_RESULT result = op_node.CheckNode(path, out _);
			if (result == CHECK_RESULT.MATCH)
				return op_node;
			else if ((result == CHECK_RESULT.PARTIAL_MATCH) || (result == CHECK_RESULT.VIRTUAL_FOLDER))
				return op_node.FindChild(path, ref imageList, isShowHidden);
			return null;
		}
		/// <summary>
		/// チェック結果Enum
		/// </summary>
		private enum CHECK_RESULT
		{
			MATCH = 1,			//!< 一致
			PARTIAL_MATCH = 2,	//!< 部分一致
			VIRTUAL_FOLDER = 3,	//!< 仮想フォルダ
			NOT_MATCH = 0,		//!< 不一致
		}
		/// <summary>
		/// 現在のNodeと一致するかチェック
		/// </summary>
		/// <param name="path">パス</param>
		/// <param name="match_length">一致した長さ</param>
		/// <returns>チェック結果</returns>
		private CHECK_RESULT CheckNode(string path, out int match_length)
		{
			match_length = 0;
			if (path.EndsWith("\\"))
				path.Substring(0, path.Length - 1);

			string now_path = ShellItem.Path;
			if ((now_path == null) || (now_path.Length == 0))
				return CHECK_RESULT.VIRTUAL_FOLDER;
			else if ((now_path.Length < path.Length) && (path.StartsWith(now_path)))
			{
				match_length = now_path.Length;
				return CHECK_RESULT.PARTIAL_MATCH;
			}
			else if (now_path == path)
				return CHECK_RESULT.MATCH;
			else if (Parent == null)
				return CHECK_RESULT.PARTIAL_MATCH;
			else if (ShellItem.DisplayName == System.IO.Path.GetFileName(path))
                return CHECK_RESULT.MATCH;
            return CHECK_RESULT.NOT_MATCH;
		}

        /// <summary>
        /// 子Nodeの検索
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="imageList">画像リスト(アイコン表示用)</param>
        /// <param name="isShowHidden">隠しフォルダの表示・非表示</param>
        /// <returns>指定パスのNode。見つからない場合は自分を返す</returns>
        private FolderTreeNode FindChild(string path, ref ImageList imageList, bool isShowHidden = false)
		{
			if (SubFolderIsDummy)
			{   // 子を取得
				Nodes.Clear();
				ShellItem shellItem = ShellItem;
				if (shellItem != null)
				{
					ArrayList arrSub = shellItem.GetSubFolders(isShowHidden);
					foreach (ShellItem shChild in arrSub)
					{
						FolderTreeNode childNode = new FolderTreeNode(shChild, ref imageList);
						// If this is a folder item and has children then add a place holder node.
						if (shChild.IsFolder && shChild.HasSubFolder)
						{
							childNode.SubFolderIsDummy = true;
							childNode.Nodes.Add("PH");
						}
						Nodes.Add(childNode);
					}
				}
				SubFolderIsDummy = false;
			}
			if (Nodes.Count > 0)
			{
				FolderTreeNode find_node = null;
				int max_match_len = 0;
				foreach (TreeNode childNode in Nodes)
				{
					if (childNode is FolderTreeNode tree_node)
					{
						CHECK_RESULT result = tree_node.CheckNode(path, out int match_len);
						if (result == CHECK_RESULT.MATCH)
							return tree_node;
						if (result == CHECK_RESULT.PARTIAL_MATCH)
						{
							if (max_match_len < match_len)
								find_node = tree_node;
						}
					}
				}
				if (find_node != null)
				{
					return find_node.FindPath(path, ref imageList, null, isShowHidden);
				}
				foreach (TreeNode childNode in Nodes)
				{
					if (childNode is FolderTreeNode tree_node)
					{
						CHECK_RESULT result = tree_node.CheckNode(path, out _);
						if (result == CHECK_RESULT.VIRTUAL_FOLDER)
							return tree_node.FindPath(path, ref imageList, null, isShowHidden);
					}
				}
			}
			return this;
		}
	}
}
