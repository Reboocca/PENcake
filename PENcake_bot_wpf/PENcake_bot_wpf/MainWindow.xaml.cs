using EV3WifiLib;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace PENcake_bot_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Global 
        private int chosenVorm          = 0;
        private EV3Wifi myEv3           = new EV3Wifi();
        private DispatcherTimer dTimer  = new DispatcherTimer();

        #endregion
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if(chosenVorm == 0)
            {
                Melding m = new Melding("shape", this);
                grBlack.Visibility = Visibility.Visible;
                m.ShowDialog();
            }
            else
            {
                //Stuur bericht
                sendVorm(chosenVorm);

                //Laad scherm
                grBlack.Visibility = Visibility.Visible;
                Laadscherm form = new Laadscherm(this, myEv3);
                form.ShowDialog();
            }
        }

        public void hideGrid()
        {
            grBlack.Visibility = Visibility.Hidden;
        }

        private void lbVorm_Selected(object sender, RoutedEventArgs e)
        {
            switch (lbVorm.SelectedIndex)
            {
                case 0:
                    chosenVorm = 1;
                    break;
                case 1:
                    chosenVorm = 2;
                    break;
                case 2:
                    chosenVorm = 3;
                    break;
            }
        }

        private void connectEv3()
        {
            dTimer.Interval = TimeSpan.FromMilliseconds(100);
            dTimer.Tick += dTimer_Tick;

            if (myEv3.Connect("1234", System.IO.File.ReadAllText("Resources/ev3_ip.txt")) == false)
            {
                //kan niet connecten
                myEv3.Disconnect();
                Melding m = new Melding("connect", this);
                grBlack.Visibility = Visibility.Visible;
                m.ShowDialog();
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void dTimer_Tick(object sender, EventArgs e)
        {
            //Leest data terug (NIET AF)
            if (myEv3.isConnected)
            {
                // EV3: ReceiveMessage is asynchronous so it actually gets the message read at the previous call to ReceiveMessage
                //      and it triggers reading the next message from the specified mailbox.
                //      Due to the simple implementation the message read does not contain information of the mailbox it came from.
                //      Therefore the advise is to only use one mailbox to read from: EV3_OUTBOX0.
                string strMessage = myEv3.ReceiveMessage("EV3_OUTBOX0");
                if (strMessage != "")
                {
                    string[] data = strMessage.Split(' ');
                    if (data.Length == 2)
                    {
                    }
                }
            }
        }

        public void sendVorm(int vorm)
        {

            if (!myEv3.isConnected)
            {
                connectEv3();
            }
            
            //Stuur welk vorm de gebruiker uitgeprint wilt hebben
            myEv3.SendMessage("Vorm " + vorm.ToString(), "0");
        }
    }
}
