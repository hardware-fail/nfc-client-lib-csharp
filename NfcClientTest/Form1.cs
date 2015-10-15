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
                
                button1.Invoke(new Action(()=>
                {
                    button1.Text = "Connected!";
                    button1.Enabled = false;
                }));
            };

            listener.OnScannerRegistered += (s) =>
            {
                listView1.Invoke(new Action(() =>
                {
                    listView1.Items.Add(s.ToString());
                }));
            };

            listener.OnScannerDisconnected += (s) =>
            {
                listView1.Invoke(new Action(() =>
                {
                    listView1.Items.Clear();
                    foreach (var i in listener.Scanners)
                        listView1.Items.Add(i.ToString());
                }));
            };


            listener.OnScanned += (scan) =>
            {
                textBox2.Invoke(new Action(() =>
                {
                    textBox2.Text += $"{scan.Scanner.ToString()} scanned card {scan.CardId}{Environment.NewLine}";
                }));
            };
            listener.Connect();
        }
    }
}
