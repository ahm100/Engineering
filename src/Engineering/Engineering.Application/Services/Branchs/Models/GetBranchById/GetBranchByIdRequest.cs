namespace Engineering.Application.Services.Branchs.Models.GetBranchById;

public record GetBranchByIdRequest(
    long Id)
    : IHttpRequest;