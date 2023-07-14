using HangMe.Engine.Client.Classes.Create;
using HangMe.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangMe.Engine.Common;

namespace HangMe.Engine.Client.Classes.Widgets
{
    internal class ALoginScreen
    {
        public async Task showContents()
        {
            AConsoleUtilities smoothWriter = new AConsoleUtilities();

            AScreenPositioner.TopCenterScreen("hangMe!");

            AScreenPositioner.MiddleCenterScreen("Please Login:");
            
            LoginForm();

            return; 
        }
        
        public static void LoginForm()
        {
            // get email 
            AConsoleUtilities.SmoothWrite("Email: ", 100);
            var email = AConsoleUtilities.UserInput();

            // get password
            AConsoleUtilities.SmoothWrite("Password: ", 100);
            var password = AConsoleUtilities.UserInput();

            // ... TODO: Add API connecter stuff here. (need to make API)

            return;
        }
    }
}
