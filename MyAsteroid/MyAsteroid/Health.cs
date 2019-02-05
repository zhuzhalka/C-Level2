using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyAsteroid
{
    class Health : BaseObject
    {
        public Health(Point Pos, Point Dir, Size size) : base(Pos, Dir, size)
        {
        }
       
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.Green, Pos.X, Pos.Y, Pos.X+10, Pos.Y);
            Game.Buffer.Graphics.DrawLine(Pens.Green, Pos.X+5, Pos.Y-5, Pos.X + 5, Pos.Y+5);
        }
        Random rnd = new Random();
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0)
            {
                Pos.X = Game.Width + Size.Width;
                Pos.Y = rnd.Next(0, 900);
            }
        }

        public override void ColUpdate()
        {
            Pos.X = Game.Width + Size.Width;
            Pos.Y = rnd.Next(0, 900);
        }
    }
}
