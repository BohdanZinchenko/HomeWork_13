using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DepsWebApp.Services;
using Microsoft.EntityFrameworkCore;

namespace DepsWebApp
{
    /// <summary>
    /// User Service (Save users in memory)
    /// Check Authenticate
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserManagerContext _dbContext;
       


        /// <summary>
        /// Constructor of UserService
        /// </summary>
        public UserService(UserManagerContext dbContext)
        {
            _dbContext = dbContext;
            //_dbContext = dbContext;
        }
        /// <summary>
        /// Authenticate user 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> Authenticate(string login, string password)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Login == login && x.Password == password);
        }
        
        
    }
}