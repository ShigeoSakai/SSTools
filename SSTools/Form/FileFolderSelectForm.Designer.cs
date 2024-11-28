namespace SSTools
{
    partial class FileFolderSelectForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileFolderSelectForm));
            this.MainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.FolderTreeView = new System.Windows.Forms.TreeView();
            this.SmallImageList = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.FileListView = new System.Windows.Forms.ListView();
            this.FileLargeIconList = new System.Windows.Forms.ImageList(this.components);
            this.FileSmallIconList = new System.Windows.Forms.ImageList(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.CbView = new System.Windows.Forms.ComboBox();
            this.RbTile = new System.Windows.Forms.RadioButton();
            this.RbList = new System.Windows.Forms.RadioButton();
            this.RbLarge = new System.Windows.Forms.RadioButton();
            this.RbSmall = new System.Windows.Forms.RadioButton();
            this.RbDedicate = new System.Windows.Forms.RadioButton();
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCreateDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnUpdateDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnAccsessDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnKind = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MainLayout.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainLayout
            // 
            this.MainLayout.ColumnCount = 3;
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.MainLayout.Controls.Add(this.panel1, 0, 0);
            this.MainLayout.Controls.Add(this.FolderTreeView, 0, 1);
            this.MainLayout.Controls.Add(this.panel2, 1, 1);
            this.MainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLayout.Location = new System.Drawing.Point(0, 0);
            this.MainLayout.Name = "MainLayout";
            this.MainLayout.RowCount = 3;
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.MainLayout.Size = new System.Drawing.Size(793, 436);
            this.MainLayout.TabIndex = 0;
            // 
            // panel1
            // 
            this.MainLayout.SetColumnSpan(this.panel1, 3);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(787, 23);
            this.panel1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox1.Location = new System.Drawing.Point(26, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(712, 19);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Location = new System.Drawing.Point(738, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "更新";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "パス:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FolderTreeView
            // 
            this.FolderTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FolderTreeView.ImageIndex = 0;
            this.FolderTreeView.ImageList = this.SmallImageList;
            this.FolderTreeView.Location = new System.Drawing.Point(0, 29);
            this.FolderTreeView.Margin = new System.Windows.Forms.Padding(0);
            this.FolderTreeView.Name = "FolderTreeView";
            this.FolderTreeView.SelectedImageIndex = 0;
            this.FolderTreeView.Size = new System.Drawing.Size(253, 343);
            this.FolderTreeView.TabIndex = 1;
            this.FolderTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.FolderTreeView_BeforeExpand);
            this.FolderTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.FolderTreeView_AfterSelect);
            // 
            // SmallImageList
            // 
            this.SmallImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.SmallImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.SmallImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.FileListView);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(253, 29);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(379, 343);
            this.panel2.TabIndex = 3;
            // 
            // FileListView
            // 
            this.FileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnKind,
            this.columnSize,
            this.columnCreateDate,
            this.columnUpdateDate,
            this.columnAccsessDate});
            this.FileListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileListView.HideSelection = false;
            this.FileListView.LargeImageList = this.FileLargeIconList;
            this.FileListView.Location = new System.Drawing.Point(0, 24);
            this.FileListView.Margin = new System.Windows.Forms.Padding(0);
            this.FileListView.Name = "FileListView";
            this.FileListView.ShowItemToolTips = true;
            this.FileListView.Size = new System.Drawing.Size(379, 319);
            this.FileListView.SmallImageList = this.FileSmallIconList;
            this.FileListView.TabIndex = 2;
            this.FileListView.UseCompatibleStateImageBehavior = false;
            this.FileListView.SelectedIndexChanged += new System.EventHandler(this.FileListView_SelectedIndexChanged);
            this.FileListView.Click += new System.EventHandler(this.FileListView_Click);
            this.FileListView.DoubleClick += new System.EventHandler(this.FileListView_DoubleClick);
            // 
            // FileLargeIconList
            // 
            this.FileLargeIconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.FileLargeIconList.ImageSize = new System.Drawing.Size(32, 32);
            this.FileLargeIconList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FileSmallIconList
            // 
            this.FileSmallIconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.FileSmallIconList.ImageSize = new System.Drawing.Size(16, 16);
            this.FileSmallIconList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.CbView);
            this.panel3.Controls.Add(this.RbTile);
            this.panel3.Controls.Add(this.RbList);
            this.panel3.Controls.Add(this.RbLarge);
            this.panel3.Controls.Add(this.RbSmall);
            this.panel3.Controls.Add(this.RbDedicate);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(379, 24);
            this.panel3.TabIndex = 0;
            // 
            // CbView
            // 
            this.CbView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbView.FormattingEnabled = true;
            this.CbView.Location = new System.Drawing.Point(208, 2);
            this.CbView.Margin = new System.Windows.Forms.Padding(0);
            this.CbView.Name = "CbView";
            this.CbView.Size = new System.Drawing.Size(121, 20);
            this.CbView.TabIndex = 5;
            this.CbView.SelectionChangeCommitted += new System.EventHandler(this.CbView_SelectionChangeCommitted);
            // 
            // RbTile
            // 
            this.RbTile.Appearance = System.Windows.Forms.Appearance.Button;
            this.RbTile.Image = ((System.Drawing.Image)(resources.GetObject("RbTile.Image")));
            this.RbTile.Location = new System.Drawing.Point(140, 0);
            this.RbTile.Margin = new System.Windows.Forms.Padding(0);
            this.RbTile.Name = "RbTile";
            this.RbTile.Size = new System.Drawing.Size(35, 24);
            this.RbTile.TabIndex = 4;
            this.RbTile.TabStop = true;
            this.RbTile.UseVisualStyleBackColor = true;
            this.RbTile.CheckedChanged += new System.EventHandler(this.RbDedicate_CheckedChanged);
            // 
            // RbList
            // 
            this.RbList.Appearance = System.Windows.Forms.Appearance.Button;
            this.RbList.Image = ((System.Drawing.Image)(resources.GetObject("RbList.Image")));
            this.RbList.Location = new System.Drawing.Point(105, 0);
            this.RbList.Margin = new System.Windows.Forms.Padding(0);
            this.RbList.Name = "RbList";
            this.RbList.Size = new System.Drawing.Size(35, 24);
            this.RbList.TabIndex = 3;
            this.RbList.TabStop = true;
            this.RbList.UseVisualStyleBackColor = true;
            this.RbList.CheckedChanged += new System.EventHandler(this.RbDedicate_CheckedChanged);
            // 
            // RbLarge
            // 
            this.RbLarge.Appearance = System.Windows.Forms.Appearance.Button;
            this.RbLarge.Image = ((System.Drawing.Image)(resources.GetObject("RbLarge.Image")));
            this.RbLarge.Location = new System.Drawing.Point(70, 0);
            this.RbLarge.Margin = new System.Windows.Forms.Padding(0);
            this.RbLarge.Name = "RbLarge";
            this.RbLarge.Size = new System.Drawing.Size(35, 24);
            this.RbLarge.TabIndex = 2;
            this.RbLarge.TabStop = true;
            this.RbLarge.UseVisualStyleBackColor = true;
            this.RbLarge.CheckedChanged += new System.EventHandler(this.RbDedicate_CheckedChanged);
            // 
            // RbSmall
            // 
            this.RbSmall.Appearance = System.Windows.Forms.Appearance.Button;
            this.RbSmall.Image = ((System.Drawing.Image)(resources.GetObject("RbSmall.Image")));
            this.RbSmall.Location = new System.Drawing.Point(35, 0);
            this.RbSmall.Margin = new System.Windows.Forms.Padding(0);
            this.RbSmall.Name = "RbSmall";
            this.RbSmall.Size = new System.Drawing.Size(35, 24);
            this.RbSmall.TabIndex = 1;
            this.RbSmall.TabStop = true;
            this.RbSmall.UseVisualStyleBackColor = true;
            this.RbSmall.CheckedChanged += new System.EventHandler(this.RbDedicate_CheckedChanged);
            // 
            // RbDedicate
            // 
            this.RbDedicate.Appearance = System.Windows.Forms.Appearance.Button;
            this.RbDedicate.Image = ((System.Drawing.Image)(resources.GetObject("RbDedicate.Image")));
            this.RbDedicate.Location = new System.Drawing.Point(0, 0);
            this.RbDedicate.Margin = new System.Windows.Forms.Padding(0);
            this.RbDedicate.Name = "RbDedicate";
            this.RbDedicate.Size = new System.Drawing.Size(35, 24);
            this.RbDedicate.TabIndex = 0;
            this.RbDedicate.TabStop = true;
            this.RbDedicate.UseVisualStyleBackColor = true;
            this.RbDedicate.CheckedChanged += new System.EventHandler(this.RbDedicate_CheckedChanged);
            // 
            // columnName
            // 
            this.columnName.Text = "ファイル名";
            this.columnName.Width = 240;
            // 
            // columnSize
            // 
            this.columnSize.Text = "サイズ";
            this.columnSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnCreateDate
            // 
            this.columnCreateDate.Text = "作成日時";
            this.columnCreateDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnUpdateDate
            // 
            this.columnUpdateDate.Text = "更新日時";
            this.columnUpdateDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnAccsessDate
            // 
            this.columnAccsessDate.Text = "アクセス日時";
            this.columnAccsessDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnKind
            // 
            this.columnKind.Text = "種別";
            this.columnKind.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FileFolderSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 436);
            this.Controls.Add(this.MainLayout);
            this.Name = "FileFolderSelectForm";
            this.Text = "FileFolderSelectForm";
            this.Shown += new System.EventHandler(this.FileFolderSelectForm_Shown);
            this.MainLayout.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainLayout;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TreeView FolderTreeView;
        private System.Windows.Forms.ImageList SmallImageList;
        private System.Windows.Forms.ListView FileListView;
        private System.Windows.Forms.ImageList FileSmallIconList;
        private System.Windows.Forms.ImageList FileLargeIconList;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton RbDedicate;
        private System.Windows.Forms.ComboBox CbView;
        private System.Windows.Forms.RadioButton RbTile;
        private System.Windows.Forms.RadioButton RbList;
        private System.Windows.Forms.RadioButton RbLarge;
        private System.Windows.Forms.RadioButton RbSmall;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnSize;
        private System.Windows.Forms.ColumnHeader columnKind;
        private System.Windows.Forms.ColumnHeader columnCreateDate;
        private System.Windows.Forms.ColumnHeader columnUpdateDate;
        private System.Windows.Forms.ColumnHeader columnAccsessDate;
    }
}