using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Chess;
using Chess.Basic;
using Chess.Basic.Pieces;
using Chess.Player;
using Chess.Player.AI;
using Color = System.Drawing.Color;

namespace Visual
{
    public partial class Form1 : Form, ChessListener
    {
        private Brush _brush = new SolidBrush(Color.Black);
        private int _current;
        private readonly string _path = "C:\\Users\\Imray\\source\\repos\\ChessCsharp\\Visual\\resources\\";
        private readonly Pen _pen = new Pen(Color.Black);

        private bool _play = true;

        private readonly Timer _timer1;

        public List<Board> Board;

        public Form1(Board board)
        {
            Board = new List<Board>();
            Board.Add(board);
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        public Form1()
        {
            InitializeComponent();
            _timer1 = new Timer();
            _timer1.Interval = 500; //0.5 seconds
            _timer1.Tick += timer1_Tick;
            _timer1.Start();
            trackBar2.SetRange(100, 1000);
            trackBar2.Value = 500;
        }

        public void MoveEvent(Board board)
        {
            if (Board == null)
                Board = new List<Board>();
            Board.Add(board);
            trackBar1.SetRange(0, Board.Count - 1);
        }


        public void Draw()
        {
        }

        public void Winner(Chess.Basic.Color color)
        {
            throw new NotImplementedException();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PaintCustom();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void PaintCustom()
        {
            var width = Canvas.Width / 8;
            var height = Canvas.Height / 8;
            var g1 = Canvas.CreateGraphics();
            Image tmp = new Bitmap(Canvas.Width, Canvas.Height);
            var g = Graphics.FromImage(tmp);
            g.FillRectangle(new SolidBrush(Canvas.BackColor), 0, 0, Canvas.Width, Canvas.Height);
            for (var x = 0; x < 8; x++)
            for (var y = 0; y < 8; y++)
                g.DrawRectangle(_pen, x * width, y * height, width, height);

            if (Board != null && _current >= Board.Count) _current = Board.Count - 1;


            if (Board != null && Board.Count != 0)
                foreach (var p in Board[_current].AllPieces1)
                {
                    if (!p.InPlay) continue;
                    var image = Image.FromFile(ImagePath(p));
                    image = ResizeImage(image, width, height);
                    g.DrawImage(image, new Point(p.PositionPoint2D.X * width, p.PositionPoint2D.Y * height));
                }


            if (_play)
            {
                if (Board != null)
                {
                    trackBar1.Value = _current;
                    labelTurn.Text = "Turn " + _current;
                }

                _current++;
            }

            g1.DrawImage(tmp, 0, 0);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private string ImagePath(Piece piece)
        {
            return _path + piece.Color.ToString().ToLower() + piece.Name + ".png";
        }

        /// <summary>
        ///     Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }


        private void Canvas_Resize(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var tmp = new Board();
            if (Board == null)
                Board = new List<Board>();
            Board.Clear();
            trackBar1.SetRange(0, 1);
            _current = 0;
            Board.Add(tmp);
            PlayerAbstract whitePlayer =
                new AlphaBetaSimple(tmp, Chess.Basic.Color.White, 2, BoardEval.SimpleTable);
            PlayerAbstract blackPlayer =
                new AlphaBetaSimple(tmp, Chess.Basic.Color.Black, 2, BoardEval.Table);
//            PlayerAbstract whitePlayer = new RandomAI(tmp, Chess.Basic.Color.White);
//            PlayerAbstract blackPlayer = new Greedy1Ply(tmp, Chess.Basic.Color.Black);

            var result = GameLoop.Game(tmp, whitePlayer, blackPlayer, this);
            labelBlack.Text = blackPlayer.Name + " " + result.Item4 + "ms";
            labelWhite.Text = whitePlayer.Name + " " + result.Item3 + "ms";
            MessageBox.Show(result.Item1 == Chess.Basic.Color.NoColor ? "Draw" : "Winner is " + result.Item1);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            _current = trackBar1.Value;
            PaintCustom();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _play = !_play;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            _timer1.Interval = trackBar2.Value;
        }
    }
}