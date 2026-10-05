using Engineering.Application.Abstractions.Data;
using Engineering.Domain.Entities.ContractorEmployees;

namespace Engineering.Application.Services.Contractors.Commands.ContractorEmployees.ChangeActiveContractorEmployee;

public class ChangeActiveContractorEmployeeCommandHandler : ICommandHandler<ChangeActiveContractorEmployeeCommand, ContractorEmployee>
{
    private readonly IContractorEmployeeRepository _contractorEmployeeRepository;
    private readonly ILogger<ChangeActiveContractorEmployeeCommandHandler> _logger;

    public ChangeActiveContractorEmployeeCommandHandler(ILogger<ChangeActiveContractorEmployeeCommandHandler> logger,
                                                  IContractorEmployeeRepository contractorEmployeeRepository)
    {
        _contractorEmployeeRepository = contractorEmployeeRepository;
        _logger = logger;
    }

    public async Task<Result<ContractorEmployee?>> Handle(ChangeActiveContractorEmployeeCommand request, CT ct)
    {
        try
        {
            var entity = await _contractorEmployeeRepository.FindById(request.Id, ct);

            if (entity is null)
                return Result.Failure<ContractorEmployee>(ContractorEmployeeErrors.ContractorEmployeeIdNotFound);

            if (entity.IsDeleted)
                return Result.Failure<ContractorEmployee>(ContractorEmployeeErrors.IsDeleted);

            bool active = !entity.IsActive;
            if (request.IsActive is not null)
                active = request.IsActive.Value!;

            if (active)
            {
                var exists = await _contractorEmployeeRepository.GetActiveContractorEmployeesByEmployeeId(entity.EmployeeId, ct);
                if (!request.IsConfirm)
                {
                    if (exists is not null)
                        return Result.Failure<ContractorEmployee>(ContractorEmployeeErrors.IsDuplicatedActive);
                }
                else
                {
                    if (exists is not null)
                        exists.SetIsActive(false);
                }
            }

            entity.SetIsActive(active);

            await _contractorEmployeeRepository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorEmployee>(SharedErrors.UnknownError);
        }
    }
}
