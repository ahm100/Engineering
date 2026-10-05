namespace Engineering.Application.Services.ProjectOperations.Commands.UpdateProjectForNewContract;

public class UpdateProjectForNewContractCommandValidator : AbstractValidator<UpdateProjectForNewContractCommand>
{
    public UpdateProjectForNewContractCommandValidator()
    {
        RuleFor(oo => oo.ProjectOperation).NotNull().WithError(ProjectOperationErrors.IdIsEmpty);
        RuleFor(oo => oo.Project).NotNull().WithError(ProjectOperationErrors.ProjectIdIsEmpty);
    }
}
