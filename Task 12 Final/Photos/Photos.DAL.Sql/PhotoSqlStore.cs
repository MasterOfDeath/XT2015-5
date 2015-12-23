﻿namespace Photos.DAL.Sql
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using Contract;
    using Entites;

    public class PhotoSqlStore : IPhotoStore
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["MSSql"].ConnectionString;

        public bool AddPhoto(Photo photo, byte[] data)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Photo_AddPhoto";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@name", photo.Name);
                command.Parameters.AddWithValue("@albumId", photo.AlbumId);
                command.Parameters.AddWithValue("@size", photo.Size);
                command.Parameters.AddWithValue("@mime", photo.Mime);
                command.Parameters.AddWithValue("@date", photo.Date);

                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    photo.Id = (int)(decimal)reader["newId"];
                }
                else
                {
                    return false;
                }
            }

            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Photo_AddPhotoData";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@photoId", photo.Id);
                command.Parameters.AddWithValue("@data", data);

                connection.Open();
                var result = command.ExecuteNonQuery();

                return result > 0;
            }
        }

        public byte[] GetDataById(int photoId)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Photo_GetPhotoDataById";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@id", photoId);

                byte[] result = null;

                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    result = (byte[])reader["data"];
                }

                return result;
            }
        }

        public Photo GetPhotoById(int photoId)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Photo_GetPhotoById";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@id", photoId);

                Photo photo = null;

                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    photo = this.RowToPhoto(reader);
                }

                return photo;
            }
        }

        public bool InsertPhoto(Photo photo, byte[] data)
        {
            throw new NotImplementedException();
        }

        public ICollection<Photo> ListPhotosInAlbum(int albumId)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var storeProcedure = "Photo_ListPhotosInAlbum";

                var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@albumId", albumId);

                List<Photo> result = null;

                connection.Open();
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    result = new List<Photo>();
                }

                while (reader.Read())
                {
                    result.Add(this.RowToPhoto(reader));
                }

                return result;
            }
        }

        public bool RemovePhoto(int photoId)
        {
            throw new NotImplementedException();
        }

        private Photo RowToPhoto(SqlDataReader reader)
        {
            var id = (int)reader["id"];
            var name = (string)reader["name"];
            var albumId = (int)reader["album_id"];
            var size = (int)reader["size"];
            var mime = (string)reader["mime"];
            var date = (DateTime)reader["date"];

            return new Photo(id, name, albumId, size, mime, date);
        }
    }
}
