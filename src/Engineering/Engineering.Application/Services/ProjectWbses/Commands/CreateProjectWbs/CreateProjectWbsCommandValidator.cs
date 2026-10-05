using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectWbs;

namespace Engineering.Application.Services.ProjectWbses.Commands.CreateProjectWbs;

public class CreateProjectWbsCommandValidator : AbstractValidator<CreateProjectWbsCommand>
{
    public CreateProjectWbsCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);

        RuleFor(x => x.ProjectScheduleImportId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
