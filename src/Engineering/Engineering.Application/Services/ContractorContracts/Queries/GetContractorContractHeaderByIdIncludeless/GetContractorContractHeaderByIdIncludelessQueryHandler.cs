using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetContractorContractHeaderByIdIncludeless;

public class GetContractorContractHeaderByIdIncludelessQueryHandler : IQueryHandler<GetContractorContractHeaderByIdIncludelessQuery, ContractorContractHeader?>
{
    private readonly ILogger<GetContractorContractHeaderByIdIncludelessQueryHandler> _logger;
    private readonly IContractorContractHeaderRepository _repository;

    public GetContractorContractHeaderByIdIncludelessQueryHandler(ILogger<GetContractorContractHeaderByIdIncludelessQueryHandler> logger,
                                                 IContractorContractHeaderRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorContractHeader?>> Handle(GetContractorContractHeaderByIdIncludelessQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetContractorContractHeaderByIdIncludeless(request.Id, ct);
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
