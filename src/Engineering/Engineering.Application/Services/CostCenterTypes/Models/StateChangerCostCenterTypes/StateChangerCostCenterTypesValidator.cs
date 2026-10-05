namespace Engineering.Application.Services.CostCenterTypes.Models.StateChangerCostCenterTypes;

public class StateChangerCostCenterTypesValidator : AbstractValidator<StateChangerCostCenterTypesRequest>
{
    public StateChangerCostCenterTypesValidator()
    {
        RuleForEach(v => v.Ids)
            .IsPositive(CCenterCmts.CostCenterTypeId);
    }
}