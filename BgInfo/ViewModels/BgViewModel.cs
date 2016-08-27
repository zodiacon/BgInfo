using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BgInfo.NativeMethods;
using System.Windows;
using System.Runtime.InteropServices;
using System.IO;
using System.Management;

namespace BgInfo.ViewModels {
    class BgViewModel : BindableBase {
        MonitorInfo _monitor;
        PerformanceInformation _perf;
        public IEnumerable<DriveInfoViewModel> _drives;

        public BgViewModel(MonitorInfo monitor) {
            _monitor = monitor;
            _drives = DriveInfo.GetDrives().Select(drive => new DriveInfoViewModel(drive));

            Refresh(false);
            //var version = new OSVersionInfoEx();
            //version.dwOSVersionInfoSize = Marshal.SizeOf<OSVersionInfoEx>();
            //GetVersionEx(ref version);
        }

        public IEnumerable<DriveInfoViewModel> Drives => _drives;
        public DateTime BootTime => DateTime.Now - TimeSpan.FromMilliseconds(Environment.TickCount);
        public string OSVersion => Environment.OSVersion.ToString();
        public string ComputerName => Environment.MachineName;

        public string DomainName => Environment.UserDomainName;
        public string Resolution => $"{_monitor.rcMonitor.Width} X {_monitor.rcMonitor.Height}";

        public string Memory => $"{_perf.PhysicalTotal >> 8} MB";
        public string AvailableMemory => $"{_perf.PhysicalAvailable >> 8} MB";
        public uint Processes => _perf.ProcessCount;
        public uint Threads => _perf.ThreadCount;
        public uint Handles => _perf.HandleCount;

        public string Commit => $"{_perf.CommitTotal >> 8} MB / {_perf.CommitLimit >> 8} MB";

        public int ProcessorCount => Environment.ProcessorCount;

        public string Processor => GetProcessorName();

        private string GetProcessorName() {
            var mgt = new ManagementClass("Win32_Processor");
            var processors = mgt.GetInstances();
            if(processors.Count == 0)
                return "Unknown";
            return processors.Cast<ManagementObject>().First().Properties["Name"].Value.ToString();
        }

        public DateTime UpdateTime => DateTime.Now;

        public void Refresh(bool raiseChanges = true) {
            GetPerformanceInfo(out _perf, Marshal.SizeOf<PerformanceInformation>());
            if(raiseChanges) {
                OnPropertyChanged(nameof(Resolution));
                OnPropertyChanged(nameof(Processes));
                OnPropertyChanged(nameof(AvailableMemory));
                OnPropertyChanged(nameof(Threads));
                OnPropertyChanged(nameof(Handles));
                OnPropertyChanged(nameof(Drives));
            }
        }
    }
}
