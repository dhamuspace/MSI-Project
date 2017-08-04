using System;
using System.Collections.Generic;
using System.Text;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace frmActivation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please Wait...");
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("Update User_table set Active='False' where LSystemName=convert(varchar(max),(SELECT Host_name()))", con);            
            cmd.ExecuteNonQuery();
            con.Close();
        } 
    }
}
