
using ScreenSaver.Classes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace ScreenSaver
{
    public partial class MainForm : Form
    {
        Point snowflakePoint;
        Image snowflake;
        int deltaY = 5;
        Image scene;

        public MainForm()
        {
            InitializeComponent();
            snowflake = Properties.Resources.snowFlake;
            snowflakePoint = new Point(0, 0);
            timer.Interval = 50;
            timer.Start();
            DoubleBuffered = true;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            snowflakePoint.Y += deltaY;
            if (snowflakePoint.Y > ClientRectangle.Height)
            {
                snowflakePoint.X = 50;
                snowflakePoint.Y = 0;
            }
            Invalidate();

        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            Close();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(snowflake, snowflakePoint.X, snowflakePoint.Y, 32,32 );
        }

    }
}
