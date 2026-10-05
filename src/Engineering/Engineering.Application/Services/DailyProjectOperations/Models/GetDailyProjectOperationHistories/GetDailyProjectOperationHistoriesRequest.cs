
namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationHistories;

public record GetDailyProjectOperationHistoriesRequest(long ProjectOperationDetailId,
                                                       long? ContractorId,
                                                       decimal? Length,
                                                       decimal? Width,
                                                       decimal? Height,
                                                       decimal? Weight,
                                                       decimal? Number,
                                                       DateTime? StartDate,
                                                       DateTime? EndDate,
                                                       long? CreatorId,
                                                       string? FilterData,
                                                       string[]? OrderBy,
                                                       int PageIndex,
                                                       int PageSize) : IHttpRequest;
