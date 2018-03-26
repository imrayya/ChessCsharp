using System;
using System.Diagnostics;
using Chess.Basic;

namespace Chess.Player
{
    public abstract class PlayerAbstract
    {
        protected Stopwatch _stopwatch = new Stopwatch();

        protected PlayerAbstract(PlayerAbstract playerAbstract)
        {
            Board = playerAbstract.Board;
            Name = playerAbstract.Name;
            Color = playerAbstract.Color;
        }

        protected PlayerAbstract(PlayerAbstract playerAbstract, Board board)
        {
            Board = board;
            Name = playerAbstract.Name;
            Color = playerAbstract.Color;
        }

        protected PlayerAbstract(Board board, Color color, string name)
        {
            Color = color;
            Board = board;
            Name = name;
        }

        protected Board Board { get; }

        public string Name { get; }

        public Color Color { get; }

        public Stopwatch Stopwatch => _stopwatch;

        public abstract Tuple<Point2D, Point2D> GetMove();

        public abstract PlayerAbstract Clone(Board board);
    }
}