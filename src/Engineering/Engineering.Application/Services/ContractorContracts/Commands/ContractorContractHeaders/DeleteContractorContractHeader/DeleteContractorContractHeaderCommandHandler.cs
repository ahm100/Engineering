using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.ContractorStatusStatements.Enums;

namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContractHeaders.DeleteContractorContractHeader;

public class DeleteContractorContractHeaderCommandHandler : ICommandHandler<DeleteContractorContractHeaderCommand, ContractorContractHeader>
{
    private ILogger<DeleteContractorContractHeaderCommandHandler> _logger;
    private IContractorContractHeaderRepository _repository;

    public DeleteContractorContractHeaderCommandHandler(
        ILogger<DeleteContractorContractHeaderCommandHandler> logger,
        IContractorContractHeaderRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorContractHeader?>> Handle(DeleteContractorContractHeaderCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetContractorContractHeaderForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<ContractorContractHeader>(ContractorContractErrors.InValidProjectOperationServiceId);

            if (entity.ContractorContracts.Any(x => x.ContractorStatusStatementDetails.Any(c =>
                !CSSStatusRules.AllowForDelete.Any(x => !x.Equals(c.ContractorStatusStatement.Status)))))
                return Result.Failure<ContractorContractHeader>(ContractorContractErrors.InValidProjectOperationServiceId);

            if (!ContractorContractStatusRules.AllowForDelete.Any(x => x.Equals(entity.Status)))
                return Result.Failure<ContractorContractHeader>(ContractorContractErrors.InValidStatusForDelete);
            else
            {
                entity.SoftDelete();
                foreach (var contractorContract in entity.ContractorContracts)
                {
                    contractorContract.SoftDelete();
                    foreach (var detail in contractorContract.Details)
                    {
                        detail.SoftDelete();
                        foreach (var service in detail.ContractorContractDetailServices)
                            service.SetIsDeleted();
                    }
                }
            }

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorContractHeader>(SharedErrors.UnknownError);
        }
    }
}
