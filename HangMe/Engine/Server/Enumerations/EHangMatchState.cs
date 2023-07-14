using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Server.Enumerations
{
    enum EHangMatchState
    {
        VOID, // Nothing is happening, there is no game currently in progress and it's just limboing
        WAITING, // Waiting for players to log on and come play
        PRE, // In PreGame Lobby, Waiting for Server to choose word and wait for all players to connect successfully
        DURING, // Middle of a current game, Nobody can join during this.
        POST // Game has ended and they are seeing their scores
    }
}
