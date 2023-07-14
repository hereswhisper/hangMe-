using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.API.Classes.Enumerations
{
#if WITH_SERVER_CODE
    public struct EHangHTTPMethod
    {
        public static string GET = "GET";
        public static string POST = "POST";
        public static string PUT = "PUT";
        public static string DELETE = "DELETE";

        public static string getMethodFromString(string method)
        {
            string mToBigString = method.ToUpper();
            switch(mToBigString)
            {
                case "GET":
                    return EHangHTTPMethod.GET;
                case "POST":
                    return EHangHTTPMethod.POST;
                case "PUT":
                    return EHangHTTPMethod.PUT;
                case "DELETE":
                    return EHangHTTPMethod.DELETE;
                default:
                    return EHangHTTPMethod.GET;
            }
        }
    }
#endif
}
