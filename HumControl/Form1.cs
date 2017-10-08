using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;

namespace HumControl
{

    public partial class Form1 : Form
    {
        const int port = 80;
        const string address = "192.168.1.101";
        private static readonly string ClientRequestStringH = "H";
        private static readonly byte[] ClientRequestBytesH = Encoding.ASCII.GetBytes(ClientRequestStringH);
        private static readonly string ClientRequestStringT = "T";
        private static readonly byte[] ClientRequestBytesT = Encoding.ASCII.GetBytes(ClientRequestStringT);
        private static readonly string ClientRequestStringMAX = "MAX";
        private static readonly byte[] ClientRequestBytesMAX = Encoding.ASCII.GetBytes(ClientRequestStringMAX);
        private static readonly string ClientRequestStringMIN = "MIN";
        private static readonly byte[] ClientRequestBytesMIN = Encoding.ASCII.GetBytes(ClientRequestStringMIN);


        public Form1()
        {
            InitializeComponent();
            ConnectAsTcpClient(ClientRequestBytesH, label3);
            ConnectAsTcpClient(ClientRequestBytesT, label4);
            ConnectAsTcpClient(ClientRequestBytesMAX, textBox2);
            ConnectAsTcpClient(ClientRequestBytesMIN, textBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectAsTcpClient(ClientRequestBytesH, label3);
            ConnectAsTcpClient(ClientRequestBytesT, label4);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ConnectAsTcpClient(ClientRequestBytesH, label3);
            ConnectAsTcpClient(ClientRequestBytesT, label4);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string HMAI = textBox2.Text + textBox1.Text + "HMAI";
            byte[] HMAIB = Encoding.ASCII.GetBytes(HMAI);
            ConnectAsTcpClient(HMAIB);
            ConnectAsTcpClient(ClientRequestBytesMAX, textBox2);
            ConnectAsTcpClient(ClientRequestBytesMIN, textBox1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        public async void ConnectAsTcpClient(byte[] CR, Label label)
        {
            using (var tcpClient = new TcpClient())
            {
                await tcpClient.ConnectAsync("192.168.1.101", 80);
                using (var networkStream = tcpClient.GetStream())
                {
                    await networkStream.WriteAsync(CR, 0, CR.Length);

                    var buffer = new byte[4096];
                    var byteCount = await networkStream.ReadAsync(buffer, 0, buffer.Length);
                    var response = Encoding.ASCII.GetString(buffer, 0, byteCount);
                    label.Text = response;
                }
            }
        }
        public async void ConnectAsTcpClient(byte[] CR, TextBox textBox)
        {
            using (var tcpClient = new TcpClient())
            {
                await tcpClient.ConnectAsync("192.168.1.101", 80);
                using (var networkStream = tcpClient.GetStream())
                {
                    await networkStream.WriteAsync(CR, 0, CR.Length);

                    var buffer = new byte[4096];
                    var byteCount = await networkStream.ReadAsync(buffer, 0, buffer.Length);
                    var response = Encoding.ASCII.GetString(buffer, 0, byteCount);
                    textBox.Text = response;
                }
            }
        }
        public async void ConnectAsTcpClient(byte[] CR)
        {
            using (var tcpClient = new TcpClient())
            {
                await tcpClient.ConnectAsync("192.168.1.101", 80);
                using (var networkStream = tcpClient.GetStream())
                {
                    await networkStream.WriteAsync(CR, 0, CR.Length);
                }
            }
        }
    }
}
