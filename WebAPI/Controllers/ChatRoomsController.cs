using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomsController : ControllerBase
    {
        private readonly IChatRoomService _chatRoomService;

        public ChatRoomsController(IChatRoomService chatRoomService)
        {
            _chatRoomService = chatRoomService;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] ChatRoomCreateDto chatRoomCreateDto)
        {
            var result = _chatRoomService.Add(chatRoomCreateDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ChatRoomUpdateDto chatRoomUpdateDto)
        {
            chatRoomUpdateDto.Id = id; 
            var result = _chatRoomService.Update(chatRoomUpdateDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _chatRoomService.Delete(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _chatRoomService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _chatRoomService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getchatrooms")]
        public IActionResult GetChatRooms()
        {
            var result = _chatRoomService.GetChatRooms();
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
