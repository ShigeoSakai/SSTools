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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtOpen = new System.Windows.Forms.Button();
            this.BtImageFolderSelect = new SSTools.FolderSelectButton();
            this.TbImageFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LbImages = new System.Windows.Forms.ListBox();
            this.ZPbImage = new SSTools.ZoomPictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZPbImage)).BeginInit();
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 256F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.LbImages, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ZPbImage, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.BtOpen);
            this.panel1.Controls.Add(this.BtImageFolderSelect);
            this.panel1.Controls.Add(this.TbImageFolder);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 64);
            this.panel1.TabIndex = 0;
            // 
            // BtOpen
            // 
            this.BtOpen.Location = new System.Drawing.Point(714, 9);
            this.BtOpen.Name = "BtOpen";
            this.BtOpen.Size = new System.Drawing.Size(75, 23);
            this.BtOpen.TabIndex = 3;
            this.BtOpen.Text = "開く";
            this.BtOpen.UseVisualStyleBackColor = true;
            this.BtOpen.Click += new System.EventHandler(this.BtOpen_Click);
            // 
            // BtImageFolderSelect
            // 
            this.BtImageFolderSelect.AutoSize = true;
            this.BtImageFolderSelect.LinkControl = this.TbImageFolder;
            this.BtImageFolderSelect.Location = new System.Drawing.Point(681, 9);
            this.BtImageFolderSelect.Name = "BtImageFolderSelect";
            this.BtImageFolderSelect.SendClickControl = this.BtOpen;
            this.BtImageFolderSelect.Size = new System.Drawing.Size(27, 23);
            this.BtImageFolderSelect.TabIndex = 2;
            this.BtImageFolderSelect.Text = "...";
            this.BtImageFolderSelect.UserCustomDialog = true;
            this.BtImageFolderSelect.UseVisualStyleBackColor = true;
            // 
            // TbImageFolder
            // 
            this.TbImageFolder.Location = new System.Drawing.Point(101, 11);
            this.TbImageFolder.Name = "TbImageFolder";
            this.TbImageFolder.Size = new System.Drawing.Size(574, 19);
            this.TbImageFolder.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "画像フォルダ:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LbImages
            // 
            this.LbImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LbImages.FormattingEnabled = true;
            this.LbImages.ItemHeight = 12;
            this.LbImages.Location = new System.Drawing.Point(3, 67);
            this.LbImages.Name = "LbImages";
            this.LbImages.Size = new System.Drawing.Size(250, 380);
            this.LbImages.TabIndex = 1;
            this.LbImages.SelectedIndexChanged += new System.EventHandler(this.LbImages_SelectedIndexChanged);
            // 
            // ZPbImage
            // 
            this.ZPbImage.AreaSelectColor = System.Drawing.Color.YellowGreen;
            this.ZPbImage.AutoImageDispose = true;
            this.ZPbImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ZPbImage.Image = null;
            this.ZPbImage.Location = new System.Drawing.Point(259, 67);
            this.ZPbImage.MaskImage = null;
            this.ZPbImage.MaskShow = true;
            this.ZPbImage.Name = "ZPbImage";
            this.ZPbImage.PictureBoxMode = SSTools.ZoomPictureBox.PictureBoxDrawMode.NORMAL;
            this.ZPbImage.ShapeShow = true;
            this.ZPbImage.ShowImage = true;
            this.ZPbImage.Size = new System.Drawing.Size(538, 380);
            this.ZPbImage.TabIndex = 2;
            this.ZPbImage.TabStop = false;
            this.ZPbImage.XRatio = 1F;
            this.ZPbImage.YRatio = 1F;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZPbImage)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ImageList SmallIconList;
		private System.Windows.Forms.ImageList LargeIconList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtOpen;
        private SSTools.FolderSelectButton BtImageFolderSelect;
        private System.Windows.Forms.TextBox TbImageFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox LbImages;
        private SSTools.ZoomPictureBox ZPbImage;
    }
}

