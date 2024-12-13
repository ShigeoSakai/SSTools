using System;
using System.Collections.Generic;

namespace SSTools
{
	/// <summary>
	/// Windowメッセージ解析
	/// </summary>
	public partial class WndMsgAnalysis
	{
		/// <summary>
		/// 解析モード
		/// </summary>
		[Flags]
		public enum ANALYSIS_MODE
		{
			NONE = 0,                   //!< なし
            WINDOW_MSG = 0x01,          //!< Window関連
            CONTROL_MSG = 0x02,         //!< Control関連
            MOUSE_MSG = 0x04,           //!< マウス関連
            WINDOW_MGR_MSG = 0x08,      //!< ウィンドウマネージャ関連
            IME_MSG = 0x10,             //!< IME関連
            OTHER_MSG = 0x20,           //!< その他メッセージ関連

            USER_DEFINE = 0x100,        //!< ユーザー定義処理
            SHOW_DESCRIPTION = 0x1000,  //!< 説明を表示
            DEBUG = 0x2000,             //!< デバッグ

            ALL = WINDOW_MSG | CONTROL_MSG | MOUSE_MSG | WINDOW_MGR_MSG | IME_MSG | OTHER_MSG, //!< メッセージ全て
        }

		/// <summary>
		/// ID - Windowsメッセージ処理辞書
		/// </summary>
		private readonly Dictionary<ushort, WMFactory> WndMsgDictionary = new Dictionary<ushort, WMFactory>();
		/// <summary>
		/// 解析モード
		/// </summary>
		private readonly ANALYSIS_MODE AnalysisMode = ANALYSIS_MODE.NONE;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="mode">解析モード</param>
		public unsafe WndMsgAnalysis(ANALYSIS_MODE mode = ANALYSIS_MODE.NONE)
		{
			// 不正な組み合わせチェック
			Console.WriteLine(string.Format("IntPtr={0} CPU={1}", sizeof(IntPtr), Environment.Is64BitProcess ? "64" : "32"));
			System.Diagnostics.Debug.Assert(((Environment.Is64BitProcess) && (sizeof(IntPtr) == 8)) ||
				((Environment.Is64BitProcess == false) && (sizeof(IntPtr) == 4)),
				string.Format("想定されていないIntPtr({0})とCPUビット数({1})の組み合わせです",
				sizeof(IntPtr),Environment.Is64BitProcess ? "64" : "32"));
#if WIN32
			System.Diagnostics.Debug.Assert(Environment.Is64BitProcess == false, "Win32用を64bitで実行しようとしています。");
#endif
			AnalysisMode = mode;
			foreach(WMFactory factory in windowsMessageDefines)
			{
				if ((AnalysisMode.HasFlag(ANALYSIS_MODE.WINDOW_MSG)) && (factory.AnalysisMode.HasFlag(ANALYSIS_MODE.WINDOW_MSG)))
				{   // Window関連メッセージのデフォルト追加
					WndMsgDictionary.Add((ushort)factory.MsgID, factory);
				}
				else if ((AnalysisMode.HasFlag(ANALYSIS_MODE.MOUSE_MSG)) && (factory.AnalysisMode.HasFlag(ANALYSIS_MODE.MOUSE_MSG)))
				{   // マウス関連メッセージのデフォルト追加
					WndMsgDictionary.Add((ushort)factory.MsgID, factory);
				}
				else if ((AnalysisMode.HasFlag(ANALYSIS_MODE.CONTROL_MSG)) && (factory.AnalysisMode.HasFlag(ANALYSIS_MODE.CONTROL_MSG)))
				{   // Control関連メッセージのデフォルト追加
					WndMsgDictionary.Add((ushort)factory.MsgID, factory);
				}
				else if ((AnalysisMode.HasFlag(ANALYSIS_MODE.WINDOW_MGR_MSG)) && (factory.AnalysisMode.HasFlag(ANALYSIS_MODE.WINDOW_MGR_MSG)))
				{   // DWM(ウィンドウマネージャ)関連
					WndMsgDictionary.Add((ushort)factory.MsgID, factory);
				}
				else if ((AnalysisMode.HasFlag(ANALYSIS_MODE.IME_MSG)) && (factory.AnalysisMode.HasFlag(ANALYSIS_MODE.IME_MSG)))
				{   // IME関連
					WndMsgDictionary.Add((ushort)factory.MsgID, factory);
				}
				else if ((AnalysisMode.HasFlag(ANALYSIS_MODE.OTHER_MSG)) && (factory.AnalysisMode.HasFlag(ANALYSIS_MODE.OTHER_MSG)))
				{   // その他
					WndMsgDictionary.Add((ushort)factory.MsgID, factory);
				}
			}
		}

