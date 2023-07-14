using HangMe.Engine.Client.Classes.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Client
{
    internal class client
    {
        public static void startClient()
        {
            AWidgetCreator creator = new AWidgetCreator();
            creator.createWidget("disclaimer");
        }
    }
}
