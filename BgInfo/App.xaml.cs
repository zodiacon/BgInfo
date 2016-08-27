using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Zodiacon.WPF;

namespace BgInfo
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		Mutex _oneInstanceMutex;

        BgInfoManager _mgr;

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			bool createNew;
			_oneInstanceMutex = new Mutex(false, "BgInfo_OneInstanceMutex", out createNew);
			if(!createNew) {
				Shutdown();
				return;
			}

            _mgr = new BgInfoManager();
            _mgr.CreateWindows();
			_mgr.InitTray();
		}

    }
}
