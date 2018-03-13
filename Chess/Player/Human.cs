﻿using System;
using Chess.Basic;
using Chess.Player.AI;

namespace Chess.Player
{
    public class Human : PlayerAbstract
    {
        private RandomAI _randomAi;
        public Human(Board board, Color color) : base(board, color, "Human " + color)
        {
            _randomAi= new RandomAI(board,color);
        }

        public Human(Human human) : base(human)
        {
            _randomAi = (RandomAI) human._randomAi.Clone(Board);
        }
        public Human(Human human,Board board) : base(human,board)
        {
            _randomAi = (RandomAI) human._randomAi.Clone(board);
        }
        public override PlayerAbstract Clone(Board board)
        {
            return new Human(this,board);
        }
        public override Tuple<Point2D, Point2D> GetMove()
        {

            string input = Console.ReadLine();
            string[] split = input.Split(' ');
            if (split[0] == "ran") return _randomAi.GetMove();
            var start = new Point2D(split[0]);
            var end = new Point2D(split[1]);
            return new Tuple<Point2D, Point2D>(start, end);
        }
    }
}