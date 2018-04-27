using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BgInfo {
    [SuppressUnmanagedCodeSecurity]
    static class NativeMethods {
        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_NOACTIVATE = 0x8000000;
        public const int HWND_BOTTOM = 1;
        public const int SWP_NOMOVE = 2;
        public const int SWP_NOSIZE = 1;
        public const int SWP_NOACTIVATE = 0x10;
        public const int SWP_NOZORDER = 4;

        public const int WM_WINDOWPOSCHANGING = 0x46;

        [StructLayout(LayoutKind.Sequential)]
        public struct PerformanceInformation {
            public int cb;
            public uint CommitTotal;
            public uint CommitLimit;
            public uint CommitPeak;
            public uint PhysicalTotal;
            public uint PhysicalAvailable;
            public uint SystemCache;
            public uint KernelTotal;
            public uint KernelPaged;
            public uint KernelNonpaged;
            public uint PageSize;
            public uint HandleCount;
            public uint ProcessCount;
            public uint ThreadCount;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct OSVersionInfoEx {
            public int dwOSVersionInfoSize;
            public uint dwMajorVersion;
            public uint dwMinorVersion;
            public uint dwBuildNumber;
            public uint dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public string szCSDVersion;
            public ushort wServicePackMajor;
            public ushort wServicePackMinor;
            public ushort wSuiteMask;
            public byte wProductType;
            public byte wReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WindowPos {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public uint flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT {
            public int Left, Top, Right, Bottom;

            public int Width => Right - Left;
            public int Height => Bottom - Top;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MonitorInfo {
            public uint cbSize;
            public RECT rcMonitor;
            public RECT rcWork;
            public uint dwFlags;

            public void Init() {
                cbSize = (uint)Marshal.SizeOf(this);
            }
        }

        public delegate bool EnumMonitorProc(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT rcMonitor, IntPtr data);

        [DllImport("user32")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, IntPtr hWndAfter, int x, int y, int dx, int cy, uint flags);

        [DllImport("user32")]
        public static extern bool EnumDisplayMonitors(IntPtr hDC, IntPtr clipRect, EnumMonitorProc proc, IntPtr data);

        [DllImport("user32")]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MonitorInfo info);

        [DllImport("user32")]
        public static extern int SetWindowLong(IntPtr hWnd, int index, int value);

        [DllImport("user32")]
        public static extern int GetWindowLong(IntPtr hWnd, int index);

        [DllImport("psapi")]
        public static extern bool GetPerformanceInfo(out PerformanceInformation pi, int size);

        [DllImport("kernel32")]
        public static extern bool GetVersionEx(ref OSVersionInfoEx versionInfo);
    }
}
