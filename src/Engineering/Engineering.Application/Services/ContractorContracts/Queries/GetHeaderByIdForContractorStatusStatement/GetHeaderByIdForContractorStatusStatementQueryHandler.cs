using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetHeaderByIdForContractorStatusStatement;

public class GetHeaderByIdForContractorStatusStatementQueryHandler : IQueryHandler<GetHeaderByIdForContractorStatusStatementQuery, ContractorContractHeader?>
{
    private readonly ILogger<GetHeaderByIdForContractorStatusStatementQueryHandler> _logger;
    private readonly IContractorContractHeaderRepository _repository;

    public GetHeaderByIdForContractorStatusStatementQueryHandler(
        ILogger<GetHeaderByIdForContractorStatusStatementQueryHandler> logger,
        IContractorContractHeaderRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorContractHeader?>> Handle(GetHeaderByIdForContractorStatusStatementQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetHeaderByIdForContractorStatusStatement(request.Id, ct);
            if (result is null)
                return Result.Failure<ContractorContractHeader>(ContractorContractErrors.InValidContractorContractId);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorContractHeader?>(SharedErrors.UnknownError);
        }
    }
}
