using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using fail.hardware.Hotzone.Client;
using fail.hardware.NfcClient;

namespace NfcClientTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private WebNfcListener listener;

        private void button1_Click(object sender, EventArgs e)
        {
            listener = new WebNfcListener(textBox1.Text);
            listener.OnConnected += () =>
            {
                button1.Text = "Connected!";
                button1.Enabled = false;
            };

            listener.OnRegistered += (s) =>
            {
                listView1.Items.Add(s.ToString());
            };

            listener.OnScan += (scan) =>
            {
                
            };
        }
    }
}
