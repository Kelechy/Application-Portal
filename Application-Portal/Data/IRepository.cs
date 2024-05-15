using Application_Portal.Models;

namespace Application_Portal.Data
{
    public interface IRepository
    {
        Task<bool> CreateApplicationAsync(PersonalInformation personalInformation);
        Task<bool> EditApplicationAsync(Question questions);
        Task<bool> SaveAnswerAsync(List<Answer> answer);
        Task<Question> GetApplicationAsync(string questionType);
        Task<bool> CreateQuestionAsync(List<Question> questions);
    }
}
