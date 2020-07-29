using System.Net.Sockets;
using System.Drawing;

using Core.ChatServer.Common;

namespace Core.ChatServer
{
    class Client
    {
        public Socket ClientSocket { get; set; }

        private string identification;
        public string Identification
        {
            get
            {
                return identification;
            }
            set
            {
                identification = value;
                ClientColor = Util.GetRandomColor();
            }
        }

        public Color ClientColor { get; set; }

        public string EndPointString
        {
            get
            {
                return ClientSocket.LocalEndPoint.ToString();
            }
        }

        public Client(Socket clientSocket)
        {
            ClientSocket = clientSocket;
            Identification = clientSocket.LocalEndPoint.ToString();
            ClientColor = Util.GetRandomColor();
        }
    }
}
