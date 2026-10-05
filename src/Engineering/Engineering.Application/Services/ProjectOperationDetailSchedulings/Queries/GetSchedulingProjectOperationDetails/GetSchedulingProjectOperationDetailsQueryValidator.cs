
namespace Engineering.Application.Services.ProjectOperationDetailSchedulings.Queries.GetSchedulingProjectOperationDetails;

public class GetSchedulingProjectOperationDetailsQueryValidator : AbstractValidator<GetSchedulingProjectOperationDetailsQuery>
{
    public GetSchedulingProjectOperationDetailsQueryValidator()
    {
        RuleFor(oo => oo.CostCenterId).NotNull().WithError(CostCenterErrors.IdIsEmpty);
        RuleFor(oo => oo.ProjectId).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectErrors.IdIsEmpty);
    }
}
