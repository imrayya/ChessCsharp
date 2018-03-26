using System;

namespace Chess.Basic.Pieces
{
    [Flags]
    public enum PieceEnum
    {
        /**
         * The piece is in play
         */
        InPlay = 1<<1,
        /**
         * The piece has moved alread
         */
        FirstMove = 1<<2,
        /**
         * The piece can move more than one move set
         */
        MoveRepeat = 1<<4,
        /**
         * Piece is standing on a light color
         */
        LightColor = 1<<8,
    }

    public static class FlagsHelper
    {
        public static bool IsInSet<T>(T flags, T flag) where T : struct
        {
            int flagsValue = (int)(object)flags;
            int flagValue = (int)(object)flag;

            return (flagsValue & flagValue) != 0;
        }

        public static void Set<T>(ref T flags, T flag) where T : struct
        {
            int flagsValue = (int)(object)flags;
            int flagValue = (int)(object)flag;

            flags = (T)(object)(flagsValue | flagValue);
        }

        public static void Unset<T>(ref T flags, T flag) where T : struct
        {
            int flagsValue = (int)(object)flags;
            int flagValue = (int)(object)flag;

            flags = (T)(object)(flagsValue & (~flagValue));
        }
    }
    
}