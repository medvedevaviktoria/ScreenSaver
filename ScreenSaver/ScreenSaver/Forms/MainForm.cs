using ScreenSaver.Classes;

namespace ScreenSaver
{
    public partial class MainForm : Form
    {
        const int SNOWFLAKESCOUNT = 150;
        private List<Snowflake> Snowflakes;
        readonly Image snowflakeImage = Properties.Resources.snowFlake;
        Image scene;
        private readonly Image backgroundPic = Properties.Resources.winterBackground; 
        private readonly Random random = new();
        System.Windows.Forms.Timer timer;

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод инициализирует список снежинок с случайными параметрами.
        /// </summary>
        private void InitializeSnowflakes()
        {
            Snowflakes = [];
            for (int i = 0; i < SNOWFLAKESCOUNT; i++)
            {
                Snowflakes.Add(new Snowflake
                {
                    X = random.Next(ClientSize.Width),
                    Y = random.Next(ClientSize.Height),
                    Size = random.Next(32, 64),
                    Speed = random.Next(1, 5)
                });
            }
        }

        /// <summary>
        /// Метод настраивает и запускает таймер с интервалом 30 мс.
        /// </summary>
        private void InitializeTimer()
        {
            timer = new System.Windows.Forms.Timer
            {
                Interval = 30
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        /// <summary>
        /// Обработчик события таймера, который обновляет позицию каждой снежинки. После обновления вызывается метод рисования для обновления изображения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object? sender, EventArgs e)
        {
            foreach (Snowflake snowflake in Snowflakes)
            {
                snowflake.Y += snowflake.Speed;
                if (snowflake.Y > ClientRectangle.Height)
                {
                    snowflake.X = random.Next(ClientSize.Width);
                    snowflake.Y = -64;
                }
            }
            MainForm_Paint(this, new PaintEventArgs(CreateGraphics(), ClientRectangle));
        }

        /// <summary>
        /// Обработчик события нажатия клавиши на форме. Закрывает форму при любом нажатии клавиши.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик события перерисовки формы. Рисует фоновое изображение и поверх него все снежинки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            var bg = Graphics.FromImage(scene);
            bg.DrawImage(backgroundPic, 0, 0, Width, Height);
            foreach (Snowflake snowflake in Snowflakes)
            {
                bg.DrawImage(snowflakeImage, snowflake.X, snowflake.Y, snowflake.Size, snowflake.Size);
            }
            e.Graphics.DrawImage(scene, 0,0 );
        }

        /// <summary>
        /// Обработчик изменения размера окна. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            scene = new Bitmap(ClientSize.Width, ClientSize.Height);
            InitializeSnowflakes();
            InitializeTimer();
        }
    }
}
