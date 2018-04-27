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
using BgInfo.Models;
using System.Net.NetworkInformation;

namespace BgInfo.ViewModels {
    class BgViewModel : BindableBase {
        MonitorInfo _monitor;
        PerformanceInformation _perf;
        public Settings Settings { get; }

        public BgViewModel(MonitorInfo monitor, Settings settings) {
            _monitor = monitor;
            Settings = settings;

            Refresh(false);
        }

        public IEnumerable<DriveInfoViewModel> Drives => DriveInfo.GetDrives().Select(drive => new DriveInfoViewModel(drive));
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
                RaisePropertyChanged(nameof(Resolution));
                RaisePropertyChanged(nameof(Processes));
                RaisePropertyChanged(nameof(AvailableMemory));
                RaisePropertyChanged(nameof(Threads));
                RaisePropertyChanged(nameof(Handles));
                RaisePropertyChanged(nameof(Drives));
                RaisePropertyChanged(nameof(UpdateTime));
                RaisePropertyChanged(nameof(Commit));
            }
        }

        public IEnumerable<string> Network {
            get {
                var macs = new List<string>(4);
                foreach(var nic in NetworkInterface.GetAllNetworkInterfaces()) {
                    var address = nic.GetPhysicalAddress().ToString();
                    if(!string.IsNullOrEmpty(address) && address.Length == 12)
                        macs.Add($"{nic.Description}\n\t {ToMacAddress(address)} {nic.Speed / 1000000} Mb/s");
                }
                return macs.Distinct();
            }
        }

        private string ToMacAddress(string address) {
            var mac = new StringBuilder(32);
            for(int i = 0; i < address.Length; i += 2) {
                mac.Append(address.Substring(i, 2));
                if(i < address.Length - 2)
                    mac.Append("-");
            }
            return mac.ToString();
        }
    }
}
