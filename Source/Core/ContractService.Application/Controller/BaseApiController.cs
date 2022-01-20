using System.Threading;
using System.Threading.Tasks;
using ContactService.Application.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ContactService.Application.Controller
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

        protected async Task<IActionResult> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken)
        {
            TResponse result = await Mediator.Send(request, cancellationToken);
            if (result is ApiResponse apiResponse)
            {
                return StatusCode(apiResponse.HttpStatusCode, result);
            }

            return Ok(result);
        }
    }
}
