namespace Engineering.Application.Services.ConsumableVolumes.Models.GetProjectOperationDetailVolumes;

public class GetProjectOperationDetailVolumesValidator : AbstractValidator<GetProjectOperationDetailVolumesRequest>
{
    public GetProjectOperationDetailVolumesValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().WithError(ProjectOperationDetailErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
