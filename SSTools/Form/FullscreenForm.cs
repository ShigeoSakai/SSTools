using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace SSTools
{
    /// <summary>
    /// ウィンドウ表示状態
    /// </summary>
    public enum FullscreenFormWindowShowState
    {
        /// <summary>
        /// 通常(0)
        /// </summary>
        Normal = FormWindowState.Normal,
        /// <summary>
        /// 最小化(1)
        /// </summary>
        Minimized = FormWindowState.Minimized,
        /// <summary>
        /// 最大化(2) 
        /// </summary>
        Maximized = FormWindowState.Maximized,
        /// <summary>
        /// フルスクリーン
        /// </summary>
        FullScreen = -1,
    }


    /// <summary>
    /// フルスクリーン表示対応フォーム
    /// </summary>
    /// <remarks>
    /// プロパティ:WindowShowStateを使って、ウィンドウ表示状態を更新する
    /// </remarks>
    public partial class FullscreenForm: Form
    {
		/// <summary>
		/// バージョン情報
		/// </summary>
		public static string Version = "0.0.0.0";
		/// <summary>
		/// Copyright情報
		/// </summary>
		public static string Copyright = "";

		/// <summary>
		/// ウィンドウ表示状態更新イベントハンドラ
		/// </summary>
		/// <param name="sender">送信元</param>
		/// <param name="newState">更新後のウィンドウ表示状態</param>
		public delegate void WindowShowStateChangeEventHandler(object sender, FullscreenFormWindowShowState newState);
        /// <summary>
        /// ウィンドウ表示状態更新イベント
        /// </summary>
        public event WindowShowStateChangeEventHandler WindowShowStateChange;
        /// <summary>
        /// ウィンドウ表示状態更新イベント発行
        /// </summary>
        /// <param name="newState">更新後のウィンドウ表示状態</param>
        protected virtual void OnWindowShowStateChangeEvent(FullscreenFormWindowShowState newState)
        {
            WindowShowStateChange?.Invoke(this, newState);
        }


        /// <summary>
        /// 設定中かどうか
        /// </summary>
        private volatile bool _isSetting = false;
        /// <summary>
        /// ウィンドウ表示状態
        /// </summary>
        private FullscreenFormWindowShowState _windowShowState = FullscreenFormWindowShowState.Normal;
        /// <summary>
        /// ウィンドウ表示状態プロパティ
        /// </summary>
        [DefaultValue(FullscreenFormWindowShowState.Normal),Category("配置")]
        public FullscreenFormWindowShowState WindowShowState
        {
            get
            {
                return _windowShowState;
            }
            set
            {
                if (_windowShowState != value)
                {
                    // 設定中
                    _isSetting = true;

                    SuspendLayout();

                    if (value != FullscreenFormWindowShowState.FullScreen)
                    {
                        // メニューバーとステータスバーを表示する
                        MenuAndStatusBarDisplay(MemuBarAlwaysShow, StatusBarAlwaysShow, true);
                        base.ClientSize = _clientSize;
                        // 0. 最大化表示に戻す場合にはいったん通常表示を行う
                        // （フルスクリーン表示の処理とのバランスと取るため）
                        if (_windowShowState == FullscreenFormWindowShowState.FullScreen)
                            base.WindowState = FormWindowState.Normal;
                        // 1. フォームの境界線スタイルを元に戻す
                        base.FormBorderStyle = _borderStyle;
                        // 2. フォームのウィンドウ状態を設定する
                        base.WindowState = (FormWindowState)value;
                        this._windowState = (FormWindowState)value;
                    }
                    else
                    {
                        // メニューバーとステータスバーを消す
                        MenuAndStatusBarDisplay(MemuBarAlwaysShow, StatusBarAlwaysShow, false);
                        // 0. 「最大化表示」→「フルスクリーン表示」では
                        // タスク・バーが消えないので、いったん「通常表示」を行う
                        if (_windowShowState == FullscreenFormWindowShowState.Maximized)
                            base.WindowState = FormWindowState.Normal;
                        // 1. フォームの境界線スタイルを設定する
                        base.FormBorderStyle = FormBorderStyle.None;
                        // 2. フォームのウィンドウ状態を設定する
                        base.WindowState = FormWindowState.Maximized;
                    }
                    // 内部状態を更新
                    _windowShowState = value;
                    // メニューの有効・無効
                    EnableDisableToolStripMenu(_windowShowState);
                    // 更新イベント発行
                    OnWindowShowStateChangeEvent(_windowShowState);


					base.OnClientSizeChanged(new EventArgs());
					
                    // 設定中解除
					_isSetting = false;

                    ResumeLayout();
                }
            }
        }
        /// <summary>
        /// ウィンドウ表示
        /// </summary>
        private FormWindowState _windowState = FormWindowState.Normal;
        /// <summary>
        /// ウィンドウ表示プロパティ
        /// </summary>
        /// <remarks>
        /// 既存プロパティを不可視にする
        /// </remarks>
        [Browsable(false)]
        public new FormWindowState WindowState
        {
            get
            {
                return _windowState;
            }
            set
            {
                if (_windowState != value)
                {   // ウィンドウ表示状態を更新する
                    //   FullScreenは入ってこないので、そのまま設定
                    WindowShowState = (FullscreenFormWindowShowState)value;
                    _windowState = value;
                }
            }
        }
        /// <summary>
        /// ボーダースタイル
        /// </summary>
        private FormBorderStyle _borderStyle = FormBorderStyle.Sizable;
        /// <summary>
        /// ボーダースタイルプロパティ
        /// </summary>
        /// <remarks>
        /// 既存プロパティを不可視にする
        /// </remarks>
        public new FormBorderStyle FormBorderStyle
        {
            get
            {
                return _borderStyle;
            }
            set
            {
                if (_windowShowState == FullscreenFormWindowShowState.Normal)
                {   // 通常表示の場合のみ、スタイルを更新
                    _borderStyle = value;
                    base.FormBorderStyle = value;
                }
            }
        }
        /// <summary>
        /// クライアントサイズ
        /// </summary>
        private Size _clientSize;
        /// <summary>
        /// クライアントサイズプロパティ
        /// </summary>
        /// <remarks>
        /// 既存プロパティを不可視にする
        /// </remarks>
        public new Size ClientSize
        {
            get
            {
                return _clientSize;
            }
            set
            {
                //if (_windowShowState == FFormWindowShowState.Normal)
                {   // 通常表示の場合のみ、サイズを更新
                    _clientSize = value;
                    base.ClientSize = value;
                }
            }
        }

        /// <summary>
        /// メニューバー常時表示(default:false)
        /// </summary>
        private bool _memuBarAlwaysShow = false;
        /// <summary>
        /// メニューバー常時表示プロパティ
        /// </summary>
        [DefaultValue(false), Category("ウィンドウスタイル")]
        public bool MemuBarAlwaysShow 
        { 
            get { return _memuBarAlwaysShow; }
            set 
            { 
                if (_memuBarAlwaysShow != value)
                {
                    _memuBarAlwaysShow = value;
                    MenuAndStatusBarDisplay(_memuBarAlwaysShow, _statusBarAlwaysShow, _windowShowState != FullscreenFormWindowShowState.FullScreen);
                }
            } 
        }
        /// <summary>
        /// ステータスバー常時表示(default:false)
        /// </summary>
        private bool _statusBarAlwaysShow = false;
        /// <summary>
        /// ステータスバー常時表示プロパティ
        /// </summary>
        [DefaultValue(false), Category("ウィンドウスタイル")]
        public bool StatusBarAlwaysShow 
        {
            get { return _statusBarAlwaysShow; }
            set 
            { 
                if (_statusBarAlwaysShow != value)
                {
                    _statusBarAlwaysShow = value;
                    MenuAndStatusBarDisplay(_memuBarAlwaysShow, _statusBarAlwaysShow, _windowShowState != FullscreenFormWindowShowState.FullScreen);
                }
            } 
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FullscreenForm()
        {
            InitializeComponent();

            // メニューの有効・無効
            EnableDisableToolStripMenu(_windowShowState);
        }

        /// <summary>
        /// フォームのサイズ変更
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClientSizeChanged(EventArgs e)
        {
            // 設定中か？
            if (_isSetting == false)
            {
                SuspendLayout();
                // 表示状態が変わった
                if (_windowState != base.WindowState)
                {
                    if (_windowShowState != FullscreenFormWindowShowState.FullScreen)
                    {   // フルスクリーン表示でない場合、変更になった値を設定する
                        WindowShowState = (FullscreenFormWindowShowState)base.WindowState;
                    }
                    // 内部を更新
                    _windowState = base.WindowState;
                }
                else if (_windowShowState == FullscreenFormWindowShowState.Normal)
                {   // ノーマル状態のままの場合は、クライアントサイズを変更する
                    _clientSize = base.ClientSize;
                    base.OnClientSizeChanged(e);
                }
                ResumeLayout();
            }
        }

        /// <summary>
        /// メニューバー/ステータスバーの表示・非表示
        /// </summary>
        /// <param name="menuAlwaysShow">メニューバー常時表示</param>
        /// <param name="statusAlwaysShow">ステータスバー表示・非表示</param>
        /// <param name="show">true:表示/false:非表示</param>
        /// <returns>true:メニューバーステータスバーがフォームにある</returns>
        /// <remarks>
        ///                 AlwaysShow
        ///             | true  |  false
        ///  -----------+-------+---------
        ///  show true  |  〇   |   〇
        ///       ------+-------+---------
        ///       false |  〇   |   ×
        ///  -----------+-------+---------
        /// </remarks>
        private bool MenuAndStatusBarDisplay(bool menuAlwaysShow,bool statusAlwaysShow,bool show)
        {
            bool result = false;
            foreach(Control control in this.Controls)
            {
                if (control is MenuStrip menuStrip)
                {
                    //          menuAlwaysShow
                    //            〇      ×
                    //  show 〇   〇      〇
                    //       ×   〇      ×
                    //
                    menuStrip.Visible = show | menuAlwaysShow;
                    result |= true;
                }
                if (control is StatusStrip statusBar)
                {
                    statusBar.Visible = show | statusAlwaysShow;
                    result |= true;
                }
            }
            return result;
        }
        /// <summary>
        /// メニュー（元に戻す）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnToolStripMenuItemNormal(object sender, EventArgs e)
        {
            WindowShowState = FullscreenFormWindowShowState.Normal;
        }
        /// <summary>
        /// メニュー（最小化）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnToolStripMenuItemMinimized(object sender, EventArgs e)
        {
            WindowShowState = FullscreenFormWindowShowState.Minimized;
        }
        /// <summary>
        /// メニュー（最大化）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected virtual void OnToolStripMenuItemMaximized(object sender, EventArgs e)
        {
            WindowShowState = FullscreenFormWindowShowState.Maximized;
        }
        /// <summary>
        /// メニュー（全画面）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnToolStripMenuItemFullScreen(object sender, EventArgs e)
        {
            WindowShowState = FullscreenFormWindowShowState.FullScreen;
        }
        /// <summary>
        /// ToolStripMenuの有効・無効
        /// </summary>
        /// <param name="state">ウィンドウ表示状態</param>
        private void EnableDisableToolStripMenu(FullscreenFormWindowShowState state)
        {
            switch (state)
            {
                case FullscreenFormWindowShowState.Normal:
                    ToolStripMenuItemNormal.Enabled = false;
                    ToolStripMenuItemMinimized.Enabled = true;
                    ToolStripMenuItemMaximized.Enabled = true;
                    ToolStripMenuItemFullScreen.Enabled = true;
                    break;
                case FullscreenFormWindowShowState.Minimized:
                    ToolStripMenuItemNormal.Enabled = true;
                    ToolStripMenuItemMinimized.Enabled = false;
                    ToolStripMenuItemMaximized.Enabled = true;
                    ToolStripMenuItemFullScreen.Enabled = true;
                    break;
                case FullscreenFormWindowShowState.Maximized:
                    ToolStripMenuItemNormal.Enabled = true;
                    ToolStripMenuItemMinimized.Enabled = true;
                    ToolStripMenuItemMaximized.Enabled = false;
                    ToolStripMenuItemFullScreen.Enabled = true;
                    break;
                case FullscreenFormWindowShowState.FullScreen:
                    ToolStripMenuItemNormal.Enabled = true;
                    ToolStripMenuItemMinimized.Enabled = true;
                    ToolStripMenuItemMaximized.Enabled = true;
                    ToolStripMenuItemFullScreen.Enabled = false;
                    break;
            }
        }
    }
}
