using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Server.GameState
{
#if WITH_SERVER_CODE
    /// <summary>
    /// ONLY CALL THIS WHENEVER PLAYER JOINS, DO NOT CALL IT FROM STARTUP.
    /// </summary>
    internal class ASpectatorGameState
    {
        private List<string[]> _players = new List<string[]>(); // Players currently using SpectatorGameState

        /// <summary>
        /// The Spectator Game State for players who joined while game is in "DURING"
        /// </summary>
        public ASpectatorGameState(string name, int uniqueCode)
        {
            string code = uniqueCode.ToString();
            string[] newPlayer = { name, code };
            _players.Add(newPlayer); // the new player has been initalized
        }
    }
#endif
}
