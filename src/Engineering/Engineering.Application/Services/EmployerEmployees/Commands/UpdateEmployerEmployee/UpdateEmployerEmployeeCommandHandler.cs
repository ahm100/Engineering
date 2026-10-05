using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.EmployerEmployees;
using Engineering.Domain.Entities.EmployerEmployees;
using Engineering.Domain.Errors.EmployerEmployees;

namespace Engineering.Application.Services.EmployerEmployees.Commands.UpdateEmployerEmployee;

public class UpdateEmployerEmployeeCommandHandler : ICommandHandler<UpdateEmployerEmployeeCommand, EmployerEmployee>
{
    private readonly ILogger<UpdateEmployerEmployeeCommand> _logger;
    private readonly IEmployerEmployeeRepository _repository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;

    public UpdateEmployerEmployeeCommandHandler(
        ILogger<UpdateEmployerEmployeeCommand> logger,
        IEmployerEmployeeRepository repository,
        IViewThirdPartyRepository thirdPartyRepo)
    {
        _logger = logger;
        _repository = repository;
        _thirdPartyRepo = thirdPartyRepo;
    }

    public async Task<Result<EmployerEmployee?>> Handle(UpdateEmployerEmployeeCommand request, CT ct)
    {
        try
        {
            var employerEmployee = await _repository.GetById(request.Id, ct);
            if (employerEmployee is null)
                return Result.Failure<EmployerEmployee?>(EmployerEmployeeErrors.EmployerEmployeeNotFound);
            if (request.EmployerId is not null)
            {
                var employer = await _thirdPartyRepo.GetByIds([request.EmployerId.Value], ct);
                if (employer is null)
                    return Result.Failure<EmployerEmployee?>(EmployerEmployeeErrors.EmployerNotFound);
            }

            employerEmployee.Update(request.EmployerId, request.IsActive);

            return employerEmployee;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerEmployee>(SharedErrors.UnknownError);
        }
    }
}