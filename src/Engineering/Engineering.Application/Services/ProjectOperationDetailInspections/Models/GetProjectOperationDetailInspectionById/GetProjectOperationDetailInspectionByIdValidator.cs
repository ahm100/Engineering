namespace Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetProjectOperationDetailInspectionById;

public class GetProjectOperationDetailInspectionByIdValidator : AbstractValidator<GetProjectOperationDetailInspectionByIdRequest>
{
    public GetProjectOperationDetailInspectionByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationDetailInspectionErrors.IdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
