using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes.Skeletons
{
    internal class AHangGameState
    {
        public List<string> _guessedletters = new List<string>();
        //private List<string> _possibleWords = new List<string>(); (wait client doesn't need this)
        public int _gameId = -1;
        //public string[] _players = { }; // Client only needs this for putting their names on the screen (unneeded)
        public int _playerCount = 0; // Player Count
        public List<string> _correctLetters = new List<string>();
        public string _selectedWord = ""; // Word that is currently selected.

        // ... Rest of the GameState is server sided (this is just what we need for client)
    }
}
