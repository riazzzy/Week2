using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SimpleClient
{
    public class SimpleClient
    {

        private TcpClient tcpClient;
        private NetworkStream stream;
        private StreamWriter writer { get; set; }
        private StreamReader reader;
        Thread UIThread;
        Form1 form;

        public SimpleClient()
        {
            form = new Form1(this);
            tcpClient = new TcpClient();
            UIThread = new Thread(new ThreadStart(runChatThread));
        }

        public bool Connect(string ipAddress, int port)
        {
            try
            {
                tcpClient.Connect(ipAddress, port);
                stream = tcpClient.GetStream();
                writer = new StreamWriter(stream, Encoding.UTF8);
                reader = new StreamReader(stream, Encoding.UTF8);
                UIThread.Start();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return false;
            }

            return true;
        }

        public void Run()
        {
            Application.Run(form);
            UIThread.Abort();
            tcpClient.Close();
        }

        void runChatThread()
        {
            String userInput;
            while (tcpClient.Connected)
            {
                if ((userInput = reader.ReadLine()) != null)
                {
                    form.UpdateChatWindow(userInput);
                    if (userInput == "end") break;
                }
            }
        }

        public void sendMessageToServer(string s)
        {
            writer.WriteLine(s);
            writer.Flush();
        }

        private void ProcessServerResponse()
        {
            Console.WriteLine("Server says: " + reader.ReadLine());
            Console.WriteLine();
        }
    }
}
