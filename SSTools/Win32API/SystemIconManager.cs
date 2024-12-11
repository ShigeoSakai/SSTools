using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static SSTools.Win32FileInfo;

namespace SSTools
{
	/// <summary>
	/// システムアイコンマネージャ
	/// </summary>
	public class SystemIconManager
	{
		/// <summary>
		/// アイコンサイズ指定
		/// </summary>
		[Flags]
		public enum ICON_SIZE
		{
			SMALL = 0x0001,			//!< 小アイコン
			LARGE = 0x0002,			//!< 大アイコン
			EXTRA_LARGE = 0x0004,	//!< 特大アイコン
			JUMBO = 0x0008,			//!< ジャンボアイコン
		}

		/// <summary>
		/// 小アイコンリスト
		/// </summary>
		private static IntPtr SmallIconList = IntPtr.Zero;
		/// <summary>
		/// 大アイコンリスト
		/// </summary>
		private static IntPtr LargeIconList = IntPtr.Zero;
		/// <summary>
		/// 特大アイコンリスト
		/// </summary>
		private static IntPtr ExtraLargeIconList = IntPtr.Zero;
		/// <summary>
		/// ジャンボアイコンリスト
		/// </summary>
		private static IntPtr JumboIconList = IntPtr.Zero;

		/// <summary>
		/// アイコンマネージャ初期化
		/// </summary>
		/// <param name="kind">アイコンサイズ</param>
		public static void Init(ICON_SIZE kind)
		{
			// システムアイコンを取得
			SHFILEINFO finfo = new SHFILEINFO();
			if ((kind.HasFlag(ICON_SIZE.SMALL)) && (SmallIconList == IntPtr.Zero))
			{   // 小アイコン
				SmallIconList = SHGetFileInfo(".txt", FILE_ATTRIBUTE_NORMAL, out _, (uint)Marshal.SizeOf(finfo),
					SHGFI.SHGFI_USEFILEATTRIBUTES | SHGFI.SHGFI_SYSICONINDEX | SHGFI.SHGFI_SMALLICON);
			}
			if ((kind.HasFlag(ICON_SIZE.LARGE)) && (LargeIconList == IntPtr.Zero))
			{   // 大アイコン
				LargeIconList = SHGetFileInfo(".txt", FILE_ATTRIBUTE_NORMAL, out _, (uint)Marshal.SizeOf(finfo),
					SHGFI.SHGFI_USEFILEATTRIBUTES | SHGFI.SHGFI_SYSICONINDEX | SHGFI.SHGFI_LARGEICON);
			}
			// 特大アイコン
			if ((kind.HasFlag(ICON_SIZE.EXTRA_LARGE)) && (IsXpOrAbove()) && (ExtraLargeIconList == IntPtr.Zero))
			{   // XP以降
				SHGetImageList(SHIL.SHIL_EXTRALARGE, ref IID_IImageList, out ExtraLargeIconList);
			}
			if ((kind.HasFlag(ICON_SIZE.JUMBO)) && (IsVistaOrAbove()) && (JumboIconList == IntPtr.Zero))
			{   // XP以降
				SHGetImageList(SHIL.SHIL_JUMBO, ref IID_IImageList, out JumboIconList);
			}
		}
        /// <summary>
        /// アイコンマネージャ初期化
        /// </summary>
		/// <remarks>
		/// アイコンサイズは、大・小を指定
		/// </remarks>
        public static void Init() => Init(ICON_SIZE.SMALL | ICON_SIZE.LARGE);
		/// <summary>
		/// デストラクタ
		/// </summary>
		public static void Dispose()
		{
			if (SmallIconList != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(SmallIconList);
				SmallIconList = IntPtr.Zero;
			}
			if (LargeIconList != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(LargeIconList);
				LargeIconList = IntPtr.Zero;
			}
			if (ExtraLargeIconList != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(ExtraLargeIconList);
				ExtraLargeIconList = IntPtr.Zero;
			}
			if (JumboIconList != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(JumboIconList);
				JumboIconList = IntPtr.Zero;
			}
		}
		/// <summary>
		/// アイコンの取得
		/// </summary>
		/// <param name="list">アイコンリストのアドレス</param>
		/// <param name="index">インデックス</param>
		/// <returns>アイコン</returns>
		public static Icon GetIcon(IntPtr list, int index)
		{
			if (list != IntPtr.Zero)
			{
				IntPtr icon_ptr = ImageList_GetIcon(list, index, IDL.ILD_NORMAL);
				if (icon_ptr != IntPtr.Zero)
				{
					Icon result = (Icon)Icon.FromHandle(icon_ptr).Clone();
					DestroyIcon(icon_ptr);
					return result;
				}
			}
			return null;
		}


	

