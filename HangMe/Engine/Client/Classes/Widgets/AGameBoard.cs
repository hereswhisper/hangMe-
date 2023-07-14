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
        public async Task showContents()
        {
            // making sure all relevant information is correct
            if(AGSConnector._localGameState._playerCount != 0)
            {

            } else
            {
                return; // L bozo
            }
        }
    }
}
