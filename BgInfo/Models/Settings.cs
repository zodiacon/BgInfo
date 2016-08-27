using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BgInfo.Models {
    public class Settings {
        public string FontFamily { get; set; } = "Arial";
        public int FontSize { get; set; } = 14;
        public Color BackgroundColor { get; set; } = Color.Transparent;
        public Color TextColor { get; set; } = Color.White;
    }
}
