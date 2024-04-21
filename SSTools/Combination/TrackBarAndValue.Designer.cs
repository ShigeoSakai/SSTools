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
			this.TbValue = new System.Windows.Forms.NumericUpDown();
			this.TrackBarValue = new System.Windows.Forms.TrackBar();
			((System.ComponentModel.ISupportInitialize)(this.TbValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TrackBarValue)).BeginInit();
			this.SuspendLayout();
			// 
			// TbValue
			// 
			this.TbValue.Location = new System.Drawing.Point(3, 6);
			this.TbValue.Name = "TbValue";
			this.TbValue.Size = new System.Drawing.Size(56, 19);
			this.TbValue.TabIndex = 0;
			this.TbValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.TbValue.ValueChanged += new System.EventHandler(this.TbValue_ValueChanged);
			// 
			// TrackBarValue
			// 
			this.TrackBarValue.AutoSize = false;
			this.TrackBarValue.Location = new System.Drawing.Point(65, 3);
			this.TrackBarValue.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.TrackBarValue.Name = "TrackBarValue";
			this.TrackBarValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.TrackBarValue.Size = new System.Drawing.Size(112, 24);
			this.TrackBarValue.TabIndex = 1;
			this.TrackBarValue.TickFrequency = 10;
			this.TrackBarValue.Scroll += new System.EventHandler(this.TrackBarValue_Scroll);
			this.TrackBarValue.ValueChanged += new System.EventHandler(this.TrackBarValue_ValueChanged);
			// 
			// TrackBarAndValue
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.TrackBarValue);
			this.Controls.Add(this.TbValue);
			this.Name = "TrackBarAndValue";
			this.Size = new System.Drawing.Size(180, 28);
			((System.ComponentModel.ISupportInitialize)(this.TbValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TrackBarValue)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NumericUpDown TbValue;
		private System.Windows.Forms.TrackBar TrackBarValue;
	}
}
