namespace Engineering.Application.Services.OperationInfos.Models.AddSeasonsToOperationInfos;

public class AddSeasonsToOperationInfosValidator : AbstractValidator<AddSeasonsToOperationInfosRequest>
{
    public AddSeasonsToOperationInfosValidator()
    {
        RuleFor(oo => oo.Ids).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}