using HangMe.Engine.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes.Functions
{
    internal class ClientForceLeave
    {
        public static void execute(JObject json)
        {
            string Reason = json["Reason"]?.ToString();

            AConsoleUtilities.ShowMessageBox("You've been kicked. Reason: " + Reason + ". Closing hangMe in 3 seconds");

            Thread.Sleep(3000);

            Environment.Exit(0);
        }
    }
}
