namespace Engineering.Application.Services.Projects.Models.FindLastUnitOrg;

public record FindLastUnitOrgRequest(
    long OrganizationId) : IHttpRequest;