using Application_Portal.Models;
using Microsoft.Azure.Cosmos;
using System.Collections.Concurrent;
using System.ComponentModel;
using Container = Microsoft.Azure.Cosmos.Container;

namespace Application_Portal.Data
{
    public class Repository : IRepository
    {
        private readonly Container _container;

        public Repository(Container container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public async Task<Question> GetApplicationAsync(string questionType)
        {
            try
            {
                var response = await _container.ReadItemAsync<Question>(questionType, new PartitionKey(questionType));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null; // Question with the specified type was not found
            }
        }

        public async Task<bool> CreateApplicationAsync(PersonalInformation personalInformation)
        {
            var response = await _container.CreateItemAsync(personalInformation, new PartitionKey(personalInformation.Id));
            if (response.Resource != null)
                return true;
            return false;
        }

        public async Task<bool> CreateQuestionAsync(List<Question> questions)
        {
            var batch = _container.CreateTransactionalBatch(new PartitionKey(questions[0].Id));

            foreach (var question in questions)
            {
                batch.CreateItem(question);
            }
            var response = await batch.ExecuteAsync();

            if (response.IsSuccessStatusCode)
            {
                foreach (var operationResult in response)
                {
                    if (!operationResult.IsSuccessStatusCode)
                    {
                        return false;
                    }
                }

                return true; // Return true if all items were created successfully
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> EditApplicationAsync(Question questions)
        {
            var existingCustomer = await _container.ReadItemAsync<Question>(questions.Id.ToString(), new PartitionKey(questions.Id));
            if (existingCustomer == null)
            {
                return false;
            }

            var response = await _container.ReplaceItemAsync(questions, questions.Id.ToString(), new PartitionKey(questions.Id.ToString()));
            if (response != null)
                return true;
            return false;
        }

        public async Task<bool> SaveAnswerAsync(List<Answer> answers)
        {
            var batch = _container.CreateTransactionalBatch(new PartitionKey(answers[0].ApplicantId));

            foreach (var answer in answers)
            {
                batch.CreateItem(answer);
            }
            var response = await batch.ExecuteAsync();

            if (response.IsSuccessStatusCode)
            {
                foreach (var operationResult in response)
                {
                    if (!operationResult.IsSuccessStatusCode)
                    {
                        return false;
                    }
                }

                return true; // Return true if all items were created successfully
            }
            else
            {
                return false;
            }
        }
    }
}
