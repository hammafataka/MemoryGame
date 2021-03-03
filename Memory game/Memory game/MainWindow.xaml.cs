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

namespace Memory_game
{
    public partial class MainWindow : Window
    {
        Button compButton1 = null;
        Button compButton2 = null;
        bool preesbutton1 = false;
        bool preesbutton2 = false;
        List<int> column = new List<int>();
        List<int> row = new List<int>();
        Random columnLocation = new Random();
        int attempt;
        int gameAttempts = 7;
        float firstWidth;
        float firstHeight;


        public MainWindow()
        {
            
            InitializeComponent();
            
        }
        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {

            
            attemptCounter.Visibility = Visibility.Hidden;
            attemptlbl.Visibility = Visibility.Hidden;
            attemptCounter.Content = gameAttempts;
            attempt = gameAttempts;
     

            DispatcherTimer timer1 = new DispatcherTimer();
            timer1.Tick += timer1_tick;
            timer1.Interval = new TimeSpan(0, 0, 3);

            void timer1_tick(object s, EventArgs es)
            {


                foreach (UIElement element in myGrid.Children)
                {
                    if (element.GetType() == typeof(Button))
                    {


                        Button button = (Button)element;
                        int x = Grid.GetColumn(button);
                        column.Add(x);
                        int y = Grid.GetRow(button);
                        row.Add(y);

                        button.Cursor = Cursors.Hand;
                    }
                }






                foreach (UIElement element in myGrid.Children)
                {
                    if (element.GetType() == typeof(Button))
                    {
                        Button button = (Button)element;
                        Image coverImage = new Image();
                        coverImage.Source = new BitmapImage(new Uri("images/question.jpg", UriKind.Relative));
                        button.Content = coverImage;
                        button.IsEnabled = true;

                    }
                }

                timer1.Stop();
                countdowntimer.Visibility = Visibility.Hidden;
                attemptlbl.Visibility = Visibility.Visible;
                attemptCounter.Visibility = Visibility.Visible;

            }





            DispatcherTimer timer2 = new DispatcherTimer();
            timer2.Tick += timer2_tick;
            timer2.Interval = new TimeSpan(0, 0, 1);

            void timer2_tick(object s, EventArgs es)
            {

                int timer = Convert.ToInt32(countdowntimer.Content.ToString());
                timer = timer - 1;
                countdowntimer.Content = Convert.ToString(timer);
                if (timer == 0)
                    timer2.Stop();



            }
            timer2.Start();
            foreach (UIElement iElement in myGrid.Children)
            {
                if (iElement.GetType() == typeof(Button))
                    iElement.IsEnabled = false;

            }


            timer1.Start();



            foreach (UIElement element in myGrid.Children)
            {
                if (element.GetType() == typeof(Button))
                {
                    Button button = (Button)element;
                    Image foreImage = new Image();
                    foreImage.Source = new BitmapImage(new Uri($"images/{ button.Tag}.jpg", UriKind.Relative));
                    button.Content = foreImage;

                }
            }







        }

        
        private void click_Click(object sender, RoutedEventArgs e)
        {

            Button pressed = (Button)sender;
            Image tmg = new Image();
            tmg.Source = new BitmapImage(new Uri($"images/{ pressed.Tag}.jpg", UriKind.Relative));
            pressed.Content = tmg;



            if (!preesbutton1)
            {
                compButton1 = pressed;
                preesbutton1 = true;
            }
            else if (preesbutton1 && !preesbutton2)
            {
                compButton2 = pressed;
                preesbutton2 = true;

            }
            if (preesbutton1 && preesbutton2)
            {
                if (compButton1.Tag.ToString() == compButton2.Tag.ToString())
                {
                    int x = 0;
                    x = Convert.ToInt32(scoreCounter.Content.ToString());
                    x = x + 1;
                    scoreCounter.Content = Convert.ToString(x);
                    compButton1.IsEnabled = false;
                    compButton2.IsEnabled = false;

                }
                else
                {
                    DispatcherTimer timer3 = new DispatcherTimer();
                    timer3.Interval = new TimeSpan(0, 0, 0, 0, 300);
                    timer3.Start();
                    timer3.Tick += timer3_tick;
                    void timer3_tick(object s, EventArgs es)
                    {
                        timer3.Stop();
                        coverbtn(compButton1);
                        coverbtn(compButton2);
                    }
                    attempt -= 1;
                    attemptCounter.Content = attempt;

                }
                preesbutton1 = false;
                preesbutton2 = false;

            }
            if (Convert.ToInt32(scoreCounter.Content) == 15)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Game finished you won..", "Great", MessageBoxButton.OK);


            }
            if (attempt == 0)
            {
                MessageBox.Show("you lose try again..", "Game over", MessageBoxButton.OK);
                foreach (UIElement element in myGrid.Children)
                {
                    if (element.GetType() == typeof(Button))
                    {
                        Button button = (Button)element;
                        Image image = new Image();
                        image.Source = new BitmapImage(new Uri($"images/question.jpg", UriKind.Relative));
                        button.Content = image;
                    }

                }
                if (Convert.ToInt32(highScoreCounter.Content) > Convert.ToInt32(scoreCounter.Content))
                {

                }
                else
                {
                    highScoreCounter.Content = Convert.ToInt32(scoreCounter.Content);

                }
                scoreCounter.Content = 0;

                attempt = gameAttempts;
                attemptCounter.Content = attempt;

            }

        }
        private void coverbtn(Button button)
        {

            Image coverImage = new Image();
            coverImage.Source = new BitmapImage(new Uri("images/question.jpg", UriKind.Relative));
            button.Content = coverImage;


        }

        private void restert_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(highScoreCounter.Content) > Convert.ToInt32(scoreCounter.Content))
            {

            }
            else
            {
                highScoreCounter.Content = Convert.ToInt32(scoreCounter.Content);

            }
            scoreCounter.Content = 0;
            attemptCounter.Content = gameAttempts;
            attempt = gameAttempts;

            foreach (UIElement element in myGrid.Children)
            {

                if (element.GetType() == typeof(Button))
                {

                    Button button = (Button)element;
                    Image restartImage = new Image();
                    restartImage.Source = new BitmapImage(new Uri($"images/question.jpg", UriKind.Relative));
                    button.Content = restartImage;
                    button.IsEnabled = true;
                }

            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }
    }
}
