using ACM_System.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACM_System.DAL
{
    class ShowEmailSmsDAL
    {
        SqlConnection connection = new SqlConnection(Connection.connectionString);

        //Delete Method
        public bool DeleteData(ShowEmailSmsBLL emalSms)
        {
            try
            {
                string sql = "DELETE FROM sms_mail WHERE id=@id";
                SqlCommand command = new SqlCommand(sql,connection);
                command.Parameters.AddWithValue("@id", emalSms.Id);
                connection.Open();
                command.ExecuteNonQuery();

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }

        //Search Method
        public DataTable SearchData(string keyword)
        {
            DataTable table = new DataTable();
            try
            {
                string sql = "SELECT * FROM sms_mail WHERE id LIKE '%"+keyword+ "%' OR status LIKE '%"+keyword+"%' ";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                connection.Open();
                adapter.Fill(table);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return table;
        }
        //select Method
        public DataTable SelectData()
        {
            DataTable table = new DataTable();
            try
            {
                string sql = "SELECT * FROM sms_mail";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                connection.Open();
                adapter.Fill(table);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return table;
        }

        //Insert Method
        public bool InsertData(ShowEmailSmsBLL emailSms)
        {
            try
            {
                string sql = "INSERT INTO sms_mail(clientid,message,subject,status) VALUES(@clientid,@message,@subject,@status)";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@clientid", emailSms.ClientId);
                command.Parameters.AddWithValue("@message", emailSms.Message);
                command.Parameters.AddWithValue("@subject", emailSms.Subject);
                command.Parameters.AddWithValue("@status", emailSms.status);
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            catch
            {
                connection.Close();
            }

            return true;
        }
    }
}
