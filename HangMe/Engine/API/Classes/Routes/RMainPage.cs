using HangMe.Engine.API.Classes.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.API.Classes.Routes
{
#if WITH_SERVER_CODE
    internal class RMainPage
    {
        public static async void ShowContent(HttpListenerContext context)
        {
            string responseString = "Welcome to the homepage!";

            byte[] buffer = Encoding.UTF8.GetBytes(responseString);

            context.Response.ContentType = EHangHTTPContentTypes.PlainText;
            context.Response.ContentLength64 = buffer.Length;
            await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            context.Response.Close();
        }
    }
#endif
}
