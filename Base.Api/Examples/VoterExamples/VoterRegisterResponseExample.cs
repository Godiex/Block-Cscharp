using Base.Application.UseCases.Voters.Queries.GetVoter;

namespace Base.Api.Examples.VoterExamples
{
    
    public class VoterRegisterResponseExample : IMultipleExamplesProvider<VoterDto>
    {
        public IEnumerable<SwaggerExample<VoterDto>> GetExamples()
        {
            var alarmaRequest = new VoterDto(
                Guid.NewGuid(),
                DateTime.Now.AddYears(-16),
                "Colombia"
            );
            yield return SwaggerExample.Create("crearAlarmaRequest", alarmaRequest);
        }
    }
}
