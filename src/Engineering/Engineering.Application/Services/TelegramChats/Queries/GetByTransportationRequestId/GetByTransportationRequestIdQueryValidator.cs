namespace Engineering.Application.Services.TelegramChats.Queries.GetByTransportationRequestId;

public class GetByTransportationRequestIdQueryValidator : AbstractValidator<GetByTransportationRequestIdQuery>
{
    public GetByTransportationRequestIdQueryValidator()
    {
        RuleFor(oo => oo.TransportationRequestId).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationRequestErrors.IdIsEmpty);
    }
}