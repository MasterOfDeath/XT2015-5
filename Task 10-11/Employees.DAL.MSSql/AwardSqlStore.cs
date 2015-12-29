namespace Employees.DAL.MSSql
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using Contract;
    using Entites;

    public class AwardSqlStore : IAwardStore
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["MSSql"].ConnectionString;

        public int AddAward(Award award)
        {
            if (award.Id == 0)
            {
                return this.AppendAward(award);
            }
            else
            {
                return this.InsertAward(award);
            }
        }

        public bool DeleteAward(int awardId)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Awards_DeleteAward";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@awardId", awardId);

                connection.Open();
                var result = command.ExecuteNonQuery();

                return result > 0;
            }
        }

        public Tuple<byte[], string> GetAvatar(int awardId)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Awards_GetImage";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@awardId", awardId);

                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var image = (byte[])reader["image"];
                    var imageType = (string)reader["image_type"];
                    return new Tuple<byte[], string>(image, imageType);
                }
                else
                {
                    return null;
                }
            }
        }

        public Award GetAwardById(int awardId)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Awards_GetAward";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@awardId", awardId);

                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return this.RowToAward(reader);
                }
                else
                {
                    return null;
                }
            }
        }

        public ICollection<Award> ListAllAwards()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Awards_GetAllAwards";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                List<Award> list = new List<Award>();

                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(this.RowToAward(reader));
                }

                return (list.Count != 0) ? list : null;
            }
        }

        public IEnumerable<Award> ListAwardsByUserId(int userId)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Awards_GetAwardsOfUser";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@userId", userId);

                List<Award> list = new List<Award>();

                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(this.RowToAward(reader));
                }

                return list;
            }
        }

        public bool PresentAward(int userId, int awardId)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Awards_PresentAward";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@awardId", awardId);

                connection.Open();
                var result = command.ExecuteNonQuery();

                return result > 0;
            }
        }

        public bool PullOffAward(int userId, int awardId)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Awards_PullOffAward";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@awardId", awardId);

                connection.Open();
                var result = command.ExecuteNonQuery();

                return result > 0;
            }
        }

        public bool SaveAvatar(int awardId, byte[] imageArray, string imageType)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Awards_SaveImage";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@awardId", awardId);
                command.Parameters.AddWithValue("@image", imageArray);
                command.Parameters.AddWithValue("@mimeType", imageType);

                connection.Open();
                var result = command.ExecuteNonQuery();

                return result > 0;
            }
        }

        private int AppendAward(Award award)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Awards_AddAward";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@title", award.Title);

                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    award.Id = (int)(decimal)reader["newId"];
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        private int InsertAward(Award award)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Awards_InsertAward";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@title", award.Title);
                command.Parameters.AddWithValue("@awardId", award.Id);

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        private Award RowToAward(SqlDataReader reader)
        {
            var id = (int)reader["id"];
            var title = (string)reader["title"];

            return new Award(id, title);
        }
    }
}
