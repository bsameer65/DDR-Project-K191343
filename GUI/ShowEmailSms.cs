using ACM_System.BLL;
using ACM_System.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACM_System.GUI
{
    public partial class ShowEmailSms : Form
    {
        public ShowEmailSms()
        {
            InitializeComponent();
        }

        ShowEmailSmsDAL smsEmailDAL = new ShowEmailSmsDAL();
        ShowEmailSmsBLL smsEmailBLL = new ShowEmailSmsBLL();

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

        private void ShowEmailSms_Load(object sender, EventArgs e)
        { 

            DataTable table = smsEmailDAL.SelectData();
            dgvRecord.DataSource = table;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            DataTable table = smsEmailDAL.SelectData();
            dgvRecord.DataSource = table;
        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            if(textSearch.Text != "")
            {
                DataTable table = smsEmailDAL.SelectData();
                dgvRecord.DataSource = table;
            }
            else
            {
                DataTable table = smsEmailDAL.SearchData(textSearch.Text);
                dgvRecord.DataSource = table;
            }
        }

        private void dgvRecord_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int indexRow = e.RowIndex;
            textID.Text = dgvRecord.Rows[indexRow].Cells[0].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(textID.Text != "")
            {
                if (MessageBox.Show("Are You Sure to Delete this record ?", "Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    smsEmailBLL.Id = int.Parse(textID.Text);

                    if (smsEmailDAL.DeleteData(smsEmailBLL))
                    {
                        MessageBox.Show("Record Deleted");
                        DataTable table = smsEmailDAL.SelectData();
                        dgvRecord.DataSource = table;
                        textID.Text = "";
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Select the record firts. ");
            }
        }
    }
}
