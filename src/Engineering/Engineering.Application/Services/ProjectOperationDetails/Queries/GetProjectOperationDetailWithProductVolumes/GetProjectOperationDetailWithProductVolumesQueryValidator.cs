
namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailWithProductVolumes;

public class GetProjectOperationDetailWithProductVolumesQueryValidator : AbstractValidator<GetProjectOperationDetailWithProductVolumesQuery>
{
    public GetProjectOperationDetailWithProductVolumesQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
