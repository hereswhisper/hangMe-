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
        /// Suspends Thread (make sure the TICK is on a different thread)
        /// </summary>
        /// <param name="time"></param>
        public static void Sleep(int time)
        {
            Thread.Sleep(time);
            return; // return to normal execution
        }
    }
}
