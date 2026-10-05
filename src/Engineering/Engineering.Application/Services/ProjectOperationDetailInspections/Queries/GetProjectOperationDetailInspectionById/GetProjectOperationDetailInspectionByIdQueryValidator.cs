namespace Engineering.Application.Services.ProjectOperationDetailInspections.Queries.GetProjectOperationDetailInspectionById;

public class GetProjectOperationDetailInspectionByIdQueryValidator : AbstractValidator<GetProjectOperationDetailInspectionByIdQuery>
{
    public GetProjectOperationDetailInspectionByIdQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailInspectionId).NotNull().WithError(ProjectOperationDetailInspectionErrors.IdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
