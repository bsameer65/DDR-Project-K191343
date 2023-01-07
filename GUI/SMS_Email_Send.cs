using ACM_System.BLL;
using ACM_System.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ACM_System.GUI
{
    public partial class SMS_Email_Send : Form
    {
        Lazy<ClientBLL> clientBLL;
        Lazy<ClientDAL> clientDAL;
        public SMS_Email_Send()
        {
            InitializeComponent();
            clientBLL = new Lazy<ClientBLL>(() => new ClientBLL());
            clientDAL = new Lazy<ClientDAL>(() => new ClientDAL());
        }
        
        SendSmsMailDAL mail = new SendSmsMailDAL();
        DataTable MessageTable = new DataTable();

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            Dashboard display = new Dashboard();
            display.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            Dashboard display = new Dashboard();
            display.Show();
        }

        private void SMS_Email_Send_Load(object sender, EventArgs e)
        {
            lblSmsEmail.Text = Dashboard.formType;

            
            DataTable table = clientDAL.Value.SelectData();
            dgvClients.DataSource = table;

            comboBoxCity.DataSource = mail.UniqueCitySelection();
            comboBoxCity.DisplayMember = "city";
            comboBoxCity.ValueMember = "city";

            comboBoxStatus.DataSource = mail.UniqueStatusSelection();
            comboBoxStatus.DisplayMember = "status";
            comboBoxStatus.ValueMember = "status";

            dgvAddedClient.DataSource = "";

            MessageTable.Columns.Add("ID");
            MessageTable.Columns.Add("Name");
            MessageTable.Columns.Add("Email");
            MessageTable.Columns.Add("Phone");
            MessageTable.Columns.Add("Gender");
            MessageTable.Columns.Add("City");
            MessageTable.Columns.Add("Address");
            MessageTable.Columns.Add("Status");
            MessageTable.Columns.Add("Notes");
            MessageTable.Columns.Add("Added Date");
          

        }

        private void textBoxSeach_TextChanged(object sender, EventArgs e)
        {
            if(textBoxSearch.Text == " ")
            {
                DataTable table = clientDAL.Value.SelectData();
                dgvClients.DataSource = table;

            }
            else
            {
                DataTable table = clientDAL.Value.SearchData(textBoxSearch.Text);
                dgvClients.DataSource = table;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable table = clientDAL.Value.SelectData();
            dgvAddedClient.DataSource = table;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            reset();
        }

        public void reset()
        {
            textBoxSubject.Text = "";
            textMessage.Text = "";
            dgvAddedClient.ClearSelection();
            MessageTable.Clear();
            dgvAddedClient.DataSource = "";
        }

        private void dgvClients_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int indexRow = e.RowIndex;
            MessageTable.Rows.Add(
                dgvClients.Rows[indexRow].Cells[0].Value.ToString(),
                dgvClients.Rows[indexRow].Cells[1].Value.ToString(),
                dgvClients.Rows[indexRow].Cells[2].Value.ToString(),
                dgvClients.Rows[indexRow].Cells[3].Value.ToString(),
                dgvClients.Rows[indexRow].Cells[4].Value.ToString(),
                dgvClients.Rows[indexRow].Cells[5].Value.ToString(),
                dgvClients.Rows[indexRow].Cells[6].Value.ToString(),
                dgvClients.Rows[indexRow].Cells[7].Value.ToString(),
                dgvClients.Rows[indexRow].Cells[8].Value.ToString()
                );


            dgvAddedClient.DataSource = MessageTable;
        }
        private void comboBoxCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxCity.Text != "")
            {
                DataTable table = clientDAL.Value.SearchData(comboBoxCity.Text);
                dgvAddedClient.DataSource = table;
            }
        }

        private void comboBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStatus.Text != "")
            {
                DataTable table = clientDAL.Value.SearchData(comboBoxStatus.Text);
                dgvAddedClient.DataSource = table;
            }

        }
         private void btnSend_Click(object sender, EventArgs e)
        {
            string logedUserEmail = Login.userEmail;
            string logeedUserPassword = Login.userPassword;
            int success = 0;
            Console.WriteLine(logedUserEmail);
            Console.WriteLine(logeedUserPassword);

            if (textBoxSubject.Text != "" && textMessage.Text != "" && dgvAddedClient.Rows.Count>1)
            {
                //if(Dashboard.formType == "Email")
                //{
                    for (int i = 0; i < dgvAddedClient.Rows.Count - 1; i++)
                    {
                        try
                        { //pattern implementation
                        string textTo = dgvAddedClient.Rows[i].Cells[2].Value.ToString();
                        MessageBox.Show(textTo);
                        MessageBox.Show(logedUserEmail + " logged with");
                        String subject = textBoxSubject.Text;
                        String msg = textMessage.Text;
                        MailServiceVirtualProxy vp=new MailServiceVirtualProxy(logedUserEmail,logeedUserPassword,
                                                    textTo,subject,msg);
                        vp.sendmail();
                        success++;
                        int ClientID = int.Parse(dgvAddedClient.Rows[i].Cells[0].Value.ToString());
                        InsertDataSmsEmail(ClientID, textMessage.Text, Dashboard.formType, textBoxSubject.Text);
                    }
                       catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            break;
                        }
                    
                    }
                    if (success > 0)
                    {
                        MessageBox.Show(success + " Mail Sent.");
                        reset();
                    }
               
                
            }
            else
            {
                MessageBox.Show("Plead enter all the fields");
            }
        }

        private void dgvClients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void InsertDataSmsEmail(int clientID, string messageText, string status, string subject)
        {
            ShowEmailSmsBLL smsMessageBLL = new ShowEmailSmsBLL();
            ShowEmailSmsDAL smsMessageDAL = new ShowEmailSmsDAL();

            smsMessageBLL.ClientId = clientID;
            smsMessageBLL.Message = messageText;
            smsMessageBLL.status = status;
            smsMessageBLL.Subject = subject;

            smsMessageDAL.InsertData(smsMessageBLL);
        }

        
    }
}
