using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Core.Chat
{
    class Program
    {
        const short IP_PORT = 11111;
        const string END_MESSAGE = "Fly Safe";

        static void Main(string[] args)
        {
            StartClient();
        }

        static void StartClient()
        {
            try
            {
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHost.AddressList[0];
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, IP_PORT);

                Socket client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    client.Connect(ipEndPoint);

                    Console.WriteLine($"[INITIALIZATION] Connected to {client.RemoteEndPoint.ToString()}");

                    while (true)
                    {
                        string message = Console.ReadLine() + char.MinValue;
                        byte[] messageSent = Encoding.ASCII.GetBytes(message);
                        int byteSent = client.Send(messageSent);
                        byte[] messageReceived = new byte[1024];
                        int byteReceived = client.Receive(messageReceived);
                        string data = Encoding.ASCII.GetString(messageReceived, 0, byteReceived);

                        Console.WriteLine($"{data}");

                        if (data.Equals(END_MESSAGE))
                        {
                            break;
                        }
                    }

                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                }
                catch (ArgumentNullException exc)
                {
                    Console.WriteLine($"ArgumentNullException: {exc.ToString()}");
                }
                catch (SocketException exc)
                {
                    Console.WriteLine($"SocketException: {exc.ToString()}");
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Exception: {exc.ToString()}");
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Exception: {exc.ToString()}");
            }
        }
    }
}
