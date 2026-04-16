using System;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SimplePlatformer
{
    public class GameForm : Form
    {
        // Игровые объекты
        private Player player;
        private Platform[] platforms;

        // Графика
        private BufferedGraphicsContext graphicsContext;
        private BufferedGraphics graphicsBuffer;

        // Управление
        private bool leftPressed = false;
        private bool rightPressed = false;
        private bool isJumping = false;
        private int jumpForce = 0;

        // Физика
        private const float Gravity = 0.8f;
        private const int JumpPower = 15;
        private const int PlayerSpeed = 5;

        // Камера
        private int cameraX = 0;

        public GameForm()
        {
            // Настройки формы
            this.Text = "Simple Platformer";
            this.Size = new Size(800, 600);
            this.DoubleBuffered = true;
            this.KeyPreview = true;

            // Создаем игрока
            player = new Player(100, 450, 30, 30);

            // Создаем платформы
            platforms = new Platform[]
            {
                new Platform(0, 550, 800, 50, Color.DarkGreen), // Земля
                new Platform(200, 450, 100, 20, Color.Brown),
                new Platform(400, 380, 100, 20, Color.Brown),
                new Platform(600, 310, 100, 20, Color.Brown),
                new Platform(800, 240, 100, 20, Color.Brown),
                new Platform(1000, 170, 100, 20, Color.Brown),
                new Platform(1200, 100, 100, 20, Color.Brown),
                new Platform(1400, 450, 100, 20, Color.Brown),
                new Platform(1600, 380, 100, 20, Color.Brown),
                new Platform(1800, 310, 100, 20, Color.Brown),
                new Platform(2000, 240, 100, 20, Color.Brown),
                new Platform(2200, 170, 100, 20, Color.Brown)
            };

            // Настройка буферизованной графики
            graphicsContext = BufferedGraphicsManager.Current;
            graphicsBuffer = graphicsContext.Allocate(this.CreateGraphics(), this.ClientRectangle);

            // Таймер игры
            Timer gameTimer = new Timer();
            gameTimer.Interval = 16; // ~60 FPS
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            // Обработка клавиш
            this.KeyDown += OnKeyDown;
            this.KeyUp += OnKeyUp;
        }

        private void GameLoop(object sender, EventArgs e)
        {
            UpdateGame();
            DrawGame();
        }

        private void UpdateGame()
        {
            // Горизонтальное движение
            if (leftPressed)
                player.X -= PlayerSpeed;
            if (rightPressed)
                player.X += PlayerSpeed;

            // Гравитация
            player.VelocityY += Gravity;
            player.Y += player.VelocityY;

            // Проверка столкновений с платформами
            bool onGround = false;
            foreach (var platform in platforms)
            {
                if (player.GetBounds().IntersectsWith(platform.GetBounds()))
                {
                    // Столкновение сверху
                    if (player.VelocityY > 0 && player.Y + player.Height - player.VelocityY <= platform.Y)
                    {
                        player.Y = platform.Y - player.Height;
                        player.VelocityY = 0;
                        onGround = true;
                        isJumping = false;
                    }
                    // Столкновение снизу
                    else if (player.VelocityY < 0 && player.Y - player.VelocityY >= platform.Y + platform.Height)
                    {
                        player.Y = platform.Y + platform.Height;
                        player.VelocityY = 0;
                    }
                    // Столкновение сбоку
                    else
                    {
                        if (player.X + player.Width > platform.X && player.X < platform.X + platform.Width)
                        {
                            if (rightPressed && player.X + player.Width > platform.X)
                                player.X = platform.X - player.Width;
                            if (leftPressed && player.X < platform.X + platform.Width)
                                player.X = platform.X + platform.Width;
                        }
                    }
                }
            }

            // Прыжок
            if (isJumping && onGround)
            {
                player.VelocityY = -JumpPower;
                isJumping = false;
            }

            // Обновление камеры
            cameraX = (int)(player.X - this.ClientSize.Width / 2);
            if (cameraX < 0) cameraX = 0;
            if (cameraX > 2400 - this.ClientSize.Width) cameraX = 2400 - this.ClientSize.Width;

            // Проверка падения в пропасть
            if (player.Y > this.ClientSize.Height)
            {
                ResetGame();
            }
        }

        private void ResetGame()
        {
            player.X = 100;
            player.Y = 450;
            player.VelocityY = 0;
            leftPressed = false;
            rightPressed = false;
            isJumping = false;
        }

        private void DrawGame()
        {
            Graphics g = graphicsBuffer.Graphics;
            g.Clear(Color.LightSkyBlue);

            // Рисуем платформы
            foreach (var platform in platforms)
            {
                platform.Draw(g, cameraX);
            }

            // Рисуем игрока
            player.Draw(g, cameraX);

            // Рисуем информацию
            g.DrawString($"Position: X={player.X}, Y={player.Y}",
                new Font("Arial", 12), Brushes.Black, 10, 10);
            g.DrawString("Use ARROW KEYS to move, SPACE to jump",
                new Font("Arial", 12), Brushes.Black, 10, 30);

            // Выводим на экран
            graphicsBuffer.Render(this.CreateGraphics());
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left: leftPressed = true; break;
                case Keys.Right: rightPressed = true; break;
                case Keys.Space: isJumping = true; break;
                case Keys.R: ResetGame(); break;
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left: leftPressed = false; break;
                case Keys.Right: rightPressed = false; break;
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameForm());
        }
    }

    public class Player
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float VelocityY { get; set; }

        public Player(float x, float y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            VelocityY = 0;
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)X, (int)Y, Width, Height);
        }

        public void Draw(Graphics g, int cameraX)
        {
            Rectangle rect = new Rectangle((int)X - cameraX, (int)Y, Width, Height);
            g.FillRectangle(Brushes.Red, rect);
            g.DrawRectangle(Pens.DarkRed, rect);

            // Рисуем глаза
            g.FillEllipse(Brushes.White, (int)X - cameraX + Width - 10, (int)Y + 8, 8, 8);
            g.FillEllipse(Brushes.Black, (int)X - cameraX + Width - 8, (int)Y + 10, 4, 4);
        }
    }

    public class Platform
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }

        public Platform(int x, int y, int width, int height, Color color)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Color = color;
        }

        public Rectangle GetBounds()
        {
            return new Rectangle(X, Y, Width, Height);
        }

        public void Draw(Graphics g, int cameraX)
        {
            Rectangle rect = new Rectangle(X - cameraX, Y, Width, Height);
            using (Brush brush = new SolidBrush(Color))
            {
                g.FillRectangle(brush, rect);
            }
            g.DrawRectangle(Pens.Black, rect);
        }
    }
}