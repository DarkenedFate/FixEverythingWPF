using FixEverything.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixEverything.Commands
{
    internal class OtherCommand : ICommand
    {
        public OtherCommand(OtherViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        private OtherViewModel viewModel;

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
                case "btnResetIe":
                    viewModel.ResetIe();
                    break;
                case "btnResetChrome":
                    viewModel.ResetChrome();
                    break;
                case "btnResetFirefox":
                    viewModel.ResetFirefox();
                    break;
                case "btnRefreshReset":
                    viewModel.RefreshReset();
                    break;
                case "btnAutoruns":
                    viewModel.Autoruns();
                    break;
                case "btnAmd":
                    viewModel.AmdCompatChecker();
                    break;
                default:
                    break;
            }
        }
    }
}
