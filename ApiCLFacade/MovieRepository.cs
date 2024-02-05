using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiDBLayer;
using Microsoft.Extensions.Configuration;
using MyApiModels;

namespace ApiCLFacade
{
    public class MovieRepository
    {
        DBAccess db;
        public MovieRepository(IConfiguration configuration)
        {
            db = new(configuration);
        }
        public async Task<bool> SaveMovieAsync(Movie movie)
        {

            return false;
        }
    }
}