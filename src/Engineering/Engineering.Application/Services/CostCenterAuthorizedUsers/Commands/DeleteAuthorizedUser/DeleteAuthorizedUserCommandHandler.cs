using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterAuthorizedUser = Engineering.Domain.Entities.CostCenters.CostCenterAuthorizedUser;

namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Commands.DeleteAuthorizedUser;

public class DeleteAuthorizedUserCommandHandler : ICommandHandler<DeleteAuthorizedUserCommand, CostCenterAuthorizedUser>
{
    private readonly ILogger<DeleteAuthorizedUserCommand> _logger;
    private readonly ICostCenterAuthorizedUserRepository _repository;

    public DeleteAuthorizedUserCommandHandler(ILogger<DeleteAuthorizedUserCommand> logger, ICostCenterAuthorizedUserRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterAuthorizedUser?>> Handle(DeleteAuthorizedUserCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindUser(request.AuthorizedUserId, request.CostCenterId, ct);
            if (entity is null)
                return Result.Failure<CostCenterAuthorizedUser>(CostCenterAuthorizedUserErrors.WithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<CostCenterAuthorizedUser>(CostCenterAuthorizedUserErrors.IsDeleted);

            entity.SetIsDeleted();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterAuthorizedUser>(SharedErrors.UnknownError);
        }
    }
}