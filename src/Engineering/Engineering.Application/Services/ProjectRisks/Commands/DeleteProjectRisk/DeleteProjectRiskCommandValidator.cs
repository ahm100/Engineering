namespace Engineering.Application.Services.ProjectRisks.Commands.DeleteProjectRisk;

public class DeleteProjectRiskCommandValidator : AbstractValidator<DeleteProjectRiskCommand>
{
    public DeleteProjectRiskCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}