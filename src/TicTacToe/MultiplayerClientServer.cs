using System;
using System.Collections.Generic;
using System.IO;
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
        internal TcpClient Client { get; set; }
        internal TcpListener Server { get; set; }

        internal bool IsServer { get; private set; }
        internal bool IsClient { get; private set; }

        internal ReceivedMessageEvents ReceivedMessage { get; set; }

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

        internal void StartServer()
        {
            Server = new TcpListener(CurrentIP, 55466);

            try
            {
                Server.Start();

                Client = Server.AcceptTcpClient();

                IsServer = true;
            }
            catch (SocketException)
            {
                MessageBox.Show("Сервер уже запущен.");
            }
        }

        internal void StartClient(string message)
        {
            Client = new TcpClient(OpponentIP.ToString(), 55466);
            SendMessage(message);

            IsClient = true;
        }

        internal void SendMessage(string text)
        {
            if (Client.Connected)
            {
                NetworkStream serverStream = Client.GetStream();

                byte[] data = Encoding.UTF8.GetBytes(text);
                serverStream.Write(data, 0, data.Length);

                //MessageBox.Show(text);
            }
            else
            {
                MessageBox.Show("Соединение разорвано");
            }
        }

        internal async Task ReadMessageAsync()
        {
            await Task.Run(() => ReadMessage());
        }

        internal void ReadMessage()
        {
            TransportObjects transport = new TransportObjects();

            while (true)
            {
                if (Client.Connected)
                {
                    NetworkStream clientStream = Client.GetStream();

                    byte[] data = new byte[256];

                    int lenghtData;
                    try
                    {
                        lenghtData = clientStream.Read(data, 0, data.Length);
                    }
                    catch (IOException)
                    {
                        //MessageBox.Show("Соединение разорвано.");
                        break;
                    }

                    if (lenghtData > 0)
                    {
                        string textServer = Encoding.UTF8.GetString(data, 0, lenghtData);

                        transport.Parse(textServer);
                    }

                    ReceivedMessage.Message = transport;
                    ReceivedMessage.EvokeReceivedMessage();
                }
                else
                {
                    //MessageBox.Show("Соединение разорвано");
                    break;
                }

            }
        }

        public void Dispose()
        {
            if (Server != null)
            {
                Server.Stop();
            }

            if (Client != null)
            {
                Client.Dispose();
            }
        }
    }
}
