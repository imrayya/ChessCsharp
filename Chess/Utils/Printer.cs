using System;
using System.Collections.Concurrent;
using System.Timers;

namespace Chess.Utils
{
    public class Printer
    {
        private static readonly ConcurrentQueue<string> _stack = new ConcurrentQueue<string>();

        public static void AddToPrinter(string message)
        {
            _stack.Enqueue(message);
        }

        public static void Start()
        {
            var timer = new Timer(100);
            timer.Elapsed += delegate
            {
                while (!_stack.IsEmpty)
                {
                    string message;
                    while (!_stack.TryDequeue(out message))
                    {
                    }

                    Console.WriteLine(message);
                }
            };
            timer.Enabled = true;
        }
    }
}