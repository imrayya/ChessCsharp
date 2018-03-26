using Chess.Basic;

namespace Chess.Utils
{
    public class Util
    {
        public static int? ConvertLetterToNumber(char c)
        {
            switch (c)
            {
                case 'a': return 0;
                case 'b': return 1;
                case 'c': return 2;
                case 'd': return 3;
                case 'e': return 4;
                case 'f': return 5;
                case 'g': return 6;
                case 'h': return 7;
                default: return null;
            }
        }

        public static Color ConverToOpposite(Color color)
        {
            return color == Color.Black ? Color.White : Color.Black;
        }

        public static char? ConvertNumberToLetter(int b)
        {
            switch (b)
            {
                case 0: return 'a';
                case 1: return 'b';
                case 2: return 'c';
                case 3: return 'd';
                case 4: return 'e';
                case 5: return 'f';
                case 6: return 'g';
                case 7: return 'h';
                default: return null;
            }
        }
    }
}