using Engineering.Application.Services.Seasons.Models.SeasonModels;

public record GetSeasonsForResponseQuery(
    List<long>? Ids,
    string? FilterData,
    long? BranchId,
    long? CategoryId,
    string? SeasonCode,
    string? SeasonName,
    bool? IsActive,
    string[]? OrderBy,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetSeasonsModel>?>?>;