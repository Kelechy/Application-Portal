using System.ComponentModel.DataAnnotations;

namespace Application_Portal.Models
{
    public class Answer
    {
        [Key]
        public required string ApplicantId { get; set; }
        public int QuestionId { get; set; }
        public required string Answerv { get; set; }
    }
}
