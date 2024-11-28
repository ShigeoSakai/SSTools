using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilterBase.Parts
{
    /// <summary>
    /// TextChangeEventを遅延させたTextBox
    /// </summary>
    public partial class CustomTextBox : TextBox
    {
        /// <summary>
        /// チェック遅延時間
        /// </summary>
        public int CheckTime 
        { 
            get => GetTimerValue();
            set => SetTimerValue(value);
        }
        /// <summary>
        /// タイマー有効・無効
        /// </summary>
        private bool _timerEnable = true;
        
        /// <summary>
        /// 遅延時間の取得
        /// </summary>
        /// <returns></returns>
        private int GetTimerValue()
        {
            if (_timerEnable)
                return InputTimer.Interval;
            else
                return 0;
        }
        /// <summary>
        /// 遅延時間の設定
        /// </summary>
        /// <param name="value"></param>
        private void SetTimerValue(int value)
        {
            if (value <= 0)
            {
                // タイマーが動作中か？
                if (InputTimer.Enabled)
                {   // TickEventを実行してタイマーを止める
                    InputTimer_Tick(this, EventArgs.Empty);
                }
                // タイマー無効
                _timerEnable = false;
            }
            else
            {   // タイマーを有効にして、設定する
                saveText = this.Text;
                _timerEnable = true;
                InputTimer.Interval = value;
            }
        }
        /// <summary>
        /// テキストプロパティ
        /// </summary>
        public override string Text 
        { 
            get => base.Text;
            set
            {
                // 直接設定は、TextChangeが出ないようにする
                saveText = value;
                base.Text = value;
            }
        }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CustomTextBox() :base()
        {
            InitializeComponent();
            saveText = this.Text;
        }

        /// <summary>
        /// チェックするためのTextBoxの内容
        /// </summary>
        private string saveText = null;

        /// <summary>
        /// TextChangeイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            if (_timerEnable == false)
            {   // タイマーが無効なら、TextChangeEvent発行
                base.OnTextChanged(e);
            }
            else
            {   // タイマーが有効なら、監視タイマー再起動
                InputTimer.Stop();
                InputTimer.Start();
            }
        }
        /// <summary>
        /// タイマー処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputTimer_Tick(object sender, EventArgs e)
        {
            // タイマー停止
            InputTimer.Stop();
            if (saveText != this.Text)
            {   // イベント発行
                base.OnTextChanged(e);
            }
            // 現在の内容を保存
            saveText = this.Text;
        }
    }
}
