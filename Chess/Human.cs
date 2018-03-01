using System;

namespace Chess
{
    public class Human : Player
    {
        private RandomAI _randomAi;
        public Human(Board board, Color color) : base(board, color, "Human " + color)
        {
            _randomAi= new RandomAI(board,color);
        }

        public override Tuple<Point2D, Point2D> GetMove()
        {

            string input = Console.ReadLine();
            string[] split = input.Split(' ');
            if (split[0] == "ran") return _randomAi.GetMove();
            var start = new Point2D(split[0]);
            var end = new Point2D(split[1]);
            return new Tuple<Point2D, Point2D>(start, end);
        }
    }
}