namespace SSTools
{
    partial class FileSelectDialog
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.TbPath = new System.Windows.Forms.TextBox();
            this.TbFolderSelect = new SSTools.FolderSelectButton();
            this.BtOpen = new System.Windows.Forms.Button();
            this.FolderTree = new SSTools.FolderTreeView();
            this.FileView = new SSTools.FileListView();
            this.label2 = new System.Windows.Forms.Label();
            this.CbFilter = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TbSelectFiles = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtCancel = new System.Windows.Forms.Button();
            this.BtOK = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 198F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.TbPath, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.TbFolderSelect, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.BtOpen, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.FolderTree, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.FileView, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.CbFilter, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.TbSelectFiles, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "パス:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TbPath
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.TbPath, 2);
            this.TbPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TbPath.Location = new System.Drawing.Point(51, 3);
            this.TbPath.Name = "TbPath";
            this.TbPath.Size = new System.Drawing.Size(666, 19);
            this.TbPath.TabIndex = 1;
            // 
            // TbFolderSelect
            // 
            this.TbFolderSelect.AutoSize = true;
            this.TbFolderSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TbFolderSelect.LinkControl = this.TbPath;
            this.TbFolderSelect.Location = new System.Drawing.Point(720, 0);
            this.TbFolderSelect.Margin = new System.Windows.Forms.Padding(0);
            this.TbFolderSelect.Name = "TbFolderSelect";
            this.TbFolderSelect.SendClickControl = this.BtOpen;
            this.TbFolderSelect.Size = new System.Drawing.Size(32, 24);
            this.TbFolderSelect.TabIndex = 2;
            this.TbFolderSelect.Text = "...";
            this.TbFolderSelect.UserCustomDialog = true;
            this.TbFolderSelect.UseVisualStyleBackColor = true;
            // 
            // BtOpen
            // 
            this.BtOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtOpen.Location = new System.Drawing.Point(752, 0);
            this.BtOpen.Margin = new System.Windows.Forms.Padding(0);
            this.BtOpen.Name = "BtOpen";
            this.BtOpen.Size = new System.Drawing.Size(48, 24);
            this.BtOpen.TabIndex = 3;
            this.BtOpen.Text = "開く";
            this.BtOpen.UseVisualStyleBackColor = true;
            this.BtOpen.Click += new System.EventHandler(this.BtOpen_Click);
            // 
            // FolderTree
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.FolderTree, 2);
            this.FolderTree.CurrentPath = "";
            this.FolderTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FolderTree.ImageIndex = 0;
            this.FolderTree.Location = new System.Drawing.Point(3, 27);
            this.FolderTree.Name = "FolderTree";
            this.FolderTree.SelectedImageIndex = 0;
            this.FolderTree.ShowHiddenFolder = true;
            this.FolderTree.Size = new System.Drawing.Size(240, 340);
            this.FolderTree.TabIndex = 4;
            this.FolderTree.SelectNodeEvent += new SSTools.FolderTreeView.OnSelectNodeEventHandler(this.FolderTree_SelectNodeEvent);
            // 
            // FileView
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.FileView, 3);
            this.FileView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileView.Location = new System.Drawing.Point(249, 27);
            this.FileView.MultiSelect = true;
            this.FileView.Name = "FileView";
            this.FileView.Path = null;
            this.FileView.ShowNewFolderButton = true;
            this.FileView.Size = new System.Drawing.Size(548, 340);
            this.FileView.TabIndex = 5;
            this.FileView.View = SSTools.FileListView.FILE_VIEW.LargeIcon;
            this.FileView.ChangeDirectoryEvent += new SSTools.FileListView.ChangeDirectoryEventHandler(this.FileView_ChangeDirectoryEvent);
            this.FileView.FileSelectedEvent += new SSTools.FileListView.FileSelectedEventHandler(this.FileView_SelectedEvent);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 370);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 24);
            this.label2.TabIndex = 6;
            this.label2.Text = "フィルタ:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CbFilter
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.CbFilter, 2);
            this.CbFilter.FormattingEnabled = true;
            this.CbFilter.Location = new System.Drawing.Point(51, 373);
            this.CbFilter.Name = "CbFilter";
            this.CbFilter.Size = new System.Drawing.Size(377, 20);
            this.CbFilter.TabIndex = 7;
            this.CbFilter.SelectionChangeCommitted += new System.EventHandler(this.CbFilter_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 394);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 24);
            this.label3.TabIndex = 8;
            this.label3.Text = "ファイル:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TbSelectFiles
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.TbSelectFiles, 3);
            this.TbSelectFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TbSelectFiles.Location = new System.Drawing.Point(51, 397);
            this.TbSelectFiles.Name = "TbSelectFiles";
            this.TbSelectFiles.Size = new System.Drawing.Size(698, 19);
            this.TbSelectFiles.TabIndex = 9;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 5);
            this.panel1.Controls.Add(this.BtCancel);
            this.panel1.Controls.Add(this.BtOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 418);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 32);
            this.panel1.TabIndex = 10;
            // 
            // BtCancel
            // 
            this.BtCancel.Location = new System.Drawing.Point(491, 6);
            this.BtCancel.Name = "BtCancel";
            this.BtCancel.Size = new System.Drawing.Size(75, 23);
            this.BtCancel.TabIndex = 1;
            this.BtCancel.Text = "Cancel";
            this.BtCancel.UseVisualStyleBackColor = true;
            this.BtCancel.Click += new System.EventHandler(this.BtCancel_Click);
            // 
            // BtOK
            // 
            this.BtOK.Location = new System.Drawing.Point(168, 6);
            this.BtOK.Name = "BtOK";
            this.BtOK.Size = new System.Drawing.Size(75, 23);
            this.BtOK.TabIndex = 0;
            this.BtOK.Text = "OK";
            this.BtOK.UseVisualStyleBackColor = true;
            this.BtOK.Click += new System.EventHandler(this.BtOK_Click);
            // 
            // FileSelectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FileSelectDialog";
            this.Text = "FileSelectDialog";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TbPath;
        private FolderSelectButton TbFolderSelect;
        private System.Windows.Forms.Button BtOpen;
        private FolderTreeView FolderTree;
        private FileListView FileView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CbFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TbSelectFiles;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtOK;
        private System.Windows.Forms.Button BtCancel;
    }
}