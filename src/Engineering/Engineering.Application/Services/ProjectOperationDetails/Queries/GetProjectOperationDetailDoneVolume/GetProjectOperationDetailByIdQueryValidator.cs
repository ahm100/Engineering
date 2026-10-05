
namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailDoneVolume;

public class GetProjectOperationDetailDoneVolumeQueryValidator : AbstractValidator<GetProjectOperationDetailDoneVolumeQuery>
{
    public GetProjectOperationDetailDoneVolumeQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
