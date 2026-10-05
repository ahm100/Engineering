namespace Engineering.Application.Services.CostCenterTypes.Models.ActiveCostCenterType;

public class ActiveCostCenterTypeValidator : AbstractValidator<ActiveCostCenterTypeRequest>
{
    public ActiveCostCenterTypeValidator()
    {
        RuleFor(v => v.Id)
            .IsPositive(CCenterCmts.CostCenterTypeId);
    }
}