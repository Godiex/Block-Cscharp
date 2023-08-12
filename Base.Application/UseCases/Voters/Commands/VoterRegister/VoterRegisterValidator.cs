using FluentValidation;

namespace Base.Application.UseCases.Voters.Commands
{
    public class VoterRegisterValidator : AbstractValidator<VoterRegisterCommand>
    {
        public VoterRegisterValidator()
        {
            RuleFor(_ => _.Nid).NotNull().NotEmpty();
            RuleFor(_ => _.Dob).NotNull();
            RuleFor(_ => _.Origin).NotNull().NotEmpty();
        }
    }
}