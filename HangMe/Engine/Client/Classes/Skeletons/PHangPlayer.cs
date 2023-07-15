using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes.Skeletons
{
    internal class PHangPlayer
    {
        public string PlayerId = ""; // -1 = not assigned, any other = assigned
        public string name = ""; // username
        public bool myTurn = false; // my turn to guess a letter
        public string importantAnnouncement = ""; // no announcement, no showy
    }
}
