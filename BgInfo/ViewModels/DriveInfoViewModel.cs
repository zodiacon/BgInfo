using System.IO;
using Prism.Mvvm;

namespace BgInfo.ViewModels {
    class DriveInfoViewModel : BindableBase {
        public DriveInfo DriveInfo { get; }
        public DriveInfoViewModel(DriveInfo driveInfo) {
            DriveInfo = driveInfo;
        }

        public string Name => DriveInfo.Name;
        public string TotalSize
        {
            get
            {
                if (!DriveInfo.IsReady)
                    return "Drive Not Ready";
                return GetSize(DriveInfo.TotalSize);
            }
        }

        public string FreeSpace
        {
            get
            {
                if (!DriveInfo.IsReady)
                    return "Drive Not Ready";
                return GetSize(DriveInfo.TotalFreeSpace);
            }
        }

        private string GetSize(long size) {
            if(size > 1 << 30)
                return $"{size >> 30} GB";
            return $"{size >> 20} MB";
        }
    }
}

