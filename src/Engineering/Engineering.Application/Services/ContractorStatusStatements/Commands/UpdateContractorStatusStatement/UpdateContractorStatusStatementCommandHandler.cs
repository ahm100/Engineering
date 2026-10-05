using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements.Enums;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.UpdateContractorStatusStatement;

public class UpdateContractorStatusStatementCommandHandler : ICommandHandler<UpdateContractorStatusStatementCommand, ContractorStatusStatement>
{
    private readonly ILogger<UpdateContractorStatusStatementCommandHandler> _logger;
    private readonly IContractorStatusStatementRepository _repository;

    public UpdateContractorStatusStatementCommandHandler(
        ILogger<UpdateContractorStatusStatementCommandHandler> logger,
        IContractorStatusStatementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<Result<ContractorStatusStatement?>> Handle(UpdateContractorStatusStatementCommand request, CT ct)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        try
        {
            var entity = request.Entity;

            entity.SetCreatorConfirmedAmount();
            entity.SetDescription(request.Description);

            if (request.Urls is not null && request.Urls.Any())
                entity.AddDocuments(request.Urls, false);

            if (entity.Status == CSSStatus.New)
                entity.ChangeStatus(CSSStatus.New, request.Description);

            if (entity.Status != CSSStatus.New)
                if (CSSStatusRules.AllowForUpdate.Any(x => x.Equals(entity.Status)))
                    entity.ChangeStatus(CSSStatus.ProjectManagerResend, request.Description);

            entity.ConfigPayment(request.UpdatorConfirmedAmount ?? 0, request.Description, null, null);

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
