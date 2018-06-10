using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal delegate void ReceivedMessage();

    internal class ReceivedMessageEvents : EventArgs
    {
        internal TransportObjects Message;

        internal event ReceivedMessage ReceivedMessage;

        internal void EvokeReceivedMessage()
        {
            if (ReceivedMessage == null)
                return;

            ReceivedMessage();
        }
    }
}
