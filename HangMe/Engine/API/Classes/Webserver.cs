using HangMe.Engine.API.Classes.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.API.Classes
{
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
            string responseString = "There is no webpage named that..?";

            if (context.Request.HttpMethod == EHangHTTPMethod.GET && context.Request.Url.AbsolutePath == "/")
            {
                Routes.RMainPage.ShowContent(context);
                return;
                //responseString = "Welcome to the homepage!";
            }
            else if (context.Request.HttpMethod == EHangHTTPMethod.GET && context.Request.Url.AbsolutePath == "/api/data")
            {
                responseString = "GET Endpoint: This is the data endpoint.";
            }
            else if (context.Request.HttpMethod == EHangHTTPMethod.POST && context.Request.Url.AbsolutePath == "/api/data")
            {
                responseString = "POST Endpoint: Data successfully submitted.";
            }

            byte[] buffer = Encoding.UTF8.GetBytes(responseString);

            context.Response.ContentType = "text/plain";
            context.Response.ContentLength64 = buffer.Length;
            await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            context.Response.Close();
        }
    }
}
