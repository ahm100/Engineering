
namespace Engineering.Application.Services.Transportations.Models.GetById;

public class GetTransportationByIdValidator : AbstractValidator<GetTransportationByIdRequest>
{
    public GetTransportationByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(TransportationErrors.IdIsEmpty);
    }
}
