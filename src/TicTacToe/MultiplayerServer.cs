using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TicTacToe
{
    internal class MultiplayerServer : IDisposable
    {
        internal TcpClient _client;
        internal TcpListener _server;

        internal IPAddress CurrentIP { get; set; }
        internal IPAddress OpponentIP { get; set; }

        internal async void StartServer()
        {
            _server = new TcpListener(CurrentIP, 55466);
            _server.Start();

            TcpClient tcpClient = await _server.AcceptTcpClientAsync();

            while (true)
            {
                NetworkStream clientStream = tcpClient.GetStream();

                byte[] data = new byte[256];
                int lenghtData = clientStream.Read(data, 0, data.Length);

                if (lenghtData > 0)
                {
                    string textServer = Encoding.UTF8.GetString(data, 0, lenghtData);
                    MessageBox.Show(textServer);
                }
            }
        }

        internal void StartClient()
        {
            _client = new TcpClient(OpponentIP.ToString(), 55466);
            SendMessage("Ок. Я подключился.");
        }

        internal void SendMessage(string text)
        {
            NetworkStream serverStream = _client.GetStream();

            byte[] data = Encoding.UTF8.GetBytes(text);
            serverStream.Write(data, 0, data.Length);
        }

        public void Dispose()
        {
            if (_server != null)
            {
                _server.Stop();
            }

            if (_client != null)
            {
                _client.Dispose();
            }
        }
    }
}
