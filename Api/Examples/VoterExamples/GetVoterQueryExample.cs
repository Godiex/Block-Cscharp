using Application.UseCases.Voters.Queries.GetVoter;

namespace Api.Examples.VoterExamples
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
