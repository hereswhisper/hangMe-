using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes
{
    internal class Global
    {
        public static bool isConnected = false; // change this when it is
        public static bool hasRecievedGameState = false; // waiting for GameState
        public static bool isAcknowledged = false; // Acknowledgement is IMPORTANT for security.
        public static bool hasRegistered = false; // Has to register as a proper user in the server
        public static float lastTick = 0f;
    }
}
