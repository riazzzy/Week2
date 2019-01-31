using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleClient
{
    public partial class Form1 : Form
    {
        delegate void UpdateChatWindowDelagate(string message);
        UpdateChatWindowDelagate updateChatWindowDelagate;
        SimpleClient client;
        public Form1(SimpleClient client)
        {
            this.client = client;
            InitializeComponent();
            updateChatWindowDelagate = new UpdateChatWindowDelagate(UpdateChatWindow);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = String.Empty;
            s = textBox1.Text;
            textBox1.Text = String.Empty;

            client.sendMessageToServer(s);
        }

        public void UpdateChatWindow(string message)
        {
            if (InvokeRequired)
            {
                Invoke(updateChatWindowDelagate, message);
            }
            else
            {
                richTextBox1.Text += message += "\n";
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
            }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}