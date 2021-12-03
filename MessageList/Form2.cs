using DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MessageList
{
    public partial class Form2 : Form
    {

        string UserName = "";
        string Password = "";
        string UserCode = "";
        string AccountId = "";
        string Originator = "";

        public Form2()
        {
            InitializeComponent();
        }
        public Form2(string UserName, string Password, string UserCode, string AccountId,string Originator)
        {

            this.UserName = UserName;
            this.Password = Password;
            this.UserCode = UserCode;
            this.AccountId = AccountId;
            this.Originator = Originator;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {

                string TemplateText = "";
                for (int i = 0; i < textBox2.Lines.Length; i++)   // Gonderilecek mesaj 
                {
                TemplateText += textBox2.Lines[i];
                }

                var GsmNumbers = new List<string>();
                for (int i = 0; i < textBox1.Lines.Length; i++)       // Buraya Atilicak telefon listesi girilicek
                {
                GsmNumbers.Add(textBox1.Lines[i]);
                }

                 var Title = "";
                if (textBox3.Text == string.Empty)    // title
                {
                    MessageBox.Show("Title giriniz lütfen");
                }
                else
                {
                     Title = textBox3.Text;
                }

                var resp = WebServiceTransaction.SendSms(UserName, Password, UserCode, AccountId, Originator, Title, TemplateText, GsmNumbers);
        }


    }
}
