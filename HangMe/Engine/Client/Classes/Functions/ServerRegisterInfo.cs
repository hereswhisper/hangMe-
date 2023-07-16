using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes.Functions
{
    internal class ServerRegisterInfo
    {
        public static void execute(string receivedMessage, Skeletons.PHangPlayer _localPlayer)
        {
            var json = JObject.Parse(receivedMessage);
            Console.Beep(); // notify that it's registered
                            //Console.WriteLine("Alive!");
                            //string username = json["Name"]?.ToString();
            string id = json["UserId"]?.ToString();

            //Console.WriteLine(id);
            Thread.Sleep(2000);

            //_localPlayer.name = username;
            _localPlayer.PlayerId = id;
            Global.hasRegistered = true; // User has registered
        }
    }
}
