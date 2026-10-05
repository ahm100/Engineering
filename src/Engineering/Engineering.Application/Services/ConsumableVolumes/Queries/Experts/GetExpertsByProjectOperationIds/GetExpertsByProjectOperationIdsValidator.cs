namespace Engineering.Application.Services.ConsumableVolumes.Queries.Experts.GetExpertsByProjectOperationIds;

public class GetExpertsByProjectOperationIdsQueryValidator : AbstractValidator<GetExpertsByProjectOperationIdsQuery>
{
    public GetExpertsByProjectOperationIdsQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationIds).NotNull().WithError(ConsumableVolumeExpertErrors.ProjectOperationIdIsEmpty);
    }
}
