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
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            this.trackBarAndValue21 = new SSTools.TrackBarAndValue();
            this.colorSelectionButton1 = new SSTools.ColorSelectionButton();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(221, 123);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBar1.RightToLeftLayout = true;
            this.trackBar1.Size = new System.Drawing.Size(45, 104);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.TickFrequency = 10;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar1.Value = 10;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(713, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 426);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // trackBarAndValue21
            // 
            this.trackBarAndValue21.AutoSize = true;
            this.trackBarAndValue21.DecimalPlaces = 2;
            this.trackBarAndValue21.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.trackBarAndValue21.LargeChange = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.trackBarAndValue21.Location = new System.Drawing.Point(375, 193);
            this.trackBarAndValue21.Name = "trackBarAndValue21";
            this.trackBarAndValue21.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarAndValue21.Size = new System.Drawing.Size(66, 224);
            this.trackBarAndValue21.SmallChange = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.trackBarAndValue21.TabIndex = 4;
            this.trackBarAndValue21.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.trackBarAndValue21.TickFrequency = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.trackBarAndValue21.UpDownControlWidth = 60;
            this.trackBarAndValue21.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // colorSelectionButton1
            // 
            this.colorSelectionButton1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.colorSelectionButton1.Color = System.Drawing.SystemColors.ButtonFace;
            this.colorSelectionButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorSelectionButton1.ForeColor = System.Drawing.Color.Black;
            this.colorSelectionButton1.Location = new System.Drawing.Point(345, 102);
            this.colorSelectionButton1.Name = "colorSelectionButton1";
            this.colorSelectionButton1.ShowColorName = SSTools.ColorSelectionButton.SHOW_COLOR_NAME.SYSTEM;
            this.colorSelectionButton1.Size = new System.Drawing.Size(75, 23);
            this.colorSelectionButton1.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.colorSelectionButton1.TabIndex = 3;
            this.colorSelectionButton1.Text = "Control";
            this.colorSelectionButton1.UseVisualStyleBackColor = false;
            this.colorSelectionButton1.WindowLocation = new System.Drawing.Point(0, 0);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.trackBarAndValue21);
            this.Controls.Add(this.colorSelectionButton1);
            this.Controls.Add(this.trackBar1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TrackBar trackBar1;
		private SSTools.ColorSelectionButton colorSelectionButton1;
		private SSTools.TrackBarAndValue trackBarAndValue21;
        private System.Windows.Forms.Button button1;
    }
}

