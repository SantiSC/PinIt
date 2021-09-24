using System;
using System.Runtime.InteropServices;

namespace PinIt.Win
{
    class WinAPI
    {
        // Window state pointers
        private static readonly IntPtr HWND_TOPMOST_STATE = new IntPtr(-1);
        private static readonly IntPtr HWND_NORMAL_STATE = new IntPtr(-2);
        private static readonly IntPtr HWND_BOTTOMMOST_STATE = new IntPtr(1);

        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOACTIVATE = 0x0010;
        private const uint UFLAGS = SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE;

        // Window Position Management
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        // Get current active windows
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        // Register hotkeys
        // DLL libraries used to manage hotkeys
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public static void SetTop(IntPtr hWnd)
        {
            SetWindowPos(hWnd, HWND_TOPMOST_STATE, 0, 0, 0, 0, UFLAGS);
        }

        public static void SetNormal(IntPtr hWnd)
        {
            SetWindowPos(hWnd, HWND_NORMAL_STATE, 0, 0, 0, 0, UFLAGS);
        }

        public static void SetBottom(IntPtr hWnd)
        {
            SetWindowPos(hWnd, HWND_BOTTOMMOST_STATE, 0, 0, 0, 0, UFLAGS);
        }
    }
}
