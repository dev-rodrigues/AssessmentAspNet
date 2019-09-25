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

        //private static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=H:\DESENVOLVIMENTO\workspace\ASP\AssessmentAspNet\AssessmentAspNet\App_Data\Assessment.mdf;Integrated Security=True";
        //private static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rodri\source\repos\AssessmentAspNet\AssessmentAspNet\App_Data\Assessment.mdf;Integrated Security=True";
        private static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\DESENVOLVIMENTO\workspace\ASP\AssessmentAspNet\AssessmentAspNet\App_Data\Assessment.mdf;Integrated Security=True";

        public IEnumerable<Friends> GetAllFriends() {

            using (var connection = new SqlConnection(ConnectionString)) {
                var sql =   "select* " +
                            "from            Friends as f " +
                            "where(1 = 1) " +
                            "        and MONTH(f.BirthDate) >= MONTH(GETDATE()) " +
                            "        and DAY(f.BirthDate) <= DAY(GETDATE()) " +
                            " order by          MONTH(f.BirthDate) " +
                            "               ,	DAY(f.BirthDate) ASC ";

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

        public IEnumerable<FriendViewModel> GetFriendByString(string part) {
            using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                var sql = $" SELECT *FROM Friends AS f              " +
                          $" WHERE  f.FristName like '%{part}%'     " +
                          $" or f.LastName like '%{part}%'          ";

                SqlCommand selectCommand = new SqlCommand(sql, connection);

                var list = new List<FriendViewModel>();

                try {
                    connection.Open();

                    using (var reader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection)) {
                        while (reader.Read()) {
                            var nFriend = new FriendViewModel();
                            nFriend.Id = (int)reader["Id"];
                            nFriend.FristName = reader["FristName"].ToString();
                            nFriend.LastName = reader["LastName"].ToString();
                            nFriend.BirthDate = (DateTime)reader["BirthDate"];
                            list.Add(nFriend);
                        }
                    }
                } finally {
                    connection.Close();
                }
                return list;
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

        public void DeleteFriend(int id) {
            using (var connection = new SqlConnection(ConnectionString)) {
                var sql = $"DELETE FROM Friends WHERE Id = {id}";
                var insertCommand = new SqlCommand(sql, connection);

                try {
                    connection.Open();
                    insertCommand.ExecuteNonQuery();
                } finally {
                    connection.Close();
                }
            }
        }
    }
}