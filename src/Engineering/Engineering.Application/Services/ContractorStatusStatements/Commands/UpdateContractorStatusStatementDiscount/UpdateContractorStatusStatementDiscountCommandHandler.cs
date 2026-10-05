using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.UpdateContractorStatusStatementDiscount;

public class UpdateContractorStatusStatementDiscountCommandHandler : ICommandHandler<UpdateContractorStatusStatementDiscountCommand, ContractorStatusStatementDiscount>
{
    private readonly ILogger<UpdateContractorStatusStatementDiscountCommandHandler> _logger;
    private readonly IContractorStatusStatementDiscountRepository _repository;

    public UpdateContractorStatusStatementDiscountCommandHandler(
        ILogger<UpdateContractorStatusStatementDiscountCommandHandler> logger,
        IContractorStatusStatementDiscountRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorStatusStatementDiscount?>> Handle(UpdateContractorStatusStatementDiscountCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ContractorStatusStatementDiscount>(CSSErrors.DiscountNotFound);

            entity.SetDescription(request.Description);
            entity.SetDiscountPrice(request.DiscountPrice);
            entity.SetRegistrationDate(request.RegistrationDate);

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorStatusStatementDiscount>(SharedErrors.UnknownError);
        }
    }
}
