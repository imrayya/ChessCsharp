namespace Chess.Basic.Pieces
{
    public abstract class Piece
    {
        private PieceEnum _flags;
        private Point2D _positionPoint2D;
        private readonly string _letter;
        private readonly string _name;
        private Point2D[] _moveSet;

        public bool LightColor => FlagsHelper.IsInSet(_flags, PieceEnum.LightColor);
        public bool InPlay => FlagsHelper.IsInSet(_flags, PieceEnum.InPlay);
        public bool FirstMove => FlagsHelper.IsInSet(_flags, PieceEnum.FirstMove);
        public bool MoveRepeat => FlagsHelper.IsInSet(_flags, PieceEnum.MoveRepeat);
        public int Rank => PositionPoint2D.Y - (Color == Color.Black ? 8 : 0);
        public Point2D[] MoveSet => _moveSet;
        public string Letter => _letter;
        public string Name => _name;
        protected void SetMoveSet(Point2D[] moveSet) => _moveSet = moveSet;
        public Color Color { get; }

        public Point2D PositionPoint2D
        {
            get => _positionPoint2D;
            set
            {
                _positionPoint2D = value;
                FlagsHelper.Set(ref _flags, PieceEnum.FirstMove);
                if (value.X % 2 == value.Y % 2)
                {
                    FlagsHelper.Set(ref _flags, PieceEnum.LightColor);
                }
                else
                {
                    FlagsHelper.Unset(ref _flags, PieceEnum.LightColor);
                }
            }
        }

        protected Piece(Piece piece)
        {
            _flags = piece._flags;
            _positionPoint2D = piece._positionPoint2D;
            _letter = piece.Letter;
            _name = piece.Name;
            _moveSet = piece._moveSet;
            Color = piece.Color;
        }

        protected Piece(Point2D positionPoint2D, Color color, string letter, string name, bool moveRepeat)
        {
            _name = name;
            _flags = moveRepeat
                ? PieceEnum.InPlay |
                  PieceEnum.MoveRepeat
                : PieceEnum.InPlay;
            _letter = letter;
            _positionPoint2D = positionPoint2D;
            Color = color;
            if (positionPoint2D.X % 2 == positionPoint2D.Y % 2)
            {
                FlagsHelper.Set(ref _flags, PieceEnum.LightColor);
            }
            else
            {
                FlagsHelper.Unset(ref _flags, PieceEnum.LightColor);
            }
        }

        public Piece OutOfPlay()
        {
            FlagsHelper.Unset(ref _flags, PieceEnum.InPlay);
            return this;
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