using HangMe.Engine.Client.Classes.Connectors;
using HangMe.Engine.Client.Classes.Create;
using HangMe.Engine.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes.Widgets
{
    internal class AGameBoard
    {
        public static int a = 0;
        public static string c = "";
        public static List<string> d = new List<string>();
        public static bool b = false;
        public async Task showContents()
        {
            a = AGSConnector._localGameState._playerCount;
            c = AGSConnector._localGameState._selectedWord;
            d = AGSConnector._localGameState._guessedletters;
            if(b == false)
            {
                Task.Run(() => updateLoop()); // run update loop
                b = true; // update loop is running
            }
            
            // making sure all relevant information is correct
            if(AGSConnector._localGameState._playerCount != 0)
            {
                // ... Make sure to add thread to loop and make sure nothing has changed with gameState, if so refresh gameboard

                //AScreenPositioner.BottomCenterScreen("DEBUGG: " + AGSConnector._localGameState._selectedWord);
                AScreenPositioner.MiddleCenterScreen(getWord()); // gonna need a function for this
                AScreenPositioner.BottomLeftScreen(getGuessedLetters());
                AScreenPositioner.BottomRightScreen("Player Count: " + AGSConnector._localGameState._playerCount);
                if(AGSConnector._localPlayer.importantAnnouncement != "" || AGSConnector._localGameState._turnPlayerId == AGSConnector._localPlayer.PlayerId)
                {
                    AScreenPositioner.BottomCenterScreen(AGSConnector._localPlayer.importantAnnouncement);
                }

                if(AGSConnector._localPlayer.myTurn == true)
                {
                    Guess();
                }
                

            } else
            {
                return; // L bozo
            }
        }

        public static string getGuessedLetters()
        {
           string finalWord = "Guessed Letters: ";

           for(int i = 0; i < AGSConnector._localGameState._guessedletters.Count; i++)
            {
                finalWord = finalWord + AGSConnector._localGameState._guessedletters[i].ToString() + " ";
            }

           return finalWord;
        }

        public static void Guess()
        {
            Console.Write("Guess a LETTER: ");
            string letter = Console.ReadLine();
            AGSConnector.SendGuessRequest(AGSConnector.webSocket, letter);
        }

        public static async void ForceRefreshGameboard()
        {
            AWidgetCreator a = new AWidgetCreator();
            a.wipeAllWidgets(true);
            await a.createWidgetAsync("gameboard");
        }

        public static string getWord()
        {
            string finalWord = "";
            if (AGSConnector._localGameState._selectedWord == "")
            {
                return "Waiting for word...";
            }
            else
            {
                bool ahh = false;
                for (int i = 0; i < AGSConnector._localGameState._selectedWord.Length; i++)
                {
                    if (AGSConnector._localGameState._correctLetters.Count != 0)
                    {
                        for (int a = 0; a < AGSConnector._localGameState._correctLetters.Count; a++)
                        {
                            if (AGSConnector._localGameState._selectedWord[i] == AGSConnector._localGameState._correctLetters.ToString()[a])
                            {
                                ahh = true;
                                finalWord += AGSConnector._localGameState._correctLetters[a] + " ";
                            }
                        }
                    }
                    else
                    {
                            finalWord += "_ ";
                    }

                    if(ahh == false)
                    {
                        finalWord += "_ ";
                    }
                }
            }
            return finalWord;
        }

        public static async Task<bool> updateLoop()
        {
            while(true)
            {
                if(a != AGSConnector._localGameState._playerCount || c != AGSConnector._localGameState._selectedWord || d.Count != AGSConnector._localGameState._guessedletters.Count)
                {
                //Console.WriteLine("DEBUG: " + AGSConnector._localPlayer.PlayerId);
                    AWidgetCreator a = new AWidgetCreator();
                    a.wipeAllWidgets(true);
                    await a.createWidgetAsync("gameboard");
                //Console.WriteLine("Turn is: " + AGSConnector._localGameState._turnPlayerId);
                }
            }
            return false; // no update
        }
    }
}
