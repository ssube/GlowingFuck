﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * Grammar:
 *  <   move left
 *  >   move right
 *  +   increment
 *  -   decrement
 *  .   print
 *  ,   read
 *  0   reset
 *  [   start loop
 *  ]   end loop 
 *  {   start of tape
 *  }   end of tape
 *  |   end of program
 *  \0  nop
 */


namespace GlowingFuck
{
    class Program
    {
        static int position = 0;
        static List<byte> tape = new List<byte>(1024);
        static Stack<int> loops = new Stack<int>();

        static void Main(string[] args)
        {
            bool timing = false;

            if (args.Length > 0)
            {
                timing = args.Contains("--time");
            }

            tape.Add(0);
            int inputPos = 0;
            string input = Console.ReadLine();
            while (input != null)
            {
                DateTime start = DateTime.Now;

                while (inputPos < input.Length)
                {
                    switch (input[inputPos])
                    {
                        case '<':
                            --position;
                            if (position < 0)
                            {
                                position = tape.Count - 1;
                            }
                            break;
                        case '>':
                            if (position == (tape.Count - 1))
                            {
                                tape.Add(0);
                            }
                            ++position;
                            break;
                        case '+':
                            ++tape[position];
                            break;
                        case '-':
                            --tape[position];
                            break;
                        case '.':
                            Console.Write((char)tape[position]);
                            break;
                        case ',':
                            tape[position] = (byte)input[++inputPos];
                            break;
                        case '0':
                            tape[position] = 0;
                            break;
                        case '[':
                            loops.Push(inputPos);
                            break;
                        case ']':
                            if (tape[position] > 0)
                            {
                                inputPos = loops.Peek();
                            } else {
                                loops.Pop();
                            }
                            break;
                        case '{':
                            position = 0;
                            break;
                        case '}':
                            position = tape.Count - 1;
                            break;
                       case '|':
                            return;
                    }

                    ++inputPos;
                }

                DateTime end = DateTime.Now;

                Console.WriteLine();

                if (timing)
                {
                    TimeSpan duration = end - start;
                    Console.WriteLine("Runtime: {0}", duration.TotalSeconds);
                }

                input = Console.ReadLine();
                inputPos = 0;
            }
        }
    }
}
