    using System;
using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;
using System.Web;

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

        /**
         * takes a playerid, 
         * gameboard in a json string, 
         * jsonified stats
         */
        public void UpdateGameState(int PlayerID, string GameJson, string StatsJson) {
            string query = "use Minesweeper; UPDATE dbo.GameState  SET game_json = @GameJson, player_stat_json = @StatsJson  WHERE ID = @PlayerID; ";

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
                    cmd.Parameters.AddWithValue("@StatsJson", StatsJson);

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
        public void InsertGameState(int PlayerID, string GameJson, string StatsJson) {

            string query = @"use Minesweeper; INSERT INTO dbo.GameState (player_id, game_json,player_stat_json)  VALUES(@player_id,@game_json,@player_stat_json);";

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
                    cmd.Parameters.AddWithValue("@player_stat_json", StatsJson);

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