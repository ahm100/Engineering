using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementDiscount;

public class CreateContractorStatusStatementDiscountCommandHandler : ICommandHandler<CreateContractorStatusStatementDiscountCommand, ContractorStatusStatementDiscount>
{
    private readonly ILogger<CreateContractorStatusStatementDiscountCommandHandler> _logger;
    private readonly IContractorStatusStatementDiscountRepository _repository;

    public CreateContractorStatusStatementDiscountCommandHandler(
        ILogger<CreateContractorStatusStatementDiscountCommandHandler> logger,
        IContractorStatusStatementDiscountRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorStatusStatementDiscount?>> Handle(CreateContractorStatusStatementDiscountCommand request, CT ct)
    {
        try
        {
            var entity = new ContractorStatusStatementDiscount(
                request.ContractorStatusStatement,
                request.RequestReward,
                request.RegistrationDate,
                request.DiscountPrice,
                request.Description);

            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorStatusStatementDiscount>(SharedErrors.UnknownError);
        }
    }
}
