
namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailWithProductSupply;

public class GetsProjectOperationDetailWithProductSupplyQueryValidator : AbstractValidator<GetsProjectOperationDetailWithProductSupplyQuery>
{
    public GetsProjectOperationDetailWithProductSupplyQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
