using Engineering.Application.Abstractions.Data.Branchs;
using Engineering.Application.Services.Branchs.Models.GetsByCategoryId;

namespace Engineering.Application.Services.Branchs.Queries.GetsBranchByCategoryId;

public class GetsBranchByCategoryIdQueryHandler : IQueryHandler<GetsBranchByCategoryIdQuery, DataResult<List<GetsBranchByCategoryIdModel>>>
{
    private readonly IBranchRepository _repository;
    private readonly ILogger<GetsBranchByCategoryIdQueryHandler> _logger;

    public GetsBranchByCategoryIdQueryHandler(
        ILogger<GetsBranchByCategoryIdQueryHandler> logger,
        IBranchRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsBranchByCategoryIdModel>>?>> Handle(
        GetsBranchByCategoryIdQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsBranchByCategoryIdModel(
                request.CategoryId,
                request.PageIndex,
                request.PageSize, ct);

            if (items.Data is null || !items.Data.Any())
                return Result.Failure<DataResult<List<GetsBranchByCategoryIdModel>>?>(BranchErrors.FilteredBranchNotFound);

            return new DataResult<List<GetsBranchByCategoryIdModel>>
            {
                Data = items.Data,
                RowCount = items.RowCount
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsBranchByCategoryIdModel>>?>(SharedErrors.UnknownError);
        }
    }
}