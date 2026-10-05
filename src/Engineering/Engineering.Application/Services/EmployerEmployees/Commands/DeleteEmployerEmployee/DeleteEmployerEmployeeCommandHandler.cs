using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.EmployerEmployees;
using Engineering.Domain.Errors.EmployerEmployees;

namespace Engineering.Application.Services.EmployerEmployees.Commands.DeleteEmployerEmployee;

public class DeleteEmployerEmployeeCommandHandler : ICommandHandler<DeleteEmployerEmployeeCommand, bool?>
{
    private readonly ILogger<DeleteEmployerEmployeeCommand> _logger;
    private readonly IEmployerEmployeeRepository _repository;

    public DeleteEmployerEmployeeCommandHandler(
        ILogger<DeleteEmployerEmployeeCommand> logger,
        IEmployerEmployeeRepository repository,
        IViewThirdPartyRepository thirdPartyRepo)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(DeleteEmployerEmployeeCommand request, CT ct)
    {
        try
        {
            var employerEmployee = await _repository.GetById(request.Id, ct);
            if (employerEmployee is null)
                return Result.Failure<bool?>(EmployerEmployeeErrors.EmployerEmployeeNotFound);

            employerEmployee.SoftDelete();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }
}