namespace Engineering.Application.Services.TransportationRequests.Models.GetSnapById;

public class GetSnapByIdValidator : AbstractValidator<GetSnapByIdRequest>
{
    public GetSnapByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(TransportationRequestErrors.IdIsEmpty);
    }
}
