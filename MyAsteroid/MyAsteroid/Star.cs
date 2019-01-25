
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyAsteroid
{
    class Star : BaseObject
    {
        public Star(Point Pos, Point Dir, Size size) : base(Pos, Dir, size)
        {
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
            if (Pos.X > Game.Width) Pos.X = Size.Width + Size.Width;


        }
    }
}