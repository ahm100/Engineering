namespace Engineering.Application.Services.EmployerContracts.Contracts.DeleteEContract;

public class DeleteEContractValidator : AbstractValidator<DeleteEContractRequest>
{
    public DeleteEContractValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
