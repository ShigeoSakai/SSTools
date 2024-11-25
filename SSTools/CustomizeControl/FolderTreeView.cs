using SSTools.CustomizeControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSTools
{
	public partial class FolderTreeView : TreeView
	{
		private FolderTreeNode rootNode = null;
		private bool first_expand = false;


		private bool m_ShowHiddenFolder = false;
		public bool ShowHiddenFolder
		{
			get => m_ShowHiddenFolder;
			set
			{
				m_ShowHiddenFolder = value;
			}
		}

		public FolderTreeView() : this(Environment.SpecialFolder.Desktop)
		{
		}
		public FolderTreeView(Environment.SpecialFolder rootFolder)
		{
			InitializeComponent();
			// Iconリストの初期化
			SystemIconManager.Init(SystemIconManager.ICON_SIZE.SMALL);
			SystemIconManager.InitImageList(ref IconImageList);

			ImageList = IconImageList;

			LoadRootNodes(rootFolder);
		}
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new TreeNodeCollection Nodes
		{
			get { return base.Nodes; }
		}
		/// <summary>
		/// Loads the root TreeView nodes.
		/// </summary>
		private void LoadRootNodes(Environment.SpecialFolder folder = Environment.SpecialFolder.Desktop)
		{
			rootNode = new FolderTreeNode(ref IconImageList, ShowHiddenFolder, folder);
			// Add the root node to the tree.
			Nodes.Clear();
			Nodes.Add(rootNode);
		}
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);

			if ((first_expand == false) && (Visible))
			{
				rootNode.Expand();
				first_expand = true;
			}
		}
		protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
		{
			if ((first_expand) && (e.Node != null) && (e.Node is FolderTreeNode node))
			{
				node.ExpandSubFolder(ref IconImageList, ShowHiddenFolder);
			}
			base.OnBeforeExpand(e);
		}

		public delegate void OnSelectNodeEventHandler(object sender, string path, string fullpath, FolderTreeNode topNode);
		public event OnSelectNodeEventHandler SelectNodeEvent;
		protected virtual void OnSelectNode(string path, string fullpath, FolderTreeNode topNode)
		{
			SelectNodeEvent?.Invoke(this, path, fullpath, topNode);
		}

		protected override void OnAfterSelect(TreeViewEventArgs e)
		{
			if (isUpdating == false)
			{
				if ((e.Node != null) && (e.Node is FolderTreeNode node))
				{
					FolderTreeNode topNode = node.GetTopOfNode(rootNode);
					OnSelectNode(node.Text, node.ShellItem.Path, topNode);
				}
			}
			base.OnAfterSelect(e);
		}


		private bool isUpdating = false;

		public new void BeginUpdate()
		{
			isUpdating = true;
			base.BeginUpdate();
		}
		public new void EndUpdate()
		{
			base.EndUpdate();
			isUpdating = false;
		}
		public void ChangeRoot(Environment.SpecialFolder folder)
		{
			BeginUpdate();

			if (rootNode != null)
			{
				rootNode.Dispose();
				rootNode = null;
			}

			LoadRootNodes(folder);
			rootNode.Expand();

			EndUpdate();
		}

		public void SelectFolder(string path, FolderTreeNode topNode = null)
		{
			FolderTreeNode op_node = topNode ?? rootNode;
			BeginUpdate();
			//op_node.Collapse(false);
			FolderTreeNode node = op_node.FindPath(path, ref IconImageList, op_node, ShowHiddenFolder);
			if ((node != null) && (SelectedNode.Equals(node) == false))
			{
				node.ExpandToTopNode();
				SelectedNode = node;
				// イベント発行
				OnSelectNode(node.Text, node.ShellItem.Path, topNode);
			}
			else
				op_node.Expand();
			EndUpdate();
		}
	}
}
