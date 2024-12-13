using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SSTools.SystemIconManager;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SSTools
{
	/// <summary>
	/// Win32 File Infomation Class
	/// </summary>
	public class Win32FileInfo
	{
		/// <summary>
		/// Normalのファイル属性
		/// </summary>
		public const Int32 FILE_ATTRIBUTE_NORMAL = 0x80;
		/// <summary>
		/// Shell FolderのGUID
		/// </summary>
		public static Guid IID_IShellFolder = new Guid("000214E6-0000-0000-C000-000000000046");
		/// <summary>
		/// Image ListのGUID
		/// </summary>
		public static Guid IID_IImageList = new Guid("46EB5926-582E-4017-9FDF-E8998DAA0950");

        /// <summary>
        /// シェルの名前空間のルートであるデスクトップ フォルダーの IShellFolder インターフェイスを取得します。
        /// </summary>
        /// <param name="ppshf">このメソッドが戻ると、デスクトップ フォルダーの IShellFolder インターフェイス ポインターを受け取ります。 
		/// 呼び出し元のアプリケーションは、最終的に IUnknown::Release メソッドを呼び出してインターフェイスを解放します。</param>
        /// <returns>この関数が成功すると、 S_OKが返されます。 そうでない場合は、HRESULT エラー コードを返します。</returns>
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
		public static extern Int32 SHGetDesktopFolder(ref IShellFolder ppshf);
        /// <summary>
        /// ファイル、フォルダー、ディレクトリ、ドライブ ルートなど、ファイル システム内のオブジェクトに関する情報を取得します。
        /// </summary>
        /// <param name="pszPath">パスとファイル名を含む最大長MAX_PATHの null終了文字列へのポインター。 絶対パスと相対パスの両方が有効です。</param>
        /// <param name="dwFileAttribs">1 つ以上の ファイル属性フラグの組み合わせ (Winnt.h で定義されている値FILE_ATTRIBUTE_)。 uFlags SHGFI_USEFILEATTRIBUTES フラグが含まれていない場合、このパラメーターは無視されます。</param>
        /// <param name="psfi">ファイル情報を受信する SHFILEINFO 構造体へのポインター。</param>
        /// <param name="cbFileInfo">psfi パラメーターによって指 SHFILEINFO 構造体のサイズ (バイト単位)。</param>
        /// <param name="uFlags">取得するファイル情報を指定するフラグ。 このパラメーターには、次の値の組み合わせを指定できます。</param>
        /// <returns>uFlags パラメーターに依存する意味を持つ値を返します。
		/// uFlags に SHGFI_EXETYPE または SHGFI_SYSICONINDEXが含まれていない場合、戻り値は成功した場合は 0 以外、それ以外の場合は 0 になります。</returns>
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttribs, out SHFILEINFO psfi, uint cbFileInfo, SHGFI uFlags);
        /// <summary>
        /// ファイル、フォルダー、ディレクトリ、ドライブ ルートなど、ファイル システム内のオブジェクトに関する情報を取得します。
        /// </summary>
        /// <param name="pIDL">シェルの名前空間内のファイルを一意に識別する項目識別子のリストを含む ITEMIDLIST (PIDL) 構造体のアドレスである必要があります。 PIDL は完全修飾 PIDL である必要があります。 相対 PIDL は使用できません。</param>
        /// <param name="dwFileAttributes">1 つ以上の ファイル属性フラグの組み合わせ (Winnt.h で定義されている値FILE_ATTRIBUTE_)。 uFlags SHGFI_USEFILEATTRIBUTES フラグが含まれていない場合、このパラメーターは無視されます。</param>
        /// <param name="psfi">ファイル情報を受信する SHFILEINFO 構造体へのポインター。</param>
        /// <param name="cbFileInfo">psfi パラメーターによって指 SHFILEINFO 構造体のサイズ (バイト単位)。</param>
        /// <param name="uFlags">取得するファイル情報を指定するフラグ。 このパラメーターには、次の値の組み合わせを指定できます。</param>
        /// <returns>uFlags パラメーターに依存する意味を持つ値を返します。
		/// uFlags に SHGFI_EXETYPE または SHGFI_SYSICONINDEXが含まれていない場合、戻り値は成功した場合は 0 以外、それ以外の場合は 0 になります。</returns>
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SHGetFileInfo(IntPtr pIDL, uint dwFileAttributes, out SHFILEINFO psfi, uint cbFileInfo, SHGFI uFlags);
        /// <summary>
        /// 特殊なフォルダーの ITEMIDLIST 構造体へのポインターを取得します。
        /// </summary>
        /// <param name="hwndOwner">予約済み。</param>
        /// <param name="nFolder">配置するフォルダーを識別するCSIDL値。 CSIDLs に関連付けられているフォルダーが特定のシステムに存在しない可能性があります。</param>
        /// <param name="ppidl">名前空間 (デスクトップ) のルートに対するフォルダーの場所を指定するアイテム識別子リスト構造へのポインターのアドレス。 失敗した場合、 ppidl パラメーターは NULL に設定されます。 呼び出し元のアプリケーションは、 ILFree を呼び出してこのリソースを解放する役割を担います。</param>
        /// <returns>成功した場合はS_OK</returns>
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
		public static extern Int32 SHGetSpecialFolderLocation(IntPtr hwndOwner, CSIDL nFolder, ref IntPtr ppidl);
        /// <summary>
        /// 特殊なフォルダーの ITEMIDLIST 構造体へのポインターを取得します。
        /// </summary>
        /// <param name="hwndOwner">予約済み。</param>
        /// <param name="nFolder">配置するフォルダーを識別するCSIDL値。 CSIDLs に関連付けられているフォルダーが特定のシステムに存在しない可能性があります。</param>
        /// <param name="ppidl">名前空間 (デスクトップ) のルートに対するフォルダーの場所を指定するアイテム識別子リスト構造へのポインターのアドレス。 失敗した場合、 ppidl パラメーターは NULL に設定されます。 呼び出し元のアプリケーションは、 ILFree を呼び出してこのリソースを解放する役割を担います。</param>
        /// <returns>成功した場合はS_OK</returns>
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
		public static extern Int32 SHGetSpecialFolderLocation(IntPtr hwndOwner, uint nFolder, ref IntPtr ppidl);
        /// <summary>
        /// 2 つの ITEMIDLIST 構造体を結合します。
        /// </summary>
        /// <param name="pIDLParent">最初の ITEMIDLIST 構造体へのポインター。</param>
        /// <param name="pIDLChild">2 番目の ITEMIDLIST 構造体へのポインター。 この構造体は、 pidl1 が指す構造体に追加されます。</param>
        /// <returns>結合された構造体を含む ITEMIDLIST を返します。 pidl1 または pidl2 を NULL に設定した場合、返される ITEMIDLIST 構造体は NULL 以外のパラメーターの複製です。 pidl1 と pidl2 の両方が NULL に設定されている場合は NULL を返します。</returns>
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr ILCombine(IntPtr pIDLParent, IntPtr pIDLChild);
        /// <summary>
        /// 項目識別子リストをファイル システム パスに変換します。
        /// </summary>
        /// <param name="pIDL">名前空間 (デスクトップ) のルートを基準にしたファイルまたはディレクトリの場所を指定する項目識別子リストのアドレス。</param>
        /// <param name="strPath">ファイル システム パスを受け取るバッファーのアドレス。 このバッファーのサイズは、少なくともMAX_PATH文字にする必要があります。</param>
        /// <returns>成功した場合 TRUE を返します。</returns>
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
		public static extern Int32 SHGetPathFromIDList(IntPtr pIDL, StringBuilder strPath);
        /// <summary>
        /// イメージ リストを取得します。
        /// </summary>
        /// <param name="iImageList">リストに含まれるイメージの種類。</param>
        /// <param name="riid">イメージ リスト インターフェイス識別子への参照(通常はIID_IImageList)。</param>
        /// <param name="handle">このメソッドが戻るとき、 には 、riid で要求されたインターフェイス ポインターが含まれます。 これは通常 、IImageList です。</param>
        /// <returns>この関数が成功すると、 S_OKが返されます。</returns>
		[DllImport("shell32.dll", EntryPoint = "#727")]
		public static extern Int32 SHGetImageList(SHIL iImageList,ref Guid riid, out IntPtr handle);


        /// <summary>
        /// イメージからアイコンを作成し、イメージ リストにマスクします。
        /// </summary>
        /// <param name="himl">イメージ リストへのハンドル。</param>
        /// <param name="i">イメージのインデックス。</param>
        /// <param name="flags">描画スタイルを指定するフラグの組み合わせ。</param>
        /// <returns>成功した場合はアイコンへのハンドルを返し、それ以外の場合は NULL を 返します</returns>
        /// <remarks>
        /// ImportするDLLは、
        ///    "comctl32"     ... OK
        ///    "comctl32.dll" ... NG（何故かマスクが適用されていないアイコンを取得する）
        ///  なので注意
        /// </remarks>
        [DllImport("comctl32", CharSet = CharSet.Auto)]
		public static extern IntPtr ImageList_GetIcon(IntPtr himl, int i, IDL flags);
        /// <summary>
        /// イメージ リスト内のイメージのディメンションを取得します。 イメージ リスト内のすべてのイメージのサイズは同じです。
        /// </summary>
        /// <param name="himl">イメージ リストへのハンドル。</param>
        /// <param name="cx">各画像の幅をピクセル単位で受け取る整数変数へのポインター。</param>
        /// <param name="cy">各画像の高さをピクセル単位で受け取る整数変数へのポインター。</param>
        /// <returns>成功した場合は 0 以外、それ以外の場合は 0 を返します。</returns>
        /// <remarks>
        /// ImportするDLLは、
        ///    "comctl32"     ... OK
        ///    "comctl32.dll" ... NG（何故かマスクが適用されていないアイコンを取得する）
        ///  なので注意
        /// </remarks>
        [DllImport("comctl32", CharSet = CharSet.Auto)]
		public static extern bool ImageList_GetIconSize(IntPtr himl, ref int cx, ref int cy);

        /// <summary>
        /// イメージ リスト内のイメージの数を取得します。
        /// </summary>
        /// <param name="himl">イメージ リストへのハンドル。</param>
        /// <returns>画像の数を返します。</returns>
        /// ImportするDLLは、
        ///    "comctl32"     ... OK
        ///    "comctl32.dll" ... NG（何故かマスクが適用されていないアイコンを取得する）
        ///  なので注意
        /// </remarks>
        [DllImport("comctl32", CharSet = CharSet.Auto)]
		public static extern int ImageList_GetImageCount(IntPtr himl);
        /// <summary>
        /// アイコンを破棄し、アイコンが占有していたメモリを解放します。
        /// </summary>
        /// <param name="handle">破棄するアイコンのハンドル。 アイコンを使用することはできません。</param>
        /// <returns>関数が成功すると、戻り値は 0 以外になります。関数が失敗した場合は、0 を返します。</returns>
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool DestroyIcon(IntPtr handle);
        /// <summary>
        /// ファイル名またはフォルダー名の種類
        /// </summary>
		[Flags]
		public enum SHGNO : uint
		{
			SHGDN_NORMAL = 0x0000,                 //!< Default (display purpose)
			SHGDN_INFOLDER = 0x0001,               //!< Displayed under a folder (relative)
            SHGDN_FOREDITING = 0x1000,             //!< For in-place editing
            SHGDN_FORADDRESSBAR = 0x4000,          //!< UI friendly parsing name (remove ugly stuff)
            SHGDN_FORPARSING = 0x8000,             //!< Parsing name for ParseDisplayName()
        }
        /// <summary>
        /// 列挙に含まれる項目の種類
        /// </summary>
		[Flags]
		public enum SHCONTF : uint
		{
			SHCONTF_FOLDERS = 0x0020,              //!< Only want folders enumerated (SFGAO_FOLDER)
            SHCONTF_NONFOLDERS = 0x0040,           //!< Include non folders
            SHCONTF_INCLUDEHIDDEN = 0x0080,        //!< Show items normally hidden
            SHCONTF_INIT_ON_FIRST_NEXT = 0x0100,   //!< Allow EnumObject() to return before validating enum
            SHCONTF_NETPRINTERSRCH = 0x0200,       //!< Hint that client is looking for printers
            SHCONTF_SHAREABLE = 0x0400,            //!< Hint that client is looking sharable resources (remote shares)
            SHCONTF_STORAGE = 0x0800,              //!< Include all items with accessible storage and their ancestors
        }
        /// <summary>
        /// アイテム (ファイルまたはフォルダー) またはアイテムのセットで取得できる属性
        /// </summary>
		[Flags]
		public enum SFGAOF : uint
		{
			SFGAO_CANCOPY = 0x1,                   //!< Objects can be copied  (DROPEFFECT_COPY)
            SFGAO_CANMOVE = 0x2,                   //!< Objects can be moved   (DROPEFFECT_MOVE)
            SFGAO_CANLINK = 0x4,                   //!< Objects can be linked  (DROPEFFECT_LINK)
            SFGAO_STORAGE = 0x00000008,            //!< Supports BindToObject(IID_IStorage)
            SFGAO_CANRENAME = 0x00000010,          //!< Objects can be renamed
            SFGAO_CANDELETE = 0x00000020,          //!< Objects can be deleted
            SFGAO_HASPROPSHEET = 0x00000040,       //!< Objects have property sheets
            SFGAO_DROPTARGET = 0x00000100,         //!< Objects are drop target
            SFGAO_CAPABILITYMASK = 0x00000177,     //!< Capability Mask
            SFGAO_ENCRYPTED = 0x00002000,          //!< Object is encrypted (use alt color)
            SFGAO_ISSLOW = 0x00004000,             //!< 'Slow' object
            SFGAO_GHOSTED = 0x00008000,            //!< Ghosted icon
            SFGAO_LINK = 0x00010000,               //!< Shortcut (link)
            SFGAO_SHARE = 0x00020000,              //!< Shared
            SFGAO_READONLY = 0x00040000,           //!< Read-only
            SFGAO_HIDDEN = 0x00080000,             //!< Hidden object
            SFGAO_DISPLAYATTRMASK = 0x000FC000,    //!< Display Attribute Mask
            SFGAO_FILESYSANCESTOR = 0x10000000,    //!< May contain children with SFGAO_FILESYSTEM
            SFGAO_FOLDER = 0x20000000,             //!< Support BindToObject(IID_IShellFolder)
            SFGAO_FILESYSTEM = 0x40000000,         //!< Is a win32 file system object (file/folder/root)
            SFGAO_HASSUBFOLDER = 0x80000000,       //!< May contain children with SFGAO_FOLDER
            SFGAO_CONTENTSMASK = 0x80000000,       //!< Contexts Mask
            SFGAO_VALIDATE = 0x01000000,           //!< Invalidate cached information
            SFGAO_REMOVABLE = 0x02000000,          //!< Is this removeable media?
            SFGAO_COMPRESSED = 0x04000000,         //!< Object is compressed (use alt color)
            SFGAO_BROWSABLE = 0x08000000,          //!< Supports IShellFolder, but only implements CreateViewObject() (non-folder view)
            SFGAO_NONENUMERATED = 0x00100000,      //!< Is a non-enumerated object
            SFGAO_NEWCONTENT = 0x00200000,         //!< Should show bold in explorer tree
            SFGAO_CANMONIKER = 0x00400000,         //!< Defunct
            SFGAO_HASSTORAGE = 0x00400000,         //!< Defunct
            SFGAO_STREAM = 0x00400000,             //!< Supports BindToObject(IID_IStream)
            SFGAO_STORAGEANCESTOR = 0x00800000,    //!< May contain children with SFGAO_STORAGE or SFGAO_STREAM
            SFGAO_STORAGECAPMASK = 0x70C50008,     //!< For determining storage capabilities, ie for open/save semantics
        }
        /// <summary>
        /// IShellFolderインターフェイスメソッドから返される文字列の種類
        /// </summary>
		[Flags]
		public enum STRRET : uint
		{
			STRRET_WSTR = 0,		//!< WSTR
			STRRET_OFFSET = 0x1,	//!< Offset
			STRRET_CSTR = 0x2,		//!< CStr
		}
        /// <summary>
        /// 取得するファイル情報を指定するフラグ
        /// </summary>
        [Flags]
		public enum SHGFI
		{
			SHGFI_ICON = 0x000000100,					//!< Icon
			SHGFI_DISPLAYNAME = 0x000000200,			//!< Diaplay Name
			SHGFI_TYPENAME = 0x000000400,				//!< Type Name
			SHGFI_ATTRIBUTES = 0x000000800,				//!< Attributes
			SHGFI_ICONLOCATION = 0x000001000,			//!< Icon Location
			SHGFI_EXETYPE = 0x000002000,				//!< Exe Type
			SHGFI_SYSICONINDEX = 0x000004000,			//!< System Icon Index
			SHGFI_LINKOVERLAY = 0x000008000,			//!< Link Overlay
			SHGFI_SELECTED = 0x000010000,				//!< Selected
			SHGFI_ATTR_SPECIFIED = 0x000020000,			//!< Attribute Specified
			SHGFI_LARGEICON = 0x000000000,				//!< Large Icon
			SHGFI_SMALLICON = 0x000000001,				//!< Small Icon
			SHGFI_OPENICON = 0x000000002,				//!< Open Icon
			SHGFI_SHELLICONSIZE = 0x000000004,			//!< Shell Icon Size
			SHGFI_PIDL = 0x000000008,					//!< PIDL
			SHGFI_USEFILEATTRIBUTES = 0x000000010,		//!< Use File Attribute
			SHGFI_ADDOVERLAYS = 0x000000020,			//!< Add Overlays
			SHGFI_OVERLAYINDEX = 0x000000040			//!< Overlay Index
		}
        /// <summary>
        /// 配置するフォルダーを識別するCSIDL値
        /// </summary>
        [Flags]
		public enum CSIDL : uint
		{
			CSIDL_DESKTOP = 0x0000,		//!< Desktop
			CSIDL_WINDOWS = 0x0024		//!< WIndows
		}
        /// <summary>
        /// 取得するIconの種類
        /// </summary>
		public enum SHIL
		{
            /// <summary>
            /// 画像のサイズは通常 32 x 32 ピクセルです。
            /// ただし、[表示プロパティ] の[外観] タブの[効果] セクションで[大きいアイコンを使用する] オプションが選択されている場合、
            /// 画像は 48 x 48 ピクセルです。
            /// </summary>
            SHIL_LARGE = 0x0000,
            /// <summary>
            /// これらのイメージはシェル標準の小さいアイコン サイズ 16 x 16 ですが、サイズはユーザーがカスタマイズできます。
            /// </summary>
            SHIL_SMALL = 0x0001,
            /// <summary>
            /// これらのイメージは、シェル標準の特大アイコン サイズです。 これは通常 48 x 48 ですが、サイズはユーザーがカスタマイズできます。
            /// </summary>
            SHIL_EXTRALARGE = 0x002,
            /// <summary>
            /// これらのイメージは、SM_CXSMICON で呼び出される GetSystemMetrics と 、SM_CYSMICON で呼び出された GetSystemMetricsで指定されたサイズです。
            /// </summary>
            SHIL_SYSSMALL =   0x003,
            /// <summary>
            /// Windows Vista 以降。 画像は通常 256 x 256 ピクセルです。
            /// </summary>
            SHIL_JUMBO = 0x004,
			/// <summary>
			/// 最後
			/// </summary>
			SHIL_LAST
		}
        /// <summary>
        /// Icon描画方法
        /// </summary>
		[Flags]
		public enum IDL
		{
            /// <summary>
            /// イメージ リストの背景色を使用してイメージを描画します。 背景色がCLR_NONE値の場合、イメージはマスクを使用して透明に描画されます。
            /// </summary>
            ILD_NORMAL = 0x00000000,
            /// <summary>
            /// 背景色に関係なく、マスクを使用してイメージを透明に描画します。 イメージ リストにマスクが含まれていない場合、この値は無効です。
            /// </summary>
            ILD_TRANSPARENT = 0x00000001,
            /// <summary>
            /// 画像を描画し、 rgbFg で指定されたブレンド色で 25% ブレンドします。 イメージ リストにマスクが含まれていない場合、この値は無効です。
            /// </summary>
            ILD_BLEND25 = 0x00000002,
            /// <summary>
            /// ILD_BLEND25と同じです。
            /// </summary>
            ILD_FOCUS = 0x00000002,
            /// <summary>
            /// 画像を描画し、 rgbFg で指定されたブレンド色で 50% ブレンドします。 イメージ リストにマスクが含まれていない場合、この値は無効です。
            /// </summary>
            ILD_BLEND50 = 0x00000004,
            /// <summary>
            /// ILD_BLEND50と同じです。
            /// </summary>
            ILD_SELECTED = 0x00000004,
            /// <summary>
            // ILD_BLEND50/ILD_SELECTEDと同じです。
            /// </summary>
            ILD_BLEND = 0x00000004,
            /// <summary>
            // マスクを描画します。
            /// </summary>
            ILD_MASK = 0x00000010,
            /// <summary>
            /// オーバーレイでマスクを描画する必要がない場合は、このフラグを設定します。
            /// </summary>
            ILD_IMAGE = 0x00000020
		}
		/// <summary>
		/// Shell File Info構造体
		/// </summary>
		[StructLayout(LayoutKind.Sequential, CharSet= CharSet.Auto)]
		public struct SHFILEINFO
		{
			/// <summary>
			/// Iconのポインタ
			/// </summary>
			public IntPtr hIcon;
			/// <summary>
			/// Iconのインデックス
			/// </summary>
			public int iIcon;
			/// <summary>
			/// 属性値
			/// </summary>
			public uint dwAttributes;
			/// <summary>
			/// 表示名
			/// </summary>
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szDisplayName;
			/// <summary>
			/// ファイルタイプ名
			/// </summary>
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szTypeName;
		}

		/// <summary>
		/// Shell Folder I/F
		/// </summary>
		[ComImport]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("000214E6-0000-0000-C000-000000000046")]
		public interface IShellFolder
		{
            /// <summary>
            /// Translates a file object's or folder's display name into an item identifier list.
            /// </summary>
            /// <returns>error code, if any</returns>
            [PreserveSig()]
			uint ParseDisplayName(
				IntPtr hwnd,             //!< Optional window handle
                IntPtr pbc,              //!< Optional bind context that controls the parsing operation. This parameter is normally set to NULL. 
                [In(), MarshalAs(UnmanagedType.LPWStr)]
				string pszDisplayName,   //!< Null-terminated UNICODE string with the display name.
                out uint pchEaten,       //!< Pointer to a ULONG value that receives the number of characters of the display name that was parsed.
                out IntPtr ppidl,        //!< Pointer to an ITEMIDLIST pointer that receives the item identifier list for the object.
                ref uint pdwAttributes   //!< Optional parameter that can be used to query for file attributes. This can be values from the SFGAO enum
            );
            /// <summary>
            /// Allows a client to determine the contents of a folder by creating an item identifier enumeration object and returning its IEnumIDList interface.
            /// </summary>
            /// <returns>error code, if any</returns>
            [PreserveSig()]
			uint EnumObjects(
				IntPtr hwnd,                    //!< If user input is required to perform the enumeration, this window handle should be used by the enumeration object as the parent window to take user input.
                SHCONTF grfFlags,               //!< Flags indicating which items to include in the enumeration. For a list of possible values, see the SHCONTF enum. 
                out IEnumIDList ppenumIDList    //!< Address that receives a pointer to the IEnumIDList interface of the enumeration object created by this method. 
            );
            /// <summary>
            /// Retrieves an IShellFolder object for a subfolder.
            /// </summary>
            /// <returns>error code, if any</returns>
            [PreserveSig()]
            uint BindToObject(
                IntPtr pidl,            //!< Address of an ITEMIDLIST structure (PIDL) that identifies the subfolder.
                IntPtr pbc,             //!< Optional address of an IBindCtx interface on a bind context object to be used during this operation.
                [In()]
                ref Guid riid,          //!< Identifier of the interface to return. 
                out IShellFolder ppv        //!< Address that receives the interface pointer.
            );
            /// <summary>
            /// Requests a pointer to an object's storage interface
            /// </summary>
            /// <returns>error code, if any</returns>
            [PreserveSig()]
			uint BindToStorage(
				IntPtr pidl,            //!< Address of an ITEMIDLIST structure that identifies the subfolder relative to its parent folder. 
                IntPtr pbc,             //!< Optional address of an IBindCtx interface on a bind context object to be used during this operation.
                [In()]
				ref Guid riid,          //!< Interface identifier (IID) of the requested storage interface.
                [MarshalAs(UnmanagedType.Interface)]
				out object ppv         //!< Address that receives the interface pointer specified by riid.
            );
            /// <summary>
            /// Determines the relative order of two file objects or folders, given their item identifier lists. 
            /// Return value: If this method is successful, the CODE field of the HRESULT contains one of the following values (the code can be retrived using the helper function GetHResultCode)...
            /// </summary>
            /// <returns>
            /// A negative return value indicates that the first item should precede the second (pidl1 < pidl2). 
            /// A positive return value indicates that the first item should follow the second (pidl1 > pidl2).  Zero A return value of zero indicates that the two items are the same (pidl1 = pidl2). 
            /// </returns>
            [PreserveSig()]
            int CompareIDs(
                int lParam,             //!< Value that specifies how the comparison should be performed. The lower sixteen bits of lParam define the sorting rule.
                                        // The upper sixteen bits of lParam are used for flags that modify the sorting rule. values can be from the SHCIDS enum
                IntPtr pidl1,           //!< Pointer to the first item's ITEMIDLIST structure.
                IntPtr pidl2            //!< Pointer to the second item's ITEMIDLIST structure.
            );
            /// <summary>
            /// Requests an object that can be used to obtain information from or interact with a folder object.
            /// </summary>
            /// <returns>error code, if any</returns>
            [PreserveSig()]
			uint CreateViewObject(
				IntPtr hwndOwner,       //!< Handle to the owner window.
				[In()]
				ref Guid riid,          //!< Identifier of the requested interface.
				[MarshalAs(UnmanagedType.Interface)]
				out object ppv          //!< Address of a pointer to the requested interface. 
            );
            /// <summary>
            // Retrieves the attributes of one or more file objects or subfolders. 
            /// </summary>
            /// <returns></returns>
            [PreserveSig()]
            uint GetAttributesOf(
                int cidl,               //!< Number of file objects from which to retrieve attributes. 
                out IntPtr apidl,       //!< Address of an array of pointers to ITEMIDLIST structures, each of which uniquely identifies a file object relative to the parent folder.
                out SFGAOF rgfInOut   //!< Address of a single ULONG value that, on entry, contains the attributes that the caller is requesting. On exit, this value contains the requested attributes that are common to all of the specified objects. this value can be from the SFGAO enum
            );
            /// <summary>
            /// Retrieves an OLE interface that can be used to carry out actions on the specified file objects or folders. 
            /// </summary>
            /// <returns>error code, if any</returns>
            [PreserveSig()]
			uint GetUIObjectOf(
				IntPtr hwndOwner,       //!< Handle to the owner window that the client should specify if it displays a dialog box or message box.
                int cidl,               //!< Number of file objects or subfolders specified in the apidl parameter. 
                [In(), MarshalAs(UnmanagedType.LPArray)] IntPtr[]
				apidl,                  //!< Address of an array of pointers to ITEMIDLIST structures, each of which uniquely identifies a file object or subfolder relative to the parent folder.
                [In()]
				ref Guid riid,          //!< Identifier of the COM interface object to return.
                IntPtr rgfReserved,     //!< Reserved. 
                [MarshalAs(UnmanagedType.Interface)]
				out object ppv          //!< Pointer to the requested interface.
            );
            /// <summary>
            /// Retrieves the display name for the specified file object or subfolder. 
            /// </summary>
            /// <returns>error code, if any</returns>
            [PreserveSig()]
            uint GetDisplayNameOf(
                IntPtr pidl,            //!< Address of an ITEMIDLIST structure (PIDL) that uniquely identifies the file object or subfolder relative to the parent folder. 
                SHGNO uFlags,           //!< Flags used to request the type of display name to return. For a list of possible values. 
                out STRRET pName      //!< Address of a STRRET structure in which to return the display name.
            );
            /// <summary>
            /// Sets the display name of a file object or subfolder, changing the item identifier in the process.
            /// </summary>
            /// <returns>error code, if any</returns>
            [PreserveSig()]
			uint SetNameOf(
				IntPtr hwnd,            //!< Handle to the owner window of any dialog or message boxes that the client displays.
				IntPtr pidl,            //!< Pointer to an ITEMIDLIST structure that uniquely identifies the file object or subfolder relative to the parent folder. 
                [In(), MarshalAs(UnmanagedType.LPWStr)]
				string pszName,         //!< Pointer to a null-terminated string that specifies the new display name. 
                SHGNO uFlags,           //!< Flags indicating the type of name specified by the lpszName parameter. For a list of possible values, see the description of the SHGNO enum. 
                out IntPtr ppidlOut     //!< Address of a pointer to an ITEMIDLIST structure which receives the new ITEMIDLIST. 
            );
        }
		/// <summary>
		/// IDList I/F
		/// </summary>
		[ComImport]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("000214F2-0000-0000-C000-000000000046")]
		public interface IEnumIDList
		{

            /// <summary>
			/// Retrieves the specified number of item identifiers in the enumeration sequence and advances the current position by the number of items retrieved. 
            /// </summary>
            /// <returns>error code, if any</returns>
			[PreserveSig()]
            uint Next(
				uint celt,                //!< Number of elements in the array pointed to by the rgelt parameter.
				out IntPtr rgelt,         //!< Address of an array of ITEMIDLIST pointers that receives the item identifiers. The implementation must allocate these item identifiers using the Shell's allocator (retrieved by the SHGetMalloc function). 
                                          //!< The calling application is responsible for freeing the item identifiers using the Shell's allocator.
                out Int32 pceltFetched    //!< Address of a value that receives a count of the item identifiers actually returned in rgelt. The count can be smaller than the value specified in the celt parameter. This parameter can be NULL only if celt is one. 
                );

            /// <summary>
            /// Skips over the specified number of elements in the enumeration sequence. 
            /// </summary>
            /// <returns>error code, if any</returns>
            [PreserveSig()]
			uint Skip(
				uint celt                 //!< Number of item identifiers to skip.
                );

            /// <summary>
            /// Returns to the beginning of the enumeration sequence. 
            /// </summary>
            /// <returns>error code, if any</returns>
            [PreserveSig()]
			uint Reset();

            /// <summary>
            /// Creates a new item enumeration object with the same contents and state as the current one. 
            /// </summary>
            /// <returns>error code, if any</returns>
            [PreserveSig()]
			uint Clone(
				out IEnumIDList ppenum    //!< Address of a pointer to the new enumeration object. The calling application must eventually free the new object by calling its Release member function. 
				);
		}
		/// <summary>
		/// WindowsXP以上か？
		/// </summary>
		/// <returns>true:WindowsXP以上</returns>
		public static bool IsXpOrAbove() => ((Environment.OSVersion.Version.Major > 5) ||
			((Environment.OSVersion.Version.Major == 5) && (Environment.OSVersion.Version.Minor >= 1)));
        /// <summary>
        /// Windows2000以上か？
        /// </summary>
        /// <returns>true:Windows2000以上</returns>
        public static bool Is2KOrAbove() => (Environment.OSVersion.Version.Major >= 5);
        /// <summary>
        /// WindowsVista以上か？
        /// </summary>
        /// <returns>true:WindowsVista以上</returns>
        public static bool IsVistaOrAbove() => (Environment.OSVersion.Version.Major >= 6);
	}
}
