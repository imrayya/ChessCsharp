using System;
using System.Collections.Concurrent;
using System.Timers;

namespace Chess.Utils
{
    public class Printer
    {
        private static ConcurrentQueue<string> _stack = new ConcurrentQueue<string>();

        public static void AddToPrinter(string message)
        {
            _stack.Enqueue(message);
        }

        public static void Start()
        {
            Timer timer = new Timer(100);
            timer.Elapsed += delegate(object sender, ElapsedEventArgs args)
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