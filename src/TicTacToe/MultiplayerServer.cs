using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace TicTacToe
{
    internal class MultiplayerClientServer : IDisposable
    {
        internal TcpClient _client;
        internal TcpListener _server;

        internal IPAddress CurrentIP { get; set; }
        internal IPAddress OpponentIP { get; set; }

        internal MultiplayerClientServer(string opponentIP = null)
        {
            if (!string.IsNullOrEmpty(opponentIP))
            {
                try
                {
                    OpponentIP = IPAddress.Parse(opponentIP);
                }
                catch (Exception)
                {
                }
            }
        }
        internal bool IsServer { get; private set; }
        internal bool IsClient { get; private set; }

        internal void StartServer()
        {
            _server = new TcpListener(CurrentIP, 55466);
            _server.Start();

            _client = _server.AcceptTcpClient();

            ReadMessage();
            IsServer = true;
        }

        internal void StartClient(string message)
        {
            _client = new TcpClient(OpponentIP.ToString(), 55466);
            SendMessage(message);

            IsClient = true;
        }

        internal void SendMessage(string text)
        {
            NetworkStream serverStream = _client.GetStream();

            byte[] data = Encoding.UTF8.GetBytes(text);
            serverStream.Write(data, 0, data.Length);
        }

        internal TransportObjects ReadMessage()
        {
            TransportObjects transport = new TransportObjects();

            while (!transport.IsFilled)
            {
                NetworkStream clientStream = _client.GetStream();

                byte[] data = new byte[256];
                int lenghtData = clientStream.Read(data, 0, data.Length);

                if (lenghtData > 0)
                {
                    string textServer = Encoding.UTF8.GetString(data, 0, lenghtData);

                    transport.Parse(textServer);

                    if (!string.IsNullOrEmpty(transport.Text))
                        MessageBox.Show(transport.Text);
                }

                Thread.Sleep(1 * 1000);
            }

            return transport;
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
