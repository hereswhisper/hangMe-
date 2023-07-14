using HangMe.Engine.Client.Classes.Create;
using HangMe.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes.Widgets
{
    internal class AStartMenuScreen
    {
        public async Task showContents()
        {
            AConsoleUtilities smoothWriter = new AConsoleUtilities();

            AScreenPositioner.TopCenterScreen("\nhangMe");

            AScreenPositioner.BottomLeftScreen("Welcome, Whisper"); // Placeholder till account system was added

            AScreenPositioner.MiddleCenterScreen("1. Connect to Session  2. Quit Game");

            string a = "";
            a = askForInput();

            switch(a)
            {
                case "1":
                    await ShowConnectIpScreen();
                    break;
                case "2":
                    break;
                default:
                    await reShowScreenFix();
                    break;
            }

            return; // make sure all Widgets have return at the ends 
        }

        // wowie
        public static async Task<bool> ShowConnectIpScreen()
        {
            AWidgetCreator aWidget = new AWidgetCreator();
            aWidget.wipeAllWidgets(true);
            await aWidget.createWidgetAsync("connectip");
            return true;
        }
        // wow epic fix
        public static async Task<bool> reShowScreenFix()
        {
            AWidgetCreator aWidgetCreator = new AWidgetCreator();
            aWidgetCreator.wipeAllWidgets(true);
            await aWidgetCreator.createWidgetAsync("startmenu");
            return true;
        }
        private static string askForInput()
        {
            string a = Console.ReadLine();
            if (a == "" || a == null) return "2";
            return a;
        }
    }
}
