using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements.Enums;
using Gita.Backend.Shared.Domain.Base;

namespace Engineering.Application.Services.EmployerStatusStatements.Commands.ChangeEmployerStatusStatementStatus;

public class ChangeEmployerStatusStatementStatusCommandHandler : ICommandHandler<ChangeEmployerStatusStatementStatusCommand, EmployerStatusStatement>
{
    private readonly ILogger<ChangeEmployerStatusStatementStatusCommand> _logger;
    private readonly IEmployerStatusStatementRepository _repository;

    public ChangeEmployerStatusStatementStatusCommandHandler(ILogger<ChangeEmployerStatusStatementStatusCommand> logger, IEmployerStatusStatementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<EmployerStatusStatement?>> Handle(ChangeEmployerStatusStatementStatusCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.ChangeEmployerStatusStatementStatus(request.Id, ct);
            if (entity is null)
                return Result.Failure<EmployerStatusStatement>(EmployerStatusStatementErrors.DataNotFound);

            var statusConditions = new Dictionary<EmployerStatusStatementStatus, Func<bool>>
            {
                { EmployerStatusStatementStatus.Pending,
                    () => EmployerStatusStatementStatusRules.AllowForPending.Any(x => x.Equals(entity.Status)) },
                { EmployerStatusStatementStatus.Invalidated,
                    () => EmployerStatusStatementStatusRules.AllowForInvalidated.Any(x => x.Equals(entity.Status)) },
                { EmployerStatusStatementStatus.ReturnForReview,
                    () => EmployerStatusStatementStatusRules.AllowForReturnForReview.Any(x => x.Equals(entity.Status)) },
                { EmployerStatusStatementStatus.SendToSupervisor,
                    () => EmployerStatusStatementStatusRules.AllowForSendToSupervisor.Any(x => x.Equals(entity.Status)) },
                { EmployerStatusStatementStatus.SupervisorPending,
                    () => EmployerStatusStatementStatusRules.AllowForSupervisorPending.Any(x => x.Equals(entity.Status)) },
                { EmployerStatusStatementStatus.SendToConsultant,
                    () => EmployerStatusStatementStatusRules.AllowForSendToConsultant.Any(x => x.Equals(entity.Status)) },
                { EmployerStatusStatementStatus.ConsultantPending,
                    () => EmployerStatusStatementStatusRules.AllowForConsultantPending.Any(x => x.Equals(entity.Status)) },
                { EmployerStatusStatementStatus.SendToEmployerRepresentative,
                    () => EmployerStatusStatementStatusRules.AllowForSendToEmployerRepresentative.Any(x => x.Equals(entity.Status)) },
                { EmployerStatusStatementStatus.EmployerRepresentativePending,
                    () => EmployerStatusStatementStatusRules.AllowForEmployerRepresentativePending.Any(x => x.Equals(entity.Status)) },
            };

            var errorMessages = new Dictionary<EmployerStatusStatementStatus, Error>
            {
                { EmployerStatusStatementStatus.Pending, EmployerStatusStatementErrors.ChangeToSendToModerator },
                { EmployerStatusStatementStatus.Invalidated, EmployerStatusStatementErrors.ChangeToSendToModerator },
                { EmployerStatusStatementStatus.ReturnForReview, EmployerStatusStatementErrors.ChangeToSendToModerator },
                { EmployerStatusStatementStatus.SendToSupervisor, EmployerStatusStatementErrors.ChangeToSendToModerator },
                { EmployerStatusStatementStatus.SupervisorPending, EmployerStatusStatementErrors.ChangeToSendToModerator },
                { EmployerStatusStatementStatus.SendToConsultant, EmployerStatusStatementErrors.ChangeToSendToModerator },
                { EmployerStatusStatementStatus.ConsultantPending, EmployerStatusStatementErrors.ChangeToSendToModerator },
                { EmployerStatusStatementStatus.SendToEmployerRepresentative, EmployerStatusStatementErrors.ChangeToSendToModerator },
                { EmployerStatusStatementStatus.EmployerRepresentativePending, EmployerStatusStatementErrors.ChangeToSendToModerator },
            };

            if (!statusConditions[request.Status]())
                return Result.Failure<EmployerStatusStatement>(errorMessages[request.Status]);

            if (EmployerStatusStatementStatusRules.IsPending.Any(x => x.Equals(request.Status)))
                entity.SetStatus(request.Status, request.Description);

            if (request.Status == EmployerStatusStatementStatus.SendToSupervisor)
            {
                if (request.ESSProjectOperations is not null && request.ESSProjectOperations.Any() && request.ESSProjectOperations.Count > 0)
                    foreach (var operation in request.ESSProjectOperations)
                        if (entity.EmployerStatusStatementProjectOperations.Any(x => x.Id == operation.Id))
                            entity.EmployerStatusStatementProjectOperations.FirstOrDefault(x => x.Id == operation.Id)!.SetContractorWorkVolumeToZero(operation.Description);

                if (request.ESSProjectOperationDetailDailies is not null && request.ESSProjectOperationDetailDailies.Any() && request.ESSProjectOperationDetailDailies.Count > 0)
                {
                    var dailies = entity.EmployerStatusStatementProjectOperations.SelectMany(x => x.EmployerStatusStatementProjectOperationDetails.SelectMany(d => d.EmployerStatusStatementProjectOperationDetailDailies)).ToList();
                    foreach (var detailDaily in request.ESSProjectOperationDetailDailies)
                        if (dailies.Any(daily => daily.Id == detailDaily.Id))
                            dailies.FirstOrDefault(daily => daily.Id == detailDaily.Id)!
                                .SetContractorWorkVolume(detailDaily.Length, detailDaily.Width, detailDaily.Height, detailDaily.Weight, detailDaily.Number, detailDaily.Description);
                }
            }

            if (request.Status == EmployerStatusStatementStatus.SendToConsultant)
            {
                if (request.ESSProjectOperations is not null && request.ESSProjectOperations.Any() && request.ESSProjectOperations.Count > 0)
                    foreach (var operation in request.ESSProjectOperations)
                        if (entity.EmployerStatusStatementProjectOperations.Any(x => x.Id == operation.Id))
                            entity.EmployerStatusStatementProjectOperations.FirstOrDefault(x => x.Id == operation.Id)!.SetSupervisorWorkVolumeToZero(operation.Description);

                if (request.ESSProjectOperationDetailDailies is not null && request.ESSProjectOperationDetailDailies.Any() && request.ESSProjectOperationDetailDailies.Count > 0)
                {
                    var dailies = entity.EmployerStatusStatementProjectOperations.SelectMany(x => x.EmployerStatusStatementProjectOperationDetails.SelectMany(d => d.EmployerStatusStatementProjectOperationDetailDailies)).ToList();
                    foreach (var detailDaily in request.ESSProjectOperationDetailDailies)
                        if (dailies.Any(daily => daily.Id == detailDaily.Id))
                            dailies.FirstOrDefault(daily => daily.Id == detailDaily.Id)!
                                .SetSupervisorVolume(detailDaily.Length, detailDaily.Width, detailDaily.Height, detailDaily.Weight, detailDaily.Number, detailDaily.Description);
                }
            }

            if (request.Status == EmployerStatusStatementStatus.SendToEmployerRepresentative)
            {
                if (request.ESSProjectOperations is not null && request.ESSProjectOperations.Any() && request.ESSProjectOperations.Count > 0)
                    foreach (var operation in request.ESSProjectOperations)
                        if (entity.EmployerStatusStatementProjectOperations.Any(x => x.Id == operation.Id))
                            entity.EmployerStatusStatementProjectOperations.FirstOrDefault(x => x.Id == operation.Id)!.SetConsultantWorkVolumeToZero(operation.Description);

                if (request.ESSProjectOperationDetailDailies is not null && request.ESSProjectOperationDetailDailies.Any() && request.ESSProjectOperationDetailDailies.Count > 0)
                {
                    var dailies = entity.EmployerStatusStatementProjectOperations.SelectMany(x => x.EmployerStatusStatementProjectOperationDetails.SelectMany(d => d.EmployerStatusStatementProjectOperationDetailDailies)).ToList();
                    foreach (var detailDaily in request.ESSProjectOperationDetailDailies)
                        if (dailies.Any(daily => daily.Id == detailDaily.Id))
                            dailies.FirstOrDefault(daily => daily.Id == detailDaily.Id)!
                                .SetConsultantVolume(detailDaily.Length, detailDaily.Width, detailDaily.Height, detailDaily.Weight, detailDaily.Number, detailDaily.Description);
                }
            }

            entity.SetStatus(request.Status, request.Description);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerStatusStatement>(SharedErrors.UnknownError);
        }
    }
}
