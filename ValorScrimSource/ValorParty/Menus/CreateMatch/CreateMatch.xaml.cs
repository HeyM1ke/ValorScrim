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
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace ValorParty.Menus.CreateMatch
{
    /// <summary>
    /// Interaction logic for CreateMatch.xaml
    /// </summary>
    public partial class CreateMatch : Page
    {
        string selectedMap;
        string selectedServer;

        public CreateMatch()
        {
            InitializeComponent();
            ServerDetection();
        }

        void ServerDetection()
        {
            var currentRegion = RiotUser.Instance.riotDetails.gameShard;
            switch (currentRegion.ToLower())
            {
                case "na":
                    AddNAServers();
                    break;
                case "eu":
                    AddEUServers();
                    break;
                case "latam":
                    AddLATAMServers();
                    break;
                case "ap":
                    AddAPServers();
                    break;
                case "kr":
                    AddKRServers();
                    break;
                case "br":
                    AddBRServers();
                    break;
            }

        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void createMatchBTN_Click(object sender, RoutedEventArgs e)
        {
            string oldPartyID;
            string newPartyID;

            // Get the party ID
            var currentpartyresp = Endpoints.GETCurrentParty();
            oldPartyID = (String)currentpartyresp["CurrentPartyID"];
            RiotUser.Instance.riotUserData.partyID = oldPartyID;
            Debug.WriteLine(RiotUser.Instance.riotUserData.partyID);
            // Create new party
            var createpartyresp = Endpoints.POSTCreateparty();
            // Get the party ID of new party
            newPartyID = (String)createpartyresp["CurrentPartyID"];
            if (newPartyID == null) { return; }
            RiotUser.Instance.riotUserData.partyID = newPartyID;
            // Use new id, and create custom party with specified settings
            Endpoints.POSTMakeCustomGame();
            Endpoints.POSTCustomGameSettings(selectedMap, selectedServer);
            //Party Open
            Endpoints.POSTOpenLobby();
            // Show code to user
            Clipboard.SetText(RiotUser.Instance.riotUserData.partyID);
            MessageBox.Show("Lobby code is now in your clipboard. Have fun!");
            MainWindow.loaderFrameRef.Content = new Menus.Home.Home();
        }

        private void mapComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem typeItem = (ComboBoxItem)mapComboBox.SelectedItem;
            string value = typeItem.Content.ToString();
            switch (value.ToLower())
            {
                case "ascent":
                    mapPreview.Source = new BitmapImage(new Uri(@"pack://application:,,,/images/Ascent_preview.png", UriKind.Absolute));
                    selectedMap = "/Game/Maps/Ascent/Ascent";
                    break;
                case "bind":
                    mapPreview.Source = new BitmapImage(new Uri(@"pack://application:,,,/images/Duality_preview.png", UriKind.Absolute));
                    selectedMap = "/Game/Maps/Duality/Duality";
                    break;
                case "split":
                    mapPreview.Source = new BitmapImage(new Uri(@"pack://application:,,,/images/Bonsai_preview.png", UriKind.Absolute));
                    selectedMap = "/Game/Maps/Bonsai/Bonsai";
                    break;
                case "haven":
                    mapPreview.Source = new BitmapImage(new Uri(@"pack://application:,,,/images/Triad_preview.png", UriKind.Absolute));
                    selectedMap = "/Game/Maps/Triad/Triad";
                    break;
                case "icebox":
                    mapPreview.Source = new BitmapImage(new Uri(@"pack://application:,,,/images/Icebox_preview.png", UriKind.Absolute));
                    selectedMap = "/Game/Maps/Port/Port";
                    break;

                default:
                    mapPreview.Source = new BitmapImage(new Uri(@"pack://application:,,,/images/Ascent_preview.png", UriKind.Absolute));
                    selectedMap = "/Game/Maps/Ascent/Ascent";
                    break;
            }
        }

        private void serverComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string value = (string)serverComboBox.SelectedItem;
            //string value = typeItem.Content.ToString();

            switch (RiotUser.Instance.riotDetails.gameShard.ToLower())
            {
                case "na":
                    NAServerDetermination(value);
                    break;
                case "eu":
                    EUServerDetermination(value);
                    break;
                case "latam":
                    LATAMServerDetermination(value);
                    break;
                case "br":
                    BRServerDetermination(value);
                    break;
                case "ap":
                    APServerDetermination(value);
                    break;
                case "kr":
                    KRServerDetermination(value);
                    break;
                default:
                    NAServerDetermination(value);
                    break;

                    // This fucking works, i dont know why but yea
            }
        }

        #region Server Manager
        void AddEUServers()
        {
            serverComboBox.Items.Add("London");
            serverComboBox.Items.Add("Paris 1");
            serverComboBox.Items.Add("Paris 2");
            serverComboBox.Items.Add("Frankfurt 1");
            serverComboBox.Items.Add("Frankfurt 2");
            serverComboBox.Items.Add("Warsaw");
            serverComboBox.Items.Add("Stockholm 1");
            serverComboBox.Items.Add("Istanbul");
            serverComboBox.Items.Add("Madrid");
            serverComboBox.Items.Add("Bahrain");
            serverComboBox.Items.Add("Tokyo");
        }
        void AddNAServers()
        {
            serverComboBox.Items.Add("Oregon 1");
            serverComboBox.Items.Add("Oregon 2");
            serverComboBox.Items.Add("California 1");
            serverComboBox.Items.Add("California 2");
            serverComboBox.Items.Add("Virginia 1");
            serverComboBox.Items.Add("Virginia 2");
            serverComboBox.Items.Add("Texas");
            serverComboBox.Items.Add("Illinois");
            serverComboBox.Items.Add("Georgia");
        }
        void AddLATAMServers()
        {
            serverComboBox.Items.Add("Chicago");
            serverComboBox.Items.Add("Miami");
            serverComboBox.Items.Add("Mexico City");
            serverComboBox.Items.Add("Santiago");
        }
        void AddBRServers()
        {
            serverComboBox.Items.Add("Sao Paulo");
        }
        void AddAPServers()
        {
            serverComboBox.Items.Add("Tokyo 1");
            serverComboBox.Items.Add("Tokyo 2");
            serverComboBox.Items.Add("Mumbai");
            serverComboBox.Items.Add("Hong Kong 1");
            serverComboBox.Items.Add("Hong Kong 2");
            serverComboBox.Items.Add("Sydney 1");
            serverComboBox.Items.Add("Sydney 2");
            serverComboBox.Items.Add("Singapore 1");
            serverComboBox.Items.Add("Singapore 2");
        }
        void AddKRServers()
        {
            serverComboBox.Items.Add("Seoul 1");
            serverComboBox.Items.Add("Seoul 2");
        }


        void NAServerDetermination(string value)
        {
            switch (value.ToLower())
            {
                case "georgia":
                    selectedServer = "aresriot.aws-rclusterprod-atl1-1.na-gp-atlanta-1";
                    break;
                case "texas":
                    selectedServer = "aresriot.aws-rclusterprod-dfw1-1.na-gp-dallas-1";
                    break;
                case "illinois":
                    selectedServer = "aresriot.mtl-riot-ord2-3.na-gp-chicago-1";
                    break;
                case "oregon 1":
                    selectedServer = "aresriot.aws-rclusterprod-usw2-1.na-gp-oregon-1";
                    break;
                case "california 1":
                    selectedServer = "aresriot.aws-rclusterprod-usw1-1.na-gp-norcal-1";
                    break;
                case "california 2":
                    selectedServer = "aresriot.aws-rclusterprod-usw1-1.na-gp-norcal-awsedge-1";
                    break;
                case "virginia 1":
                    selectedServer = "aresriot.aws-rclusterprod-use1-1.na-gp-ashburn-1";
                    break;
                case "virginia 2":
                    selectedServer = "aresriot.aws-rclusterprod-use1-1.na-gp-ashburn-awsedge-1";
                    break;
            }
        }

        void EUServerDetermination(string value)
        {
            switch (value.ToLower())
            {
                // EU Server Cases

                case "london":
                    selectedServer = "aresriot.aws-rclusterprod-euw2-1.eu-gp-london-awsedge-1";
                    break;
                case "paris 1":
                    selectedServer = "aresriot.aws-rclusterprod-euw3-1.eu-gp-paris-1";
                    break;
                case "paris 2":
                    selectedServer = "aresriot.aws-rclusterprod-euw3-1.eu-gp-paris-awsedge-1";
                    break;
                case "frankfurt 1":
                    selectedServer = "aresriot.aws-rclusterprod-euc1-1.eu-gp-frankfurt-1";
                    break;
                case "frankfurt 2":
                    selectedServer = "aresriot.aws-rclusterprod-euc1-1.eu-gp-frankfurt-awsedge-1";
                    break;
                case "warsaw":
                    selectedServer = "aresriot.aws-rclusterprod-waw1-1.eu-gp-warsaw-1";
                    break;
                case "stockholm 1":
                    selectedServer = "aresriot.aws-rclusterprod-eun1-1.eu-gp-stockholm-1";
                    break;
                case "istanbul":
                    selectedServer = "aresriot.mtl-riot-ist1-2.eu-gp-istanbul-1";
                    break;
                case "madrid":
                    selectedServer = "aresriot.aws-rclusterprod-mad1-1.eu-gp-madrid-1";
                    break;
                case "bagrain":
                    selectedServer = "aresriot.aws-rclusterprod-mes1-1.eu-gp-bahrain-awsedge-1";
                    break;
                case "tokyo":
                    selectedServer = "aresriot.aws-rclusterprod-apne1-1.eu-gp-tokyo-1";
                    break;

                default:
                        selectedServer = "aresriot.aws-rclusterprod-euc1-1.eu-gp-frankfurt-awsedge-1";

                    // This is dumb but it works for now.
                    break;

            }
        }

        void LATAMServerDetermination(string value)
        {
            switch (value.ToLower())
            {
                // LATAM Server Cases

                case "chicago":
                    selectedServer = "resriot.mtl-riot-ord2-3.latam-gp-chicago-1";
                    break;
                case "miami":
                    selectedServer = "aresriot.mia1.latam-gp-miami-1";
                    break;
                case "mexico city":
                    selectedServer = "aresriot.mtl-tmx-mex1-1.latam-gp-mexicocity-1";
                    break;
                case "santiago":
                    selectedServer = "aresriot.mtl-ctl-scl2-2.latam-gp-santiago-1";
                    break;

                default:
                    selectedServer = "aresriot.mtl-ctl-scl2-2.latam-gp-santiago-1";

                    // This is dumb but it works for now.
                    break;

            }
        }

        void BRServerDetermination(string value)
        {
            switch (value.ToLower())
            {
                // BR Server Cases

                case "sao paulo":
                    selectedServer = "aresriot.aws-rclusterprod-sae1-1.br-gp-saopaulo-1";
                    break;
                
                default:
                    selectedServer = "aresriot.aws-rclusterprod-sae1-1.br-gp-saopaulo-1";

                    // This is dumb but it works for now.
                    break;

            }
        }

        void APServerDetermination(string value)
        {
            switch (value.ToLower())
            {
                // AP/OCE Server Cases

                case "hong kong 1":
                    selectedServer = "aresriot.aws-rclusterprod-ape1-1.ap-gp-hongkong-1";
                    break;
                case "singapore 1":
                    selectedServer = "aresriot.aws-rclusterprod-apse1-1.ap-gp-singapore-1";
                    break;
                case "singapore 2":
                    selectedServer = "aresriot.aws-rclusterprod-apse1-1.ap-gp-singapore-awsedge-1";
                    break;
                case "sydney 1":
                    selectedServer = "aresriot.aws-rclusterprod-apse2-1.ap-gp-sydney-1";
                    break;
                case "sydney 2":
                    selectedServer = "aresriot.aws-rclusterprod-apse2-1.ap-gp-sydney-awsedge-1";
                    break;
                case "hong kong 2":
                    selectedServer = "aresriot.aws-rclusterprod-ape1-1.ap-gp-hongkong-awsedge-1";
                    break;
                case "mumbai":
                    selectedServer = "aresriot.aws-rclusterprod-aps1-1.ap-gp-mumbai-awsedge-1";
                    break;
                case "tokyo 1":
                    selectedServer = "aresriot.aws-rclusterprod-apne1-1.ap-gp-tokyo-1";
                    break;
                case "tokyo 2":
                    selectedServer = "aresriot.aws-rclusterprod-apne1-1.ap-gp-tokyo-awsedge-1";
                    break;

                default:
                    selectedServer = "aresriot.aws-rclusterprod-apse2-1.ap-gp-sydney-1";

                    // This is dumb but it works for now.
                    break;

            }
        }

        void KRServerDetermination(string value)
        {
            switch (value.ToLower())
            {
                // EU Server Cases

                case "seoul 1":
                    selectedServer = "aresriot.aws-rclusterprod-apne2-1.kr-gp-seoul-1";
                    break;
                case "seoul 2":
                    selectedServer = "aresriot.aws-rclusterprod-apne2-1.kr-gp-seoul-awsedge-1";
                    break;

                default:
                    selectedServer = "aresriot.aws-rclusterprod-apne2-1.kr-gp-seoul-1";

                    // This is dumb but it works for now.
                    break;

            }
        }


        #endregion

        private void backBTN_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.loaderFrameRef.Content = new Menus.Home.Home();
        }
    }
}
