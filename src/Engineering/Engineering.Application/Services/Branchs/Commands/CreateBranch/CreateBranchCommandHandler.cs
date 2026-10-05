using Engineering.Application.Abstractions.Data.Branchs;
using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Application.Services.Branchs.Commands.CreateBranch;

public class CreateBranchCommandHandler : ICommandHandler<CreateBranchCommand, Branch?>
{
    private readonly ILogger<CreateBranchCommand> _logger;
    private readonly IBranchRepository _repository;

    public CreateBranchCommandHandler(
        ILogger<CreateBranchCommand> logger,
        IBranchRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Branch?>> Handle(CreateBranchCommand request, CT ct)
    {
        try
        {
            var entity = new Branch(
                request.Category,
                request.BranchName,
                request.BranchCode,
                request.IsActive,
                request.CompanyId);

            var result = await _repository.Create(entity, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Branch?>(SharedErrors.UnknownError);
        }
    }
}