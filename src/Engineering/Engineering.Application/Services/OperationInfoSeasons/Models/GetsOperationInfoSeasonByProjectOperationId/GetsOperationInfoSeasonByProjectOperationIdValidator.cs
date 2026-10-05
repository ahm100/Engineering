namespace Engineering.Application.Services.OperationInfoSeasons.Models.GetsOperationInfoSeasonByProjectOperationId;

public class GetsOperationInfoSeasonByProjectOperationIdValidator : AbstractValidator<GetsOperationInfoSeasonByProjectOperationIdRequest>
{
    public GetsOperationInfoSeasonByProjectOperationIdValidator()
    {
        RuleFor(oo => oo.ProjectOperationId).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoSeasonErrors.OperationInfoIsEmpty);
    }
}
