using Engineering.Application.Abstractions.Data;
using Engineering.Domain.Entities.ContractorEmployees;

namespace Engineering.Application.Services.Contractors.Queries.ContractorEmployees.GetActiveContractorEmployeesByEmployeeId;

public class GetActiveContractorEmployeesByEmployeeIdQueryHandler : IQueryHandler<GetActiveContractorEmployeesByEmployeeIdQuery, ContractorEmployee>
{
    private readonly IContractorEmployeeRepository _repository;
    private readonly ILogger<GetActiveContractorEmployeesByEmployeeIdQueryHandler> _logger;

    public GetActiveContractorEmployeesByEmployeeIdQueryHandler(IContractorEmployeeRepository contractorEmployeeRepository, ILogger<GetActiveContractorEmployeesByEmployeeIdQueryHandler> logger)
    {
        _repository = contractorEmployeeRepository;
        _logger = logger;
    }

    public async Task<Result<ContractorEmployee?>> Handle(GetActiveContractorEmployeesByEmployeeIdQuery request, CT ct)
    {
        try
        {
            var contractorEmployee = await _repository.GetActiveContractorEmployeesByEmployeeId(request.Id,
                                                                                                ct);

            return contractorEmployee;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<ContractorEmployee>(SharedErrors.UnknownError);
        }
    }
}
