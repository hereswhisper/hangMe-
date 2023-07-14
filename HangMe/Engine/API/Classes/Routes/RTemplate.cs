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
    /// <summary>
    /// DONT SERVE THIS AS A ACTUAL PAGE. THIS IS FOR THE ENGINE AND FOR TEMPLATE.
    /// </summary>
    internal class RTemplate
    {
        public static async void ShowContent(HttpListenerContext context) // MAKE SURE THE FUNCTION IS ALWAYS CALLED "ShowContent".
        {
            string responseString = "you've displayed text!"; // Make sure this variable is named 'responseString' at ALL TIMES. No inconsistancies.

            byte[] buffer = Encoding.UTF8.GetBytes(responseString); // get the buffer for responseString.

            context.Response.ContentType = EHangHTTPContentTypes.PlainText; // Content type for the page. (USE EHangHTTPContentTypes AT ALL TIMES.)
            context.Response.ContentLength64 = buffer.Length; // Content Length in Base64 (basically buffer.Length)
            await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length); // Write it & send off to the recipient.
            context.Response.Close(); // close it (no memory leaks please.)
        }
    }
#endif
}
