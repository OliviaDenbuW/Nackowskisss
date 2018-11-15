using Nackowskisss.Models.API_ViewModels.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskisss.Services.Identity
{
    public interface IUserService
    {
        string GetCurrentUserName();

        string AddNewAdmin(AddNewAdminViewModel newAdmin);
    }
}
