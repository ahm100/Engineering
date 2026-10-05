namespace Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeById;

public class GetCostCenterTypeByIdValidator : AbstractValidator<GetCostCenterTypeByIdRequest>
{
    public GetCostCenterTypeByIdValidator()
    {
        RuleFor(v => v.Id)
            .IsPositive(CCenterCmts.CostCenterTypeId);
    }
}