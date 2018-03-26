using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Chess.Basic;
using Chess.Utils;

namespace Chess.Player.AI
{
    public class AlphaBetaSimple : PlayerAbstract
    {
        private readonly BoardEval _boardEval;

        private readonly int _depth;

        public AlphaBetaSimple(AlphaBetaSimple playerAbstract, Board board) : base(playerAbstract, board)
        {
            _boardEval = playerAbstract._boardEval;
            _depth = playerAbstract._depth;
        }

        public AlphaBetaSimple(Board board, Color color, int depth = 3, BoardEval boardEval = BoardEval.Simple) : base(
            board, color, "Alpha Beta Simple" + depth)
        {
            _boardEval = boardEval;
            _depth = depth;
        }

        /*
        function alphabeta(node, depth, α, β, maximizingPlayer)
02      if depth = 0 or node is a terminal node
03          return the heuristic value of node
04      if maximizingPlayer
05          v := -∞
06          for each child of node
07              v := max(v, alphabeta(child, depth – 1, α, β, FALSE))
08              α := max(α, v)
09              if β ≤ α
10                  break (* β cut-off *)
11          return v
12      else
13          v := +∞
14          for each child of node
15              v := min(v, alphabeta(child, depth – 1, α, β, TRUE))
16              β := min(β, v)
17              if β ≤ α
18                  break (* α cut-off *)
19          return v

            alphabeta(origin, depth, -∞, +∞, TRUE)

         */
        private float AlpaBetaMinimax(Node node, float alpha, float beta, int depth, Color color)
        {
            if (depth == 0 || Rules.GameFinished(node.Board))
            {
                if (Rules.CheckCheckMate(node.Board))
                    node.Score = color == Color ? 1000 : -1000;
                else
                    node.Score = BoardEvalMethod.GetEvalFunc(_boardEval).Invoke(node.Board, Color);

                return node.Score;
            }

            if (color == Color)
            {
                var v = -float.MaxValue;
                foreach (var childNode in node.GenerateChildNodes())
                {
                    v = Math.Max(v, AlpaBetaMinimax(childNode, alpha, beta, depth - 1, Util.ConverToOpposite(color)));
                    alpha = Math.Max(v, alpha);
                    node.Score = alpha;
                    if (beta <= alpha) break;
                }

                return v;
            }
            else
            {
                var v = float.MaxValue;
                foreach (var childNode in node.GenerateChildNodes())
                {
                    v = Math.Min(v, AlpaBetaMinimax(childNode, alpha, beta, depth - 1, Util.ConverToOpposite(color)));
                    beta = Math.Min(v, beta);
                    node.Score = beta;
                    if (beta <= alpha)
                    {
                        ;
                        break;
                    }
                }

                return v;
            }
        }

        private void PrintTreeStart(Node node)
        {
            while (node.PreviousNode != null) node = node.PreviousNode;

            PrintNode(node, 0);
        }

        private void PrintNode(Node node, int depth)
        {
            var tmp = "";
            for (var i = 0; i < depth * 3; i++) tmp += "-";

            tmp += node.Score;
            if (node.Score == 0) return;
            Debug.WriteLine(tmp);
            foreach (var node1 in node.childerNodes ?? new List<Node>()) PrintNode(node1, depth + 1);
        }

        public override PlayerAbstract Clone(Board board)
        {
            return new AlphaBetaSimple(this, board);
        }

        public override Tuple<Point2D, Point2D> GetMove()
        {
            _stopwatch.Start();
            var start = new Node(_depth, Board, _boardEval);
            AlpaBetaMinimax(start, -float.MaxValue, float.MaxValue, _depth, Color);
            if (start.childerNodes == null) return new RandomAI(Board, Color).GetMove();

            PrintTreeStart(start);
            var max = start.childerNodes.Max(node => node.Score);
            _stopwatch.Stop();
            return start.childerNodes.Find(node => node.Score == max).MovePair.ToTuple();
        }
    }
}