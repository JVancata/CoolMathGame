using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Data;
using System.Timers;
using System.Windows.Threading;

namespace KonecneOknoXD
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public int counter = 0;
        //ObservableCollection<Person> persons = new ObservableCollection<Person>();
        public static int difficulty = 2;        
        public static int correct;
        
        private Random r = new Random();
        int timeCounter = 0;
        public void Render()
        {
            int[] numbers = new int[difficulty];
            Problem.Content = "";
            int operatorGame = r.Next(1, 4);
            //wholeList.ItemsSource = persons;
            for (int i = 0; i < difficulty; i++)
            {
                numbers[i] = r.Next(0, 11);
                Problem.Content += numbers[i].ToString();

                if (i + 1 < difficulty)
                {
                    operatorGame = r.Next(1, 4);
                    switch (operatorGame)
                    {
                        case 1:
                            Problem.Content += " + ";
                            break;
                        case 2:
                            Problem.Content += " - ";
                            break;
                        case 3:
                            Problem.Content += " * ";
                            break;
                        default:
                            break;
                    }
                }

            }
            DataTable dt = new DataTable();
            //string trueResult;
            //if(r.Next(1, 2) == 1)
            int btnRand = r.Next(1, 3);
            Button correctBtn;
            Button wrongBtn;
            if (btnRand == 1)
            {
                correctBtn = Button1;
                wrongBtn = Button2;
            }
            else
            {
                correctBtn = Button2;
                wrongBtn = Button1;
            }
            // = btnRand == 1 ? Button1 : Button2;
            // = btnRand == 2 ? Button1 : Button2;


            correctBtn.Content = dt.Compute((string)Problem.Content, "");
            if(r.Next(1,2) == 1)
            {
                wrongBtn.Content = (int)dt.Compute((string)Problem.Content, "") - r.Next(1, 5);
            }
            else
            {
                wrongBtn.Content = (int)dt.Compute((string)Problem.Content, "") + r.Next(1, 5);
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            Render();
            Result.Content = "";
            
            DispatcherTimer t = new DispatcherTimer();
            t.Interval = new TimeSpan(0, 0, 0, 0, 10);
            t.Tick += (sender, args) => { x(timeCounter); };
            t.Start();
        }
        private void x(int time)
        {
            timeCounter++;
            TestTimer.Content = timeCounter/100;
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*counter++;
            FirstLabel.Content = counter;
             persons.Add(new Person("name " + r.Next()));
             */
            Button btn = sender as Button;
            string s = (string)btn.Content.ToString();

            DataTable dt = new DataTable();

            
            if (s == dt.Compute((string)Problem.Content, "").ToString() )
            {
                Result.Foreground = System.Windows.Media.Brushes.Green;
                pBar.Value += 10;
                Result.Content = "Correct!!!";
            }            
            else
            {
                Result.Foreground = System.Windows.Media.Brushes.Red;
                if (pBar.Value > 0)
                {
                    pBar.Value -= 10;
                }
                Result.Content = "Wrong!!!";
            }
            if (pBar.Value >= 100)
            {
                difficulty++;
                pBar.Value = 0;
            }

            Render();
        }
    }
}
