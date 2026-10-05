using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetServiceContractorContractByIdIncludeless;

public class GetServiceContractorContractByIdIncludelessQueryHandler : IQueryHandler<GetServiceContractorContractByIdIncludelessQuery, ContractorContract?>
{
    private readonly ILogger<GetServiceContractorContractByIdIncludelessQueryHandler> _logger;
    private readonly IContractorContractRepository _repository;

    public GetServiceContractorContractByIdIncludelessQueryHandler(
        ILogger<GetServiceContractorContractByIdIncludelessQueryHandler> logger,
        IContractorContractRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorContract?>> Handle(GetServiceContractorContractByIdIncludelessQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetServiceContractorContractByIdIncludeless(request.Id, request.CompanyId, ct);
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
