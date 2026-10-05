namespace Engineering.Application.Services.CostCenterTypes.Models.InactiveCostCenterType;

public class InactiveCostCenterTypeValidator : AbstractValidator<InactiveCostCenterTypeRequest>
{
    public InactiveCostCenterTypeValidator()
    {
        RuleFor(v => v.Id)
            .IsPositive(CCenterCmts.CostCenterTypeId);
    }
}