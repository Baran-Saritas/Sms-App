using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using DataLayer;

namespace MessageList
{
    public partial class Form1 : Form
    {        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            var response =  WebServiceTransaction.SendGetOriginator(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);   // GetOriginator burda

            if (response.IsSuccess == true)
            {
                Form2 form2 = new Form2(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text,response.Originator);
                form2.Show();
            }


        }


    }
}





