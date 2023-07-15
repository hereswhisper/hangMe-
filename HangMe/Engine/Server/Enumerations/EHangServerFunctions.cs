using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Server.Enumerations
{
    public struct EHangServerFunctions
    {
        public static string ServerNotifyUserLogon = "ServerNotifyUserLogon";
        public static string ClientTickRequest = "ClientTickRequest";
        public static string ClientRequestGameState = "ClientRequestGameState";
        public static string ClientRequestNewGameState = "ClientRequestNewGameState";
        public static string ClientAcknowledgment = "ClientAcknowledgment";
        public static string ClientRegister = "ClientRegisterPlayer";
    }
}
