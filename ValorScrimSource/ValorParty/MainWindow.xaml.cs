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
using AutoUpdaterDotNET;

namespace ValorParty
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public Frame loaderFrameRef;
        
        public MainWindow()
        {

            InitializeComponent();
            loaderFrameRef = loaderFrame;
            loaderFrameRef.Content = new Menus.Loading.Loading();
            new RumbleMike.Updates.Updator();
            new RiotUser();
            
            
            //If Lockfile couldnt be found, cause error screen to show
            if (RiotUser.Instance.lockfileData.port == null) { loaderFrameRef.Content = new Menus.ErrorPage.ErrorPage("Game is not running, please relaunch when game is running."); return; }

            //Load Home Screen
            loaderFrameRef.Content = new Menus.Home.Home();
        }
    }
}
