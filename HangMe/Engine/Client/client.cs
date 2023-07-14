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

            await creator.createWidgetAsync("disclaimer"); // create ADisclaimerScreen.

            AConsoleUtilities.Sleep(3000); // sleep

            creator.wipeAllWidgets(); // wipe all the widgets since we no longer need it.
            

        }
    }
}
