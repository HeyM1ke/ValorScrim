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

namespace ValorParty.Menus.Home
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }


        private void videoBackground_Loaded(object sender, RoutedEventArgs e)
        {
            videoBackground.Play();
        }

        private void videoBackground_MediaEnded(object sender, RoutedEventArgs e)
        {
            videoBackground.Position = TimeSpan.FromSeconds(0);
        }

        private void CreateNatchHomeBTN_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.loaderFrameRef.Content = new CreateMatch.CreateMatch();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            videoContainer.Source = new Uri("https://502.wtf/ValorScrimMike/resources/homeVideo.mp4", UriKind.Absolute);
            logoPic.Source = new BitmapImage(new Uri(@"pack://application:,,,/images/logo.png", UriKind.Absolute));

            if (RiotUser.Instance.riotDetails.gameShard != string.Empty)
                
                regionLabel.Content = $"REGION: {RiotUser.Instance.riotDetails.gameShard.ToUpper()}";
            else 
                regionLabel.Content = $"NOT FOUND";

            string playercardLink = Endpoints.GetPlayerCard();
            playerCardPreview.Source = new BitmapImage(new Uri(playercardLink, UriKind.Absolute));
            
        }

        private void joinBTN_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.loaderFrameRef.Content = new JoinMatch.JoinMatch();
        }
    }
}
