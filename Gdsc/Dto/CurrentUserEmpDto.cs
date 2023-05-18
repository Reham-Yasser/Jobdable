using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace Gdsc.Dto
{
    public class CurrentUserEmpDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Cv { get; set; }
        public string Skils { get; set; }
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public char Gender { get; set; }

        

    }
}
