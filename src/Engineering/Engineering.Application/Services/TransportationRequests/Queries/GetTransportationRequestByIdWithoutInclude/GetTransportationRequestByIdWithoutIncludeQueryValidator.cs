
namespace Engineering.Application.Services.TransportationRequests.Queries.GetTransportationRequestByIdWithoutInclude;

public class GetTransportationRequestByIdWithoutIncludeQueryValidator : AbstractValidator<GetTransportationRequestByIdWithoutIncludeQuery>
{
    public GetTransportationRequestByIdWithoutIncludeQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationRequestErrors.IdIsEmpty);
    }
}
