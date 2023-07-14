using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Server.GameState
{
#if WITH_SERVER_CODE
    internal class AHangGameState
    {
        private List<string> _guessedLetters = new List<string>(); // Where the player's are gonna guess
        private List<string> _possibleWords = new List<string>(); // Possible words the gamestate can choose
        private int _gameId = -1; // the unique Game identification code. (if the GameId is -1, there's been a issue).
        public string[] _players = { }; // player names who are in the session
        public int _playerCount = 0; // Player Count
        public List<string> _correctLetters = new List<string>(); // Letters that are correct

        /// <summary>
        /// The HangMe main GameState
        /// </summary>
        /// <param name="words"></param>
        /// The possible words the Gamestate can choose from. This could be the pre word list made from scratch from hangME! or a custom one by the user
        /// <param name="gameId"></param>
        /// The game ID (this isn't going to be used till I add the ability to host multiple games at once)
        public AHangGameState(List<string> words, int gameId = 0000) {
            if(words != null && gameId != -1)
            {
                _possibleWords = words;
                _gameId = gameId;
            } else
            {
                Console.WriteLine("[HangGameState FATAL]: AHangGameState failed to initalize. GameID or Possible words is NULL");
            }
        }

        /// <summary>
        /// Adds letter to guessedLetters (will required all players force Tick)
        /// </summary>
        /// <param name="letter"></param>
        /// <returns></returns>
        public int AddLetterToList(string letter)
        {
            if (letter == null)
            {
                Console.WriteLine("[HangGameState NON-FATAL]: Letter was marked NULL. No letter was added to guessed letters");
                return 1; // failure
            }

            if (letter.Length >= 2)
            {
                Console.WriteLine("[HangGameState NON-FATAL]: More than one Letter was marked. No letters were added to guessed letters.");
            }

            _guessedLetters.Add(letter);
            Console.WriteLine("[HangGameState INFO]: Successfully added " + letter + "to GameState!");
            return 0; // completed and ready for next tick
        }

        public int EndGame()
        {
            // TODO: Add this <3
            return 0; // success
        }
    }
#endif
}
