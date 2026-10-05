using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractHeaderById;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetContractorContractHeaderByIdNew;

public class GetContractorContractHeaderByIdNewQueryHandler : IQueryHandler<GetContractorContractHeaderByIdNewQuery, GetContractorContractHeaderByIdResponse?>
{
    private readonly ILogger<GetContractorContractHeaderByIdNewQueryHandler> _logger;
    private readonly IContractorContractHeaderRepository _repository;

    public GetContractorContractHeaderByIdNewQueryHandler(
        ILogger<GetContractorContractHeaderByIdNewQueryHandler> logger,
        IContractorContractHeaderRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetContractorContractHeaderByIdResponse?>> Handle(GetContractorContractHeaderByIdNewQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetContractorContractHeaderByIdNew(request.Id, request.CompanyId, ct);
            if (result is null)
                return Result.Failure<GetContractorContractHeaderByIdResponse>(ContractorContractErrors.InValidContractorContractId);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetContractorContractHeaderByIdResponse?>(SharedErrors.UnknownError);
        }
    }
}
