using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Gita.Backend.Shared.Domain.Base;

namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContractHeaders.ContractorContractHeaderStatusChanger;

public class ContractorContractHeaderStatusChangerCommandHandler : ICommandHandler<ContractorContractHeaderStatusChangerCommand, ContractorContractHeader>
{
    private readonly ILogger<ContractorContractHeaderStatusChangerCommandHandler> _logger;
    private readonly IContractorContractHeaderRepository _repository;

    public ContractorContractHeaderStatusChangerCommandHandler(
        ILogger<ContractorContractHeaderStatusChangerCommandHandler> logger,
        IContractorContractHeaderRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorContractHeader?>> Handle(ContractorContractHeaderStatusChangerCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;

            var statusConditions = new Dictionary<ContractorContractStatus, Func<bool>>
            {
                { ContractorContractStatus.ProjectManagerResend,
                    () => ContractorContractStatusRules.AllowForProjectManagerResend.Any(x => x.Equals(entity.Status)) },
                { ContractorContractStatus.ProjectManagerPending,
                    () => ContractorContractStatusRules.AllowForProjectManagerPending.Any(x => x.Equals(entity.Status)) },
                { ContractorContractStatus.ProjectManagerReturned,
                    () => ContractorContractStatusRules.AllowForProjectManagerReturned.Any(x => x.Equals(entity.Status)) },
                { ContractorContractStatus.ProjectManagerRejected,
                    () => ContractorContractStatusRules.AllowForProjectManagerRejected.Any(x => x.Equals(entity.Status)) },
                { ContractorContractStatus.ProjectManagerConfirmed,
                    () => ContractorContractStatusRules.AllowForProjectManagerConfirmed.Any(x => x.Equals(entity.Status)) },
                { ContractorContractStatus.ManagementPending,
                    () => ContractorContractStatusRules.AllowForManagementPending.Any(x => x.Equals(entity.Status)) },
                { ContractorContractStatus.ManagementReturned,
                    () => ContractorContractStatusRules.AllowForManagementReturned.Any(x => x.Equals(entity.Status)) },
                { ContractorContractStatus.ManagementRejected,
                    () => ContractorContractStatusRules.AllowForManagementRejected.Any(x => x.Equals(entity.Status)) },
                { ContractorContractStatus.ManagementConfirmed,
                    () => ContractorContractStatusRules.AllowForManagementConfirmed.Any(x => x.Equals(entity.Status)) },
                { ContractorContractStatus.ManagementReturnForReview,
                    () => ContractorContractStatusRules.AllowForManagementReturnForReview.Any(x => x.Equals(entity.Status)) },
                { ContractorContractStatus.Archived,
                    () => ContractorContractStatusRules.AllowForArchived.Any(x => x.Equals(entity.Status)) }
            };

            var errorMessages = new Dictionary<ContractorContractStatus, Error>
            {
                { ContractorContractStatus.ProjectManagerResend, ContractorContractErrors.InValidStatusForProjectManagerResend },
                { ContractorContractStatus.ProjectManagerPending, ContractorContractErrors.InValidStatusForProjectManagerPending },
                { ContractorContractStatus.ProjectManagerReturned, ContractorContractErrors.InValidStatusForProjectManagerReturned },
                { ContractorContractStatus.ProjectManagerRejected, ContractorContractErrors.InValidStatusForProjectManagerRejected },
                { ContractorContractStatus.ProjectManagerConfirmed, ContractorContractErrors.InValidStatusForProjectManagerConfirmed },
                { ContractorContractStatus.ManagementPending, ContractorContractErrors.InValidStatusForManagementPending },
                { ContractorContractStatus.ManagementReturned, ContractorContractErrors.InValidStatusForManagementReturned },
                { ContractorContractStatus.ManagementRejected, ContractorContractErrors.InValidStatusForManagementRejected },
                { ContractorContractStatus.ManagementConfirmed, ContractorContractErrors.InValidStatusForManagementConfirmed },
                { ContractorContractStatus.ManagementReturnForReview, ContractorContractErrors.InValidStatusForManagementReturnForReview },
                { ContractorContractStatus.Archived, ContractorContractErrors.InValidStatusForArchived }
            };

            if (!statusConditions[request.Status]())
                return Result.Failure<ContractorContractHeader>(errorMessages[request.Status]);

            if (ContractorContractStatusRules.AllowForWriteDescription.Contains(request.Status))
            {
                entity.SetDescription(request.Description);
            }
            else if (request.Status == ContractorContractStatus.ProjectManagerConfirmed)
                entity.AddManagerHistory(request.Description);
            entity.SetStatus(request.Status);

            if ((request.Status == ContractorContractStatus.ManagementReturned || request.Status == ContractorContractStatus.ProjectManagerReturned)
               && request.Urls.HasAny())
                entity.AddDocuments(request.Urls, false);
            else
                entity.AddDocuments(request.Urls, true);

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
