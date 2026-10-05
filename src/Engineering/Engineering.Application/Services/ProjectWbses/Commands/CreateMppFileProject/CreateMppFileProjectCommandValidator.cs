using Engineering.Application.Services.ProjectWbses.Contracts.CreateMppFileProject;

namespace Engineering.Application.Services.ProjectWbses.Commands.CreateMppFileProject;

public class CreateMppFileProjectCommandValidator : AbstractValidator<CreateMppFileProjectCommand>
{
    public CreateMppFileProjectCommandValidator()
    {
        RuleFor(c => c.ProjectId)
            .IsPositive(GlobalCmts.Id)
            .WithError(ProjectErrors.UnValidId);

        RuleFor(c => c.UserId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
