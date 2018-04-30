using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Minesweeper.Models;

namespace Minesweeper.Services.Data {

    /// <summary>
    /// Provides methods to 
    /// do CRUD operations on Gamestate
    /// </summary>
    public class GameStateDAO {

        private static string connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;Connect Timeout=15;Encrypt=False;
                  TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private static string DBSelect = @"use Minesweeper;";

        /// <summary>
        /// Checks to see if given player 
        /// is in db
        /// </summary>
        /// <exception cref="SqlException"></exception>
        /// <param name="PlayerID">PlayerID</param>
        /// <returns>bool result</returns>
        public bool PlayerIDInDatabase(int PlayerID) {

            bool PlayerIdInDB = false;

            string query = $"{DBSelect} SELECT * from dbo.GameState where player_id = @PlayerID";

            //Create connection and command
            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn)) {

                //Open the connection
                cn.Open();

                //Set query parameters and their values
                cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = PlayerID;

                // check if player in db
                using (SqlDataReader reader = cmd.ExecuteReader()) {

                    // checks if any rows in db
                    if (reader.Read()) {
                        PlayerIdInDB = true;
                    }

                }

                int result = cmd.ExecuteNonQuery();
            }


            return PlayerIdInDB;
        }

        /// <summary>
        /// Returns a PlayerStatModel,
        /// Looks up info by PlayerId
        /// Will Return null if not found
        /// </summary>
        /// <exception cref="SqlException"></exception>
        /// <param name="PlayerID"></param>
        /// <returns>PlayerStatModel</returns>
        public PlayerStatModel GetPlayerStatModelById(int PlayerID) {
            PlayerStatModel Result = null;

            try {
                string query =
                    $"{DBSelect} SELECT player_id, number_of_clicks, seconds_playing from dbo.GameState where player_id = @player_id;";
                //Create connection and command
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn)) {
                    //Open the connection
                    cn.Open();

                    cmd.Parameters.AddWithValue("@player_id", PlayerID);

                    // use a reader to get all rows from db
                    using (SqlDataReader Reader = cmd.ExecuteReader()) {
                        if (Reader.HasRows) {
                            // fill Stats List with Every Player
                            while (Reader.Read()) {
                                Result = new PlayerStatModel(Reader.GetInt32(0), Reader.GetInt32(1),
                                    Reader.GetInt32(2));
                                break;
                            }
                        }
                    }

                    int result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e) {
                Debug.WriteLine(e.StackTrace);
            }


            return Result;
        }

        /// <summary>
        /// returns null if db doesnt find anythin
        /// returns string (of the game state)
        /// </summary>
        /// <param name="PlayerID"></param>
        /// <exception cref="SqlException"></exception>
        /// <returns>string GameState(currently stored as json)</returns>
        public string GetGameJsonByID(int PlayerID) {
            string Result = null;

            string query = $"{DBSelect} SELECT game_json from dbo.GameState where player_id = @player_id;";
            //Create connection and command
            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn)) {
                //Open the connection
                cn.Open();

                cmd.Parameters.AddWithValue("@player_id", PlayerID);

                // use a reader to get all rows from db
                using (SqlDataReader Reader = cmd.ExecuteReader()) {
                    if (Reader.HasRows) {
                        // fill Stats List with Every Player
                        while (Reader.Read()) {
                            Result = Reader.GetString(0);
                            break;
                        }
                    }
                }

                int result = cmd.ExecuteNonQuery();
            }


            return Result;
        }

        /// <summary>
        /// returns All PlayerStats in db
        /// empty list if none found
        /// </summary>
        /// <exception cref="SqlException"></exception>
        /// <returns> List of "PlayerStatModel" </returns>
        public List<PlayerStatModel> GetAllPlayerStats() {
            List<PlayerStatModel> Stats = new List<PlayerStatModel>();

            string query =
                $"{DBSelect} SELECT player_id, number_of_clicks, seconds_playing from dbo.GameState;";

            //Create connection and command
            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn)) {
                //Open the connection
                cn.Open();

                // use a reader to get all rows from db
                using (SqlDataReader reader = cmd.ExecuteReader()) {
                    if (reader.HasRows) {
                        // fill Stats List with Every Player
                        while (reader.Read()) {
                            PlayerStatModel Tmp =
                                new PlayerStatModel(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
                            Stats.Add(Tmp);
                        }
                    }
                }

                int result = cmd.ExecuteNonQuery();
            }


            return Stats;
        }

        /// <summary>
        /// takes a playerid, 
        /// gameboard in a json string,
        /// jsonified stats
        /// </summary>
        /// <param name="PlayerID"></param>
        /// <param name="GameJson"></param>
        /// <param name="Stat"></param>
        /// <exception cref="SqlException"></exception>
        public void UpdateGameState(int PlayerID, string GameJson, PlayerStatModel Stat) {
            // string query
            string query =
                $"{DBSelect} UPDATE dbo.GameState  SET game_json = @GameJson, number_of_clicks = @number_of_clicks, seconds_playing = @seconds_playing  WHERE player_id = @PlayerID; ";

            //Create connection and command
            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn)) {
                //Open the connection
                cn.Open();

                //Set query parameters and their values
                cmd.Parameters.AddWithValue("@PlayerID", PlayerID);
                cmd.Parameters.AddWithValue("@GameJson", GameJson);
                cmd.Parameters.AddWithValue("@seconds_playing", Stat.SecondsPlaying);
                cmd.Parameters.AddWithValue("@number_of_clicks", Stat.NumberOfClicks);

                int result = cmd.ExecuteNonQuery();
            }
        }


        /**
         * takes player stats and game state in json format
         * then inserts them into the database
         */
        public void InsertGameState(int PlayerID, string GameJson, PlayerStatModel Stat) {
            // check if Game State Needs to be updated
            if (PlayerIDInDatabase(PlayerID)) {
                Console.WriteLine("In Update");
                UpdateGameState(PlayerID, GameJson, Stat);
                return;
            }


            string query =
                $@"{DBSelect} INSERT INTO dbo.GameState (player_id, game_json, number_of_clicks,seconds_playing ) 
                              VALUES (@player_id, @game_json, @number_of_clicks, @seconds_playing);";

            //Create connection and command
            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn)) {
                //Open the connection
                cn.Open();

                //Set query parameters and their values
                cmd.Parameters.AddWithValue("@player_id", PlayerID);
                cmd.Parameters.AddWithValue("@game_json", GameJson);
                cmd.Parameters.AddWithValue("@seconds_playing", Stat.SecondsPlaying);
                cmd.Parameters.AddWithValue("@number_of_clicks", Stat.NumberOfClicks);

                int result = cmd.ExecuteNonQuery();
            }
        }
    }
}