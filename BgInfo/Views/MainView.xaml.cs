using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static BgInfo.NativeMethods;

namespace BgInfo.Views {
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window {
        public MainView() {
            InitializeComponent();

            Loaded += delegate {
                var handle = new WindowInteropHelper(this).Handle;
                WindowUtils.SetCommonStyles(handle);
                WindowUtils.ShowAlwaysOnDesktop(handle);

                if (Environment.OSVersion.Version.Major >= 10)
                {
                    WindowUtils.ShowBehindDesktopIcons(handle);
                }

                var wndSource = HwndSource.FromHwnd(handle);
                wndSource.AddHook(WindowProc);
            };
        }
        unsafe IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) {
            switch (msg) {
                case WM_WINDOWPOSCHANGING:
                    var windowPos = Marshal.PtrToStructure<WindowPos>(lParam);
                    windowPos.hwndInsertAfter = new IntPtr(HWND_BOTTOM);
                    windowPos.flags &= ~(uint)SWP_NOZORDER;
                    handled = true;
                    break;

                case WM_DPICHANGED:
                    var handle = new WindowInteropHelper(this).Handle;
                    var rc = (RECT*)lParam.ToPointer();
                    SetWindowPos(handle, IntPtr.Zero, 0, 0, rc->Right, rc->Left, SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOZORDER);
                    break;

            }
            return IntPtr.Zero;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            e.Cancel = true;
        }

    }
}
