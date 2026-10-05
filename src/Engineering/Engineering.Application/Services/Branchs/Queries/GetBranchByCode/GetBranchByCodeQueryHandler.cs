using Engineering.Application.Abstractions.Data.Branchs;
using Engineering.Application.Services.Branchs.Models.GetBranchByCode;

namespace Engineering.Application.Services.Branchs.Queries.GetBranchByCode;

public class GetBranchByCodeQueryHandler : IQueryHandler<GetBranchByCodeQuery, GetBranchByCodeResponse?>
{
    private readonly ILogger<GetBranchByCodeQueryHandler> _logger;
    private readonly IBranchRepository _repository;

    public GetBranchByCodeQueryHandler(
        ILogger<GetBranchByCodeQueryHandler> logger,
        IBranchRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetBranchByCodeResponse?>> Handle(GetBranchByCodeQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetBranchByCode(
                request.BranchCode,
                request.CategoryId,
                request.CompanyId, ct);

            return item ?? Result.Failure<GetBranchByCodeResponse?>(BranchErrors.BranchWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetBranchByCodeResponse?>(SharedErrors.UnknownError);
        }
    }
}