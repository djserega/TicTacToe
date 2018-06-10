using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace TicTacToe
{
    internal class TransportObjects
    {
        internal TypeTransportObject TypeTransport { get; set; }
        internal string Text { get; set; }
        internal Board Board { get; set; }

        internal bool IsFilled { get => ObjectIsFilled(); }

        internal void Clear()
        {
            TypeTransport = TypeTransportObject.Empty;
            Text = string.Empty;
            Board = null;
        }

        private bool ObjectIsFilled()
        {
            return TypeTransport != TypeTransportObject.Empty
                || !string.IsNullOrWhiteSpace(Text)
                || false;
        }

        public override string ToString()
        {
            var serializer = new JavaScriptSerializer();

            string[] message = new string[3];
            message[0] = serializer.Serialize(TypeTransport);
            message[1] = serializer.Serialize(Text);
            if (Board == null)
                message[2] = string.Empty;
            else
                message[2] = serializer.Serialize(Board.ToString());

            return new JavaScriptSerializer().Serialize(message);
        }

        public void Parse(string serializedText)
        {
            var deserializer = new JavaScriptSerializer();

            try
            {
                string[] message = deserializer.Deserialize<string[]>(serializedText);

                TypeTransport = deserializer.Deserialize<TypeTransportObject>(message[0]);
                Text = deserializer.Deserialize<string>(message[1]);
                if (Board == null)
                    Board = new Board();
                Board.Parse(deserializer.Deserialize<string>(message[2]));
            }
            catch (Exception)
            {
            }
        }
    }
}
