using Chess.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Player.AI
{
    class Node
    {
        public Node(Node node, Pair<Point2D, Point2D> movePair, Board board, int depth=0)
        {
            PreviousNode = node;
            MovePair = movePair;
            Depth = depth;
            Board = board;
        }

        public Node PreviousNode { get; }

        public Pair<Point2D, Point2D> MovePair { get; set; }

        public int Depth { get; set; }

        public Board Board { get; set; }
    }
}