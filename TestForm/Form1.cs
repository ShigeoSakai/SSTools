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

namespace TestForm
{
	public partial class Form1 : Form
	{

		SystemIconManager iconManager = new SystemIconManager();
		public Form1()
		{
			InitializeComponent();

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
