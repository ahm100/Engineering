using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterAuthorizedUser = Engineering.Domain.Entities.CostCenters.CostCenterAuthorizedUser;

namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Commands.CreateAuthorizedUser;

public class CreateAuthorizedUserCommandHandler : ICommandHandler<CreateAuthorizedUserCommand, CostCenterAuthorizedUser>
{
    private readonly ILogger<CreateAuthorizedUserCommand> _logger;
    private readonly ICostCenterAuthorizedUserRepository _repository;

    public CreateAuthorizedUserCommandHandler(ILogger<CreateAuthorizedUserCommand> logger, ICostCenterAuthorizedUserRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterAuthorizedUser?>> Handle(CreateAuthorizedUserCommand request, CT ct)
    {
        try
        {
            return await _repository.Create(new CostCenterAuthorizedUser(request.CostCenter, request.AuthorizedUserId), ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterAuthorizedUser>(SharedErrors.UnknownError);
        }
    }
}