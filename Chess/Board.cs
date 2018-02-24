using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Chess
{
    public class Board
    {
        Piece[,] _board = new Piece[8, 8];
        private List<Piece> AllPieces = new List<Piece>();
        public Board()
        {
            for (int x = 0; x < 8; x++)
            {
                _board[x, 1] = new Pawn(new Point2D(x, 1), Color.Black);
                _board[x, 6] = new Pawn(new Point2D(x, 6), Color.White);
            }

            for (int i = 0; i < 2; i++)
            {
                //Rooks
                _board[0, i == 1 ? 0 : 7] = new Rook(new Point2D(0, i == 1 ? 0 : 7), i == 1 ? Color.Black : Color.White);
                _board[7, i == 1 ? 0 : 7] = new Rook(new Point2D(7, i == 1 ? 0 : 7), i == 1 ? Color.Black : Color.White);

                _board[1, i == 1 ? 0 : 7] = new Knight(new Point2D(1, i == 1 ? 0 : 7), i == 1 ? Color.Black : Color.White);
                _board[6, i == 1 ? 0 : 7] = new Knight(new Point2D(6, i == 1 ? 0 : 7), i == 1 ? Color.Black : Color.White);

                _board[2, i == 1 ? 0 : 7] = new Bishop(new Point2D(2, i == 1 ? 0 : 7), i == 1 ? Color.Black : Color.White);
                _board[5, i == 1 ? 0 : 7] = new Bishop(new Point2D(5, i == 1 ? 0 : 7), i == 1 ? Color.Black : Color.White);

               

                //board[0, i == 1 ? 0 : 7] = new Rook(new Point2D(0, i == 1 ? 0 : 7), i == 1 ? Color.Black : Color.White);
                //board[7, i == 1 ? 0 : 7] = new Rook(new Point2D(7, i == 1 ? 0 : 7), i == 1 ? Color.Black : Color.White);
            }
            _board[3, 0] = new King(new Point2D(3, 0), Color.Black );
            _board[4, 7] = new King(new Point2D(4, 7), Color.White);
            _board[3, 7] = new Queen(new Point2D(3, 7), Color.White);
            _board[4, 0] = new Queen(new Point2D(4, 0), Color.Black);

            foreach (var piece in _board)
            {
                if (piece != null)
                {
                    AllPieces.Add(piece);
                }
            }
        }

        public Point2D[] GetAllPossibleMoves(Point2D position)
        {
            Piece piece = _board[position.X, position.Y];
            return null;
        }

        public Point2D[] GetAllPossibleMoves(Piece piece)
        {
            return GetAllPossibleMoves(piece.PositionPoint2D);
        }

        public void PrintBoard()
        {
            Console.Write(" |");
            for (int y = 0; y < 8; y++)
            {
                Console.Write(Util.ConvertNumberToLetter(y) + "|");
            }

            Console.WriteLine("");


            for (int x = 0; x < 8; x++)
            {
                Console.Write((x + 1) + "|");

                for (int y = 0; y < 8; y++)
                {
                    var piece = _board[y, x];
                    Console.Write(piece == null
                        ? " "
                        : piece.Color == Color.Black
                            ? piece.Letter.ToLower()
                            : piece.Letter.ToUpper());
                    Console.Write("|");
                }

                Console.WriteLine("");
            }
        }
    }
}