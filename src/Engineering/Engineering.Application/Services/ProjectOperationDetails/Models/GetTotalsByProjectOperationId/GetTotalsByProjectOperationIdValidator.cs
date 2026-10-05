
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetTotalsByProjectOperationId;

public class GetTotalsByProjectOperationIdValidator : AbstractValidator<GetTotalsByProjectOperationIdRequest>
{
    public GetTotalsByProjectOperationIdValidator()
    {
        RuleFor(oo => oo.ProjectOperationId).NotNull().WithError(ProjectOperationDetailErrors.ProjectOperationIdIsEmpty);
    }
}
