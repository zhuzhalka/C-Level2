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

        public override void Draw()
        {
            Image ioda = Image.FromFile("C:\\\\Users\\Konstantin\\source\\repos\\MyAsteroid\\ioda.png");
            Game.Buffer.Graphics.DrawImage(ioda, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
    }
}