namespace Engineering.Application.Services.Projects.Models.AssignProjectsToCostCenter;

public class AssignProjectsToCostCenterValidator : AbstractValidator<AssignProjectsToCostCenterRequest>
{
    public AssignProjectsToCostCenterValidator()
    {
        RuleFor(oo => oo.CostCenterId)
            .IsPositive(GlobalCmts.CostCenterId);
        RuleForEach(oo => oo.ProjectIds)
            .IsPositive(GlobalCmts.ProjectId);
    }
}