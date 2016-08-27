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

namespace BgInfo.Views
{
	/// <summary>
	/// Interaction logic for BgView.xaml
	/// </summary>
	public partial class BgView : Window
	{
		public BgView()
		{
			InitializeComponent();

			Loaded += delegate
			{
				var handle = new WindowInteropHelper(this).Handle;

                SetWindowLong(handle, GWL_EXSTYLE, GetWindowLong(handle, GWL_EXSTYLE) | WS_EX_NOACTIVATE);
				SetWindowPos(handle, new IntPtr(1), 0, 0, 0, 0, SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSIZE);

                var wndSource = HwndSource.FromHwnd(handle);
                wndSource.AddHook(WindowProc);
            };
		}

        private IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) {
            if(msg == WM_WINDOWPOSCHANGING) {
                var windowPos = Marshal.PtrToStructure<WindowPos>(lParam);
                windowPos.hwndInsertAfter = new IntPtr(HWND_BOTTOM);
                handled = true;
            }
            return IntPtr.Zero;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;
		}
	}
}
