namespace Engineering.Application.Services.ProjectOperations.Commands.AssignContract;

public class AssignContractCommandValidator : AbstractValidator<AssignContractCommand>
{
    public AssignContractCommandValidator()
    {
        RuleFor(oo => oo.ProjectOperation).NotNull().WithError(ProjectOperationErrors.IdIsEmpty);
        RuleFor(oo => oo.EmployerContract).NotNull().WithError(ProjectOperationErrors.EmployerContractIdIsEmpty);
    }
}
