namespace Engineering.Application.Services.EmployerContracts.Contracts.GetEContractById;

public class GetEContractByIdValidator : AbstractValidator<GetEContractByIdRequest>
{
    public GetEContractByIdValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(EContractCmts.EmployerContract);
    }
}