		/// <summary>
		/// アイコンリストのポインタを取得
		/// </summary>
		/// <param name="size">アイコンのサイズ</param>
		/// <returns>リストのポインタ。IntPtr.Zeroの場合はエラー</returns>
		private static IntPtr GetListPtr(ICON_SIZE size)
		{
			if ((size == ICON_SIZE.SMALL) && (SmallIconList != IntPtr.Zero))
				return SmallIconList;
			else if ((size == ICON_SIZE.LARGE) && (LargeIconList != IntPtr.Zero))
				return LargeIconList;
			else if ((size == ICON_SIZE.EXTRA_LARGE) && (ExtraLargeIconList != IntPtr.Zero))
				return ExtraLargeIconList;
			else if ((size == ICON_SIZE.JUMBO) && (JumboIconList != IntPtr.Zero))
				return JumboIconList;
			return IntPtr.Zero;
		}
		/// <summary>
		/// リストに登録されているアイコンの数を取得
		/// </summary>
		/// <param name="size">アイコンのサイズ</param>
		/// <returns>アイコンの数。-1の場合はエラー</returns>
		public static int GetIconCount(ICON_SIZE size = ICON_SIZE.SMALL) 
		{ 
			IntPtr list = GetListPtr(size);
			if (list != IntPtr.Zero)
				return ImageList_GetImageCount(list);
			return -1;
		}
		/// <summary>
		/// アイコンを取得
		/// </summary>
		/// <param name="index">インデックス</param>
		/// <param name="size">アイコンのサイズ</param>
		/// <returns>アイコン</returns>
		public static Icon GetIcon(int index, ICON_SIZE size = ICON_SIZE.SMALL)
		{
			IntPtr list = GetListPtr(size);
			if (list != IntPtr.Zero)
			{
				IntPtr iconPtr = ImageList_GetIcon(list, index, IDL.ILD_NORMAL);
				if (iconPtr != IntPtr.Zero)
					return Icon.FromHandle(iconPtr);
			}
			return null;
		}
		/// <summary>
		/// アイコンのサイズを取得する
		/// </summary>
		/// <param name="size"></param>
		/// <returns>アイコンサイズ</returns>
		public static Size GetIconSize(ICON_SIZE size = ICON_SIZE.SMALL)
		{
			IntPtr list = GetListPtr(size);
			if (list != IntPtr.Zero)
			{
				int cx = 0, cy = 0;
				if (ImageList_GetIconSize(list, ref cx, ref cy))
					return new Size(cx, cy);
			}
			return new Size();
		}
		/// <summary>
		/// 画像リストを初期化する
		/// </summary>
		/// <param name="imgList">画像リスト</param>
		/// <param name="size">アイコンサイズ</param>
		/// <returns>true:初期化完了</returns>
		public static bool InitImageList(ref ImageList imgList, ICON_SIZE size = ICON_SIZE.SMALL)
		{
			IntPtr list = GetListPtr(size);
			if (list != IntPtr.Zero)
			{
				imgList.Images.Clear();
				Size iconSize = GetIconSize(size);
				if (iconSize.IsEmpty == false)
				{
					imgList.ImageSize = iconSize;
					imgList.ColorDepth = ColorDepth.Depth32Bit;
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// 画像リストにアイコンを格納する
		/// </summary>
		/// <param name="imgList">画像リスト</param>
		/// <param name="icon_index">アイコンインデックス</param>
		/// <param name="size">アイコンサイズ</param>
		/// <returns>画像リストのインデックス</returns>
		public static int StoreImageList(ref ImageList imgList,int icon_index, ICON_SIZE size = ICON_SIZE.SMALL) 
		{
			IntPtr list = GetListPtr(size);
			if (list != IntPtr.Zero)
			{
				int icon_num = ImageList_GetImageCount(list);
				if ((icon_index >= 0) && (icon_index < icon_num))
				{
					string key = icon_index.ToString("X4");
					if (imgList.Images.ContainsKey(key) == false)
					{
						imgList.Images.Add(key,GetIcon(icon_index,size));
					}
					return imgList.Images.IndexOfKey(key);
				}
			}
			return -1;
		}
        /// <summary>
        /// 画像リストに「開いた」アイコンを格納する
        /// </summary>
        /// <param name="imgList">画像リスト</param>
        /// <param name="openIcon">開いたアイコンのアドレス</param>
        /// <param name="icon_index">アイコンインデックス</param>
        /// <returns>画像リストのインデックス</returns>
        public static int StoreOpenIconImageList(ref ImageList imgList, IntPtr openIcon,int icon_index)
		{
			if (openIcon != IntPtr.Zero)
			{
				string key = string.Format("O_{0:X4}", icon_index);
				if (imgList.Images.ContainsKey(key) == false)
				{
					Icon icon = Icon.FromHandle(openIcon);
					imgList.Images.Add(key,(Icon)icon.Clone());
					icon.Dispose();
				}
				DestroyIcon(openIcon);
				return imgList.Images.IndexOfKey(key);
			}
			return -1;
		}

		/// <summary>
		/// 画像リストに全てのアイコンを格納する
		/// </summary>
		/// <param name="imgList">画像リスト</param>
		/// <param name="size">アイコンサイズ</param>
		/// <returns>画像リストの件数</returns>
		public static int StoreAllIcon(ref ImageList imgList, ICON_SIZE size = ICON_SIZE.SMALL)
		{
			IntPtr list = GetListPtr(size);
			if (list != IntPtr.Zero)
			{
				imgList.Images.Clear();
				for(int index = 0; index < GetIconCount(size); index ++)
				{
					string key = index.ToString("X4");
					imgList.Images.Add(key, GetIcon(list, index));
				}
				return imgList.Images.Count;
			}
			return 0;
		}

	}
}
