using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectCalendar;

namespace Engineering.Application.Services.ProjectWbses.Commands.CreateProjectCalendar;

public class CreateProjectCalendarCommandValidator : AbstractValidator<CreateProjectCalendarCommand>
{
    public CreateProjectCalendarCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .IsPositive(GlobalCmts.Id)
            .WithError(ProjectErrors.UnValidId);

        RuleFor(x => x.TitleFa)
            .NotEmpty()
            .WithError(GlobalErrors.ValueIsNull);
    }
}
