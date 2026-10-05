namespace Engineering.Application.Services.EmployerContracts.Contracts.DeleteEContractHeader;

public class DeleteEContractHeaderValidator : AbstractValidator<DeleteEContractHeaderRequest>
{
    public DeleteEContractHeaderValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
