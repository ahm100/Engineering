namespace Engineering.Application.Services.CostCenterTypes.Models.CostCenterTypeGroupDelete;

public class CostCenterTypeGroupDeleteValidator : AbstractValidator<CostCenterTypeGroupDeleteRequest>
{
    public CostCenterTypeGroupDeleteValidator()
    {
        RuleForEach(v => v.Ids)
            .IsPositive(CCenterCmts.CostCenterTypeId);
    }
}