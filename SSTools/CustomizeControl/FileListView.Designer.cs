namespace SSTools{
	partial class FileListView
	{
		/// <summary> 
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region コンポーネント デザイナーで生成されたコード

		/// <summary> 
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.LayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.BtNewFolder = new System.Windows.Forms.Button();
            this.RbLargeTile = new System.Windows.Forms.RadioButton();
            this.BtShrink = new System.Windows.Forms.Button();
            this.RbTile = new System.Windows.Forms.RadioButton();
            this.RbList = new System.Windows.Forms.RadioButton();
            this.RbDetail = new System.Windows.Forms.RadioButton();
            this.RbJumbo = new System.Windows.Forms.RadioButton();
            this.RbExtraLarge = new System.Windows.Forms.RadioButton();
            this.RbLarge = new System.Windows.Forms.RadioButton();
            this.CbView = new System.Windows.Forms.ComboBox();
            this.RbSmall = new System.Windows.Forms.RadioButton();
            this.SplitPanel = new System.Windows.Forms.SplitContainer();
            this.ListViewFile = new System.Windows.Forms.ListView();
            this.CHeaderFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CHeaderSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CHeaderCreateDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CHeaderUpdateDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CHeaderAccessDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CHeaderAttr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FileListViewMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemNewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemNewFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemRename = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.LargeIconList = new System.Windows.Forms.ImageList(this.components);
            this.SmallIconList = new System.Windows.Forms.ImageList(this.components);
            this.DedicateInfoPanel = new System.Windows.Forms.Panel();
            this.DedicateLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.LbAccessDate = new System.Windows.Forms.Label();
            this.LbUpdateDate = new System.Windows.Forms.Label();
            this.LbCreateDate = new System.Windows.Forms.Label();
            this.LbKind = new System.Windows.Forms.Label();
            this.LbAttribute = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.PbFileImage = new System.Windows.Forms.PictureBox();
            this.LbFileName = new System.Windows.Forms.Label();
            this.ExtraLargeIconList = new System.Windows.Forms.ImageList(this.components);
            this.JumboIconList = new System.Windows.Forms.ImageList(this.components);
            this.LayoutPanel.SuspendLayout();
            this.ControlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitPanel)).BeginInit();
            this.SplitPanel.Panel1.SuspendLayout();
            this.SplitPanel.Panel2.SuspendLayout();
            this.SplitPanel.SuspendLayout();
            this.FileListViewMenuStrip.SuspendLayout();
            this.DedicateInfoPanel.SuspendLayout();
            this.DedicateLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbFileImage)).BeginInit();
            this.SuspendLayout();
            // 
            // LayoutPanel
            // 
            this.LayoutPanel.ColumnCount = 1;
            this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.LayoutPanel.Controls.Add(this.ControlPanel, 0, 0);
            this.LayoutPanel.Controls.Add(this.SplitPanel, 0, 1);
            this.LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.LayoutPanel.Name = "LayoutPanel";
            this.LayoutPanel.RowCount = 2;
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPanel.Size = new System.Drawing.Size(625, 317);
            this.LayoutPanel.TabIndex = 0;
            // 
            // ControlPanel
            // 
            this.ControlPanel.Controls.Add(this.BtNewFolder);
            this.ControlPanel.Controls.Add(this.RbLargeTile);
            this.ControlPanel.Controls.Add(this.BtShrink);
            this.ControlPanel.Controls.Add(this.RbTile);
            this.ControlPanel.Controls.Add(this.RbList);
            this.ControlPanel.Controls.Add(this.RbDetail);
            this.ControlPanel.Controls.Add(this.RbJumbo);
            this.ControlPanel.Controls.Add(this.RbExtraLarge);
            this.ControlPanel.Controls.Add(this.RbLarge);
            this.ControlPanel.Controls.Add(this.CbView);
            this.ControlPanel.Controls.Add(this.RbSmall);
            this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlPanel.Location = new System.Drawing.Point(0, 0);
            this.ControlPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(625, 26);
            this.ControlPanel.TabIndex = 0;
            // 
            // BtNewFolder
            // 
            this.BtNewFolder.Location = new System.Drawing.Point(378, 2);
            this.BtNewFolder.Name = "BtNewFolder";
            this.BtNewFolder.Size = new System.Drawing.Size(86, 23);
            this.BtNewFolder.TabIndex = 10;
            this.BtNewFolder.Text = "新規フォルダ";
            this.BtNewFolder.UseVisualStyleBackColor = true;
            this.BtNewFolder.Visible = false;
            this.BtNewFolder.Click += new System.EventHandler(this.BtNewFolder_Click);
            // 
            // RbLargeTile
            // 
            this.RbLargeTile.Appearance = System.Windows.Forms.Appearance.Button;
            this.RbLargeTile.Location = new System.Drawing.Point(182, 0);
            this.RbLargeTile.Margin = new System.Windows.Forms.Padding(0);
            this.RbLargeTile.Name = "RbLargeTile";
            this.RbLargeTile.Size = new System.Drawing.Size(26, 26);
            this.RbLargeTile.TabIndex = 9;
            this.RbLargeTile.TabStop = true;
            this.RbLargeTile.UseVisualStyleBackColor = true;
            this.RbLargeTile.CheckedChanged += new System.EventHandler(this.RbView_CheckedChanged);
            // 
            // BtShrink
            // 
            this.BtShrink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtShrink.Location = new System.Drawing.Point(589, 2);
            this.BtShrink.Margin = new System.Windows.Forms.Padding(0);
            this.BtShrink.Name = "BtShrink";
            this.BtShrink.Size = new System.Drawing.Size(32, 23);
            this.BtShrink.TabIndex = 8;
            this.BtShrink.Text = ">>";
            this.BtShrink.UseVisualStyleBackColor = true;
            this.BtShrink.Click += new System.EventHandler(this.BtShrink_Click);
            // 
            // RbTile
            // 
            this.RbTile.Appearance = System.Windows.Forms.Appearance.Button;
            this.RbTile.Location = new System.Drawing.Point(156, 0);
            this.RbTile.Margin = new System.Windows.Forms.Padding(0);
            this.RbTile.Name = "RbTile";
            this.RbTile.Size = new System.Drawing.Size(26, 26);
            this.RbTile.TabIndex = 6;
            this.RbTile.TabStop = true;
            this.RbTile.UseVisualStyleBackColor = true;
            this.RbTile.CheckedChanged += new System.EventHandler(this.RbView_CheckedChanged);
            // 
            // RbList
            // 
            this.RbList.Appearance = System.Windows.Forms.Appearance.Button;
            this.RbList.Location = new System.Drawing.Point(130, 0);
            this.RbList.Margin = new System.Windows.Forms.Padding(0);
            this.RbList.Name = "RbList";
            this.RbList.Size = new System.Drawing.Size(26, 26);
            this.RbList.TabIndex = 5;
            this.RbList.TabStop = true;
            this.RbList.UseVisualStyleBackColor = true;
            this.RbList.CheckedChanged += new System.EventHandler(this.RbView_CheckedChanged);
            // 
            // RbDetail
            // 
            this.RbDetail.Appearance = System.Windows.Forms.Appearance.Button;
            this.RbDetail.Location = new System.Drawing.Point(104, 0);
            this.RbDetail.Margin = new System.Windows.Forms.Padding(0);
            this.RbDetail.Name = "RbDetail";
            this.RbDetail.Size = new System.Drawing.Size(26, 26);
            this.RbDetail.TabIndex = 4;
            this.RbDetail.TabStop = true;
            this.RbDetail.UseVisualStyleBackColor = true;
            this.RbDetail.CheckedChanged += new System.EventHandler(this.RbView_CheckedChanged);
            // 
            // RbJumbo
            // 
            this.RbJumbo.Appearance = System.Windows.Forms.Appearance.Button;
            this.RbJumbo.Location = new System.Drawing.Point(78, 0);
            this.RbJumbo.Margin = new System.Windows.Forms.Padding(0);
            this.RbJumbo.Name = "RbJumbo";
            this.RbJumbo.Size = new System.Drawing.Size(26, 26);
            this.RbJumbo.TabIndex = 3;
            this.RbJumbo.TabStop = true;
            this.RbJumbo.UseVisualStyleBackColor = true;
            this.RbJumbo.CheckedChanged += new System.EventHandler(this.RbView_CheckedChanged);
            // 
            // RbExtraLarge
            // 
            this.RbExtraLarge.Appearance = System.Windows.Forms.Appearance.Button;
            this.RbExtraLarge.Location = new System.Drawing.Point(52, 0);
            this.RbExtraLarge.Margin = new System.Windows.Forms.Padding(0);
            this.RbExtraLarge.Name = "RbExtraLarge";
            this.RbExtraLarge.Size = new System.Drawing.Size(26, 26);
            this.RbExtraLarge.TabIndex = 2;
            this.RbExtraLarge.TabStop = true;
            this.RbExtraLarge.UseVisualStyleBackColor = true;
            this.RbExtraLarge.CheckedChanged += new System.EventHandler(this.RbView_CheckedChanged);
            // 
            // RbLarge
            // 
            this.RbLarge.Appearance = System.Windows.Forms.Appearance.Button;
            this.RbLarge.Location = new System.Drawing.Point(26, 0);
            this.RbLarge.Margin = new System.Windows.Forms.Padding(0);
            this.RbLarge.Name = "RbLarge";
            this.RbLarge.Size = new System.Drawing.Size(26, 26);
            this.RbLarge.TabIndex = 1;
            this.RbLarge.TabStop = true;
            this.RbLarge.UseVisualStyleBackColor = true;
            this.RbLarge.CheckedChanged += new System.EventHandler(this.RbView_CheckedChanged);
            // 
            // CbView
            // 
            this.CbView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbView.FormattingEnabled = true;
            this.CbView.Location = new System.Drawing.Point(219, 4);
            this.CbView.Margin = new System.Windows.Forms.Padding(0);
            this.CbView.Name = "CbView";
            this.CbView.Size = new System.Drawing.Size(121, 20);
            this.CbView.TabIndex = 7;
            this.CbView.SelectionChangeCommitted += new System.EventHandler(this.CbView_SelectionChangeCommitted);
            // 
            // RbSmall
            // 
            this.RbSmall.Appearance = System.Windows.Forms.Appearance.Button;
            this.RbSmall.Location = new System.Drawing.Point(0, 0);
            this.RbSmall.Margin = new System.Windows.Forms.Padding(0);
            this.RbSmall.Name = "RbSmall";
            this.RbSmall.Size = new System.Drawing.Size(26, 26);
            this.RbSmall.TabIndex = 0;
            this.RbSmall.TabStop = true;
            this.RbSmall.UseVisualStyleBackColor = true;
            this.RbSmall.CheckedChanged += new System.EventHandler(this.RbView_CheckedChanged);
            // 
            // SplitPanel
            // 
            this.SplitPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitPanel.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.SplitPanel.Location = new System.Drawing.Point(3, 29);
            this.SplitPanel.Name = "SplitPanel";
            // 
            // SplitPanel.Panel1
            // 
            this.SplitPanel.Panel1.Controls.Add(this.ListViewFile);
            // 
            // SplitPanel.Panel2
            // 
            this.SplitPanel.Panel2.Controls.Add(this.DedicateInfoPanel);
            this.SplitPanel.Panel2MinSize = 240;
            this.SplitPanel.Size = new System.Drawing.Size(619, 285);
            this.SplitPanel.SplitterDistance = 375;
            this.SplitPanel.TabIndex = 3;
            // 
            // ListViewFile
            // 
            this.ListViewFile.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CHeaderFileName,
            this.CHeaderType,
            this.CHeaderSize,
            this.CHeaderCreateDate,
            this.CHeaderUpdateDate,
            this.CHeaderAccessDate,
            this.CHeaderAttr});
            this.ListViewFile.ContextMenuStrip = this.FileListViewMenuStrip;
            this.ListViewFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListViewFile.HideSelection = false;
            this.ListViewFile.LargeImageList = this.LargeIconList;
            this.ListViewFile.Location = new System.Drawing.Point(0, 0);
            this.ListViewFile.Margin = new System.Windows.Forms.Padding(0);
            this.ListViewFile.Name = "ListViewFile";
            this.ListViewFile.Size = new System.Drawing.Size(375, 285);
            this.ListViewFile.SmallImageList = this.SmallIconList;
            this.ListViewFile.TabIndex = 0;
            this.ListViewFile.UseCompatibleStateImageBehavior = false;
            this.ListViewFile.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.ListViewFile_AfterLabelEdit);
            this.ListViewFile.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.ListViewFile_BeforeLabelEdit);
            this.ListViewFile.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewFile_ColumnClick);
            this.ListViewFile.SelectedIndexChanged += new System.EventHandler(this.ListViewFile_SelectedIndexChanged);
            this.ListViewFile.ClientSizeChanged += new System.EventHandler(this.ListViewFile_ClientSizeChanged);
            this.ListViewFile.DoubleClick += new System.EventHandler(this.ListViewFile_DoubleClick);
            // 
            // CHeaderFileName
            // 
            this.CHeaderFileName.Text = "ファイル名";
            this.CHeaderFileName.Width = 116;
            // 
            // CHeaderType
            // 
            this.CHeaderType.Text = "種別";
            // 
            // CHeaderSize
            // 
            this.CHeaderSize.Text = "サイズ";
            this.CHeaderSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CHeaderCreateDate
            // 
            this.CHeaderCreateDate.Text = "作成日時";
            this.CHeaderCreateDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CHeaderUpdateDate
            // 
            this.CHeaderUpdateDate.Text = "更新日時";
            this.CHeaderUpdateDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CHeaderAccessDate
            // 
            this.CHeaderAccessDate.Text = "アクセス日時";
            this.CHeaderAccessDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CHeaderAttr
            // 
            this.CHeaderAttr.Text = "属性";
            this.CHeaderAttr.Width = 0;
            // 
            // FileListViewMenuStrip
            // 
            this.FileListViewMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemNewFile,
            this.ToolStripMenuItemNewFolder,
            this.toolStripSeparator2,
            this.ToolStripMenuItemRename,
            this.toolStripSeparator1,
            this.ToolStripMenuItemDelete});
            this.FileListViewMenuStrip.Name = "FileListViewMenuStrip";
            this.FileListViewMenuStrip.Size = new System.Drawing.Size(181, 126);
            // 
            // ToolStripMenuItemNewFile
            // 
            this.ToolStripMenuItemNewFile.Name = "ToolStripMenuItemNewFile";
            this.ToolStripMenuItemNewFile.Size = new System.Drawing.Size(180, 22);
            this.ToolStripMenuItemNewFile.Text = "ファイルを作成(&N)";
            this.ToolStripMenuItemNewFile.Click += new System.EventHandler(this.ToolStripMenuItemNewFile_Click);
            // 
            // ToolStripMenuItemNewFolder
            // 
            this.ToolStripMenuItemNewFolder.Name = "ToolStripMenuItemNewFolder";
            this.ToolStripMenuItemNewFolder.Size = new System.Drawing.Size(180, 22);
            this.ToolStripMenuItemNewFolder.Text = "フォルダを作成(&D)";
            this.ToolStripMenuItemNewFolder.Click += new System.EventHandler(this.ToolStripMenuItemNewFolder_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // ToolStripMenuItemRename
            // 
            this.ToolStripMenuItemRename.Name = "ToolStripMenuItemRename";
            this.ToolStripMenuItemRename.Size = new System.Drawing.Size(180, 22);
            this.ToolStripMenuItemRename.Text = "名前を変更(&R)";
            this.ToolStripMenuItemRename.Click += new System.EventHandler(this.ToolStripMenuItemRename_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // ToolStripMenuItemDelete
            // 
            this.ToolStripMenuItemDelete.Enabled = false;
            this.ToolStripMenuItemDelete.Name = "ToolStripMenuItemDelete";
            this.ToolStripMenuItemDelete.Size = new System.Drawing.Size(180, 22);
            this.ToolStripMenuItemDelete.Text = "削除(&X)";
            this.ToolStripMenuItemDelete.Click += new System.EventHandler(this.ToolStripMenuItemDelete_Click);
            // 
            // LargeIconList
            // 
            this.LargeIconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.LargeIconList.ImageSize = new System.Drawing.Size(16, 16);
            this.LargeIconList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // SmallIconList
            // 
            this.SmallIconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.SmallIconList.ImageSize = new System.Drawing.Size(16, 16);
            this.SmallIconList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // DedicateInfoPanel
            // 
            this.DedicateInfoPanel.Controls.Add(this.DedicateLayoutPanel);
            this.DedicateInfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DedicateInfoPanel.Location = new System.Drawing.Point(0, 0);
            this.DedicateInfoPanel.MinimumSize = new System.Drawing.Size(240, 260);
            this.DedicateInfoPanel.Name = "DedicateInfoPanel";
            this.DedicateInfoPanel.Size = new System.Drawing.Size(240, 285);
            this.DedicateInfoPanel.TabIndex = 2;
            // 
            // DedicateLayoutPanel
            // 
            this.DedicateLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.DedicateLayoutPanel.ColumnCount = 2;
            this.DedicateLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DedicateLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.DedicateLayoutPanel.Controls.Add(this.LbAccessDate, 1, 6);
            this.DedicateLayoutPanel.Controls.Add(this.LbUpdateDate, 1, 5);
            this.DedicateLayoutPanel.Controls.Add(this.LbCreateDate, 1, 4);
            this.DedicateLayoutPanel.Controls.Add(this.LbKind, 1, 3);
            this.DedicateLayoutPanel.Controls.Add(this.LbAttribute, 1, 2);
            this.DedicateLayoutPanel.Controls.Add(this.label1, 0, 1);
            this.DedicateLayoutPanel.Controls.Add(this.label2, 0, 2);
            this.DedicateLayoutPanel.Controls.Add(this.label3, 0, 3);
            this.DedicateLayoutPanel.Controls.Add(this.label4, 0, 4);
            this.DedicateLayoutPanel.Controls.Add(this.label5, 0, 5);
            this.DedicateLayoutPanel.Controls.Add(this.label6, 0, 6);
            this.DedicateLayoutPanel.Controls.Add(this.PbFileImage, 0, 0);
            this.DedicateLayoutPanel.Controls.Add(this.LbFileName, 1, 1);
            this.DedicateLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DedicateLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.DedicateLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.DedicateLayoutPanel.Name = "DedicateLayoutPanel";
            this.DedicateLayoutPanel.RowCount = 7;
            this.DedicateLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.DedicateLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.DedicateLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.DedicateLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.DedicateLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.DedicateLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.DedicateLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.DedicateLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.DedicateLayoutPanel.Size = new System.Drawing.Size(240, 285);
            this.DedicateLayoutPanel.TabIndex = 0;
            // 
            // LbAccessDate
            // 
            this.LbAccessDate.AutoEllipsis = true;
            this.LbAccessDate.AutoSize = true;
            this.LbAccessDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LbAccessDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LbAccessDate.Location = new System.Drawing.Point(75, 264);
            this.LbAccessDate.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LbAccessDate.Name = "LbAccessDate";
            this.LbAccessDate.Size = new System.Drawing.Size(161, 20);
            this.LbAccessDate.TabIndex = 13;
            this.LbAccessDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LbUpdateDate
            // 
            this.LbUpdateDate.AutoEllipsis = true;
            this.LbUpdateDate.AutoSize = true;
            this.LbUpdateDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LbUpdateDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LbUpdateDate.Location = new System.Drawing.Point(75, 243);
            this.LbUpdateDate.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LbUpdateDate.Name = "LbUpdateDate";
            this.LbUpdateDate.Size = new System.Drawing.Size(161, 20);
            this.LbUpdateDate.TabIndex = 12;
            this.LbUpdateDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LbCreateDate
            // 
            this.LbCreateDate.AutoEllipsis = true;
            this.LbCreateDate.AutoSize = true;
            this.LbCreateDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LbCreateDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LbCreateDate.Location = new System.Drawing.Point(75, 222);
            this.LbCreateDate.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LbCreateDate.Name = "LbCreateDate";
            this.LbCreateDate.Size = new System.Drawing.Size(161, 20);
            this.LbCreateDate.TabIndex = 11;
            this.LbCreateDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LbKind
            // 
            this.LbKind.AutoEllipsis = true;
            this.LbKind.AutoSize = true;
            this.LbKind.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LbKind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LbKind.Location = new System.Drawing.Point(75, 201);
            this.LbKind.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LbKind.Name = "LbKind";
            this.LbKind.Size = new System.Drawing.Size(161, 20);
            this.LbKind.TabIndex = 10;
            this.LbKind.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LbAttribute
            // 
            this.LbAttribute.AutoEllipsis = true;
            this.LbAttribute.AutoSize = true;
            this.LbAttribute.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LbAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LbAttribute.Location = new System.Drawing.Point(75, 180);
            this.LbAttribute.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LbAttribute.Name = "LbAttribute";
            this.LbAttribute.Size = new System.Drawing.Size(161, 20);
            this.LbAttribute.TabIndex = 9;
            this.LbAttribute.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "ファイル名:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(4, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "属性:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(4, 201);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "種別:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(4, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "作成日時:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(4, 243);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "更新日時:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(4, 264);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "アクセス日時:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PbFileImage
            // 
            this.DedicateLayoutPanel.SetColumnSpan(this.PbFileImage, 2);
            this.PbFileImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PbFileImage.Location = new System.Drawing.Point(1, 1);
            this.PbFileImage.Margin = new System.Windows.Forms.Padding(0);
            this.PbFileImage.Name = "PbFileImage";
            this.PbFileImage.Size = new System.Drawing.Size(238, 157);
            this.PbFileImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PbFileImage.TabIndex = 6;
            this.PbFileImage.TabStop = false;
            // 
            // LbFileName
            // 
            this.LbFileName.AutoEllipsis = true;
            this.LbFileName.AutoSize = true;
            this.LbFileName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LbFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LbFileName.Location = new System.Drawing.Point(75, 159);
            this.LbFileName.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LbFileName.Name = "LbFileName";
            this.LbFileName.Size = new System.Drawing.Size(161, 20);
            this.LbFileName.TabIndex = 8;
            this.LbFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ExtraLargeIconList
            // 
            this.ExtraLargeIconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ExtraLargeIconList.ImageSize = new System.Drawing.Size(16, 16);
            this.ExtraLargeIconList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // JumboIconList
            // 
            this.JumboIconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.JumboIconList.ImageSize = new System.Drawing.Size(16, 16);
            this.JumboIconList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FileListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LayoutPanel);
            this.Name = "FileListView";
            this.Size = new System.Drawing.Size(625, 317);
            this.LayoutPanel.ResumeLayout(false);
            this.ControlPanel.ResumeLayout(false);
            this.SplitPanel.Panel1.ResumeLayout(false);
            this.SplitPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitPanel)).EndInit();
            this.SplitPanel.ResumeLayout(false);
            this.FileListViewMenuStrip.ResumeLayout(false);
            this.DedicateInfoPanel.ResumeLayout(false);
            this.DedicateLayoutPanel.ResumeLayout(false);
            this.DedicateLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbFileImage)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel LayoutPanel;
		private System.Windows.Forms.Panel ControlPanel;
		private System.Windows.Forms.RadioButton RbLarge;
		private System.Windows.Forms.ComboBox CbView;
		private System.Windows.Forms.RadioButton RbSmall;
		private System.Windows.Forms.RadioButton RbTile;
		private System.Windows.Forms.RadioButton RbList;
		private System.Windows.Forms.RadioButton RbDetail;
		private System.Windows.Forms.RadioButton RbJumbo;
		private System.Windows.Forms.RadioButton RbExtraLarge;
		private System.Windows.Forms.ListView ListViewFile;
		private System.Windows.Forms.Panel DedicateInfoPanel;
		private System.Windows.Forms.TableLayoutPanel DedicateLayoutPanel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.PictureBox PbFileImage;
		private System.Windows.Forms.Button BtShrink;
		private System.Windows.Forms.SplitContainer SplitPanel;
		private System.Windows.Forms.Label LbAccessDate;
		private System.Windows.Forms.Label LbUpdateDate;
		private System.Windows.Forms.Label LbCreateDate;
		private System.Windows.Forms.Label LbKind;
		private System.Windows.Forms.Label LbAttribute;
		private System.Windows.Forms.Label LbFileName;
		private System.Windows.Forms.ImageList SmallIconList;
		private System.Windows.Forms.ImageList LargeIconList;
		private System.Windows.Forms.ImageList ExtraLargeIconList;
		private System.Windows.Forms.ImageList JumboIconList;
		private System.Windows.Forms.ColumnHeader CHeaderFileName;
		private System.Windows.Forms.ColumnHeader CHeaderType;
		private System.Windows.Forms.ColumnHeader CHeaderSize;
		private System.Windows.Forms.ColumnHeader CHeaderCreateDate;
		private System.Windows.Forms.ColumnHeader CHeaderUpdateDate;
		private System.Windows.Forms.ColumnHeader CHeaderAccessDate;
		private System.Windows.Forms.ColumnHeader CHeaderAttr;
		private System.Windows.Forms.RadioButton RbLargeTile;
        private System.Windows.Forms.Button BtNewFolder;
        private System.Windows.Forms.ContextMenuStrip FileListViewMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemRename;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDelete;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemNewFile;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemNewFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}
