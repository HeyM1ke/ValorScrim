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
using RumbleMike;
using System.Diagnostics;

namespace ValorParty.Menus.JoinMatch
{
    /// <summary>
    /// Interaction logic for JoinMatch.xaml
    /// </summary>
    public partial class JoinMatch : Page
    {
        public JoinMatch()
        {
            InitializeComponent();
        }

        private void backBTN_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.loaderFrameRef.Content = new Menus.Home.Home();
        }

        private void joinMatchBTN_Click(object sender, RoutedEventArgs e)
        {
            Endpoints.POSTJoinparty(codeInput.Password);
            MainWindow.loaderFrameRef.Content = new Menus.Home.Home();
        }
    }
}
