namespace Engineering.Application.Services.Projects.Commands.CreateProjectCode;

public class CreateProjectCodeCommandValidator : AbstractValidator<CreateProjectCodeCommand>
{
    public CreateProjectCodeCommandValidator()
    {
        RuleFor(oo => oo.EmployerSymbol)
            .IsRequiredString(ProjectErrors.EmployerSymbolIsEmpty);
        RuleFor(oo => oo.ProjectCode)
            .IsPositive(ProjectCmts.ProjectCode);
    }
}