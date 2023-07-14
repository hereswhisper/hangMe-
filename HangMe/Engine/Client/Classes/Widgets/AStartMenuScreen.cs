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

            AScreenPositioner.TopCenterScreen("hangMe");

            AScreenPositioner.MiddleCenterScreen("1. Connect to Session\n2. Quit Game");

            return; // make sure all Widgets have return at the ends 
        }
    }
}
