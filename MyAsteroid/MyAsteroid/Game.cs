using System;
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

            //Создание исключения по проверке размеров экрана ДЗ№2 пункт 4   
            if (form.Width > 1000 || form.Height > 1000 || form.Width < 0 || form.Height < 0) throw new ArgumentOutOfRangeException();
                       

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

        public static BaseObject[] _stars;
        public static BaseObject[] _asteroids;
        public static BaseObject _bullet;

        public static void Load()
        {

            Random rnd = new Random();
            _asteroids = new Asteroid[3];
            _stars = new Star[100];
            

            for (int i = 0; i < _stars.Length; i++)
            { 
                _stars[i] = new Star(new Point(rnd.Next(0, 600), rnd.Next(0, 800)), new Point(rnd.Next(-20, 0), 0), new Size(1, 1));
                //Выброс исключения о неправильном размере объкта ДЗ №2 пункт 5
                if(Game._stars[i].Size.Width < 0 || Game._stars[i].Size.Height < 0) throw new MyException("Не правильный размер объекта");
            }

            for (int i = 0; i < _asteroids.Length; i++)
            {
                _asteroids[i] = new Asteroid(new Point(rnd.Next(600, 600), 350 * i), new Point(rnd.Next(-2, 0), 0), new Size(100, 100));
                //Выброс исключения о неправильном размере объкта ДЗ №2 пункт 5
                if (Game._asteroids[i].Size.Width < 0 || Game._asteroids[i].Size.Height < 0) throw new MyException("Не правильный размер объекта");
            }
            _bullet = new Bullet(new Point(0, 400), new Point(-20, 0), new Size(40, 0));

            //Выброс исключения о неправильном размере объкта ДЗ №2 пункт 5
            if (Game._bullet.Size.Width < 0 || Game._bullet.Size.Height < 0) throw new MyException("Не правильный размер объекта");
        }

        public static void Update()
        {
            foreach (Star obj in _stars)
                obj.Update();
            foreach (Asteroid a in _asteroids)
            {
                a.Update();
                if (a.Collision(_bullet))
                {
                    System.Media.SystemSounds.Hand.Play();
                    _bullet.ColUpdate();
                    a.ColUpdate();
                }
            }
            _bullet.Update();

        }

        public static void Draw()
        {
            //Проверяем вывод графики
            Buffer.Graphics.Clear(Color.Black);

            foreach (Star obj in _stars)
                obj.Draw();
            foreach (Asteroid obj in _asteroids)
                obj.Draw();
            _bullet.Draw();

            Buffer.Render();
        }
    }
}