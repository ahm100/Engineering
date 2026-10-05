using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementProduct;

public class CreateContractorStatusStatementProductCommandHandler : ICommandHandler<CreateContractorStatusStatementProductCommand, ContractorStatusStatementProduct>
{
    private readonly ILogger<CreateContractorStatusStatementProductCommandHandler> _logger;
    private readonly IContractorStatusStatementProductRepository _repository;

    public CreateContractorStatusStatementProductCommandHandler(
        ILogger<CreateContractorStatusStatementProductCommandHandler> logger,
        IContractorStatusStatementProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorStatusStatementProduct?>> Handle(CreateContractorStatusStatementProductCommand request, CT ct)
    {
        try
        {
            var result = await _repository.Create(request.Entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorStatusStatementProduct>(SharedErrors.UnknownError);
        }
    }
}
