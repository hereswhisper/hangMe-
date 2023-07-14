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
            }
        }
    }
}
