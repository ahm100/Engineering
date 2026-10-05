
namespace Engineering.Application.Services.Transportations.Queries.GetById;

public class GetTransportationByIdQueryValidator : AbstractValidator<GetTransportationByIdQuery>
{
    public GetTransportationByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(TransportationErrors.IdIsEmpty);
    }
}
