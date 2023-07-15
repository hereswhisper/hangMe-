using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HangMe.Engine.Common
{
    internal class AConsoleUtilities
    {
        public static void SmoothWrite(string textToSmooth, int timeToSleep = 300)
        {
            for(int i = 0; i < textToSmooth.Length; i++)
            {
                Console.WriteLine(textToSmooth[i]);
                Thread.Sleep(timeToSleep);
            }
        }

        /// <summary>
        /// Request User Input (Keyboard)
        /// </summary>
        /// <returns></returns>
        public static string UserInput()
        {
            var e = Console.ReadLine();
            return e;
        }

        /// <summary>
        /// Suspends Thread (make sure the TICK is on a different thread)
        /// </summary>
        /// <param name="time"></param>
        public static void Sleep(int time)
        {
            Thread.Sleep(time);
            return; // return to normal execution
        }

        public static void ShowMessageBox(string message)
        {
            int width = Console.WindowWidth - 4;
            string horizontalLine = new string('-', width);

            Console.WriteLine(horizontalLine);
            Console.WriteLine($"| {message.PadRight(width)} |");
            Console.WriteLine(horizontalLine);
        }
    }
}
