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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ValorParty.Menus.ErrorPage
{
    /// <summary>
    /// Interaction logic for ErrorPage.xaml
    /// </summary>
    public partial class ErrorPage : Page
    {
        string message;
        public ErrorPage(string errormessage)
        {
            InitializeComponent();
            errorMessage.Text = errormessage;
        }

        private void quitBTN_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
