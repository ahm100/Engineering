namespace Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentIndexById;

public class GetContractAdjustmentIndexByIdValidator
    : AbstractValidator<GetContractAdjustmentIndexByIdRequest>
{
    public GetContractAdjustmentIndexByIdValidator()
    {
        RuleFor(oo => oo.ReferenceId)
            .IsPositive(ContractCmts.ContractAdjustmentReferenceId);

        RuleFor(oo => oo.Id)
            .IsPositive(ContractCmts.ContractAdjustmentIndexId);
    }
}
