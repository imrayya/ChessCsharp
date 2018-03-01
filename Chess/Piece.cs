namespace Chess
{
    public abstract class Piece
    {
        public Point2D PositionPoint2D
        {
            get => _positionPoint2D;
            set
            {
                _positionPoint2D = value;
                _firstMove = true;
            }
        }

        protected void SetMoveSet(Point2D[] moveSet)
        {
            _moveSet = moveSet;
        }

        private bool _firstMove = false;
        private Point2D _positionPoint2D;
        private readonly string _letter;
        private readonly string _name;
        private readonly bool _moveRepeat;
        private Point2D[] _moveSet;
        public bool InPlay = true;

        public bool FirstMove => _firstMove;
        public Point2D[] MoveSet => _moveSet;
        public string Letter => _letter;
        public string Name => _name;
        public bool MoveRepeat => _moveRepeat;

        public Color Color { get; }

        protected Piece(Piece piece)
        {
            _firstMove = piece._firstMove;
            _positionPoint2D = piece._positionPoint2D;
            _letter = piece.Letter;
            _name = piece.Name;
            _moveRepeat = piece.MoveRepeat;
            _moveSet = piece._moveSet;
            InPlay = piece.InPlay;
            Color = piece.Color;
        }

        protected Piece(Point2D positionPoint2D, Color color, string letter, string name, bool moveRepeat)
        {
            _name = name;
            _moveRepeat = moveRepeat;
            _letter = letter;
            _positionPoint2D = positionPoint2D;
            Color = color;
        }

        public abstract Piece Clone();
        
        public override string ToString()
        {
            return Name + " at " + _positionPoint2D.ToCoor();
        }
    }

    public class Knight : Piece
    {
        public Knight(Knight knight) : base(knight)
        {
        }

        public Knight(Point2D positionPoint2D, Color color) : base(positionPoint2D, color, "n", "Knight", false)
        {
            var tmp = new Point2D[]
            {
                new Point2D(1, 2), new Point2D(1, -2), new Point2D(-1, 2), new Point2D(-1, -2),
                new Point2D(2, 1), new Point2D(-2, 1), new Point2D(2, -1), new Point2D(-2, -1)
            };
            SetMoveSet(tmp);
        }

        public override Piece Clone()
        {
            return new Knight(this);
        }
    }

    public class Pawn : Piece
    {
        private Point2D[] _firstMoveSet;

        public Point2D[] FirstMoveSet => _firstMoveSet;

        private Point2D[] _attackSet;

        public Point2D[] AttackSet => _attackSet;

        public Pawn(Pawn pawn) : base(pawn)
        {
            _firstMoveSet = pawn._firstMoveSet;
            _attackSet = pawn._attackSet;
        }

        public Pawn(Point2D positionPoint2D, Color color) : base(positionPoint2D, color, "p", "Pawn", false)
        {
            var tmp = color == Color.Black
                ? new Point2D[] {new Point2D(0, 1)}
                : new Point2D[] {new Point2D(0, -1)};
            SetMoveSet(tmp);
            _attackSet = color == Color.Black
                ? new Point2D[] {new Point2D(1, 1), new Point2D(-1, 1)}
                : new Point2D[] {new Point2D(1, -1), new Point2D(-1, -1)};
            _firstMoveSet = color == Color.Black
                ? new Point2D[] {new Point2D(0, 2)}
                : new Point2D[] {new Point2D(0, -2)};
        }

        public override Piece Clone()
        {
            return new Pawn(this);
        }
    }

    public class Rook : Piece
    {
        public Rook(Rook rook) : base(rook)
        {
        }

        public Rook(Point2D positionPoint2D, Color color) : base(positionPoint2D, color, "r", "Rook", true)
        {
            var tmp = new Point2D[] {new Point2D(1, 0), new Point2D(-1, 0), new Point2D(0, 1), new Point2D(0, -1)};
            SetMoveSet(tmp);
        }

        public override Piece Clone()
        {
            return new Rook(this);
        }
    }

    public class Bishop : Piece
    {
        public Bishop(Bishop bishop) : base(bishop)
        {
        }

        public Bishop(Point2D positionPoint2D, Color color) : base(positionPoint2D, color, "b", "Bishop", true)
        {
            var tmp = new Point2D[] {new Point2D(1, 1), new Point2D(-1, 1), new Point2D(1, -1), new Point2D(-1, -1)};
            SetMoveSet(tmp);
        }

        public override Piece Clone()
        {
            return new Bishop(this);
        }
    }

    public class King : Piece
    {
        public King(King king) : base(king)
        {
        }

        public King(Point2D positionPoint2D, Color color) : base(positionPoint2D, color, "k", "King", false)
        {
            var tmp = new Point2D[]
            {
                new Point2D(1, 0), new Point2D(-1, 0), new Point2D(0, 1), new Point2D(0, -1), new Point2D(1, 1),
                new Point2D(-1, 1), new Point2D(1, -1), new Point2D(-1, -1)
            };
            SetMoveSet(tmp);
        }

        public override Piece Clone()
        {
            return new King(this);
        }
    }

    public class Queen : Piece
    {
        public Queen(Queen queen) : base(queen)
        {
        }

        public Queen(Point2D positionPoint2D, Color color) : base(positionPoint2D, color, "q", "Queen", true)
        {
            var tmp = new Point2D[]
            {
                new Point2D(1, 0), new Point2D(-1, 0), new Point2D(0, 1), new Point2D(0, -1), new Point2D(1, 1),
                new Point2D(-1, 1), new Point2D(1, -1), new Point2D(-1, -1)
            };
            SetMoveSet(tmp);
        }

        public override Piece Clone()
        {
            return new Queen(this);
        }
    }
}