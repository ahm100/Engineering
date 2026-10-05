namespace Engineering.Application.Services.ProjectRisks.Contracts.CreateProjectRisk;

public class CreateProjectRiskValidator : AbstractValidator<CreateProjectRiskRequest>
{
    public CreateProjectRiskValidator()
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
