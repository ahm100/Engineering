namespace Engineering.Application.Services.CostCenterTypes.Models.DisableCostCenterType;

public class DisableCostCenterTypeValidator : AbstractValidator<DisableCostCenterTypeRequest>
{
    public DisableCostCenterTypeValidator()
    {
        RuleFor(v => v.Id)
            .IsPositive(CCenterCmts.CostCenterTypeId);
    }
}