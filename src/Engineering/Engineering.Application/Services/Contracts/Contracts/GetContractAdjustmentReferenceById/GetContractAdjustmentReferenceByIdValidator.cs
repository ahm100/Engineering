namespace Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentReferenceById;

public class GetContractAdjustmentReferenceByIdValidator
    : AbstractValidator<GetContractAdjustmentReferenceByIdRequest>
{
    public GetContractAdjustmentReferenceByIdValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(ContractCmts.ContractAdjustmentReferenceId);
    }
}
