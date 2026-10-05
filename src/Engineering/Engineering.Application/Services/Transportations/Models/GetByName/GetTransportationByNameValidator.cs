
namespace Engineering.Application.Services.Transportations.Models.GetByName;

public class GetTransportationByNameValidator : AbstractValidator<GetTransportationByNameRequest>
{
    public GetTransportationByNameValidator()
    {
        RuleFor(oo => oo.TransportationName).NotEmpty().WithError(TransportationErrors.NameIsEmpty);
    }
}
