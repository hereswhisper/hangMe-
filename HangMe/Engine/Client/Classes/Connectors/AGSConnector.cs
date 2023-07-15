using HangMe.Engine.Client.Classes.Create;
using HangMe.Engine.Client.Classes.Skeletons;
using HangMe.Engine.Client.Classes.Widgets;
using HangMe.Engine.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HangMe.Engine.Client.Classes.Connectors
{
    internal class AGSConnector
    {
        public static ClientWebSocket webSocket = new ClientWebSocket();
        public static Client.Classes.Skeletons.AHangGameState _localGameState = new Client.Classes.Skeletons.AHangGameState();
        public static Client.Classes.Skeletons.PHangPlayer _localPlayer = new Skeletons.PHangPlayer();
        public static async void connectToGS(string host)
        {
            string websocketHost = "ws://" + host + "/";

            while (true)
            {
                // Connect to the WebSocket server
                Uri serverUri = new Uri(websocketHost); // Change the URL to match your server configuration

                try
                {
                    await webSocket.ConnectAsync(serverUri, CancellationToken.None);
                    await SendTextMessage(webSocket, "ServerNotifyUserLogon");
                    Global.isConnected = true; // is Connected!

                    // Start a separate thread to receive messages from the server
                    _ = Task.Run(() => ReceiveMessages(webSocket));

                    // Wait for the disconnect instruction from the server
                    while (webSocket.State == WebSocketState.Open)
                    {
                        // Check for the server's disconnect instruction
                        if (ShouldDisconnect(webSocket))
                            break;

                        // Perform other tasks or send messages to the server as needed
                        // ...

                        await Task.Delay(1000); // Wait for a certain duration before the next iteration
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"WebSocket connection error: {ex.Message}");
                }
                finally
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client disconnecting", CancellationToken.None);
                }

                // Wait before attempting to reconnect
                await Task.Delay(5000); // Wait for a certain duration before the next connection attempt
            }
        }

        private static bool ShouldDisconnect(ClientWebSocket webSocket)
        {
            // Replace this condition with your own logic to determine when to disconnect based on the server's instruction
            // For example, you could check for a specific message received from the server
            // or monitor a flag that indicates the disconnect instruction

            // Sample condition: Disconnect when a specific message is received
            //if (receivedMessage == "disconnect")
            //{
            //    return true;
            //}

            // Sample condition: Disconnect when a flag is set
            //if (shouldDisconnect)
            //{
            //    return true;
            //}

            // Return false by default to continue the loop
            return false;
        }

        public static async void SendGameStateRequest(ClientWebSocket webSocket)
        {
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes("ClientRequestGameState");
            await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        // ClientRequestNewGameState
        public static async void SendNewGameStateRequest(ClientWebSocket webSocket)
        {
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes("ClientRequestNewGameState");
            await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        // ClientWhoPlayerTurn
        public static async void SendGetPlayerTurnRequest(ClientWebSocket webSocket)
        {
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes("ClientWhoPlayerTurn");
            await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        // ClientRegisterPlayer
        public static async void SendRegisterUserRequest(ClientWebSocket webSocket)
        {
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes("ClientRegisterPlayer");
            await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
            return;
        }

        public static async void SendAcknowledgementRequest(ClientWebSocket webSocket)
        {
            var data = new
            {
                Command = "ClientAcknowledgment",
                Size = "134122"
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            byte[] messageBytes = Encoding.UTF8.GetBytes(json);

            await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        /// <summary>
        /// wowie
        /// </summary>
        /// <param name="webSocket"></param>
        public static async void SendGuessRequest(ClientWebSocket webSocket, string letter)
        {
            var data = new
            {
                Command = "ClientGuess",
                UserId = _localPlayer.PlayerId,
                Size = letter
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            byte[] messageBytes = Encoding.UTF8.GetBytes(json);

            await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        private static async Task SendTextMessage(ClientWebSocket webSocket, string message)
        {
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
            await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        private static async Task SendJsonMessage(ClientWebSocket webSocket, object data)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            byte[] messageBytes = Encoding.UTF8.GetBytes(json);

            await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        private static async Task ReceiveMessages(ClientWebSocket webSocket)
        {
            byte[] receiveBuffer = new byte[1024];

            while (webSocket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    string receivedMessage = System.Text.Encoding.UTF8.GetString(receiveBuffer, 0, result.Count);
                    //Console.WriteLine("Received: " + receivedMessage);

                    
                    if (Global.isConnected == true && Global.hasRecievedGameState == false)
                    {
                        try
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
                        catch (JsonException ex)
                        {
                            if (Build.bLog) Console.WriteLine("Error parsing JSON: " + ex.Message);
                        }
                    }
                    else if (receivedMessage == "OK")
                    {
                        // assume it's for ClientAcknowledgement
                        Global.isAcknowledged = true; // Client has authenticated.
                    } else if (receivedMessage == "NO")
                    {
                        // assume it's for ClientAcknowledgement
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                        Console.Clear();
                        Console.WriteLine("Disconnected: Unauthorized client detected. Please install the latest update on our Discord.");
                    }
                    else if (receivedMessage.Contains("ServerRegisterInfo"))
                    {
                        var json = JObject.Parse(receivedMessage);
                        Console.Beep(); // notify that it's registered
                        //Console.WriteLine("Alive!");
                        //string username = json["Name"]?.ToString();
                        string id = json["UserId"]?.ToString();

                        //Console.WriteLine(id);
                        Thread.Sleep(2000);

                        //_localPlayer.name = username;
                        _localPlayer.PlayerId = id;
                        Global.hasRegistered = true; // User has registered
                    }
                    else if (receivedMessage.StartsWith("{") ||  receivedMessage.StartsWith("[")) {
                        // ASSUME IT'S A JSON
                        var json = JObject.Parse(receivedMessage);

                        string command = json["Command"]?.ToString();

                        //Console.WriteLine("Command: " + command);

                        if (command == "ServerRegisterInfo")
                        {
                            Console.Beep();
                            //Console.WriteLine("Alive!");
                            //string username = json["Name"]?.ToString();
                            string id = json["UserId"]?.ToString();

                            //Console.WriteLine(id);
                            Thread.Sleep(2000);

                            //_localPlayer.name = username;
                            _localPlayer.PlayerId = id;
                            Global.hasRegistered = true; // User has registered
                        }

                        if (command == "ClientRequestNewGameState")
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

                        // Kicked or something
                        if(command == "ClientForceLeave")
                        {
                            string Reason = json["Reason"]?.ToString();

                            AConsoleUtilities.ShowMessageBox("You've been kicked. Reason: " + Reason + ". Closing hangMe in 3 seconds");

                            Thread.Sleep(3000);

                            Environment.Exit(0);
                        }

                        if(command == "ClientRoomLocked")
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

                        if (command == "ClientWhoPlayerTurn")
                        {
                            string userid = json["UserId"]?.ToString();

                            if(AGSConnector._localPlayer.PlayerId == userid)
                            {
                                AGSConnector._localPlayer.myTurn = true;
                                AGameBoard.ForceRefreshGameboard(); // force refresh board
                            }
                        }

                        // most simple part of the code ever
                        if(command == "ServerEndTurn")
                        {
                            AGameBoard.ForceRefreshGameboard(); // force gameboard refresh.
                        }

                        if(command == "ServerRotateTurns")
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
                            } else
                            {
                                _localPlayer.myTurn = false;
                                _localGameState._turnPlayerId = id;
                                _localPlayer.importantAnnouncement = "it's " + id + " turn!";
                                AGameBoard.ForceRefreshGameboard(); // force refresh game board so it can have the new turn
                            }
                        }
                    }
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                }
            }
        }
    }
}
