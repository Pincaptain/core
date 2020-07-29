using System.Net.Sockets;

namespace Core.ChatServer
{
    class Server
    {
        public Socket ServerSocket { get; set; }

        private string identification;

        public string Identification
        {
            get
            {
                if (identification != null)
                {
                    return identification;
                }

                return EndPointString;
            }
            set { identification = value; }
        }

        public string EndPointString
        {
            get
            {
                return ServerSocket.LocalEndPoint.ToString();
            }
        }

        public Server(Socket serverSocket)
        {
            ServerSocket = serverSocket;
        }

        public Server(Socket serverSocket, string identification)
        {
            ServerSocket = serverSocket;
            Identification = identification;
        }
    }
}
