using ACM_System.BLL;
using ACM_System.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ACM_System
{
    public partial class Login : Form
    {
        
        public Login()
        {
            InitializeComponent();
        }

        AdminBLL adminBLL = new AdminBLL();
        AdminDAL adminDAL = new AdminDAL();


       

        public static string userEmail;
        public static string userPassword;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            System.Windows.Forms.Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(textEmail.Text != "" && textPassword.Text != "")
            {
                adminBLL.Password = textPassword.Text;
                adminBLL.Email = textEmail.Text;
                
                if(checkMail(textEmail.Text))
                {
                    if(adminDAL.LoginUser(adminBLL))
                    {
                        userEmail = textEmail.Text;
                        userPassword = textPassword.Text;
                        this.Hide();
                        Dashboard dashboard = new Dashboard();
                        dashboard.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Please enter valid email or password");
                }
            }
            else
            {
                MessageBox.Show("Please enter all the fields");
            }
            //this.Hide();
            //Dashboard dashboard = new Dashboard();
            //dashboard.Show();
        }

        public bool checkMail(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
