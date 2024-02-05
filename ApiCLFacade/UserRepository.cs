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
    public class UserRepository : IUserRepository
    {
        DBAccess db;
        public UserRepository(IConfiguration configuration)
        {
            db = new DBAccess(configuration);
        }
        public async Task<User> GetOneUserAsync(int userId)
        {
            return await db.GetOneUserAsync(userId);
        }
    }
}