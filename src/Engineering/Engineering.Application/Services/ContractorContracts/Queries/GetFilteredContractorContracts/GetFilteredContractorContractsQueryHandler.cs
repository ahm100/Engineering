using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetFilteredContractorContracts;

public class GetFilteredContractorContractsQueryHandler : IQueryHandler<GetFilteredContractorContractsQuery, DataResult<List<ContractorContract>>>
{
    private readonly ILogger<GetFilteredContractorContractsQueryHandler> _logger;
    private readonly IContractorContractRepository _repository;

    public GetFilteredContractorContractsQueryHandler(ILogger<GetFilteredContractorContractsQueryHandler> logger,
                                                      IContractorContractRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ContractorContract>>?>> Handle(GetFilteredContractorContractsQuery request, CT ct)
    {
        try
        {
            var contractType = (ContractorContractType?)request.ContractorContractTypeId;
            var result = await _repository.GetFilteredAsync(request.ContractorId, request.CostCenterId, request.ProjectIds, request.ProjectOperationIds, request.EmployerContracts,
                request.FromDate, request.ToDate, request.Status, contractType, request.FilterData, request.OrderBy, request.CompanyId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ContractorContract>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ContractorContract>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ContractorContract>>>(SharedErrors.UnknownError);
        }
    }
}
