using AssessmentAspNet.Domain;
using AssessmentAspNet.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AssessmentAspNet.Repository {
    public class FriendsRepository {

        private static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rodri\source\repos\AssessmentAspNet\AssessmentAspNet\App_Data\Assessment.mdf;Integrated Security=True";

        public IEnumerable<Friends> GetAllFriends() {

            using (var connection = new SqlConnection(ConnectionString)) {
                var sql = "SELECT *FROM Friends";
                var selectCommand = new SqlCommand(sql, connection);

                var friends = new List<Friends>();

                try {
                    connection.Open();

                    using (var reader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection)) {
                        while (reader.Read()) {
                            var friend = new Friends();
                            friend.Id = (int)reader["Id"];
                            friend.FristName = reader["FristName"].ToString();
                            friend.LastName = reader["LastName"].ToString();
                            friend.BirthDate = (DateTime)reader["BirthDate"];
                            friends.Add(friend);
                        }
                    }
                } finally {
                    connection.Close();
                }
                return friends;
            }
        }

        public FriendViewModel GetFriendById(int IdFriend) {

            using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                var sql = $" SELECT *FROM Friends AS f" +
                          $" WHERE  f.Id = {IdFriend}";

                SqlCommand selectCommand = new SqlCommand(sql, connection);

                var fd = new FriendViewModel();

                try {
                    connection.Open();

                    using (var reader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection)) {
                        while (reader.Read()) {
                            fd.Id = (int)reader["Id"];
                            fd.FristName = reader["FristName"].ToString();
                            fd.LastName = reader["LastName"].ToString();
                            fd.BirthDate = (DateTime)reader["BirthDate"];
                        }
                    }
                } finally {
                    connection.Close();
                }
                return fd;
            }
        }

        public int InsertFriend(string FristName, string LastName, DateTime BirthDate) {
            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();
                var sql = "INSERT INTO Friends (FristName, LastName, BirthDate) VALUES (@FristName, @LastName, @BirthDate)";
                SqlCommand selectCommand = new SqlCommand(sql, connection);
                selectCommand.Parameters.AddWithValue("@FristName", FristName);
                selectCommand.Parameters.AddWithValue("@LastName", LastName);
                selectCommand.Parameters.AddWithValue("@BirthDate", BirthDate);
                return selectCommand.ExecuteNonQuery();
            }
        }

        public int UpdateFriend(FriendViewModel FriendUpdated) {            
            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();
                var sql = "UPDATE Friends SET " +
                          "FristName = @FristName, " +
                          "LastName = @LastName, " +
                          "BirthDate = @BirthDate " +
                          $"WHERE Id = {FriendUpdated.Id}";
                SqlCommand selectCommand = new SqlCommand(sql, connection);
                selectCommand.Parameters.AddWithValue("@FristName", FriendUpdated.FristName);
                selectCommand.Parameters.AddWithValue("@LastName", FriendUpdated.LastName);
                selectCommand.Parameters.AddWithValue("@BirthDate", FriendUpdated.BirthDate);
                return selectCommand.ExecuteNonQuery();
            }
        }
    }
}