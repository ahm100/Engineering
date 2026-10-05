namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailWithVolumes;

public class GetProjectOperationDetailWithVolumesQueryValidator : AbstractValidator<GetProjectOperationDetailWithVolumesQuery>
{
    public GetProjectOperationDetailWithVolumesQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
