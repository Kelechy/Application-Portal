using Application_Portal.Data;
using Application_Portal.DTOs;
using Application_Portal.Models;
using AutoMapper;
using System.Transactions;

namespace Application_Portal.Services
{
    public class CandidateApplicationService : ICandidateApplicationService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public CandidateApplicationService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> CreateApplicationAsync(CreateApplicationDTO createApplicationDTO)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    // Create PersonalInformation object
                    var personalInfo = new PersonalInformation
                    {
                        Email = createApplicationDTO.Email,
                        FirstName = createApplicationDTO.FirstName,
                        LastName = createApplicationDTO.LastName,
                        PhoneNumber = createApplicationDTO.PhoneNumber,
                        Nationality = createApplicationDTO.Nationality,
                        Address = createApplicationDTO.Address,
                        DateOfBirth = createApplicationDTO.DateOfBirth,
                        Gender = createApplicationDTO.Gender
                    };
                    var personalInfoSaved = await _repository.CreateApplicationAsync(personalInfo);

                    if (personalInfoSaved)
                    {
                        var questions = new List<Question>();

                        foreach (var questionDTO in createApplicationDTO.Questions)
                        {
                            var question = new Question
                            {
                                QuestionText = questionDTO.QuestionText,
                                Type = questionDTO.Type
                            };

                            questions.Add(question);
                        }
                        var questionsSaved = await _repository.CreateQuestionAsync(questions);
                        if (questionsSaved)
                        {
                            scope.Complete();
                            return true;
                        }
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> EditApplicationAsync(QuestionDTO question)
        {
            var createApplicationModel = _mapper.Map<Question>(question);
            var res = await _repository.EditApplicationAsync(createApplicationModel);
            if (res) return true;
            return false;
        }

        public async Task<Question> GetApplicationAsync(string questionType)
        {
            var res = await _repository.GetApplicationAsync(questionType);
            return res;
        }

        public async Task<bool> SaveApplicationAsync(SaveApplicationDTO saveApplicationDTO)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    // Create PersonalInformation object
                    var personalInfo = new PersonalInformation
                    {
                        Email = saveApplicationDTO.Email,
                        FirstName = saveApplicationDTO.FirstName,
                        LastName = saveApplicationDTO.LastName,
                        PhoneNumber = saveApplicationDTO.PhoneNumber,
                        Nationality = saveApplicationDTO.Nationality,
                        Address = saveApplicationDTO.Address,
                        DateOfBirth = saveApplicationDTO.DateOfBirth,
                        Gender = saveApplicationDTO.Gender
                    };
                    var personalInfoSaved = await _repository.CreateApplicationAsync(personalInfo);

                    if (personalInfoSaved)
                    {
                        var answers = new List<Answer>();

                        foreach (var questionDTO in saveApplicationDTO.Questions)
                        {
                            var answer = new Answer
                            {
                                ApplicantId = saveApplicationDTO.Email,
                                QuestionId = int.Parse(questionDTO.QuestionId),
                                Answerv = questionDTO.Answer
                            };

                            answers.Add(answer);
                        }
                        var questionsSaved = await _repository.SaveAnswerAsync(answers);
                        if (questionsSaved)
                        {
                            scope.Complete();
                            return true;
                        }
                    }
                    return false;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
