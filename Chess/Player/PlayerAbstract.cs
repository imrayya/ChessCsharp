using System;
using System.Diagnostics;
using Chess.Basic;

namespace Chess.Player
{
    public abstract class PlayerAbstract
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

        protected PlayerAbstract(PlayerAbstract playerAbstract)
        {
            _board = playerAbstract._board;
            _name = playerAbstract._name;
            _color = playerAbstract._color;
        }

        protected PlayerAbstract(PlayerAbstract playerAbstract, Board board)
        {
            _board = board;
            _name = playerAbstract._name;
            _color = playerAbstract._color;
        }

        public abstract PlayerAbstract Clone(Board board);

        protected PlayerAbstract(Board board, Color color, string name)
        {
            _color = color;
            _board = board;
            _name = name;
        }
    }
}