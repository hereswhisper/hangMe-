using HangMe.Engine.Client.Classes.Connectors;
using HangMe.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes.Replicator
{
    internal class AReplicator
    {
        /// <summary>
        /// Ticks player for the most relevant information from Server
        /// </summary>
        /// <param name="isServer"></param>
        public static void TickThread(int isServer = 0)
        {
            bool isRunning = true;

            while(isRunning)
            {
                if (isServer == 0)
                {
                    continue;
                }

                AGSConnector.SendNewGameStateRequest(AGSConnector.webSocket); // Get new updated gamestate again

                AConsoleUtilities.Sleep(1500);

                AGSConnector.SendGetPlayerTurnRequest(AGSConnector.webSocket); // Get player Turn


                //Console.WriteLine("Tick");

                AConsoleUtilities.Sleep(1500); // sleep till next tick
            }
        }
    }
}
