
namespace Engineering.Application.Services.CostCenters.Commands.Delete;

public class DeleteCostCenterCommandValidator : AbstractValidator<DeleteCostCenterCommand>
{
    public DeleteCostCenterCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(CostCenterErrors.IdIsEmpty);
    }
}