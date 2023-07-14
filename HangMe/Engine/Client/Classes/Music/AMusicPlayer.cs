using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HangMe.Engine.Client.Classes.Music
{
    internal class AMusicPlayer
    {
        private int[,] _notes;
        private int _tempo;

        public AMusicPlayer(int[,] notes, int tempo)
        {
            this._notes = notes;
            this._tempo = tempo;
        }

        public void Play()
        {
            for (int i = 0; i < _notes.GetLength(0); i++)
            {
                int frequency = _notes[i, 0];
                int duration = _notes[i, 1];

                if (frequency == 0)
                {
                    Thread.Sleep(duration);
                }
                else
                {
                    Console.Beep(frequency, duration);
                    Thread.Sleep(duration);
                }
            }

        }
    }
}
