
namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Commands.DeleteProjectOperationDetailDeduction;

public class DeleteProjectOperationDetailDeductionCommandValidator : AbstractValidator<DeleteProjectOperationDetailDeductionCommand>
{
    public DeleteProjectOperationDetailDeductionCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ProjectOperationDetailDeductionErrors.IdIsEmpty);
    }
}