using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectScheduledTask;

namespace Engineering.Application.Services.ProjectWbses.Commands.CreateProjectScheduledTask;

public class CreateProjectScheduledTaskCommandValidator : AbstractValidator<CreateProjectScheduledTaskCommand>
{
    public CreateProjectScheduledTaskCommandValidator()
    {
        RuleFor(x => x.ImportId)
            .IsPositive(GlobalCmts.Id)
            .InclusiveBetween(0, 100)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
