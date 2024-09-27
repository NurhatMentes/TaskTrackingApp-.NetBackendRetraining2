using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private  IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }


        [HttpPost("add")]
        public IActionResult Add([FromBody] ProjectAddDto projectAddDto)
        {
            var result = _projectService.Add(projectAddDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] ProjectUpdateDto projectDto)
        {
            var result = _projectService.Update(projectDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{projectId}")]
        public IActionResult GetById(int projectId)
        {
            var result = _projectService.GetById(projectId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _projectService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("status/{projectId}")]
        public IActionResult ChangeStatus(int projectId, [FromQuery] bool status)
        {
            var result = _projectService.ChangeStatus(projectId, status);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
