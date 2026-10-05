namespace Engineering.Application.Services.TransportationRequests.Models.UpdateTransportLoadWeight;

public class UpdateTransportLoadWeightRequestValidator : AbstractValidator<UpdateTransportLoadWeightRequest>
{
    public UpdateTransportLoadWeightRequestValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(TransportationRequestErrors.IdIsEmpty);
        RuleFor(oo => oo.LoadWeight).NotNull().GreaterThanOrEqualTo(0).NotEmpty().WithError(TransportationRequestErrors.LoadWeightIsEmpty);
    }
}