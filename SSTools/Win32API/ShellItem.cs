using System;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using static SSTools.Win32FileInfo;
using System.IO;

namespace SSTools
{
    public class ShellItem : IDisposable
    {
        // Sets a flag specifying whether or not we've got the IShellFolder interface for the Desktop.
        private static Boolean m_bHaveRootShell = false;

        private bool disposed = false;

        private string[] CompressdFileTypes = new string[]
        {
            "圧縮","アーカイブ","Compressed","Zip"
        };

        private bool IsCompressdFileType(string typeString)
        {
            foreach (string type in CompressdFileTypes)
                if (typeString.ToLower().Contains(type.ToLower()))
                    return true;
            return false;
        }

		/// <summary>
		/// Constructor. Creates the ShellItem object for the Desktop.
		/// </summary>
		public ShellItem(Environment.SpecialFolder folder = Environment.SpecialFolder.Desktop, bool include_hidden = false)
        {
            // new ShellItem() can only be called once.
            // if (m_bHaveRootShell)
            //     throw new Exception("The Desktop shell item already exists so cannot be created again.");
            {
                // Obtain the root IShellFolder interface.
                int hRes = SHGetDesktopFolder(ref m_shRootShell);
                if (hRes != 0)
                    Marshal.ThrowExceptionForHR(hRes);

                // Now get the PIDL for the Desktop shell item.
                hRes = SHGetSpecialFolderLocation(IntPtr.Zero, (uint)folder, ref m_pIDL);
                if (hRes != 0)
                    Marshal.ThrowExceptionForHR(hRes);

                // Now retrieve some attributes for the root shell item.
                SHFILEINFO shInfo = new SHFILEINFO();
                SHGetFileInfo(m_pIDL, 0, out shInfo, (uint)Marshal.SizeOf(shInfo),
                    SHGFI.SHGFI_DISPLAYNAME |
                    SHGFI.SHGFI_TYPENAME |
                    SHGFI.SHGFI_PIDL |
                    SHGFI.SHGFI_SMALLICON |
                    SHGFI.SHGFI_SYSICONINDEX
                );

                // Set the arributes to object properties.
                DisplayName = shInfo.szDisplayName;
                FileType = shInfo.szTypeName;
                IconIndex = shInfo.iIcon;
                IsFolder = true;
                HasSubFolder = true;
                Path = GetPath();

                // 選択アイコン取得
                SHFILEINFO shInfo_openicon = new SHFILEINFO();
                SHGetFileInfo(m_pIDL, 0, out shInfo_openicon, (uint)Marshal.SizeOf(shInfo_openicon),
                    SHGFI.SHGFI_PIDL |
                    SHGFI.SHGFI_SMALLICON |
                    SHGFI.SHGFI_SELECTED |
                    SHGFI.SHGFI_ADDOVERLAYS |
                    SHGFI.SHGFI_ICON
                );
                OpenIconIndex = shInfo_openicon.iIcon;
                OpenIcon = shInfo_openicon.hIcon;

                // 本当にサブフォルダがあるか確認
                if ((IsFolder) && (folder != Environment.SpecialFolder.Desktop))
                {
                    if (IsCompressdFileType(FileType))
                    {
                        IsFolder = false;
                        HasSubFolder = false;
                    }
                    else
                    {
                        uint hRes2 = m_shRootShell.BindToObject(m_pIDL, IntPtr.Zero, ref IID_IShellFolder, out m_shShellFolder);
                        if (hRes2 != 0)
                            Marshal.ThrowExceptionForHR((int)hRes2);
                        HasSubFolder = IsRealyHasSubFolder(include_hidden);
                    }
                }
                else
                {   // Internal with no set{} mutator.
                    m_shShellFolder = RootShellFolder;
                }
                m_bHaveRootShell = true;
            }
        }

