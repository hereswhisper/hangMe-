using HangMe.Engine.Client.Classes.Widgets;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes.Functions
{
    internal class ServerRotateTurns
    {
        public static void execute(JObject json, Skeletons.PHangPlayer _localPlayer, Skeletons.AHangGameState _localGameState)
        {
            Console.Beep();
            Console.WriteLine("DEBUG");
            string id = json["UserId"]?.ToString();
            Console.WriteLine("DEBUG: " + id);
            Console.WriteLine("Your Id DEBUG: " + _localPlayer.PlayerId);

            if (_localPlayer.PlayerId == id)
            {
                _localPlayer.myTurn = true;
                _localPlayer.importantAnnouncement = "it's " + id + " turn!";
                _localGameState._turnPlayerId = id;
                AGameBoard.ForceRefreshGameboard(); // force refresh game board so it can have the new turn
                                                    //AGameBoard.Guess(); // Make user guess
            }
            else
            {
                _localPlayer.myTurn = false;
                _localGameState._turnPlayerId = id;
                _localPlayer.importantAnnouncement = "it's " + id + " turn!";
                AGameBoard.ForceRefreshGameboard(); // force refresh game board so it can have the new turn
            }
        }
    }
}
