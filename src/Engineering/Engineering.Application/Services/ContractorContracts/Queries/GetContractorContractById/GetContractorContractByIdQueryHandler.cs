using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetContractorContractById;

public class GetContractorContractByIdQueryHandler : IQueryHandler<GetContractorContractByIdQuery, ContractorContract?>
{
    private readonly ILogger<GetContractorContractByIdQueryHandler> _logger;
    private readonly IContractorContractRepository _repository;

    public GetContractorContractByIdQueryHandler(
        ILogger<GetContractorContractByIdQueryHandler> logger,
        IContractorContractRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorContract?>> Handle(GetContractorContractByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetContractorContractById(request.ContractorContractId, request.CompanyId, ct);
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
