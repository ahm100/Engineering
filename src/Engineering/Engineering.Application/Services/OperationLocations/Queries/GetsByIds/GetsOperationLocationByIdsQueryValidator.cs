
namespace Engineering.Application.Services.OperationLocations.Queries.GetsByIds;

public class GetsOperationLocationByIdsQueryValidator : AbstractValidator<GetsOperationLocationByIdsQuery>
{
    public GetsOperationLocationByIdsQueryValidator()
    {
        RuleFor(oo => oo.Ids).NotNull().WithError(OperationLocationErrors.UnValidIds);
    }
}
