﻿using System;
using System.Windows.Forms;
using System.Drawing;

namespace MyAsteroid
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        public static int Width { get; set; }
        public static int Height { get; set; }

        static Game()
        {
        }

        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать вбуфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();

            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static BaseObject[] _objs;

        public static void Load()
        {
            Random rnd = new Random();
            _objs = new BaseObject[50];
            for (int i = 0; i < _objs.Length; i = i + 2)
                _objs[i] = new Img(new Point(rnd.Next(0, 600), rnd.Next(0, 800)), new Point(rnd.Next(-5, 5), rnd.Next(-5, 5)),
                    new Size(rnd.Next(10, 40), rnd.Next(10, 40)));
            for (int i = 1; i < _objs.Length; i = i + 2)
                _objs[i] = new Star(new Point(rnd.Next(0, 600), rnd.Next(0, 800)), new Point(rnd.Next(-20, 0), 0), new Size(1, 1));
        }
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();

        }

        public static void Draw()
        {
            //Проверяем вывод графики
            Buffer.Graphics.Clear(Color.Black);
            Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            Buffer.Render();

            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();

            Buffer.Render();
        }
    }
}