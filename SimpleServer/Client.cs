using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace SimpleServer
{
    class Client
    {
        private Socket socket;
        private NetworkStream stream;
        public StreamReader reader { get; set; }
        public StreamWriter writer { get; set; }

        public Client(Socket socket)
        {
            this.socket = socket;
            stream = new NetworkStream(socket, false);
            reader = new StreamReader(stream, Encoding.UTF8);
            writer = new StreamWriter(stream, Encoding.UTF8);
            writer.WriteLine("Client Reader/Writer Set");
            writer.Flush();

        }

        public void Close()
        {
            socket.Close();
        }

        public void sendMessage(string s)
        {
            writer.WriteLine(s);
            writer.Flush();
        }
    }
}
