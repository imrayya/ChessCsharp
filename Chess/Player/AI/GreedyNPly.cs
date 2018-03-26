using System;
using Chess.Basic;
using Chess.Utils;

namespace Chess.Player.AI
{
    public class GreedyNPly : PlayerAbstract
    {
        private readonly BoardEval _boardEval;

        private readonly int _n;
        private readonly RandomAI _randomAi;


        public GreedyNPly(GreedyNPly player, Board board) : base(player, board)
        {
            _randomAi = new RandomAI(board, player.Color);
            _boardEval = player._boardEval;
            _n = player._n;
        }

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
                var currentColor = Color;
                for (var j = 1; j <= _n; j++)
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