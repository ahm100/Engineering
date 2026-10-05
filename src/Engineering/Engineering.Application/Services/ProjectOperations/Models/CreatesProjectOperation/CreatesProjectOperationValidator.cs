
namespace Engineering.Application.Services.ProjectOperations.Models.CreatesProjectOperation;

public class CreatesProjectOperationValidator : AbstractValidator<CreatesProjectOperationRequest>
{
    public CreatesProjectOperationValidator()
    {
        RuleFor(oo => oo.ProjectOperationModel).NotEmpty().WithError(ProjectOperationErrors.OperationInfoIdIsEmpty);
    }
}
