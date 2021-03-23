using System;
using System.Collections.Generic;
using System.Text;
using AutoUpdaterDotNET;
using Newtonsoft.Json;
using System.Diagnostics;
namespace ValorParty.RumbleMike.Updates
{
    class Updator
    {
        public Updator()
        {
            AutoUpdater.ParseUpdateInfoEvent += AutoUpdaterOnParseUpdateInfoEvent;
            Debug.WriteLine("Updator Called");
            AutoUpdater.Start("https://502.wtf/ValorScrimMike/resources/updateScrim.json");
        }

        void AutoUpdaterOnParseUpdateInfoEvent(ParseUpdateInfoEventArgs args)
        {
            dynamic json = JsonConvert.DeserializeObject(args.RemoteData);
            args.UpdateInfo = new UpdateInfoEventArgs
            {
                CurrentVersion = json["version"],
                ChangelogURL = json["changelog"],
                DownloadURL = json["url"],
                Mandatory = new Mandatory
                {
                    Value = json["mandatory"]["value"],
                    UpdateMode = json["mandatory"]["UpdateMode"],
                    MinimumVersion = json["mandatory"]["MinimumVersion"]
                }
            };

        }
    }
}
