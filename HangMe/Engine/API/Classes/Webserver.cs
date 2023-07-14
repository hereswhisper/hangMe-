using HangMe.Engine.API.Classes.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.API.Classes
{
#if WITH_SERVER_CODE
    internal class WebServer
    {
        private readonly HttpListener listener;

        public WebServer(string[] prefixes)
        {
            listener = new HttpListener();
            foreach (string prefix in prefixes)
            {
                listener.Prefixes.Add(prefix);
            }
        }

        public async Task Start()
        {
            listener.Start();
            Console.WriteLine("Web server started.");

            while (true)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                HandleRequest(context);
            }
        }

        public void Stop()
        {
            listener.Stop();
            listener.Close();
            Console.WriteLine("Web server stopped.");
        }

        private async void HandleRequest(HttpListenerContext context)
        {
            bool content = false;

            // handle routing
            if(context.Request.Url.AbsolutePath == "/")
            {
                content = await Routes.RMainPage.ShowContent(context);
            }
            else if (context.Request.Url.AbsolutePath == "/register")
            {
                content = await Routes.RRegisterPage.ShowContent(context);
            }

            if (content == true) return;

            //string responseString = "404: Endpoint not found";

            // Create a sample JSON response
            var data = new { Error = "404", ApplicationId = "com.hangMe.game.api.service", Message = "This endpoint was not found. Or this method type cannot be used on this endpoint." };
            string jsonResponse = Newtonsoft.Json.JsonConvert.SerializeObject(data);

            byte[] buffer = Encoding.UTF8.GetBytes(jsonResponse);
            context.Response.ContentType = EHangHTTPContentTypes.JSON;
            context.Response.ContentLength64 = buffer.Length;
            await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            context.Response.Close();
        }
    }
#endif
}
