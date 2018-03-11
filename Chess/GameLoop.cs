using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Chess
{
    public static class GameLoop
    {
        public static Tuple<Color, int, long, long>[] Games(Board board, Player white, Player black, int numberOfGames,
            bool debugPrint = false, bool boardPrint = false)
        {
            List<Task<Tuple<Color, int, long, long>>> tasks = new List<Task<Tuple<Color, int, long, long>>>();
            for (int i = 0; i < numberOfGames; i++)
            {
                var i1 = i;
                var bC = board.Clone();
                var wC = white.Clone(bC);
                var blC = black.Clone(bC);

                tasks.Add(new Task<Tuple<Color, int, long, long>>(() =>
                    Game(board: bC, white: wC, black: blC, printBoard: boardPrint, printDebug: debugPrint)));
            }

            tasks.ForEach(task => task.Start());
            while (!tasks.All(task => task.IsCompleted))
            {
            }

            return tasks.ConvertAll(task => task.Result).ToArray();
        }



        public static Tuple<Color, int, long, long> Game(Board board, Player white, Player black,
            ChessListener listener)
        {
            int i = 0;
            while (true)
            {
                board.Move(white.GetMove());
                listener.MoveEvent(board.Clone());
                if (board.AllPieces1.FindAll(p => p is King && p.InPlay).Count != 2) break; //if checkmate

                //Check for draw
                if (Rules.CheckDraw(board))
                {
                    return new Tuple<Color, int, long, long>(Color.NoColor, i, white.Stopwatch.ElapsedMilliseconds,
                        black.Stopwatch.ElapsedMilliseconds);
                }
                board.Move(black.GetMove());
                listener.MoveEvent(board.Clone());
                if (board.AllPieces1.FindAll(p => p is King && p.InPlay).Count != 2) break; //if checkmate 
                i++;
            }


            Color winner = board.AllPieces1.Find(king => king is King && king.InPlay).Color;

            return new Tuple<Color, int, long, long>(winner, i, white.Stopwatch.ElapsedMilliseconds,
                black.Stopwatch.ElapsedMilliseconds);
        }

        public static Tuple<Color, int, long, long> Game(Board board, Player white, Player black,
            bool printBoard = false,
            bool printDebug = false, bool printGameBNumber = false, int gameNumber = 0)
        {
            int i = 0;
            while (true)
            {
                if (printDebug)
                    Console.WriteLine("Making move " + (i));
                board.Move(white.GetMove());
                i++;
                if (printBoard)
                    board.PrintBoardToPrinter();
                if (printGameBNumber)
                    Printer.AddToPrinter(gameNumber + "");
                if (board.AllPieces1.FindAll(p => p is King && p.InPlay).Count != 2) break; //if checkmate
                if (printDebug)
                    Console.WriteLine("Making move " + (i));
                //Check for draw
                if (Rules.CheckDraw(board))
                {
                    if (printDebug)
                        Console.WriteLine("White has taken {0} ms and black has taken {1} ms",
                            white.Stopwatch.ElapsedMilliseconds,
                            black.Stopwatch.ElapsedMilliseconds);
                    return new Tuple<Color, int, long, long>(Color.NoColor, i, white.Stopwatch.ElapsedMilliseconds,
                        black.Stopwatch.ElapsedMilliseconds);
                }

                board.Move(black.GetMove());
                if (printBoard)
                    board.PrintBoardToPrinter();
                if (printGameBNumber)
                    Printer.AddToPrinter(gameNumber + "");
                if (board.AllPieces1.FindAll(p => p is King && p.InPlay).Count != 2) break; //if checkmate 
                i++;
            }

            if (printDebug)
                Console.WriteLine("White has taken {0} ms and black has taken {1} ms",
                    white.Stopwatch.ElapsedMilliseconds,
                    black.Stopwatch.ElapsedMilliseconds);
            Color winner = board.AllPieces1.Find(king => king is King && king.InPlay).Color;

            return new Tuple<Color, int, long, long>(winner, i, white.Stopwatch.ElapsedMilliseconds,
                black.Stopwatch.ElapsedMilliseconds);
        }
    }
}