using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.InterFaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user,UserManager<User> userManager);
    }
}
