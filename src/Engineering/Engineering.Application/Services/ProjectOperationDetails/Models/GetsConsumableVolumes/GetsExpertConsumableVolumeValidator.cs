namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsConsumableVolumes;

public class GetsConsumableVolumesValidator : AbstractValidator<GetsConsumableVolumesRequest>
{
    public GetsConsumableVolumesValidator()
    {
        RuleFor(oo => oo.ProjectOperationId).NotNull().WithError(ProjectOperationDetailErrors.ProjectOperationIdIsEmpty);
        RuleFor(oo => oo.FinalAmount).NotEmpty().WithError(ProjectOperationDetailErrors.FinalAmountIsEmpty);
    }
}
