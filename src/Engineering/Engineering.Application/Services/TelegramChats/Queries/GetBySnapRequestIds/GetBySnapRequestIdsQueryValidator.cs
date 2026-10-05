namespace Engineering.Application.Services.TelegramChats.Queries.GetBySnapRequestIds;

public class GetBySnapRequestIdsQueryValidator : AbstractValidator<GetBySnapRequestIdsQuery>
{
    public GetBySnapRequestIdsQueryValidator()
    {
        RuleFor(oo => oo.SnapRequestIds).NotNull().NotEmpty().WithError(TransportationRequestErrors.SnapIdsIsEmpty);
    }
}