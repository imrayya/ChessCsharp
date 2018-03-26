using System;
using Chess.Basic.Pieces;

namespace Chess.Basic
{
    public class IllegalMove : Exception
    {
        public IllegalMove()
        {
        }

        public IllegalMove(Point2D start, Point2D moveLocation, string message, Color? color = null) : base(
            start.ToCoor() + " can not move to " +
            moveLocation.ToCoor() + " " + message + " " + color ?? "")
        {
        }

        public IllegalMove(Point2D start, Point2D moveLocation) : base(
            start.ToCoor() + " can not move to " +
            moveLocation.ToCoor())
        {
        }

        public IllegalMove(Piece piece, Point2D moveLocation) : base(
            piece.Color + " " + piece.Name + " at " + piece.PositionPoint2D.ToCoor() + " can not move to " +
            moveLocation.ToCoor())
        {
        }

        public IllegalMove(string message) : base(message)
        {
        }
    }
}