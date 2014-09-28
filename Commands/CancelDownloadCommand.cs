using FixEverything.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixEverything.Commands
{
    class CancelDownloadCommand : ICommand
    {
        public CancelDownloadCommand(DownloadViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        private DownloadViewModel viewModel;

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
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.CancelDownload();
        }
    }
}
