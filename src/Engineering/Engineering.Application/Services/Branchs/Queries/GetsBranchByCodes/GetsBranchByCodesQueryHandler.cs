using Engineering.Application.Abstractions.Data.Branchs;
using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Application.Services.Branchs.Queries.GetsBranchByCodes;

public class GetsBranchByCodesQueryHandler : IQueryHandler<GetsBranchByCodesQuery, DataResult<List<Branch>>>
{
    private readonly IBranchRepository _repository;
    private readonly ILogger<GetsBranchByCodesQueryHandler> _logger;

    public GetsBranchByCodesQueryHandler(
        ILogger<GetsBranchByCodesQueryHandler> logger,
        IBranchRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<Branch>>?>> Handle(GetsBranchByCodesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsBranchByCodes(
                request.Codes,
                request.CategoryId,
                request.CompanyId, ct);

            return result.Data.Any() ?
                new DataResult<List<Branch>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<Branch>>>(BranchErrors.FilteredBranchNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<Branch>>>(SharedErrors.UnknownError);
        }
    }
}