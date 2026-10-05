using Engineering.Application.Abstractions.Data.Branchs;
using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Application.Services.Branchs.Queries.GetsBranchs;

public class GetsBranchsQueryHandler : IQueryHandler<GetsBranchsQuery, DataResult<List<Branch>>>
{
    private readonly IBranchRepository _repository;
    private readonly ILogger<GetsBranchsQueryHandler> _logger;

    public GetsBranchsQueryHandler(
        ILogger<GetsBranchsQueryHandler> logger,
        IBranchRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<Branch>>?>> Handle(GetsBranchsQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsBranchs(
                request.Ids,
                request.FilterData,
                request.CategoryId,
                request.BranchCode,
                request.BranchName,
                request.IsActive,
                request.OrderBy,
                request.CompanyId,
                request.PageIndex,
                request.PageSize, ct);

            return items.Data.Any() ?
                new DataResult<List<Branch>>
                {
                    Data = items.Data,
                    RowCount = items.RowCount
                } : Result.Failure<DataResult<List<Branch>>>(BranchErrors.FilteredBranchNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<Branch>>>(SharedErrors.UnknownError);
        }
    }
}