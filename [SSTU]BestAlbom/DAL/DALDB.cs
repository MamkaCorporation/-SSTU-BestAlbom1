using Entities;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALDB:IDAL
    {
        readonly string _connectionString;
        public DALDB()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public bool Add(User user)
        {
            var queryString = "INSERT INTO [dbo].[Users] ([Login] ,[Password] ,[Avatar] ,[Mimetype] ,[Id]) " +
                              "VALUES (@Login, @Password, @Avatar, @Mimetype, @Id);";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("Login", user.Name);
                command.Parameters.AddWithValue("Password", user.Password);
                command.Parameters.AddWithValue("Id", user.ID);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                { throw; }
            }
            return true;
        }

        public bool AddPhoto(Photo photo)
        {
            var queryString = "INSERT INTO [dbo].[Photos] ([IDPhoto], [IDUser], [Name], [MymeType], [File], [Rating]) " +
                              "VALUES (@idphoto, @iduser, @name, @mymetype, @file, @rating);";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("idphoto", photo.IDPhoto);
                command.Parameters.AddWithValue("iduser", photo.IDUser);
                command.Parameters.AddWithValue("name", photo.Name);
                command.Parameters.AddWithValue("mymetype", photo.MymeType);
                command.Parameters.AddWithValue("file", photo.File);
                command.Parameters.AddWithValue("rating", photo.Rating);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                { throw; }
            }
            return true;
        }

        public bool AddLike(Like like)
        {
            var queryString = "INSERT INTO [dbo].[Likes] ([Id], [IdPhoto]) " +
                              "VALUES (@id, @idphoto);" +
                              "UPDATE [dbo].[Photos] (Rating) " +
                              "SET Rating = Rating + 1 " +
                              "WHERE [dbo].Users.Id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("id", like.IDUser);
                command.Parameters.AddWithValue("idphoto", like.IDPhoto);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                { throw; }
            }
            return true;
        }


        public bool AddSub(Sub sub)
        {
            var queryString = "INSERT INTO [dbo].[Subs] ([IDUserFirst], [IDUserSecond]) " +
                              "VALUES (@iduserfirst, @idusersecond);";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("iduserfirst", sub.IDUserFirst);
                command.Parameters.AddWithValue("idusersecond", sub.IDUserSecond);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                { throw; }
            }
            return true;
        }

        public bool DeleteLike(Like like)
        {
            var queryString = "DELETE FROM [dbo].[Likes] " +
                              "WHERE ([dbo].[Likes].Id = @id) AND ([dbo].Likes.IdPhoto = @idphoto);" +
                              "UPDATE [dbo].[Photos] (Rating) " +
                              "SET Rating = Rating + 1 " +
                              "WHERE [dbo].Users.Id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("id", like.IDUser);
                command.Parameters.AddWithValue("idphoto", like.IDPhoto);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                { throw; }
                return true;
            }
        }

        public bool DeleteSub(Sub sub)
        {
            var queryString = "DELETE FROM [dbo].[Subs] " +
                              "WHERE ([dbo].[Subs].IDUserFirst = @id);";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("id", sub.IDUserFirst);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                { throw; }
                return true;
            }
        }

        public User GetUser(Guid IDUser)
        {
            var queryString = "SELECT Users.Id, Users.Login " +
                              "FROM [dbo].Users " +
                              "WHERE Users.Id = @userid";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("userid", IDUser);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return new User()
                    {
                        ID = (Guid)reader[0],
                        Name = (string)reader[1]
                    };
                }
                return null;
            }
        }

        public Photo GetPhoto(Guid IDPhoto)
        {
            var queryString = "SELECT [IDPhoto], [IDUser], [Name], [MymeType], [File], [Rating] " +
                              "FROM [dbo].[Photos] " +
                              "WHERE [dbo].[Photos].[IDPhoto] = @idphoto;";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return new Photo()
                    {
                        IDPhoto = (Guid)reader[0],
                        IDUser = (Guid)reader[1],
                        Name = (string)reader[2],
                        MymeType = (string)reader[3],
                        File = (byte[])reader[4],
                        Rating = (int)reader[5]
                    };
                }
            }
            return null;
        }

        public IEnumerable<Photo> GetPhotoesUser(Guid IDUser)
        {
            var queryString = "SELECT [IDPhoto], [IDUser], [Name], [MymeType], [File], [Rating] " +
                              "FROM [dbo].[Photos] " +
                              "WHERE [dbo].[Photos].[IDUser] = @iduser;";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("iduser", IDUser);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return new Photo()
                    {
                        IDPhoto = (Guid)reader[0],
                        IDUser = (Guid)reader[1],
                        Name = (string)reader[2],
                        MymeType = (string)reader[3],
                        File = (byte[])reader[4],
                        Rating = (int)reader[5]
                    };
                }
                yield return null;
            }
        }


        public IEnumerable<User> GetAllUser()
        {
            var queryString = "SELECT Users.Id, Users.Login " +
                               "FROM [dbo].Users;";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return new User()
                    {
                        ID = (Guid)reader[0],
                        Name = (string)reader[1]
                    };
                }
                yield return null;
            }
        }

        public IEnumerable<Sub> GetAllSubUser()
        {
            var queryString = "SELECT Subs.IDUserFirst, Subs.IDUserSecond " +
                               "FROM [dbo].Subs;";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return new Sub()
                    {
                        IDUserFirst = (Guid)reader[0],
                        IDUserSecond = (Guid)reader[1]
                    };
                }
                yield return null;
            }
        }

        public IEnumerable<Like> GetAllLikes()
        {
            var queryString = "SELECT [Id] ,[IdPhoto] " +
                              "FROM [dbo].[Likes];";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return new Like()
                    {
                        IDUser = (Guid)reader[0],
                        IDPhoto = (Guid)reader[1]
                    };
                }
                yield return null;
            }
        }

        public Photo GetAvatar(Guid IDUser)
        {
            var queryString = "SELECT [Avatar], [Mimetype], [Id] " +
                              "FROM [dbo].[Users]";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return new Photo()
                    {
                        File = (byte[])reader[0],
                        MymeType = (string)reader[1],
                        IDUser = (Guid)reader[2]
                    };
                }
                return null;
            }
        }
    }
}
