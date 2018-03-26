using System;
using System.Linq;
using Chess.Basic;
using Chess.Basic.Pieces;

namespace Chess.Player.AI
{
    public enum BoardEval
    {
        Shannon,
        Simple,
        SimpleTable,
        Table
    }

    public static class BoardEvalMethod
    {
        public static Func<Board, Color, int> GetEvalFunc(BoardEval eval)
        {
            switch (eval)
            {
                case BoardEval.Shannon:
                    return PieceStrength.EvalBoardShannon;
                case BoardEval.Simple:
                    return PieceStrength.EvalBoardSimple;
                case BoardEval.SimpleTable:
                    return PieceStrength.EvalBoardSimpeTable;
                case BoardEval.Table:
                    return PieceStrength.EvalBoardTable;
                default:
                    throw new ArgumentOutOfRangeException(nameof(eval), eval, null);
            }
        }
    }

    public static class PieceStrength
    {
        private static readonly short[] PawnTable =
        {
            0, 0, 0, 0, 0, 0, 0, 0,
            50, 50, 50, 50, 50, 50, 50, 50,
            10, 10, 20, 30, 30, 20, 10, 10,
            5, 5, 10, 27, 27, 10, 5, 5,
            0, 0, 0, 25, 25, 0, 0, 0,
            5, -5, -10, 0, 0, -10, -5, 5,
            5, 10, 10, -25, -25, 10, 10, 5,
            0, 0, 0, 0, 0, 0, 0, 0
        };

        private static readonly short[] KnightTable =
        {
            -50, -40, -30, -30, -30, -30, -40, -50,
            -40, -20, 0, 0, 0, 0, -20, -40,
            -30, 0, 10, 15, 15, 10, 0, -30,
            -30, 5, 15, 20, 20, 15, 5, -30,
            -30, 0, 15, 20, 20, 15, 0, -30,
            -30, 5, 10, 15, 15, 10, 5, -30,
            -40, -20, 0, 5, 5, 0, -20, -40,
            -50, -40, -20, -30, -30, -20, -40, -50
        };

        private static readonly short[] BishopTable =
        {
            -20, -10, -10, -10, -10, -10, -10, -20,
            -10, 0, 0, 0, 0, 0, 0, -10,
            -10, 0, 5, 10, 10, 5, 0, -10,
            -10, 5, 5, 10, 10, 5, 5, -10,
            -10, 0, 10, 10, 10, 10, 0, -10,
            -10, 10, 10, 10, 10, 10, 10, -10,
            -10, 5, 0, 0, 0, 0, 5, -10,
            -20, -10, -40, -10, -10, -40, -10, -20
        };

        private static readonly short[] RookTable =
        {
            0, 0, 0, 0, 0, 0, 0, 0,
            5, 10, 10, 10, 10, 10, 10, 5,
            -5, 0, 0, 0, 0, 0, 0, -5,
            -5, 0, 0, 0, 0, 0, 0, -5,
            -5, 0, 0, 0, 0, 0, 0, -5,
            -5, 0, 0, 0, 0, 0, 0, -5,
            5, 10, 10, 10, 10, 10, 10, 5,
            0, 0, 0, 5, 5, 0, 0, 0
        };

        private static readonly short[] QueenTable =
        {
            20, -10, -10, -5, -5, -10, -10, -20,
            -10, 0, 0, 0, 0, 0, 0, -10,
            -10, 0, 5, 5, 5, 5, 0, -10,
            -5, 0, 5, 5, 5, 5, 0, -5,
            0, 0, 5, 5, 5, 5, 0, -5,
            -10, 5, 5, 5, 5, 5, 0, -10,
            -10, 0, 5, 0, 0, 0, 0, -10,
            -20, -10, -10, -5, -5, -10, -10, -20
        };


        private static readonly short[] KingTable =
        {
            -30, -40, -40, -50, -50, -40, -40, -30,
            -30, -40, -40, -50, -50, -40, -40, -30,
            -30, -40, -40, -50, -50, -40, -40, -30,
            -30, -40, -40, -50, -50, -40, -40, -30,
            -20, -30, -30, -40, -40, -30, -30, -20,
            -10, -20, -20, -20, -20, -20, -20, -10,
            20, 20, 0, 0, 0, 0, 20, 20,
            20, 30, 10, 0, 0, 10, 30, 20
        };

