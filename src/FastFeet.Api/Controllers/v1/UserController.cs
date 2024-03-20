using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using FastFeet.Application.Commons.Response;
using FastFeet.Application.Users.CreateUserCommand;
using FastFeet.Application.Users.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FastFeet.Api.Controllers.v1;

[ExcludeFromCodeCoverage]
[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/users")]
[ProducesResponseType(type: typeof(ErrorResponse), statusCode: (int)HttpStatusCode.UnprocessableEntity)]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator) => _mediator = mediator;

    [HttpPost()]
    [ProducesResponseType(type: typeof(SuccessResponse), statusCode: (int)HttpStatusCode.Created)]
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

    [HttpGet("{id}")]
    [ProducesResponseType(type: typeof(GetUserByIdResponse), statusCode: (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
          request: new GetUserByIdQuery(id),
          cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }
}
