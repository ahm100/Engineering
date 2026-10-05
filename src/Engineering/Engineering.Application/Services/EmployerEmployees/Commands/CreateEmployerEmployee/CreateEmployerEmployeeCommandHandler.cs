using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.EmployerEmployees;
using Engineering.Domain.Entities.EmployerEmployees;
using IdentityServer.ClientSdk.Services;

namespace Engineering.Application.Services.EmployerEmployees.Commands.CreateEmployerEmployee;

public class CreateEmployerEmployeeCommandHandler : ICommandHandler<CreateEmployerEmployeeCommand, EmployerEmployee>
{
    private readonly ILogger<CreateEmployerEmployeeCommand> _logger;
    private readonly IEmployerEmployeeRepository _repository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;
    private readonly IUserInfoProvider _userInfoProvider;

    public CreateEmployerEmployeeCommandHandler(
        ILogger<CreateEmployerEmployeeCommand> logger,
        IEmployerEmployeeRepository repository,
        IViewThirdPartyRepository thirdPartyRepo,
        IUserInfoProvider userInfoProvider)
    {
        _logger = logger;
        _repository = repository;
        _userInfoProvider = userInfoProvider;
        _thirdPartyRepo = thirdPartyRepo;
    }

    public async Task<Result<EmployerEmployee?>> Handle(CreateEmployerEmployeeCommand request, CT ct)
    {
        try
        {
            var companyId = _userInfoProvider.CompanyId;

            List<long> users = [request.EmployeeId, request.EmployerId];
            var getUsers = await _thirdPartyRepo.GetByIds(users, ct);
            if (getUsers == null || getUsers.Count != users.Count)
                return Result.Failure<EmployerEmployee>(SharedErrors.UnknownError);
            var create = await _repository.Create(new EmployerEmployee(request.EmployeeId, request.EmployerId, request.IsActive, companyId), ct);

            return create;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerEmployee>(SharedErrors.UnknownError);
        }
    }
}