using HangMe.Engine.API.Classes.Enumerations;
using HangMe.Engine.API.Classes.Routing;
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
        public static async Task<bool> ShowContent(HttpListenerContext context) // MAKE SURE THE FUNCTION IS ALWAYS CALLED "ShowContent". (AND ALWAYS MAKE IT A BOOL RETURN VALUE)
        {
            string method = EHangHTTPMethod.GET; // What Method you will be accepting

            bool isMatch = ARoute.isMatch(context, method); // Testing if the recipient's method is correct with yours

            if (!isMatch) return false; // If it doesn't match, return false so our webserver knows it was not handled

            string responseString = "you've displayed text!"; // Make sure this variable is named 'responseString' at ALL TIMES. No inconsistancies.

            byte[] buffer = Encoding.UTF8.GetBytes(responseString); // get the buffer for responseString.

            context.Response.ContentType = EHangHTTPContentTypes.PlainText; // Content type for the page. (USE EHangHTTPContentTypes AT ALL TIMES.)
            context.Response.ContentLength64 = buffer.Length; // Content Length in Base64 (basically buffer.Length)
            await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length); // Write it & send off to the recipient.
            context.Response.Close(); // close it (no memory leaks please.)
            return true; // content was sent to recipient.
        }


        /*
         GET QUERY STRING:
            NameValueCollection queryString = ARoute.getQuery(context);

            string name = queryString.Get("a");

            Console.WriteLine(name);
         */
    }
#endif
}
