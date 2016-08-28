using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Zodiacon.WPF;
using System.ComponentModel.Composition;
using BgInfo.Views;

namespace BgInfo.ViewModels {
    [Export]
    sealed class TaskbarIconViewModel : BindableBase {
        readonly BgInfoManager _mgr;
        public TaskbarIconViewModel(BgInfoManager mgr) {
            _mgr = mgr;

            ExitCommand = new DelegateCommand(() => {
                Application.Current.Shutdown();
            });

            SettingsCommand = new DelegateCommand(() => {
                _mgr.EnableTray(false);
                var vm = DialogService.CreateDialog<SettingsViewModel, SettingsView>(_mgr.Settings);
                if(vm.ShowDialog() == true) {
                    // apply changes
                    mgr.ApplySettings(vm);
                }

                _mgr.EnableTray(true);
            });
        }

        [Import]
#pragma warning disable 649     // uninitialized variable (satisfied by MEF)
        IDialogService DialogService;

        public ICommand ExitCommand { get; }
        public ICommand SettingsCommand { get; }

    }
}
