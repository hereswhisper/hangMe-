using HangMe.Engine.Client.Classes.Connectors;
using HangMe.Engine.Client.Classes.Create;
using HangMe.Engine.Client.Classes.Replicator;
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

            ShowRegisteringStatus();

            AGSConnector.SendRegisterUserRequest(AGSConnector.webSocket); // wowie

            while (!Global.hasRegistered)
            {
                // ... At this point the server would be setting shit up
            }

            ShowAwaitingGameStateStatus();

            AGSConnector.SendGameStateRequest(AGSConnector.webSocket); // Send SendGameStateRequest to Server (after this, it should ask client for Ack)

            while (!Global.hasRecievedGameState)
            {
                // ... At this point the server will be sending the server's copy of the GameState over to the user to replicate their variables
            }

            ShowAcknowledgementScreen(); // Acknowledging server so they know I am a legitimate client.

            AGSConnector.SendAcknowledgementRequest(AGSConnector.webSocket); // Send Acknowledgement packet
            
            while(!Global.isAcknowledged)
            {
                // ... At this point the client needs to authenticate to the server that yes it's a authorized hangMe Client
            }

            ShowDroppingScreen(); // We are all ready for it, dropping Connecting Screen

            Task tickTask = Task.Run(() => AReplicator.TickThread(1));

            // Additional code here that can run concurrently with TickThread

            await Task.Delay(1); // Allow a small delay to yield control back to the main thread

            await createGameBoard();

            await tickTask;


            return;
        }

        public static bool ShowConnectingStatus()
        {
            AScreenPositioner.MiddleCenterScreen("Connecting...");
            return true;
        }

        public static bool ShowRegisteringStatus()
        {
            AScreenPositioner.MiddleCenterScreen("Registering Player to Server...");
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

        public static async Task<bool> createGameBoard()
        {
            Console.WriteLine("a");
            AWidgetCreator a = new AWidgetCreator(); // ONLY TIME WHERE THIS IS ALLOWED.
            a.wipeAllWidgets(true);
            await a.createWidgetAsync("gameboard");
            return true;
        }
    }
}
