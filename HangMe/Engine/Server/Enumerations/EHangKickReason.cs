using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Server.Enumerations
{
#if WITH_SERVER_CODE
    public struct EHangKickReason
    {
        public static string UNSPECIFIED = "Unknown";
        public static string KICKEDALL = "You have been kicked because the server has kicked everyone. (Game restarting..?)";
        public static string BANNED = "You have been banned from playing hangMe!";
        public static string KICKED = "You have been kicked from this room.";
        public static string NETWORKERR = "You have been kicked due to a networking error. Please check your internet connection and try again.";
        public static string GAMESERVERERR = "You have been kicked due to a error with communicating with the Gameserver. Try again later";
        public static string ABNORMALGAMEPLAY = "You have been kicked due to abnormal gameplay. Please reconnect and try again";
        public static string IPBANNED = "You have been IP banned from playing hangMe";
        public static string UNAUTHCLIENT = "You have a unauthorized Client trying to connect to hangMe Servers. Please download the official one and try again";
        public static string VOTEKICKED = "You have been vote kicked from the server. Please reconnect and play again";
        public static string ADMINKICK = "You have been kicked from a admin of hangMe";
    }
#endif
}
