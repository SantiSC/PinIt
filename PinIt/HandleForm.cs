using PinIt.Win;
using System;
using System.Windows.Forms;

namespace PinIt
{
    public partial class HandleForm : Form
    {
        private static int TOP_HOTKEY;
        private static int NORMAL_HOTKEY;
        private static int BOTTOM_HOTKEY;

        // We need a form to override wndproc and get hotkeys
        public HandleForm()
        {
            InitializeComponent();

            TOP_HOTKEY = (int)Keys.F8 ^ this.Handle.ToInt32();
            NORMAL_HOTKEY = (int)Keys.F9 ^ this.Handle.ToInt32();
            BOTTOM_HOTKEY = (int)Keys.F7 ^ this.Handle.ToInt32();
        }

        // Override wndproc to handle the key press
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312) // 0x0312 -> WM_HOTKEY
            {
                // If the key is F8
                if (m.WParam.ToInt32() == TOP_HOTKEY)
                {
                    // Get the current active window
                    IntPtr activeWindow = WinAPI.GetForegroundWindow();
                    WinAPI.SetTop(activeWindow);
                }
                // If the key is F9
                else if (m.WParam.ToInt32() == NORMAL_HOTKEY)
                {
                    // Get the current active window
                    IntPtr activeWindow = WinAPI.GetForegroundWindow();
                    WinAPI.SetNormal(activeWindow);
                }
                else if (m.WParam.ToInt32() == BOTTOM_HOTKEY)
                {
                    // Get the current active window
                    IntPtr activeWindow = WinAPI.GetForegroundWindow();
                    WinAPI.SetBottom(activeWindow);
                }
            }

            base.WndProc(ref m);
        }

        private void HandleForm_Load(object sender, EventArgs e)
        {
            // Hide the window from showing in the taskbar and minimize it.
            this.ShowInTaskbar = false;
            this.Hide();

            // We need to register the hotkeys after hiding the form, cause sometimes, when hiding it, the window handle changes.
            UnRegisterHotKeys();
            RegisterHotKeys();
        }

        private void RegisterHotKeys()
        {
            // Modifier keys codes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8
            // Compute the addition of each combination of the keys you want to be pressed
            // ALT+CTRL = 1 + 2 = 3 , CTRL+SHIFT = 2 + 4 = 6...
            WinAPI.RegisterHotKey(this.Handle, TOP_HOTKEY, 6, (int)Keys.F8);
            WinAPI.RegisterHotKey(this.Handle, NORMAL_HOTKEY, 6, (int)Keys.F9);
            WinAPI.RegisterHotKey(this.Handle, BOTTOM_HOTKEY, 6, (int)Keys.F7);
        }

        private void UnRegisterHotKeys()
        {
            WinAPI.UnregisterHotKey(this.Handle, TOP_HOTKEY);
            WinAPI.UnregisterHotKey(this.Handle, NORMAL_HOTKEY);
            WinAPI.UnregisterHotKey(this.Handle, BOTTOM_HOTKEY);
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            // If the tray icon is clicked with rmb, unregister the hotkeys and exit the application.
            if (e.Button == MouseButtons.Right)
            {
                UnRegisterHotKeys();
                Application.Exit();
            }
        }
    }
}
