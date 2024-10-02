using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public IActionResult Add([FromBody] MessageAddDto messageCreateDto)
        {
            var result = _messageService.Add(messageCreateDto);
            if (result.IsSuccess)
            {
                Ok(new { isSuccess = true, message = result.Message });
            }
            return BadRequest(new { isSuccess = false, message = result.Message });
        }

        [HttpPut]
        public IActionResult Update([FromBody] MessageUpdateDto messageUpdateDto)
        {
            var result = _messageService.Update(messageUpdateDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _messageService.Delete(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("user/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _messageService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _messageService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("chatroom/{chatRoomId}")]
        public IActionResult GetMessagesByChatRoomId(int chatRoomId)
        {
            var result = _messageService.GetMessagesByChatRoomId(chatRoomId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
