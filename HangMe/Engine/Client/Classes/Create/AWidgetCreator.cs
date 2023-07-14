using HangMe.Engine.Client.Classes.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HangMe.Engine.Client.Classes.Create
{
    internal class AWidgetCreator
    {
        /// <summary>
        /// Creates a Widget onto the canvas
        /// </summary>
        /// <param name="name"></param>
        public async Task<int> createWidgetAsync(string name)
        {
            ADisclaimerScreen _aDisclaimerScreen = new ADisclaimerScreen();

            bool createdWidget = false;
            switch (name)
            {
                case "disclaimer":
                    
                    await _aDisclaimerScreen.showContents();
                    createdWidget = true;
                    break;
            }

            if (!createdWidget) return 1; // failure

            return 0; // success
        }

        /// <summary>
        /// Wipes all Widgets from Canvas
        /// </summary>
        public int wipeAllWidgets(bool dirtyClean = false)
        {
            if(dirtyClean)
            {
                Console.Clear(); // simple fix
            }
            return 0;
        }
    }
}
