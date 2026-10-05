namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.DeleteProjectOperationTemporaryDaily;

public class DeleteProjectOperationTemporaryDailyValidator : AbstractValidator<DeleteProjectOperationTemporaryDailyRequest>
{
    public DeleteProjectOperationTemporaryDailyValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationTemporaryDailyErrors.IdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
