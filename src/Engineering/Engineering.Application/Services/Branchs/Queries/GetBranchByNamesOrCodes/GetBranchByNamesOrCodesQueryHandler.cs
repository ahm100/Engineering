using Engineering.Application.Abstractions.Data.Branchs;

namespace Engineering.Application.Services.Branchs.Queries.GetBranchByNamesOrCodes;

public class GetBranchByNamesOrCodesQueryHandler : IQueryHandler<GetBranchByNamesOrCodesQuery, bool>
{
    private readonly ILogger<GetBranchByNamesOrCodesQueryHandler> _logger;
    private readonly IBranchRepository _repository;

    public GetBranchByNamesOrCodesQueryHandler(
        ILogger<GetBranchByNamesOrCodesQueryHandler> logger,
        IBranchRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(GetBranchByNamesOrCodesQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetBranchByNamesOrCodes(
                request.Names,
                request.Codes,
                request.CategoryId,
                request.CompanyId, ct);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}