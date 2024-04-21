using System.Windows.Forms;

namespace SSTools
{
    public partial class FullscreenForm : Form 
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
            this.DisplayMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemNormal = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemMinimized = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemMaximized = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemFullScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // DisplayMenuStrip
            // 
            this.DisplayMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemNormal,
            this.ToolStripMenuItemMinimized,
            this.ToolStripMenuItemMaximized,
            this.ToolStripMenuItemFullScreen});
            this.DisplayMenuStrip.Name = "DisplayMenuStrip";
            this.DisplayMenuStrip.Size = new System.Drawing.Size(181, 114);
            this.DisplayMenuStrip.Text = "表示";
            // 
            // ToolStripMenuItemNormal
            // 
            this.ToolStripMenuItemNormal.Name = "ToolStripMenuItemNormal";
            this.ToolStripMenuItemNormal.Size = new System.Drawing.Size(180, 22);
            this.ToolStripMenuItemNormal.Text = "元に戻す";
            this.ToolStripMenuItemNormal.Click += new System.EventHandler(this.OnToolStripMenuItemNormal);
            // 
            // ToolStripMenuItemMinimized
            // 
            this.ToolStripMenuItemMinimized.Name = "ToolStripMenuItemMinimized";
            this.ToolStripMenuItemMinimized.Size = new System.Drawing.Size(180, 22);
            this.ToolStripMenuItemMinimized.Text = "最小化";
            this.ToolStripMenuItemMinimized.Click += new System.EventHandler(this.OnToolStripMenuItemMinimized);
            // 
            // ToolStripMenuItemMaximized
            // 
            this.ToolStripMenuItemMaximized.Name = "ToolStripMenuItemMaximized";
            this.ToolStripMenuItemMaximized.Size = new System.Drawing.Size(180, 22);
            this.ToolStripMenuItemMaximized.Text = "最大化";
            this.ToolStripMenuItemMaximized.Click += new System.EventHandler(this.OnToolStripMenuItemMaximized);
            // 
            // ToolStripMenuItemFullScreen
            // 
            this.ToolStripMenuItemFullScreen.Name = "ToolStripMenuItemFullScreen";
            this.ToolStripMenuItemFullScreen.Size = new System.Drawing.Size(180, 22);
            this.ToolStripMenuItemFullScreen.Text = "全画面";
            this.ToolStripMenuItemFullScreen.Click += new System.EventHandler(this.OnToolStripMenuItemFullScreen);
            // 
            // FForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ContextMenuStrip = this.DisplayMenuStrip;
            this.Name = "FForm";
            this.DisplayMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ToolStripMenuItem ToolStripMenuItemNormal;
        private ToolStripMenuItem ToolStripMenuItemMinimized;
        private ToolStripMenuItem ToolStripMenuItemMaximized;
        private ToolStripMenuItem ToolStripMenuItemFullScreen;
        protected ContextMenuStrip DisplayMenuStrip;
    }
}
