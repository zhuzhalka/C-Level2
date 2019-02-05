using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyAsteroid
{
    class Ship : BaseObject
    {
        private int _energy = 100;
        public int Energy => _energy;
        public int score;

        public void EnergyLow(int n)
        {
            _energy -= n;
        }

        public Ship(Point Pos, Point Dir, Size size) : base(Pos, Dir, size)
        {
        }

        public void ScorePlus()
        {
            score = score + 10;
        }

        public void ScoreMinus()
        {
            score = score - 30;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Wheat, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
        }

        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }

        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }
              
        public static event Message MessageDie;
        public void Die()
        {
            MessageDie?.Invoke();
        }

        public override void ColUpdate()
        {
            _energy = _energy + 10;
        }
    }
}
