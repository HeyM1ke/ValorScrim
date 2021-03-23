using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WebSocketSharp;
using Newtonsoft.Json.Linq;
using System.Security.Authentication;
using System.Diagnostics;

namespace RumbleMike
{
    /// <summary>
    /// Connect to the Local Websocket, this class can be expanded very easily aswell.
    /// </summary>
    /// 
    public struct RiotDetails
    {
        public string userPID;
        public string accessToken;
        public string entitlementToken;
        public string gameShard;
    }

    public struct LockfileData
    {
        public string processName;
        public string processId;
        public string port;
        public string password;
        public string protocol;
    }

    public struct RiotUserData
    {
        public string glzURL;
        public string pdURL;
        public string partyID;
    }

    public class RiotUser
    {
        #region Variables
        public static RiotUser Instance;

        WebSocket socket;

        public RiotDetails riotDetails;

        public LockfileData lockfileData;

        public RiotUserData riotUserData;
        #endregion

        public RiotUser()
        {
            if (Instance != null) { return; }

            Instance = this;

            if (ParseLockFile() == false) { Debug.WriteLine("Not Found"); return; }
            // Connect to local sock
            ConnectToWebsocket();
            GetTokens();

            var valorantData = new
            {
                product = "valorant"
            };

            riotDetails.gameShard = (string)Endpoints.POSTWebsocketAPI("/player-affinity/product/v1/token", valorantData)["affinities"]["live"];
            DetermineEndPoints();
        }

        #region Meth

        bool ParseLockFile()
        {
            var lockfileLocation = $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\Riot Games\Riot Client\Config\lockfile";

            if (File.Exists(lockfileLocation))
            {
                using (FileStream fileStream = new FileStream(lockfileLocation, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                using (StreamReader sr = new StreamReader(fileStream))
                {
                    string[] parts = sr.ReadToEnd().Split(":");

                    lockfileData.processName = parts[0];
                    lockfileData.processId = parts[1];
                    lockfileData.port = parts[2];
                    lockfileData.password = parts[3];
                    lockfileData.protocol = parts[4];

                    return true;
                }
            }


            // Lock File was not found, wait for the file to show up using a watcher.
            return false;
        }

        public void GetTokens()
        {
            // Get bearer and entitle from sock
            var responceEntitlements = Endpoints.GetWebsocketAPI("/entitlements/v1/token");
            riotDetails.accessToken = (string)responceEntitlements["accessToken"];
            riotDetails.entitlementToken = (string)responceEntitlements["token"];
            riotDetails.userPID = (string)responceEntitlements["subject"];
        }

        void DetermineEndPoints()
        {
            switch (riotDetails.gameShard.ToLower())
            {
                case "na":
                    riotUserData.glzURL = "https://glz-na-1.na.a.pvp.net/";
                    riotUserData.pdURL = "https://pd.na.a.pvp.net/";
                    break;
                case "eu":
                    riotUserData.glzURL = "https://glz-eu-1.eu.a.pvp.net/";
                    riotUserData.pdURL = "https://pd.eu.a.pvp.net/";
                    break;
                case "latam":
                    riotUserData.glzURL = "https://glz-latam-1.na.a.pvp.net/";
                    riotUserData.pdURL = "https://pd.na.a.pvp.net/";
                    break;
                case "br":
                    riotUserData.glzURL = "https://glz-br-1.na.a.pvp.net/";
                    riotUserData.pdURL = "https://pd.na.a.pvp.net/";
                    break;
                case "ap":
                    riotUserData.glzURL = "https://glz-ap-1.ap.a.pvp.net/";
                    riotUserData.pdURL = "https://pd.ap.a.pvp.net/";
                    break;
                case "kr":
                    riotUserData.glzURL = "https://glz-kr-1.kr.a.pvp.net/";
                    riotUserData.pdURL = "https://pd.kr.a.pvp.net/";
                    break;


            }
        }
        #endregion

        #region Websocket

        void ConnectToWebsocket()
        {
            socket = new WebSocket($"wss://127.0.0.1:{lockfileData.port}/", "wamp");
            socket.SetCredentials("riot", lockfileData.password, true);
            // sock sll off woooooooooo poggers
            socket.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12;
            socket.SslConfiguration.ServerCertificateValidationCallback = delegate { return true; };
            socket.OnMessage += Socket_OnMessage;
            socket.OnClose += Socket_OnClose;
            socket.Connect();
            socket.Send("[5, \"OnJsonApiEvent\"]");

        }

        private static void Socket_OnMessage(object sender, MessageEventArgs e)
        {
            // Sock resp? then do something.
            Debug.WriteLine(e.Data);
        }

        private static void Socket_OnClose(object sender, CloseEventArgs e)
        {
            // Sock close? then close program to avoid errors.
            Environment.Exit(1);
        }

        #endregion
    }
}
