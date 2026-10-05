using Engineering.Application.Abstractions.Data;
using Engineering.Domain.Entities.ContractorEmployees;

namespace Engineering.Application.Services.Contractors.Queries.ContractorEmployees.GetContractorEmployeesByContractorId;

public class GetContractorEmployeesByContractorIdQueryHandler : IQueryHandler<GetContractorEmployeesByContractorIdQuery, DataResult<List<ContractorEmployee>>>
{
    private readonly IContractorEmployeeRepository _repository;
    private readonly ILogger<GetContractorEmployeesByContractorIdQueryHandler> _logger;

    public GetContractorEmployeesByContractorIdQueryHandler(IContractorEmployeeRepository contractorEmployeeRepository, ILogger<GetContractorEmployeesByContractorIdQueryHandler> logger)
    {
        _repository = contractorEmployeeRepository;
        _logger = logger;
    }

    public async Task<Result<DataResult<List<ContractorEmployee>>?>> Handle(GetContractorEmployeesByContractorIdQuery request, CT ct)
    {
        try
        {
            var contractorEmployees = await _repository.GetContractorEmployeesByContractorId(request.Ids,
                                                                                             request.EmployeeId,
                                                                                             ct);

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
