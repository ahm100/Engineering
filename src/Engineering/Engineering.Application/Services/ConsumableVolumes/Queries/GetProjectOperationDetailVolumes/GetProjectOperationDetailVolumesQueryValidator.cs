namespace Engineering.Application.Services.ConsumableVolumes.Queries.GetProjectOperationDetailVolumes;

public class GetProjectOperationDetailVolumesQueryValidator : AbstractValidator<GetProjectOperationDetailVolumesQuery>
{
    public GetProjectOperationDetailVolumesQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationDetailErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
