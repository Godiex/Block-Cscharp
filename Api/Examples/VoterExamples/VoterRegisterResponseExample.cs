using Application.UseCases.Voters.Queries.GetVoter;

namespace Api.Examples.VoterExamples
{
    
    public class VoterRegisterResponseExample : IMultipleExamplesProvider<VoterDto>
    {
        public IEnumerable<SwaggerExample<VoterDto>> GetExamples()
        {
            var voterDto = new VoterDto(
                Guid.NewGuid(),
                DateTime.Now.AddYears(-19),
                "Colombia"
            );
            yield return SwaggerExample.Create("crearAlarmaRequest", voterDto);
        }
    }
}
