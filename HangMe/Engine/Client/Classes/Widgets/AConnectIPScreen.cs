using HangMe.Engine.Client.Classes.Create;
using HangMe.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes.Widgets
{
    internal class AConnectIPScreen
    {
        public async Task showContents()
        {
            AConsoleUtilities smoothWriter = new AConsoleUtilities();

            AScreenPositioner.BottomCenterScreen("IP Address: ", true, 100);

            string ip = "";
            ip = getIP();

            StartConnectionProcess(ip);

            // ... Connect to Gameserver starts here

            return;
        }

        public static bool StartConnectionProcess(string ip)
        {
            AWidgetCreator a = new AWidgetCreator();
            a.wipeAllWidgets(true);
            a.createWidgetAsync("connectionscreen", ip);
            return true;
        }
        public static string getIP()
        {
            string ip = Console.ReadLine();
            if (ip == null || ip == "") return "127.0.0.1:8080";
            return ip;
        }
    }
}
