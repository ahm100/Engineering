using Engineering.Application.Abstractions.Data;
using Engineering.Domain.Entities.ContractorEmployees;

namespace Engineering.Application.Services.Contractors.Commands.ContractorEmployees.CreateContractorEmployee;

public class CreateContractorEmployeeCommandHandler : ICommandHandler<CreateContractorEmployeeCommand, ContractorEmployee>
{
    private readonly ILogger<CreateContractorEmployeeCommandHandler> _logger;
    private readonly IContractorEmployeeRepository _contractorEmployeeRepository;

    public CreateContractorEmployeeCommandHandler(ILogger<CreateContractorEmployeeCommandHandler> logger,
                                                  IContractorEmployeeRepository contractorEmployeeRepository
                                                  )
    {
        _contractorEmployeeRepository = contractorEmployeeRepository;
        _logger = logger;
    }

    public async Task<Result<ContractorEmployee?>> Handle(CreateContractorEmployeeCommand request, CT ct)
    {
        try
        {
            var exists = await _contractorEmployeeRepository.ExistAsync(request.EmployeeId, request.ContractorId, ct);
            if (exists)
                return Result.Failure<ContractorEmployee>(ContractorEmployeeErrors.ContractorEmployeeIsDuplicate);

            var activeEmployee = await _contractorEmployeeRepository.GetActiveContractorEmployeesByEmployeeId(request.EmployeeId, ct);
            if (!request.IsConfirm)
            {
                if (activeEmployee is not null)
                    return Result.Failure<ContractorEmployee>(ContractorEmployeeErrors.IsDuplicatedActive);
            }
            else
            {
                if (activeEmployee is not null)
                    activeEmployee.SetIsActive(false);
            }

            var entity = ContractorEmployee.Create(request.EmployeeId, request.ContractorId, request.IsActive, request.CompanyId);
            var result = await _contractorEmployeeRepository.Create(entity, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorEmployee>(SharedErrors.UnknownError);
        }
    }
}