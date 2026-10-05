
namespace Engineering.Application.Services.TransportationRequests.Queries.IsDuplicateTransportWithPackingIds;

public class IsDuplicateTransportWithPackingIdsQueryValidator : AbstractValidator<IsDuplicateTransportWithPackingIdsQuery>
{
    public IsDuplicateTransportWithPackingIdsQueryValidator()
    {
        RuleForEach(oo => oo.PackingIds).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationRequestErrors.IdIsEmpty);
    }
}
