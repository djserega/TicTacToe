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
        internal int WinsXs { get; set; }
        internal int WinsOs { get; set; }


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
                || !string.IsNullOrWhiteSpace(Text);
        }

        public override string ToString()
        {
            var serializer = new JavaScriptSerializer();

            string textBoard = string.Empty;
            if (Board != null)
                textBoard = serializer.Serialize(Board.ToString());

            string[] message = new string[5]
            {
                serializer.Serialize(TypeTransport),
                serializer.Serialize(Text),
                textBoard,
                WinsXs.ToString(),
                WinsOs.ToString()
            };

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
                WinsXs = int.Parse(message[3]);
                WinsOs = int.Parse(message[4]);
            }
            catch (Exception)
            {
            }
        }
    }
}
