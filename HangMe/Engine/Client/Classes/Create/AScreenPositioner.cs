using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes.Create
{
    /// <summary>
    /// This going to be really needed for the game board
    /// </summary>
    internal class AScreenPositioner
    {
        public static void MiddleCenterScreen(string message)
        {
            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;

            int messageLength = message.Length;
            int centerWidth = (screenWidth - messageLength) / 2;
            int centerHeight = screenHeight / 2;

            Console.SetCursorPosition(centerWidth, centerHeight);
            Console.WriteLine(message);
        }

        public static void CenterRightScreen(string message)
        {
            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;

            int messageLength = message.Length;
            int centerHeight = screenHeight / 2;

            int centerRightWidth = screenWidth - messageLength;

            Console.SetCursorPosition(centerRightWidth, centerHeight);
            Console.WriteLine(message);
        }

        public static void CenterLeftScreen(string message)
        {
            int screenHeight = Console.WindowHeight;

            int centerHeight = screenHeight / 2;

            Console.SetCursorPosition(0, centerHeight);
            Console.WriteLine(message);
        }

        public static void BottomRightScreen(string message)
        {
            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;

            int messageLength = message.Length;
            int messageHeight = 1; // Assuming single line message

            int bottomRightWidth = screenWidth - messageLength;
            int bottomRightHeight = screenHeight - messageHeight;

            Console.SetCursorPosition(bottomRightWidth, bottomRightHeight);
            Console.WriteLine(message);
        }

        public static void BottomCenterScreen(string message, bool smoothWrite = false, int amountToSleep = 300)
        {
            if (message == null) return; // no message nessecary

            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;

            int messageLength = message.Length;
            int messageHeight = 1; // Assuming single line message

            int centerWidth = (screenWidth - messageLength) / 2;
            int bottomCenterHeight = screenHeight - messageHeight;

            Console.SetCursorPosition(centerWidth, bottomCenterHeight);
            
            if(!smoothWrite)
            {
                Console.WriteLine(message);
                return;
            }

            for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i]);
                Thread.Sleep(amountToSleep);
            }
        }

        public static void BottomLeftScreen(string message)
        {
            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;

            int messageHeight = 1; // Assuming single line message

            int bottomLeftHeight = screenHeight - messageHeight;

            Console.SetCursorPosition(0, bottomLeftHeight);
            Console.WriteLine(message);
        }

        public static void TopLeftScreen(string message)
        {
            Console.SetCursorPosition(0, 0); // most simple
            Console.WriteLine(message);
        }

        public static void TopCenterScreen(string message)
        {
            int screenWidth = Console.WindowWidth;

            int centerWidth = (screenWidth - message.Length) / 2;

            Console.SetCursorPosition(centerWidth, 0);
            Console.WriteLine(message);
        }

        public static void TopRightScreen(string message)
        {
            int screenWidth = Console.WindowWidth;

            int messageLength = message.Length;

            int topRightWidth = screenWidth - messageLength;

            Console.SetCursorPosition(topRightWidth, 0);
            Console.WriteLine(message);
        }
    }
}
