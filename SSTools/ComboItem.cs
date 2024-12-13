using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSTools
{
	/// <summary>
	/// コンボボックス登録クラス
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ComboItem<T> 
	{
		/// <summary>
		/// 値
		/// </summary>
		public T Value { get; private set; }
		/// <summary>
		/// コンボボックスに表示する文字列
		/// </summary>
		public string Text { get; private set; }
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="value">値</param>
		/// <param name="text">文字列</param>
		public ComboItem(T value, string text)
		{
			Value = value;
			Text = text;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="value">値</param>
		public ComboItem(T value)
		{
			Value = value;
			Text = value.ToString();
		}
		/// <summary>
		/// 文字列変換
		/// </summary>
		/// <returns>Textの内容</returns>
		public override string ToString()
		{
			return Text;
		}
		/// <summary>
		/// コンボボックス作成
		/// </summary>
		/// <param name="combo">コンボボックス</param>
		/// <param name="items">アイテム</param>
		public static void MakeCombo(ComboBox combo, IEnumerable<ComboItem<T>> items)
		{
			combo.Items.Clear();
			foreach (ComboItem<T> item in items)
				combo.Items.Add(item);
		}
		/// <summary>
		/// コンボボックスの作成
		/// </summary>
		/// <param name="combo">コンボボックス</param>
		/// <param name="array">配列</param>
		public static void MakeCombo(Array array, ComboBox combo)
		{
			combo.Items.Clear();
			foreach(T item in array)
				combo.Items.Add(new ComboItem<T>(item));
		}
        /// <summary>
        /// コンボボックスの作成
        /// </summary>
        /// <param name="combo">コンボボックス</param>
        /// <param name="items">Tの配列</param>
        public static void MakeCombo(ComboBox combo, IEnumerable<T> items)
		{
			combo.Items.Clear();
			foreach (T item in items)
				combo.Items.Add(new ComboItem<T>(item));
		}


		/// <summary>
		/// コンボボックスから値を取得
		/// </summary>
		/// <param name="combo">コンボボックス</param>
		/// <param name="defaultValue">デフォルト値</param>
		/// <returns>選択されている値</returns>
		public static T Get(ComboBox combo, T defaultValue)
		{
			if (combo.SelectedItem is ComboItem<T> item)
				return item.Value;
			return defaultValue;
		}
		/// <summary>
		/// コンボボックスに値を設定
		/// </summary>
		/// <param name="combo">コンボボックス</param>
		/// <param name="value">設定する値</param>
		public static void Set(ComboBox combo, T value)
		{
			for (int i = 0; i < combo.Items.Count; i++)
			{
				if (combo.Items[i] is ComboItem<T> item)
					if (item.Value.Equals(value))
					{
						combo.SelectedIndex = i;
						return;
					}
			}
			combo.SelectedIndex = 0;
		}
	}
}
