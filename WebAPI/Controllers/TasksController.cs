using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _taskService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("byProject/{projectId}")]
        public IActionResult GetAllByProjectId(int projectId)
        {
            var result = _taskService.GetAllByProjectId(projectId);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("byUser/{userId}")]
        public IActionResult GetAllByAssignedUserId(int userId)
        {
            var result = _taskService.GetAllByAssignedUserId(userId);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _taskService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add(TaskAddDto taskAddDto)
        {
            var result = _taskService.Add(taskAddDto);
            if (result.IsSuccess)
            {
                return Ok(new { isSuccess = true, message = result.Message });
            }
            return BadRequest(new { isSuccess = false, message = result.Message });
        }

        [HttpPut("update")]
        public IActionResult Update(TaskUpdateDto taskUpdateDto)
        {
            var result = _taskService.Update(taskUpdateDto);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
