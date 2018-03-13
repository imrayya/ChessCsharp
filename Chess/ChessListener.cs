using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Basic;

namespace Chess
{
    public interface ChessListener
    {
        void MoveEvent(Board board);
        void Draw();
        void Winner(Color color);
    }
}
