
namespace Engineering.Application.Services.OperationInfoSeasons.Models.CreateOperationInfoSeason;

public class CreateOperationInfoSeasonValidator : AbstractValidator<CreateOperationInfoSeasonRequest>
{
    public CreateOperationInfoSeasonValidator()
    {
        RuleFor(oo => oo.OperationInfoIds).NotNull().WithError(OperationInfoSeasonErrors.OperationInfoIsEmpty);
    }
}
