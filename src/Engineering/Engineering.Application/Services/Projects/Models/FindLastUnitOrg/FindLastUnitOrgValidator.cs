namespace Engineering.Application.Services.Projects.Models.FindLastUnitOrg;

public class FindLastUnitOrgValidator : AbstractValidator<FindLastUnitOrgRequest>
{
    public FindLastUnitOrgValidator()
    {
        RuleFor(oo => oo.OrganizationId)
            .IsPositive(ProjectCmts.OrganizationId);
    }
}
