namespace Engineering.Application.Services.ConsumableVolumes.Queries.Experts.GetsByProjectOperationDetailId;

public class GetExpertsByProjectOperationDetailIdQueryValidator : AbstractValidator<GetExpertsByProjectOperationDetailIdQuery>
{
    public GetExpertsByProjectOperationDetailIdQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().WithError(ConsumableVolumeExpertErrors.ProjectOperationDetailIdIsEmpty);
    }
}
