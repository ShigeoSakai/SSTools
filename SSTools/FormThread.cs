using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSTools
{
	/// <summary>
	/// フォーム用スレッドクラス
	/// </summary>
	/// <remarks>
	/// 生成と表示
	///    SSTools.FormThread thread = new SSTools.FormThread(form);
	///       formのスレッド化と表示(formは、Formクラスを継承していれば何でもOK)
	///       showDialog()で表示する。閉じた場合、結果をDialogResultプロパティに設定し、
	///         スレッドを終了させる。
	///         
	/// フォームの表示結果      
	///    DialogResultプロパティに設定される。
	///    閉じていない場合は、None。中断された場合はAbortが格納される。
	///    
	/// フォームのプロパティ取得
	///    object thread.GetProperty(プロパティ名,プロパティの型)
	///        プロパティ名,型が一致した場合、プロパティ値をobject型で返す。
	///        (プロパティの型にキャストが必要)
	///        取得できない場合はnull
	///        
	///    プロパティの型  thread.GetProperty<プロパティの型>(プロパティ名)
	///        プロパティ名,型が一致した場合、プロパティ値を返す。
	///        取得できない場合は、その型のデフォルト値
	///        
	/// フォームのプロパティ設定
	///    bool thread.SetProperty(プロパティ名,プロパティの型,設定する値)
	///        プロパティ名,型が一致して、設定できればtrue
	///        それ以外はfalse
	///    
	///    bool thread.SetProperty<プロパティの型>(プロパティ名,設定する値)
	///        プロパティ名,型が一致して、設定できればtrue
	///        それ以外はfalse
	/// 
	/// フォームのメソッド呼び出し
	///    object thread.CallMethod(メソッド名,引数,...)
	///    　　戻り値は、object型なので、メソッドの型にキャストが必要
	///    　　
	/// 終了
	///     thread.Dispose()
	///         フォームが表示されていたら、閉じてスレッドを終了する。
	///         この場合、DialogResultがAbortになる。
	/// </remarks>
	public class FormThread : IDisposable
	{
		/// <summary>
		/// 動作UIスレッド
		/// </summary>
		private Thread UIThread = null;
		/// <summary>
		/// フォーム
		/// </summary>
		private Form UIForm = null;
		/// <summary>
		/// UIスレッド表示完了イベント
		/// </summary>
		private readonly ManualResetEvent setEvent = new ManualResetEvent(false);

		/// <summary>
		/// ダイアログの表示結果
		/// </summary>
		private DialogResult _dialogResult = DialogResult.None;
		/// <summary>
		/// ダイアログの表示結果(プロパティ)
		/// </summary>
		public DialogResult DialogResult { get { return _dialogResult; } }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="caption">表示する文字列</param>
		/// <param name="total">合計数</param>
		public FormThread(Form form)
		{
            // スレッドを生成
            UIThread = new Thread(() =>
            {
                try
                {
                    // フォームを生成
                    UIForm = form;
                    // イベント追加
                    UIForm.Shown += Form_Shown;
                    UIForm.FormClosed += UIForm_FormClosed;
                    // ダイアログで表示
                    _dialogResult = UIForm.ShowDialog();
                }
                catch (Exception ex) when ((ex is ThreadInterruptedException) || (ex is ThreadAbortException))
                {   // 割り込み、中断の場合はフォームのスレッドを停止
                    // イベント削除
                    UIForm.Shown -= Form_Shown;
                    UIForm.FormClosed -= UIForm_FormClosed;
                    _dialogResult = DialogResult.Abort;
                    UIForm?.Close();
                    UIForm?.Dispose();
                    UIForm = null;
                }
            })
            {
                IsBackground = true                   // 親スレッド(例えばメニュー画面)の終了時に同時に終了する
            };
            UIThread.SetApartmentState(ApartmentState.STA); // 保存ダイアログ等を利用したとき問題が出ないよう、STA属性を指定
															// UIスレッド開始
			UIThread.Start();
			// インスタンス生成待ち
			setEvent.WaitOne();
		}
		/// <summary>
		/// フォームが表示された時の処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form_Shown(object sender, EventArgs e)
		{
			// 設定完了を通知
			setEvent.Set();
		}
		/// <summary>
		/// フォームが閉じた
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UIForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
		{
			if ((UIThread != null) && (UIThread.IsAlive))
			{
				// スレッドを中断して終了
				UIThread.Abort();
			}
		}
		/// <summary>
		/// Dispose処理
		/// </summary>
		public void Dispose()
		{
			if ((UIThread != null) && (UIThread.IsAlive))
				UIThread.Abort();
			UIThread.Join();
			UIThread = null;
		}
		/// <summary>
		/// フォームのプロパティを設定
		/// </summary>
		/// <param name="propertyName">プロパティ名</param>
		/// <param name="type">型</param>
		/// <param name="value">値</param>
		/// <returns>true:設定OK</returns>
		public bool SetProperty(string propertyName, Type type, object value)
		{
			if (UIForm != null)
			{
				PropertyInfo propertyInfo = UIForm.GetType().GetProperty(propertyName, type);
				if (propertyInfo != null)
				{
					SetPropertyExec(propertyInfo, value);
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// プロパティ設定
		/// </summary>
		/// <param name="propertyInfo">プロパティ情報</param>
		/// <param name="value">値</param>
		/// <remarks>
		///    フォームが実行されているスレッドへInvoke()する
		/// </remarks>
		private void SetPropertyExec(PropertyInfo propertyInfo, object value)
		{
			if (UIForm.InvokeRequired)
			{
				UIForm.Invoke((MethodInvoker)delegate { SetPropertyExec(propertyInfo, value); });
				return;
			}
			propertyInfo.SetValue(UIForm, value);
		}
		/// <summary>
		/// プロパティ設定(ジェネリック)
		/// </summary>
		/// <typeparam name="T">プロパティの型</typeparam>
		/// <param name="propertyName">プロパティ名</param>
		/// <param name="value">値</param>
		/// <returns>true:設定OK</returns>
		public bool SetProperty<T>(string propertyName, T value)
		{
			if (UIForm != null)
			{
				PropertyInfo propertyInfo = UIForm.GetType().GetProperty(propertyName, typeof(T));
				if (propertyInfo != null)
				{
					SetPropertyExec<T>(propertyInfo, value);
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// プロパティ設定(ジェネリック)
		/// </summary>
		/// <typeparam name="T">プロパティの型</typeparam>
		/// <param name="propertyInfo">プロパティ情報</param>
		/// <param name="value">値</param>
		/// <remarks>
		///    フォームが実行されているスレッドへInvoke()する
		/// </remarks>
		private void SetPropertyExec<T>(PropertyInfo propertyInfo, T value)
		{
			if (UIForm.InvokeRequired)
			{
				UIForm.Invoke((MethodInvoker)delegate { SetPropertyExec<T>(propertyInfo, value); });
				return;
			}
			propertyInfo.SetValue(UIForm, value);
		}
		/// <summary>
		/// プロパティ取得(ジェネリック)
		/// </summary>
		/// <typeparam name="T">プロパティの型</typeparam>
		/// <param name="propertyName">プロパティ名</param>
		/// <returns>プロパティの値</returns>
		public T GetProperty<T>(string propertyName)
		{
			if (UIForm != null)
			{
				PropertyInfo propertyInfo = UIForm.GetType().GetProperty(propertyName, typeof(T));
				if (propertyInfo != null)
				{
					return GetPropertyExec<T> (propertyInfo);
				}
			}
			return default;
		}
		/// <summary>
		/// プロパティ取得(ジェネリック)
		/// </summary>
		/// <typeparam name="T">プロパティの型</typeparam>
		/// <param name="propertyInfo">プロパティ情報</param>
		/// <returns>プロパティ値</returns>
		/// <remarks>
		///    フォームが実行されているスレッドへInvoke()する
		/// </remarks>
		private T GetPropertyExec<T>(PropertyInfo propertyInfo)
		{
			if (UIForm.InvokeRequired)
			{
				return (T)UIForm.Invoke((Func<T>)(() => { return GetPropertyExec<T>(propertyInfo); }));
			}
			return (T)propertyInfo.GetValue(UIForm);
		}

		/// <summary>
		/// プロパティ取得
		/// </summary>
		/// <param name="propertyName">プロパティ名</param>
		/// <param name="type">型</param>
		/// <returns>プロパティの値</returns>
		public object GetProperty(string propertyName, Type type)
		{
			if (UIForm != null)
			{
				PropertyInfo propertyInfo = UIForm.GetType().GetProperty(propertyName, type);
				if (propertyInfo != null)
				{
					return GetPropertyExec(propertyInfo);
				}
			}
			return null;
		}
		/// <summary>
		/// プロパティ取得
		/// </summary>
		/// <param name="propertyInfo">プロパティ情報</param>
		/// <returns>プロパティの値</returns>
		/// <remarks>
		///    フォームが実行されているスレッドへInvoke()する
		/// </remarks>
		private object GetPropertyExec(PropertyInfo propertyInfo)
		{
			if (UIForm.InvokeRequired)
			{
				return UIForm.Invoke((Func<object>)(() => { return GetPropertyExec(propertyInfo); }));
			}
			return propertyInfo.GetValue(UIForm);
		}

		/// <summary>
		/// フォームのメソッドを呼び出す
		/// </summary>
		/// <param name="methodName">メソッド名</param>
		/// <param name="args">引数</param>
		/// <returns>結果</returns>
		public object CallMethod(string methodName, params object[] args)
		{
			if (UIForm != null)
			{
				List<Type> types = new List<Type>();
				foreach (object o in args)
					types.Add(o.GetType());

				MethodInfo methodInfo = UIForm.GetType().GetMethod(methodName, types.ToArray());
				if (methodInfo != null)
				{
					return CallMethodExec(methodInfo, args);
				}
			}
			return null;
		}
		/// <summary>
		/// フォームのメソッドを呼び出す
		/// </summary>
		/// <param name="method">メソッド情報</param>
		/// <param name="parameters">引数</param>
		/// <returns></returns>
		/// <remarks>
		///    フォームが実行されているスレッドへInvoke()する
		/// </remarks>
		private object CallMethodExec(MethodInfo method, object[] parameters)
		{
			if (UIForm.InvokeRequired)
			{
				return UIForm.Invoke((Func<object>)(() => { return CallMethodExec(method, parameters); }));
			}
			return method.Invoke(UIForm,parameters);
		}
	}
}
