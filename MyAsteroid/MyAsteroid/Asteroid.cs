using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyAsteroid
{
    class Asteroid : BaseObject
    {
        public Asteroid(Point Pos, Point Dir, Size size) : base(Pos, Dir, size)
        {
        }

        public override void Draw()
        {
            Color customColor = Color.FromArgb(98, Color.Aqua);
            SolidBrush brush = new SolidBrush(customColor);

            Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
            Game.Buffer.Graphics.FillEllipse(brush, Pos.X, Pos.Y, Size.Width, Size.Height);

        }
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
            if (Pos.X > Game.Width) Pos.X = Size.Width + Size.Width;
        }

        //Метод дляреализации ДЗ №2 пункт 3
        public override void ColUpdate()
        {
            Pos.X = 800;
        }
    }
}
