using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectUsersController : ControllerBase
    {
        private readonly IProjectUserService _projectUserService;

        public ProjectUsersController(IProjectUserService projectUserService)
        {
            _projectUserService = projectUserService;
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _projectUserService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getallbyprojectid")]
        public IActionResult GetAllByProjectId(int projectId)
        {
            var result = _projectUserService.GetAllByProjectId(projectId);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getallbyuserid")]
        public IActionResult GetAllByUserId(int userId)
        {
            var result = _projectUserService.GetAllByUserId(userId);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _projectUserService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add(ProjectUserAddDto projectUserAddDto)
        {
            var result = _projectUserService.Add(projectUserAddDto); 
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("update")]
        public IActionResult Update(ProjectUserUpdateDto dto)
        {
            var result = _projectUserService.Update(dto);
            if (result.IsSuccess)
            {
                return Ok(Messages.ProjectUserUpdated);
            }
            return BadRequest(result.Message);
        }
    }
}
