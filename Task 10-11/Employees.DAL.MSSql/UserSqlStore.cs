namespace Employees.DAL.MSSql
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using Contract;
    using Entites;

    public class UserSqlStore : IUserStore
    {
        private readonly string connectionString = 
            ConfigurationManager.ConnectionStrings["MSSql"].ConnectionString;

        public bool AddUser(User user)
        {
            if (user.Id == 0)
            {
                return this.AppendUser(user);
            }
            else
            {
                return this.InsertUser(user);
            }
        }

        public bool DeleteUser(User user)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Users_DeleteUser";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                
                command.Parameters.AddWithValue("@userId", user.Id);

                connection.Open();
                var result = command.ExecuteNonQuery();

                return result > 0;
            }
        }

        public Tuple<byte[], string> GetAvatar(int userId)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Users_GetImage";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@userId", userId);

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

        public User GetUserById(int userId)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Users_GetUser";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@userId", userId);

                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return this.RowToUser(reader);
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable<User> ListAllUsers()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Users_GetAllUsers";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                List<User> list = new List<User>();

                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(this.RowToUser(reader));
                }

                return (list.Count != 0) ? list : null;
            }
        }

        public IEnumerable<User> ListUsersByAwardId(int awardId)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Users_GetUsersInAward";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@awardId", awardId);

                List<User> list = new List<User>();

                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(this.RowToUser(reader));
                }

                return (list.Count != 0) ? list : null;
            }
        }

        public bool SaveAvatar(int userId, byte[] imageArray, string imageType)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Users_SaveImage";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@image", imageArray);
                command.Parameters.AddWithValue("@mimeType", imageType);

                connection.Open();
                var result = command.ExecuteNonQuery();

                return result > 0;
            }
        }

        private bool AppendUser(User user)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Users_AddUser";

                var dateParameter = new SqlParameter("@bDay", SqlDbType.Date)
                {
                    Value = user.BirthDay
                };

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.Add(dateParameter);

                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    user.Id = (int)(decimal)reader["newId"];
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool InsertUser(User user)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Users_InsertUser";

                var dateParameter = new SqlParameter("@bDay", SqlDbType.Date)
                {
                    Value = user.BirthDay
                };

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.Add(dateParameter);
                command.Parameters.AddWithValue("@userId", user.Id);

                connection.Open();
                var result = command.ExecuteNonQuery();

                return result > 0;
            }
        }

        private User RowToUser(SqlDataReader reader)
        {
            var id = (int)reader["id"];
            var name = (string)reader["name"];
            var birthDay = (DateTime)reader["birth_day"];

            return new User(id, name, birthDay);
        }
    }
}
