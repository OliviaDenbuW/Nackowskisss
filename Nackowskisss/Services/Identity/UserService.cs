using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Nackowskisss.Models;
using Nackowskisss.Models.API_ViewModels.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskisss.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private UserManager<ApplicationUser> _userManager;

        public UserService(IHttpContextAccessor httpContextAccessor,
                           UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public string GetCurrentUserName()
        {
            string userName = _httpContextAccessor.HttpContext.User.Identity.Name;

            return userName;
        }

        public string AddNewAdmin(AddNewAdminViewModel viewModel)
        {
            bool userNameIsUnique = NewAdminIsValid(viewModel.Email);

            if (userNameIsUnique == true)
            {
                ApplicationUser newAdmin = new ApplicationUser();
                newAdmin.UserName = viewModel.Email;
                newAdmin.Email = viewModel.Email;

                IdentityResult result = _userManager.CreateAsync(newAdmin, viewModel.Password).Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(newAdmin, "Admin").Wait();
                }

                return "Succeeded"/*result.ToString()*/;
            }

            return "Failed";
        }

        public bool NewAdminIsValid(string newUsername)
        {
            bool usernameIsUnique = false;

            if (_userManager.FindByNameAsync(newUsername).Result == null)
            {
                usernameIsUnique = true;
            }
            else
            {
                usernameIsUnique = false;
            }

            return usernameIsUnique;
        }
    }
}
