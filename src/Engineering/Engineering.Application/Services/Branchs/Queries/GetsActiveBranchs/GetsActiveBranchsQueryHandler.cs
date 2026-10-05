using Engineering.Application.Abstractions.Data.Branchs;
using Engineering.Application.Services.Branchs.Models.GetsActiveBranchs;

namespace Engineering.Application.Services.Branchs.Queries.GetsActiveBranchs;

public class GetsActiveBranchsQueryHandler : IQueryHandler<GetsActiveBranchsQuery, DataResult<List<GetsActiveBranchsResponseModel>>>
{
    private readonly IBranchRepository _repository;
    private readonly ILogger<GetsActiveBranchsQuery> _logger;

    public GetsActiveBranchsQueryHandler(
        ILogger<GetsActiveBranchsQuery> logger,
        IBranchRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsActiveBranchsResponseModel>>?>> Handle(GetsActiveBranchsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsActiveBranchs(
                request.FilterData,
                request.CategoryId,
                request.BranchCode,
                request.BranchName,
                request.CompanyId,
                request.PageIndex,
                request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<GetsActiveBranchsResponseModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsActiveBranchsResponseModel>>>(BranchErrors.BranchWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsActiveBranchsResponseModel>>>(SharedErrors.UnknownError);
        }
    }
}