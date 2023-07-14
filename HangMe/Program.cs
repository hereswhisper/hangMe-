using HangMe.Engine.API;
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
            bool hasDidSomething = false;
            // see if there is -server or -api flag on it
            // Get the command-line arguments
            string[] commandLineArgs = Environment.GetCommandLineArgs();

            // Process the arguments
            for (int i = 0; i < commandLineArgs.Length; i++)
            {
                if (commandLineArgs[i] == "-api")
                {
                    hasDidSomething = true;
                    Task.Run(() => api.StartAPI()).Wait();
                    break;
                } 
                Console.WriteLine($"Argument {i}: {commandLineArgs[i]}");
            }

            //if (hasDidSomething) return;
#endif

            //client.startClientAsync();


        }
    }
}
