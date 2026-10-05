namespace Engineering.Application.Services.Projects.Command.AssignProjectsToCostCenter;

public class AssignProjectsToCostCenterCommandValidator : AbstractValidator<AssignProjectsToCostCenterCommand>
{
    public AssignProjectsToCostCenterCommandValidator()
    {
        RuleFor(oo => oo.CostCenterId)
            .IsPositive(GlobalCmts.CostCenterId);
        RuleForEach(oo => oo.ProjectIds)
            .IsPositive(GlobalCmts.ProjectId);
    }
}