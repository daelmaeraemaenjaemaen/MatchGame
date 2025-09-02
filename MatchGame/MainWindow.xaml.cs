using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    using System.Windows.Threading;

    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed = 0;
        int matchesFound;
        bool firstStart = true;
        int life;
        bool isMoon = true;
        bool startTime;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            TimeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                firstStart = false;
                timer.Stop();
                startTime = false;
                TimeTextBlock.Text = TimeTextBlock.Text + " - Play again?";
            }
            
            Life.Text = "Life: " + life.ToString();

            if (life == 0)
            {
                firstStart = false;
                timer.Stop();
                startTime = false;
                TimeTextBlock.Text = "Game Over. Replay?";
            }
        }

        private void SetUpMoonGame()
        {
            List<string> moonEmoji = new List<string>()
            {
                "🌑", "🌑",
                "🌒", "🌒",
                "🌓", "🌓",
                "🌔", "🌔",
                "🌕", "🌕",
                "🌖", "🌖",
                "🌗", "🌗",
                "🌘", "🌘"
            };

            Random random = new Random();
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "TimeTextBlock" && textBlock.Name != "Life" && textBlock.Name != "Moon" && textBlock.Name != "Animal" && textBlock.Name != "Now")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(moonEmoji.Count);
                    string nextemoji = moonEmoji[index];
                    textBlock.Text = nextemoji;
                    moonEmoji.RemoveAt(index);
                }
            }

            timer.Start();
            startTime = true;
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
            life = 3;

        }

        private void SetUpAnimalGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🐶", "🐶",
                "🐺", "🐺",
                "🦁", "🦁",
                "🐯", "🐯",
                "🐮", "🐮",
                "🐷", "🐷",
                "🐭", "🐭",
                "🐼", "🐼"
            };

            Random random = new Random();
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "TimeTextBlock" && textBlock.Name != "Life" && textBlock.Name != "Moon" && textBlock.Name != "Animal" && textBlock.Name != "Now")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);
                    string nextemoji = animalEmoji[index];
                    textBlock.Text = nextemoji;
                    animalEmoji.RemoveAt(index);
                }
            }

            timer.Start();
            startTime = true;
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
            life = 3;

        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;

            if (life != 0)
            {
                if (findingMatch == false)
                {
                    textBlock.Visibility = Visibility.Hidden;
                    lastTextBlockClicked = textBlock;
                    findingMatch = true;
                }
                else if (textBlock.Text == lastTextBlockClicked.Text)
                {
                    matchesFound++;
                    textBlock.Visibility = Visibility.Hidden;
                    findingMatch = false;
                }
                else
                {
                    lastTextBlockClicked.Visibility = Visibility.Visible;
                    findingMatch = false;
                    life--;
                }
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (firstStart)
            {
                if (isMoon)
                {
                    SetUpMoonGame();
                }

                else
                {
                    SetUpAnimalGame();
                }

            }

            if (matchesFound == 8 || life == 0)
            {
                if (isMoon)
                {
                    SetUpMoonGame();
                }

                else
                {
                    SetUpAnimalGame();
                }

            }
        }

        private void Moon_MouseDown(object sender, MouseButtonEventArgs e)
        {   if (!startTime)
            {
                isMoon = true;
                Now.Text = "Now: Moon";
            }
        }

        private void Animal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!startTime)
            {
                isMoon = false;
                Now.Text = "Now: Animal";
            }
        }
    }
}
