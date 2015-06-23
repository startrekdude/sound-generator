using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoundGenerator.CommandREPL
{
    /* Template copied From: http://stackoverflow.com/questions/1923323/console-animations */
    public class ConsoleSpinner
    {
        int counter;
        string[] sequence;

        public ConsoleSpinner()
        {
            counter = 0;
            sequence = new string[] { "V", "<", "^", ">" };
        }

        public void Turn()
        {
            counter++;

            if (counter >= sequence.Length)
                counter = 0;

            Console.Write(sequence[counter]);
            Console.SetCursorPosition(Console.CursorLeft - sequence[counter].Length, Console.CursorTop);
        }
    }
}
