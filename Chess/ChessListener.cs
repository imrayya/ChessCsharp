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