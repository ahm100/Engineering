
namespace Engineering.Application.Services.Transportations.Queries.GetsTransportationByIds;

public class GetsTransportationByIdsQueryValidator : AbstractValidator<GetsTransportationByIdsQuery>
{
    public GetsTransportationByIdsQueryValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
