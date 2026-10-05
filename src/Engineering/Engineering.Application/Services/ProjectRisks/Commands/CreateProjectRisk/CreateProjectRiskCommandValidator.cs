namespace Engineering.Application.Services.ProjectRisks.Commands.CreateProjectRisk;

public class CreateProjectRiskCommandValidator : AbstractValidator<CreateProjectRiskCommand>
{
    public CreateProjectRiskCommandValidator()
    {
        RuleFor(oo => oo.Code)
            .IsRequiredString(GlobalCmts.Code);

        RuleFor(oo => oo.Title)
            .IsRequiredString(GlobalCmts.Title);

        RuleFor(oo => oo.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);

        RuleFor(oo => oo.RiskProbability)
            .IsEnum(ProjectCmts.RiskProbability);

        RuleFor(oo => oo.RiskImpact)
            .IsEnum(ProjectCmts.RiskImpact);

        RuleFor(oo => oo.RiskStatus)
            .IsEnum(GlobalCmts.Status);
    }
}
