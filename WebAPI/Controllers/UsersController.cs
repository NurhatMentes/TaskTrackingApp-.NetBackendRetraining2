﻿using Business.Abstract;
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

        [HttpGet]
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


        [HttpPost("add")]
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

        [HttpPut("update/{userId}")]
        public IActionResult Update(UserForUpdateDto userForUpdateDto, int userId)
        {
            var result = _userService.Update(userForUpdateDto, userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("admin/update/{userId}")]
        public IActionResult AdminUpdate(UserForAdminUpdateDto userForAdminUpdateDto)
        {
            var result = _userService.AdminUserUpdate(userForAdminUpdateDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("password")]
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

    }
}
