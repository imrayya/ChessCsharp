using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Basic.Pieces;

namespace Chess.Basic
{
    public class Rules
    {
        public static bool CheckDraw(Board board)
        {
            //Can't move
            List<Piece> list = board.GetAllColored(board.CurrentInPlay);
            bool canMove = false;
            foreach (var piece in list)
            {
                if (board.GetAllPossibleMovesPerPiece(piece).Length != 0)
                {
                    canMove = true;
                    break;
                }
            }

            if (canMove == false) return true;

            //Can't Make a move
            var moves = board.GetAllPossibleMoves();
            int x = 0;
            foreach (Tuple<Piece, Point2D> tuple in moves)
            {
                if (tuple.Item1.Color == board.CurrentInPlay) x++;
            }

            if (x == 0) return true;


            //Same move in the last 10 turns (between both)
            var history = board.MoveHistory.Last;
            for (int i = 0; i < 10; i++)
            {
            }

            //if no pawn has moved in the last 75 moves or !no capture!
            if (history.List.Count > 75)
            {
                bool capture = false;
                for (int i = 0; i < 75; i++)
                {
                    if (history.Value.Item3)
                    {
                        capture = true;
                        break;
                    }

                    history = history.Previous;
                }

                if (!capture) return true;
            }

            //Impossible Checkmate
            /*king versus king
             king and bishop versus king
             king and knight versus king
             king and bishop versus king and bishop with the bishops on the same colour. 
                 (Any number of additional bishops of either color on the same color of 
                 square due to underpromotion do not affect the situation.)
             * 
             */


            //If the same position occurs for five consecutive moves by both players, the game is automatically a draw (i.e. a player does not have to claim it)

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