using AssessmentAspNet.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AssessmentAspNet.Repository {
    public class FriendsRepository {
        public IEnumerable<Friends> GetAllFriends() {
            var ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rodri\source\repos\AssessmentAspNet\AssessmentAspNet\App_Data\Assessment.mdf;Integrated Security=True";
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
    }
}