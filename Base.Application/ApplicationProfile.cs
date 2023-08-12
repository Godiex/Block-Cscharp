using Base.Application.UseCases.Voters.Queries.GetVoter;
using Base.Domain.Entities;

namespace Base.Application
{

    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Voter, VoterDto>().ReverseMap();
        }
    }
}