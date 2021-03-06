﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chess.Basic.Pieces;
using Chess.Utils;

namespace Chess.Basic
{
    public class Board
    {
        private readonly Dictionary<Point2D, Point2D[]> _cachePossibleMove
            = new Dictionary<Point2D, Point2D[]>();

        private readonly Piece[,] _board = new Piece[8, 8];

        //start, finish ,pieces taken?
        public LinkedList<Tuple<Point2D, Point2D, bool>> MoveHistory = new LinkedList<Tuple<Point2D, Point2D, bool>>();

        public bool PrintDebug = false;

        public Board()
        {
            for (var x = 0; x < 8; x++)
            {
                _board[x, 1] = new Pawn(new Point2D(x, 1), Color.Black);
                _board[x, 6] = new Pawn(new Point2D(x, 6), Color.White);
            }

            for (var i = 0; i < 2; i++)
            {
                //Rooks
                _board[0, i == 1 ? 0 : 7] =
                    new Rook(new Point2D(0, i == 1 ? 0 : 7), i == 1 ? Color.Black : Color.White);
                _board[7, i == 1 ? 0 : 7] =
                    new Rook(new Point2D(7, i == 1 ? 0 : 7), i == 1 ? Color.Black : Color.White);

                _board[1, i == 1 ? 0 : 7] =
                    new Knight(new Point2D(1, i == 1 ? 0 : 7), i == 1 ? Color.Black : Color.White);
                _board[6, i == 1 ? 0 : 7] =
                    new Knight(new Point2D(6, i == 1 ? 0 : 7), i == 1 ? Color.Black : Color.White);

                _board[2, i == 1 ? 0 : 7] =
                    new Bishop(new Point2D(2, i == 1 ? 0 : 7), i == 1 ? Color.Black : Color.White);
                _board[5, i == 1 ? 0 : 7] =
                    new Bishop(new Point2D(5, i == 1 ? 0 : 7), i == 1 ? Color.Black : Color.White);
            }

            _board[3, 0] = new King(new Point2D(3, 0), Color.Black);
            _board[4, 7] = new King(new Point2D(4, 7), Color.White);
            _board[3, 7] = new Queen(new Point2D(3, 7), Color.White);
            _board[4, 0] = new Queen(new Point2D(4, 0), Color.Black);

            foreach (var piece in _board)
                if (piece != null)
                    AllPieces1.Add(piece);
        }

        public Board(Board board)
        {
            _board = new Piece[8, 8];
            for (var i = 0; i < board._board.GetLength(0); i++)
            for (var j = 0; j < board._board.GetLength(1); j++)
                if (board._board[i, j] != null)
                    _board[i, j] = board._board[i, j].Clone();

            MoveHistory = new LinkedList<Tuple<Point2D, Point2D, bool>>();
            foreach (var tuple in board.MoveHistory)
                MoveHistory.AddLast(
                    new Tuple<Point2D, Point2D, bool>(tuple.Item1.Clone(), tuple.Item2.Clone(), tuple.Item3));

            AllPieces1 = new List<Piece>();
            foreach (var piece in _board)
                if (piece != null)
                    AllPieces1.Add(piece);

            CurrentInPlay = board.CurrentInPlay;
        }

        public Color CurrentInPlay { get; private set; } = Color.White;

        public int MoveNumber { get; private set; }

        public List<Piece> AllPieces1 { get; } = new List<Piece>();

        public List<Piece> GetAllColored(Color color)
        {
            return AllPieces1.FindAll(p => p.Color == color && p.InPlay);
        }

        public Board Clone()
        {
            return new Board(this);
        }

        private Piece GetAt(Point2D position)
        {
            return _board[position.X, position.Y];
        }

        private bool OutOfBounds(Point2D position)
        {
            return position.X > 7 || position.X < 0 || position.Y > 7 || position.Y < 0;
        }

        public Piece Move(Piece start, Point2D finish)
        {
            return Move(start.PositionPoint2D, finish);
        }

        /// <summary>
        ///     Moves a piece on the board
        /// </summary>
        /// <param name="start">The coor of the piece you want to move</param>
        /// <param name="finish">The coor of where you want the piece to move to</param>
        /// <returns>Any piece that been taken by the starting piece</returns>
        /// <exception cref="IllegalMove"></exception>
        public Piece Move(Point2D start, Point2D finish)
        {
            //check if its a legal move
            if (GetAllPossibleMovesPerPiece(start).Contains(finish))
            {
                var atStart = GetAt(start);
                if (atStart == null)
                    throw new IllegalMove(start, finish, "Not a Piece");
                if (atStart.Color != CurrentInPlay) throw new IllegalMove(start, finish, "Wrong color", CurrentInPlay);

                var atEnd = GetAt(finish);
                atEnd?.OutOfPlay();


                atStart.PositionPoint2D = finish;
                if (atStart is Pawn && atStart.Rank == 8)
                {
                    var newPawn = new Queen(atStart.PositionPoint2D, atStart.Color);
                    _board[atStart.PositionPoint2D.X, atStart.PositionPoint2D.Y] =
                        newPawn;
                    atStart.OutOfPlay();
                    AllPieces1.Add(newPawn);
                    AllPieces1.Remove(atStart);
                    atStart = newPawn;
                }

                _board[finish.X, finish.Y] = atStart;
                _board[start.X, start.Y] = null;
                if (PrintDebug)
                    Console.WriteLine("Moving {0} {1} from {2} to {3}. {4}", atStart.Color, atStart.Name,
                        start.ToCoor(),
                        finish.ToCoor(), atEnd != null ? atEnd.Color + " " + atEnd.Name + " has been taken" : "");
                _cachePossibleMove.Clear();
                MoveNumber++;
                MoveHistory.AddLast(new Tuple<Point2D, Point2D, bool>(start, finish, atEnd != null));
                CurrentInPlay = Util.ConverToOpposite(CurrentInPlay);
                return atEnd;
            }

            PrintBoardToPrinter();
            Printer.AddToPrinter("Failed to move " + start.ToCoor() + " to " + finish.ToCoor());
            throw new IllegalMove(start, finish);
        }

