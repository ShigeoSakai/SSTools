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
	/// <summary>
	/// フォルダTreeView
	/// </summary>
	public partial class FolderTreeView : TreeView 
	{
		/// <summary>
		/// Root Node
		/// </summary>
		private FolderTreeNode rootNode = null;
		/// <summary>
		/// 最初に拡張されたか
		/// </summary>
		private bool first_expand = false;
		/// <summary>
		/// 隠しフォルダを表示するか
		/// </summary>
		private bool m_ShowHiddenFolder = false;
		/// <summary>
		/// 隠しフォルダを表示するか(プロパティ)
		/// </summary>
		public bool ShowHiddenFolder
		{
			get => m_ShowHiddenFolder;
			set
			{
				m_ShowHiddenFolder = value;
			}
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <remarks>
		/// ルートフォルダをDesktopで生成する
		/// </remarks>
		public FolderTreeView() : this(Environment.SpecialFolder.Desktop)
		{
		}
        /// <summary>
        /// コンストラクタ(ルートフォルダ指定)
        /// </summary>
        /// <param name="rootFolder">ルートフォルダ指定</param>
        public FolderTreeView(Environment.SpecialFolder rootFolder)
		{
			InitializeComponent();
			// Iconリストの初期化
			SystemIconManager.Init(SystemIconManager.ICON_SIZE.SMALL);
			SystemIconManager.InitImageList(ref IconImageList);

			ImageList = IconImageList;

			LoadRootNodes(rootFolder);
		}
        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
			if (rootNode != null)
			{
				rootNode.Dispose();
				rootNode.DisposeRoot();
                rootNode = null;
            }


            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		/// <summary>
		/// Tree Node
		/// </summary>
		/// <remarks>
		/// プロパティエディタで非表示にする
		/// </remarks>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new TreeNodeCollection Nodes
		{
			get { return base.Nodes; }
		}
		/// <summary>
		/// ルートフォルダを指定してTreeNodeを作成する
		/// </summary>
		/// <param name="folder">ルートフォルダ(デフォルト:Desktop)</param>
		private void LoadRootNodes(Environment.SpecialFolder folder = Environment.SpecialFolder.Desktop)
		{
			rootNode = new FolderTreeNode(ref IconImageList, ShowHiddenFolder, folder);
			// Add the root node to the tree.
			Nodes.Clear();
			Nodes.Add(rootNode);
		}
		/// <summary>
		/// 表示・非表示が変更になった
		/// </summary>
		/// <param name="e">イベント引数</param>
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);

			if ((first_expand == false) && (Visible))
			{
				rootNode.Expand();
				first_expand = true;
			}
		}
        /// <summary>
        /// Tree展開前イベント処理
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
		{
			if ((first_expand) && (e.Node != null) && (e.Node is FolderTreeNode node))
			{
				node.ExpandSubFolder(ref IconImageList, ShowHiddenFolder);
			}
			base.OnBeforeExpand(e);
		}
		/// <summary>
		/// Node選択イベントハンドラ
		/// </summary>
		/// <param name="sender">送信元</param>
		/// <param name="path">パス</param>
		/// <param name="fullpath">フルパス</param>
		/// <param name="topNode">Top Node</param>
		public delegate void OnSelectNodeEventHandler(object sender, string path, string fullpath, FolderTreeNode topNode);
		/// <summary>
		/// Node選択イベント
		/// </summary>
		/// <remarks>
		/// Nodeが選択された時に、パスを通知します。
		/// </remarks>
		public event OnSelectNodeEventHandler SelectNodeEvent;
        /// <summary>
        /// Node選択イベント発行
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="fullpath">フルパス</param>
        /// <param name="topNode">Top Node</param>
        protected virtual void OnSelectNode(string path, string fullpath, FolderTreeNode topNode)
		{
            // 現在のパスを設定
            CurrentPath = fullpath;
            // イベント発行
			SelectNodeEvent?.Invoke(this, path, fullpath, topNode);
		}
        /// <summary>
        /// Node選択後イベント処理
        /// </summary>
        /// <param name="e">イベント引数</param>
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

		/// <summary>
		/// 更新中か？
		/// </summary>
		private bool isUpdating = false;
		/// <summary>
		/// 更新開始
		/// </summary>
		public new void BeginUpdate()
		{
			isUpdating = true;
			base.BeginUpdate();
		}
		/// <summary>
		/// 更新終了
		/// </summary>
		public new void EndUpdate()
		{
			base.EndUpdate();
			isUpdating = false;
		}
        /// <summary>
        /// ルートフォルダの変更
        /// </summary>
        /// <param name="folder">ルートフォルダ</param>
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
		/// <summary>
		/// フォルダを選択
		/// </summary>
		/// <param name="path">選択するパス</param>
		/// <param name="topNode">Top Node</param>
		public void SelectFolder(string path, FolderTreeNode topNode = null)
		{
			FolderTreeNode op_node = topNode ?? rootNode;
			BeginUpdate();
			//op_node.Collapse(false);
			FolderTreeNode node = op_node.FindPath(path, ref IconImageList, op_node, ShowHiddenFolder);
			if ((node != null) && ((SelectedNode == null) || (SelectedNode.Equals(node) == false)))
			{
				node.ExpandToTopNode();
				SelectedNode = node;
				// イベント発行
				OnSelectNode(node.Text, node.ShellItem.Path, topNode);
			}
			else
			{
				op_node.Expand();
				// 現在のパスを設定
				CurrentPath = path;
            }
			EndUpdate();
		}
		/// <summary>
		/// 現在選択されているパス
		/// </summary>
		public string CurrentPath { get; set; } = string.Empty;

	}
}
