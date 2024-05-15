namespace Application_Portal.DTOs
{
    public class SaveApplicationDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Nationality { get; set; }
        public string? Address { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public List<QuestionandAnswer> Questions { get; set; } = new List<QuestionandAnswer>();
    }
}
