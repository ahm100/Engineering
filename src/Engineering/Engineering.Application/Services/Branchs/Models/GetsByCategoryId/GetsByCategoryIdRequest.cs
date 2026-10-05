namespace Engineering.Application.Services.Branchs.Models.GetsByCategoryId;

public record GetsByCategoryIdRequest(
    long CategoryId,
    int PageIndex,
    int PageSize)
    : IHttpRequest;