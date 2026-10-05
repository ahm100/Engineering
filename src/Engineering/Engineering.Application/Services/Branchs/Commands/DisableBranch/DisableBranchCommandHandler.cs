using Engineering.Application.Abstractions.Data.Branchs;
using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Application.Services.Branchs.Commands.DisableBranch;

public class DisableBranchCommandHandler : ICommandHandler<DisableBranchCommand, Branch>
{
    private readonly ILogger<DisableBranchCommand> _logger;
    private readonly IBranchRepository _repository;

    public DisableBranchCommandHandler(
        ILogger<DisableBranchCommand> logger,
        IBranchRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Branch?>> Handle(DisableBranchCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;
            if (entity.Seasons.Any(s => s.OperationInfoSeasons.Count > 0))
                return Result.Failure<Branch>(BranchErrors.CanNottDelete);

            entity.SoftDelete();
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