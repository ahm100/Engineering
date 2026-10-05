namespace Engineering.Application.Services.Projects.Queries.FindLastUnitOrg;

public class FindLastUnitOrgQueryValidator : AbstractValidator<FindLastUnitOrgQuery>
{
    public FindLastUnitOrgQueryValidator()
    {
        RuleFor(x => x.OrganizationId)
            .IsPositive(GlobalCmts.Id);
    }
}