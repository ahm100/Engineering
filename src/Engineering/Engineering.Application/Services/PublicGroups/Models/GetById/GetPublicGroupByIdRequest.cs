namespace Engineering.Application.Services.PublicGroups.Models.GetById;

public record GetPublicGroupByIdRequest(
    long Id
     ) : IHttpRequest;
