using HangMe.Engine.API.Classes.Enumerations;
using HangMe.Engine.Server.GameState;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.API.Classes.Routing
{
    internal class ARoute
    {
        public static bool isMatch(HttpListenerContext context, string expectedMethod)
        {
            if (context.Request.HttpMethod == EHangHTTPMethod.getMethodFromString(expectedMethod)) return true; // They match!

            return false; // oopsy, someone being bad boy
        }

        public static NameValueCollection getQuery(HttpListenerContext context)
        {
            return context.Request.QueryString;
        }
    }
}
