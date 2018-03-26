using System;
using System.Collections.Generic;
using Chess.Basic;
using Chess.Basic.Pieces;
using Chess.Player;

namespace Chess.Player.AI
{
    public class Greedy1Ply : PlayerAbstract
    {
        private RandomAI _randomAi;

        public Greedy1Ply(Greedy1Ply player, Board board) : base(player, board)
        {
            _randomAi = new RandomAI(board, player.Color);
            _boardEval = player._boardEval;
        }

        private BoardEval _boardEval;
        
        public Greedy1Ply(Board board, Color color, BoardEval boardEval = BoardEval.Simple) : base(board, color, "Greedy 1ply AI")
        {
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
            var bestValue = BoardEvalMethod.GetEvalFunc(_boardEval).Invoke(Board,Color);

            for (var i = 0; i < possibleMoves.Count; i++)
            {
                var newGameMove = possibleMoves[i];
                var tmpBoard = Board.Clone();

                tmpBoard.Move(newGameMove.Item1, newGameMove.Item2);
                //take the negative as AI plays as black
                var boardValue = BoardEvalMethod.GetEvalFunc(_boardEval).Invoke(tmpBoard, Color);
                if (boardValue > bestValue)
                {
                    bestValue = boardValue;
                    bestMove = new Tuple<Point2D, Point2D>(newGameMove.Item1.PositionPoint2D, newGameMove.Item2);
                }
            }
            if(bestValue == PieceStrength.EvalBoardSimple(Board,Color))
            { _stopwatch.Stop();
                return _randomAi.GetMove();
            }

            _stopwatch.Stop();
            return bestMove;
        }

        public override PlayerAbstract Clone(Board board)
        {
            return new Greedy1Ply(this, board);
        }
    }
}