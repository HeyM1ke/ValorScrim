using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace RumbleMike
{
    class Endpoints
    {

        public static JObject GetWebsocketAPI(string endpoint)
        {
            IRestClient GetClient = new RestClient(new Uri($"https://127.0.0.1:{RiotUser.Instance.lockfileData.port}{endpoint}"));
            GetClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            IRestRequest GetRequest = new RestRequest(Method.GET);
            GetRequest.AddHeader("Authorization", $"Basic {Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"riot:{RiotUser.Instance.lockfileData.password}"))}");
            GetRequest.AddHeader("X-Riot-ClientPlatform", "ew0KCSJwbGF0Zm9ybVR5cGUiOiAiUEMiLA0KCSJwbGF0Zm9ybU9TIjogIldpbmRvd3MiLA0KCSJwbGF0Zm9ybU9TVmVyc2lvbiI6ICIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwNCgkicGxhdGZvcm1DaGlwc2V0IjogIlVua25vd24iDQp9");
            GetRequest.AddHeader("X-Riot-ClientVersion", "release-02.05-shipping-3-531230");
            IRestResponse getResp = GetClient.Get(GetRequest);

            if (getResp.IsSuccessful)
                return JObject.Parse(getResp.Content);
            else
                return null;
        }

        public static JObject POSTWebsocketAPI(string endpoint, Object data)
        {
            IRestClient postClient = new RestClient(new Uri($"https://127.0.0.1:{RiotUser.Instance.lockfileData.port}{endpoint}"));
            postClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            IRestRequest postRequest = new RestRequest(Method.POST);
            postRequest.AddHeader("Authorization", $"Basic {Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"riot:{RiotUser.Instance.lockfileData.password}"))}");

            string body = JsonConvert.SerializeObject(data);
            postRequest.AddJsonBody(body);

            IRestResponse postResp = postClient.Post(postRequest);

            

            if (postResp.IsSuccessful)
                return JObject.Parse(postResp.Content);
            else
                return null;
        }


        public static JObject GETCurrentParty()
        {
            RiotUser.Instance.GetTokens();
            IRestClient getClient = new RestClient(new Uri($"{RiotUser.Instance.riotUserData.glzURL}parties/v1/players/{RiotUser.Instance.riotDetails.userPID}"));
            IRestRequest getRequest = new RestRequest(Method.GET);
            getRequest.AddHeader("Authorization", $"Bearer {RiotUser.Instance.riotDetails.accessToken}");
            getRequest.AddHeader("X-Riot-Entitlements-JWT", RiotUser.Instance.riotDetails.entitlementToken);
            getRequest.AddHeader("X-Riot-ClientPlatform", "ew0KCSJwbGF0Zm9ybVR5cGUiOiAiUEMiLA0KCSJwbGF0Zm9ybU9TIjogIldpbmRvd3MiLA0KCSJwbGF0Zm9ybU9TVmVyc2lvbiI6ICIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwNCgkicGxhdGZvcm1DaGlwc2V0IjogIlVua25vd24iDQp9");
            getRequest.AddHeader("X-Riot-ClientVersion", "release-02.05-shipping-3-531230");

            IRestResponse getResp = getClient.Get(getRequest);


            if (getResp.IsSuccessful)
                return JObject.Parse(getResp.Content);
            else
                return null;
        }

        public static JObject POSTCustomGameSettings(string map, string server)
        {
            RiotUser.Instance.GetTokens();
            IRestClient postClient = new RestClient(new Uri($"{RiotUser.Instance.riotUserData.glzURL}parties/v1/parties/{RiotUser.Instance.riotUserData.partyID}/customgamesettings"));
            IRestRequest postRequest = new RestRequest(Method.POST);
            postRequest.AddHeader("Authorization", $"Bearer {RiotUser.Instance.riotDetails.accessToken}");
            postRequest.AddHeader("X-Riot-Entitlements-JWT", RiotUser.Instance.riotDetails.entitlementToken);
            postRequest.AddHeader("X-Riot-ClientPlatform", "ew0KCSJwbGF0Zm9ybVR5cGUiOiAiUEMiLA0KCSJwbGF0Zm9ybU9TIjogIldpbmRvd3MiLA0KCSJwbGF0Zm9ybU9TVmVyc2lvbiI6ICIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwNCgkicGxhdGZvcm1DaGlwc2V0IjogIlVua25vd24iDQp9");
            postRequest.AddHeader("X-Riot-ClientVersion", "release-02.05-shipping-3-531230");

            Object settings = new
            {
                map = map,
                Mode = "/Game/GameModes/Bomb/BombGameMode.BombGameMode_C",
                GamePod = server
            };

            string body = JsonConvert.SerializeObject(settings);
            postRequest.AddJsonBody(body);

            IRestResponse postResp = postClient.Post(postRequest);


            if (postResp.IsSuccessful)
                return JObject.Parse(postResp.Content);
            else
                return null;
        }
        public static JObject POSTMakeCustomGame()
        {
            RiotUser.Instance.GetTokens();
            IRestClient postClient = new RestClient(new Uri($"{RiotUser.Instance.riotUserData.glzURL}parties/v1/parties/{RiotUser.Instance.riotUserData.partyID}/makecustomgame"));
            IRestRequest postRequest = new RestRequest(Method.POST);
            postRequest.AddHeader("Authorization", $"Bearer {RiotUser.Instance.riotDetails.accessToken}");
            postRequest.AddHeader("X-Riot-Entitlements-JWT", RiotUser.Instance.riotDetails.entitlementToken);
            postRequest.AddHeader("X-Riot-ClientPlatform", "ew0KCSJwbGF0Zm9ybVR5cGUiOiAiUEMiLA0KCSJwbGF0Zm9ybU9TIjogIldpbmRvd3MiLA0KCSJwbGF0Zm9ybU9TVmVyc2lvbiI6ICIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwNCgkicGxhdGZvcm1DaGlwc2V0IjogIlVua25vd24iDQp9");
            postRequest.AddHeader("X-Riot-ClientVersion", "release-02.05-shipping-3-531230");

            IRestResponse postResp = postClient.Post(postRequest);


            if (postResp.IsSuccessful)
                return JObject.Parse(postResp.Content);
            else
                return null;
        }
        public static JObject POSTCreateparty()
        {
            RiotUser.Instance.GetTokens();
            IRestClient postClient = new RestClient(new Uri($"{RiotUser.Instance.riotUserData.glzURL}parties/v1/players/{RiotUser.Instance.riotDetails.userPID}/leaveparty/{RiotUser.Instance.riotUserData.partyID}"));
            IRestRequest postRequest = new RestRequest(Method.POST);
            postRequest.AddHeader("Authorization", $"Bearer {RiotUser.Instance.riotDetails.accessToken}");
            postRequest.AddHeader("X-Riot-Entitlements-JWT", RiotUser.Instance.riotDetails.entitlementToken);
            postRequest.AddHeader("X-Riot-ClientPlatform", "ew0KCSJwbGF0Zm9ybVR5cGUiOiAiUEMiLA0KCSJwbGF0Zm9ybU9TIjogIldpbmRvd3MiLA0KCSJwbGF0Zm9ybU9TVmVyc2lvbiI6ICIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwNCgkicGxhdGZvcm1DaGlwc2V0IjogIlVua25vd24iDQp9");
            postRequest.AddHeader("X-Riot-ClientVersion", "release-02.05-shipping-3-531230");

            IRestResponse postResp = postClient.Post(postRequest);


            if (postResp.IsSuccessful)
                return JObject.Parse(postResp.Content);
            else
                return null;
        }

        public static JObject POSTOpenLobby()
        {
            RiotUser.Instance.GetTokens();
            IRestClient postClient = new RestClient(new Uri($"{RiotUser.Instance.riotUserData.glzURL}parties/v1/parties/{RiotUser.Instance.riotUserData.partyID}/accessibility"));
            IRestRequest postRequest = new RestRequest(Method.POST);
            postRequest.AddHeader("Authorization", $"Bearer {RiotUser.Instance.riotDetails.accessToken}");
            postRequest.AddHeader("X-Riot-Entitlements-JWT", RiotUser.Instance.riotDetails.entitlementToken);
            postRequest.AddHeader("X-Riot-ClientPlatform", "ew0KCSJwbGF0Zm9ybVR5cGUiOiAiUEMiLA0KCSJwbGF0Zm9ybU9TIjogIldpbmRvd3MiLA0KCSJwbGF0Zm9ybU9TVmVyc2lvbiI6ICIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwNCgkicGxhdGZvcm1DaGlwc2V0IjogIlVua25vd24iDQp9");
            postRequest.AddHeader("X-Riot-ClientVersion", "release-02.05-shipping-3-531230");

             postRequest.AddParameter("application/json", "{\r\n    \"Accessibility\":\"OPEN\"\r\n}", ParameterType.RequestBody);
            IRestResponse postResp = postClient.Post(postRequest);


            if (postResp.IsSuccessful)
                return JObject.Parse(postResp.Content);
            else
                return null;
        }

        public static JObject POSTJoinparty(string code)
        {
            RiotUser.Instance.GetTokens();
            IRestClient postClient = new RestClient(new Uri($"{RiotUser.Instance.riotUserData.glzURL}parties/v1/players/{RiotUser.Instance.riotDetails.userPID}/joinparty/{code}"));
            IRestRequest postRequest = new RestRequest(Method.POST);
            postRequest.AddHeader("Authorization", $"Bearer {RiotUser.Instance.riotDetails.accessToken}");
            postRequest.AddHeader("X-Riot-Entitlements-JWT", RiotUser.Instance.riotDetails.entitlementToken);
            postRequest.AddHeader("X-Riot-ClientPlatform", "ew0KCSJwbGF0Zm9ybVR5cGUiOiAiUEMiLA0KCSJwbGF0Zm9ybU9TIjogIldpbmRvd3MiLA0KCSJwbGF0Zm9ybU9TVmVyc2lvbiI6ICIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwNCgkicGxhdGZvcm1DaGlwc2V0IjogIlVua25vd24iDQp9");
            postRequest.AddHeader("X-Riot-ClientVersion", "release-02.05-shipping-3-531230");

            IRestResponse postResp = postClient.Post(postRequest);


            if (postResp.IsSuccessful)
                return JObject.Parse(postResp.Content);
            else
                return null;
        }

        public static string GetPlayerCard()
        {
            RiotUser.Instance.GetTokens();
            string playercardID;
            IRestClient getClient = new RestClient(new Uri($"{RiotUser.Instance.riotUserData.pdURL}personalization/v2/players/{RiotUser.Instance.riotDetails.userPID}/playerloadout"));
            IRestRequest getRequest = new RestRequest(Method.GET);
            getRequest.AddHeader("Authorization", $"Bearer {RiotUser.Instance.riotDetails.accessToken}");
            getRequest.AddHeader("X-Riot-Entitlements-JWT", RiotUser.Instance.riotDetails.entitlementToken);
            getRequest.AddHeader("X-Riot-ClientPlatform", "ew0KCSJwbGF0Zm9ybVR5cGUiOiAiUEMiLA0KCSJwbGF0Zm9ybU9TIjogIldpbmRvd3MiLA0KCSJwbGF0Zm9ybU9TVmVyc2lvbiI6ICIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwNCgkicGxhdGZvcm1DaGlwc2V0IjogIlVua25vd24iDQp9");
            getRequest.AddHeader("X-Riot-ClientVersion", "release-02.05-shipping-3-531230");

            IRestResponse getResp = getClient.Get(getRequest);
            if (getResp.IsSuccessful)
            {
                var resp = JObject.Parse(getResp.Content);
                playercardID = (string)resp["PlayerCard"]["ID"];
                Debug.WriteLine(playercardID);
                return $"https://media.valorant-api.com/playercards/{playercardID}/smallart.png";
            }
                
            else
                return null;

            
        }
    }
}
