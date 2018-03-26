using System;
using System.Collections.Generic;
using Chess.Basic;
using Chess.Basic.Pieces;

namespace Chess.Player.AI
{
    internal class Node
    {
        private readonly BoardEval BoardEval;
        public List<Node> childerNodes;

        public Node(int depth, Board board, BoardEval boardEval = BoardEval.Simple)
        {
            Depth = depth;
            Board = board;
            BoardEval = boardEval;
        }

        public Node(Node node, Pair<Point2D, Point2D> movePair, Board board, int depth = 0,
            BoardEval boardEval = BoardEval.Simple)
        {
            PreviousNode = node;
            MovePair = movePair;
            Depth = depth;
            Board = board;
            BoardEval = boardEval;
        }

        public Node PreviousNode { get; }

        public Pair<Point2D, Point2D> MovePair { get; set; }

        public int Depth { get; set; }

        public Board Board { get; set; }
        public float Score { get; set; }
        public Color Color => Board.CurrentInPlay;

        public Node[] GenerateChildNodes()
        {
            var list = new List<Node>();
            foreach (var allPossibleMove in Organize(Board.GetAllPossibleMoves(Color),
                BoardEvalMethod.GetEvalFunc(BoardEval)))
            {
                var newBoard = Board.Clone();
                newBoard.Move(allPossibleMove);
                list.Add(new Node(this,
                    new Pair<Point2D, Point2D>(allPossibleMove.Item1.PositionPoint2D, allPossibleMove.Item2), newBoard,
                    Depth - 1, BoardEval));
            }

            childerNodes = list;
            return list.ToArray();
        }

        public List<Tuple<Piece, Point2D>> Organize(List<Tuple<Piece, Point2D>> moves,
            Func<Board, Color, int> func)
        {
            moves.Sort(delegate(Tuple<Piece, Point2D> tuple, Tuple<Piece, Point2D> tuple1)
            {
                var temp = Board.Clone();
                temp.Move(tuple);
                var a = func.Invoke(temp, Color);
                temp = Board.Clone();
                temp.Move(tuple1);
                var b = func.Invoke(temp, Color);
                return a.CompareTo(b);
            });
            return moves;
        }
    }
}