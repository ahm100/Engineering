namespace Engineering.Application.Services.PublicGroups.Models.CreatePublicGroup;

public record CreatePublicGroupRequest(
    List<long> ProductGroupIds
     ) : IHttpRequest;
