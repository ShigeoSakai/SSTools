using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SSTools
{
    public partial class FileFolderSelectForm : Form
    {
        public FileFolderSelectForm()
        {
            InitializeComponent();

            // アイコンのリストを取得
            Win32API.Shell32.GetIconList(ref SmallIconList, ref LargeIconList, ref ExtraLargeIconList, ref JumboIconList);

            MakeTreeView(Environment.SpecialFolder.Favorites);

            ComboItem<View>.MakeCombo(CbView,Enum.GetValues(typeof(View)));
            ComboItem<View>.Set(CbView, FileListView.View);
            SetViewRadioButton(FileListView.View);

            //MakeFileView();
        }
        /// <summary>
        /// 小アイコンリスト
        /// </summary>
        private IntPtr SmallIconList;
        /// <summary>
        /// 大アイコンリスト
        /// </summary>
        private IntPtr LargeIconList;
        /// <summary>
        /// 特大アイコンリスト
        /// </summary>
        private IntPtr ExtraLargeIconList;
        /// <summary>
        /// ジャンボアイコンリスト
        /// </summary>
        private IntPtr JumboIconList;


        private bool init_proc = false;

        private class TreeNodeExt : TreeNode
        {
            public string Path { get; private set; }
            public string DispName { get; private set; }

            public TreeNodeExt(string path,string dispName) :base()
            {
                Path = path;             
                Text = dispName ?? System.IO.Path.GetFileName(path);
            }
            public TreeNodeExt(string path, string dispName,int imageIndex,int selectImageIndex) : this(path,dispName)
            {
                ImageIndex = imageIndex;
                SelectedImageIndex = selectImageIndex;
            }

        }

        private int GetIndexFromKey(ref ImageList imgList,int key,bool small = true)
        {
            for (int i = 0; i < imgList.Images.Count; i++)
            {
                if (imgList.Images.Keys[i] == key.ToString())
                    return i;
            }
            imgList.Images.Add(key.ToString(), Win32API.Shell32.GetIcon(key,(small) ? SmallIconList : LargeIconList));
            return imgList.Images.Count - 1;
        }


        private void MakeTreeView(Environment.SpecialFolder specialFolder)
        {
            FolderTreeView.Nodes.Clear();

            SmallImageList.Images.Clear();
            SmallImageList.ImageSize = new Size(16, 16);
            SmallImageList.ColorDepth = ColorDepth.Depth32Bit;

            // お気に入りの取得
            List<TreeNode> nodes = new List<TreeNode>();
            Shell32.Shell shell = new Shell32.Shell();
            Shell32.Folder folder = shell.NameSpace("shell:::{679f85cb-0220-4080-b29b-5540cc05aab6}");
            if (folder != null)
            {
                foreach (Shell32.FolderItem fi in folder.Items())
                {
                    // IconのIndex取得
                    int iconIdx = Win32API.Shell32.GetIconIndex(fi.Path);
                    // ImageListのチェック
                    int imgIndex = GetIndexFromKey(ref SmallImageList,iconIdx);

                    TreeNodeExt node = new TreeNodeExt(fi.Path,fi.Name,imgIndex,imgIndex);


                    nodes.Add(node);
                }
                // ImageListのチェック
                int imgIndex_home = GetIndexFromKey(ref SmallImageList, 67);
                FolderTreeView.Nodes.Add(new TreeNode(folder.Title, imgIndex_home, imgIndex_home, nodes.ToArray()) { Name = folder.Title });
                FolderTreeView.Nodes[folder.Title].Expand();

            }
            // ドライブの検索
            DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                List<TreeNode> subNodes = new List<TreeNode>();
                foreach(string subDir in Directory.EnumerateDirectories(drive.Name))
                {
                    try
                    {
                        DirectoryInfo dinfo = new DirectoryInfo(subDir);

                        int iconIdx = Win32API.Shell32.GetIconIndex(subDir);
                        int imgIndex = GetIndexFromKey(ref SmallImageList, iconIdx);
                        subNodes.Add(new TreeNode(Path.GetFileName(subDir), imgIndex, imgIndex));
                    }
                    catch (UnauthorizedAccessException) { }
                }


                string drv_name = drive.Name;
                if (drv_name.EndsWith("\\"))
                    drv_name = drv_name.Substring(0,drv_name.Length-1);
                int iconIdx_drv = Win32API.Shell32.GetIconIndex(drv_name);
                int imgIndex_drv = GetIndexFromKey(ref SmallImageList, iconIdx_drv);
                FolderTreeView.Nodes.Add(new TreeNode(drv_name, imgIndex_drv, imgIndex_drv,subNodes.ToArray()));
            }
        }


        private void MakeFileView()
        {
            for (int index = 0; index < Win32API.Shell32.GetIconCount(SmallIconList); index++)
            {
                Icon small_icon = Win32API.Shell32.GetIcon(index, SmallIconList);
                FileSmallIconList.Images.Add(index.ToString(), small_icon);

                Icon large_icon = Win32API.Shell32.GetIcon(index, LargeIconList);
                FileLargeIconList.Images.Add(index.ToString(), large_icon);


                FileListView.Items.Add(new ListViewItem(index.ToString(), index.ToString()));
            }
        }

        private void FolderTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (formShown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    foreach (TreeNode node in e.Node.Nodes)
                    {
                        if (node.Nodes.Count == 0)
                        {   // 下があるかどうか
                            string path = node.FullPath;
                            List<TreeNode> subNodes = new List<TreeNode>();

                            try
                            {
                                foreach (string subDir in Directory.EnumerateDirectories(path))
                                {
                                    int iconIdx = Win32API.Shell32.GetIconIndex(subDir);
                                    int imgIndex = GetIndexFromKey(ref SmallImageList, iconIdx);
                                    subNodes.Add(new TreeNode(Path.GetFileName(subDir), imgIndex, imgIndex));
                                }
                                if (subNodes.Count > 0)
                                    node.Nodes.AddRange(subNodes.ToArray());
                            }
                            catch (UnauthorizedAccessException) { }
                        }
                    }
                }
            }
        }

        private bool formShown = false;
        private void FileFolderSelectForm_Shown(object sender, EventArgs e)
        {
            formShown = true;
        }

        private void CbView_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FileListView.View = ComboItem<View>.Get(CbView, View.SmallIcon);
            SetViewRadioButton(FileListView.View);
        }

        private bool CheckedChangeProgram = false;
        private void SetViewRadioButton(View view)
        {
            CheckedChangeProgram = true;
            switch(view)
            {
                case View.SmallIcon:
                    RbSmall.Checked = true; 
                    break;
                case View.LargeIcon:
                    RbLarge.Checked = true;
                    break;
                case View.Details:
                    RbDedicate.Checked = true;
                    break;
                case View.List:
                    RbList.Checked = true;
                    break;
                case View.Tile:
                    RbTile.Checked = true;
                    break;
            }
            CheckedChangeProgram = false;
        }

        private void RbDedicate_CheckedChanged(object sender, EventArgs e)
        {
            if ((CheckedChangeProgram == false) && (sender is RadioButton rb) && (rb.Checked))
            {
                View view = View.LargeIcon;
                if (rb.Equals(RbSmall))
                    view = View.SmallIcon;
                else if (rb.Equals(RbTile))
                    view = View.Tile;
                else if (rb.Equals(RbList))
                    view = View.List;
                else if (rb.Equals(RbDedicate))
                    view = View.Details;
                FileListView.View = view;
                ComboItem<View>.Set(CbView, view);
            }
        }

        private void FolderTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            if (node != null)
            {
                string path = node.FullPath;
                if (Directory.Exists(path))
                {
                    FileListView.Items.Clear();
                    foreach(string file_name in Directory.EnumerateFileSystemEntries(path)) 
                    {
                        FileSystemInfo f_info = null; ;
                        FileAttributes attr =  File.GetAttributes(file_name);

                        string fileSize = "";
                        string ext = "";
                        if (attr.HasFlag(FileAttributes.Directory))
                        {
                            f_info = new DirectoryInfo(file_name);
                        }
                        else if ((attr.HasFlag(FileAttributes.Normal) || (attr.HasFlag(FileAttributes.Archive))))
                        {
                            FileInfo f_info_f = new FileInfo(file_name);
                            fileSize = f_info_f.Length.ToString("#,0");
                            ext = f_info_f.Extension;
                            f_info = f_info_f;
                        }
                        if (f_info != null)
                        {
                            int iconIdx = Win32API.Shell32.GetIconIndex(file_name);
                            int imgSmallIndex = GetIndexFromKey(ref FileSmallIconList, iconIdx);
                            int imgLargeIndex = GetIndexFromKey(ref FileLargeIconList, iconIdx, false);

                            ListViewItem item = new ListViewItem(new string[]
                            {
                                f_info.Name,
                                ext,
                                fileSize,
                                f_info.CreationTime.ToString("yy/MM/dd HH:mm:ss"),
                                f_info.LastWriteTime.ToString("yy/MM/dd HH:mm:ss"),
                                f_info.LastAccessTime.ToString("yy/MM/dd HH:mm:ss"),

                            }, imgSmallIndex)
                            {
                                Tag = attr,
                            };
                            FileListView.Items.Add(item);
                        }

                    }
                }
            }
        }

        private void FileListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FileListView_Click(object sender, EventArgs e)
        {
            if (FileListView.SelectedItems.Count > 0)
            {

            }
        }

        private void FileListView_DoubleClick(object sender, EventArgs e)
        {
            if (FileListView.SelectedItems.Count == 1)
            {
                ListViewItem item = FileListView.SelectedItems[0];
                if ((item != null) && (item.Tag is FileAttributes attr) && (attr.HasFlag(FileAttributes.Directory)))
                {   // ディレクトリを移動
                    TreeNode node = FolderTreeView.SelectedNode;
                    if (node != null)
                    {
                        string path = Path.Combine(node.FullPath, item.Text);
                        ChangeDirectory(path);
                    }
                }
            }
        }
        private void ChangeDirectory(string path)
        {
            Console.WriteLine("ChDir:{0}", path);

            TreeNode node = Find(FolderTreeView.Nodes, path + FolderTreeView.PathSeparator, out string remain);

            if (remain.Length == 0)
                FolderTreeView.SelectedNode = node;
            
        }

        private TreeNode Find(TreeNode node, string path,out string remain)
        {
            if (path.StartsWith(node.FullPath + FolderTreeView.PathSeparator))
            {
                remain = path.Substring(node.FullPath.Length + FolderTreeView.PathSeparator.Length);
                if (remain.Length == 0)
                    return node;

                if (node.Nodes.Count > 0)
                {
                    foreach(TreeNode subNode in node.Nodes)
                    {
                        TreeNode result = Find(subNode, path, out remain);
                        if (result != null)
                            return result;
                    }
                }
                return node;
            }
            remain = path;
            return null;
        }

        private TreeNode Find(TreeNodeCollection collection, string path, out string remain)
        {
            if (collection.Count > 0)
            {
                foreach (TreeNode node in collection)
                {
                    if (path.StartsWith(node.FullPath + FolderTreeView.PathSeparator))
                    {
                        remain = path.Substring(node.FullPath.Length + FolderTreeView.PathSeparator.Length);
                        if (remain.Length == 0)
                            return node;
                        if (node.Nodes.Count > 0)
                            return Find(node.Nodes, path, out remain);
                        return node;
                    }
                }
            }
            remain = path;
            return null;
        }
    }
}
