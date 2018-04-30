using Minesweeper.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Minesweeper.Services.Utility;

namespace Minesweeper.Services.Data {

  
    /// <summary>
    /// Handles tasks related to authentication and 
    /// registration
    /// </summary>
    public class SecurityDAO {

        //Setup connection string
        string connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;initial catalog=Minesweeper;Integrated Security=SSPI;";

        /// <summary>
        /// attempts to register user   
        /// </summary>
        /// <param name="user"></param>
        public void RegisterUser(UserModel user) {
            string query =
                "INSERT INTO dbo.Users VALUES(@Username,@Password,@Sex,@Age,@Email,@FirstName,@LastName,@State)";

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

        /// <summary>
        /// Checks if user is registered
        /// </summary>
        /// <param name="user"></param>
        /// <returns>bool userRegistered</returns>
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
                
                throw e;
            }
        }

        /// <summary>
        /// Retrieves the userId of a user
        /// given username and password
        /// will be -1 if none found
        /// </summary>
        /// <param name="user"></param>
        /// <returns>UserID</returns>
        public int GetUserId(UserModel user) {

            int id = -1;

            // try to get userID
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

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        id = int.Parse(reader["id"].ToString());
                        break;
                    }
                }
            }
            catch (SqlException e) {
                throw e;
            }

            return id;
        }

        /// <summary>
        /// returns a bool indicating
        /// whether or not a user was found,
        /// searches by username
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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
                throw e;
            }
            return result;
        }
    }
}