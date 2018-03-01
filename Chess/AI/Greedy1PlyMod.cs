using System;
using System.Collections.Generic;

namespace Chess.AI
{
    public class Greedy1PlyMod : Player
    {
        private RandomAI _randomAi;

        public Greedy1PlyMod(Greedy1PlyMod player, Board board) : base(player, board)
        {
            _randomAi = new RandomAI(board, player.Color);
        }

        public Greedy1PlyMod(Board board, Color color) : base(board, color, "Greedy 1.5ply Modded AI")
        {
            _randomAi = new RandomAI(board, color);
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
                tmpBoard.Move(new RandomAI(tmpBoard,Util.ConverToOpposite(Color)).GetMove());
                //take the negative as AI plays as black
                var boardValue = PieceStrength.EvalBoard(tmpBoard, Color, PieceStrength.StandardEval);
                if (boardValue > bestValue)
                {
                    bestValue = boardValue;
                    bestMove = new Tuple<Point2D, Point2D>(newGameMove.Item1.PositionPoint2D, newGameMove.Item2);
                }
            }

            _stopwatch.Stop();
            return bestMove;
        }

        public override Player Clone(Board board)
        {
            return new Greedy1PlyMod(this, board);
        }
    }
}