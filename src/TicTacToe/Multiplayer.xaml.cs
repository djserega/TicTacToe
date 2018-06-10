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
        private MultiplayerClientServer _mpServer;
        private MultiplayerClientServer _mpClient;

        internal MultiplayerClientServer Server { get => _mpServer; }
        internal MultiplayerClientServer Client { get => _mpClient; }

        internal bool IsServer { get; private set; }
        internal bool IsClient { get; private set; }

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
            IPOpponent.IP = _currentIP.ToString();

            TextBlockAwaitingConnection.Visibility = Visibility.Collapsed;
            TextBlockInProgressConnection.Visibility = Visibility.Collapsed;
            TextBlockConnectionOk.Visibility = Visibility.Collapsed;
        }

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            TextBlockAwaitingConnection.Visibility = Visibility.Visible;

            _mpServer = new MultiplayerClientServer()
            {
                ReceivedMessage = ((MainWindow)Owner).ReceivedMessage,
                CurrentIP = _currentIP
            };
            _mpServer.StartServer();

            TextBlockAwaitingConnection.Visibility = Visibility.Collapsed;
            TextBlockConnectionOk.Visibility = Visibility.Visible;

            IsServer = true;
            IsClient = false;
        }

        private void ButtonConnectOpponent_Click(object sender, RoutedEventArgs e)
        {
            if (IPOpponent.IsFilled)
            {
                TransportObjects transport = new TransportObjects
                {
                    TypeTransport = TypeTransportObject.AwaitConnection
                };

                TextBlockInProgressConnection.Visibility = Visibility.Visible;

                _mpClient = new MultiplayerClientServer(IPOpponent.IP)
                {
                    ReceivedMessage = ((MainWindow)Owner).ReceivedMessage,
                };
                _mpClient.StartClient(transport.ToString());

                TextBlockInProgressConnection.Visibility = Visibility.Collapsed;
                TextBlockConnectionOk.Visibility = Visibility.Visible;

                IsServer = false;
                IsClient = true;
            }
            else
            {
                MessageBox.Show("Не указан IP сервера");
                IPOpponent.Focus();
            }
        }

        private void FormMultiplayer_Loaded(object sender, RoutedEventArgs e)
        {
            Left = Owner.Left + 10;
            Top = Owner.Top + 10;
        }
    }
}
