using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements.Enums;
using Gita.Backend.Shared.Domain.Base;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.ContractorStatusStatementStatusChanger;

public class ContractorStatusStatementStatusChangerCommandHandler : ICommandHandler<ContractorStatusStatementStatusChangerCommand, ContractorStatusStatement>
{
    private readonly ILogger<ContractorStatusStatementStatusChangerCommandHandler> _logger;
    private readonly IContractorStatusStatementRepository _repository;

    public ContractorStatusStatementStatusChangerCommandHandler(ILogger<ContractorStatusStatementStatusChangerCommandHandler> logger, IContractorStatusStatementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorStatusStatement?>> Handle(ContractorStatusStatementStatusChangerCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;

            var statusConditions = new Dictionary<CSSStatus, Func<bool>>
            {
                { CSSStatus.ProjectManagerResend,
                    () => CSSStatusRules.AllowForProjectManagerResend.Any(x => x.Equals(entity.Status)) },
                { CSSStatus.ProjectManagerPending,
                    () => CSSStatusRules.AllowForProjectManagerPending.Any(x => x.Equals(entity.Status)) },
                { CSSStatus.ProjectManagerReturned,
                    () => CSSStatusRules.AllowForProjectManagerReturned.Any(x => x.Equals(entity.Status)) },
                { CSSStatus.ProjectManagerRejected,
                    () => CSSStatusRules.AllowForProjectManagerRejected.Any(x => x.Equals(entity.Status)) },
                { CSSStatus.ProjectManagerConfirmed,
                    () => CSSStatusRules.AllowForProjectManagerConfirmed.Any(x => x.Equals(entity.Status)) },
                { CSSStatus.ManagementPending,
                    () => CSSStatusRules.AllowForManagementPending.Any(x => x.Equals(entity.Status)) },
                { CSSStatus.ManagementReturned,
                    () => CSSStatusRules.AllowForManagementReturned.Any(x => x.Equals(entity.Status)) },
                { CSSStatus.ReturnToProjectManager,
                    () => CSSStatusRules.AllowForReturnToProjectManager.Any(x => x.Equals(entity.Status)) },
                { CSSStatus.ManagementRejected,
                    () => CSSStatusRules.AllowForManagementRejected.Any(x => x.Equals(entity.Status)) },
                { CSSStatus.ManagementConfirmed,
                    () => CSSStatusRules.AllowForManagementConfirmed.Any(x => x.Equals(entity.Status)) },
                { CSSStatus.ManagementReturnForReview,
                    () => CSSStatusRules.AllowForManagementReturnForReview.Any(x => x.Equals(entity.Status)) },
                { CSSStatus.PrimaryManagerRejected,
                    () => CSSStatusRules.AllowForPrimaryManagerRejected.Any(x => x.Equals(entity.Status)) },
                { CSSStatus.PrimaryManagerConfirmed,
                    () => CSSStatusRules.AllowForPrimaryManagerConfirmed.Any(x => x.Equals(entity.Status)) },
                { CSSStatus.FinalManagerRejected,
                    () => CSSStatusRules.AllowForFinalManagerRejected.Any(x => x.Equals(entity.Status)) },
                { CSSStatus.FinalManagerConfirmed,
                    () => CSSStatusRules.AllowForFinalManagerConfirmed.Any(x => x.Equals(entity.Status)) },
                { CSSStatus.PaymentConfirmation,
                    () => CSSStatusRules.AllowForPaymentConfirmation.Any(x => x.Equals(entity.Status)) },
                { CSSStatus.Paid,
                    () => CSSStatusRules.AllowForPaid.Any(x => x.Equals(entity.Status)) },
                { CSSStatus.Invalidated,
                    () => CSSStatusRules.AllowForInvalidated.Any(x => x.Equals(entity.Status)) },
                { CSSStatus.RejectPaid,
                    () => CSSStatusRules.AllowForRejectPaid.Any(x => x.Equals(entity.Status)) }
            };

            var errorMessages = new Dictionary<CSSStatus, Error>
            {
                { CSSStatus.ProjectManagerResend, CSSErrors.InValidStatusForProjectManagerResend },
                { CSSStatus.ProjectManagerPending, CSSErrors.InValidStatusForProjectManagerPending },
                { CSSStatus.ProjectManagerReturned, CSSErrors.InValidStatusForProjectManagerReturned },
                { CSSStatus.ProjectManagerRejected, CSSErrors.InValidStatusForProjectManagerRejected },
                { CSSStatus.ProjectManagerConfirmed, CSSErrors.InValidStatusForProjectManagerConfirmed },
                { CSSStatus.ManagementPending, CSSErrors.InValidStatusForManagementPending },
                { CSSStatus.ManagementReturned, CSSErrors.InValidStatusForManagementReturned },
                { CSSStatus.ReturnToProjectManager, CSSErrors.InValidStatusForReturnToProjectManager },
                { CSSStatus.ManagementRejected, CSSErrors.InValidStatusForManagementRejected },
                { CSSStatus.ManagementConfirmed, CSSErrors.InValidStatusForManagementConfirmed },
                { CSSStatus.ManagementReturnForReview, CSSErrors.InValidStatusForManagementReturnForReview },
                { CSSStatus.PrimaryManagerRejected, CSSErrors.InValidStatusForPrimaryManagerRejected },
                { CSSStatus.PrimaryManagerConfirmed, CSSErrors.InValidStatusForPrimaryManagerConfirmed },
                { CSSStatus.FinalManagerRejected, CSSErrors.InValidStatusForFinalManagerRejected },
                { CSSStatus.FinalManagerConfirmed, CSSErrors.InValidStatusForFinalManagerConfirmed },
                { CSSStatus.PaymentConfirmation, CSSErrors.InValidStatusForPaymentConfirmation },
                { CSSStatus.Paid, CSSErrors.InValidStatusForPaid },
                { CSSStatus.RejectPaid, CSSErrors.InValidStatusForRejectPaid },
                { CSSStatus.Invalidated, CSSErrors.InValidStatusForInvalidated }
            };

            if (!statusConditions[request.Status]())
                return Result.Failure<ContractorStatusStatement>(errorMessages[request.Status]);

            if ((request.Status == CSSStatus.ManagementConfirmed) &&
                (request.MultiPayment.HasValue && request.MultiPayment == true) &&
                request.ConfirmedAmount == entity.PayableAmount)
                return Result.Failure<ContractorStatusStatement>(CSSErrors.CanNotMultiPayment);

            entity.ChangeStatus(request.Status, request.LastDescription);

            switch (request.Status)
            {
                case CSSStatus.ProjectManagerConfirmed:
                    entity.SetProjectManagerConfirmedAmount(request.ConfirmedAmount);
                    entity.SetProjectManagmentDescription(request.Description);
                    break;

                case CSSStatus.ManagementConfirmed:
                    entity.SetManagementConfirmedAmount(request.ConfirmedAmount);
                    entity.SetManagmentDescription(request.Description);
                    entity.SetMultiPayment(request.MultiPayment);
                    break;

                case CSSStatus.PrimaryManagerConfirmed:
                    entity.SetPrimaryManagerConfirmedAmount(request.ConfirmedAmount);
                    entity.SetPrimaryManagerDescription(request.Description);
                    entity.SetPrimaryManagerConfirmed();
                    break;

                case CSSStatus.PrimaryManagerRejected:
                    entity.SetPrimaryManagerDescription(request.Description);
                    entity.SetPrimaryManagerNotConfirmed();
                    entity.SetFinalManagerNotConfirmed();
                    break;

                case CSSStatus.FinalManagerConfirmed:
                    entity.SetFinalManagerConfirmedAmount(request.ConfirmedAmount);
                    entity.SetFinalManagerDescription(request.Description);
                    entity.SetFinalManagerConfirmed();
                    break;

                case CSSStatus.FinalManagerRejected:
                    entity.SetFinalManagerDescription(request.Description);
                    entity.SetPrimaryManagerNotConfirmed();
                    entity.SetFinalManagerNotConfirmed();
                    break;
            }

            if (request.Status == CSSStatus.ManagementConfirmed &&
                entity.MultiPayment && entity.ContractorStatusStatementPayments.Any(x => x.Status == CSSPaymentStatus.Paid))
                entity.ConfigPayment(request.ConfirmedAmount!.Value, request.Description, request.Urls, request.PaymentOrderId);

            else if (CSSStatusRules.AllowForConfigPayment.Any(x => x.Equals(request.Status)))
                entity.ConfigPayment(request.ConfirmedAmount!.Value, request.Description, request.Urls, request.PaymentOrderId);

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
