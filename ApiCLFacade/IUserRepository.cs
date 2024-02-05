using MyApiModels;

namespace ApiCLFacade
{
    public interface IUserRepository
    {
        Task<User> GetOneUserAsync(int userId);
    }
}