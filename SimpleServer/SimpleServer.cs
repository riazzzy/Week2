using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace SimpleServer
{
    class SimpleServer
    {
        private TcpListener tcpListener;
        private static List<Client> clients;
        public SimpleServer(String ipAddress, int port)
        {
            IPAddress address = IPAddress.Parse(ipAddress);
            tcpListener = new TcpListener(address, port);
            clients = new List<Client>();
        }

        public void Start()
        {
            tcpListener.Start();
            while (true)
            {
                Console.WriteLine("listening");
                Console.WriteLine("Accepted");
                Socket socket = tcpListener.AcceptSocket();
                Client client = new Client(socket);
                clients.Add(client);
                Thread t = new Thread(new ParameterizedThreadStart(ClientMethod));
                t.Start(client);
            }

        }

        public void Stop()
        {
            tcpListener.Stop();
        }

        private static void ClientMethod(object clientObj)
        {
            Client client = (Client)clientObj;
            string recievedMessage;

            while ((recievedMessage = client.reader.ReadLine()) != null)
            {
                string s = GetReturnMessage(recievedMessage);

                if (s == "end")
                {
                    break;
                }
                else
                {
                    Console.WriteLine(recievedMessage);
                    foreach (Client c in clients)
                    {
                        c.writer.WriteLine(s);
                        c.writer.Flush();
                    }
                }
            }
        }

        private static string GetReturnMessage(string recievedMessage)
        {
            if (recievedMessage == "hello")
            {
                return "Hello";
            }
            if (recievedMessage == "bye")
            {
                return "Bye";
            }
            if (recievedMessage == "whats up")
            {
                return "not much";
            }
            if (recievedMessage == "how are you?")
            {
                return "good, what about you?";
            }
            else return "Please type something";
        }

        public static List<Client> getAllClients()
        {
            return clients;
        }
    }
}
