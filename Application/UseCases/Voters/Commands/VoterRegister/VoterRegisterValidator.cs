namespace Application.UseCases.Voters.Commands.VoterRegister
{
    public class VoterRegisterValidator : AbstractValidator<VoterRegisterCommand>
    {
        public VoterRegisterValidator()
        {
            RuleFor(c => c.Nid).NotNull().NotEmpty();
            RuleFor(c => c.Dob).NotNull();
            RuleFor(c => c.Origin).NotNull().NotEmpty();
        }
    }
}