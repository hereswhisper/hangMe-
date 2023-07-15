using HangMe.Engine.Client.Classes.Connectors;
using HangMe.Engine.Client.Classes.Create;
using HangMe.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes.Widgets
{
    internal class AGameBoard
    {
        public static int a = 0;
        public static bool b = false;
        public async Task showContents()
        {
            a = AGSConnector._localGameState._playerCount;
            if(b == false)
            {
                Task.Run(() => updateLoop()); // run update loop
                b = true; // update loop is running
            }
            
            // making sure all relevant information is correct
            if(AGSConnector._localGameState._playerCount != 0)
            {
                // ... Make sure to add thread to loop and make sure nothing has changed with gameState, if so refresh gameboard

                AScreenPositioner.MiddleCenterScreen("Waiting for word..."); // gonna need a function for this
                AScreenPositioner.BottomLeftScreen("Guessed letters: ");
                AScreenPositioner.BottomRightScreen("Player Count: " + AGSConnector._localGameState._playerCount);

            } else
            {
                return; // L bozo
            }
        }


        public static async Task<bool> updateLoop()
        {
            while(true)
            {
                if(a != AGSConnector._localGameState._playerCount)
                {
                    AWidgetCreator a = new AWidgetCreator();
                    a.wipeAllWidgets(true);
                    await a.createWidgetAsync("gameboard");
                }
            }
            return false; // no update
        }
    }
}
