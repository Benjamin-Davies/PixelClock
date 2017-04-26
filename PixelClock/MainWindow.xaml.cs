using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PixelClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SpriteManager sm;
        private Image[] numbers;
        private Image period, day;
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            // events
            SizeChanged += MainWindow_SizeChanged;
            PreviewKeyDown += MainWindow_PreviewKeyDown;
            timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 500), DispatcherPriority.Normal, new EventHandler(Timer_Elapsed), Dispatcher);

            // pixelate not blur
            RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.NearestNeighbor);

            sm = new SpriteManager(Properties.Resources.sprites);

            // init the numbers
            numbers = new Image[8];
            int k = 0; // index counter
            for (int i = 0; i < 2; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    var image = new Image();
                    image.SetValue(Grid.ColumnProperty, j);
                    image.SetValue(Grid.RowProperty, i);
                    MainGrid.Children.Add(image);
                    numbers[k] = image;
                    k++;
                }
            }

            // init period
            period = new Image();
            period.SetValue(Grid.ColumnProperty, 5);
            period.SetValue(Grid.RowProperty, 0);
            MainGrid.Children.Add(period);

            // init days
            day = new Image();
            day.SetValue(Grid.ColumnProperty, 5);
            day.SetValue(Grid.RowProperty, 1);
            MainGrid.Children.Add(day);
        }

        private void Timer_Elapsed(object sender, EventArgs e)
        {
            var now = DateTime.Now;

            // numbers
            string s = now.ToString("hhmmddMM");
            for (int i = 0; i < 8; i++)
            {
                numbers[i].Source = sm.Numbers[int.Parse(s[i].ToString()) % 10, i % 4 > 1 ? 1 : 0];
            }

            // period am/pm
            int periodNumber = now.Hour > 11 ? 1 : 0;
            period.Source = sm.Periods[periodNumber];

            // day of week
            int dayNumber = (int)now.DayOfWeek;
            day.Source = sm.Days[dayNumber];
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            WindowStyle = WindowState == WindowState.Maximized ? WindowStyle.None : WindowStyle.SingleBorderWindow;
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    break;
            }
        }
    }
}
