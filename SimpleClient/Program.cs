using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleClient client = new SimpleClient();
            client.Connect("127.0.0.1", 4444);
            client.Run();
        }
    }
}
