using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public abstract class Piece
    {
        public Point2D PositionPoint2D { get; set; }

        internal string letter;
        internal string name;
        internal bool moveRepeat;
        internal Point2D[] moveSet;

        public Point2D[] MoveSet => moveSet;
        public string Letter => letter;
        public string Name => name;
        public bool MoveRepeat => moveRepeat;

        public Color Color { get; }

        public Piece(Piece piece)
        {
            PositionPoint2D = piece.PositionPoint2D;
            letter = piece.Letter;
            name = piece.Name;
            moveRepeat = piece.MoveRepeat;
            moveSet = piece.moveSet;
            Color = piece.Color;
        }

        public Piece(Point2D positionPoint2D, Color color)
        {
            PositionPoint2D = positionPoint2D;
            Color = color;
        }

        public override string ToString()
        {
            return Name + " at " + PositionPoint2D.ToCoor();
        }
    }

    public class Knight : Piece
    {
        public Knight(Point2D positionPoint2D, Color color) : base(positionPoint2D, color)
        {
            letter = "n";
            name = "Knight";
            moveRepeat = false;
            moveSet = new Point2D[]
            {
                new Point2D(1, 2), new Point2D(1, -2), new Point2D(-1, 2), new Point2D(-1, -2),
                new Point2D(2, 1), new Point2D(-2, 1), new Point2D(2, -1), new Point2D(-2, -1)
            };
        }
    }

    public class Pawn : Piece
    {
        public Pawn(Point2D positionPoint2D, Color color) : base(positionPoint2D, color)
        {
            letter = "p";
            name = "Pawn";
            moveRepeat = false;
            moveSet = color == Color.Black ? new Point2D[] {new Point2D(0, 1)} : new Point2D[] {new Point2D(0, -1)};
        }
    }

    public class Rook : Piece
    {
        public Rook(Point2D positionPoint2D, Color color) : base(positionPoint2D, color)
        {
            letter = "r";
            name = "Rook";
            moveRepeat = true;
            moveSet = new Point2D[] {new Point2D(1, 0), new Point2D(-1, 0), new Point2D(0, 1), new Point2D(0, -1)};
        }
    }

    public class Bishop : Piece
    {
        public Bishop(Point2D positionPoint2D, Color color) : base(positionPoint2D, color)
        {
            letter = "b";
            name = "Bishop";
            moveRepeat = true;
            moveSet = new Point2D[] {new Point2D(1, 1), new Point2D(-1, 1), new Point2D(1, -1), new Point2D(-1, -1)};
        }
    }

    public class King : Piece
    {
        public King(Point2D positionPoint2D, Color color) : base(positionPoint2D, color)
        {
            letter = "k";
            name = "King";
            moveRepeat = false;
            moveSet = new Point2D[]
            {
                new Point2D(1, 0), new Point2D(-1, 0), new Point2D(0, 1), new Point2D(0, -1), new Point2D(1, 1),
                new Point2D(-1, 1), new Point2D(1, -1), new Point2D(-1, -1)
            };
        }
    }

    public class Queen : Piece
    {
        public Queen(Point2D positionPoint2D, Color color) : base(positionPoint2D, color)
        {
            letter = "q";
            name = "Queen";
            moveRepeat = true;
            moveSet = new Point2D[]
            {
                new Point2D(1, 0), new Point2D(-1, 0), new Point2D(0, 1), new Point2D(0, -1), new Point2D(1, 1),
                new Point2D(-1, 1), new Point2D(1, -1), new Point2D(-1, -1)
            };
        }
    }
}