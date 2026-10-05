namespace Engineering.Application.Services.CostOvers.Models.GetCostOverById;

public class GetCostOverByIdValidator : AbstractValidator<GetCostOverByIdRequest>
{
    public GetCostOverByIdValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(CostOverErrors.CostOverWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}