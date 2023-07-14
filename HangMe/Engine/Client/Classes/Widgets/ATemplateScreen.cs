using HangMe.Engine.Client.Classes.Create;
using HangMe.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes.Widgets
{
    /// <summary>
    /// NEVER USE THIS, IT'S FOR TEMPLATE AND ENGINE PURPOSES.
    /// </summary>
    internal class ATemplateScreen
    {
        public async Task showContents()
        {
            AConsoleUtilities smoothWriter = new AConsoleUtilities(); // Make all widgets have this please. It's so that if I ever need it, I may add stuff.

            AScreenPositioner.MiddleCenterScreen("Disclaimer: This game is still in it's early stages."); // add contents (please don't use built in c# .net shit)

            return; // make sure all Widgets have return at the ends 
        }

        // ... Add any other functions it may need like for key presses, etc
    }
}
