namespace Engineering.Application.Services.RequestRewards.Commands.CreateRequestRewardDocument;

public class CreateRequestRewardDocumentCommandValidator : AbstractValidator<CreateRequestRewardDocumentCommand>
{
    public CreateRequestRewardDocumentCommandValidator()
    {
        RuleFor(oo => oo.Url).NotEmpty().WithError(RequestRewardDocumentErrors.InValidUrl);
        RuleFor(oo => oo.RequestReward).NotNull().WithError(RequestRewardDocumentErrors.InValidRequestReward);
    }
}
