namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Commands.DeleteProjectOperationTemporaryDaily;

public class DeleteProjectOperationTemporaryDailyCommandValidator : AbstractValidator<DeleteProjectOperationTemporaryDailyCommand>
{
    public DeleteProjectOperationTemporaryDailyCommandValidator()
    {
        RuleFor(oo => oo.ProjectOperationTemporaryDailyId).NotNull().WithError(ProjectOperationTemporaryDailyErrors.IdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
