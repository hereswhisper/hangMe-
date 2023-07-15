using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Server.Enumerations
{
#if WITH_SERVER_CODE
    enum EHangGameVisibility
    {
        OPEN,
        LOCKED,
        PASSWORD_PROTECTED,
        NONE // should never call this
    }
#endif
}
