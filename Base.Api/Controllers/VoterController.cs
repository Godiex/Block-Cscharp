using Base.Api.Examples.VoterExamples;
using Base.Application.UseCases.Voters.Commands;
using Base.Application.UseCases.Voters.Queries.GetVoter;
using Epm.Renovables.Maestros.Api.Filtros;

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
    /// <response code="201">Register sucefully</response>
    /// <response code="400">parameters validation fails</response>
    [HttpPost]
    [SwaggerRequestExample(typeof(VoterRegisterCommand), typeof(VoterRegisterCommandExample))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(VoterRegisterResponseExample))]
    [SwaggerResponseExample(400, typeof(ValidationErrorResponse))]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(VoterDto), StatusCodes.Status200OK)]
    public async Task<VoterDto> Create(VoterRegisterCommand command) => await _mediator.Send(command);
    
    
    [HttpGet]
    public async Task<List<VoterDto>> Get() => await _mediator.Send(new VoterQuery());
}
