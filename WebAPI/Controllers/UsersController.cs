using Business.Abstract;
using Core.Entities.Concrete;
using Core.Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
           _userService = userService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetById")]
        public IActionResult Get(int userId)
        {
            var result = _userService.GetById(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("UserAdd")]
        public IActionResult Post([FromBody] UserForRegisterDto userForRegisterDto)
        {
            var password = userForRegisterDto.Password;

            var result = _userService.Add(userForRegisterDto, password);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("UserUpdate")]
        public IActionResult Update([FromBody] User user, [FromQuery] string currentPassword)
        {
            if (user == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid user data.");
            }

            var result = _userService.Update(user, currentPassword);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


    }
}
