using Engineering.Application.Abstractions.Data.Branchs;
using Engineering.Application.Services.Branchs.Models.GetsBranchByCategoryIds;

namespace Engineering.Application.Services.Branchs.Queries.GetsBranchByCategoryIds;

public class GetsBranchByCategoryIdsQueryHandler : IQueryHandler<GetsBranchByCategoryIdsQuery, DataResult<List<GetsBranchByCategoryIdsModel>>>
{
    private readonly IBranchRepository _repository;
    private readonly ILogger<GetsBranchByCategoryIdsQueryHandler> _logger;

    public GetsBranchByCategoryIdsQueryHandler(
        ILogger<GetsBranchByCategoryIdsQueryHandler> logger,
        IBranchRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsBranchByCategoryIdsModel>>?>> Handle(GetsBranchByCategoryIdsQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsBranchByCategoryIds(
                request.CategoryIds,
                request.FilterData,
                request.IsActive,
                request.OrderBy,
                request.PageIndex,
                request.PageSize, ct);

            return items.Data.Any() ?
                new DataResult<List<GetsBranchByCategoryIdsModel>>
                {
                    Data = items.Data,
                    RowCount = items.RowCount
                } : Result.Failure<DataResult<List<GetsBranchByCategoryIdsModel>>>(BranchErrors.FilteredBranchNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsBranchByCategoryIdsModel>>>(SharedErrors.UnknownError);
        }
    }
}