using Engineering.Application.Abstractions.Data.Branchs;
using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Application.Services.Branchs.Queries.HaveBranchChild;

public class HaveBranchChildQueryHandler : IQueryHandler<HaveBranchChildQuery, Branch?>
{
    private readonly ILogger<HaveBranchChildQueryHandler> _logger;
    private readonly IBranchRepository _repository;

    public HaveBranchChildQueryHandler(
        ILogger<HaveBranchChildQueryHandler> logger,
        IBranchRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Branch?>> Handle(HaveBranchChildQuery request, CT ct)
    {
        try
        {
            var item = await _repository.HaveBranchChild(request.Id, ct);
            return item ?? Result.Failure<Branch?>(BranchErrors.BranchChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Branch?>(SharedErrors.UnknownError);
        }
    }
}