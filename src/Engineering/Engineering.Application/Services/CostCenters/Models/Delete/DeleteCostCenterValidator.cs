
namespace Engineering.Application.Services.CostCenters.Models.Delete;

public class DeleteCostCenterValidator : AbstractValidator<DeleteCostCenterRequest>
{
    public DeleteCostCenterValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
