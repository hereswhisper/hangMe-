using HangMe.Engine.Client.Classes.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangMe.Engine.Common;

namespace HangMe.Engine.Client.Classes.Widgets
{
    internal class ADisclaimerScreen
    {
        public async Task showContents()
        {
            AConsoleUtilities smoothWriter; // never used at the moment
            AScreenPositioner.MiddleCenterScreen("Disclaimer: This game is still in it's early stages.");
            AScreenPositioner.BottomCenterScreen("Created by @whisperends on discord", true, 100);
            return; // always return a Widget to make sure everything doesn't break
        }
    }
}
