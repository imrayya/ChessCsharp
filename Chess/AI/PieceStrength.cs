using System;
using System.Linq;

namespace Chess.AI
{
    public class PieceStrength
    {
        public static int EvalBoard(Board board, Color color, Func<Piece, int> pieceEvalFunc)
        {
            return board.AllPieces1.FindAll(a => a.InPlay).Sum(a =>
                a.Color == color ? pieceEvalFunc.Invoke(a) : pieceEvalFunc.Invoke(a) * -1);
        }

        public static int StandardEval(Piece piece)
        {
            switch (piece)
            {
                case Pawn _:
                    return 10;
                case King _:
                    return 10000;
                case Knight _:
                    return 30;
                case Rook _:
                    return 50;
                case Bishop _:
                    return 35;
                case Queen _:
                    return 90;
                default:
                    return 0;
            }
        }
    }
}