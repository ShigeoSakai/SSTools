using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace SSTools
{
    public class GrowLabel : Label
    {
        private bool mGrowing;
        public GrowLabel()
        {
            this.AutoSize = false;
        }

        private int _maxTextWidth = 124;
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
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            resizeLabel();
        }
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            resizeLabel();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            resizeLabel();
        }
    }
}
