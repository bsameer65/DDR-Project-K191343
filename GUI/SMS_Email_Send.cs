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

        
    }
}
