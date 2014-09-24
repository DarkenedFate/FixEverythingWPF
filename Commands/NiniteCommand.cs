using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using FixEverything.ViewModels;

namespace FixEverything.Commands
{
    class NiniteCommand : ICommand
    {
        public NiniteCommand(NiniteViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        private NiniteViewModel viewModel;

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
                case "btnAvast":
                    viewModel.Avast();
                    break;
                case "btnChrome":
                    viewModel.Chrome();
                    break;
                case "btnFirefox":
                    viewModel.Firefox();
                    break;
                case "btnITunes":
                    viewModel.ITunes();
                    break;
                case "btnJava":
                    viewModel.Java();
                    break;
                case "btnLibre":
                    viewModel.LibreOffice();
                    break;
                case "btnOpenOffice":
                    viewModel.OpenOffice();
                    break;
                case "btnVlc":
                    viewModel.Vlc();
                    break;
                case "btnThunderbird":
                    viewModel.Thunderbird();
                    break;
                default:
                    break;
            }
        }
    }
}
