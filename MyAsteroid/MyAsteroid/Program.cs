using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyAsteroid
{
    static class Program
    {


        static void Main(string[] args)
        {
            Form form = new Form
            {
                //Width = Screen.PrimaryScreen.Bounds.Width,
                //Height = Screen.PrimaryScreen.Bounds.Height
                Width = 900,
                Height = 900
            };
            Game.Init(form);
            form.Show();
            Game.Load();
            Game.Draw();
            Application.Run(form);

        }
    }
}