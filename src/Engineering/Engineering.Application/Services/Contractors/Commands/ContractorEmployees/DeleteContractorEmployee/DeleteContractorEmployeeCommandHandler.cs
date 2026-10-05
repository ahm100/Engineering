using Engineering.Application.Abstractions.Data;
using Engineering.Domain.Entities.ContractorEmployees;

namespace Engineering.Application.Services.Contractors.Commands.ContractorEmployees.DeleteContractorEmployee;

public class DeleteContractorEmployeeCommandHandler : ICommandHandler<DeleteContractorEmployeeCommand, ContractorEmployee>
{
    private readonly IContractorEmployeeRepository _contractorEmployeeRepository;
    private readonly ILogger<DeleteContractorEmployeeCommandHandler> _logger;

    public DeleteContractorEmployeeCommandHandler(ILogger<DeleteContractorEmployeeCommandHandler> logger,
                                                  IContractorEmployeeRepository contractorEmployeeRepository)
    {
        _contractorEmployeeRepository = contractorEmployeeRepository;
        _logger = logger;
    }

    public async Task<Result<ContractorEmployee?>> Handle(DeleteContractorEmployeeCommand request, CT ct)
    {
        try
        {
            var entity = await _contractorEmployeeRepository.FindById(request.Id, ct);

            if (entity is null)
            {
                return Result.Failure<ContractorEmployee>(ContractorEmployeeErrors.ContractorEmployeeIdNotFound);
            }

            if (entity.IsDeleted)
            {
                return Result.Failure<ContractorEmployee>(ContractorEmployeeErrors.IsDeleted);
            }

            entity.SetIsDeleted();
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
