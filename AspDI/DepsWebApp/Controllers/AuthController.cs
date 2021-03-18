using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DepsWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;

namespace DepsWebApp.Controllers
{
    /// <summary>
    /// Auth controller for registrations user 
    /// </summary>
    [Route("register")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserManagerContext _dbContext;

        /// <summary>
        /// Constructor for Auth Controller
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="dbContext"></param>
        public AuthController(IUserService userService,UserManagerContext dbContext)
        {
            
            _dbContext = dbContext;
        }
        /// <summary>
        /// Register method
        /// Allow anonymous for create user
        /// </summary>
        /// <param name="user"></param>
        /// <returns> Basic64 </returns>
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            
            try
            {
               var userAlready =  _dbContext.Users.FirstOrDefault(x => x.Login == user.Login);
               if(userAlready==null)
                   await _dbContext.Users.AddAsync(user);
               else
               {
                   return Conflict("This user is already registered");
               }
            }
            catch
            {
                return Conflict("something go wrong with add user");
            }

            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(user.Login + ":" + user.Password));
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                return Conflict("Cant update database");
            }
            

            return Ok($"Basic {svcCredentials}");
        }
        
    }





    



}