		/// <summary>
		/// 説明を取得
		/// </summary>
		/// <param name="msgId">Windowメッセージ</param>
		/// <returns>説明</returns>
		private string GetDescription(ushort msgId)
		{
			WMFactory factory = windowsMessageDefines.Find((p) => (ushort)p.MsgID == msgId);
			if (factory != null)
				return factory.Description;
			return null;
		}
		/// <summary>
		/// 処理関数を追加する
		/// </summary>
		/// <typeparam name="T">パラメータ型</typeparam>
		/// <param name="msgId">WindowメッセージID</param>
		/// <param name="msgName">Windowメッセージ名</param>
		/// <param name="func">処理関数</param>
		/// <returns>true;定義済みメッセージに登録/false:新規にメッセージを登録</returns>
		/// <remarks>
		/// WindowメッセージID もしくは Windowメッセージ名がWND_MSG_ENUMある事。
		/// （ない場合は、falseが返る）
		/// </remarks>
		public bool SetFunc<T>(ushort msgId,string msgName, Func<IntPtr, WND_MSG_ENUM, T, string, bool> func) where T :class
		{
			WND_MSG_ENUM MsgEnumID;
			if (Enum.IsDefined(typeof(WND_MSG_ENUM), msgId))
			{	// IDがある
				MsgEnumID = (WND_MSG_ENUM)msgId;
			}
			else if (Enum.TryParse<WND_MSG_ENUM>(msgName,out MsgEnumID) == false)
			{   // 変換できない
				return false;
			}

			if (WndMsgDictionary.ContainsKey((ushort)MsgEnumID))
			{	// 登録が辞書にあった
				if  (WndMsgDictionary[(ushort)MsgEnumID].SetFunc<T>(func))
					return true;
				// 型が一致しない → 定義を削除する
				WndMsgDictionary.Remove((ushort)MsgEnumID);
			}
			// 説明を取得
			string description = GetDescription((ushort)MsgEnumID);
			// 定義を新規登録
			WndMsgDictionary.Add(msgId, WMFactory.New<T>(MsgEnumID, ANALYSIS_MODE.USER_DEFINE, func, description));
			return false;
		}
		/// <summary>
		/// 処理関数を追加する
		/// </summary>
		/// <typeparam name="T">パラメータ型</typeparam>
		/// <param name="msgId">Windowメッセージ</param>
		/// <param name="func">処理関数</param>
		/// <returns>true;定義済みメッセージに登録/false:新規にメッセージを登録</returns>
		public bool SetFunc<T>(WND_MSG_ENUM msgId, Func<IntPtr, WND_MSG_ENUM, T, string, bool> func) where T : class
		{
			ushort id = (ushort)msgId;
			if (WndMsgDictionary.ContainsKey(id))
			{   // 登録が辞書にあった
				if (WndMsgDictionary[id].SetFunc<T>(func))
					return true;
				// 型が一致しない → 定義を削除する
				WndMsgDictionary.Remove(id);
			}
			// 説明を取得
			string description = GetDescription(id);
			// 定義を新規登録
			WndMsgDictionary.Add(id, WMFactory.New<T>(msgId, ANALYSIS_MODE.USER_DEFINE, func,description));
			return false;
		}

		/// <summary>
		/// メッセージ解析
		/// </summary>
		/// <param name="hWnd">Windowハンドル</param>
		/// <param name="msg">WindowメッセージID</param>
		/// <param name="wparam">WParam</param>
		/// <param name="lparam">LParam</param>
		/// <returns>true:処理済み/false:未処理</returns>
		public bool Analysis(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			bool result = false;
			// 辞書のMSG IDがあるか？
			if (WndMsgDictionary.ContainsKey((ushort)msg))
			{   
				// メッセージ名
				string name = WndMsgDictionary[(ushort)msg].MsgID.ToString();
				// 処理関数を夜b出したか？
				bool is_func_called = false;
				// 型指定がされているか？
				if (WndMsgDictionary[(ushort)msg].Type != null)
				{	// 処理関数を呼び出して結果をもらう
					result = WndMsgDictionary[(ushort)msg].Call(hWnd, wparam, lparam, AnalysisMode.HasFlag(ANALYSIS_MODE.SHOW_DESCRIPTION));
					// 処理関数呼び出し
					is_func_called　= true;
				}
				if ((AnalysisMode.HasFlag(ANALYSIS_MODE.DEBUG)) && (is_func_called == false))
				{	// デバッグモードで、処理関数を呼び出していない... メッセージをコンソールに表示
					Console.WriteLine("{0}(0x{1:X4}) {2}", name, (ushort)msg,
						((AnalysisMode.HasFlag(ANALYSIS_MODE.SHOW_DESCRIPTION)) ? WndMsgDictionary[(ushort)msg].Description : ""));
				}
			}
			else if (AnalysisMode.HasFlag(ANALYSIS_MODE.DEBUG))
			{	// デバックモード ... 未定義メッセージをコンソールに表示
				Console.WriteLine("ID:0x{0:X4} Unknown", (ushort)msg);
			}
			return result;
		}
	}
}
