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
        }

        private async void HandleWebSocketConnection(WebSocket webSocket)
        {
            byte[] buffer = new byte[1024];

            while (webSocket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    // Handle received text message
                    string receivedMessage = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine("Received: " + receivedMessage);

                    // Send a response back to the client
                    string responseMessage = "Server received: " + receivedMessage;
                    byte[] responseBytes = System.Text.Encoding.UTF8.GetBytes(responseMessage);
                    await webSocket.SendAsync(new ArraySegment<byte>(responseBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    // Handle WebSocket close message
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                }
            }
        }
    }
#endif
}
