namespace Engineering.Application.Services.Contracts.Contracts.GetContractChangeById;

public class GetContractChangeByIdValidator : AbstractValidator<GetContractChangeByIdRequest>
{
    public GetContractChangeByIdValidator()
    {
        RuleFor(oo => oo.ContractId).IsPositive(GlobalCmts.ContractId);
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
    }
}
