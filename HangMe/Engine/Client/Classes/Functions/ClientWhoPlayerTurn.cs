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
    internal class ClientWhoPlayerTurn
    {
        public static void execute(JObject json)
        {
            string userid = json["UserId"]?.ToString();

            if (AGSConnector._localPlayer.PlayerId == userid)
            {
                AGSConnector._localPlayer.myTurn = true;
                AGameBoard.ForceRefreshGameboard(); // force refresh board
            }
        }
    }
}
