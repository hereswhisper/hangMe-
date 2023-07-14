using Newtonsoft.Json;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes.Connectors
{
    internal class AGSConnector
    {
        public static async void connectToGS(string host)
        {
            string websocketHost = "ws://" + host + "/";

            while (true)
            {
                // Connect to the WebSocket server
                using (ClientWebSocket webSocket = new ClientWebSocket())
                {
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
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                }
            }
        }
    }
}
