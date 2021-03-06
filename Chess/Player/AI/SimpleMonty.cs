﻿using System;
using System.Linq;
using Chess.Basic;

namespace Chess.Player.AI
{
    public class SimpleMonty : PlayerAbstract
    {
        private readonly BoardEval _boardEval;
        private readonly int _MontyGames;

        public SimpleMonty(SimpleMonty player, Board board) : base(player, board)
        {
            _boardEval = player._boardEval;
        }

        public SimpleMonty(Board board, Color color, int numberOfSimsPerMove = 50) : base(board, color, "Simple Monty")
        {
            _MontyGames = numberOfSimsPerMove;
        }

        public override Tuple<Point2D, Point2D> GetMove()
        {
            Stopwatch.Start();

            var pieces = Board.GetAllColored(Color);
            var possibleMoves = Board.GetAllPossibleMoves();
            possibleMoves = possibleMoves.FindAll(tuple => tuple.Item1.Color == Color);

            Tuple<Point2D, Point2D> bestMove = null;
            //use any negative large number
            var bestValue = 0d;

            for (var i = 0; i < possibleMoves.Count; i++)
            {
                var newGameMove = possibleMoves[i];
                var tmpBoard = Board.Clone();

                tmpBoard.Move(newGameMove.Item1, newGameMove.Item2);
                //take the negative as AI plays as black
                var games = GameLoop.Games(tmpBoard, new RandomAI(tmpBoard, Color.White),
                    new RandomAI(tmpBoard, Color.Black), _MontyGames);
                var wins = games.Count(a => a.Item1 == Color);
                var currentVal = wins / (double) _MontyGames;
                if (currentVal > bestValue)
                {
                    bestValue = currentVal;
                    bestMove = new Tuple<Point2D, Point2D>(newGameMove.Item1.PositionPoint2D, newGameMove.Item2);
                }
            }

            _stopwatch.Stop();
            return bestMove;
        }

        public override PlayerAbstract Clone(Board board)
        {
            return new SimpleMonty(this, board);
        }
    }
}