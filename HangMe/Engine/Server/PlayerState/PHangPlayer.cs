using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Server.PlayerState
{
#if WITH_SERVER_CODE
    internal class PHangPlayer
    {
        public string name = "";
        public string userid = "";
        /// <summary>
        /// User Class to know who is playing and is available to be chosen
        /// </summary>
        /// <param name="name"></param>
        /// <param name="userid"></param>
        /// <param name="isAccount"></param>
        public PHangPlayer(string username, string uuserid, bool isAccount = false)
        {
            username = name;
            userid = uuserid;
            // isAccount will be added when I feel like to finish API
        }
    }
#endif
}
