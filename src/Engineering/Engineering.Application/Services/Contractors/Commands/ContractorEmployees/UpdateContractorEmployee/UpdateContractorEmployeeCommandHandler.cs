using Engineering.Application.Abstractions.Data;
using Engineering.Domain.Entities.ContractorEmployees;

namespace Engineering.Application.Services.Contractors.Commands.ContractorEmployees.UpdateContractorEmployee;

public class UpdateContractorEmployeeCommandHandler : ICommandHandler<UpdateContractorEmployeeCommand, ContractorEmployee>
{
    private readonly ILogger<UpdateContractorEmployeeCommandHandler> _logger;
    private readonly IContractorEmployeeRepository _contractorEmployeeRepository;

    public UpdateContractorEmployeeCommandHandler(ILogger<UpdateContractorEmployeeCommandHandler> logger,
                                                  IContractorEmployeeRepository contractorEmployeeRepository
                                                  )
    {
        _contractorEmployeeRepository = contractorEmployeeRepository;
        _logger = logger;
    }

    public async Task<Result<ContractorEmployee?>> Handle(UpdateContractorEmployeeCommand request, CT ct)
    {
        try
        {
            var entity = await _contractorEmployeeRepository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ContractorEmployee>(ContractorEmployeeErrors.ContractorEmployeeIdNotFound);

            entity!.SetContractorId(request.ContractorId);
            entity!.SetEmployeeId(request.EmployeeId);
            entity!.SetCompanyId(request.CompanyId);

            await _contractorEmployeeRepository.Update(entity!);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorEmployee>(SharedErrors.UnknownError);
        }
    }
}