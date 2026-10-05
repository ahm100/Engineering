using Engineering.Application.Abstractions.Data.Contracts;
using Engineering.Application.Services.CabinTypes.Models.GetCabinTypeByCode;
using Engineering.Domain.Entities.Contracts;

namespace Engineering.Application.Services.Contracts.Queries.GetContractById;

public class GetContractByIdQueryHandler : IQueryHandler<GetContractByIdQuery, Contract?>
{
    private readonly ILogger<GetContractByIdQueryHandler> _logger;
    private readonly IContractRepository _repository;

    public GetContractByIdQueryHandler(
        ILogger<GetContractByIdQueryHandler> logger,
        IContractRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Contract?>> Handle(
        GetContractByIdQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.GetContractById(
                request.Id, ct);
            return entity ?? Result.Failure<Contract>(ContractErrors.ContractNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Contract>(SharedErrors.UnknownError);
        }
    }
}
