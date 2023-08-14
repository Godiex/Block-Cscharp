using Base.Application.UseCases.Voters.Commands;
using Base.Application.UseCases.Voters.Commands.VoterRegister;
using Base.Application.UseCases.Voters.Queries.GetVoter;

namespace Base.Api.Examples.VoterExamples
{
    
    public class GetVoterQueryExample : IMultipleExamplesProvider<VoterQuery>
    {
        public IEnumerable<SwaggerExample<VoterQuery>> GetExamples()
        {
            var voterQuery = new VoterQuery();
            yield return SwaggerExample.Create("voterQuery", voterQuery);
        }
    }
}
