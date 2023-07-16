using HangMe.Engine.Client.Classes.Connectors;
using HangMe.Engine.Client.Classes.Widgets;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes.Functions
{
    internal class ServerEndTurn
    {
        public static void execute(JObject json)
        {
            string UserID = json["UserID"]?.ToString(); // user Id

            if (AGSConnector._localPlayer.PlayerId == UserID)
            {
                AGSConnector._localPlayer.myTurn = false; // remove turn.
                AGameBoard.ForceRefreshGameboard(); // Force refresh <3
            }
        }
    }
}
