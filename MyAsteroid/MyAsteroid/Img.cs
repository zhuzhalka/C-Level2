using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyAsteroid
{
    class Img : BaseObject
    {
        public Img(Point Pos, Point Dir, Size size) : base(Pos, Dir, size)
        {
        }

        Image ioda = Image.FromFile("ioda.png");

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(ioda, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void ColUpdate()
        {

        }
    }
}