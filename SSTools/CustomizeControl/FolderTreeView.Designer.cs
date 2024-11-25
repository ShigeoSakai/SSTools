namespace SSTools
{
	partial class FolderTreeView
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
			this.IconImageList = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// IconImageList
			// 
			this.IconImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.IconImageList.ImageSize = new System.Drawing.Size(16, 16);
			this.IconImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// FolderTreeView
			// 
			this.ImageIndex = 0;
			this.ImageList = this.IconImageList;
			this.LineColor = System.Drawing.Color.Black;
			this.SelectedImageIndex = 0;
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ImageList IconImageList;
	}
}
