
namespace Engineering.Application.Services.OperationLocations.Queries.GetsLocationByProjectOperationDetailIds;

public class GetsLocationByProjectOperationDetailIdsQueryValidator : AbstractValidator<GetsLocationByProjectOperationDetailIdsQuery>
{
    public GetsLocationByProjectOperationDetailIdsQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailIds).NotNull().WithError(OperationLocationErrors.UnValidIds);
    }
}
