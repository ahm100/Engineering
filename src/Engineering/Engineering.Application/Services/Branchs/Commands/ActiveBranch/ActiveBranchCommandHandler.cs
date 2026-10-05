using Engineering.Application.Abstractions.Data.Branchs;
using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Application.Services.Branchs.Commands.ActiveBranch;

public class ActiveBranchCommandHandler : ICommandHandler<ActiveBranchCommand, Branch>
{
    private readonly ILogger<ActiveBranchCommand> _logger;
    private readonly IBranchRepository _repository;

    public ActiveBranchCommandHandler(
        ILogger<ActiveBranchCommand> logger,
        IBranchRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Branch?>> Handle(ActiveBranchCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;
            if (entity.IsActive)
                return Result.Failure<Branch>(BranchErrors.IsActive);

            entity.SetActive();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Branch>(SharedErrors.UnknownError);
        }
    }
}