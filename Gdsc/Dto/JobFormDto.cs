namespace Gdsc.Dto
{
    public class JobFormDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public IFormFile? CvFile { get; set; }
        public string PhoneNumber { get; set; } //Just Added
        public int JopId { get; set; }
    }
}
