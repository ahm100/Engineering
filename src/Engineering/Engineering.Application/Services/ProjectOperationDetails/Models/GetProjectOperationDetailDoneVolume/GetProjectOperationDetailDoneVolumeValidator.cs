
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectOperationDetailDoneVolume;

public class GetProjectOperationDetailDoneVolumeValidator : AbstractValidator<GetProjectOperationDetailDoneVolumeRequest>
{
    public GetProjectOperationDetailDoneVolumeValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
