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
	public partial class Form1 : SSTools.FullscreenForm
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Form1() :base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 画像ファイル拡張子
        /// </summary>
        private string[] ImageExtensions = new string[]
        {
            ".jpg",".jpeg",".bmp",".png",".tif",".tiff"
        };

        /// <summary>
        /// 画像フォルダを開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtOpen_Click(object sender, EventArgs e)
        {

            if ((TbImageFolder.Text.Length > 0) && (Directory.Exists(TbImageFolder.Text)))
            {
                LbImages.Items.Clear();
                foreach (string fname in Directory.EnumerateFiles(TbImageFolder.Text))
                {
                    string ext = Path.GetExtension(fname);
                    if (ImageExtensions.Contains(ext))
                    {
                        LbImages.Items.Add(Path.GetFileName(fname));
                    }
                }
                if (LbImages.Items.Count > 0) LbImages.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 画像ファイルが選択された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LbImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LbImages.SelectedItems.Count > 0)
            {
                string image_file = Path.Combine(TbImageFolder.Text,
                    LbImages.SelectedItems[0].ToString());
                if (File.Exists(image_file))
                {
                    Bitmap bmp = null;
                    using(FileStream fs = new FileStream(image_file, FileMode.Open, FileAccess.Read))
                        bmp = new Bitmap(fs);
                    if (bmp != null) 
                        ZPbImage.Image = bmp;
                }
            }
        }
    }
}