        /// <summary>
        /// Constructor. Create a sub-item shell item object.
        /// </summary>
        /// <param name="shDesktop">IShellFolder interface of the Desktop</param>
        /// <param name="pIDL">The fully qualified PIDL for this shell item</param>
        /// <param name="shParent">The ShellItem object for this item's parent</param>
        public ShellItem(IShellFolder shDesktop, IntPtr pIDL, ShellItem shParent, bool include_hidden = false)
        {
            // We need the Desktop shell item to exist first.
            if (m_bHaveRootShell == false)
                throw new Exception("The root shell item must be created before creating a sub-item");

            // Create the FQ PIDL for this new item.
            m_pIDL = ILCombine(shParent.PIDL, pIDL);

            // Get the properties of this item.
            SFGAOF uFlags = SFGAOF.SFGAO_FOLDER | SFGAOF.SFGAO_HASSUBFOLDER;

            // Here we get some basic attributes.
            shDesktop.GetAttributesOf(1, out m_pIDL, out uFlags);
            IsFolder = Convert.ToBoolean(uFlags & SFGAOF.SFGAO_FOLDER);
            HasSubFolder = Convert.ToBoolean(uFlags & SFGAOF.SFGAO_HASSUBFOLDER);

            // Now we want to get extended attributes such as the icon index etc.
            SHFILEINFO shInfo = new SHFILEINFO();
            SHGFI vFlags =
                SHGFI.SHGFI_SMALLICON |
                SHGFI.SHGFI_SYSICONINDEX |
				SHGFI.SHGFI_PIDL |
                SHGFI.SHGFI_DISPLAYNAME |
				SHGFI.SHGFI_TYPENAME;
            SHGetFileInfo(m_pIDL, 0, out shInfo, (uint)Marshal.SizeOf(shInfo), vFlags);
            DisplayName = shInfo.szDisplayName;
            FileType    = shInfo.szTypeName;
			IconIndex   = shInfo.iIcon;
            Path        = GetPath();
			// 選択アイコン取得
			SHFILEINFO shInfo_openicon = new SHFILEINFO();
			SHGetFileInfo(m_pIDL, 0, out shInfo_openicon, (uint)Marshal.SizeOf(shInfo_openicon),
				SHGFI.SHGFI_PIDL |
				SHGFI.SHGFI_SMALLICON |
				SHGFI.SHGFI_SELECTED |
				SHGFI.SHGFI_ADDOVERLAYS |
				SHGFI.SHGFI_ICON
			);
			OpenIconIndex = shInfo_openicon.iIcon;
			OpenIcon = shInfo_openicon.hIcon;

			// Create the IShellFolder interface for this item.
			if (IsFolder)
            {
				if (IsCompressdFileType(FileType))
				{
					IsFolder = false;
                    HasSubFolder = false;
                }
                else
                {
                    uint hRes = shParent.m_shShellFolder.BindToObject(pIDL, IntPtr.Zero, ref IID_IShellFolder, out m_shShellFolder);
                    if (hRes != 0)
                        Marshal.ThrowExceptionForHR((int)hRes);
                    HasSubFolder = IsRealyHasSubFolder(include_hidden);
                }
            }
        }

        /// <summary>
        /// デストラクタ
        /// </summary>
        ~ShellItem() => Dispose(false);

