
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectOperationDetailById;

public class GetProjectOperationDetailByIdValidator : AbstractValidator<GetProjectOperationDetailByIdRequest>
{
    public GetProjectOperationDetailByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
