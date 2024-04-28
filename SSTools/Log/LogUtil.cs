using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace SSTools
{
    /// <summary>
    /// ログユーティリティ
    /// </summary>
    /// <remarks>
    /// 使い方：
    ///    LogUtil.Log("log.txt").WriteLine("a={0},b={1}",a,b);
    ///    LogUtil.Log("log.txt",LogUtil.KIND.SHORT,"[ERROR]").WriteLine(ex.toString());
    ///    LogUtil.Console(LogUtil.KIND.TRACE,"[Info]").DebugWriteLine("This is {0}",zzz);
    /// </remarks>
    public static class LogUtil
    {
        /// <summary>
        /// Console出力の文字列
        /// </summary>
        public const string CONSOLE = "Console:";

        /// <summary>
        /// 呼び出し元表示
        /// </summary>
        [Flags]
        public enum KIND
        {
            /// <summary>
            /// なし
            /// </summary>
            NONE = 0,
            /// <summary>
            /// メソッド名
            /// </summary>
            METHOD = 0x01,
            /// <summary>
            /// ファイル名
            /// </summary>
            FILE_NAME = 0x02,
            /// <summary>
            /// ファイル名(フルパス)
            /// </summary>
            /// <remarks>FILE_NAMEとセットで指定</remarks>
            FULL_PATH = 0x04,
            /// <summary>
            /// 行番号
            /// </summary>
            LINE_NO = 0x08,
            /// <summary>
            /// スタックトレース
            /// </summary>
            TRACE = 0x10,

            /// <summary>
            /// 全て(メソッド名,フルパスファイル名,行番号)
            /// </summary>
            ALL = METHOD | FILE_NAME | FULL_PATH |LINE_NO,
            /// <summary>
            /// 短いフォーマット(メソッド名,ファイル名,行番号)
            /// </summary>
            SHORT = METHOD | FILE_NAME | LINE_NO,
            /// <summary>
            /// 短いソース(ファイル名,行番号)
            /// </summary>
            SOURCE_SHORT = FILE_NAME | LINE_NO,
            /// <summary>
            /// ソース(フルパスファイル名,行番号)
            /// </summary>
            SOURCE = SOURCE_SHORT | FULL_PATH,
        }

        /// <summary>
        /// LogDetailオブジェクト取得
        /// </summary>
        /// <param name="kind">呼び出し元表示</param>
        /// <param name="tag">タグ</param>
        /// <param name="memberName">メンバー名</param>
        /// <param name="sourceFilePath">ソースファイルパス</param>
        /// <param name="sourceLineNumber">行番号</param>
        /// <returns>LogDetailオブジェクト</returns>
        public static LogDetail Log(
            KIND kind = KIND.NONE,
            string tag = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return new LogDetail(memberName, sourceFilePath, sourceLineNumber,null,tag,kind);
        }
        /// <summary>
        /// LogDetailオブジェクト取得
        /// </summary>
        /// <param name="log_path">ログパス</param>
        /// <param name="kind">呼び出し元表示</param>
        /// <param name="tag">タグ</param>
        /// <param name="memberName">メンバー名</param>
        /// <param name="sourceFilePath">ソースファイルパス</param>
        /// <param name="sourceLineNumber">行番号</param>
        /// <returns>LogDetailオブジェクト</returns>
        public static LogDetail Log(
            string log_path,
            KIND kind = KIND.NONE,
            string tag = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return new LogDetail(memberName, sourceFilePath, sourceLineNumber,log_path,tag,kind);
        }
        /// <summary>
        /// Console用LogDetailオブジェクト取得
        /// </summary>
        /// <param name="kind">呼び出し元表示</param>
        /// <param name="tag">タグ</param>
        /// <param name="memberName">メンバー名</param>
        /// <param name="sourceFilePath">ソースファイルパス</param>
        /// <param name="sourceLineNumber">行番号</param>
        /// <returns>LogDetailオブジェクト</returns>
        public static LogDetail Console(
            KIND kind = KIND.NONE,
            string tag = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return new LogDetail(memberName, sourceFilePath, sourceLineNumber, CONSOLE,tag, kind);
        }
    }
    /// <summary>
    /// ログ詳細クラス
    /// </summary>
    public class LogDetail
    {
        /// <summary>
        /// コンソールと判定する文字列
        /// </summary>
        private static readonly string[] ConsoleStrings = new string[]
        {
            "console",
            "console:",
            "stdout",
            "stdout:",
            "stderr",
            "stderr:",
            "con",
            "con:",
        };
        /// <summary>
        /// 呼び出し元メソッド名
        /// </summary>
        private readonly string _memberName;
        /// <summary>
        /// 呼び出し元ソースパス
        /// </summary>
        private readonly string _sourceFilePath;
        /// <summary>
        /// 呼び出し元行番号
        /// </summary>
        private readonly int _sourceLineNumber;
        /// <summary>
        /// ログファイルパス
        /// </summary>
        private readonly string _log_path;
        /// <summary>
        /// 呼び出し元表示
        /// </summary>
        private readonly LogUtil.KIND _kind;
        /// <summary>
        /// タグ
        /// </summary>
        private readonly string _tag;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="memberName">呼び出し元メソッド名</param>
        /// <param name="sourceFilePath">呼び出し元ソースパス</param>
        /// <param name="sourceLineNumber">行番号</param>
        /// <param name="log_path">ログのパス</param>
        /// <param name="kind">呼び出し元表示</param>
        public LogDetail(string memberName, string sourceFilePath, int sourceLineNumber,string log_path = null,string tag = null,LogUtil.KIND kind = LogUtil.KIND.NONE)
        {
            _memberName = memberName;
            _sourceFilePath = sourceFilePath;
            _sourceLineNumber = sourceLineNumber;
            _log_path = log_path;
            _kind = kind;
            _tag = tag;
        }

        /// <summary>
        /// 呼び出し元情報作成
        /// </summary>
        /// <returns></returns>
        private string GetCallerInfo()
        {
            string text = string.Empty;

            if (_kind.HasFlag(LogUtil.KIND.TRACE))
            {   // スタックトレースの場合...
                //  スタックトレースを取得
                StackTrace st = new StackTrace(true);
                // インデント
                string stackIndent = "  ";
                // スタックトレース表示
                text += Environment.NewLine + stackIndent + "Stack Trace:" + Environment.NewLine;
                bool isLog = true;
                // 自分の呼び出しまでは無視(0～3Frame)
                for (int i = 4; i < st.FrameCount; i++)
                {
                    // スタックフレームを取得
                    StackFrame sf = st.GetFrame(i);
                    if (sf.GetFileName() != null)
                    {   // ファイル名がnull以外 ... C#のモジュール
                        text += stackIndent + sf.GetMethod() + " at " +
                            string.Format("{0}({1})", sf.GetFileName(), sf.GetFileLineNumber()) +
                            Environment.NewLine;

                        stackIndent += "  ";
                        isLog = true;
                    }
                    else
                    {   // ファイル名がnull ... Native Method
                        if (isLog)
                        {
                            text += stackIndent + "...(Native Methods)..." + Environment.NewLine;
                            isLog = false;
                        }
                    }
                }
                return text;
            }
            else
            {
                // 呼び出し元メソッド名
                if (_kind.HasFlag(LogUtil.KIND.METHOD))
                {
                    text += _memberName;
                    if (_kind.HasFlag(LogUtil.KIND.FILE_NAME) ||
                        _kind.HasFlag(LogUtil.KIND.FULL_PATH) ||
                        _kind.HasFlag(LogUtil.KIND.LINE_NO))
                        text += " at ";
                }
                // 呼び出し元ファイル名
                if (_kind.HasFlag(LogUtil.KIND.FILE_NAME))
                    if (_kind.HasFlag(LogUtil.KIND.FULL_PATH))
                        text += _sourceFilePath;
                    else
                        text += Path.GetFileName(_sourceFilePath);
                // 呼び出し元行番号
                if (_kind.HasFlag(LogUtil.KIND.LINE_NO))
                    text += string.Format("({0})", _sourceLineNumber);
                return ((text != null) && (text.Length > 0)) ? text + ":" : "";
            }
        }

        /// <summary>
        /// ファイル名がコンソール文字列かチェック
        /// </summary>
        /// <param name="log_path">パス</param>
        /// <returns></returns>
        private bool CheckConsole(string log_path)
        {
            if ((log_path != null) && (log_path.Trim().Length > 0))
                return ConsoleStrings.Contains(log_path.ToLower());
            else 
                return true;
        }
        /// <summary>
        /// フォーマット指定ログ出力
        /// </summary>
        /// <param name="format">書式フォーマット</param>
        /// <param name="args">出力するオブジェクト</param>
        public void WriteFormat(string format, params object[] args)
        {
            string date_str = DateTime.Now.ToString("yy/MM/dd HH:mm:ss.fff") + " : ";
            string tag_str = ((_tag != null) && (_tag.Length > 0)) ? _tag + ":" : "";
            if (CheckConsole(_log_path))
                Console.Write(date_str + tag_str + GetCallerInfo() + string.Format(format, args));
            else
                File.AppendAllText(_log_path, date_str + tag_str + GetCallerInfo() + string.Format(format, args));
        }

        /// <summary>
        /// フォーマット指定ログ出力
        /// </summary>
        /// <param name="format">書式フォーマット</param>
        /// <param name="args">出力するオブジェクト</param>
        public void WriteFormatLine(string format, params object[] args)
            => WriteFormat(format + Environment.NewLine, args);

        /// <summary>
        /// オブジェクトの型を文字列変換
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns>型文字列</returns>
        private string GetObject(object obj)
        {
            string typeStr = obj.GetType().ToString();
            if (typeStr != null)
            {
                string[] typeStrs = typeStr.Split('.');
                return typeStrs[typeStrs.Length - 1] + ":" + obj.ToString();
            }
            return obj.ToString();
        }
        /// <summary>
        /// フォーマット文字列かチェック
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns>true:フォーマット文字列</returns>
        private bool IsFormatStr(object obj)
        {
            if ((obj != null) && (obj is string str))
            {
                if (Regex.IsMatch(str, @".*\{\d+.*\}"))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 出力文字列生成
        /// </summary>
        /// <param name="tag">タグ</param>
        /// <param name="args">オブジェクト</param>
        /// <returns>出力する文字列</returns>
        public string GetValueText(params object[] args)
        {
            // 日付
            string date_str = DateTime.Now.ToString("yy/MM/dd HH:mm:ss.fff") + " : ";
            // タグ
            string text = ((_tag != null) && (_tag.Length > 0)) ?  _tag + ":" : "";
            // 呼び出し元を追加
            text += GetCallerInfo();
            if (IsFormatStr(args[0]))
            {   // 先頭がFormat文字列なのでstring.Formatを呼ぶ
                text += string.Format((string)args[0], args.Skip(1).ToArray());
            }
            else
            {   // オブジェクトを文字列に変換
                foreach (object obj in args)
                {
                    text += GetObject(obj) + " , ";
                }
                text = text.Substring(0, text.Length - 3);
            }
            return date_str + text;
        }
        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="args">オブジェクト</param>
        /// <remarks>
        /// argsの先頭が文字列かつFormat文字列の場合は、
        /// string.Format()を使用して変換する。
        /// </remarks>
        public void Write(params object[] args)
        {
            string text = GetValueText(args);
            if (CheckConsole(_log_path))
                Console.Write(text);
            else
                File.AppendAllText(_log_path, text);
        }
        /// <summary>
        /// ログ出力(改行あり)
        /// </summary>
        /// <param name="args">オブジェクト</param>
        /// <remarks>
        /// argsの先頭が文字列かつFormat文字列の場合は、
        /// string.Format()を使用して変換する。
        /// </remarks>
        public void WriteLine(params object[] args)
        {
            string text = GetValueText(args);
            if (CheckConsole(_log_path))
                Console.WriteLine(text);
            else
                File.AppendAllText(_log_path, text + Environment.NewLine);
        }

#if DEBUG
        /// <summary>
        /// デバッグ用フォーマット指定ログ出力
        /// </summary>
        /// <param name="format">書式フォーマット</param>
        /// <param name="args">出力するオブジェクト</param>
        public void DebugWriteFormat(string format, params object[] args)
            => WriteFormat(format, args);

        /// <summary>
        /// デバッグ用フォーマット指定ログ出力(改行あり)
        /// </summary>
        /// <param name="format">書式フォーマット</param>
        /// <param name="args">出力するオブジェクト</param>
        public void DebugWriteFormatLine(string format, params object[] args)
            => WriteFormatLine(format, args);
        /// <summary>
        /// デバッグ用ログ出力
        /// </summary>
        /// <param name="args">オブジェクト</param>
        /// <remarks>
        /// argsの先頭が文字列かつFormat文字列の場合は、
        /// string.Format()を使用して変換する。
        /// </remarks>
        public void DebugWrite(params object[] args)
            => Write(args);
        /// <summary>
        /// デバッグ用ログ出力(改行あり)
        /// </summary>
        /// <param name="args">オブジェクト</param>
        /// <remarks>
        /// argsの先頭が文字列かつFormat文字列の場合は、
        /// string.Format()を使用して変換する。
        /// </remarks>
        public void DebugWriteLine(params object[] args)
            => WriteLine(args);

#else
        /// <summary>
        /// デバッグ用フォーマット指定ログ出力
        /// </summary>
        /// <param name="format">書式フォーマット</param>
        /// <param name="args">出力するオブジェクト</param>
        /// <remarks>何もしない</remarks>
        public void DebugWriteFormat(string format, params object[] args) { }

        /// <summary>
        /// デバッグ用フォーマット指定ログ出力(改行あり)
        /// </summary>
        /// <param name="format">書式フォーマット</param>
        /// <param name="args">出力するオブジェクト</param>
        /// <remarks>何もしない</remarks>
        public void DebugWriteFormatLine(string format, params object[] args) { }
        /// <summary>
        /// デバッグ用ログ出力
        /// </summary>
        /// <param name="args">オブジェクト</param>
        /// <remarks>何もしない</remarks>
        public void DebugWrite(params object[] args) { }
        /// <summary>
        /// デバッグ用ログ出力(改行あり)
        /// </summary>
        /// <param name="args">オブジェクト</param>
        /// <remarks>何もしない</remarks>
        public void DebugWriteLine(params object[] args) { }
#endif

    }
}
