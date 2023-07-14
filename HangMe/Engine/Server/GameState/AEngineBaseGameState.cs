using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Server.GameState
{
#if WITH_SERVER_CODE
    internal class AEngineBaseGameState
    {
        /// <summary>
        /// DON'T EVER USE THIS GAMESTATE. IT'S EMPTY AND FOR THE ENGINE
        /// </summary>
        public AEngineBaseGameState() {
            
        }
    }
#endif
}
