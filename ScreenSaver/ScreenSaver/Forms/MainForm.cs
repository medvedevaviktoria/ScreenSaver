using ScreenSaver.Classes;
using System;

namespace ScreenSaver
{
    public partial class MainForm : Form
    {
        const int SNOWFLAKESCOUNT = 150;
        private Snowflake[] Snowflakes;
        readonly Image snowflakeImage = Properties.Resources.snowFlake;
        private readonly Random random = new Random();
        System.Windows.Forms.Timer timer;

        public MainForm()
        {
            InitializeComponent();

            InitializeSnowflakes();
            InitializeTimer();
        }

        private void InitializeSnowflakes()
        {
            Snowflakes = new Snowflake[SNOWFLAKESCOUNT];
            for (int i = 0; i < SNOWFLAKESCOUNT; i++)
            {
                var x = random.Next(ClientSize.Width);
                var y = random.Next(ClientSize.Height);
                var size = random.Next(32, 64);
                var speed = random.Next(1, 5);
                Snowflakes[i] = new Snowflake(x, y, size, speed);
            }
        }

        private void InitializeTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 5;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {

            foreach (var snowflake in Snowflakes)
            {
                snowflake.Y += snowflake.Speed;
                if (snowflake.Y > ClientRectangle.Height)
                {
                    snowflake.X = random.Next(ClientSize.Width);
                    snowflake.Y = -64;
                }
            }
            Refresh();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            Close();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            foreach (var snowflake in Snowflakes)
            {
                e.Graphics.DrawImage(snowflakeImage, snowflake.X, snowflake.Y, snowflake.Size, snowflake.Size);
            }
        }

    }
}
