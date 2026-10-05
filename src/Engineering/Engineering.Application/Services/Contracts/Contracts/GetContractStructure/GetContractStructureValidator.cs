namespace Engineering.Application.Services.Contracts.Contracts.GetContractStructure;

public class GetContractStructureValidator
    : AbstractValidator<GetContractStructureRequest>
{
    public GetContractStructureValidator()
    {
        RuleFor(oo => oo.ContractId)
            .IsPositive(GlobalCmts.Id);
    }
}