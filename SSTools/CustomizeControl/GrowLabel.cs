using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace SSTools
{
    /// <summary>
    /// 折り返し可能なラベル
    /// </summary>
    public class GrowLabel : Label
    {
        /// <summary>
        /// 計算中
        /// </summary>
        private bool mGrowing;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GrowLabel()
        {
            // 自動サイス調整は、false
            this.AutoSize = false;
        }
        /// <summary>
        /// ラベルの最大横幅
        /// </summary>
        private int _maxTextWidth = 124;
        /// <summary>
        /// ラベルの最大横幅(プロパティ)
        /// </summary>
        public int MaxTextWidth
        {
            get => _maxTextWidth;
            set
            {
                if (_maxTextWidth != value)
                {
                    _maxTextWidth = value;
                    resizeLabel();
                }
            }
        }
        /// <summary>
        /// ラベルのリサイズ
        /// </summary>
        /// <remarks>
        /// リサイズ中に呼び出された場合は何もしない
        /// </remarks>
        private void resizeLabel()
        {
            if (mGrowing)
                return;
            try
            {
                mGrowing = true;
                Size sz = new Size(_maxTextWidth, Int32.MaxValue);
                sz = TextRenderer.MeasureText(this.Text, this.Font, sz, TextFormatFlags.WordBreak);
                this.Width = sz.Width;
                this.Height = sz.Height;
            }
            finally
            {
                mGrowing = false;
            }
        }
        /// <summary>
        /// ラベルの表示内容が変わった
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            resizeLabel();
        }
        /// <summary>
        /// フォントが変更になった
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            resizeLabel();
        }
        /// <summary>
        /// コントロールのサイズが変更になった
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            resizeLabel();
        }
    }
}
