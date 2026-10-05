namespace Engineering.Application.Services.ProjectRisks.Contracts.DeleteProjectRisk;

public class DeleteProjectRiskValidator : AbstractValidator<DeleteProjectRiskRequest>
{
    public DeleteProjectRiskValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
