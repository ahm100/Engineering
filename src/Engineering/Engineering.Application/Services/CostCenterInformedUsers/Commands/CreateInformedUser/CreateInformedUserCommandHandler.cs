using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterInformedUser = Engineering.Domain.Entities.CostCenters.CostCenterInformedUser;

namespace Engineering.Application.Services.CostCenterInformedUsers.Commands.CreateInformedUser;

public class CreateInformedUserCommandHandler : ICommandHandler<CreateInformedUserCommand, CostCenterInformedUser>
{
    private readonly ILogger<CreateInformedUserCommand> _logger;
    private readonly ICostCenterInformedUserRepository _repository;

    public CreateInformedUserCommandHandler(ILogger<CreateInformedUserCommand> logger, ICostCenterInformedUserRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterInformedUser?>> Handle(CreateInformedUserCommand request, CT ct)
    {
        try
        {
            return await _repository.Create(new CostCenterInformedUser(request.CostCenter, request.EmployeeId), ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterInformedUser>(SharedErrors.UnknownError);
        }
    }
}