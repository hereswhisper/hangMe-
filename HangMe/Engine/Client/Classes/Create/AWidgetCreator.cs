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
        public async Task<int> createWidgetAsync(string name, string arg = "")
        {
            ADisclaimerScreen _aDisclaimerScreen = new ADisclaimerScreen();
            AStartMenuScreen _aStartMenuScreen = new AStartMenuScreen();
            ALoginScreen _aLoginScreen = new ALoginScreen();
            AConnectIPScreen _aConnectIpScreen = new AConnectIPScreen();
            AConnectionScreen _aConnectionScreen = new AConnectionScreen();
            AGameBoard _aGameBoard = new AGameBoard();
            AKickedScreen _aKickedScreen = new AKickedScreen();

            bool createdWidget = false;
            switch (name)
            {
                case "disclaimer":
                    
                    await _aDisclaimerScreen.showContents();
                    createdWidget = true;
                    break;
                case "startmenu":
                    await _aStartMenuScreen.showContents();
                    createdWidget = true;
                    break;
                case "login":
                    await _aLoginScreen.showContents();
                    createdWidget = true;
                    break;
                case "connectip":
                    await _aConnectIpScreen.showContents();
                    createdWidget = true;
                    break;
                case "connectionscreen":
                    await _aConnectionScreen.showContents(arg);
                    createdWidget = true;
                    break;
                case "gameboard":
                    await _aGameBoard.showContents();
                    createdWidget = true;
                    break;
                case "kickscreen":
                    await _aKickedScreen.showContents(arg);
                    createdWidget = true;
                    break;
            }

            if (!createdWidget) return 1; // failure (not good btw)

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
