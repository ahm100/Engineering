using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetDailyProjectOperationDetails;

public record GetDailyProjectOperationDetailsQuery(long ProjectOperationDetailId,
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
                                                   int PageSize) : IQuery<DataResult<List<DailyProjectOperation>>>;
