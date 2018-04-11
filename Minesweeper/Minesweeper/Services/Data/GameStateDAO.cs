    using System;
using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;
using System.Web;
    using Minesweeper.Models;

namespace Minesweeper.Services.Data {
    public class GameStateDAO {
        private string connectionString =
                @"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;Connect Timeout=15;Encrypt=False;
                  TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool PlayerIDInDatabase(int PlayerID) {

            bool PlayerIdInDB = false;

            try
            {
                string query = "use Minesweeper; SELECT * from dbo.GameState where player_id = @PlayerID";
                //Create connection and command
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    //Open the connection
                    cn.Open();

                    //Set query parameters and their values
                    cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = PlayerID;

                    using (SqlDataReader Reader = cmd.ExecuteReader()) {

                        if (Reader.Read()) {
                            PlayerIdInDB = true;
                        }

                    }

                    int result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }


            return PlayerIdInDB;
        }


        public PlayerStatModel GetPlayerStatModelById(int PlayerID) {

            PlayerStatModel Result = null;

            try
            {
                string query = "use Minesweeper; SELECT player_id, number_of_clicks, seconds_playing from dbo.GameState where player_id = @player_id;";
                //Create connection and command
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    //Open the connection
                    cn.Open();

                    cmd.Parameters.AddWithValue("@player_id", PlayerID);

                    // use a reader to get all rows from db
                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {

                        if (Reader.HasRows)
                        {
                            // fill Stats List with Every Player
                            while (Reader.Read())
                            {
                                Result = new PlayerStatModel(Reader.GetInt32(0), Reader.GetInt32(1), Reader.GetInt32(2));
                                break;
                            }
                        }

                    }

                    int result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }


            return Result;
        }

        /**
         * returns a list of PlayerStats
         */
        public List<PlayerStatModel> GetAllPlayerStats() {

            List<PlayerStatModel> Stats = new List<PlayerStatModel>();
            
            try
            {
                string query = "use Minesweeper; SELECT player_id, number_of_clicks, seconds_playing from dbo.GameState;";
                //Create connection and command
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    //Open the connection
                    cn.Open();

                    // use a reader to get all rows from db
                    using (SqlDataReader Reader = cmd.ExecuteReader()) {

                        if (Reader.HasRows) {
                            // fill Stats List with Every Player
                            while (Reader.Read()) {
                                PlayerStatModel Tmp = new PlayerStatModel(Reader.GetInt32(0),Reader.GetInt32(1), Reader.GetInt32(2));
                                Stats.Add(Tmp);
                            }
                        }

                    }

                    int result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }


            return Stats;
        }

        /**
         * takes a playerid, 
         * gameboard in a json string, 
         * jsonified stats
         */
        public void UpdateGameState(int PlayerID, string GameJson, PlayerStatModel Stat) {
            string query = "use Minesweeper; UPDATE dbo.GameState  SET game_json = @GameJson, number_of_clicks = @number_of_clicks, seconds_playing = @seconds_playing  WHERE ID = @PlayerID; ";

            try
            {
                //Create connection and command
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    //Open the connection
                    cn.Open();

                    //Set query parameters and their values
                    cmd.Parameters.AddWithValue("@PlayerID", PlayerID);
                    cmd.Parameters.AddWithValue("@GameJson", GameJson);
                    cmd.Parameters.AddWithValue("@seconds_playing", Stat.TimeSpent);
                    cmd.Parameters.AddWithValue("@number_of_clicks", Stat.NumberOfClicks);

                    int result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }


        }



        /**
         * takes player stats and game state in json format
         * then inserts them into the database
         */
        public void InsertGameState(int PlayerID, string GameJson, PlayerStatModel Stat) {

            string query = @"use Minesweeper; INSERT INTO dbo.GameState (player_id, game_json, number_of_clicks,seconds_playing ) 
                            VALUES(@player_id, @game_json, @number_of_clicks, @seconds_playing);";

            try
            {
                //Create connection and command
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    //Open the connection
                    cn.Open();

                    //Set query parameters and their values
                    cmd.Parameters.AddWithValue("@player_id", PlayerID);
                    cmd.Parameters.AddWithValue("@game_json", GameJson);
                    cmd.Parameters.AddWithValue("@seconds_playing", Stat.TimeSpent);
                    cmd.Parameters.AddWithValue("@number_of_clicks", Stat.NumberOfClicks);

                    int result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }

        }

    }
}