using DAL.Entities;

namespace Gdsc.Dto
{
    public class CurrentUserHierDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public List<Jop> Jops { get; set; } = new List<Jop>();
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public char Gender { get; set; }
    }
}
