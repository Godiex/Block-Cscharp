using Application.UseCases.Voters.Commands.VoterRegister;

namespace Api.Examples.VoterExamples
{
    
    public class VoterRegisterCommandExample : IMultipleExamplesProvider<VoterRegisterCommand>
    {
        public IEnumerable<SwaggerExample<VoterRegisterCommand>> GetExamples()
        {
            var voterCommand = new VoterRegisterCommand(
                "12342212",
                "Colombia",
                DateTime.Now.AddYears(-19)
            );
            yield return SwaggerExample.Create("voterRegisterCommand", voterCommand);
        }
    }
}
