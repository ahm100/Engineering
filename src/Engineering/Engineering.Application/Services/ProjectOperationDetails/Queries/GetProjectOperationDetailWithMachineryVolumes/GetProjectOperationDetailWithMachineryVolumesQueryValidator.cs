
namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailWithMachineryVolumes;

public class GetProjectOperationDetailWithMachineryVolumesQueryValidator : AbstractValidator<GetProjectOperationDetailWithMachineryVolumesQuery>
{
    public GetProjectOperationDetailWithMachineryVolumesQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
