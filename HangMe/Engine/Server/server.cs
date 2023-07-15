using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Net.Sockets;
using System.Security.Policy;
using HangMe.Engine.Client.Classes.Skeletons;
using HangMe.Engine.Server.Enumerations;
using System.Net.Mail;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HangMe.Engine.Server.Challenge;
using System.Xml.Linq;
using System.Collections.Concurrent;

namespace HangMe.Engine.Server
{
#if WITH_SERVER_CODE
    internal class server
    {
        private int _port;
        private string _host;
        private bool _isLocal;
        private bool _isDedicated;
        private HttpListener _httpListener;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _RotateTurns = false;
        private static ConcurrentDictionary<string, WebSocket> _clients = new ConcurrentDictionary<string, WebSocket>();
        public static List<string> words = new List<string>
{
    "apple",
    "banana",
    "cherry",
    "orange"
};
        private Server.GameState.AHangGameState _gameState = new Server.GameState.AHangGameState(words);

        public server(int port = 7777, string host = "http://localhost:", bool isLocal = true, bool isDedicated = true) { 
            _port = port;
            _isLocal = isLocal;
            _isDedicated = isDedicated;
            _host = host;
        }

        public async Task startServer()
        {
            string url = _host + _port + "/";
            // Create an HTTP listener
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add(url);
            _httpListener.Start();

            Console.WriteLine("WebSocket server started");

            _cancellationTokenSource = new CancellationTokenSource();

            // Start a separate task for handling user input
            Task userInputTask = Task.Run(ProcessUserInput);

            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                // Await incoming HTTP requests
                HttpListenerContext context = await _httpListener.GetContextAsync();

                if (context.Request.IsWebSocketRequest)
                {
                    // Accept the WebSocket connection
                    HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);

                    // Handle the WebSocket connection
                    HandleWebSocketConnection(webSocketContext.WebSocket);
                }
                else
                {
                    // Handle non-WebSocket requests here, if needed
                }
            }

