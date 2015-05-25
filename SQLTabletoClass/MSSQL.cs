using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace SQLTabletoClass
{
    public class MSSQL
    {
        private string ConnectionString = "";
        
        public MSSQL(string connectionString)
        {
            ConnectionString = connectionString;
        }
        
        public string GetDatabaseValue(string QueryString)
        {
            if (ConnectionString == "")
            {
                throw new Exception("Connection String has not been set");
            }
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlCommand myCommand = new SqlCommand(QueryString, myConnection);

            try
            {
                myConnection.Open();
                return myCommand.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                //if (ErrorDetected != null)
                //{
                //    ErrorDetected(CallingFunction + " has produced an error in " + ThisFunction + ": " + ex.Message);
                //}
                return "";
            }
            finally
            {
                myConnection.Close();
                myCommand.Dispose();
            }
        }
        
        public DataTable GetDatabaseTable(string QueryString)
        {
            if (ConnectionString == "")
            {
                throw new Exception("Connection String has not been set");
            }
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlCommand myCommand = new SqlCommand(QueryString, myConnection);

            
            try
            {
                string TableName = "GeneralTable";
                SqlDataAdapter localDataAdapter = default(SqlDataAdapter);
                localDataAdapter = new SqlDataAdapter(myCommand);

                DataSet localDataSet = new DataSet();

                localDataAdapter.Fill(localDataSet, TableName);
                return localDataSet.Tables[0];
            }
            catch (Exception ex)
            {
                string ExceptionMessage = ex.Message;
                throw new Exception(ExceptionMessage);
            }
        }
        
        public bool ExecuteDatabaseCommand(string ExecuteString)
        {
            if (ConnectionString == "")
            {
                throw new Exception("Connection String has not been set");
            }
            SqlConnection myConnection = new SqlConnection(ConnectionString);
            SqlCommand myCommand = new SqlCommand(ExecuteString, myConnection);
            
            myConnection.Open();
            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                //TODO: I need to record this SQL exception somewhere
                return false;
            }
            finally
            {
                myConnection.Close();
            }
            return true;
        }
        
    }
}
