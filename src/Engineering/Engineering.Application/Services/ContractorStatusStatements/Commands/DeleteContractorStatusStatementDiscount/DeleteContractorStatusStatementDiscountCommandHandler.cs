using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.DeleteContractorStatusStatementDiscount;

public class DeleteContractorStatusStatementDiscountCommandHandler : ICommandHandler<DeleteContractorStatusStatementDiscountCommand, ContractorStatusStatementDiscount>
{
    private readonly ILogger<DeleteContractorStatusStatementDiscountCommandHandler> _logger;
    private readonly IContractorStatusStatementDiscountRepository _repository;

    public DeleteContractorStatusStatementDiscountCommandHandler(
        ILogger<DeleteContractorStatusStatementDiscountCommandHandler> logger,
        IContractorStatusStatementDiscountRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorStatusStatementDiscount?>> Handle(DeleteContractorStatusStatementDiscountCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ContractorStatusStatementDiscount>(CSSErrors.DiscountNotFound);

            entity.SetIsDeleted();

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
