using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyAsteroid
{
    class Bullet : BaseObject
    {
        public Bullet(Point Pos, Point Dir, Size size) : base(Pos, Dir, size)
        {
        }

        public override void Draw()
        {

            Game.Buffer.Graphics.DrawLine(Pens.Red, Pos.X, Pos.Y+3, Pos.X+15, Pos.Y);
            Game.Buffer.Graphics.DrawLine(Pens.Red, Pos.X+15, Pos.Y, Pos.X, Pos.Y-3);
            Game.Buffer.Graphics.DrawLine(Pens.Red, Pos.X, Pos.Y-3, Pos.X, Pos.Y+3);
        }

        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
            if (Pos.X > Game.Width) Pos.X = Size.Width + Size.Width;
           
        }
        
        //Метод для возвращения пули в нулевую позицию ДЗ №2 пункт 3
        public override void ColUpdate()
        {
            Pos.X = 0;
        }
    }
}
