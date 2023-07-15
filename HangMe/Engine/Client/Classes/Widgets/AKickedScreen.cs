using HangMe.Engine.Client.Classes.Create;
using HangMe.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes.Widgets
{
    internal class AKickedScreen
    {
        /// <summary>
        /// DEPRECATED.
        /// </summary>
        /// <param name="reason"></param>
        /// <returns></returns>
        public async Task showContents(string reason)
        {
            AConsoleUtilities smoothWriter = new AConsoleUtilities();

            AScreenPositioner.TopLeftScreen("hangMe!");
            AScreenPositioner.TopRightScreen("You've been kicked.");

            AScreenPositioner.MiddleCenterScreen("Reason: " + reason + ". You will return to lobby in a few seconds.");

            AConsoleUtilities.Sleep(3000);

            returnToMenu(); // return to main menu.

            return;
        }

        public static async void returnToMenu()
        {
            AWidgetCreator a = new AWidgetCreator();
            a.wipeAllWidgets(true);
            await a.createWidgetAsync("startmenu");
        }
    }
}
