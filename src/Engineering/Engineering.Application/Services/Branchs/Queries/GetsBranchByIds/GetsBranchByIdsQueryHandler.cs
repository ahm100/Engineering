using Engineering.Application.Abstractions.Data.Branchs;
using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Application.Services.Branchs.Queries.GetsBranchByIds;

public class GetsBranchByIdsQueryHandler : IQueryHandler<GetsBranchByIdsQuery, List<Branch>>
{
    private readonly IBranchRepository _repository;
    private readonly ILogger<GetsBranchByIdsQuery> _logger;

    public GetsBranchByIdsQueryHandler(
        ILogger<GetsBranchByIdsQuery> logger,
        IBranchRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<Branch>?>> Handle(GetsBranchByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsBranchByIds(request.Items, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<Branch>>(SharedErrors.UnknownError);
        }
    }
}