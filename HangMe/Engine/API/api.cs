using System.Net;
using System;
using System.Threading.Tasks;
using HangMe.Engine.API.Classes;

namespace HangMe.Engine.API
{
#if WITH_SERVER_CODE
    internal class api
    {
        public static async Task StartAPI()
        {
            string[] prefixes = { "http://localhost:8080/", "http://127.0.0.1:8080/" }; // Set the desired server URL(s)
            WebServer server = new WebServer(prefixes);
            server.Start();

            Console.WriteLine("Press Enter to stop the server...");
            Console.ReadLine();

            server.Stop();
        }

    }
#endif
}
