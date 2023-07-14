using HangMe.Engine.Client;
using HangMe.Engine.Client.Classes.Music;
using HangMe.Engine.Server;
using HangMe.Engine.Server.GameState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HangMe
{
    internal class Program
    {
        static void Main(string[] args)
        {
#if WITH_SERVER_CODE
            // see if there is -server flag on it
#endif

            client.startClientAsync();


        }
    }
}
