using System;
using System.Collections.Generic;

namespace Chess.Basic
{
    public struct Pair<T1, T2>
    {

        public Tuple<T1, T2> ToTuple()
        {
            return new Tuple<T1, T2>(Left,Right);
        }
        public bool Equals(Pair<T1, T2> other)
        {
            return EqualityComparer<T1>.Default.Equals(Left, other.Left) &&
                   EqualityComparer<T2>.Default.Equals(Right, other.Right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Pair<T1, T2> && Equals((Pair<T1, T2>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<T1>.Default.GetHashCode(Left) * 397) ^
                       EqualityComparer<T2>.Default.GetHashCode(Right);
            }
        }

        public static bool operator ==(Pair<T1, T2> left, Pair<T1, T2> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Pair<T1, T2> left, Pair<T1, T2> right)
        {
            return !left.Equals(right);
        }

        public T1 Left { get; set; }

        public T2 Right { get; set; }

        public Pair(T1 left, T2 right)
        {
            Left = left;
            Right = right;
        }
    }
}