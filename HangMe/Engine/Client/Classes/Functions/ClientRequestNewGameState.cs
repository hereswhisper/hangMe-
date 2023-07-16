using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes.Functions
{
    internal class ClientRequestNewGameState
    {
        public static void execute(JObject json, Skeletons.AHangGameState _localGameState)
        {
            JArray guessedLettersArray = json["guessedletters"] as JArray;
            int gameId = json["gameId"]?.ToObject<int>() ?? -1;
            //JArray playersArray = json["players"] as JArray;
            int playerCount = json["playerCount"]?.ToObject<int>() ?? 0;
            JArray correctLettersArray = json["correctLetters"] as JArray;
            string selectedWord = json["selectedWord"]?.ToString();
            string nextCommand = json["nextCommand"]?.ToString();

            List<string> guessedLetters = guessedLettersArray?.ToObject<List<string>>();
            List<string> correctLetters = correctLettersArray?.ToObject<List<string>>();
            //string[] PlayersArrayClean = playersArray?.ToObject<string[]>();

            _localGameState._gameId = gameId;
            //_localGameState._players = PlayersArrayClean;
            _localGameState._guessedletters = guessedLetters;
            _localGameState._playerCount = playerCount;
            _localGameState._correctLetters = correctLetters;
            _localGameState._selectedWord = selectedWord;
        }
    }
}
