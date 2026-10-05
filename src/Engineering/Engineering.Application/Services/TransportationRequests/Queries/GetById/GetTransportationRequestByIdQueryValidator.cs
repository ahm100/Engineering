
namespace Engineering.Application.Services.TransportationRequests.Queries.GetById;

public class GetTransportationRequestByIdQueryValidator : AbstractValidator<GetTransportationRequestByIdQuery>
{
    public GetTransportationRequestByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationRequestErrors.IdIsEmpty);
    }
}
