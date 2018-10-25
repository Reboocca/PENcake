using EV3WifiLib;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PENcake_bot_wpf
{
    /// <summary>
    /// Interaction logic for Laadscherm.xaml
    /// </summary>
    public partial class Laadscherm : Window
    {
        #region Global
        MainWindow form = new MainWindow();
        EV3Wifi myEv3   = new EV3Wifi();

        private DispatcherTimer dt  = new DispatcherTimer();
        private int increment       = 0;
        private bool toggled        = false;
        private bool soundplayed    = false;

        #endregion

        public Laadscherm(MainWindow f, EV3Wifi ev3 )
        {
            InitializeComponent();
            form = f;
            myEv3 = ev3;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Maak timer aan voor het laden
            dt.Interval         = TimeSpan.FromMilliseconds(500);
            dt.Tick += dtTicker;
            dt.Start();
        }

        private void dtTicker(object sender, EventArgs e)
        {
            //Zorg voor "Printen . . " animatie
            switch (tbLaad.Text)
            {
                case "Printen":
                    tbLaad.Text = "Printen.";
                    break;
                case "Printen.":
                    tbLaad.Text = "Printen..";
                    break;
                case "Printen..":
                    tbLaad.Text = "Printen...";
                    break;
                case "Printen...":
                    tbLaad.Text = "Printen";
                    break;

            }

            //Zolang % lager is dan 50, blijf laden
            if(increment < 50)
            {
                increment += 1;
                pbLaad.Value = increment;

                lblTime.Content = increment.ToString();
            }
            else if (toggled)
            {
                //Gebruiker heeft aangegeven gedraaid is
                gbScreen.Header = "Je pannenkoek is bijna klaar";
                spRotate.Visibility = Visibility.Hidden;
                spLoading.Visibility = Visibility.Visible;

                increment += 1;
                pbLaad.Value = increment;

                lblTime.Content = increment.ToString();

            }
            else
            {
                //Pannenkoek is op 50%, meldt draaien
                if (!soundplayed)
                {
                    gbScreen.Header = "Draai de pannenkoek";
                    spLoading.Visibility = Visibility.Hidden;
                    spRotate.Visibility = Visibility.Visible;

                    myEv3.SendMessage("Rotate" , "0");
                }
            }

            //Pannenkoek is 100%, dus klaar
            if(increment == 100)
            {
                gbScreen.Header = "Je pannenkoek is klaar";
                dt.Stop();

                myEv3.SendMessage("Done", "0");

                spLoading.Visibility    = Visibility.Hidden;
                spDone.Visibility       = Visibility.Visible;
            }
        }

        private void btnRotated_Click(object sender, RoutedEventArgs e)
        {
            toggled = true;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            form.hideGrid();
            this.Close();

        }
    }
}
