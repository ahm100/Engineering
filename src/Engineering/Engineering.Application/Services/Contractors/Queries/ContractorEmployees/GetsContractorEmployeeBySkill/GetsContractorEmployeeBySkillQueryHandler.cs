using Engineering.Application.Abstractions.Data;
using Engineering.Domain.Entities.ContractorEmployees;

namespace Engineering.Application.Services.Contractors.Queries.ContractorEmployees.GetsContractorEmployeeBySkill;

public class GetsContractorEmployeeBySkillQueryHandler : IQueryHandler<GetsContractorEmployeeBySkillQuery, DataResult<List<ContractorEmployee>>>
{
    private readonly IContractorEmployeeRepository _repository;
    private readonly ILogger<GetsContractorEmployeeBySkillQueryHandler> _logger;

    public GetsContractorEmployeeBySkillQueryHandler(IContractorEmployeeRepository contractorEmployeeRepository, ILogger<GetsContractorEmployeeBySkillQueryHandler> logger)
    {
        _repository = contractorEmployeeRepository;
        _logger = logger;
    }

    public async Task<Result<DataResult<List<ContractorEmployee>>?>> Handle(GetsContractorEmployeeBySkillQuery request, CT ct)
    {
        try
        {
            var contractorEmployees = await _repository.GetsContractorEmployeeBySkill(request.ContractorId, request.CompanyId, ct);

            var response = contractorEmployees.Data.Any()
                       ? new DataResult<List<ContractorEmployee>>
                       {
                           Data = contractorEmployees.Data,
                           RowCount = contractorEmployees.RowCount
                       }
                      : Result.Failure<DataResult<List<ContractorEmployee>>>(SharedErrors.ItemNotFound);


            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<ContractorEmployee>>>(SharedErrors.UnknownError);
        }
    }
}
