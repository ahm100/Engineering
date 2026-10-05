
namespace Engineering.Application.Services.TransportationRequests.Queries.GetSnapById;

public class GetSnapByIdQueryValidator : AbstractValidator<GetSnapByIdQuery>
{
    public GetSnapByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationRequestErrors.IdIsEmpty);
    }
}
