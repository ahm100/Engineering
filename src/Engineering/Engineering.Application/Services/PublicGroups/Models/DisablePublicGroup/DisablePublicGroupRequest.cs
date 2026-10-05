namespace Engineering.Application.Services.PublicGroups.Models.DisablePublicGroup;

public record DisablePublicGroupRequest(
    long Id
     ) : IHttpRequest;
