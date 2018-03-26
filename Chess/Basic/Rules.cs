using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Basic.Pieces;

namespace Chess.Basic
{
    public static class Rules
    {
        public static bool GameFinished(Board board)
        {
            if (CheckDraw(board)) return true;
            if (CheckCheckMate(board)) return true;
            return board.AllPieces1.Exists(piece => !piece.InPlay && piece is King);
        }

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

            //Same move in the last 10 turns (between both)
            var history = board.MoveHistory.Last;
            var set = new List<Tuple<Point2D, Point2D>>();
            if (history == null) return false;
            for (int i = 0; i < 10; i++)
            {
                Tuple<Point2D, Point2D> currentMove =
                    new Tuple<Point2D, Point2D>(history.Value.Item1, history.Value.Item2);
                if (set.Any(a => !Equals(a, currentMove)))
                {
                    set.Add(currentMove);
                }

                history = history.Previous;
                if (history == null) break;
            }

            if (set.Count == 10) return true;


            //if no pawn has moved in the last 75 moves or !no capture!

            if (history != null &&history.List.Count > 75)
            {
                var pieceHistory = Board.PieceFromHistory(history).Last;

                bool capture = false;
                bool pawn = false;
                for (int i = 0; i < 75; i++)
                {
                    if (history.Value.Item3)
                    {
                        capture = true;
                        break;
                    }

                    if (pawn || pieceHistory.Value is Pawn) pawn = true;
                    pieceHistory = pieceHistory.Previous;
                    history = history.Previous;
                }

                if (!pawn) return true;
                if (!capture) return true;
            }

            //Impossible Checkmate
            /*king versus king ~ Done
             king and bishop versus king ~ Done
             king and knight versus king ~ Done
             king and bishop versus king and bishop with the bishops on the same colour. 
                 (Any number of additional bishops of either color on the same color of 
                 square due to underpromotion do not affect the situation.) ~ Done (first line)
             * 
             */
            var inPlay = board.AllPieces1.FindAll(piece => piece.InPlay);

            if (inPlay.All(piece => piece is King))
                return true;
            if (inPlay.Count == 3)
            {
                if (inPlay.FindAll(piece => piece is King | piece is Bishop).Count == 3)
                    return true;
                if (inPlay.FindAll(piece => piece is King | piece is Knight).Count == 3)
                    return true;
            }

            if (inPlay.Count == 4)
            {
                var temp = inPlay.FindAll(piece => piece is Bishop);
                var firstBishop = temp[0];
                var secondBishop = temp[1];
                if (firstBishop.LightColor == secondBishop.LightColor) return true;
            }

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