        /// <summary>
        /// デストラクタ
        /// </summary>
        public void Dispose() => Dispose(true);
        /// <summary>
        /// デストラクタ
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed== false) 
            {
                if (disposing)
                {   // マネージドリソース解放
                
                }
                // アンマネージドリソース解放
                if (m_shRootShell != null)
                {
                    Marshal.ReleaseComObject(m_shRootShell);
                    m_shRootShell = null;
					m_bHasSubFolder = false;
				}
                // Release the IShellFolder interface of this shell item.
                if (m_shShellFolder != null)
                {
                    Marshal.ReleaseComObject(m_shShellFolder);
                    m_shShellFolder = null;
				}
                // Free the PIDL too.
                if (!m_pIDL.Equals(IntPtr.Zero))
                {
                    Marshal.FreeCoTaskMem(m_pIDL);
                    m_pIDL = IntPtr.Zero;
                }
				GC.SuppressFinalize(this);

                disposed = true;
			}
		}

        /// <summary>
        /// Gets the system path for this shell item.
        /// </summary>
        /// <returns>A path string.</returns>
        public string GetPath()
        {
            StringBuilder strBuffer = new StringBuilder(256);
            SHGetPathFromIDList(
                m_pIDL, 
                strBuffer
            );
            return strBuffer.ToString();
        }

        /// <summary>
        /// Retrieves an array of ShellItem objects for sub-folders of this shell item.
        /// </summary>
        /// <returns>ArrayList of ShellItem objects.</returns>
        public ArrayList GetSubFolders(bool include_hidden = false)
        {
            // Make sure we have a folder.
            if (IsFolder == false)
                throw new Exception("Unable to retrieve sub-folders for a non-folder.");

            ArrayList arrChildren = new ArrayList();
            try
            {
                // Get the IEnumIDList interface pointer.
                IEnumIDList pEnum = null;
                SHCONTF contf = SHCONTF.SHCONTF_FOLDERS;
                if (include_hidden)
                    contf |= SHCONTF.SHCONTF_INCLUDEHIDDEN;

				uint hRes = ShellFolder.EnumObjects(IntPtr.Zero, contf, out pEnum);
                if (hRes != 0)
                    Marshal.ThrowExceptionForHR((int)hRes);
                
                IntPtr pIDL = IntPtr.Zero;
                Int32 iGot = 0;

                // Grab the first enumeration.
                pEnum.Next(1, out pIDL, out iGot);

                // Then continue with all the rest.
                while ((pIDL.Equals(IntPtr.Zero) == false) && (iGot == 1))
                {

                    // Create the new ShellItem object.
					arrChildren.Add(new ShellItem(m_shRootShell, pIDL, this));

                    // Free the PIDL and reset counters.
                    Marshal.FreeCoTaskMem(pIDL);
                    pIDL = IntPtr.Zero;
                    iGot = 0;

                    // Grab the next item.
                    pEnum.Next(1, out pIDL, out iGot);
                }

                // Free the interface pointer.
                if (pEnum != null)
                    Marshal.ReleaseComObject(pEnum);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error:",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error
                );
            }

            return arrChildren;
        }
        /// <summary>
        /// 本当にサブフォルダがあるか？
        /// </summary>
        /// <param name="include_hidden"></param>
        /// <returns></returns>
        public bool IsRealyHasSubFolder(bool include_hidden = false)
        {
            if ((IsFolder == false) || (HasSubFolder == false))
                return false;

            try
            {
                IEnumIDList pEnum = null;
                SHCONTF contf = SHCONTF.SHCONTF_FOLDERS;
                if (include_hidden)
                    contf |= SHCONTF.SHCONTF_INCLUDEHIDDEN;

                uint hRes = ShellFolder.EnumObjects(IntPtr.Zero, contf, out pEnum);
                //if (hRes != 0)
                //    Marshal.ThrowExceptionForHR((int)hRes);
                if (hRes == 0)
                {
                    if (pEnum != null)
                    {
                        IntPtr pIDL = IntPtr.Zero;
                        Int32 iGot = 0;

                        pEnum.Next(1, out pIDL, out iGot);

                        bool result = ((pIDL.Equals(IntPtr.Zero) == false) && (iGot == 1));

                        if (pIDL.Equals(IntPtr.Zero) == false)
                            Marshal.FreeCoTaskMem(pIDL);
                        Marshal.ReleaseComObject(pEnum);
                        return result;
                    }
                }
            }
            catch { }
            return false;
		}

        /// <summary>
        /// Gets or set the display name for this shell item.
        /// </summary>
        public string DisplayName
        {
            get { return m_strDisplayName; }
            set { m_strDisplayName = value; }
        }
        string m_strDisplayName = "";

        /// <summary>
        /// Gets or sets the system image list icon index for this shell item.
        /// </summary>
        public Int32 IconIndex
        {
            get { return m_iIconIndex; }
            set { m_iIconIndex = value; }
        }
        Int32 m_iIconIndex = -1;

        /// <summary>
        /// 選択アイコンのインデックス
        /// </summary>
        public Int32 OpenIconIndex
        {
            get => m_openIconIndex;
            set => m_openIconIndex = value;
        }
        private Int32 m_openIconIndex = -1;
        /// <summary>
        /// 選択アイコンのハンドル
        /// </summary>
        public IntPtr OpenIcon
        {
            get => m_OpenIcon;
            set => m_OpenIcon = value;
        }
        private IntPtr m_OpenIcon = IntPtr.Zero;
        /// <summary>
        /// Gets the IShellFolder interface of the Desktop.
        /// </summary>
        public IShellFolder RootShellFolder
        {
            get { return m_shRootShell; }
        }
        static IShellFolder m_shRootShell = null;

        /// <summary>
        /// Gets the IShellFolder interface of this shell item.
        /// </summary>
        public IShellFolder ShellFolder
        {
            get { return m_shShellFolder; }
        }
        IShellFolder m_shShellFolder = null;

        /// <summary>
        /// Gets the fully qualified PIDL for this shell item.
        /// </summary>
        public IntPtr PIDL
        {
            get { return m_pIDL; }
        }
        IntPtr m_pIDL = IntPtr.Zero;

        /// <summary>
        /// Gets or sets a boolean indicating whether this shell item is a folder.
        /// </summary>
        public bool IsFolder
        {
            get { return m_bIsFolder; }
            set { m_bIsFolder = value; }
        }
        bool m_bIsFolder = false;

        /// <summary>
        /// Gets or sets a boolean indicating whether this shell item has any sub-folders.
        /// </summary>
        public bool HasSubFolder
        {
            get { return m_bHasSubFolder; }
            set { m_bHasSubFolder = value; }
        }
        bool m_bHasSubFolder = false;

        /// <summary>
        /// Gets or sets the system path for this shell item.
        /// </summary>
        public string Path
        {
            get { return m_strPath;  }
            set { m_strPath = value; }
        }
        string m_strPath = "";
        
        /// <summary>
        /// ファイル種別
        /// </summary>
        public string FileType
        {
            get => m_FileTYpe;
            set => m_FileTYpe = value;
        }
        private string m_FileTYpe;
    }
}
