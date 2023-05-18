namespace Gdsc.Dto
{
    public class HierRegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public char Gender { get; set; }
        public string Company { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
