using HangMe.Engine.Client.Classes.Create;
using HangMe.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HangMe.Engine.Client
{
    internal class client
    {
        public static async void startClientAsync()
        {
            AWidgetCreator creator = new AWidgetCreator(); // gonna be using this a lot 

            var a = await creator.createWidgetAsync("disclaimer"); // create ADisclaimerScreen.

            if(a == 1)
            {
                creator.wipeAllWidgets(); // wipe all widgets if there is any leftover.
                AConsoleUtilities.SmoothWrite("[AWidgetCreator FATAL]: There has been a issue initalizing AWidgetCreator. Please reinstall your game, if this error continues please contact @whisperends on discord.", 50);
                return; // cannot do anymore.
            }

            AConsoleUtilities.Sleep(3000); // sleep

            creator.wipeAllWidgets(true); // wipe all the widgets since we no longer need it.

            //await creator.createWidgetAsync("login"); // create ALoginScreen (do one day)
        }
    }
}
