using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

namespace TicTacToe
{
    /// <summary>
    /// Логика взаимодействия для Multiplayer.xaml
    /// </summary>
    public partial class Multiplayer : Window
    {
        private IPAddress _currentIP;
        public Multiplayer()
        {
            InitializeComponent();

            IPHostEntry iPHost = Dns.GetHostEntry(Dns.GetHostName());
            _currentIP = iPHost.AddressList.FirstOrDefault(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            if (_currentIP == null)
            {
                MessageBox.Show("Не удалось настроить мультиплеер.");
                Close();
                return;
            }
            IPCurrent.IP = _currentIP.ToString();
        }

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            MultiplayerServer mpServer = new MultiplayerServer(_currentIP);
            mpServer.StartServer();
            mpServer.StartClient();
        }
    }
}
