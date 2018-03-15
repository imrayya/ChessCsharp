using System;
using System.Collections.Generic;
using Chess.Basic;
using Chess.Basic.Pieces;
using Chess.Utils;

namespace Chess.Player.AI
{
    public class GreedyNPly : PlayerAbstract
    {
        private RandomAI _randomAi;

        private int _n;


        public GreedyNPly(GreedyNPly player, Board board) : base(player, board)
        {
            _randomAi = new RandomAI(board, player.Color);
            _boardEval = player._boardEval;
            _n = player._n;
        }

        private BoardEval _boardEval;

        public GreedyNPly(Board board, Color color, BoardEval boardEval = BoardEval.Simple, int n = 3) : base(board,
            color, "Greedy " + n + " ply AI")
        {
            if (n < 0) throw new ArgumentException("n has to be larger than 3");
            _n = n;
            _randomAi = new RandomAI(board, color);
            _boardEval = boardEval;
        }

        public override Tuple<Point2D, Point2D> GetMove()
        {
            Stopwatch.Start();

            List<Piece> pieces = Board.GetAllColored(Color);
            List<Tuple<Piece, Point2D>> possibleMoves = Board.GetAllPossibleMoves();
            possibleMoves = possibleMoves.FindAll(tuple => tuple.Item1.Color == Color);

            //var newGameMoves = game.ugly_moves();
            Tuple<Point2D, Point2D> bestMove = _randomAi.GetMove();
            //use any negative large number
            var bestValue = -999999;

            for (var i = 0; i < possibleMoves.Count; i++)
            {
                var newGameMove = possibleMoves[i];
                var tmpBoard = Board.Clone();

                tmpBoard.Move(newGameMove.Item1, newGameMove.Item2);
                var currentColor = Color;
                for (int j = 1; j <= _n; j++)
                {
                    currentColor = Util.ConverToOpposite(currentColor);
                    tmpBoard.Move(new Greedy1Ply(tmpBoard, currentColor)
                        .GetMove());
                }

                //take the negative as AI plays as black
                var boardValue = BoardEvalMethod.GetEvalFunc(_boardEval).Invoke(tmpBoard, Color);
                if (boardValue > bestValue)
                {
                    bestValue = boardValue;
                    bestMove = new Tuple<Point2D, Point2D>(newGameMove.Item1.PositionPoint2D, newGameMove.Item2);
                }
            }

            _stopwatch.Stop();
            return bestMove;
        }

        public override PlayerAbstract Clone(Board board)
        {
            return new GreedyNPly(this, board);
        }
    }
}