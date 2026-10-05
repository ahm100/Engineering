using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterInformedUser = Engineering.Domain.Entities.CostCenters.CostCenterInformedUser;

namespace Engineering.Application.Services.CostCenterInformedUsers.Commands.CreateInformedUsers;

public class CreateInformedUsersCommandHandler : ICommandHandler<CreateInformedUsersCommand, List<CostCenterInformedUser?>>
{
    private readonly ILogger<CreateInformedUsersCommand> _logger;
    private readonly ICostCenterInformedUserRepository _repository;

    public CreateInformedUsersCommandHandler(ILogger<CreateInformedUsersCommand> logger, ICostCenterInformedUserRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    public async Task<Result<List<CostCenterInformedUser?>>> Handle(CreateInformedUsersCommand request, CT ct)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    {
        try
        {
            var response = new List<CostCenterInformedUser?>();
            if (request.CostCenter.InformedUsers is not null || request.CostCenter.InformedUsers?.Count > 0)
                foreach (var item in request.CostCenter.InformedUsers)
                    await _repository.Remove(item);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            foreach (var item in request.EmployeeIds)
                response.Add(await _repository.Create(new CostCenterInformedUser(request.CostCenter, (long)item!), ct));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return Result.Failure<List<CostCenterInformedUser?>>(SharedErrors.UnknownError);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }
    }
}
