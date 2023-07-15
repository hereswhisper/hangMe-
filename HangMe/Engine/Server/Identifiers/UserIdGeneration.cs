using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Server.Identifiers
{
    internal class UserIdGeneration
    {
        public static string GenerateUserId()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }
    }
}
