namespace Engineering.Application.Services.Projects.Queries.FindLastUnitOrg;

public record FindLastUnitOrgQuery(
    long OrganizationId) : IQuery<long>;