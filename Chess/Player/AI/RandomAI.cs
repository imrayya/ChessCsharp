using System;
using Chess.Basic;

namespace Chess.Player.AI
{
    public class RandomAI : PlayerAbstract
    {
        private readonly Random _random;

        public RandomAI(Board board, Color color) : base(board, color, "Random AI")
        {
            _random = new Random();
        }

        public RandomAI(Board board, Color color, int seed) : base(board, color, "Random AI")
        {
            _random = new Random(seed);
        }

        public RandomAI(RandomAI randomAi) : base(randomAi)
        {
            _random = randomAi._random;
        }

        public RandomAI(RandomAI randomAi, Board board) : base(randomAi, board)
        {
            _random = randomAi._random;
        }

        public override PlayerAbstract Clone(Board board)
        {
            return new RandomAI(this, board);
        }

        public override Tuple<Point2D, Point2D> GetMove()
        {
            Stopwatch.Start();
            var pieces = Board.GetAllColored(Color);
            var possibleMoves = Board.GetAllPossibleMoves();
            possibleMoves = possibleMoves.FindAll(tuple => tuple.Item1.Color == Color);
            var r = _random.Next(possibleMoves.Count);
            var tmp = possibleMoves[r];
            Stopwatch.Stop();
            return new Tuple<Point2D, Point2D>(tmp.Item1.PositionPoint2D, tmp.Item2);
        }
    }
}