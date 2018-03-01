using System;

namespace Chess
{
    public class IllegalMove : Exception
    {
        public IllegalMove()
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