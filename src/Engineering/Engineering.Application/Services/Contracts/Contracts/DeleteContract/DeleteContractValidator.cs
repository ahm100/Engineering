namespace Engineering.Application.Services.Contracts.Contracts.DeleteContract;

public class DeleteContractValidator : AbstractValidator<DeleteContractRequest>
{
    public DeleteContractValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}