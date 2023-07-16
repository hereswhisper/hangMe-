using HangMe.Engine.Client.Classes.Connectors;
using HangMe.Engine.Client.Classes.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes.Functions
{
    internal class ClientRoomLocked
    {
        public static async void execute(ClientWebSocket webSocket)
        {
            if (AGSConnector._localGameState._playerCount != 0) return; // not important to you

            Console.Clear();
            Console.WriteLine("This room is currently locked. Please restart your game to play");

            Thread.Sleep(3000);
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
            AWidgetCreator a = new AWidgetCreator();

            a.wipeAllWidgets(true);
            await a.createWidgetAsync("startmenu");
        }
    }
}
