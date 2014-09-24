using FixEverything.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixEverything.Commands
{
    class AntivirusCommand : ICommand
    {
        public AntivirusCommand(AntivirusViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        private AntivirusViewModel viewModel;

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
                case "btnMcAfee":
                    viewModel.McAfeeDownloader();
                    break;
                case "btnRemoveMcAfee":
                    viewModel.RemoveMcAfee();
                    break;
                case "btnTrendMicro":
                    viewModel.TrendMicroDownloader();
                    break;
                case "btnRemoveTrend":
                    viewModel.RemoveTrendMicro();
                    break;
                case "btnRemoveNorton":
                    viewModel.RemoveNorton();
                    break;
                default:
                    break;
            }
        }
    }
}
