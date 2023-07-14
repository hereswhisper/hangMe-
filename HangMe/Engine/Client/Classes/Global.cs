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
        public static float lastTick = 0f;
    }
}
