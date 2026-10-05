namespace Engineering.Application.Services.Contracts.Contracts.GetContractTypeDetails;

public class GetContractTypeDetailsValidator
    : AbstractValidator<GetContractTypeDetailsRequest>
{
    public GetContractTypeDetailsValidator()
    {
        RuleFor(oo => oo.ContractId)
            .IsPositive(GlobalCmts.ContractId);

        RuleFor(oo => oo.ContractTypeId)
            .IsPositive(ContractCmts.ContractTypeId);
    }
}