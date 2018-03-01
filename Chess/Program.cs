﻿using System;

namespace Chess
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
            Board board = new Board();
            board.PrintBoard();
            Player white = new RandomAI(board, Color.White);
            Player black = new RandomAI(board, Color.Black);
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("Making move " + (i * 2));
                board.Move(white.GetMove());
                board.PrintBoard();
                if (board.AllPieces1.FindAll(p => p is King && p.InPlay).Count != 2) break;
                Console.WriteLine("Making move " + (i * 2 + 1));
                board.Move(black.GetMove());
                board.PrintBoard();
                if (board.AllPieces1.FindAll(p => p is King && p.InPlay).Count != 2) break;
            }

            Console.WriteLine("White has taken {0} ms and black has taken {1} ms", white.Stopwatch.ElapsedMilliseconds,
                black.Stopwatch.ElapsedMilliseconds);
        }
    }
}