        private static readonly short[] KingTableEndGame =
        {
            -50, -40, -30, -20, -20, -30, -40, -50,
            -30, -20, -10, 0, 0, -10, -20, -30,
            -30, -10, 20, 30, 30, 20, -10, -30,
            -30, -10, 30, 40, 40, 30, -10, -30,
            -30, -10, 30, 40, 40, 30, -10, -30,
            -30, -10, 20, 30, 30, 20, -10, -30,
            -30, -30, 0, 0, 0, 0, -30, -30,
            -50, -30, -30, -30, -30, -30, -30, -50
        };

        /*
        f(p) = 200(K-K') Done
      + 9(Q-Q') Done
      + 5(R-R') Done
      + 3(B-B' + N-N') Done
      + 1(P-P') Done
      - 0.5(D-D' + S-S' + I-I')
      + 0.1(M-M') + ... Done
      KQRBNP = number of kings, queens, rooks, bishops, knights and pawns
       D,S,I = doubled, blocked and isolated pawns
       M = Mobility (the number of legal moves)
        */
        public static int EvalBoardShannon(Board board, Color color)
        {
            float score = 0;
            board.AllPieces1.FindAll(piece => piece.InPlay).ForEach(piece =>
                score += piece.Color == color ? ShannonPieceEval(piece) : -ShannonPieceEval(piece));
            var moves = board.GetAllPossibleMoves();
            var tmp = moves.FindAll(tuple => tuple.Item1.Color == color).Count;
            score += 0.1f * (tmp - (moves.Count - tmp));
            var pawnThatCanMove = moves.FindAll(tuple => tuple.Item1 is Pawn).ConvertAll(tuple => tuple.Item1);
            score -= 0.5f * board.AllPieces1.Except(pawnThatCanMove).ToList().FindAll(piece => piece.Color == color)
                         .Count();
            score += 0.5f * board.AllPieces1.Except(pawnThatCanMove).ToList().FindAll(piece => piece.Color != color)
                         .Count();

            return (int) score;
        }

        private static int ShannonPieceEval(Piece piece)
        {
            switch (piece)
            {
                case Pawn _:
                    return 1;
                case King _:
                    return 200;
                case Knight _:
                    return 3;
                case Rook _:
                    return 5;
                case Bishop _:
                    return 3;
                case Queen _:
                    return 9;
                default:
                    return 0;
            }
        }

        public static int EvalBoardSimple(Board board, Color color)
        {
            return board.AllPieces1.FindAll(a => a.InPlay).Sum(a =>
                a.Color == color ? NaiveEval(a) : NaiveEval(a) * -1);
        }

        private static int NaiveEval(Piece piece)
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

        public static int EvalBoardSimpeTable(Board board, Color color)
        {
            return board.AllPieces1.FindAll(a => a.InPlay).Sum(a =>
                a.Color == color ? NaiveTableEval(a) : NaiveTableEval(a) * -1);
        }

        public static int EvalBoardTable(Board board, Color color)
        {
            return board.AllPieces1.FindAll(a => a.InPlay).Sum(a =>
                a.Color == color ? NaiveTableEval(a) + NaiveEval(a) : (NaiveTableEval(a) + NaiveEval(a)) * -1);
        }

        private static int NaiveTableEval(Piece piece)
        {
            if (piece != null)
            {
                var xy = piece.PositionPoint2D.X * 8 + piece.PositionPoint2D.Y;
                switch (piece)
                {
                    case Bishop bishop:
                        return BishopTable[xy];
                    case King king:
                        return KingTable[xy];
                    case Knight knight:
                        return KnightTable[xy];
                    case Pawn pawn:
                        return PawnTable[xy];
                    case Queen queen:
                        return QueenTable[xy];
                    case Rook rook:
                        return RookTable[xy];
                }
            }

            return 0;
        }
    }
}