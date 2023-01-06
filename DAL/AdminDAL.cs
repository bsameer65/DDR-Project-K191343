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
    class AdminDAL
    {
        SqlConnection connection = new SqlConnection(Connection.connectionString);

        // Select data from database
        public DataTable SelectData()
        {
            DataTable table = new DataTable();


            try
            {
                string sql = "SELECT * FROM Admin";
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

        // Update Method
        public bool UpdateData(AdminBLL admin)
        {
            try
            {
                string sql = "UPDATE Admin SET name=@name, phone=@phone, email=@email, password=@password, addedDate=@addedDate WHERE id=@id";
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@id", admin.Id);
                command.Parameters.AddWithValue("@name", admin.Name);
                command.Parameters.AddWithValue("@email", admin.Email);
                command.Parameters.AddWithValue("@password", admin.Password);
                command.Parameters.AddWithValue("@phone", admin.Phone);
                command.Parameters.AddWithValue("@addedDate", admin.AddedDate);

                connection.Open();
                command.ExecuteNonQuery();
                

            }
            catch(Exception ex)
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

        //Insert Method
        public bool InsertData(AdminBLL admin)
        {
            try
            {
                string sql = "INSERT INTO Admin(name,phone,email,password,addedDate) VALUES(@name,@phone,@email,@password,@addedDate)";
                SqlCommand command = new SqlCommand(sql, connection);
                
                command.Parameters.AddWithValue("@name", admin.Name);
                command.Parameters.AddWithValue("@phone", admin.Phone);
                command.Parameters.AddWithValue("@email", admin.Email);
                command.Parameters.AddWithValue("@password", admin.Password);
                command.Parameters.AddWithValue("@addedDate", admin.AddedDate);
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

        //Delete Method
        public bool DeleteData(AdminBLL admin)
        {
            try
            {
                string sql = "DELETE FROM Admin WHERE id=@id";
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@id", admin.Id);
                
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

                string sql = "SELECT * FROM Admin WHERE id LIKE '%"+keyword+"%' OR name LIKE '%"+keyword+"%'";
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

        // Login Method
        public bool LoginUser(AdminBLL admin)
        {
            try
            {
                DataTable table = new DataTable();

                string sql = "SELECT * FROM Admin WHERE email=@email AND password=@password";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@email", admin.Email);
                command.Parameters.AddWithValue("@password", admin.Password);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(table);


                if(table.Rows.Count>0)
                {
                    return true;
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
            return false;
        }
    }
}
