namespace TestForm
{
	partial class Form1
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

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.LargeIconList = new System.Windows.Forms.ImageList(this.components);
			this.SmallIconList = new System.Windows.Forms.ImageList(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.fileListView1 = new SSTools.CustomizeControl.FileListView();
			this.folderTreeView1 = new SSTools.FolderTreeView();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// LargeIconList
			// 
			this.LargeIconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.LargeIconList.ImageSize = new System.Drawing.Size(16, 16);
			this.LargeIconList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// SmallIconList
			// 
			this.SmallIconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.SmallIconList.ImageSize = new System.Drawing.Size(16, 16);
			this.SmallIconList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// panel1
			// 
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(800, 30);
			this.panel1.TabIndex = 1;
			// 
			// fileListView1
			// 
			this.fileListView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fileListView1.Location = new System.Drawing.Point(0, 0);
			this.fileListView1.MultiSelect = true;
			this.fileListView1.Name = "fileListView1";
			this.fileListView1.Path = null;
			this.fileListView1.Size = new System.Drawing.Size(575, 420);
			this.fileListView1.TabIndex = 3;
			this.fileListView1.View = SSTools.CustomizeControl.FileListView.FILE_VIEW.LargeIcon;
			this.fileListView1.ChangeDirectoryEvent += new SSTools.CustomizeControl.FileListView.ChangeDirectoryEventHandler(this.fileListView1_ChangeDirectoryEvent);
			// 
			// folderTreeView1
			// 
			this.folderTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.folderTreeView1.ImageIndex = 0;
			this.folderTreeView1.Location = new System.Drawing.Point(0, 0);
			this.folderTreeView1.Name = "folderTreeView1";
			this.folderTreeView1.SelectedImageIndex = 0;
			this.folderTreeView1.ShowHiddenFolder = false;
			this.folderTreeView1.Size = new System.Drawing.Size(221, 420);
			this.folderTreeView1.TabIndex = 2;
			this.folderTreeView1.SelectNodeEvent += new SSTools.FolderTreeView.OnSelectNodeEventHandler(this.folderTreeView1_SelectNodeEvent);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 30);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.folderTreeView1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.fileListView1);
			this.splitContainer1.Size = new System.Drawing.Size(800, 420);
			this.splitContainer1.SplitterDistance = 221;
			this.splitContainer1.TabIndex = 4;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.panel1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ImageList SmallIconList;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ImageList LargeIconList;
		private SSTools.FolderTreeView folderTreeView1;
		private SSTools.CustomizeControl.FileListView fileListView1;
		private System.Windows.Forms.SplitContainer splitContainer1;
	}
}

