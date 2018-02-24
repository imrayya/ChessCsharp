using System;

namespace Chess
{
    public struct Point2D
    {
        public int X { get; set; }

        public int Y { get; set; }

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

        public Point2D(string coor)
        {
            X = Util.ConvertLetterToNumber(coor[0]) ?? throw new ArgumentException("Invalid Coordiantes");
            Y = int.Parse(coor[1].ToString())-1;
        }

        public string ToCoor() => (Util.ConvertNumberToLetter(X) ?? throw new ArgumentException("Number cannot convert to letter")) + "" + Y;

        public static Point2D operator +(Point2D a, Point2D b)
        {
            var x = a.X + b.X;
            var y = a.Y + b.Y;
            return new Point2D(x, y);
        }
    }
}