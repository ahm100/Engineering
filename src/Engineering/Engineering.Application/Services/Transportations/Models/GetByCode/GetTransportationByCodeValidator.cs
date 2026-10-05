namespace Engineering.Application.Services.Transportations.Models.GetByCode;

public class GetTransportationByCodeValidator : AbstractValidator<GetTransportationByCodeRequest>
{
    public GetTransportationByCodeValidator()
    {
        RuleFor(oo => oo.TransportationCode).NotEmpty().WithError(TransportationErrors.CodeIsEmpty);
    }
}
