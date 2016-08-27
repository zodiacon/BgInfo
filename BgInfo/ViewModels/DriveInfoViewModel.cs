using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace BgInfo.ViewModels {
    class DriveInfoViewModel : BindableBase {
        public DriveInfo DriveInfo { get; }
        public DriveInfoViewModel(DriveInfo driveInfo) {
            DriveInfo = driveInfo;
        }

        public string Name => DriveInfo.Name;
        public string TotalSize => GetSize(DriveInfo.TotalSize);
        public string FreeSpace => GetSize(DriveInfo.TotalFreeSpace);

        private string GetSize(long size) {
            if(size > 1 << 30)
                return $"{size >> 30} GB";
            return $"{size >> 20} MB";
        }
    }
}
