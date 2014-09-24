using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FixEverything
{
    /// <summary>
    /// Interaction logic for PasswordForm.xaml
    /// </summary>
    public partial class PasswordForm : Window
    {
        public PasswordForm()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password.Equals("ccu123"))
            {
                RefreshResetForm rrForm = new RefreshResetForm();
                rrForm.Owner = this;
                rrForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong password!", "Wrong password", MessageBoxButton.OK);
                passwordBox.Password = "";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            passwordBox.Focus();
        }
    }
}
