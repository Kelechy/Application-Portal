using System.ComponentModel.DataAnnotations;

namespace Application_Portal.Models
{
    public class Question
    {
        public int Id { get; set; }
        public required string QuestionText { get; set; }
        public required string Type { get; set; }
    }


    public enum QuestionTypes
    {
        [Display(Name = "Paragraph")]
        Paragraph = 1,

        [Display(Name = "Yes or No")]
        YesOrNo = 2,

        [Display(Name = "Dropdown")]
        Dropdown = 3,

        [Display(Name = "Date")]
        Date = 4,

        [Display(Name = "Number")]
        Number = 5
    }
}
