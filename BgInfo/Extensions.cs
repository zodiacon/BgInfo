using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BgInfo {
    static class Extensions {
        public static Color ToWPFColor(this System.Drawing.Color color) {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}
