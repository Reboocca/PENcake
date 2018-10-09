using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace PENcake_bot_wpf
{
    /// <summary>
    /// Interaction logic for setupScreen.xaml
    /// </summary>
    public partial class setupScreen : Window
    {
        public setupScreen()
        {
            InitializeComponent();
            tbIP.Text = System.IO.File.ReadAllText("Resources/ev3_ip.txt");
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSubmitA_Click(object sender, RoutedEventArgs e)
        {
            if(!IPAddress.TryParse(tbIP.Text, out IPAddress address))
            {
                MessageBox.Show("Invalid ip");
            }
            else
            {
                File.WriteAllText("Resources/ev3_ip.txt", tbIP.Text);

                MainWindow form = new MainWindow();
                this.Close();
                form.Show();
            }
            
        }
    }
}
