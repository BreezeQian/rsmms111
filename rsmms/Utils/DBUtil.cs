using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace rsmms.Utils
{
    public class DBUtil
    {
        private static String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
        "AttachDbFilename='|DataDirectory|\\rgmms.mdf';";

        /**
         *  查询
         */ 
        public static SqlDataReader ExecuteReader(string strSQL)
        {
        
             SqlConnection connection = new SqlConnection(connectionString);
             SqlCommand cmd = new SqlCommand(strSQL, connection);
             try
             {
                 connection.Open();
                 SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                 return myReader;
             }
             catch (System.Data.SqlClient.SqlException e)
             {
                 throw e;
             }   
 
        }

        /**
         * 增、删、改
         */ 
        public static int ExecuteNonQuery(string strSQL)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(strSQL, connection);
            try
            {
                connection.Open();
                int result = cmd.ExecuteNonQuery();
                return result;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
    }
}