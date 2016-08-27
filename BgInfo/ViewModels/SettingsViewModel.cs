using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using BgInfo.Models;
using Prism.Mvvm;
using Zodiacon.WPF;

namespace BgInfo.ViewModels {
    sealed class SettingsViewModel : DialogViewModelBase {
        readonly Settings _settings;

        public SettingsViewModel(Window dialog, Settings settings) : base(dialog) {
            _settings = settings;

            _selectedFont = new FontFamily(settings.FontFamily);
            _textColor = settings.TextColor.ToWPFColor();
            _backgroundColor = settings.BackgroundColor.ToWPFColor();
        }

        private FontFamily _selectedFont;

        public FontFamily SelectedFont {
            get { return _selectedFont; }
            set { SetProperty(ref _selectedFont, value); }
        }

        private Color _textColor = Colors.White;

        public Color TextColor {
            get { return _textColor; }
            set { SetProperty(ref _textColor, value); }
        }

        private Color _backgroundColor = Colors.Transparent;

        public Color BackgroundColor {
            get { return _backgroundColor; }
            set { SetProperty(ref _backgroundColor, value); }
        }

        public IEnumerable<FontFamily> SystemFonts => Fonts.SystemFontFamilies.OrderBy(font => font.Source);

    }
}
