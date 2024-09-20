using Business.Abstract;
using Core.Entities.Concrete;
using Core.Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("get-by-id")]
        public IActionResult Get(int userId)
        {
            var result = _userService.GetById(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("get-user-detail")]
        public IActionResult GetUserDetail(int userId)
        {
            var result = _userService.GetUserDetail(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("user-add")]
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

        [HttpPut("user-update")]
        public IActionResult Update([FromBody] UserForUpdateDto userForUpdateDto, int userId)
        {
            var result = _userService.Update(userForUpdateDto, userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("update-password")]
        public IActionResult UpdatePassword([FromBody] UpdatePasswordDto updatePasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var result = _userService.UpdatePassword(updatePasswordDto.UserId, updatePasswordDto.CurrentPassword, updatePasswordDto.NewPassword);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("update-role")]
        public IActionResult UpdateUserRole(int userId, int newRoleId)
        {
            var result = _userService.UpdateUserRole(userId, newRoleId);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

    }
}
