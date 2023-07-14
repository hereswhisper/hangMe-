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

            AGSConnector.SendGameStateRequest(AGSConnector.webSocket); // Send SendGameStateRequest to Server (after this, it should ask client for Ack)

            while (!Global.hasRecievedGameState)
            {

            }

            ShowAcknowledgementScreen(); // Acknowledging server so they know I am a legitimate client.

            AGSConnector.SendAcknowledgementRequest(AGSConnector.webSocket); // Send Acknowledgement packet
            
            while(!Global.isAcknowledged)
            {

            }

            ShowDroppingScreen(); // We are all ready for it, dropping Connecting Screen

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

        public static bool ShowAcknowledgementScreen()
        {
            Console.Clear(); // ONLY TIME WHERE THIS IS ALLOWED.
            AScreenPositioner.MiddleCenterScreen("Acknowledging Server...");
            return true;
        }

        public static bool ShowDroppingScreen()
        {
            Console.Clear(); // ONLY TIME WHERE THIS IS ALLOWED.
            AScreenPositioner.MiddleCenterScreen("Loading Gameboard...");
            return true;
        }
    }
}
