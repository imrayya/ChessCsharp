using System;
using Chess.Utils;

namespace Chess.Basic
{
    public struct Point2D
    {
        public int X { get; }

        public int Y { get; }

        public Point2D(Point2D point2D)
        {
            X = point2D.X;
            Y = point2D.Y;
        }

        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return "Point2D{" + X + "," + Y + "}";
        }


        public Point2D(string coor)
        {
            X = Util.ConvertLetterToNumber(coor[0]) ?? throw new ArgumentException("Invalid Coordiantes");
            Y = int.Parse(coor[1].ToString()) - 1;
        }

        public string ToCoor()
        {
            return (Util.ConvertNumberToLetter(X) ?? throw new ArgumentException("Number cannot convert to letter")) +
                   "" +
                   (Y + 1);
        }

        public static Point2D operator +(Point2D a, Point2D b)
        {
            var x = a.X + b.X;
            var y = a.Y + b.Y;
            return new Point2D(x, y);
        }

        public static Point2D operator /(Point2D a, int b)
        {
            var x = a.X / b;
            var y = a.Y / b;
            return new Point2D(x, y);
        }

        public bool Equals(Point2D other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Point2D && Equals((Point2D) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public static bool operator ==(Point2D x, Point2D y)
        {
            return x.X == y.X && x.Y == y.Y;
        }

        public static bool operator !=(Point2D x, Point2D y)
        {
            return !(x == y);
        }

        public Point2D Clone()
        {
            return new Point2D(this);
        }
    }
}