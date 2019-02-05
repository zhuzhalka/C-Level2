using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Collections;

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

        private static Timer _timer = new Timer();
        public static Random rnd = new Random();

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

            _timer.Start();
            _timer.Tick += Timer_Tick;

            form.KeyDown += Form_KeyDown;

            Ship.MessageDie += Finish;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static BaseObject[] _stars;
        public static BaseObject _health;
        private static Ship _ship = new Ship(new Point(10, 200), new Point(5, 5), new Size(10, 10));

        public static List<Asteroid> _asteroids = new List<Asteroid>();
        public static List<Bullet> _bullets = new List<Bullet>();

        public static int level;
        public static void Load()
        {
            _stars = new Star[100];         
            _health = new Health(new Point(rnd.Next(0, 600), rnd.Next(0, 600)), new Point(rnd.Next(-10, -5), 0), new Size(20, 20));

            for (int i = 0; i < _stars.Length; i++)
            { 
                _stars[i] = new Star(new Point(rnd.Next(0, 600), rnd.Next(0, 800)), new Point(rnd.Next(-20, 0), 0), new Size(1, 1));
                //Выброс исключения о неправильном размере объкта ДЗ №2 пункт 5
                if(Game._stars[i].Size.Width < 0 || Game._stars[i].Size.Height < 0) throw new MyException("Не правильный размер объекта");
            }

            level = 0;
            CreateAsteroids(level);
        }

        public static void Update()
        {
            //lesson3
            foreach (BaseObject obj in _stars) obj.Update();
            foreach (Bullet b in _bullets) b.Update();
            _health?.Update();
            for (var i = 0; i < _asteroids.Count; i++)
            {
                if (_asteroids[i] == null) continue;
                _asteroids[i]?.Update();

                for (int j=0; j < _bullets.Count; j++)
                { 
                    if (_asteroids[i] != null && _bullets[j].Collision(_asteroids[i]))
                    {
                    
                        System.Media.SystemSounds.Hand.Play();

                        //Реализация журнала (Так и не понимаю как правильно использовать делегаты!!!)
                        _asteroids[i].ColUpdate();
                        Asteroid.CollisionAsteroid += s => { var sw = new System.IO.StreamWriter("log.txt", true); sw.WriteLine(s); sw.Close(); };

                        _asteroids[i] = null;
                        _bullets.RemoveAt(j);
                        _ship.ScorePlus();
                        j--;
                    }
                }
                if (_asteroids[i] == null || !_ship.Collision(_asteroids[i])) continue;
                _ship?.EnergyLow(rnd.Next(1, 10));
                _ship?.ScoreMinus();
                System.Media.SystemSounds.Asterisk.Play();
                if (_ship.Energy <= 0) _ship?.Die();
            }
            
            //Подбор аптечки
            if (_health != null && _ship.Collision(_health))
            {
                _ship.ColUpdate();
                System.Media.SystemSounds.Question.Play();
                _health = null;
                _health = new Health(new Point(0, rnd.Next(0, 600)), new Point(rnd.Next(-10, -5), 0), new Size(20, 20));
            }
        }

        public static void Draw()
        {
            //lesson3

            Buffer.Graphics.Clear(Color.Black);

            foreach (Bullet b in _bullets)
                b.Draw();
            foreach (BaseObject star in _stars)
                star.Draw();

            foreach (Asteroid a in _asteroids)
                a?.Draw();
                       
            _ship?.Draw();
            _health?.Draw();

            //Проверка завершен ли уровень. уровень завершен если все атероиды сбиты. После чего необходимо создать на 1 астероид больше
            int n = 0;
            for (int i = 0; i < _asteroids.Count; i++)
            {
                if (_asteroids[i] == null) n++;
            }
            if (n == _asteroids.Count)
            {
                _asteroids.Clear();
                string l = level.ToString();
                Buffer.Graphics.DrawString(l+" Lelel is Complete", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
                level++;
                CreateAsteroids(level);
                foreach (Asteroid a in _asteroids)
                    a?.Draw();
            }


            if (_ship != null) Buffer.Graphics.DrawString("Energy: " + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            Buffer.Graphics.DrawString("Score: " + _ship.score, SystemFonts.DefaultFont, Brushes.White, 0, 15);

            Buffer.Render();
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) _bullets.Add( new Bullet(new Point(_ship.Rect.X + 5, _ship.Rect.Y + 5), new Point(-30, 0), new Size(40, 0)) );
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }

        public static void Finish()
        {
            _timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }

        //Метод для создания астероида
        public static List<Asteroid> CreateAsteroids(int lvl)
        {
            for (int i = 0; i < lvl; i++)
            {
                _asteroids.Add(new Asteroid(new Point(600, rnd.Next(0, Game.Height)), new Point(rnd.Next(-5, -1), 0), new Size(100, 100)));

                // Реализация журнала
                Asteroid.CreateAsteroid += s => { var sw = new System.IO.StreamWriter("log.txt", true); sw.WriteLine(s); sw.Close(); };
                
            }
            return _asteroids;
        }
    }
}