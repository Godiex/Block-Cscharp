using Base.Application.UseCases.Voters.Commands;
using Base.Application.UseCases.Voters.Queries.GetVoter;

namespace Base.Api.Examples.VoterExamples
{
    
    public class VoterRegisterCommandExample : IMultipleExamplesProvider<VoterRegisterCommand>
    {
        public IEnumerable<SwaggerExample<VoterRegisterCommand>> GetExamples()
        {
            var alarmaDto = new VoterRegisterCommand(
                "12342212",
                "Colombia",
                DateTime.Now.AddYears(-16)
            );
            yield return SwaggerExample.Create("voterRegisterCommand", alarmaDto);
        }
    }
}
