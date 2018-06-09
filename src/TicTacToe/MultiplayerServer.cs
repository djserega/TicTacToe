using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class MultiplayerServer
    {
        private IPAddress _currentIP;

        internal MultiplayerServer(IPAddress currentIP)
        {
            _currentIP = currentIP;
        }

        internal async void StartServer()
        {
            TcpListener server = new TcpListener(_currentIP, 55466);
            server.Start();
            
            TcpClient tcpClient = await server.AcceptTcpClientAsync();

            NetworkStream clientStream = tcpClient.GetStream();

            byte[] data = new byte[256];
            int lenghtData = clientStream.Read(data, 0, data.Length);

            string textServer = Encoding.UTF8.GetString(data, 0, lenghtData);

            server.Stop();
        }

        internal async void StartClient()
        {
            TcpClient client = new TcpClient(_currentIP.ToString(), 55466);
            NetworkStream serverStream = client.GetStream();

            byte[] data = Encoding.UTF8.GetBytes("Ок. Я подключился.");
            serverStream.Write(data, 0, data.Length);
        }
    }
}
