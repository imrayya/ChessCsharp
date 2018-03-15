using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Basic;

namespace Chess.Player.AI
{
    class AlphaBeta : PlayerAbstract
    {
        protected AlphaBeta(AlphaBeta playerAbstract, Board board) : base(playerAbstract, board)
        {
        }
        int _depth;
        protected AlphaBeta(Board board, Color color, int depth) : base(board, color, "Alpha Beta")
        {
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
        private float AlpaBetaMinimax(Node node, alpha, beta,int depth)
        {
            if(depth == 0||Rules.GameFinished(node.Board))return node
            if (Color == Color.White)
            {
                
            }
            else
            {
                
            }

        }

        public override PlayerAbstract Clone(Board board) => new AlphaBeta(this,board);
        public override Tuple<Point2D, Point2D> GetMove()
        {
            throw new NotImplementedException();
        }
    }
}
