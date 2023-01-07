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
    class SendSmsMailDAL
    {
        SqlConnection connection = new SqlConnection(Connection.connectionString);

        //Select Distinct City
        public DataTable UniqueCitySelection()
        {
            DataTable table = new DataTable();
            try
            {
                string sql = "SELECT DISTINCT city FROM Client";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                connection.Open();
                adapter.Fill(table);

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            
            return table;
        }
        //Select Distinct Status
        public DataTable UniqueStatusSelection()
        {
            DataTable table = new DataTable();
            try
            {
                string sql = "SELECT DISTINCT status FROM Client";
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


        
    }
}
