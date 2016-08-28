using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Prism.Mvvm;

namespace BgInfo.Models {
    public class Settings : BindableBase {
        private string _fontFamily = "Arial";

        public string FontFamily {
            get { return _fontFamily; }
            set { SetProperty(ref _fontFamily, value); }
        }

        private int _fontSize = 14;

        public int FontSize {
            get { return _fontSize; }
            set { SetProperty(ref _fontSize, value); }
        }

        private Brush _textColor = Brushes.White;

        public Brush TextColor {
            get { return _textColor; }
            set { SetProperty(ref _textColor, value); }
        }

        public int IntervalSeconds { get; set; } = 60;
    }
}
