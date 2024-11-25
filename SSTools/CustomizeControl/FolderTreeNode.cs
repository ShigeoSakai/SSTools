using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSTools.CustomizeControl
{
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

		public FolderTreeNode(ref ImageList imageList, bool isShowHidenFolder,
			Environment.SpecialFolder folder = Environment.SpecialFolder.Desktop) : base()
		{
			// Create the root shell item.
			ShellItem m_shDesktop = new ShellItem(folder);
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
		/// <param name="shellItem"></param>
		/// <param name="imgList"></param>
		public FolderTreeNode(ShellItem shellItem, ref ImageList imageList) :base() => CreateNode(shellItem, ref imageList);



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
		/// <exception cref="NotImplementedException"></exception>
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
			// ShellItemのクリア
			if (ShellItem != null)
			{
				ShellItem.Dispose();
				ShellItem = null;
			}
		}
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
		public FolderTreeNode GetTopOfNode(FolderTreeNode root)
		{
			if ((Parent == null) || (Parent.Equals(root)))
				return this;
			if (Parent is FolderTreeNode parentNode)
				return parentNode.GetTopOfNode(root);
			return null;

		}
		public void ExpandToTopNode()
		{
			TreeNode node = this;
			while (node.Parent != null)
			{
				node.Parent.Expand();
				node = node.Parent;
			}
		}
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

		private enum CHECK_RESULT
		{
			MATCH = 1,
			PARTIAL_MATCH = 2,
			VIRTUAL_FOLDER = 3,
			NOT_MATCH = 0,
		}
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
			return CHECK_RESULT.NOT_MATCH;
		}


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
