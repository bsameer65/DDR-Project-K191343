using ACM_System.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ACM_System.GUI.SMS_Email_Send;
namespace ACM_System
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        public static string formType;

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            Login login = new Login();
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Admin admin = new Admin();
            admin.Show();
            Clients client = new Clients();
            client.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
            Login login = new Login();
            login.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Clients client = new Clients();
            client.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
            ShowEmailSms emailSms = new ShowEmailSms();
            emailSms.Show();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            formType = "On Email Address";
            this.Close();
            SMS_Email_Send message = new SMS_Email_Send();
            message.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            formType = "On Phone Number";
            this.Close();
            SMS_Email_Send message = new SMS_Email_Send();
            message.Show();
        }
    }
}
