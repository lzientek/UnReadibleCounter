using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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
using Timer = System.Timers.Timer;

namespace UnReadableClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public const int Largeur = 9;
        public const int Longueur = 7;
        private int count = 0;
        public ObservableCollection<bool>[] DisplayBoxes { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Height = Longueur*50;
            Width = Largeur*50;
            DisplayBoxes = new ObservableCollection<bool>[Longueur];
            for (int i = 0; i < Longueur; i++)
            {
                DisplayBoxes[i] = (new ObservableCollection<bool>());
                for (int j = 0; j < Largeur; j++)
                {
                    DisplayBoxes[i].Add(false);
                }
            }
            DataContext = this;

            FillGrid(count);
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += TimerOnElapsed;
            timer.Start();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            count++;
            if (count == 100)
            {
                timer.Stop();
                return;
            }
            Application.Current.Dispatcher.BeginInvoke(
  DispatcherPriority.Normal,
  new Action(() => FillGrid(count)));

        }


        public void FillGrid(int value)
        {
            var displayGrid = ValueConverter(value);
            for (int i = 0; i < Longueur; i++)
            {
                for (int j = 0; j < Largeur; j++)
                {
                    DisplayBoxes[i].RemoveAt(j);
                    DisplayBoxes[i].Insert(j, displayGrid[i][j]);
                }
            }
        }

        private bool[][] un = {
            new[] {false,false,true},
            new[] {false,false,true},
            new[] { false, false, true},
            new[] { false, false,true},
            new[] { false, false, true},
        };

        private bool[][] deux = {
            new[] {true,true,true},
            new[] {false,false,true},
            new[] {true,true,true},
            new[] {true,false,false},
            new[] {true,true,true},
        };
        private bool[][] trois = {
            new[] {true,true,true},
            new[] {false,false,true},
            new[] {true,true,true},
            new[] {false,false,true},
            new[] {true,true,true},
        };
        private bool[][] quattre = {
            new[] {true,false,true},
            new[] {true,false,true},
            new[] {true,true,true},
            new[] {false,false,true},
            new[] {false,true,true},
        };
        private bool[][] cinq = {
            new[] {true,true,true},
            new[] {true,false,false},
            new[] {true,true,true},
            new[] {false,false,true},
            new[] {true,true,true},
        };
        private bool[][] six = {
            new[] {true,true,true},
            new[] {true,false,false},
            new[] {true,true,true},
            new[] {true,false,true},
            new[] {true,true,true},
        };
        private bool[][] sept = {
            new[] {true,true,true},
            new[] {false,false,true},
            new[] { false, false, true},
            new[] { false, false,true},
            new[] { false, false, true},
        };
        private bool[][] huit = {
            new[] {true,true,true},
            new[] {true,false,true},
            new[] {true,true,true},
            new[] {true,false,true},
            new[] {true,true,true},
        };
        private bool[][] neuf = {
            new[] {true,true,true},
            new[] {true,false,true},
            new[] {true,true,true},
            new[] {false,false,true},
            new[] {true,true,true},
        };
        private bool[][] zero = {
            new[] {true,true,true},
            new[] {true,false,true},
            new[] {true,false,true},
            new[] {true,false,true},
            new[] {true,true,true},
        };

        private Timer timer;


        private bool[][] ValueConverter(int value)
        {
            //init
            var ret = new bool[Longueur][];
            for (int i = 0; i < Longueur; i++)
            {
                ret[i] = new bool[Largeur];
            }

            if (value < 10)
            {
                ret = Merge(ret, zero, true);
                ret = Merge(ret, ValueToBoolNb(value), false);
            }
            else if (value < 100)
            {
                ret = Merge(ret, ValueToBoolNb(value / 10), true);
                ret = Merge(ret, ValueToBoolNb(value % 10), false);
            }

            return ret;
        }

        private bool[][] ValueToBoolNb(int value)
        {
            switch (value)
            {
                case 0:
                    return zero;
                case 1:
                    return un;
                case 2:
                    return deux;
                case 3:
                    return trois;
                case 4:
                    return quattre;
                case 5:
                    return cinq;
                case 6:
                    return six;
                case 7:
                    return sept;
                case 8:
                    return huit;
                case 9:
                    return neuf;
            }
            return zero;
        }

        private bool[][] Merge(bool[][] tab, bool[][] nombre, bool first)
        {
            int start = 0, maxLarg = Largeur / 2;
            if (!first)
            {
                start = maxLarg;
                maxLarg = Largeur;
            }
            for (int i = 0; i < Longueur; i++)
            {
                for (int j = start; j < maxLarg; j++)
                {
                    if (i == 0 || j == start || i - 1 >= nombre.Length || j - 1 - start >= nombre[0].Length)
                    {
                        tab[i][j] = false;
                    }
                    else
                    {
                        tab[i][j] = nombre[i - 1][j - 1 - start];
                    }
                }
            }

            return tab;
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_OnClickRestart(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            count = 0;
            FillGrid(count);
            timer.Start();
        }

        private void Deplacement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
    }
}
