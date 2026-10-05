
namespace Engineering.Application.Services.ProjectOperationDetails.Models.DeleteProjectOperationDetail;

public class DeleteProjectOperationDetailValidator : AbstractValidator<DeleteProjectOperationDetailRequest>
{
    public DeleteProjectOperationDetailValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
