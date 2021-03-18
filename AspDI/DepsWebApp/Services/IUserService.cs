using System.Threading.Tasks;

namespace DepsWebApp.Services
{
    /// <summary>
    /// User service interface
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticate Users in service
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<User> Authenticate(string login, string password);

        
    }
}