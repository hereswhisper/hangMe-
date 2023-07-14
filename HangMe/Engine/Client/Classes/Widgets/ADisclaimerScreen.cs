using HangMe.Engine.Client.Classes.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes.Widgets
{
    internal class ADisclaimerScreen
    {
        public static void showContents()
        {
            AScreenPositioner.MiddleCenterScreen("Disclaimer: This game is still in it's early stages.");
            return; // always return a Widget to make sure everything doesn't break
        }
    }
}
