using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : ControllerBase
    {
        private readonly IUserOperationClaimService _userOperationClaimService;

        public UserOperationClaimsController(IUserOperationClaimService userOperationClaimService)
        {
            _userOperationClaimService = userOperationClaimService;
        }

        [HttpPut("update-role")]
        public IActionResult UpdateRole(int userId, int operationClaimId)
        {
            var result = _userOperationClaimService.Update(userId, operationClaimId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("get-claims/{userId}")]
        public IActionResult GetClaimsUserId(int userId)
        {
            var result = _userOperationClaimService.GetClaimsUserId(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("get-claims")]
        public IActionResult GetClaimsUser()
        {
            var result = _userOperationClaimService.GetClaimsUser();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
