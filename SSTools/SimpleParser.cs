using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SSTools
{
    /// <summary>
    /// 簡易構文解析クラス
    /// </summary>
    /// <remarks>
    /// 継承される事が前提の解析クラス
    /// </remarks>
    public class SimpleParser
    {
        /// <summary>
        /// エラーコード
        /// </summary>
        public enum ERROR_CODE : int
        {
            NONE = 0,                           //!< エラーなし
            UNEXPECTED_PARENTHESES_START = 1,   //!< 意図しない括弧'('開始
            UNEXPECTED_PARENTHESES_END = 2,     //!< 意図しない括弧')'終了
            UNEXPECTED_BRACKETS_START = 3,      //!< 意図しない括弧'['開始
            UNEXPECTED_BRACKETS_END = 4,        //!< 意図しない括弧']'終了
            UNEXPECTED_CURLY_BRACKETS_START = 5,//!< 意図しない括弧'['開始
            UNEXPECTED_CURLY_BRACKETS_END = 6,  //!< 意図しない括弧']'終了
            UNEXPECTED_EQUAL = 7,               //!< 意図しない'='
            UNEXPECTED_COLON = 8,               //!< 意図しない':'
            UNEXPECTED_END_OF_TEXT = 9,         //!< 意図しない文字列の終了
            UNKNOWN_LITERAL = 10,               //!< リテラルが特定できない
            HEX_CONVERT_ERROR = 30,             //!< 16進数から変換エラー
            BIN_CONVERT_ERROR = 31,             //!< 2進数から変換エラー
            QUOTE_END_OF_TEXT = 40,             //!< 文字列引用符が見つからない
            ASSIGN_NAME_AND_LITERAL = 50,       //!< 名前と値の両方がある
        }
        /// <summary>
        /// 解析Exception
        /// </summary>
        [Serializable()]
        public class ParserException : Exception
        {
            /// <summary>
            /// エラーコード
            /// </summary>
            public ERROR_CODE ErrorCode { get; }
            /// <summary>
            /// 解析エラー
            /// </summary>
            /// <param name="error_code">エラーコード</param>
            /// <param name="message">メッセージ</param>
            public ParserException(ERROR_CODE error_code, string message) : base(message) => ErrorCode = error_code;
            /// <summary>
            /// 解析エラー
            /// </summary>
            /// <param name="error_code">エラーコード</param>
            /// <param name="format">フォーマット</param>
            /// <param name="args">フォーマット引数</param>
            public ParserException(ERROR_CODE error_code, string format, params object[] args)
                : base(string.Format(format, args)) => ErrorCode = error_code;
            /// <summary>
            /// 解析エラー
            /// </summary>
            /// <param name="error_code">エラーコード</param>
            /// <param name="innerException">内部で発生した例外</param>
            /// <param name="format">フォーマット</param>
            /// <param name="args">フォーマット引数</param>
            public ParserException(ERROR_CODE error_code, Exception innerException, string format, params object[] args)
                : base(string.Format(format, args), innerException) => ErrorCode = error_code;
            /// <summary>
            /// シリアル化された情報からExceptionを生成
            /// </summary>
            /// <param name="info">シリアル化情報</param>
            /// <param name="context">コンテキスト</param>
            protected ParserException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
                // エラーコードを取得
                ErrorCode = (ERROR_CODE)info.GetInt32("ParserErrorCode");
            }
            /// <summary>
            /// シリアル化情報を生成
            /// </summary>
            /// <param name="info">シリアル化情報</param>
            /// <param name="context">コンテキスト</param>
            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                base.GetObjectData(info, context);
                // エラーコードを追加
                info.AddValue("ParserErrorCode", (int)ErrorCode);
            }
        }


        //
        // リテラルの定義
        //
        // ・空白を除いた先頭から、空白,'(',')','[',']','{','}','カンマ','='までの文字列で...
        //      ・数値列
        //      ・クォーテーションで囲まれた文字列
        //      ・先頭がアルファベットからなる文字列
        //
        //   ・数値列
        //      ・先頭が符号(+/-)、もしくは数字、もしくは'.'
        //      ・"0x" ... 16進数,"0b" ... 2進数
        //      ・指数表記 123.45e-10,123.45E5

        /// <summary>
        /// リテラル区切り文字
        /// </summary>
        private static char[] LiteralSeparator = new char[] {
            ' ','(',')','[',']','{','}',',','=',':'
        };
        /// <summary>
        /// リテラルの種類
        /// </summary>
        private enum LITELAL_TYPE
        {
            NONE,       //!< なし
            STRING,     //!< 文字列(クォートされた文字列)
            NUMBER,     //!< 数値
            NAME,       //!< 名前
            PENDING,    //!< 判定保留
            ERROR       //!< エラー
        }

        /// <summary>
        /// リテラル候補文字列を取得
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="text_pos">文字列の解析位置</param>
        /// <param name="result">リテラル候補の文字列</param>
        /// <returns>リテラルの種類</returns>
        private static LITELAL_TYPE GetCandidateLiteral(string text, ref int text_pos, out string result)
        {
            // 前側の空白を読み飛ばす
            for (; text_pos < text.Length; text_pos++)
            {
                if (text[text_pos] != ' ')
                    break;
            }
            // 最後まで読み込んだ
            if (text_pos >= text.Length)
            {
                result = null;
                return LITELAL_TYPE.NONE;
            }

            if ((text[text_pos] == '\"') ||
                (text[text_pos] == '\''))
            {   // クォートされた文字列
                return QuotedString(text, ref text_pos, out result);
            }

            // 文字列開始位置
            int start_pos = text_pos;
            // セパレータを検索して、一番近いセパレータの位置を求める
            int min_pos = text.Length;
            foreach (char c in LiteralSeparator)
            {
                int sepa_pos = text.IndexOf(c, start_pos);
                if ((sepa_pos >= 0) && (min_pos > sepa_pos))
                    min_pos = sepa_pos;
            }
            // セパレータまで文字を進める
            text_pos = min_pos - 1;
            // リテラル候補文字を返す
            result = text.Substring(start_pos, min_pos - start_pos);
            if (result.Length == 0)
                return LITELAL_TYPE.NONE;
            return LITELAL_TYPE.PENDING;
        }
        /// <summary>
        /// クォート文字列を抜き出す
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="text_pos">文字列の解析位置</param>
        /// <param name="result">抜き出したクォート文字列</param>
        /// <returns>リテラルの種類(STRING)</returns>
        /// <exception cref="ParserException">文字列終端が見つからない</exception>
        private static LITELAL_TYPE QuotedString(string text, ref int text_pos, out string result)
        {
            // 先頭の文字(クォート)を保存し、1文字進める
            char quote = text[text_pos];
            text_pos++;
            // 文字列終了ならエラーを返す
            if (text_pos >= text.Length - 1)
            {
                // 例外を発行
                throw new ParserException(ERROR_CODE.QUOTE_END_OF_TEXT, "Text is Blank");
            }
            // 開始位置を保存
            int start_pos = text_pos;
            while (text_pos < text.Length)
            {
                int pos = text.IndexOf(quote, text_pos);
                if ((pos >= 1) && (text[pos - 1] == '\\'))
                {   // エスケープされたクォート
                    text_pos = pos + 1;
                    continue;
                }
                if (pos >= 0)
                {
                    result = text.Substring(start_pos, pos - start_pos);
                    text_pos = pos + 1;
                    // 空白じゃなくなるまで進める
                    for (; text_pos < text.Length; text_pos++)
                        if (text[text_pos] != ' ')
                        {
                            text_pos--;
                            break;
                        }
                    return LITELAL_TYPE.STRING;
                }
                else
                    break;
            }
            // 終端がない例外を発行
            throw new ParserException(ERROR_CODE.QUOTE_END_OF_TEXT, "Text is '{0}'", text.Substring(start_pos));
        }
        /// <summary>
        /// 数値かどうか判定
        /// </summary>
        /// <param name="str">文字列</param>
        /// <returns>true:数値</returns>
        private static bool isNumber(string str)
        {
            bool integer_part = false;
            bool exp = false;
            bool exp_digit = false;
            bool hex = false;
            bool bin = false;

            // 先頭の1文字
            char c = str[0];
            if ((c == '+') || (c == '-') || (c == '.') || ((c >= '0') && (c <= '9')))
            {
                int index = 0;
                if ((c == '+') || (c == '-'))
                {
                    index++;
                    if (index >= str.Length)
                        return false;
                }
                if (c == '.')
                {
                    integer_part = true;
                    index++;
                    if (index >= str.Length)
                        return false;
                }
                if (c == '0')
                {
                    if (index + 1 < str.Length)
                    {
                        string nxt = str[index + 1].ToString().ToUpper();
                        if ((nxt == "X") || (nxt == "B"))
                        {
                            hex = (nxt == "X");
                            bin = (nxt == "B");
                            index += 2;
                            if (index >= str.Length)
                                return false;
                        }
                    }
                }
                if ((hex == false) && (bin == false))
                {   // 数値の解析
                    for (; index < str.Length; index++)
                    {
                        char cn = str[index];
                        if ((cn >= '0') && (cn <= '9'))
                        {
                            if (exp)
                                exp_digit = true;   // 指数部の数値
                            continue;
                        }
                        if (cn == '.')
                        {
                            if (integer_part)
                                return false;   // 2個目の'.'
                            integer_part = true;
                            continue;
                        }
                        if ((cn == 'e') || (cn == 'E'))
                        {   // Exp
                            if (exp)
                                return false;   // 2個目のE
                            exp = true;
                            continue;
                        }
                        if ((cn == '+') || (cn == '-'))
                        {   // Expの符号
                            if ((exp) && (exp_digit == false))
                                continue;
                            return false;
                        }
                        return false;
                    }
                    return true;
                }
                else if (hex)
                {   // 16進数の解析
                    for (; index < str.Length; index++)
                    {
                        if ((str[index] < '0') || (str[index] > 'f') ||
                            ((str[index] > '9') && (str[index] < 'A')) ||
                            ((str[index] > 'F') && (str[index] < 'a')))
                            return false;
                    }
                    return true;
                }
                else if (bin)
                {   // 2進数の解析
                    for (; index < str.Length; index++)
                    {
                        if ((str[index] != '0') && (str[index] != '1'))
                            return false;
                    }
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 名前として適切か
        /// </summary>
        /// <param name="str">文字列</param>
        /// <returns>true:名前として適切</returns>
        /// <remarks>
        /// 名前として有効なのは、
        ///   [_A-Za-z][_0-9A-Za-z]*　で、
        ///   ダブルクォーテーションやシングルクォーテーションが含まれない
        /// </remarks>
        private static bool isName(string str)
        {
            // クォートが含まれる
            if ((str.IndexOf('\"') >= 0) ||
                (str.IndexOf('\'') >= 0))
                return false;

            // 先頭がアルファベット
            char c = str[0];
            if (((c >= 'A') && (c <= 'Z')) ||
                ((c >= 'a') && (c <= 'z')))
                return true;
            // 先頭が _
            if (c == '_')
                return true;
            return false;
        }

        /// <summary>
        /// リテラルを取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="pos">文字列解析位置</param>
        /// <param name="type">リテラルの種別</param>
        /// <returns>リテラル文字列</returns>
        /// <exception cref="ParserException">リテラルが特定できない</exception>
        private static string GetLiteral(string str, ref int pos, out LITELAL_TYPE type)
        {
            int start_pos = pos;
            type = GetCandidateLiteral(str, ref pos, out string result);
            if (type == LITELAL_TYPE.PENDING)
            {
                if (isNumber(result))
                    type = LITELAL_TYPE.NUMBER;
                if (isName(result))
                    type = LITELAL_TYPE.NAME;
            }
            if ((type != LITELAL_TYPE.NUMBER) && (type != LITELAL_TYPE.STRING) &&
                (type != LITELAL_TYPE.NAME))
            {   // リテラルの種別を特定できない
                throw new ParserException(ERROR_CODE.UNKNOWN_LITERAL, "Text is '{0}'", str.Substring(start_pos, pos - start_pos + 1));
            }
            return result;
        }
        /// <summary>
        /// リテラル文字列をオブジェクトに変換
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="type">リテラルの種別</param>
        /// <returns>変換したオブジェクト</returns>
        /// <exception cref="ParserException">変換エラー</exception>
        private static object LiteralToObject(string text, LITELAL_TYPE type)
        {
            if (type == LITELAL_TYPE.NUMBER)
            {
                if (text.ToUpper().StartsWith("0X"))
                {   // 16進数変換
                    try
                    {
                        return Convert.ToInt32(text.Substring(2), 16);
                    }
                    catch (Exception ex)
                    {   // 変換エラーを発行
                        throw new ParserException(ERROR_CODE.HEX_CONVERT_ERROR, ex, "string is '{0}'", text);
                    }
                }
                else if (text.ToUpper().StartsWith("0B"))
                {   // 2進数変換
                    try
                    {
                        return Convert.ToInt32(text.Substring(2), 2);
                    }
                    catch (Exception ex)
                    {   // 変換エラーを発行
                        throw new ParserException(ERROR_CODE.BIN_CONVERT_ERROR, ex, "string is '{0}'", text);
                    }
                }
                else if ((text.IndexOf('.') >= 0) || (text.IndexOf("e") > 0) ||
                    (text.IndexOf('E') > 0))
                {   // doubleに変換
                    if (double.TryParse(text, out double d_result))
                        return d_result;
                }
                else
                {   // 整数に変換
                    if (int.TryParse(text, out int i_result))
                        return i_result;
                }
            }
            else if ((type == LITELAL_TYPE.STRING) || (type == LITELAL_TYPE.NAME))
                return text;

            return null;
        }
        /// <summary>
        /// 構文解析
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="pos">文字列解析位置</param>
        /// <param name="end_of_analysis">解析終端文字</param>
        /// <returns>解析結果</returns>
        /// <exception cref="ParserException">構文解析エラー</exception>
        /// <remarks>
        ///   再帰呼び出しを行う
        /// </remarks>
        private static object Parser(string text, ref int pos, char? end_of_analysis = null)
        {
            string name = null;
            object literal = null;
            List<object> result = new List<object>();
            try
            {
                for (; pos < text.Length; pos++)
                {
                    if (text[pos] == ' ')
                        continue;
                    switch (text[pos])
                    {
                        case '(':
                            // 括弧開始
                            if (name != null)
                            {   // 関数として処理
                                pos++;
                                literal = new FuncName(name, Parser(text, ref pos, ')'));
                                name = null;
                            }
                            else if (literal != null)
                            {   // リテラルの後の括弧開始は意図しない
                                throw new ParserException(ERROR_CODE.UNEXPECTED_PARENTHESES_START,
                                    "Text[{0}]:'{1}'\r\nLiteral:{2}\r\nName:{3}",
                                    pos, text,
                                    (literal != null) ? literal.ToString() : "null",
                                    (name != null) ? name : "null");

                            }
                            else
                            {   // 括弧として処理
                                pos++;
                                literal = new Parentheses(Parser(text, ref pos, ')'));
                            }
                            break;
                        case ')':
                            // 括弧終了
                            if (end_of_analysis.HasValue)
                            {
                                if (end_of_analysis.Value == ')')
                                {   // 正常に括弧が終了 解析結果を返す
                                    return GetResultAnalysis(result, literal, name);
                                }
                                if (end_of_analysis.Value == ',')
                                {
                                    // 正常に括弧が終了 解析結果を返す
                                    return GetResultAnalysis(result, literal, name);
                                }
                            }

                            // 意図しない括弧の終了
                            throw new ParserException(ERROR_CODE.UNEXPECTED_PARENTHESES_END,
                                "Text[{0}]:'{1}'\r\nLiteral:{2}\r\nName:{3}",
                                pos, text,
                                (literal != null) ? literal.ToString() : "null",
                                (name != null) ? name : "null");

                        case '[':
                            // 中括弧開始
                            if (name != null)
                            {   // 配列要素として処理
                                pos++;
                                literal = new NameAndValue(name, Parser(text, ref pos, ']'), null);
                                name = null;
                            }
                            else if (literal != null)
                            {   // リテラルの後の括弧開始は意図しない
                                throw new ParserException(ERROR_CODE.UNEXPECTED_BRACKETS_START,
                                    "Text[{0}]:'{1}'\r\nLiteral:{2}\r\nName:{3}",
                                    pos, text,
                                    (literal != null) ? literal.ToString() : "null",
                                    (name != null) ? name : "null");
                            }
                            else
                            {   // 中括弧として処理
                                pos++;
                                literal = new Brackets(Parser(text, ref pos, ']'));
                            }
                            break;
                        case ']':
                            // 中括弧終了
                            if (end_of_analysis.HasValue)
                            {
                                if (end_of_analysis.Value == ']')
                                {   // 正常に括弧が終了 解析結果を返す
                                    return GetResultAnalysis(result, literal, name);
                                }
                                if (end_of_analysis.Value == ',')
                                {
                                    // 正常に括弧が終了 解析結果を返す
                                    return GetResultAnalysis(result, literal, name);
                                }
                            }
                            // 意図しない括弧の終了
                            throw new ParserException(ERROR_CODE.UNEXPECTED_BRACKETS_END,
                                "Text[{0}]:'{1}'\r\nLiteral:{2}\r\nName:{3}",
                                pos, text,
                                (literal != null) ? literal.ToString() : "null",
                                (name != null) ? name : "null");

                        case '{':
                            // 大括弧開始
                            if ((name != null) || (literal != null))
                            {   // 意図しない括弧の開始
                                throw new ParserException(ERROR_CODE.UNEXPECTED_CURLY_BRACKETS_START,
                                    "Text[{0}]:'{1}'\r\nLiteral:{2}\r\nName:{3}",
                                    pos, text,
                                    (literal != null) ? literal.ToString() : "null",
                                    (name != null) ? name : "null");
                            }
                            else
                            {   // 大括弧として処理
                                pos++;
                                literal = new CurlyBrackets(Parser(text, ref pos, '}'));
                            }
                            break;
                        case '}':
                            // 大括弧終了
                            if (end_of_analysis.HasValue)
                            {
                                if (end_of_analysis.Value == '}')
                                {   // 正常に括弧が終了 解析結果を返す
                                    return GetResultAnalysis(result, literal, name);
                                }
                                if (end_of_analysis.Value == ',')
                                {
                                    // 正常に括弧が終了 解析結果を返す
                                    return GetResultAnalysis(result, literal, name);
                                }
                            }
                            // 意図しない括弧の終了
                            throw new ParserException(ERROR_CODE.UNEXPECTED_CURLY_BRACKETS_END,
                                "Text[{0}]:'{1}'\r\nLiteral:{2}\r\nName:{3}",
                                pos, text,
                                (literal != null) ? literal.ToString() : "null",
                                (name != null) ? name : "null");

                        case '=':
                            // 名前の代入
                            if (name != null)
                            {
                                pos++;
                                literal = new NameAndValue(name, null, Parser(text, ref pos, ','));
                                if (end_of_analysis.HasValue)
                                    pos--;
                                name = null;
                            }
                            else if ((literal != null) && (literal is NameAndValue nv))
                            {
                                pos++;
                                nv.Value = Parser(text, ref pos, end_of_analysis);
                                if (end_of_analysis.HasValue)
                                    pos--;
                            }
                            else
                            {   // 意図しない"="
                                throw new ParserException(ERROR_CODE.UNEXPECTED_EQUAL,
                                    "Text[{0}]:'{1}'\r\nLiteral:{2}\r\nName:{3}",
                                    pos, text,
                                    (literal != null) ? literal.ToString() : "null",
                                    (name != null) ? name : "null");
                            }
                            break;
                        case ':':
                            // 辞書形式
                            if ((literal != null) || (name != null))
                            {
                                pos++;
                                literal = new DictionaryKeyAndValue((literal != null) ? literal : name,
                                    Parser(text, ref pos, ','));
                                if (end_of_analysis.HasValue)
                                    pos--;
                                name = null;
                            }
                            else
                            {   // 意図しない辞書形式
                                throw new ParserException(ERROR_CODE.UNEXPECTED_COLON,
                                    "Text[{0}]:'{1}'\r\nLiteral:{2}\r\nName:{3}",
                                    pos, text,
                                    (literal != null) ? literal.ToString() : "null",
                                    (name != null) ? name : "null");
                            }
                            break;
                        case ',':
                            // カンマは区切り
                            if ((end_of_analysis.HasValue) && (end_of_analysis.Value == ','))
                            {   // 正常に括弧が終了 解析結果を返す
                                return GetResultAnalysis(result, literal, name);
                            }
                            if (literal != null)
                            {
                                result.Add(literal);
                                literal = null;
                                name = null;
                            }
                            else if (name != null)
                            {
                                result.Add(name);
                                literal = null;
                                name = null;
                            }
                            else
                            {   // いきなりカンマ
                                result.Add(null);
                            }
                            break;
                        default:
                            // リテラルにしてみる
                            string literal_str = GetLiteral(text, ref pos, out LITELAL_TYPE type);
                            if ((type == LITELAL_TYPE.STRING) || (type == LITELAL_TYPE.NUMBER))
                            {   // リテラルに設定
                                literal = LiteralToObject(literal_str, type);
                                continue;
                            }
                            else if (type == LITELAL_TYPE.NAME)
                            {
                                name = literal_str;
                                continue;
                            }
                            // 解析エラー
                            return null;
                    }
                }
                if ((end_of_analysis.HasValue) && (end_of_analysis.Value != ','))
                {   // 意図しない終了
                    throw new ParserException(ERROR_CODE.UNEXPECTED_END_OF_TEXT,
                        "Text[{0}]:'{1}'\r\nLiteral:{2}\r\nName:{3}",
                        pos, text,
                        (literal != null) ? literal.ToString() : "null",
                        (name != null) ? name : "null");
                }
                // 解析結果を返す
                return GetResultAnalysis(result, literal, name);
            }
            catch (ParserException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 解析結果を返す
        /// </summary>
        /// <param name="list">解析結果リスト</param>
        /// <param name="literal">リテラル</param>
        /// <param name="name">名前</param>
        /// <returns>解析結果</returns>
        private static object GetResultAnalysis(List<object> list, object literal, string name)
        {
            // リストに中身があるか
            if ((list == null) || (list.Count == 0))
            {
                if ((name != null) && (literal != null))
                {   // 両方になにか残っている
                    throw new ParserException(ERROR_CODE.ASSIGN_NAME_AND_LITERAL,
                        "Literal:{0}\r\nName:{1}",
                        (literal != null) ? literal.ToString() : "null",
                        (name != null) ? name : "null");

                }

                if (literal != null)
                    return literal;     // リテラルを返す
                else if (name != null)
                    return name;        // 名前を返す
                else
                    return null;        // null
            }
            if (literal != null)
                list.Add(literal);      // リテラルが残っていたらリストに追加
            else if (name != null)
                list.Add(name);         // 名前が残っていたらリストに追加
            // リストを返す
            return list;
        }

        /// <summary>
        /// インタプリタのI/F
        /// </summary>
        public interface IInterpreter
        {
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <param name="level">レベル</param>
            /// <returns>変換した文字列</returns>
            string ToString(int level);
        }
        /// <summary>
        /// 引数の文字列変換
        /// </summary>
        /// <param name="argumnet">引数</param>
        /// <param name="level">レベル</param>
        /// <returns>変換した文字列</returns>
        private static string ArgumentsToString(object argumnet, int level)
        {
            if (argumnet == null)
                return "null";
            else if (argumnet is List<object> list)
            {   // 引数がリスト
                string result = Environment.NewLine;
                for (int index = 0; index < list.Count; index++)
                {
                    result += string.Format("{0}[{1}] = ", new string(' ', level * 4), index);
                    if (list[index] is IInterpreter interpreter)
                        result += interpreter.ToString(level + 1);
                    else
                        result += list[index].ToString() + Environment.NewLine;
                }
                return result;
            }
            else if (argumnet is IInterpreter interp)
            {   // 引数が解析クラスのどれか
                return interp.ToString(level + 1);
            }
            else if (argumnet is string str)
            {   // 引数が文字列
                return str;
            }
            return argumnet.ToString();
        }
        /// <summary>
        /// 引数の文字列変換
        /// </summary>
        /// <param name="引数t"></param>
        /// <returns>変換した文字列</returns>
        private static string ArgumentsToString(object argumnet)
        {
            if (argumnet == null)
                return "null";
            else if (argumnet is List<object> list)
            {
                string result = string.Empty;
                for (int index = 0; index < list.Count; index++)
                {
                    result += ((result.Length > 0) ? "," : "") + list[index].ToString();
                }
                return result;
            }
            return argumnet.ToString();
        }

        /// <summary>
        /// 関数名クラス
        /// </summary>
        protected class FuncName : IInterpreter
        {
            /// <summary>
            /// 関数名
            /// </summary>
            public string Name { get; private set; }
            /// <summary>
            /// 引数
            /// </summary>
            public object Arguments { get; set; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="name">名前</param>
            /// <param name="obj">引数オブジェクト</param>
            public FuncName(string name, object obj)
            {
                Name = name;
                Arguments = obj;
            }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <param name="level">レベル</param>
            /// <returns>変換した文字列</returns>
            public string ToString(int level)
            {
                return string.Format("{0}{1}(\r\n{0}{2}\r\n{0})\r\n", new string(' ', level * 4),
                    Name, ArgumentsToString(Arguments, level + 1));
            }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換した文字列</returns>
            public override string ToString()
            {
                return Name + "(" + ArgumentsToString(Arguments) + ")";
            }
        }
        /// <summary>
        /// 変数名クラス
        /// </summary>
        protected class NameAndValue : IInterpreter
        {
            /// <summary>
            /// 変数名
            /// </summary>
            public string Name { get; private set; }
            /// <summary>
            /// インデックス
            /// </summary>
            public object Indexes { get; private set; } = null;
            /// <summary>
            /// 値
            /// </summary>
            public object Value { get; set; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="name">名前</param>
            /// <param name="indexes">インデックスオブジェクト</param>
            /// <param name="value">値</param>
            public NameAndValue(string name, object indexes, object value)
            {
                Name = name;
                Indexes = indexes;
                Value = value;
            }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <param name="level">レベル</param>
            /// <returns>変換した文字列</returns>
            public string ToString(int level)
            {
                string result = string.Format("{0}{1}", new string(' ', level * 4), Name);
                if (Indexes != null)
                    result += "[" + ArgumentsToString(Indexes, level + 1) + new string(' ', level * 4 + Name.Length) + "]";
                result += "=" + ArgumentsToString(Value, level + 1) + Environment.NewLine;
                return result;
            }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換した文字列</returns>
            public override string ToString()
            {
                string result = Name;
                if (Indexes != null)
                    result += "[" + ArgumentsToString(Indexes) + "]";
                result += "=" + ArgumentsToString(Value);
                return result;
            }
        }
        /// <summary>
        /// 辞書クラス
        /// </summary>
        protected class DictionaryKeyAndValue : IInterpreter
        {
            /// <summary>
            /// キー値
            /// </summary>
            public object Key { get; private set; }
            /// <summary>
            /// 値
            /// </summary>
            public object Value { get; private set; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="key">キー値</param>
            /// <param name="value">値</param>
            public DictionaryKeyAndValue(object key, object value)
            {
                Key = key; Value = value;
            }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <param name="level">レベル</param>
            /// <returns>変換した文字列</returns>
            public string ToString(int level)
            {
                string result = string.Format("{0}Key:{1}", new string(' ', level * 4), ArgumentsToString(Key, level + 1)) + Environment.NewLine;
                result += string.Format("{0}Value:{1}", new string(' ', level * 4), ArgumentsToString(Value, level + 1)) + Environment.NewLine;
                return result;
            }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換した文字列</returns>
            public override string ToString()
            {
                return ArgumentsToString(Key) + ":" + ArgumentsToString(Value);
            }
        }

        /// <summary>
        /// 括弧クラス
        /// </summary>
        protected class ParenthesesOrBrackets : IInterpreter
        {
            /// <summary>
            /// 中身
            /// </summary>
            public object Arguments { get; set; }
            /// <summary>
            /// 開始文字
            /// </summary>
            private char StartChar { get; set; } = '(';
            /// <summary>
            /// 終了文字
            /// </summary>
            private char EndChar { get; set; } = ')';

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="obj">引数オブジェクト</param>
            /// <param name="startChar">開始文字</param>
            /// <param name="endChar">終了文字</param>
            public ParenthesesOrBrackets(object obj, char startChar, char endChar)
            {
                Arguments = obj;
                StartChar = startChar;
                EndChar = endChar;
            }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <param name="level">レベル</param>
            /// <returns>変換した文字列</returns>
            public string ToString(int level)
            {
                return string.Format("{0}{2}\r\n{0}{1}\r\n{0}{3}\r\n",
                    new string(' ', level * 4),
                    ArgumentsToString(Arguments, level + 1),
                    StartChar, EndChar
                    );
            }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換した文字列</returns>
            public override string ToString()
            {
                return StartChar + ArgumentsToString(Arguments) + EndChar;
            }
        }
        /// <summary>
        /// 括弧()クラス
        /// </summary>
        protected class Parentheses : ParenthesesOrBrackets
        {
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="obj">引数オブジェクト</param>
            public Parentheses(object obj) : base(obj, '(', ')') { }
        }
        /// <summary>
        /// 括弧[]クラス
        /// </summary>
        protected class Brackets : ParenthesesOrBrackets
        {
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="obj">引数オブジェクト</param>
            public Brackets(object obj) : base(obj, '[', ']') { }
        }
        /// <summary>
        /// 括弧{}クラス
        /// </summary>
        protected class CurlyBrackets : ParenthesesOrBrackets
        {
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="obj"></param>
            public CurlyBrackets(object obj) : base(obj, '{', '}') { }
        }

        /// <summary>
        /// 解析結果
        /// </summary>
        public object ParserResult { get; private set; } = null;
        /// <summary>
        /// 解析エラーコード
        /// </summary>
        public ERROR_CODE ErrorCode { get; private set; } = ERROR_CODE.NONE;
        /// <summary>
        /// 解析エラーメッセージ
        /// </summary>
        public string ErrorMessage { get; private set; } = string.Empty;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="text">解析する文字列</param>
        public SimpleParser(string text)
        {
            int pos = 0;
            // 改行を削除
            text = text.Replace(Environment.NewLine, "");
            // TAB文字を空白に変換
            text = text.Replace("\t", " ");
            // 前後の空白を削除
            text = text.Trim();

            try
            {   // 構文解析
                ParserResult = Parser(text, ref pos);
            }
            catch (ParserException ex)
            {   // 解析エラー発生
                ErrorCode = ex.ErrorCode;
                ErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    ErrorMessage += Environment.NewLine + ex.InnerException.ToString(); ;
                }

                Console.WriteLine("Exception:{0}:{1}", ErrorCode.ToString(), ErrorMessage);
                if (ex.InnerException != null)
                    Console.WriteLine("InnerException:{0}", ex.InnerException.ToString());
            }
        }
    }
}
