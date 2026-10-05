namespace Engineering.Application.Services.TransportationRequests.Models.GetAirplaneById;

public class GetAirplaneByIdValidator : AbstractValidator<GetAirplaneByIdRequest>
{
    public GetAirplaneByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(TransportationRequestErrors.IdIsEmpty);
    }
}
