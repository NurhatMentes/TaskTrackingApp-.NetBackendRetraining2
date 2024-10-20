using Business.Abstract;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : ControllerBase
    {
        private readonly IOperationClaimService _operationClaimService;

        public OperationClaimsController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }

        [ValidationAspect(typeof(OperationClaimValidator))]
        [HttpPost("add")]
        public IActionResult Add([FromBody] OperationClaim operationClaim)
        {
            var result = _operationClaimService.Add(operationClaim);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete()]
        public IActionResult Delete(int id)
        {
            var result = _operationClaimService.Delete(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [ValidationAspect(typeof(OperationClaimValidator))]
        [HttpPut("update-claim")]
        public IActionResult UpdateClaim([FromBody] OperationClaim operationClaim)
        {
            var result = _operationClaimService.Update(operationClaim);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("claims")]
        public IActionResult Claims()
        {
            var result = _operationClaimService.GetClaims();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
       
        }
    }
}
