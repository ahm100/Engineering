using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetFixContractorContractByIdIncludeless;

public class GetFixContractorContractByIdIncludelessQueryHandler : IQueryHandler<GetFixContractorContractByIdIncludelessQuery, ContractorContract?>
{
    private readonly ILogger<GetFixContractorContractByIdIncludelessQueryHandler> _logger;
    private readonly IContractorContractRepository _repository;

    public GetFixContractorContractByIdIncludelessQueryHandler(
        ILogger<GetFixContractorContractByIdIncludelessQueryHandler> logger,
        IContractorContractRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorContract?>> Handle(GetFixContractorContractByIdIncludelessQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFixContractorContractByIdIncludeless(request.Id, request.CompanyId, ct);
            if (result is null)
                return Result.Failure<ContractorContract>(ContractorContractErrors.InValidContractorContractId);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorContract?>(SharedErrors.UnknownError);
        }
    }
}
