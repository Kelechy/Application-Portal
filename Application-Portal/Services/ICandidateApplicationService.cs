using Application_Portal.DTOs;
using Application_Portal.Models;

namespace Application_Portal.Services
{
    public interface ICandidateApplicationService
    {
        Task<bool> CreateApplicationAsync(CreateApplicationDTO createApplicationDTO);
        Task<bool> EditApplicationAsync(QuestionDTO question);
        Task<bool> SaveApplicationAsync(SaveApplicationDTO createApplicationDTO);
        Task<Question> GetApplicationAsync(string questionType);
    }
}
