namespace SSTools
{
	partial class TrackBarAndValue
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
			this.UpDownValue = new SSTools.AutoSizeNumericUpDown();
			this.TrackBarValue = new System.Windows.Forms.TrackBar();
			((System.ComponentModel.ISupportInitialize)(this.UpDownValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TrackBarValue)).BeginInit();
			this.SuspendLayout();
			// 
			// UpDownValue
			// 
			this.UpDownValue.AutoSize = true;
			this.UpDownValue.Location = new System.Drawing.Point(3, 3);
			this.UpDownValue.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.UpDownValue.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.UpDownValue.Name = "UpDownValue";
			this.UpDownValue.Size = new System.Drawing.Size(42, 19);
			this.UpDownValue.TabIndex = 0;
			this.UpDownValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.UpDownValue.ValueChanged += new System.EventHandler(this.UpDownValue_ValueChanged);
			this.UpDownValue.SizeChanged += new System.EventHandler(this.UpDownValue_SizeChanged);
			// 
			// TrackBarValue
			// 
			this.TrackBarValue.AutoSize = false;
			this.TrackBarValue.Location = new System.Drawing.Point(51, 3);
			this.TrackBarValue.Name = "TrackBarValue";
			this.TrackBarValue.Size = new System.Drawing.Size(104, 19);
			this.TrackBarValue.TabIndex = 1;
			this.TrackBarValue.Scroll += new System.EventHandler(this.TrackBarValue_Scroll);
			this.TrackBarValue.ValueChanged += new System.EventHandler(this.TrackBarValue_ValueChanged);
			// 
			// TrackBarAndValue2
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoSize = true;
			this.Controls.Add(this.TrackBarValue);
			this.Controls.Add(this.UpDownValue);
			this.Name = "TrackBarAndValue2";
			this.Size = new System.Drawing.Size(158, 25);
			((System.ComponentModel.ISupportInitialize)(this.UpDownValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TrackBarValue)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private AutoSizeNumericUpDown UpDownValue;
		private System.Windows.Forms.TrackBar TrackBarValue;
	}
}
