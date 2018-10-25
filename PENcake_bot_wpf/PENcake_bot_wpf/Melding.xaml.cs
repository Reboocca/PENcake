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

namespace PENcake_bot_wpf
{
    /// <summary>
    /// Interaction logic for Melding.xaml
    /// </summary>
    public partial class Melding : Window
    {
        MainWindow main = new MainWindow();
        public Melding(string r, MainWindow f)
        {
            InitializeComponent();
            loadMessage(r);
            main = f;
        }

        private void loadMessage(string reason)
        {
            switch (reason)
            {
                //Can't connect to EV3
                case "connect":
                    gbMelding.Header        = "Verbinden mislukt";
                    tbMessage.Text          = "Kan niet verbinen met de EV3, opgegeven ip adress:" + System.IO.File.ReadAllText("Resources/ev3_ip.txt");
                    break;
                //User didn't choose shape
                case "shape":
                    gbMelding.Header        = "Geen vorm gekozen";
                    tbMessage.Text          = "Kies eerst een vorm voordat je je pannenkoek wilt uitprinten!";
                    break;
            }
        }

        private void bntOK_Click(object sender, RoutedEventArgs e)
        {
            main.hideGrid();
            this.Close();
        }
    }
}
