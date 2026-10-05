using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetContractorContractHeaderById;

public class GetContractorContractHeaderByIdQueryHandler : IQueryHandler<GetContractorContractHeaderByIdQuery, ContractorContractHeader?>
{
    private readonly ILogger<GetContractorContractHeaderByIdQueryHandler> _logger;
    private readonly IContractorContractHeaderRepository _repository;

    public GetContractorContractHeaderByIdQueryHandler(ILogger<GetContractorContractHeaderByIdQueryHandler> logger,
                                                 IContractorContractHeaderRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorContractHeader?>> Handle(GetContractorContractHeaderByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetContractorContractHeaderById(request.Id, ct);
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
