using Engineering.Application.Abstractions.Data.Branchs;
using Engineering.Application.Services.Branchs.Models.GetBranchByName;

namespace Engineering.Application.Services.Branchs.Queries.GetBranchByName;

public class GetBranchByNameQueryHandler : IQueryHandler<GetBranchByNameQuery, GetBranchByNameResponse?>
{
    private readonly ILogger<GetBranchByNameQueryHandler> _logger;
    private readonly IBranchRepository _repository;

    public GetBranchByNameQueryHandler(
        ILogger<GetBranchByNameQueryHandler> logger,
        IBranchRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetBranchByNameResponse?>> Handle(GetBranchByNameQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetBranchByName(
                request.BranchName,
                request.CategoryId,
                request.CompanyId, ct);
            return item ?? Result.Failure<GetBranchByNameResponse?>(BranchErrors.BranchWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetBranchByNameResponse?>(SharedErrors.UnknownError);
        }
    }
}