using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Server.Enumerations
{
#if WITH_SERVER_CODE
    enum EHangClientType
    {
        NONE, // nothing, shouldn't call this but if it does it will probably crash at some point
        CLIENT, // You are the client, meaning you have no authority over the game aside from playing
        SERVER, // You are the "server", You aren't the actual server but you can execute server commands
        LOCAL_SERVER, // You are a local server, You are running the actual game server, but you are probably running locally
        DEDICATED_SERVER, // You are a dedicated server, you are running the actual game server, you are just running it from a VPS or something.
        MAX // should never call this. But if it does, you are above everyone (like a lord)
    }
#endif
}
