using Application_Portal.DTOs;
using Application_Portal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application_Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateApplicationController : ControllerBase
    {
        private readonly ICandidateApplicationService _service;

        public CandidateApplicationController(ICandidateApplicationService service)
        {
            _service = service;
        }


        [HttpPost("CreateApplication")]
        public async Task<IActionResult> CreateApplication(CreateApplicationDTO createApplication)
        {
            var result = await _service.CreateApplicationAsync(createApplication);
            if (result)
                return Ok(new ResponseDTO
                {
                    Data = result,
                    Status = true,
                    Message = "Successful"
                });

            else
            {
                return BadRequest(new ResponseDTO
                {
                    Data = result,
                    Status = false,
                    Message = "Error occured, pls try again"
                });
            }

        }

        [HttpPost("EditQuestion")]
        public async Task<IActionResult> EditApplication(QuestionDTO question)
        {
            var result = await _service.EditApplicationAsync(question);
            if (result)
                return Ok(new ResponseDTO
                {
                    Data = result,
                    Status = true,
                    Message = "Successful"
                });

            else
            {
                return BadRequest(new ResponseDTO
                {
                    Data = result,
                    Status = false,
                    Message = "Error occured, pls try again"
                });
            }

        }

        [HttpPost("GetApplication")]
        public async Task<IActionResult> GetApplication(string questionType)
        {
            var result = await _service.GetApplicationAsync(questionType);
            if (result != null)
                return Ok(new ResponseDTO
                {
                    Data = result,
                    Status = true,
                    Message = "Successful"
                });

            else
            {
                return BadRequest(new ResponseDTO
                {
                    Status = false,
                    Message = "Record not found"
                });
            }

        }

        [HttpPost("SaveApplication")]
        public async Task<IActionResult> SaveApplication(SaveApplicationDTO saveApplication)
        {
            var result = await _service.SaveApplicationAsync(saveApplication);
            if (result)
                return Ok(new ResponseDTO
                {
                    Data = result,
                    Status = true,
                    Message = "Successful"
                });

            else
            {
                return BadRequest(new ResponseDTO
                {
                    Data = result,
                    Status = false,
                    Message = "Record not found"
                });
            }

        }
    }
}
