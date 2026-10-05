using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterInformedUser = Engineering.Domain.Entities.CostCenters.CostCenterInformedUser;

namespace Engineering.Application.Services.CostCenterInformedUsers.Commands.DeleteInformedUser;

public class DeleteInformedUserCommandHandler : ICommandHandler<DeleteInformedUserCommand, CostCenterInformedUser>
{
    private readonly ILogger<DeleteInformedUserCommand> _logger;
    private readonly ICostCenterInformedUserRepository _repository;

    public DeleteInformedUserCommandHandler(ILogger<DeleteInformedUserCommand> logger, ICostCenterInformedUserRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterInformedUser?>> Handle(DeleteInformedUserCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindUser(request.EmployeeId, request.CostCenterId, ct);
            if (entity is null)
                return Result.Failure<CostCenterInformedUser>(CostCenterInformedUserErrors.WithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<CostCenterInformedUser>(CostCenterInformedUserErrors.IsDeleted);

            entity.SoftDelete();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterInformedUser>(SharedErrors.UnknownError);
        }
    }
}