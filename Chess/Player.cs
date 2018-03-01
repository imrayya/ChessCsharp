using System;
using System.Diagnostics;

namespace Chess
{
    public abstract class Player
    {
        private string _name;
        private Color _color;
        private Board _board;

        protected Board Board => _board;

        public string Name => _name;

        public Color Color => _color;

        protected Stopwatch _stopwatch = new Stopwatch();

        public Stopwatch Stopwatch => _stopwatch;

        public abstract Tuple<Point2D, Point2D> GetMove();

        protected Player(Board board, Color color, string name)
        {
            _color = color;
            _board = board;
        }
    }
}