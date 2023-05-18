namespace Gdsc.Dto
{
    public class EmpRegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Skils { get; set; }
        public string PhoneNumber { get; set; }
        public char Gender { get; set; }

        public IFormFile? ImageFile { get; set; }
        public IFormFile? CvFile { get; set; }

    }
}
