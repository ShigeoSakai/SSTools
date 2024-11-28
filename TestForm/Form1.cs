using SSTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Text.RegularExpressions;
using System.IO;

namespace TestForm
{
	public partial class Form1 : Form
	{


		private int GetNum(string text)
		{
			Match match = Regex.Match(text, @".+_(\((\d+)\)|(\d+))$");
			if ((match != null) && (match.Success) && (match.Groups.Count >= 4))
			{
				string num_text = (match.Groups[2].Success) ? match.Groups[2].Value :
					(match.Groups[3].Success) ? match.Groups[3].Value : null;
				if ((string.IsNullOrEmpty(num_text) == false) &&
					(int.TryParse(num_text, out int value)))
					return value;
			}
			return -1;
        }

		SystemIconManager iconManager = new SystemIconManager();
		public Form1()
		{
			InitializeComponent();

			int val1  = GetNum("AAA_(1)_456");
            int val2 = GetNum("AAA_123");



            string text = "画像ファイル|*.bmp;*.png;*.jpg;*.jpeg;*.tif;*.tiff;*.gif;*.svg;*.webp|" +
					"ビットマップファイル|*.bmp|" +
					"PNGファイル|*.png|" +
					"JPEGファイル|*.jpg;*.jpeg|" +
					"TIFFファイル|*.tif;*.tiff|" +
					"GIFファイル|*.gif|" +
					"SVGファイル|*.svg|" +
					"WebPファイル|*.webp|" +
					"全てのファイル|*.*";


			SSTools.Form.FileSelectDialog dialog = new SSTools.Form.FileSelectDialog()
			{
				//Filter = text,
				InitialDirectory = Directory.GetCurrentDirectory(),
				FilterIndex = 1,
				FileName = "TestForm.pdb"
            };
			dialog.ShowDialog();
        }

		private void folderTreeView1_SelectNodeEvent(object sender, string path, string fullpath, SSTools.CustomizeControl.FolderTreeNode topNode)
		{
			fileListView1.SetPath(fullpath,topNode);
		}

		private void fileListView1_ChangeDirectoryEvent(object sender, string path, SSTools.CustomizeControl.FolderTreeNode topNode)
		{
			folderTreeView1.SelectFolder(path,topNode);
		}
	}
}
