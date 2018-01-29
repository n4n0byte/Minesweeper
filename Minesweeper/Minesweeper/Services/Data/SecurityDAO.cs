using Minesweeper.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Minesweeper.Services.Data {
    public class SecurityDAO {
        //Setup connection string
        string connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;initial catalog=Minesweeper;Integrated Security=SSPI;";

        public void RegisterUser(UserModel user) {
            string query = "INSERT INTO dbo.Users VALUES(@Username,@Password,@Sex,@Age,@Email,@FirstName,@LastName,@State)";

            try {
                //Create connection and command
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn)) {
                    //Open the connection
                    cn.Open();

                    //Set query parameters and their values
                    cmd.Parameters.AddWithValue("Username", user.Username);
                    cmd.Parameters.AddWithValue("Password", user.Password);
                    cmd.Parameters.AddWithValue("Sex", user.Sex);
                    cmd.Parameters.AddWithValue("Age", user.Age);
                    cmd.Parameters.AddWithValue("Email", user.Email);
                    cmd.Parameters.AddWithValue("FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("LastName", user.LastName);
                    cmd.Parameters.AddWithValue("State", user.State);

                    int result = cmd.ExecuteNonQuery();

                }
            }
            catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }

        }

        public bool CheckIfUserIsRegistered(UserModel user) {
            bool result = false;

            try {
                //Setup SELECT query with parameters
                string query = "SELECT * FROM dbo.Users WHERE USERNAME=@Username OR EMAIL=@EMAIL";

                //Create connection and command
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn)) {
                    //Set query parameters and their values
                    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = user.Username;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = user.Email;
                    //Open the connection
                    cn.Open();

                    //Using a DataReader, see if query returns any rows
                    SqlDataReader reader = cmd.ExecuteReader();
                    result = reader.HasRows;
                }
                return result;
            }
            catch (SqlException e) {
                // TODO: should log exception and then throw a custom exception
                throw e;
            }
        }

        public bool FindByUser(UserModel user) {
            bool result = false;

            try {
                //Setup SELECT query with parameters
                string query = "SELECT * FROM dbo.Users WHERE USERNAME=@Username AND PASSWORD=@Password";

                //Create connection and command
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn)) {
                    //Set query parameters and their values
                    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = user.Username;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = user.Password;

                    //Open the connection
                    cn.Open();

                    //Using a DataReader, see if query returns any rows
                    SqlDataReader reader = cmd.ExecuteReader();
                    result = reader.HasRows;
                }
                
            }
            catch (SqlException e) {
                // TODO: should log exception and then throw a custom exception
                Console.WriteLine(e.StackTrace);
            }
            return result;
        }
    }
}