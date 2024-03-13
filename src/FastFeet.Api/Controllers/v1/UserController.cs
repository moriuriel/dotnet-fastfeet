using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using FastFeet.Application.Users.CreateUserCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FastFeet.Api.Controllers.v1;

[ExcludeFromCodeCoverage]
[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator) => _mediator = mediator;

    [HttpPost()]
    public async Task<IActionResult> PostAsync(
        [FromHeader][Required] Guid idempotencyKey,
        [FromBody] CreateUserRequest user,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
          request: new CreateUserCommand(idempotencyKey, user),
          cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }
}
