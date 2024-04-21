using System;
using System.Collections.Generic;

namespace SSTools
{
	public partial class WndMsgAnalysis
	{
		[Flags]
		public enum ANALYSIS_MODE
		{
			NONE = 0,
			WINDOW_MSG = 0x01,
			CONTROL_MSG = 0x02,
			MOUSE_MSG = 0x04,
			WINDOW_MGR_MSG = 0x08,
			IIME_MSG = 0x10,
			OTHER_MSG = 0x20,
			USER_DEFINE = 0x100,
			SHOW_DESCRIPTION = 0x1000,
			DEBUG = 0x2000,
			ALL = WINDOW_MSG | CONTROL_MSG | MOUSE_MSG | WINDOW_MGR_MSG | IIME_MSG | OTHER_MSG,
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
		public WndMsgAnalysis(ANALYSIS_MODE mode = ANALYSIS_MODE.NONE)
		{
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
				else if ((AnalysisMode.HasFlag(ANALYSIS_MODE.IIME_MSG)) && (factory.AnalysisMode.HasFlag(ANALYSIS_MODE.IIME_MSG)))
				{   // IME関連
					WndMsgDictionary.Add((ushort)factory.MsgID, factory);
				}
				else if ((AnalysisMode.HasFlag(ANALYSIS_MODE.OTHER_MSG)) && (factory.AnalysisMode.HasFlag(ANALYSIS_MODE.OTHER_MSG)))
				{   // その他
					WndMsgDictionary.Add((ushort)factory.MsgID, factory);
				}
			}
		}


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
		/// <typeparam name="T"></typeparam>
		/// <param name="msgId"></param>
		/// <param name="msgName"></param>
		/// <param name="func"></param>
		/// <returns>true;定義済みメッセージに登録/false:新規にメッセージを登録</returns>
		public bool SetFunc<T>(ushort msgId,string msgName, Func<IntPtr, WND_MSG_ENUM, T, string, bool> func) where T :class
		{
			WND_MSG_ENUM MsgEnumID;
			if (Enum.IsDefined(typeof(WND_MSG_ENUM), msgId))
			{
				MsgEnumID = (WND_MSG_ENUM)msgId;
			}
			else if (Enum.TryParse<WND_MSG_ENUM>(msgName,out MsgEnumID) == false)
			{   // 変換できない
				return false;
			}

			if (WndMsgDictionary.ContainsKey((ushort)MsgEnumID))
			{
				if  (WndMsgDictionary[(ushort)MsgEnumID].SetFunc<T>(func))
					return true;
				// 定義を削除する
				WndMsgDictionary.Remove((ushort)MsgEnumID);
			}
			string description = GetDescription((ushort)MsgEnumID);
			WndMsgDictionary.Add(msgId, WMFactory.New<T>(MsgEnumID, ANALYSIS_MODE.USER_DEFINE, func, description));
			return false;
		}
		/// <summary>
		/// 処理関数を追加する
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="msgId"></param>
		/// <param name="func"></param>
		/// <returns>true;定義済みメッセージに登録/false:新規にメッセージを登録</returns>
		public bool SetFunc<T>(WND_MSG_ENUM msgId, Func<IntPtr, WND_MSG_ENUM, T, string, bool> func) where T : class
		{
			ushort id = (ushort)msgId;
			if (WndMsgDictionary.ContainsKey(id))
			{
				if (WndMsgDictionary[id].SetFunc<T>(func))
					return true;
				// 定義を削除する
				WndMsgDictionary.Remove(id);
			}
			string description = GetDescription(id);
			WndMsgDictionary.Add(id, WMFactory.New<T>(msgId, ANALYSIS_MODE.USER_DEFINE, func,description));
			return false;
		}

		/// <summary>
		/// メッセージ解析
		/// </summary>
		/// <param name="hWnd"></param>
		/// <param name="msg"></param>
		/// <param name="wparam"></param>
		/// <param name="lparam"></param>
		/// <return>true:処理済み/false:未処理</return>
		public bool Analysis(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			bool result = false;
			// 辞書のMSG IDがあるか？
			if (WndMsgDictionary.ContainsKey((ushort)msg))
			{   
				// メッセージ名
				string name = WndMsgDictionary[(ushort)msg].MsgID.ToString();
				bool is_func_called = false;
				// 型指定がされているか？
				if (WndMsgDictionary[(ushort)msg].Type != null)
				{
					result = WndMsgDictionary[(ushort)msg].Call(hWnd, wparam, lparam, AnalysisMode.HasFlag(ANALYSIS_MODE.SHOW_DESCRIPTION));
				}
				if ((AnalysisMode.HasFlag(ANALYSIS_MODE.DEBUG)) && (is_func_called == false))
				{
					Console.WriteLine("{0}(0x{1:X4}) {2}", name, (ushort)msg,
						((AnalysisMode.HasFlag(ANALYSIS_MODE.SHOW_DESCRIPTION)) ? WndMsgDictionary[(ushort)msg].Description : ""));
				}
			}
			else if (AnalysisMode.HasFlag(ANALYSIS_MODE.DEBUG))
			{
				Console.WriteLine("ID:0x{0:X4}", (ushort)msg);
			}
			return result;
		}
	}
}
