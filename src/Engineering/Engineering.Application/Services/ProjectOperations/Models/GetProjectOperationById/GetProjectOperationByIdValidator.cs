namespace Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationById;

public class GetProjectOperationByIdValidator : AbstractValidator<GetProjectOperationByIdRequest>
{
    public GetProjectOperationByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationErrors.IdIsEmpty);
    }
}
