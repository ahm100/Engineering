
namespace Engineering.Application.Services.TransportationRequests.Queries.GetAirPlaneByIdWithoutInclude;

public class GetAirPlaneByIdWithoutIncludeQueryValidator : AbstractValidator<GetAirPlaneByIdWithoutIncludeQuery>
{
    public GetAirPlaneByIdWithoutIncludeQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationRequestErrors.IdIsEmpty);
    }
}
