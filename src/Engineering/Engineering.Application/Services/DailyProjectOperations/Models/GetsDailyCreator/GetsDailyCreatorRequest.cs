
namespace Engineering.Application.Services.TransportationRequests.Models.GetsDailyCreator;

public record GetsDailyCreatorRequest(
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
