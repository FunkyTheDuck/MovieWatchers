using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyApiModels;

namespace ApiDBLayer
{
    public class DBAccess
    {
        string path = string.Empty;
        readonly SqlConnection dbConn;
        public DBAccess(IConfiguration configuration)
        {
            path = configuration.GetConnectionString("DatabaseConnection");
            dbConn = new SqlConnection(path);
        }
        #region UserRegion
        public async Task<User> GetOneUserAsync(int userId)
        {
            string query = $"EXEC GetOneUser @id";
            SqlCommand command = new(query);
            command.Parameters.Add("@id", SqlDbType.Int).Value = userId;
            command.Connection = dbConn;
            await dbConn.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            User user = new();
            while (await reader.ReadAsync())
            {
                user.Id = (int)reader.GetValue(0);
                user.Name = (string)reader.GetValue(1);
                user.Email = (string)reader.GetValue(2);
            }
            await dbConn.CloseAsync();
            return user;
        }
        #endregion

        #region MovieRegion

        public async Task<bool> SaveMovieAsync(Movie movie)
        {
            string query = $"EXEC SaveMovie @id, @title, @description, @posterUrl";
            SqlCommand command = new(query);
            command.Parameters.Add("@id", SqlDbType.VarChar, 50).Value = movie.Id;
            command.Parameters.Add("@title", SqlDbType.VarChar, 50).Value = movie.Title;
            command.Parameters.Add("@description", SqlDbType.VarChar, 200).Value = movie.Description;
            command.Parameters.Add("@posterUrl", SqlDbType.VarChar, 50).Value = movie.Poster_Url;
            command.Connection = dbConn;
            await dbConn.OpenAsync();
            await command.ExecuteReaderAsync();
            return true;
        }

        #endregion
    }
}