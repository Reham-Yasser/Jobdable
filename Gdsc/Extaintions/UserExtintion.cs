using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Gdsc.Extaintions
{
    public static class UserExtintion
    {
        public static async Task<User> FindByEmailWithJobsAsync(this UserManager<User> userManager, ClaimsPrincipal claims)
        {
            var email = claims.FindFirstValue(ClaimTypes.Email);
            return await userManager.Users.Include(u => u.Jop).ThenInclude(u => u.JopForms).SingleOrDefaultAsync(u => u.Email == email);

        }
    }
}
