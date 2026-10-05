using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterAuthorizedUser = Engineering.Domain.Entities.CostCenters.CostCenterAuthorizedUser;

namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Commands.CreateAuthorizedUsers;

public class CreateAuthorizedUsersCommandHandler : ICommandHandler<CreateAuthorizedUsersCommand, List<CostCenterAuthorizedUser?>>
{
    private readonly ILogger<CreateAuthorizedUsersCommand> _logger;
    private readonly ICostCenterAuthorizedUserRepository _repository;

    public CreateAuthorizedUsersCommandHandler(ILogger<CreateAuthorizedUsersCommand> logger, ICostCenterAuthorizedUserRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    public async Task<Result<List<CostCenterAuthorizedUser?>>> Handle(CreateAuthorizedUsersCommand request, CT ct)
    {
        try
        {
            var response = new List<CostCenterAuthorizedUser?>();
            if (request.CostCenter.CostCenterAuthorizedUsers is not null || request.CostCenter.CostCenterAuthorizedUsers?.Count > 0)
                foreach (var item in request.CostCenter.CostCenterAuthorizedUsers)
                    await _repository.Remove(item);

            foreach (var item in request.UserIds)
                response.Add(await _repository.Create(new CostCenterAuthorizedUser(request.CostCenter, item), ct));

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return Result.Failure<List<CostCenterAuthorizedUser?>>(SharedErrors.UnknownError);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }
    }
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
}
