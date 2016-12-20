using System;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Utility;
using ViewModels;
using IServices;

namespace ShaHua.Api
{
    public class UserController : ApiController
    {
        private IUserService iUserService;

        public UserController(IUserService iUserService)
        {
            this.iUserService = iUserService;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        [HttpGet]
        public async Task<bool> Login(string name, string pwd)
        {
            PasswordVerificationResult result;
            User user = await iUserService.FindByName(name);
            if (user != null)
            {
                IPasswordHasher passwordHasher = new PasswordHasher();
                result = passwordHasher.VerifyHashedPassword(user.PasswordHash, pwd);
                if (result == PasswordVerificationResult.Success)
                {
                    SignInAsync(user, false);
                    return true;
                }
            }
            return false;
        }

        [HttpGet]
        public async Task<List<User>> GetList([FromBody]Models.Filter filter)
        {
            return await iUserService.GetList(filter.GetFilter(), filter.Start, filter.PageLimit);
        }

        [HttpPost]
        public async Task<bool> Add([FromBody]User user)
        {
            return await iUserService.AddAsync(user);
        }

        [HttpPost]
        public async Task<bool> Update([FromBody]User user)
        {
            return await iUserService.UpdateAsync(user);
        }

        [HttpGet]
        public async Task<bool> Delete(Guid id)
        {
            return await iUserService.DeleteAsync(id);
        }

        private void SignInAsync(User user, bool isPersistent)
        {
            string role = Enums.Role.User.ToString();
            if (user.Role == (int)Enums.Role.Admin)
            {
                role = Enums.Role.Admin.ToString();
            }
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,role)
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }
    }
}
