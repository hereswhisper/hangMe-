using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes.Functions
{
    internal class ClientSetupLocalGameState
    {
        public static void execute(string receivedMessage, Skeletons.AHangGameState _localGameState)
        {
            var json = JObject.Parse(receivedMessage);

            // Extract values from the JSON object
            JArray guessedLettersArray = json["guessedletters"] as JArray;
            int gameId = json["gameId"]?.ToObject<int>() ?? -1;
            //JArray playersArray = json["players"] as JArray;
            int playerCount = json["playerCount"]?.ToObject<int>() ?? 0;
            JArray correctLettersArray = json["correctLetters"] as JArray;
            string nextCommand = json["nextCommand"]?.ToString();

            List<string> guessedLetters = guessedLettersArray?.ToObject<List<string>>();
            List<string> correctLetters = correctLettersArray?.ToObject<List<string>>();
            //string[] PlayersArrayClean = playersArray?.ToObject<string[]>();

            _localGameState._gameId = gameId;
            //_localGameState._players = PlayersArrayClean;
            _localGameState._guessedletters = guessedLetters;
            _localGameState._playerCount = playerCount;
            _localGameState._correctLetters = correctLetters;

            Global.hasRecievedGameState = true; // first time get GameState done! time to make sure I exist

            // Process the extracted JSON data
            // ...

            // Example: Print the extracted values
            //Console.WriteLine("Guessed Letters: " + guessedLettersArray?.ToString());
            //Console.WriteLine("Game ID: " + gameId);
            //Console.WriteLine("Players: " + playersArray?.ToString());
            ///Console.WriteLine("Player Count: " + playerCount);
            //Console.WriteLine("Correct Letters: " + correctLettersArray?.ToString());
            //Console.WriteLine("Next Command: " + nextCommand);
        }
    }
}
