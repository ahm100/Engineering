using Engineering.Application.Abstractions.Data.Branchs;
using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Application.Services.Branchs.Commands.InactiveBranch;

public class InactiveBranchCommandHandler : ICommandHandler<InactiveBranchCommand, Branch>
{
    private readonly ILogger<InactiveBranchCommand> _logger;
    private readonly IBranchRepository _repository;

    public InactiveBranchCommandHandler(
        ILogger<InactiveBranchCommand> logger,
        IBranchRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Branch?>> Handle(InactiveBranchCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;
            if (entity.IsActive == false)
                return Result.Failure<Branch>(BranchErrors.IsInactive);

            entity.SetDeactivate();
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