using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorByContractorContractType;

public class GetsContractorByContractorContractTypeQueryHandler : IQueryHandler<GetsContractorByContractorContractTypeQuery, DataResult<List<ContractorContract>>>
{
    private readonly ILogger<GetsContractorByContractorContractTypeQueryHandler> _logger;
    private readonly IContractorContractRepository _repository;

    public GetsContractorByContractorContractTypeQueryHandler(ILogger<GetsContractorByContractorContractTypeQueryHandler> logger, IContractorContractRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ContractorContract>>?>> Handle(GetsContractorByContractorContractTypeQuery request, CT ct)
    {
        try
        {
            var contractType = (ContractorContractType?)request.ContractorContractTypeId;
            var result = await _repository.GetsContractorByContractorContractType(contractType, request.CompanyId, ct);

            return result.Any() ?
                new DataResult<List<ContractorContract>>
                {
                    Data = result,
                } : Result.Failure<DataResult<List<ContractorContract>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ContractorContract>>>(SharedErrors.UnknownError);
        }
    }
}
