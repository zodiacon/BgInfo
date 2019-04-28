using System;
using static BgInfo.NativeMethods;

namespace BgInfo
{
    public static class WindowUtils
    {
        public static void SetCommonStyles(IntPtr hwnd)
        {
            SetWindowLong(hwnd, GWL_EXSTYLE, GetWindowLong(hwnd, GWL_EXSTYLE) | WS_EX_NOACTIVATE);
            SetWindowPos(hwnd, new IntPtr(HWND_BOTTOM), 0, 0, 0, 0, SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSIZE);
        }

        public static void ShowAlwaysOnDesktop(IntPtr hwnd)
        {
            var progmanHandle = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Progman", null);
            var workerWHandle = IntPtr.Zero;
            EnumWindows(new EnumWindowsProc((topHandle, topParamHandle) =>
            {
                IntPtr shellHandle = FindWindowEx(topHandle, IntPtr.Zero, "SHELLDLL_DefView", null);
                if (shellHandle != IntPtr.Zero)
                {
                    workerWHandle = FindWindowEx(IntPtr.Zero, topHandle, "WorkerW", null);
                }
                return true;
            }), IntPtr.Zero);
            workerWHandle = workerWHandle == IntPtr.Zero ? progmanHandle : workerWHandle;
            SetParent(hwnd, workerWHandle);
        }

        /// <summary>
        /// Special hack from https://www.codeproject.com/Articles/856020/Draw-behind-Desktop-Icons-in-Windows
        /// Send 0x052C to Progman. This message directs Progman to spawn a 
        /// WorkerW behind the desktop icons. If it is already there, nothing 
        /// happens.
        /// </summary>
        public static void ShowBehindDesktopIcons(IntPtr hwnd)
        {            
            var progmanHandle = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Progman", null);
            SendMessage(progmanHandle, 0x052C, 0x0000000D, 0);
            SendMessage(progmanHandle, 0x052C, 0x0000000D, 1);
        }
    }
}
