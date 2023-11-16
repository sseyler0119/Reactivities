using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator; // private can only be used inside this class

        /* protected means it can be used inside of the List class in Activities 
        or any other derived class that inherits from BaseApiController */
        // ??= means if the condition on the left is null, then assign what is on the right
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>(); 

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if(result == null) return NotFound();
            if (result.IsSuccess && result.Value != null)
                return Ok(result.Value);
            if (result.IsSuccess && result.Value == null)
                return NotFound();
            return BadRequest(result.Error);
        }
    }
}