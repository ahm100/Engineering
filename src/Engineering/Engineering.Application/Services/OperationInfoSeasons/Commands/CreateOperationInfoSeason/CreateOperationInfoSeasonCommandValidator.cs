namespace Engineering.Application.Services.OperationInfoSeasons.Commands.CreateOperationInfoSeason;

public class CreateOperationInfoSeasonCommandValidator : AbstractValidator<CreateOperationInfoSeasonCommand>
{
    public CreateOperationInfoSeasonCommandValidator()
    {
        RuleFor(oo => oo.OperationInfo).NotEmpty().WithError(OperationInfoSeasonErrors.OperationInfoIsEmpty);
    }
}
