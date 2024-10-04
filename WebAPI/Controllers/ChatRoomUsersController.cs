using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomUsersController : ControllerBase
    {
        private readonly IChatRoomUserService _chatRoomUserService;

        public ChatRoomUsersController(IChatRoomUserService chatRoomUserService)
        {
            _chatRoomUserService = chatRoomUserService;
        }


        [HttpPost("join/{chatRoomId}")]
        public IActionResult JoinChatRoom(int chatRoomId)
        {
            var result = _chatRoomUserService.JoinChatRoom(chatRoomId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost]
        public IActionResult Add([FromBody] ChatRoomUserAddDto chatRoomUserAddDto)
        {
            var result = _chatRoomUserService.Add(chatRoomUserAddDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("{chatRoomId}/{userId}")]
        public IActionResult Delete(int chatRoomId, int userId)
        {
            var result = _chatRoomUserService.Delete(chatRoomId, userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("{chatRoomId}/{userId}")]
        public IActionResult GetById(int chatRoomId, int userId)
        {
            var result = _chatRoomUserService.GetByChatRoomIdWithUserId(chatRoomId, userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetByUserId(int userId)
        {
            var result = _chatRoomUserService.GetByWithUserId(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpGet("chatroom/{chatRoomId}")]
        public IActionResult GetBychatRoomId(int chatRoomId)
        {
            var result = _chatRoomUserService.GetByChatRoomId(chatRoomId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _chatRoomUserService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getchatrooms")]
        public IActionResult GetChatRooms()
        {
            var result = _chatRoomUserService.GetChatRooms();
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
