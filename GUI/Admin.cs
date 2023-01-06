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

namespace ACM_System
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        AdminBLL adminBLL = new AdminBLL();
        AdminDAL adminDAL = new AdminDAL();

        private void Admin_Load(object sender, EventArgs e)
        {
            DataTable table = adminDAL.SelectData();
            dgvAdmin.DataSource = table;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(textName.Text != "" && textPhone.Text != "" && textEmail.Text != "" && textPassword.Text != "")
            {
                
                adminBLL.Name = textName.Text;
                adminBLL.Phone = textPhone.Text;
                adminBLL.Email = textEmail.Text;
                adminBLL.Password = textPassword.Text;
                adminBLL.AddedDate = DateTime.Now;

                if(checkMail(textEmail.Text))
                {
                    if (adminDAL.InsertData(adminBLL))
                    {
                        MessageBox.Show("Data inserted");
                        DataTable table = adminDAL.SelectData();
                        dgvAdmin.DataSource = table;
                        reset();
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Valid Email.");
                }
            }
            else
            {
                MessageBox.Show("Please enter all the fields");
            }
        }

        public void reset()
        {
            textID.Text = "";
            textName.Text = ""; 
            textPhone.Text ="";
            textEmail.Text = "";
            textPassword.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            Dashboard display = new Dashboard();
            display.Show();
        }

        private void dgvAdmin_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            textID.Text = dgvAdmin.Rows[rowindex].Cells[0].Value.ToString();
            textName.Text = dgvAdmin.Rows[rowindex].Cells[1].Value.ToString();
            textPhone.Text = dgvAdmin.Rows[rowindex].Cells[2].Value.ToString();
            textEmail.Text = dgvAdmin.Rows[rowindex].Cells[3].Value.ToString();
            textPassword.Text = dgvAdmin.Rows[rowindex].Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textID.Text != "")
            {

                if (MessageBox.Show("Are You Sure to Delete this record ?", "Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    adminBLL.Id = int.Parse(textID.Text);

                    if (adminDAL.DeleteData(adminBLL))
                    {
                        MessageBox.Show("Data Deleted");
                        DataTable table = adminDAL.SelectData();
                        dgvAdmin.DataSource = table;
                        reset();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select the record first");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
          /*  this.Close();
            Form1 update=new Form1();
            update.show();*/
            if(textID.Text != "" && textName.Text != "" && textPhone.Text != "" && textEmail.Text != "" && textPassword.Text != "")
            {
                adminBLL.Id = int.Parse(textID.Text);
                adminBLL.Name = textName.Text;
                adminBLL.Phone = textPhone.Text;
                adminBLL.Email = textEmail.Text;
                adminBLL.Password = textPassword.Text;
                adminBLL.AddedDate = DateTime.Now;

                if (checkMail(textEmail.Text))
                {

                    if (adminDAL.UpdateData(adminBLL))
                    {
                        MessageBox.Show("Data Updated");
                        DataTable table = adminDAL.SelectData();
                        dgvAdmin.DataSource = table;
                        reset();
                    }
                }
            }
        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            if(textSearch.Text == "")
            {
                DataTable table = adminDAL.SelectData();
                dgvAdmin.DataSource = table;
            }
            else
            {
                DataTable table = adminDAL.SearchData(textSearch.Text);
                dgvAdmin.DataSource = table;
            }
        }

        public bool checkMail(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return true;
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
