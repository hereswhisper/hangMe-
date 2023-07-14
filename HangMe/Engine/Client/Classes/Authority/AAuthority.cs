using HangMe.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes.Authority
{
    internal class AAuthority
    {
        public static bool isServer()
        {
#if WITH_SERVER_CODE
            return true;
#else
            return false;
#endif
        }
    }
}
