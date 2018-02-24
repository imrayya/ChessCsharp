using System;

namespace Chess
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
            Board board = new Board();
            board.PrintBoard();
            Console.ReadLine();
        }
    }
}