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

namespace ACM_System.GUI
{
    public partial class Clients : Form
    {
        public Clients()
        {
            InitializeComponent();
        }
        ClientBLL clientBLL = new ClientBLL();
        ClientDAL clientDAL = new ClientDAL();

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            Dashboard display = new Dashboard();
            display.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            Dashboard display = new Dashboard();
            display.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (textBoxname.Text != "" && textBoxemail.Text != "" && textBoxPhone.Text != "" && comboBoxGender.Text != "" 
                && textBoxCity.Text != "" && textBoxAddress.Text != "" && textBoxCity.Text != ""  && textStatus.Text != "" && textBoxNotes.Text != "")
            {
                clientBLL.Name = textBoxname.Text;
                clientBLL.Email = textBoxemail.Text;
                clientBLL.Phone = textBoxPhone.Text;
                clientBLL.Gender = comboBoxGender.Text;
                clientBLL.City = textBoxCity.Text;
                clientBLL.Address = textBoxAddress.Text;
                clientBLL.Status = textStatus.Text;
                clientBLL.Notes = textBoxNotes.Text;
                clientBLL.addedDate = DateTime.Now;

                if(checkMail(textBoxemail.Text))
                {
                    if (clientDAL.InsertData(clientBLL))
                    {
                        MessageBox.Show("Data inserted");
                        DataTable table = clientDAL.SelectData();
                        dgvClient.DataSource = table;
                        reset();
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Valid Mail");
                }

            }
            else
            {
                MessageBox.Show("Please enter all the fields");
            }
        }

        private void Clients_Load(object sender, EventArgs e)
        {
            DataTable table = clientDAL.SelectData();
            dgvClient.DataSource = table;
        }


        public void reset()
        {
            textBoxname.Text = "" ; 
            textBoxemail.Text = ""; 
            textBoxPhone.Text = "" ;
            comboBoxGender.Text = "";
            textBoxCity.Text = ""; 
            textBoxAddress.Text = "";
            textBoxCity.Text = "";
            textStatus.Text = "";
            textBoxNotes.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void dgvClient_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            textID.Text = dgvClient.Rows[rowindex].Cells[0].Value.ToString();
            textBoxname.Text = dgvClient.Rows[rowindex].Cells[1].Value.ToString();
            textBoxemail.Text = dgvClient.Rows[rowindex].Cells[2].Value.ToString();
            textBoxPhone.Text = dgvClient.Rows[rowindex].Cells[3].Value.ToString();
            comboBoxGender.Text = dgvClient.Rows[rowindex].Cells[4].Value.ToString();
            textBoxCity.Text = dgvClient.Rows[rowindex].Cells[5].Value.ToString();
            textBoxAddress.Text = dgvClient.Rows[rowindex].Cells[6].Value.ToString();
            textStatus.Text = dgvClient.Rows[rowindex].Cells[7].Value.ToString();
            textBoxNotes.Text = dgvClient.Rows[rowindex].Cells[8].Value.ToString();
 
        }

        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSearch.Text == " ")
            {
                DataTable table = clientDAL.SelectData();
                dgvClient.DataSource = table;

            }
            else
            {
                DataTable table = clientDAL.SearchData(textBoxSearch.Text);
                dgvClient.DataSource = table;
            }
        }

        private void dgvClient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBoxemail_TextChanged(object sender, EventArgs e)
        {

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
        private void button1_Click(object sender, EventArgs e)
        {
            if (textID.Text != "" && textBoxname.Text != "" && textBoxemail.Text != "" && textBoxPhone.Text != "" && comboBoxGender.Text != ""
                && textBoxCity.Text != "" && textBoxAddress.Text != "" && textBoxCity.Text != "" && textStatus.Text != "" && textBoxNotes.Text != "")
            {
                clientBLL.ID = int.Parse(textID.Text);
                clientBLL.Name = textBoxname.Text;
                clientBLL.Email = textBoxemail.Text;
                clientBLL.Phone = textBoxPhone.Text;
                clientBLL.Gender = comboBoxGender.Text;
                clientBLL.City = textBoxCity.Text;
                clientBLL.Address = textBoxAddress.Text;
                clientBLL.Status = textStatus.Text;
                clientBLL.Notes = textBoxNotes.Text;
                clientBLL.addedDate = DateTime.Now;

                if (checkMail(textBoxemail.Text))
                {
                    if (clientDAL.UpdateData(clientBLL))
                    {
                        MessageBox.Show("Data Updated");
                        DataTable table = clientDAL.SelectData();
                        dgvClient.DataSource = table;
                        reset();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter all the fields");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textID.Text != "")
            {
                if (MessageBox.Show("Are You Sure to Delete this record ?", "Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    clientBLL.ID = int.Parse(textID.Text);

                    if (clientDAL.DeleteData(clientBLL))
                    {
                        MessageBox.Show("Data Deleted");
                        DataTable table = clientDAL.SelectData();
                        dgvClient.DataSource = table;
                        reset();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select the record first");
            }
        }
    }
}
