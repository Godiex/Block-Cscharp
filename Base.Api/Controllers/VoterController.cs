using Base.Api.Examples.VoterExamples;
using Base.Api.Filters;
using Base.Application.UseCases.Voters.Commands.VoterRegister;
using Base.Application.UseCases.Voters.Queries.GetVoter;

namespace Base.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class VoterController
{
    private readonly IMediator _mediator;

    public VoterController(IMediator mediator) => _mediator = mediator;
    
    /// <summary>
    /// Create a voter
    /// </summary>
    /// <remarks>
    /// Create a voter
    /// </remarks>
    /// <param name="command">Create a voter </param>
    /// <returns>VoterDto info of voter</returns>
    /// <response code="201">Register successfully</response>
    /// <response code="400">parameters validation fails</response>
    [HttpPost]
    [SwaggerRequestExample(typeof(VoterRegisterCommand), typeof(VoterRegisterCommandExample))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(VoterRegisterResponseExample))]
    [SwaggerResponseExample(400, typeof(ValidationErrorResponse))]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(VoterDto), StatusCodes.Status200OK)]
    public async Task<VoterDto> Create(VoterRegisterCommand command) => await _mediator.Send(command);
    
    
    /// <summary>
    /// Get all voters
    /// </summary>
    /// <remarks>
    /// Get all voters
    /// </remarks>
    /// <param name="VoterQuery">Get all voters </param>
    /// <returns>listo of voterDto info of voter</returns>
    /// <response code="201">get voters successfully</response>
    /// <response code="400">parameters validation fails</response>
    [HttpGet]
    [SwaggerRequestExample(typeof(VoterQuery), typeof(GetVoterQueryExample))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(VoterRegisterResponseExample))]
    [SwaggerResponseExample(400, typeof(ValidationErrorResponse))]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(VoterDto), StatusCodes.Status200OK)]
    public async Task<List<VoterDto>> Get() => await _mediator.Send(new VoterQuery());
}
