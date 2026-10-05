namespace Engineering.Application.Services.ConsumableVolumes.Models.Experts.GetExpertsByProjectOperationId;

public class GetExpertsByProjectOperationIdValidator : AbstractValidator<GetExpertsByProjectOperationIdRequest>
{
    public GetExpertsByProjectOperationIdValidator()
    {
        RuleFor(oo => oo.ProjectOperationId).NotNull().GreaterThanOrEqualTo(1).WithError(ConsumableVolumeExpertErrors.ProjectOperationIdIsEmpty);
    }
}
