namespace Employees.DAL.MSSql
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using Contract;

    public class AuthSqlStore : IAuthStore
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["MSSql"].ConnectionString;

        public bool AddAuth(string username, byte[] hash)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Auths_AddAuth";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@userName", username);
                command.Parameters.AddWithValue("@hash", hash);

                connection.Open();
                var result = command.ExecuteNonQuery();

                return result > 0;
            }
        }

        public ICollection<string> GetAllUserNames()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Auths_GetAllUserNames";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                List<string> list = new List<string>();

                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add((string)reader["username"]);
                }

                return (list.Count != 0) ? list : null;
            }
        }

        public byte[] GetHashByUserName(string username)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Auths_GetHash";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@userName", username);

                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return (byte[])reader["hash"];
                }
                else
                {
                    return null;
                }
            }
        }

        public ICollection<string> GetRolesForUser(string username)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Auths_GetRolesOfUserName";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@userName", username);

                List<string> list = new List<string>();

                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add((string)reader["name"]);
                }

                return (list.Count != 0) ? list : null;
            }
        }

        public ICollection<string> GetUsersInRole(string roleName)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Auths_GetUserNamesOfRole";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@roleName", roleName);

                List<string> list = new List<string>();

                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add((string)reader["username"]);
                }

                return (list.Count != 0) ? list : null;
            }
        }

        public bool GiveRole(string userName, string roleName)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Auths_GiveRole";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@userNameParam", userName);
                command.Parameters.AddWithValue("@roleNameParam", roleName);

                connection.Open();
                var result = command.ExecuteNonQuery();

                return result > 0;
            }
        }

        public bool IsUserInRole(string userName, string roleName)
        {
            var list = this.GetRolesForUser(userName);
            return (list != null) ? list.Contains(roleName) : false;
        }

        public bool RevokeRole(string userName, string roleName)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Auths_RevokeRole";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@userNameParam", userName);
                command.Parameters.AddWithValue("@roleNameParam", roleName);

                connection.Open();
                var result = command.ExecuteNonQuery();

                return result > 0;
            }
        }
    }
}
