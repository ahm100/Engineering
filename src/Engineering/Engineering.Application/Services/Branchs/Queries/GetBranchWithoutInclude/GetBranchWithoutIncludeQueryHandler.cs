using Engineering.Application.Abstractions.Data.Branchs;
using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Application.Services.Branchs.Queries.GetBranchWithoutInclude;

public class GetBranchWithoutIncludeQueryHandler : IQueryHandler<GetBranchWithoutIncludeQuery, Branch?>
{
    private readonly ILogger<GetBranchWithoutIncludeQueryHandler> _logger;
    private readonly IBranchRepository _repository;

    public GetBranchWithoutIncludeQueryHandler(
        ILogger<GetBranchWithoutIncludeQueryHandler> logger,
        IBranchRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Branch?>> Handle(GetBranchWithoutIncludeQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetBranchWithoutInclude(request.Id, ct);
            return item ?? Result.Failure<Branch?>(BranchErrors.BranchWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Branch?>(SharedErrors.UnknownError);
        }
    }
}