        public Piece Move(Tuple<Piece, Point2D> move)
        {
            return Move(move.Item1.PositionPoint2D, move.Item2);
        }

        public Piece Move(Tuple<Point2D, Point2D> moves)
        {
            return Move(moves.Item1, moves.Item2);
        }

        public List<Tuple<Piece, Point2D>> GetAllPossibleMoves(Color color)
        {
            return GetAllPossibleMoves().FindAll(tuple => tuple.Item1.Color == color);
        }

        public List<Tuple<Piece, Point2D>> GetAllPossibleMoves()
        {
            var list = new List<Tuple<Piece, Point2D>>();
            var piecesInPlay = AllPieces1.FindAll(p => p.InPlay);
            foreach (var piece in piecesInPlay)
            foreach (var d in GetAllPossibleMovesPerPiece(piece.PositionPoint2D))
                list.Add(new Tuple<Piece, Point2D>(piece, d));

            return list;
        }

        public Point2D[] GetAllPossibleMovesPerPiece(Piece piece)
        {
            return GetAllPossibleMovesPerPiece(piece.PositionPoint2D);
        }

        public Point2D[] GetAllPossibleMovesPerPiece(Point2D position)
        {
            if (_cachePossibleMove.ContainsKey(position))
            {
                if (PrintDebug)
                    Console.WriteLine("Getting from Cache");
                return _cachePossibleMove[position];
            }

            var piece = GetAt(position);
            if (piece == null) return new Point2D[0];

            var allPossible = new List<Point2D>();

            if (piece is Pawn pawn)
            {
                foreach (var move in pawn.MoveSet)
                {
                    var tmp = position + move;
                    if (!OutOfBounds(tmp) && GetAt(tmp) == null)
                        allPossible.Add(tmp);
                }


                foreach (var attack in pawn.AttackSet)
                {
                    var tmp = position + attack;

                    if (!OutOfBounds(tmp) && GetAt(tmp) != null && GetAt(tmp).Color != pawn.Color)
                        allPossible.Add(tmp);
                }

                if (!pawn.FirstMove)
                    foreach (var jump in pawn.FirstMoveSet)
                    {
                        var tmp = position + jump;
                        if (!OutOfBounds(tmp) && GetAt(tmp) == null && GetAt(position + jump / 2) == null)
                            allPossible.Add(tmp);
                    }
            }
            else
            {
                foreach (var move in piece.MoveSet)
                {
                    var current = position;
                    do
                    {
                        current = current + move;
                        if (OutOfBounds(current)) break;
                        if (GetAt(current) != null && GetAt(current).Color == piece.Color) break;
                        allPossible.Add(current);
                        if (GetAt(current) != null) break;
                    } while (piece.MoveRepeat);
                }
            }

            if (!_cachePossibleMove.ContainsKey(position))
                _cachePossibleMove.Add(position, allPossible.ToArray());
            return allPossible.ToArray();
        }

        public Point2D[] GetAllPossibleMoves(Piece piece)
        {
            return GetAllPossibleMovesPerPiece(piece.PositionPoint2D);
        }

        public void GetStats()
        {
            var tmpWinner = Rules.CheckCheckMate(this);
            var numberOfBlackInPlay = AllPieces1.FindAll(p => p.InPlay && p.Color == Color.Black).Count;
            var numberOfWhiteInplay = AllPieces1.FindAll(p => p.InPlay && p.Color == Color.White).Count;
        }

        public void PrintBoardToPrinter()
        {
            var builder = new StringBuilder();
            builder.Append(" |");
            for (var y = 0; y < 8; y++) builder.Append(Util.ConvertNumberToLetter(y) + "|");

            builder.Append("\n");


            for (var x = 0; x < 8; x++)
            {
                builder.Append(x + 1 + "|");

                for (var y = 0; y < 8; y++)
                {
                    var piece = _board[y, x];
                    builder.Append(piece == null
                        ? " "
                        : piece.Color == Color.Black
                            ? piece.Letter.ToLower()
                            : piece.Letter.ToUpper());
                    builder.Append("|");
                }

                builder.Append("\n");
            }

            Printer.AddToPrinter(builder.ToString());
        }

        public static Board RecreateFromHistory(LinkedListNode<Tuple<Point2D, Point2D, bool>> node)
        {
            var board = new Board();
            var current = node.List.First;
            do
            {
                board.Move(current.Value.Item1, current.Value.Item2);
                current = current.Next;
                if (current == null) throw new Exception("Add a real description");
            } while (current != node);

            return board;
        }

        public static LinkedList<Piece> PieceFromHistory(LinkedListNode<Tuple<Point2D, Point2D, bool>> node)
        {
            var list = new LinkedList<Piece>();
            var board = new Board();
            var current = node.List.First;
            do
            {
                list.AddLast(board.GetAt(current.Value.Item1));
                board.Move(current.Value.Item1, current.Value.Item2);
                current = current.Next;
                if (current == null) throw new Exception("Add a real description");
            } while (current != node);

            return list;
        }

        public void PrintBoard()
        {
            Console.Write(" |");
            for (var y = 0; y < 8; y++) Console.Write(Util.ConvertNumberToLetter(y) + "|");

            Console.WriteLine("");


            for (var x = 0; x < 8; x++)
            {
                Console.Write(x + 1 + "|");

                for (var y = 0; y < 8; y++)
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