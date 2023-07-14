using HangMe.Engine.Client.Classes.Connectors;
using HangMe.Engine.Client.Classes.Create;
using HangMe.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes.Widgets
{
    internal class AConnectionScreen
    {
        public async Task showContents(string ip)
        {
            AConsoleUtilities smoothWriter = new AConsoleUtilities();

            ShowConnectingStatus();

            await Task.Run(() => AGSConnector.connectToGS(ip)); // run it on different thread.

            while (!Global.isConnected)
            {
                // ... Nothing
            }

            ShowAwaitingGameStateStatus();

            while(!Global.hasRecievedGameState)
            {

            }

            return;
        }

        public static bool ShowConnectingStatus()
        {
            AScreenPositioner.MiddleCenterScreen("Connecting...");
            return true;
        }

        public static bool ShowAwaitingGameStateStatus()
        {
            Console.Clear(); // ONLY TIME WHERE THIS IS ALLOWED.
            AScreenPositioner.MiddleCenterScreen("Awaiting GameState...");
            return true;
        }
    }
}
