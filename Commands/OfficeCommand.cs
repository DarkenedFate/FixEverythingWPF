using FixEverything.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixEverything.Commands
{
    internal class OfficeCommand : ICommand
    {
        public OfficeCommand(OfficeViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        private OfficeViewModel viewModel;

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
                case "btnOffice2013Direct":
                    viewModel.Office2013Direct();
                    break;
                case "btnOffice2010Direct":
                    viewModel.Office2010Direct();
                    break;
                case "btnOffice2013Dl":
                    viewModel.Office2013Website();
                    break;
                case "btnOffice2010Dl":
                    viewModel.Office2010Website();
                    break;
                case "btnOffice2013":
                    viewModel.RemoveOffice2013();
                    break;
                case "btnOffice2010":
                    viewModel.RemoveOffice2010();
                    break;
                default:
                    break;
            }
        }
    }
}
