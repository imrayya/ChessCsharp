using System;

namespace Chess
{
    public struct Point2D
    {
        private byte _x;
        private byte _y;

        public byte X
        {
            get { return _x; }
            set { _x = value; }
        }

        public byte Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public Point2D(Point2D point2D)
        {
            _x = point2D._x;
            _y = point2D._y;
        }

        public Point2D(byte x, byte y)
        {
            _x = x;
            _y = y;
        }

        public Point2D(string coor)
        {
            _x = Util.ConvertLetterToNumber(coor[0]) ?? throw new ArgumentException("Invalid Coordiantes");
            _y = byte.Parse(coor[1].ToString());

        }

        public static Point2D operator +(Point2D a, Point2D b)
        {
            var x = a._x + b._x;
            var y = a._y + b._y;
            return new Point2D((byte) x, (byte) y);
        }
    }
}