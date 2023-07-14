using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Server.Identifiers
{
#if WITH_SERVER_CODE
    internal class GUIDGenerator
    {
        private static Random random = new Random();

        public static string GenerateUniqueId()
        {
            Guid guid = Guid.NewGuid();
            string randomPart = random.Next(10000, 99999).ToString();

            string uniqueId = guid.ToString("N") + randomPart;
            return uniqueId;
        }
    }
#endif
}
