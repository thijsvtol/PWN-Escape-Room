using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
//using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace pwnDAL
{
    public abstract class Base
    {
        private MySqlDataAdapter adapter;
        private MySqlConnection conn;
        public Base()
        {
            //Connection string in App.config
            conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["PWNDatabase"].ConnectionString);
            adapter = new MySqlDataAdapter();
        }

        protected MySqlConnection OpenConnection()
        {
            if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
            {
                conn.Open();
            }
            return conn;
        }

        private void CloseConnection()
        {
            conn.Close();
        }

        /* For Insert/Update/Delete Queries with transaction */
        protected void ExecuteEditTranQuery(String query, MySqlParameter[] sqlParameters, MySqlTransaction sqlTransaction)
        {
            MySqlCommand command = new MySqlCommand(query, conn, sqlTransaction);
            try
            {
                command.Parameters.AddRange(sqlParameters);
                adapter.InsertCommand = command;
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Kan database niet bewerken/updaten/verwijderen met verhandeling", "Er is een error opgetreden:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                WriteFile("Kan database niet bewerken/updaten/verwijderen met verhandeling");
                throw;
            }
        }

        /* For Insert/Update/Delete Queries */
        protected void ExecuteEditQuery(String query, MySqlParameter[] sqlParameters)
        {
            MySqlCommand command = new MySqlCommand();
            try
            {
                command.Connection = OpenConnection();
                command.CommandText = query;
                command.Parameters.AddRange(sqlParameters);
                adapter.InsertCommand = command;
                command.ExecuteNonQuery();
            }
            catch (MySqlException)
            {
                //Print.ErrorLog(e);
                MessageBox.Show("Kan database niet bewerken/updaten/verwijderen", "Er is een error opgetreden:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                WriteFile("Kan database niet bewerken/updaten/verwijderen");
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }



        /* For Select Queries */
        protected DataTable ExecuteSelectQuery(String query, params MySqlParameter[] sqlParameters)
        {
            MySqlCommand command = new MySqlCommand();
            DataTable dataTable;
            DataSet dataSet = new DataSet();

            try
            {
                command.Connection = OpenConnection();
                command.CommandText = query;
                command.Parameters.AddRange(sqlParameters);
                command.ExecuteNonQuery();
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
                dataTable = dataSet.Tables[0];
            }
            catch (MySqlException)
            {
                // Print.ErrorLog(e);
                MessageBox.Show("Geen geldige querie(s) ingesteld", "Er is een error opgetreden:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                WriteFile("Geen geldige querie(s) ingesteld");
                return null;
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return dataTable;
        }

        public void WriteFile(string s)
        {
            StreamWriter w = new StreamWriter(@"..//..//..//LogFIleErrors.txt", true);
            w.Write("[{0} {1}]\t{2}\r\n-----------------------\r\n", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), s);
            w.Close();
        }
    }
}
