using System;
using System.Linq;

namespace Chess
{
    public class Rules
    {
        public static bool CheckDraw(Board board)
        {
            return false;
        }

        public static Tuple<bool, Color> CheckCheck(Board board)
        {
            var allMoves = board.GetAllPossibleMoves();
            var kings = board.AllPieces1.FindAll(p => p is King);
            foreach (var king in kings)
            {
                if (allMoves.Any(p => p.Item2 == king.PositionPoint2D))
                    return new Tuple<bool, Color>(true, king.Color);
            }

            return new Tuple<bool, Color>(false, Color.NoColor);
        }

        public static bool CheckCheckMate(Board board)
        {
            var check = CheckCheck(board);
            //king in trouble
            if (!check.Item1)
            {
                return false;
            }

            var king = board.AllPieces1.Find(p => p is King && p.Color == check.Item2);
            //king can move
            var allMoves = board.GetAllPossibleMoves(); //Gets all possible moves
            var allMovesOp =
                allMoves.FindAll(tuple => tuple.Item1.Color != king.Color); //Get all moves that opposition can make
            var kingMove =
                allMoves.FindAll(move => move.Item1 == king)
                    .ConvertAll(tuple => tuple.Item2); //All moves that the king can make
            foreach (var kingmove in kingMove)
            {
                if (allMovesOp.ConvertAll(tuple => tuple.Item2).Any(move => move != kingmove))
                {
                    return false;
                }
            }

            //block move
            var allMovesFriendly = allMoves.FindAll(tuple => tuple.Item1.Color == king.Color);
                        

            //eat threaten
            //checkmate
            return false;
        }
    }
}