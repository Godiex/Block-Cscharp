using Application.UseCases.Voters.Queries.GetVoter;

namespace Api.Examples.VoterExamples
{
    
    public class GetVoterQueryExample : IMultipleExamplesProvider<VoterPaginatedQuery>
    {
        public IEnumerable<SwaggerExample<VoterPaginatedQuery>> GetExamples()
        {
            var voterQuery = new VoterPaginatedQuery(1, 20);
            yield return SwaggerExample.Create("voterQuery", voterQuery);
        }
    }
}
