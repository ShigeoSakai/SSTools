namespace SSTools
{
	partial class WaitingForm
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.BtCancel = new System.Windows.Forms.Button();
			this.PBarExec = new System.Windows.Forms.ProgressBar();
			this.LbProgress = new System.Windows.Forms.Label();
			this.LbCaption = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.BtCancel, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.PBarExec, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.LbProgress, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.LbCaption, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(335, 95);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// BtCancel
			// 
			this.BtCancel.Location = new System.Drawing.Point(130, 66);
			this.BtCancel.Name = "BtCancel";
			this.BtCancel.Size = new System.Drawing.Size(75, 23);
			this.BtCancel.TabIndex = 0;
			this.BtCancel.Text = "中止";
			this.BtCancel.UseVisualStyleBackColor = true;
			this.BtCancel.Click += new System.EventHandler(this.BtCancel_Click);
			// 
			// PBarExec
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.PBarExec, 3);
			this.PBarExec.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PBarExec.Location = new System.Drawing.Point(16, 44);
			this.PBarExec.Margin = new System.Windows.Forms.Padding(16, 3, 16, 3);
			this.PBarExec.Name = "PBarExec";
			this.PBarExec.Size = new System.Drawing.Size(303, 16);
			this.PBarExec.TabIndex = 1;
			this.PBarExec.Value = 50;
			// 
			// LbProgress
			// 
			this.LbProgress.AutoSize = true;
			this.LbProgress.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LbProgress.Location = new System.Drawing.Point(130, 26);
			this.LbProgress.Margin = new System.Windows.Forms.Padding(3);
			this.LbProgress.Name = "LbProgress";
			this.LbProgress.Size = new System.Drawing.Size(75, 12);
			this.LbProgress.TabIndex = 2;
			this.LbProgress.Text = "0/0";
			this.LbProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LbCaption
			// 
			this.LbCaption.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.LbCaption, 3);
			this.LbCaption.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LbCaption.Location = new System.Drawing.Point(16, 8);
			this.LbCaption.Margin = new System.Windows.Forms.Padding(16, 8, 16, 3);
			this.LbCaption.Name = "LbCaption";
			this.LbCaption.Size = new System.Drawing.Size(303, 12);
			this.LbCaption.TabIndex = 3;
			this.LbCaption.Text = "aaa";
			this.LbCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// WaitingForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(335, 95);
			this.ControlBox = false;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "WaitingForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "処理中です...";
			this.TopMost = true;
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button BtCancel;
		private System.Windows.Forms.ProgressBar PBarExec;
		private System.Windows.Forms.Label LbProgress;
		private System.Windows.Forms.Label LbCaption;
	}
}