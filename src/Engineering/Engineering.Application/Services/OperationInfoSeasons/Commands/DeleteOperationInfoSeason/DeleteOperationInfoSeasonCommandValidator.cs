namespace Engineering.Application.Services.OperationInfoSeasons.Commands.DeleteOperationInfoSeason;

public class DeleteOperationInfoSeasonCommandValidator : AbstractValidator<DeleteOperationInfoSeasonCommand>
{
    public DeleteOperationInfoSeasonCommandValidator()
    {
        RuleFor(oo => oo.OprationInfoId).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoSeasonErrors.OperationInfoIsEmpty);
        RuleFor(oo => oo.SeasonId).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoSeasonErrors.IdIsEmpty);
    }
}
