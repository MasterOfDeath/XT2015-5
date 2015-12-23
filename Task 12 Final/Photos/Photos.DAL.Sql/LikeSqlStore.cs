namespace Photos.DAL.Sql
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using Photos.DAL.Contract;
    using Photos.Entites;

    public class LikeSqlStore : ILikeStore
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["MSSql"].ConnectionString;

        public bool AddLike(Like like)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Like_AddLike";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@photoId", like.PhotoId);
                command.Parameters.AddWithValue("@userId", like.UserId);
                command.Parameters.AddWithValue("@date", like.Date);

                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    like.Id = (int)(decimal)reader["newId"];
                }
                else
                {
                    return false;
                }

                return true;
            }
        }

        public int GetLikesCount(int photoId)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Like_CountLikes";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@photoId", photoId);

                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return (int)reader["count"];
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}
