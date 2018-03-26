using System;
using System.Linq;
using System.Threading;
using Chess.Basic;
using Chess.Player;
using Chess.Player.AI;
using Chess.Utils;

namespace Chess
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Printer.Start();
            Printer.AddToPrinter("Hello World");
            var board = new Board();
            board.PrintBoardToPrinter();
            PlayerAbstract white = new RandomAI(board, Color.White);
            PlayerAbstract black = new AlphaBetaSimple(board, Color.Black);
            var gamesResult = GameLoop.Games(board, white, black, 1, boardPrint: true);
            var count = gamesResult.Length;
            Printer.AddToPrinter("Number of games played: " + count);
            var tmp = gamesResult.Count(a => a.Item1 == Color.Black);
            Printer.AddToPrinter(tmp + " wins for black (" + tmp / (double) count * 100d + "%) (" + black.Name + ")");
            tmp = gamesResult.Count(a => a.Item1 == Color.White);
            Printer.AddToPrinter(tmp + " wins for white (" + tmp / (double) count * 100d + "%) (" + white.Name + ")");
            tmp = gamesResult.Count(a => a.Item1 == Color.NoColor);
            Printer.AddToPrinter(tmp + " draws (" + tmp / (double) count * 100d + "%)");
            Printer.AddToPrinter(gamesResult.Average(a => a.Item2) + " average number of turns needed");
            Printer.AddToPrinter(
                gamesResult.Average(a => a.Item3) + "ms need on average for white (" + white.Name + ")");
            Printer.AddToPrinter(
                gamesResult.Average(a => a.Item4) + "ms need on average for black (" + black.Name + ")");

            Thread.Sleep(1000);
            Console.ReadLine();
        }
    }
}