using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chess.AI;

namespace Chess
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Printer.Start();
            Printer.AddToPrinter("Hello World");
            Board board = new Board();
            board.PrintBoardToPrinter();
            Player white = new Human(board, Color.White);
            Player black = new GreedyNPly(board, Color.Black, 5);
            Tuple<Color, int, long, long>[] gamesResult = GameLoop.Games(board, white, black, 1, boardPrint: true);
            var count = gamesResult.Length;
            Printer.AddToPrinter("Number of games played: " + count);
            var tmp = gamesResult.Count(a => a.Item1 == Color.Black);
            Printer.AddToPrinter( tmp + " wins for black (" +(tmp/(double)count*100d)+"%) ("+black.Name+")");
            tmp = gamesResult.Count(a => a.Item1 == Color.White);
            Printer.AddToPrinter( tmp + " wins for white (" +(tmp/(double)count*100d)+"%) ("+white.Name+")");
            tmp = gamesResult.Count(a => a.Item1 == Color.NoColor);
            Printer.AddToPrinter( tmp + " draws (" +(tmp/(double)count*100d)+"%)");
            Printer.AddToPrinter(gamesResult.Average(a => a.Item2) + " average number of turns needed");
            Printer.AddToPrinter(gamesResult.Average(a => a.Item3) + "ms need on average for white ("+white.Name+")");
            Printer.AddToPrinter(gamesResult.Average(a => a.Item4) + "ms need on average for black ("+black.Name+")");

            Thread.Sleep(1000);
        }
    }
}