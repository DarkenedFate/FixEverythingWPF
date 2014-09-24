using FixEverything.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixEverything.Commands
{
    internal class PrintersCommand : ICommand
    {
        public PrintersCommand(PrintersViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        private PrintersViewModel viewModel;

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
                case "btnHp":
                    viewModel.HpPrinterInstaller();
                    break;
                case "btnPrntScanDoc":
                    viewModel.HpPrintScanDoc();
                    break;
                case "btnKodak":
                    viewModel.KodakPrinterInstaller();
                    break;
                default:
                    break;
            }
        }
    }
}
