using Engineering.Application.Abstractions.Data.Branchs;
using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Application.Services.Branchs.Queries.GetBranchById;

public class GetBranchByIdQueryHandler : IQueryHandler<GetBranchByIdQuery, Branch?>
{
    private readonly ILogger<GetBranchByIdQueryHandler> _logger;
    private readonly IBranchRepository _repository;

    public GetBranchByIdQueryHandler(
        ILogger<GetBranchByIdQueryHandler> logger,
        IBranchRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Branch?>> Handle(GetBranchByIdQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetBranchByIdWithCategory(request.Id, ct);
            return item ?? Result.Failure<Branch?>(BranchErrors.BranchWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Branch?>(SharedErrors.UnknownError);
        }
    }
}