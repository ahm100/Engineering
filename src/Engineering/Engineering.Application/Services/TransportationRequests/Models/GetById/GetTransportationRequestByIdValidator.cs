
namespace Engineering.Application.Services.TransportationRequests.Models.GetById;

public class GetTransportationRequestByIdValidator : AbstractValidator<GetTransportationRequestByIdRequest>
{
    public GetTransportationRequestByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(TransportationRequestErrors.IdIsEmpty);
    }
}
