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
            _textColor = ((SolidColorBrush)settings.TextColor).Color;
            _selectedFontSize = settings.FontSize;
            _selectedInterval = TimeSpan.FromSeconds(settings.IntervalSeconds);
        }

        private FontFamily _selectedFont;

        public FontFamily SelectedFont {
            get { return _selectedFont; }
            set { SetProperty(ref _selectedFont, value); }
        }

        private Color _textColor;

        public Color TextColor {
            get { return _textColor; }
            set { SetProperty(ref _textColor, value); }
        }

        public IEnumerable<FontFamily> SystemFonts => Fonts.SystemFontFamilies.OrderBy(font => font.Source);

        public IEnumerable<int> FontSizes => new[] { 8, 10, 12, 14, 16, 18, 20, 24, 28, 32, 36, 40 };

        private int _selectedFontSize;

        public int SelectedFontSize {
            get { return _selectedFontSize; }
            set { SetProperty(ref _selectedFontSize, value); }
        }

        public IEnumerable<TimeSpan> RefreshIntervals => new[] { 10, 20, 30, 60, 120, 300, 600, 1800, 3600, 7200, 14400, 24 * 3600 }.Select(i => TimeSpan.FromSeconds(i));

        private TimeSpan _selectedInterval;

        public TimeSpan SelectedInterval {
            get { return _selectedInterval; }
            set { SetProperty(ref _selectedInterval, value); }
        }

    }
}
