using FixEverything.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixEverything.Commands
{
    class FixitsCommand : ICommand
    {
        public FixitsCommand(FixitsViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        private FixitsViewModel viewModel;

        public event System.EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return viewModel.CanExecute;
        }

        public void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "btnWinUpdate":
                    viewModel.WinUpdateFix();
                    break;
                case "btnScans":
                    viewModel.ScansPopup();
                    break;
                case "btnDvd":
                    viewModel.DvdFix();
                    break;
                case "btnClearPrintQueue":
                    viewModel.ClearPrintQueue();
                    break;
                case "btnSoundFix":
                    viewModel.SoundFix();
                    break;
                case "btnAdminUpdateFix":
                    viewModel.AdminFix();
                    break;
                case "btnApps":
                    viewModel.AppsFix();
                    break;
                case "button2":
                    viewModel.RemoveSentinelDrivers();
                    break;
                case "btnPcSettings":
                    viewModel.FixPcSettings();
                    break;
                default:
                    break;
            }
        }
    }
}
