using Application_Portal.Models;

namespace Application_Portal.DTOs
{
    public class CreateQuestionDTO
    {
        public int Id { get; set; }
        public required string QuestionText { get; set; }
        public required QuestionTypes Type { get; set; }
    }
}
