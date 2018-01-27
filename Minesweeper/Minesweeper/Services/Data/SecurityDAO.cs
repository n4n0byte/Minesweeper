using Minesweeper.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Minesweeper.Services.Data
{
    public class SecurityDAO
    {
        //Setup connection string
        string connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;initial catalog=Test;Integrated Security=SSPI;";


        public bool FindByUser(UserModel user)
        {
            bool result = false;

            try
            {
                //Setup SELECT query with parameters
                string query = "SELECT * FROM dbo.Users WHERE USERNAME=@Username AND PASSWORD=@Password";

                //Create connection and command
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))

                {
                    //Set query parameters and their values
                    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = user.Username;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = user.Password;

                    //Open the connection
                    cn.Open();

                    //Using a DataReader, see if query returns any rows
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        result = true;
                    else
                        result = false;

                    //Close the connection
                    cn.Close();
                }
                return result;
            }
            catch (SqlException e)
            {
                // TODO: should log exception and then throw a custom exception
                throw e;
            }
        }
    }
}