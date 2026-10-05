namespace Engineering.Application.Services.Contracts.Contracts.UpdateContractAdjustmentReference;

public class UpdateContractAdjustmentReferenceValidator
    : AbstractValidator<UpdateContractAdjustmentReferenceRequest>
{
    public UpdateContractAdjustmentReferenceValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(ContractCmts.ContractAdjustmentReferenceId);

        RuleFor(oo => oo.Code)
            .HasMaxLength(GlobalCmts.Code, 100);

        RuleFor(oo => oo.FaTitle)
            .HasMaxLength(GlobalCmts.FaTitle, 250);

        RuleFor(oo => oo.EnTitle)
            .HasMaxLength(GlobalCmts.EnTitle, 250);

        RuleFor(oo => oo.Description)
            .MaximumLength(1500);
    }
}
