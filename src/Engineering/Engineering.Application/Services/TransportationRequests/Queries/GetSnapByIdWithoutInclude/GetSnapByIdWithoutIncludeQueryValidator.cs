
namespace Engineering.Application.Services.TransportationRequests.Queries.GetSnapByIdWithoutInclude;

public class GetSnapByIdWithoutIncludeQueryValidator : AbstractValidator<GetSnapByIdWithoutIncludeQuery>
{
    public GetSnapByIdWithoutIncludeQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationRequestErrors.IdIsEmpty);
    }
}
