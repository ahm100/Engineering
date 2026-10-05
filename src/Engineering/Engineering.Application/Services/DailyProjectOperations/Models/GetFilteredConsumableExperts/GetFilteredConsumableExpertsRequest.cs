
namespace Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredConsumableExperts;

public record GetFilteredConsumableExpertsRequest(long ProjectOperationDetailId,
                                                  string? FilterData,
                                                  int PageIndex,
                                                  int PageSize) : IHttpRequest;