            _httpListener.Close();
            // Wait for the userInputTask to complete before exiting the StartServer method
            await userInputTask;
        }

        private async Task ProcessUserInput()
        {
            while (true)
            {
                Console.Write("[whisper@hangme]> ");
                string input = Console.ReadLine();

                if (input == "exit")
                {
                    // Cancel the server loop by triggering cancellation
                    _cancellationTokenSource.Cancel();
                    break;
                } else if (input == "startgame")
                {
                    _gameState.selectWord(); // selects a word
                    _gameState.RotateTurns(); // Rotate turns
                    _RotateTurns = true; // time to rotate
                    _gameState._gameStarted = true; // notify server game has started.
                }

                // Process the command
                //Console.WriteLine("Command received: " + input);
                // Add your command processing logic here
            }
        }

        private async void NewTurn(WebSocket websocket)
        {
            _gameState.RotateTurns(); // rotate turns

            var data = new
            {
                Command = "ServerRotateTurns",
                UserId = _gameState._players[_gameState._lastPlayerTurn].userid
            };

            string sjson = JsonConvert.SerializeObject(data);
            byte[] messageBytes = Encoding.UTF8.GetBytes(sjson);
            foreach (WebSocket clientWebSocket in _clients.Values)
            {
                await clientWebSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            //await websocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
            return;
        }

        private async void HandleWebSocketConnection(WebSocket webSocket)
        {
            byte[] buffer = new byte[1024];

            // Generate a unique client ID for identification
            string clientId = Guid.NewGuid().ToString();

            // Add the client websocket to the dictionary
            _clients.TryAdd(clientId, webSocket);

            while (webSocket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if(_RotateTurns)
                {
                    var data = new
                    {
                        Command = "ServerRotateTurns",
                        UserId = _gameState._players[_gameState._lastPlayerTurn].userid
                    };

                    string json = JsonConvert.SerializeObject(data);
                    byte[] messageBytes = Encoding.UTF8.GetBytes(json);

                    foreach (WebSocket clientWebSocket in _clients.Values)
                    {
                        await clientWebSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    //await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                    _RotateTurns = false;
                }

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    // Handle received text message
                    string receivedMessage = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine("Received: " + receivedMessage);

                    if(receivedMessage != null)
                    {
                        if(receivedMessage == EHangServerFunctions.ServerNotifyUserLogon)
                        {
                            Console.WriteLine("[hangMe Websocket Server INFO]: Player has connected, sending GameState");
                            _gameState._playerCount = _gameState._playerCount + 1; // notify of user logon
                        }
                        else if (receivedMessage == EHangServerFunctions.ClientRequestGameState)
                        {
                            var gameStateData = new
                            {
                                guessedletters = _gameState._guessedLetters,
                                gameId = _gameState._gameId,
                                //players = _gameState._players,
                                playerCount = _gameState._playerCount,
                                correctLetters = _gameState._correctLetters,
                                selectedWord = _gameState._currentWord,
                                nextCommand = EHangServerFunctions.ClientAcknowledgment
                            };

                            string json = JsonConvert.SerializeObject(gameStateData);
                            byte[] messageBytes = Encoding.UTF8.GetBytes(json);
                            await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                            Console.WriteLine("[hangMe Server INFO]: Sent GameState to Client");
                        }
                        else if (receivedMessage == EHangServerFunctions.ClientRegister)
                        {
                            Server.PlayerState.PHangPlayer _newPlayer = new Server.PlayerState.PHangPlayer("wow", Identifiers.UserIdGeneration.GenerateUserId());
                            _gameState._players.Add(_newPlayer); // add new Player into mix.

                            var newPlayerInfo = new
                            {
                                Name = _newPlayer.name,
                                UserId = _newPlayer.userid,
                                Command = "ServerRegisterInfo"
                            };

                            string json = JsonConvert.SerializeObject(newPlayerInfo);
                            byte[] messageBytes = Encoding.UTF8.GetBytes(json);
                            await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);

                            Console.WriteLine("[hangMe Server INFO]: Registered client as new player under id: " + _newPlayer.userid);
                        }
                        else if (receivedMessage.StartsWith("{") ||  receivedMessage.StartsWith("[")) {
                            // assume it's a json

                            var json = JObject.Parse(receivedMessage);

                            string command = json["Command"]?.ToString();

                            if(command == EHangServerFunctions.ClientAcknowledgment)
                            {
                                string size = json["Size"]?.ToObject<string>();

                                if(size == ChallengeConfig.ACKSize)
                                {
                                    byte[] responseBytes = System.Text.Encoding.UTF8.GetBytes("OK");
                                    await webSocket.SendAsync(new ArraySegment<byte>(responseBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                                } else
                                {
                                    byte[] responseBytes = System.Text.Encoding.UTF8.GetBytes("NO");
                                    await webSocket.SendAsync(new ArraySegment<byte>(responseBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                                }
                            }

                            if(command == "ClientWhoPlayerTurn")
                            {
                                if (!_gameState._gameStarted) return;

                                var data = new
                                {
                                    Command = "ClientWhoPlayerTurn",
                                    UserId = _gameState._players[_gameState._lastPlayerTurn].userid
                                };

                                string sjson = JsonConvert.SerializeObject(data);
                                byte[] messageBytes = Encoding.UTF8.GetBytes(sjson);
                                await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                            }

                            // {"Command":"ClientGuess","UserId":"281a0cbd-8664-4a76-bf85-eaf3272e62c8","Size":"a"}
                            if (command == "ClientGuess")
                            {
                                string UserId = json["UserId"]?.ToString();
                                string letter = json["Size"]?.ToString();

                                if (_gameState._currentWord.Contains(letter))
                                {
                                    // mark it as there is one
                                    _gameState._correctLetters.Add(letter);
                                    var data = new
                                    {
                                        Command = "ServerEndTurn", // basically suspends user input from that user.
                                        UserID = UserId,
                                        Letter = letter,
                                    };

                                    await Task.Run(() => NewTurn(webSocket)); // rotate turns

                                    string sjson = JsonConvert.SerializeObject(data);
                                    byte[] messageBytes = Encoding.UTF8.GetBytes(sjson);
                                    await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);

                                    
                                } else
                                {
                                    _gameState._guessedLetters.Add(letter); // continue on

                                    var data = new
                                    {
                                        Command = "ServerEndTurn", // basically suspends user input from that user.
                                        UserID = UserId,
                                        Letter = letter,
                                    };

                                    await Task.Run(() => NewTurn(webSocket)); // rotate turns

                                    string sjson = JsonConvert.SerializeObject(data);
                                    byte[] messageBytes = Encoding.UTF8.GetBytes(sjson);
                                    await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);

                                }
                            }  


                        } else if (receivedMessage == EHangServerFunctions.ClientRequestNewGameState)
                        {
                            var gameStateData = new
                            {
                                guessedletters = _gameState._guessedLetters,
                                gameId = _gameState._gameId,
                                //players = _gameState._players,
                                playerCount = _gameState._playerCount,
                                correctLetters = _gameState._correctLetters,
                                selectedWord = _gameState._currentWord,
                                Command = "ClientRequestNewGameState"
                            };

                            string json = JsonConvert.SerializeObject(gameStateData);
                            byte[] messageBytes = Encoding.UTF8.GetBytes(json);
                            await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                        } 
                    }

                    // Send a response back to the client
                    //string responseMessage = "Server received: " + receivedMessage;
                    //byte[] responseBytes = System.Text.Encoding.UTF8.GetBytes(responseMessage);
                    //await webSocket.SendAsync(new ArraySegment<byte>(responseBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    // Remove the client websocket from the dictionary
                    _clients.TryRemove(clientId, out _);

                    // Handle WebSocket close message
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                }
            }
        }
    }
#endif
}
