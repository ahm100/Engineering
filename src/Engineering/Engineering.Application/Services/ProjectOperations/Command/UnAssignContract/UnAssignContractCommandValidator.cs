namespace Engineering.Application.Services.ProjectOperations.Commands.UnAssignContract;

public class UnAssignContractCommandValidator : AbstractValidator<UnAssignContractCommand>
{
    public UnAssignContractCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ProjectOperationErrors.IdIsEmpty);
    }
}
