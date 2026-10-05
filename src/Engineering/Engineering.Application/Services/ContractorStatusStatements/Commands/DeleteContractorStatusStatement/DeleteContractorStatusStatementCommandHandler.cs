using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.DeleteContractorStatusStatement;

public class DeleteContractorStatusStatementCommandHandler : ICommandHandler<DeleteContractorStatusStatementCommand, ContractorStatusStatement>
{
    private readonly ILogger<DeleteContractorStatusStatementCommandHandler> _logger;
    private readonly IContractorStatusStatementRepository _repository;

    public DeleteContractorStatusStatementCommandHandler(
        ILogger<DeleteContractorStatusStatementCommandHandler> logger,
        IContractorStatusStatementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorStatusStatement?>> Handle(DeleteContractorStatusStatementCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;

            entity.SetIsDeleted();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorStatusStatement>(SharedErrors.UnknownError);
        }
    }
}
