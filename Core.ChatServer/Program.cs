using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

using Core.ChatServer.Common;
using Core.ChatServer.Logging;

namespace Core.ChatServer
{
    enum MessageType
    {
        Identification,
        Continuation
    }

    class Program
    {
        static void Main(string[] args)
        {
            StartServer();
        }

        static void StartServer()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, Constants.DEFAULT_PORT);

            Socket serverSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            Server server = new Server(serverSocket);

            try
            {
                server.ServerSocket.Bind(ipEndPoint);
                server.ServerSocket.Listen(Constants.DEFAULT_MAX_CONNECTIONS);

                Logger.LogInitialization($"Listening on {server.ServerSocket.LocalEndPoint.ToString()}");

                while (true)
                {
                    Socket clientSocket = server.ServerSocket.Accept();
                    Client client = new Client(clientSocket);

                    Logger.LogModification($"Client {client.Identification} Connected");
                    
                    byte[] bytes = new byte[1024];
                    string data = null;

                    while (true)
                    {
                        while (true)
                        {
                            int nByte = client.ClientSocket.Receive(bytes);
                            data += Encoding.ASCII.GetString(bytes, 0, nByte);

                            if (IsTerminated(data))
                            {
                                break;
                            }
                        }

                        if (GetMessageType(data) == MessageType.Identification)
                        {
                            client.Identification = Util.Normalize(data, 5);

                            Logger.LogIdentification($"Client {client.EndPointString} Identified as {client.Identification}");
                        }

                        data = Util.Normalize(data);

                        if (GetMessageType(data) != MessageType.Identification)
                        {
                            Logger.LogMessage(client, data);
                        }

                        byte[] message = data.Equals(Constants.DEFAULT_END_MESSAGE) ?
                            Encoding.ASCII.GetBytes(Constants.DEFAULT_DISCONNECT_MESSAGE) :
                            Encoding.ASCII.GetBytes(Constants.DEFAULT_ACKNOWLEDGE_MESSAGE);

                        client.ClientSocket.Send(message);

                        if (IsFinished(data))
                        {
                            break;
                        }

                        data = null;
                    }

                    Logger.LogTermination($"Client {client.Identification}");

                    client.ClientSocket.Shutdown(SocketShutdown.Both);
                    client.ClientSocket.Close();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
            }
        }

        static MessageType GetMessageType(string message)
        {
            return message.IndexOf(Constants.DEFAULT_IDENTIFICATION_MESSAGE) > -1 ?
                MessageType.Identification :
                MessageType.Continuation;
        }

        static bool IsTerminated(string message)
        {
            return message.IndexOf(Constants.DEFAULT_TERMINATION_MESSAGE) > -1;
        }

        static bool IsFinished(string message)
        {
            return message.Equals(Constants.DEFAULT_END_MESSAGE);
        }
    }
}
