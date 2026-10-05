namespace Engineering.Application.Services.EmployerContracts.Contracts.GetEContractHeaderById;

public class GetEContractHeaderByIdValidator : AbstractValidator<GetEContractHeaderByIdRequest>
{
    public GetEContractHeaderByIdValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(EContractCmts.EmployerContractHead);
    }
}
