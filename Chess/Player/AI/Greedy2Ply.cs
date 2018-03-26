using System;
using Chess.Basic;
using Chess.Utils;

namespace Chess.Player.AI
{
    public class Greedy2Ply : PlayerAbstract
    {
        private readonly BoardEval _boardEval;
        private readonly RandomAI _randomAi;

        public Greedy2Ply(Greedy2Ply player, Board board) : base(player, board)
        {
            _randomAi = new RandomAI(board, player.Color);
            _boardEval = player._boardEval;
        }

        public Greedy2Ply(Board board, Color color, BoardEval boardEval = BoardEval.Simple) : base(board, color,
            "Greedy 2ply AI")
        {
            _randomAi = new RandomAI(board, color);
            _boardEval = boardEval;
        }

        public override Tuple<Point2D, Point2D> GetMove()
        {
            Stopwatch.Start();

            var pieces = Board.GetAllColored(Color);
            var possibleMoves = Board.GetAllPossibleMoves();
            possibleMoves = possibleMoves.FindAll(tuple => tuple.Item1.Color == Color);

            //var newGameMoves = game.ugly_moves();
            var bestMove = _randomAi.GetMove();
            //use any negative large number
            var bestValue = BoardEvalMethod.GetEvalFunc(_boardEval).Invoke(Board, Color);

            for (var i = 0; i < possibleMoves.Count; i++)
            {
                var newGameMove = possibleMoves[i];
                var tmpBoard = Board.Clone();

                tmpBoard.Move(newGameMove.Item1, newGameMove.Item2);
                tmpBoard.Move(new Greedy1Ply(tmpBoard, Util.ConverToOpposite(Color)).GetMove());
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
            return new Greedy2Ply(this, board);
        }
    }
}