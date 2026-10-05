namespace Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectCalendar;

public class CreateProjectCalendarValidator : AbstractValidator<CreateProjectCalendarRequest>
{
    public CreateProjectCalendarValidator()
    {
        RuleFor(x => x.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);

        RuleFor(x => x.TitleFa)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.MinutesPerDay)
            .GreaterThan(0)
            .LessThanOrEqualTo(24 * 60);

        RuleForEach(x => x.WorkingDays)
            .ChildRules(day =>
            {
                day.RuleForEach(d => d.WorkingTimes)
                    .Must(t => t.To > t.From)
                    .WithMessage("زمان پایان باید بعد از زمان شروع باشد.");
            });

        RuleFor(x => x.WorkingDays)
            .Must(days => days.Select(d => d.DayOfWeek).Distinct().Count() == days.Count)
            .WithMessage("هر روز هفته فقط یک‌بار می‌تواند تعریف شود.");
    }
}