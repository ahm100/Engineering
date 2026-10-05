using Engineering.Application.Services.ConsumptionStandards.Queries.Products.GetsProductByOperationInfoId;
using Engineering.Application.Services.CostOvers.Queries.GetsCostOverByIds;
using Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;
using Engineering.Application.Services.EmployerContracts.Contracts.CreateEContract;
using Engineering.Application.Services.EmployerContracts.Contracts.CreateEContractHeader;
using Engineering.Application.Services.EmployerContracts.Contracts.EContractFinancialCover;
using Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContract;
using Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContractFinancial;
using Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContractHeader;
using Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContractVolume;
using Engineering.Application.Services.OperationInfos.Queries.GetOperationInfos;
using Engineering.Application.Services.OperationInfoServices.Queries.GetsOperationInfoServiceByIds;
using Engineering.Application.Services.ProjectOperations.Commands.CreateEContractPOperation;
using Engineering.Application.Services.ProjectOperations.Queries.FindMergedProjectOperation;
using Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperations;
using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.EmployerContracts;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;
using Gita.Backend.Shared.Domain.Base;

namespace Engineering.Application.Services.EmployerContracts;

public partial class EContractHeaderLogic
{
    private async Task<Result<EmployerContractHead?>> CreateHeaderHandler(
         CostCenter costCenter, CreateEContractHeaderRequest request, long companyId, CT ct)
    {
        try
        {
            var entity = await _headRepo.Create(new EmployerContractHead(
                costCenter,
                request.EmployerId,
                request.CurrencyId,
                request.Code,
                request.VolumeTolerance,
                request.Type,
                companyId), ct);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerContractHead>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<EmployerContractHead?>> UpdateHeaderHandler(
         EmployerContractHead entity, UpdateEContractHeaderRequest request, long companyId, CT ct)
    {
        try
        {
            if (!string.IsNullOrEmpty(request.Code) && request.Code != entity.Code)
            {
                var verify = await VerifyCodeHandler(entity.Id, request.Code, companyId, ct);
                if (verify.Value)
                    return Result.Failure<EmployerContractHead>(EContractErrors.IsDuplicate);
            }

            entity.Update(
                request.Code,
                request.VolumeTolerance,
                request.Type);

            await _headRepo.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerContractHead>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<EmployerContract?>> UpdateContractHandler(
         EmployerContract entity, UpdateEContractRequest request, CT ct)
    {
        try
        {
            entity.Update(
                request.StartDate,
                request.EndDate,
                request.Code,
                request.Description);

            await _contractRepo.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerContract>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<EmployerContractHead?>> DeleteEContractHeaderHandler(
        long id, CT ct)
    {
        try
        {
            var entity = await _headRepo.DeleteEContractHeader(id, ct);
            if (entity is null)
                return Result.Failure<EmployerContractHead>(EContractErrors.NotFound);

            entity.SoftDelete();
            await _headRepo.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerContractHead>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<EmployerContractHead?>> GetEContractHeaderHandler(
        long id, CT ct)
    {
        try
        {
            var entity = await _headRepo.GetEContractHeader(id, ct);
            if (entity is null)
                return Result.Failure<EmployerContractHead>(EContractErrors.NotFound);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerContractHead>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<EmployerContractHead?>> GetEContractHeaderFullHandler(
        long id, CT ct)
    {
        try
        {
            var entity = await _headRepo.GetEContractHeaderFull(id, ct);
            if (entity is null)
                return Result.Failure<EmployerContractHead>(EContractErrors.NotFound);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerContractHead>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<EmployerContract?>> GetEContractHandler(
        long id, CT ct)
    {
        try
        {
            var entity = await _contractRepo.GetEConteract(id, ct);
            if (entity is null)
                return Result.Failure<EmployerContract>(EContractErrors.NotFound);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerContract>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<EmployerContract?>> DeleteEContractHandler(
        EmployerContract entity, CT ct)
    {
        try
        {
            if (entity.IsDeleted == true)
                return Result.Failure<EmployerContract>(EContractErrors.IsDeleted);
            if (entity.EmployerOperations.Select(x => x.ProjectOperation).Any(x => x.ProjectOperationDetails.Count > 0))
                return Result.Failure<EmployerContract>(EContractErrors.CanNottDeleteBecauseOfProjectOperationDetails);
            if (entity.EmployerConsiderations.Any(x => x.EmployerConsiderationDeps.Count > 0))
                return Result.Failure<EmployerContract>(EContractErrors.CanNottDeleteBecauseOfConsiderationDependencies);
            if (!string.IsNullOrEmpty(entity.Code))
                return Result.Failure<EmployerContract>(EContractErrors.EmployerContractHaveCode);

            entity.SoftDelete();
            await _contractRepo.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerContract>(SharedErrors.UnknownError);
        }
    }
    private async Task<Result<EmployerContract?>> CreateContractHandler(
         EmployerContractHead contractHead, Project project, CreateEContractModel request, CT ct)
    {
        try
        {
            var entity = await _contractRepo.Create(new EmployerContract(
                contractHead,
                project,
                request.StartDate,
                request.EndDate,
                request.Description,
                request.Code,
                true), ct);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerContract>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<List<EmployerDoc>?>> CreateDocHandler(
        EmployerContract contract, List<CreateEmployerDocModel> request, CT ct)
    {
        try
        {
            var entities = new List<EmployerDoc>();
            foreach (var item in request)
            {
                var entity = await _contractDocRepo.Create(new EmployerDoc(
                    contract,
                    item.Type,
                    item.Version,
                    item.Urls,
                    item.Description,
                    item.RegistrationDate,
                    item.IsActive), ct);

                entities.Add(entity);
            }

            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<EmployerDoc>>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<List<EmployerDoc>?>> UpdateDocHandler(
        EmployerContract contract, List<UpdateEmployerDocModel> requests, CT ct)
    {
        try
        {
            var ids = requests.Listed(x => x.Id);
            var entities = contract.EmployerDocs.Where(x => ids.Contains(x.Id)).ToList();
            foreach (var entity in entities)
            {
                var request = requests.FirstOrDefault(x => x.Id == entity.Id);
                if (request != null)
                {
                    entity.Update(
                        request.Type,
                        request.Version,
                        request.Urls,
                        request.Description,
                        request.RegistrationDate,
                        request.IsActive);

                    await _contractDocRepo.Update(entity);
                }
            }

            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<EmployerDoc>>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<List<EmployerDoc>?>> DeleteDocHandler(
        EmployerContract contract, List<long> requests, CT ct)
    {
        try
        {
            var entities = contract.EmployerDocs.Where(x => requests.Contains(x.Id)).ToList();
            foreach (var entity in entities)
                if (requests.Any(x => x == entity.Id))
                {
                    entity.SoftDelete();
                    await _contractDocRepo.Update(entity);
                }

            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<EmployerDoc>>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<List<EmployerConsideration>?>> CreateConsiderationHandler(
        EmployerContract contract, List<CreateEmployerConsiderationModel> request, CT ct)
    {
        try
        {
            var entities = new List<EmployerConsideration>();
            foreach (var item in request)
            {
                var entity = await _contractConsiderationRepo.Create(new EmployerConsideration(
                    contract,
                    item.Type,
                    item.Description,
                    item.IsActive,
                    item.FlagId), ct);

                entities.Add(entity);
            }

            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<EmployerConsideration>>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<List<EmployerConsideration>?>> UpdateConsiderationHandler(
        EmployerContract contract, List<UpdateEmployerConsiderationModel> requests, CT ct)
    {
        try
        {
            var ids = requests.Listed(x => x.Id);
            var entities = contract.EmployerConsiderations.Where(x => ids.Contains(x.Id)).ToList();
            foreach (var entity in entities)
            {
                var request = requests.FirstOrDefault(x => x.Id == entity.Id);
                if (request != null)
                {
                    entity.Update(
                        request.Type,
                        request.Description,
                        request.IsActive,
                        request.FlagId);

                    await _contractConsiderationRepo.Update(entity);
                }
            }

            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<EmployerConsideration>>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<List<EmployerConsideration>?>> DeleteConsiderationHandler(
        EmployerContract contract, List<long> requests, CT ct)
    {
        try
        {
            var entities = contract.EmployerConsiderations
                .Where(x => requests.Contains(x.Id)).ToList();

            foreach (var entity in entities)
                if (requests.Any(x => x == entity.Id))
                {
                    entity.SoftDelete();
                    await _contractConsiderationRepo.Update(entity);
                }

            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<EmployerConsideration>>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<List<EmployerCostOver>?>> CreateCostOverHandler(
        EmployerContract contract, List<CreateEmployerCostOverFinancialModel> request, CT ct)
    {
        try
        {
            var costIds = request!.Listed(x => x.CostOverId);
            var costOvers = await _mediator.Send(new GetsCostOverByIdsQuery(costIds), ct);
            if (costOvers.IsBad() || costOvers.Value?.Count != costIds.Count)
                return Result.Failure<List<EmployerCostOver>>(CostOverErrors.CostOverWithCodeNotFound);

            var entities = new List<EmployerCostOver>();
            foreach (var item in request)
            {
                var entity = await _contractCostRepo.Create(new EmployerCostOver(
                    contract,
                    costOvers.Value!.First(x => x.Id == item.CostOverId),
                    item.Percent), ct);

                entities.Add(entity);
            }

            var haveImpacted = request.Where(x =>
                x.CostOverImpacts is not null && x.CostOverImpacts!.Count > 0).ToList();
            if (haveImpacted.HasAny())
                foreach (var item in haveImpacted)
                {
                    var ids = item.CostOverImpacts!.Listed(x => x.CostOverId);
                    var impactedCostes = entities.Where(x => ids.Contains(x.CostOverId)).ToList();
                    var entity = entities.FirstOrDefault(x => x.CostOverId == item.CostOverId)!;
                    foreach (var impact in impactedCostes)
                        entity.AddCostOverImpact(impact, impact.Percent);
                }

            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<EmployerCostOver>>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<List<EmployerCostOver>?>> UpdateCostOverHandler(
        EmployerContract contract, List<UpdateEmployerCostOverFinancialModel> requests, CT ct)
    {
        try
        {
            var costIds = requests!.Listed(x => x.CostOverId);
            var costOvers = await _mediator.Send(new GetsCostOverByIdsQuery(costIds), ct);
            if (costOvers.IsBad())
                return Result.Failure<List<EmployerCostOver>>(costOvers.Error!);

            var ids = requests.Listed(x => x.Id);
            var entities = contract.EmployerCostOvers.Where(x => ids.Contains(x.Id)).ToList();
            foreach (var entity in entities)
            {
                var request = requests.FirstOrDefault(x => x.Id == entity.Id);
                if (request != null)
                {
                    entity.Update(
                        costOvers.Value!.First(x => x.Id == request.CostOverId),
                        request.Percent);

                    if (request.CostOverImpacts.HasAny())
                    {
                        var impacts = request.CostOverImpacts!.Listed(x => x.CostOverId);
                        var impactedCostes = contract.EmployerCostOvers
                            .Where(x => impacts.Contains(x.CostOverId)).ToList();

                        if (impactedCostes.HasAny())
                        {
                            entity.DeleteCostOverImpacts();
                            foreach (var impact in impactedCostes)
                                entity.AddCostOverImpact(impact, impact.Percent);
                        }
                    }

                    await _contractCostRepo.Update(entity);
                }
            }

            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<EmployerCostOver>>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<List<EmployerCostOver>?>> DeleteCostOverHandler(
        EmployerContract contract, List<long> requests, CT ct)
    {
        try
        {
            var entities = contract.EmployerCostOvers.Where(x => requests.Contains(x.Id)).ToList();
            foreach (var entity in entities)
                if (requests.Any(x => x == entity.Id))
                {
                    entity.SoftDelete();
                    entity.DeleteCostOverImpacts();
                    await _contractCostRepo.Update(entity);
                }

            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<EmployerCostOver>>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<List<EmployerOperation>?>> DeleteOperationsHandler(
        EmployerContract contract,
        List<long> requests,
        CT ct)
    {
        try
        {
            var entities = contract.EmployerOperations.Where(x => requests.Contains(x.Id)).ToList();
            foreach (var entity in entities)
            {
                var pO = entity.ProjectOperation;
                if (!pO.ProjectOperationDetails.Any())
                    pO.SoftDelete();

                else if (pO.ProjectOperationDetails.Any())
                {
                    var alikePOperation = await _mediator.Send(new FindMergedProjectOperationQuery(
                        pO.Id, pO.Project.Id, pO.UnitOfMeasurementId, pO.OperationInfo.Id), ct);
                    if (alikePOperation.Value is null)
                        entity.SoftDelete();
                    else
                    {
                        var alikeEntity = alikePOperation.Value;
                        foreach (var pOperationDetail in pO.ProjectOperationDetails)
                            pOperationDetail.SetProjectOperation(alikeEntity);
                        pO.SoftDelete();
                    }
                }
                await _contractOperationRepo.Update(entity);
            }
            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<EmployerOperation>>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<List<EmployerOperation>?>> CreateOperationsHandler(
        EmployerContract contract, List<EmployerConsideration>? flagedConsiderations,
        List<CreateEmployerOperationModel> request, CT ct)
    {
        try
        {
            var pOperationIds = request!.Listed(x => x.ProjectOperationId);
            var pOperations = await _mediator.Send(new GetProjectOperationsQuery(pOperationIds), ct);
            if (pOperations.IsBad())
                return Result.Failure<List<EmployerOperation>>(pOperations.Error!);

            var entities = new List<EmployerOperation>();
            foreach (var item in request)
            {
                var pOperation = pOperations.Value!.FirstOrDefault(x => x.Id == item.ProjectOperationId);
                if (pOperation is null)
                    return Result.Failure<List<EmployerOperation>>(ProjectOperationErrors.NotFound);

                var considerations = new List<EmployerConsideration>();
                if (flagedConsiderations.HasAny())
                    if (item.FlagIds.HasAny())
                        considerations = flagedConsiderations!
                            .Where(x => item.FlagIds!.Contains(x.FlagId!)).ToList();

                var entity = await _contractOperationRepo.Create(new EmployerOperation(
                    contract,
                    pOperation!,
                    item.Description,
                    considerations,
                    item.ProjectOperationDetailIds), ct);

                entities.Add(entity);
            }

            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<EmployerOperation>>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<List<ProjectOperation>?>> CreateNewOperationsHandler(
        EmployerContract contract, List<EmployerConsideration>? flagedConsiderations,
        List<CreateProjectOperationModel> request, CT ct)
    {
        try
        {
            var operationIds = request!.Listed(x => x.OperationInfoId);
            var operations = await _mediator.Send(new GetOperationInfosQuery(operationIds), ct);
            if (operations.IsBad())
                return Result.Failure<List<ProjectOperation>>(operations.Error!);

            var assignedOperationInfoIds = contract.EmployerOperations.Listed(x => x.ProjectOperation.OperationInfo?.Id);
            if (operationIds.Any(x => assignedOperationInfoIds.Contains(x)))
                return Result.Failure<List<ProjectOperation>>(EContractErrors.OperationInfoIdIsDuplicate!);

            var employerOperations = contract.EmployerOperations.Select(x => x).ToList();

            var entities = new List<ProjectOperation>();

            var createData = await _mediator.Send(new CreateEContractPOperationCommand(
                contract, operations.Value!, flagedConsiderations, request!), ct);
            if (createData.IsBad())
                return createData.Failure<List<ProjectOperation>>()!;

            return createData.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ProjectOperation>>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<List<ProjectOperation>?>> UpdateOperationsHandler(
        EmployerContract contract,
        List<UpdateProjectOperationModel> requests, CT ct)
    {
        try
        {
            var updateOperations = await _contractRepo.GetEConteract(contract.Id, ct);
            if (updateOperations is null)
                return Result.Failure<List<ProjectOperation>>(EContractErrors.OperationInfoIdIsDuplicate!);

            foreach (var request in requests)
            {
                if (!updateOperations!.EmployerOperations.Select(x => x.Id).Contains(request.Id))
                    return Result.Failure<List<ProjectOperation>>(EContractErrors.OperationInfoIdIsDuplicate!);
                else
                {
                    var entity = updateOperations.EmployerOperations.FirstOrDefault(x => x.Id == request.Id)!.ProjectOperation;
                    entity.Update(request.Workload, request.Status, request.Priority);
                    await _projectOperationRepo.Update(entity);
                }
            }
            return updateOperations!.EmployerOperations.Select(x => x.ProjectOperation).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ProjectOperation>>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<EmployerContract?>> StatusChanger(
        SetECStatusRequest request, CT ct)
    {
        try
        {
            var entity = await _contractRepo.GetEConteract(request.Id, ct);
            if (entity is null)
                return Result.Failure<EmployerContract>(EContractErrors.NotFound);

            var statusConditions = new Dictionary<EContractStatus, Func<bool>>
            {
                { EContractStatus.ProjectManagerResend,
                        () => EContractStatusRules.AllowForProjectManagerResend.Any(x => x.Equals(entity.Status)) },
                { EContractStatus.ProjectManagerPending,
                        () => EContractStatusRules.AllowForProjectManagerPending.Any(x => x.Equals(entity.Status)) },
                { EContractStatus.ProjectManagerReturned,
                        () => EContractStatusRules.AllowForProjectManagerReturned.Any(x => x.Equals(entity.Status)) },
                { EContractStatus.ProjectManagerConfirmed,
                        () => EContractStatusRules.AllowForProjectManagerConfirmed.Any(x => x.Equals(entity.Status)) },

                { EContractStatus.EmployerConfirmed, () =>
                    EContractStatusRules.AllowForEmployer.Any(x => x.Equals(entity.Status)) },
                { EContractStatus.EmployerReturned, () =>
                    EContractStatusRules.AllowForEmployer.Any(x => x.Equals(entity.Status)) },
                { EContractStatus.EmployerPending, () =>
                    EContractStatusRules.AllowForEmployer.Any(x => x.Equals(entity.Status)) },

                { EContractStatus.ContractExpertPending, () =>
                    EContractStatusRules.AllowForContractExpertPending.Any(x => x.Equals(entity.Status)) },
                { EContractStatus.ContractExpertReturned,
                        () => EContractStatusRules.AllowForContractExpertReturned.Any(x => x.Equals(entity.Status)) },
                { EContractStatus.ContractExpertConfirmed,
                        () => EContractStatusRules.AllowForContractExpertConfirmed.Any(x => x.Equals(entity.Status)) },
                { EContractStatus.ContractExpertReturnToUser,
                        () => EContractStatusRules.AllowForContractExpertReturnToUSer.Any(x => x.Equals(entity.Status)) },

                { EContractStatus.ContractSupervisorPending,
                    () => EContractStatusRules.AllowForContractSupervisorPending.Any(x => x.Equals(entity.Status)) },
                { EContractStatus.ContractSupervisorReturnedToUser,
                        () => EContractStatusRules.AllowForContractSupervisorReturned.Any(x => x.Equals(entity.Status)) },
                { EContractStatus.ReturnToContractExpert,
                        () => EContractStatusRules.AllowForReturnToProjectManager.Any(x => x.Equals(entity.Status)) },
                { EContractStatus.ContractSupervisorConfirmed,
                        () => EContractStatusRules.AllowForContractSupervisorConfirmed.Any(x => x.Equals(entity.Status)) },

                { EContractStatus.PrimaryManagerConfirmed,
                        () => EContractStatusRules.AllowForPrimaryManagerConfirmed.Any(x => x.Equals(entity.Status)) },
                { EContractStatus.PrimaryManagerReturned,
                        () => EContractStatusRules.AllowForPrimaryManagerReturned.Any(x => x.Equals(entity.Status)) },

                { EContractStatus.FinalManagerConfirmed,
                        () => EContractStatusRules.AllowForFinalManagerConfirmed.Any(x => x.Equals(entity.Status)) },
                { EContractStatus.FinalManagerReturned,
                        () => EContractStatusRules.AllowForFinalManagerReturned.Any(x => x.Equals(entity.Status)) },
            };

            var errorMessages = new Dictionary<EContractStatus, Error>
            {
                { EContractStatus.ProjectManagerResend, EContractStatusErrors.InValidStatusForProjectManagerResend },
                { EContractStatus.ProjectManagerPending, EContractStatusErrors.InValidStatusForProjectManagerPending },
                { EContractStatus.ProjectManagerReturned, EContractStatusErrors.InValidStatusForProjectManagerReturned },
                { EContractStatus.ProjectManagerConfirmed, EContractStatusErrors.InValidStatusForProjectManagerConfirmed },

                { EContractStatus.EmployerPending, EContractStatusErrors.InValidStatusForEmployerPending },
                { EContractStatus.EmployerReturned, EContractStatusErrors.InValidStatusForEmployerConfirmed },
                { EContractStatus.EmployerConfirmed, EContractStatusErrors.InValidStatusForEmployerConfirmed },

                { EContractStatus.ContractExpertPending, EContractStatusErrors.InValidStatusForContractExpertPending },
                { EContractStatus.ContractExpertReturnToUser, EContractStatusErrors.InValidStatusForContractExpertReturnToUser },
                { EContractStatus.ContractExpertReturned, EContractStatusErrors.InValidStatusForProjectManagerReturned },
                { EContractStatus.ContractExpertConfirmed, EContractStatusErrors.InValidStatusForContractExpertConfirmed },

                { EContractStatus.ContractSupervisorPending, EContractStatusErrors.InValidStatusForContractorSupervisorPending },
                { EContractStatus.ContractSupervisorConfirmed, EContractStatusErrors.InValidStatusForContractorSupervisorConfirmed },
                { EContractStatus.ContractSupervisorReturnedToUser, EContractStatusErrors.InValidStatusForContractorSupervisorReturnToUser },
                { EContractStatus.ReturnToContractExpert, EContractStatusErrors.InValidStatusForReturnToProjectManager },

                { EContractStatus.PrimaryManagerConfirmed, EContractStatusErrors.InValidStatusForPrimaryManagerConfirmed },
                { EContractStatus.PrimaryManagerReturned, EContractStatusErrors.InValidStatusForPrimaryManagerReturned },

                { EContractStatus.FinalManagerConfirmed, EContractStatusErrors.InValidStatusForFinalManagerConfirmed },
                { EContractStatus.FinalManagerReturned, EContractStatusErrors.InValidStatusForFinalManagerReturned },
            };

            if (!statusConditions[request.Status]())
                return Result.Failure<EmployerContract>(errorMessages[request.Status]);
            var history = await _contractHistoryRepo.GetEContractHistory(request.Id, 0, 0, ct);

            if (!history.Any())
                entity.AddHistory();

            if (EContractStatusRules.AllowManager.Contains(request.Status))
            {

                bool hasPrimary = history.Any(x => x.Status == EContractStatus.PrimaryManagerConfirmed);
                bool hasFinal = history.Any(x => x.Status == EContractStatus.FinalManagerConfirmed);

                if (hasPrimary && hasFinal)
                    return Result.Failure<EmployerContract>(EContractErrors.IsFinalOrPrimaryManagerConfirmed);

                entity.AddHistory(request.Status);
            }

            else if (EContractStatusRules.AllowEmployer.Contains(request.Status))
            {
                var lastStatus = history?.LastOrDefault();

                if (lastStatus != null && lastStatus.Status == request.Status)
                    return Result.Failure<EmployerContract>(EContractErrors.DuplicateStatusCombination);

                entity.AddHistory(request.Status);
            }
            else
                entity?.UpdateStatus(request.Status, request.Description);

            await _contractRepo.Update(entity!);
            return entity;
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerContract?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<EmployerOperation?>> CreateUpdateOperationProductHandler(
    UpdateProjectOperationFinancialModel covers, EmployerOperation eOperation, CancellationToken ct)
    {
        try
        {
            if (covers.CreateProducts?.Any() == true)
            {
                var existingProducts = eOperation.EmployerOperationProducts.ToList();
                var existingGroupIds = existingProducts.Select(p => p.ProductGroupId).ToHashSet();
                var newGroupIds = covers.CreateProducts.Select(p => p.ProductGroupId).ToList();

                var duplicateIds = newGroupIds.Where(id => existingGroupIds.Contains(id)).ToList();
                if (duplicateIds.Any())
                    return Result.Failure<EmployerOperation?>(EContractErrors.DuplicateProductGroupCombination);

                foreach (var product in covers.CreateProducts)
                {
                    var newProduct = new EmployerOperationProduct(
                        eOperation,
                        null,
                        product.ProductGroupId,
                        product.ProductId,
                        product.MinPrice ?? 0,
                        product.MaxPrice,
                        product.Count,
                        product.Tax,
                        product.TaxPercent,
                        product.TransportationCost,
                        product.TransportationCostPercent,
                        product.ProfitCost,
                        product.ProfitCostPercent,
                        product.OtherCost,
                        product.OtherCostPercent,
                        false,
                        product.Description);

                    await _employerOperationProductRepo.Create(newProduct, ct);
                }
            }

            if (covers.AssignProducts?.Any() == true)
            {
                var existingProducts = eOperation.EmployerOperationProducts.ToList();
                if (existingProducts is null)
                    return Result.Failure<EmployerOperation?>(EContractErrors.EContractProductNotFound);
                var oI = eOperation.ProjectOperation.OperationInfo.Id;
                var consumptions = await _mediator.Send(new GetsProductByOperationInfoIdQuery(
                    oI,
                    null,
                    null,
                    1,
                    1));
                if (consumptions is null)
                    return Result.Failure<EmployerOperation?>(EContractErrors.EOperationInfoNotFound);
                var existingGroupIds = existingProducts.Select(p => p.ProductGroupId).ToHashSet();
                var newGroupIds = covers.CreateProducts!.Select(p => p.ProductGroupId).ToList();

                var duplicateIds = newGroupIds.Where(id => existingGroupIds.Contains(id)).ToList();
                if (duplicateIds.Any())
                    return Result.Failure<EmployerOperation?>(EContractErrors.DuplicateProductGroupCombination);

                foreach (var product in covers.CreateProducts!)
                {
                    var newProduct = new EmployerOperationProduct(
                        eOperation,
                        consumptions.Value?.Data?.FirstOrDefault(x => x.ProductUnitId == product.ProductGroupId),
                        product.ProductGroupId,
                        product.ProductId,
                        product.MinPrice ?? 0,
                        product.MaxPrice,
                        product.Count,
                        product.Tax,
                        product.TaxPercent,
                        product.TransportationCost,
                        product.TransportationCostPercent,
                        product.ProfitCost,
                        product.ProfitCostPercent,
                        product.OtherCost,
                        product.OtherCostPercent,
                        true,
                        product.Description);

                    await _employerOperationProductRepo.Create(newProduct, ct);
                }
            }

            return eOperation;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerOperation?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<EmployerOperation?>> CreateOperationProductHandler(
    ProjectOperationFinancialModel covers, EmployerOperation eOperation, CancellationToken ct)
    {
        try
        {
            if (covers.CreateProducts?.Any() == true)
            {
                var existingProducts = eOperation.EmployerOperationProducts.ToList();
                var existingGroupIds = existingProducts.Select(p => p.ProductGroupId).ToHashSet();
                var newGroupIds = covers.CreateProducts.Select(p => p.ProductGroupId).ToList();

                var duplicateIds = newGroupIds.Where(id => existingGroupIds.Contains(id)).ToList();
                if (duplicateIds.Any())
                    return Result.Failure<EmployerOperation?>(EContractErrors.DuplicateProductGroupCombination);

                foreach (var product in covers.CreateProducts)
                {
                    var newProduct = new EmployerOperationProduct(
                        eOperation,
                        null,
                        product.ProductGroupId,
                        product.ProductId,
                        product.MinPrice ?? 0,
                        product.MaxPrice,
                        product.Count,
                        product.Tax,
                        product.TaxPercent,
                        product.TransportationCost,
                        product.TransportationCostPercent,
                        product.ProfitCost,
                        product.ProfitCostPercent,
                        product.OtherCost,
                        product.OtherCostPercent,
                        false,
                        product.Description);

                    await _employerOperationProductRepo.Create(newProduct, ct);
                }
            }

            return eOperation;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerOperation?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<EmployerOperation?>> AssignOperationProductHandler(
    ProjectOperationFinancialModel covers, EmployerOperation eOperation, CancellationToken ct)
    {
        try
        {
            if (covers.AssignProducts?.Any() == true)
            {
                var existingProducts = eOperation.EmployerOperationProducts.ToList();
                var oI = eOperation.ProjectOperation.OperationInfo.Id;
                var consumptions = await _mediator.Send(new GetsProductByOperationInfoIdQuery(
                    oI,
                    null,
                    null,
                    1,
                    1));
                if (consumptions is null)
                    return Result.Failure<EmployerOperation?>(EContractErrors.EOperationInfoNotFound);
                var existingGroupIds = existingProducts.Select(p => p.ProductGroupId).ToHashSet();
                var newGroupIds = covers.AssignProducts!.Select(p => p.ProductGroupId).ToList();

                var duplicateIds = newGroupIds.Where(id => existingGroupIds.Contains(id)).ToList();
                if (duplicateIds.Any())
                    return Result.Failure<EmployerOperation?>(EContractErrors.DuplicateProductGroupCombination);

                foreach (var product in covers.AssignProducts!)
                {
                    var newProduct = new EmployerOperationProduct(
                        eOperation,
                        consumptions.Value?.Data?.FirstOrDefault(x => x.ProductUnitId == product.ProductGroupId),
                        product.ProductGroupId,
                        product.ProductId,
                        product.MinPrice ?? 0,
                        product.MaxPrice,
                        product.Count,
                        product.Tax,
                        product.TaxPercent,
                        product.TransportationCost,
                        product.TransportationCostPercent,
                        product.ProfitCost,
                        product.ProfitCostPercent,
                        product.OtherCost,
                        product.OtherCostPercent,
                        true,
                        product.Description);

                    await _employerOperationProductRepo.Create(newProduct, ct);
                }
            }
            return eOperation;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerOperation?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<EmployerOperation?>> DeleteOperationProductHandler(
    List<long> productIds, EmployerOperation eOperation, CancellationToken ct)
    {
        try
        {
            if (!eOperation.EmployerOperationProducts.ToList().HasAny())
                return Result.Failure<EmployerOperation?>(EContractErrors.EContractProductNotFound);

            eOperation.EmployerOperationProducts
                .Where(x => productIds.Contains(x.Id)).ToList()
                .ForEach(p => p!.SoftDelete());

            await _contractOperationRepo.Update(eOperation);
            return eOperation;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerOperation?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<EmployerOperation?>> AssignOperationServiceHandler(
    ProjectOperationFinancialModel covers, EmployerOperation eOperation, CancellationToken ct)
    {
        try
        {
            if (covers.AssignServices?.Any() == true)
            {
                var ids = covers.AssignServices.Select(x => x.ServiceId).ToList();
                var oInfoServiceRes = await _operationInfoServiceRepository.GetsOperationInfoServiceByOIId(eOperation.ProjectOperation.OperationInfoId, ids, ct);
                if (oInfoServiceRes is null)
                    return Result.Failure<EmployerOperation>(EContractErrors.EContractServiceNotFound);
                var oInfoService = oInfoServiceRes;
                foreach (var service in covers.AssignServices)
                {
                    var oIService = oInfoService!.FirstOrDefault(x => x.ServiceInfo.Id == service.ServiceId);
                    if (oIService is null)
                        return Result.Failure<EmployerOperation>(EContractErrors.EContractServiceWithIdNotFound);
                    var newService = new EmployerOperationService(
                        eOperation,
                        oIService,
                        null,
                        service.MinPrice ?? 0,
                        service.MaxPrice,
                        service.Tax,
                        service.TaxPercent,
                        service.TransportationCost,
                        service.TransportationCostPercent,
                        service.ProfitCost,
                        service.ProfitCostPercent,
                        service.OtherCost,
                        service.OtherCostPercent,
                        true,
                        service.Description);

                    await _employerOperationServiceRepo.Create(newService, ct);
                }
            }
            return eOperation;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerOperation?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<EmployerOperation?>> CreateOperationServiceHandler(
    ProjectOperationFinancialModel covers, EmployerOperation eOperation, CancellationToken ct)
    {
        try
        {
            var creates = covers.CreateServices;
            if (creates is not null && creates.Count > 0)
            {
                var ids = creates.Listed(x => x.ServiceId);
                var serviceInfos = await _serviceInfoRepository.GetsServiceInfoByIds(ids, 1, ids.Count(), ct);
                foreach (var service in creates)
                {
                    var serviceInfo = serviceInfos.Data.FirstOrDefault(x => x.Id == service.ServiceId);
                    var newService = new EmployerOperationService(
                        eOperation,
                        null,
                        serviceInfo,
                        service.MinPrice ?? 0,
                        service.MaxPrice,
                        service.Tax,
                        service.TaxPercent,
                        service.TransportationCost,
                        service.TransportationCostPercent,
                        service.ProfitCost,
                        service.ProfitCostPercent,
                        service.OtherCost,
                        service.OtherCostPercent,
                        false,
                        service.Description);

                    await _employerOperationServiceRepo.Create(newService, ct);
                }
            }
            return eOperation;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerOperation?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<EmployerOperation?>> CreateUpdateOperationServiceHandler(
    UpdateProjectOperationFinancialModel covers, EmployerOperation eOperation, CancellationToken ct)
    {
        try
        {
            var creates = covers.CreateServices;
            if (creates is not null && creates.Count > 0)
            {
                var ids = creates.Listed(x => x.ServiceId);
                var serviceInfos = await _serviceInfoRepository.GetsServiceInfoByIds(ids, 1, ids.Count(), ct);
                foreach (var service in creates)
                {
                    var serviceInfo = serviceInfos.Data.FirstOrDefault(x => x.Id == service.ServiceId);
                    var newService = new EmployerOperationService(
                        eOperation,
                        null,
                        serviceInfo,
                        service.MinPrice ?? 0,
                        service.MaxPrice,
                        service.Tax,
                        service.TaxPercent,
                        service.TransportationCost,
                        service.TransportationCostPercent,
                        service.ProfitCost,
                        service.ProfitCostPercent,
                        service.OtherCost,
                        service.OtherCostPercent,
                        false,
                        service.Description);

                    await _employerOperationServiceRepo.Create(newService, ct);
                }
            }

            if (covers.AssignServices?.Any() == true)
            {

                var ids = covers.AssignServices.Select(x => x.ServiceId).ToList();
                var oInfoServiceRes = await _mediator.Send(new GetsOperationInfoServiceByIdsQuery(ids, 1, ids.Count));
                if (oInfoServiceRes.IsBad())
                    return oInfoServiceRes.Failure<EmployerOperation>()!;
                var oInfoService = oInfoServiceRes.Value.Data;
                foreach (var service in covers.AssignServices)
                {
                    var oIService = oInfoService!.FirstOrDefault(x => x.Id == service.ServiceId);
                    if (oIService is null)
                        return Result.Failure<EmployerOperation>(EContractErrors.EContractServiceWithIdNotFound);
                    var newService = new EmployerOperationService(
                        eOperation,
                        oIService,
                        null,
                        service.MinPrice ?? 0,
                        service.MaxPrice,
                        service.Tax,
                        service.TaxPercent,
                        service.TransportationCost,
                        service.TransportationCostPercent,
                        service.ProfitCost,
                        service.ProfitCostPercent,
                        service.OtherCost,
                        service.OtherCostPercent,
                        true,
                        service.Description);

                    await _employerOperationServiceRepo.Create(newService, ct);
                }
            }
            return eOperation;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerOperation?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<EmployerOperation?>> DeleteOperationServiceHandler(
    List<long> serviceIds, EmployerOperation eOperation, CancellationToken ct)
    {
        try
        {
            if (!eOperation.EmployerOperationServices.ToList().HasAny())
                return Result.Failure<EmployerOperation?>(EContractErrors.EContractServiceNotFound);

            eOperation.EmployerOperationServices
                .Where(x => serviceIds.Contains(x.Id)).ToList()
                .ForEach(p => p!.SoftDelete());

            await _contractOperationRepo.Update(eOperation);
            return eOperation;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerOperation?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<EmployerOperation?>> UpdateOperationProductHandler(
    ProjectOperationFinancialModel covers, EmployerOperation eOperation, CancellationToken ct)
    {
        try
        {
            var eProducts = eOperation.EmployerOperationProducts.ToList();

            if (covers.UpdateProducts?.Any() == true)
            {
                foreach (var entity in eProducts.Where(x => covers.UpdateProducts.Any(p => p.Id == x.Id)))
                {
                    var product = covers.UpdateProducts.First(p => p.Id == entity.Id);
                    entity.Update(
                        product.MinPrice ?? entity.MinPrice,
                        product.MaxPrice,
                        product.Count,
                        product.Tax,
                        product.TaxPercent,
                        product.TransportationCost,
                        product.TransportationCostPercent,
                        product.ProfitCost,
                        product.ProfitCostPercent,
                        product.OtherCost,
                        product.OtherCostPercent,
                        null,
                        product.Description);
                }
            }

            if (covers.UpdateAssignProducts?.Any() == true)
            {
                foreach (var entity in eProducts.Where(x => covers.UpdateAssignProducts.Any(p => p.Id == x.Id)))
                {
                    var product = covers.UpdateAssignProducts.First(p => p.Id == entity.Id);

                    entity.Update(
                        product.MinPrice ?? entity.MinPrice,
                        product.MaxPrice,
                        product.Count,
                        product.Tax,
                        product.TaxPercent,
                        product.TransportationCost,
                        product.TransportationCostPercent,
                        product.ProfitCost,
                        product.ProfitCostPercent,
                        product.OtherCost,
                        product.OtherCostPercent,
                        null,
                        product.Description);
                }
            }

            await _contractOperationRepo.Update(eOperation);
            return eOperation;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerOperation?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<EmployerOperation?>> UpdateOperationProductHandler(
    UpdateProjectOperationFinancialModel covers, EmployerOperation eOperation, CancellationToken ct)
    {
        try
        {
            var eProducts = eOperation.EmployerOperationProducts.ToList();

            if (covers.UpdateProducts?.Any() == true)
            {
                foreach (var entity in eProducts.Where(x => covers.UpdateProducts.Any(p => p.Id == x.Id)))
                {
                    var product = covers.UpdateProducts.First(p => p.Id == entity.Id);
                    entity.Update(
                        product.MinPrice ?? entity.MinPrice,
                        product.MaxPrice,
                        product.Count,
                        product.Tax,
                        product.TaxPercent,
                        product.TransportationCost,
                        product.TransportationCostPercent,
                        product.ProfitCost,
                        product.ProfitCostPercent,
                        product.OtherCost,
                        product.OtherCostPercent,
                        null,
                        product.Description);
                }
            }

            if (covers.UpdateAssignProducts?.Any() == true)
            {
                foreach (var entity in eProducts.Where(x => covers.UpdateAssignProducts.Any(p => p.Id == x.Id)))
                {
                    var product = covers.UpdateAssignProducts.First(p => p.Id == entity.Id);

                    entity.Update(
                        product.MinPrice ?? entity.MinPrice,
                        product.MaxPrice,
                        product.Count,
                        product.Tax,
                        product.TaxPercent,
                        product.TransportationCost,
                        product.TransportationCostPercent,
                        product.ProfitCost,
                        product.ProfitCostPercent,
                        product.OtherCost,
                        product.OtherCostPercent,
                        null,
                        product.Description);
                }
            }

            await _contractOperationRepo.Update(eOperation);
            return eOperation;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerOperation?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<EmployerOperation?>> UpdateOperationServiceHandler(
    ProjectOperationFinancialModel covers, EmployerOperation eOperation, CancellationToken ct)
    {
        try
        {
            if (covers.UpdateServices?.Any() == true)
            {
                foreach (var serviceModel in covers.UpdateServices)
                {
                    var existingService = eOperation.EmployerOperationServices
                        .FirstOrDefault(s => s.Id == serviceModel.Id);

                    if (existingService is null)
                        return Result.Failure<EmployerOperation?>(SharedErrors.UnknownError);

                    existingService.Update(
                        serviceModel.MinPrice ?? existingService.MinPrice,
                        serviceModel.MaxPrice,
                        serviceModel.Tax,
                        serviceModel.TaxPercent,
                        serviceModel.TransportationCost,
                        serviceModel.TransportationCostPercent,
                        serviceModel.ProfitCost,
                        serviceModel.ProfitCostPercent,
                        serviceModel.OtherCost,
                        serviceModel.OtherCostPercent,
                        serviceModel.Description);

                    await _employerOperationServiceRepo.Update(existingService);
                }
            }

            if (covers.UpdateAssignServices?.Any() == true)
            {
                foreach (var serviceModel in covers.UpdateAssignServices)
                {
                    var existingService = eOperation.EmployerOperationServices
                        .FirstOrDefault(s => s.Id == serviceModel.Id);

                    if (existingService is null)
                        return Result.Failure<EmployerOperation?>(SharedErrors.UnknownError);

                    existingService.Update(
                        serviceModel.MinPrice ?? existingService.MinPrice,
                        serviceModel.MaxPrice,
                        serviceModel.Tax,
                        serviceModel.TaxPercent,
                        serviceModel.TransportationCost,
                        serviceModel.TransportationCostPercent,
                        serviceModel.ProfitCost,
                        serviceModel.ProfitCostPercent,
                        serviceModel.OtherCost,
                        serviceModel.OtherCostPercent,
                        serviceModel.Description);

                    await _employerOperationServiceRepo.Update(existingService);
                }
            }

            return eOperation;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerOperation?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<EmployerOperation?>> UpdateAssignOperationServiceHandler(
    UpdateProjectOperationFinancialModel covers, EmployerOperation eOperation, CancellationToken ct)
    {
        try
        {
            if (eOperation.EmployerOperationServices == null)
                return Result.Failure<EmployerOperation?>(EContractErrors.EContractServiceNotFound);

            if (covers.UpdateAssignServices?.Any() == true)
            {
                foreach (var serviceModel in covers.UpdateAssignServices)
                {
                    var existingService = eOperation.EmployerOperationServices
                        .FirstOrDefault(s => s.Id == serviceModel.Id);

                    if (existingService is null)
                        return Result.Failure<EmployerOperation?>(SharedErrors.UnknownError);

                    existingService.Update(
                        serviceModel.MinPrice ?? existingService.MinPrice,
                        serviceModel.MaxPrice,
                        serviceModel.Tax,
                        serviceModel.TaxPercent,
                        serviceModel.TransportationCost,
                        serviceModel.TransportationCostPercent,
                        serviceModel.ProfitCost,
                        serviceModel.ProfitCostPercent,
                        serviceModel.OtherCost,
                        serviceModel.OtherCostPercent,
                        serviceModel.Description);

                    await _employerOperationServiceRepo.Update(existingService);
                }
            }

            if (covers.UpdateServices?.Any() == true)
            {
                foreach (var serviceModel in covers.UpdateServices)
                {
                    var existingService = eOperation.EmployerOperationServices
                        .FirstOrDefault(s => s.Id == serviceModel.Id);

                    if (existingService is null)
                        return Result.Failure<EmployerOperation?>(SharedErrors.UnknownError);

                    existingService.Update(
                        serviceModel.MinPrice ?? existingService.MinPrice,
                        serviceModel.MaxPrice,
                        serviceModel.Tax,
                        serviceModel.TaxPercent,
                        serviceModel.TransportationCost,
                        serviceModel.TransportationCostPercent,
                        serviceModel.ProfitCost,
                        serviceModel.ProfitCostPercent,
                        serviceModel.OtherCost,
                        serviceModel.OtherCostPercent,
                        serviceModel.Description);

                    await _employerOperationServiceRepo.Update(existingService);
                }
            }

            return eOperation;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerOperation?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<EmployerContract?>> UpdateProjectOperationVolumeHandler(
    UpdateEContractVolumeModel eContractModel, EmployerContract eContract, CancellationToken ct)
    {
        try
        {
            if (eContractModel.ProjectOperationVolumeModel?.Any() == true)
            {
                foreach (var opModel in eContractModel.ProjectOperationVolumeModel)
                {
                    var operation = eContract.EmployerOperations.FirstOrDefault(x => x.Id == opModel.Id);
                    if (operation is null)
                        return Result.Failure<EmployerContract?>(EContractErrors.EOperationIsNotAssigned);

                    operation.UpdateVolume(opModel.Workload, opModel.Description);
                    await _contractOperationRepo.Update(operation);
                }
            }
            return eContract;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerContract?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<EmployerContractHead?>> UpdateEContractHeadVolumeHandler(
    UpdateEContractVolumeRequest eContractModel, EmployerContractHead eContractHead, CancellationToken ct)
    {
        try
        {
            if (eContractModel is not null)
            {
                eContractHead.UpdateVolume(eContractModel.VolumeTolerance);
                await _headRepo.Update(eContractHead);
            }
            return eContractHead;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerContractHead?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<EmployerContractHead?>> CreateEContractHeadFinancialHandler(
    EContractFinancialCoverRequest eContractModel, EmployerContractHead eContractHead, CancellationToken ct)
    {
        try
        {
            if (eContractModel is not null)
            {
                eContractHead.UpdateCost(eContractModel.PriceTolerance);
                await _headRepo.Update(eContractHead);
            }
            return eContractHead;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerContractHead?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<EmployerContract?>> EContractFinancialCoverHandler(
    EContractFinancialCoverModel eContractModel, EmployerContractHead eContractHead, CancellationToken ct)
    {
        try
        {
            var eContractRes = await GetEContractHandler(eContractModel.Id, ct);
            if (eContractRes is null)
                return Result.Failure<EmployerContract>(EContractErrors.FilteredEmployerContractNotFound);

            var eContract = eContractRes.Value!;
            eContract.UpdateCost(eContractModel.CurrencyRate, eContractModel.AdvancePayment, eContractModel.Description);
            await _contractRepo.Update(eContract);
            return eContract;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerContract?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<EmployerContract?>> CreateProjectOperationFinancialHandler(
    EContractFinancialCoverModel eContractModel, EmployerContract eContract, CancellationToken ct)
    {
        try
        {
            foreach (var pOperation in eContractModel.ProjectOperations!)
            {
                var eOperation = eContract.EmployerOperations.FirstOrDefault(x => x.Id == pOperation.EOperationId);
                if (eOperation is null)
                    return Result.Failure<EmployerContract>(EContractErrors.EOperationIsNotAssigned);
                eOperation.UpdateCost(pOperation.Price, pOperation.TolerancePercentage, pOperation.IncreaseRate, pOperation.Description);
                eOperation.AddHistory(eOperation.ProjectOperation.Workload, pOperation.Price, pOperation.IncreaseRate, pOperation.Description);
                await _contractOperationRepo.Update(eOperation);
            }
            return eContract;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerContract?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<EmployerContractHead?>> UpdateEContractHeadFinancialHandler(
    UpdateEContractFinancialRequest eContractModel, EmployerContractHead eContractHead, CancellationToken ct)
    {
        try
        {
            if (eContractModel is not null)
            {
                eContractHead.UpdateCost(eContractModel.PriceTolerance);
                await _headRepo.Update(eContractHead);
            }
            return eContractHead;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerContractHead?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<EmployerContract?>> UpdateEContractFinancialHandler(
    UpdateEContractFinancialModel eContractModel, EmployerContractHead eContractHead, CancellationToken ct)
    {
        try
        {
            var eContractRes = await GetEContractHandler(eContractModel.Id, ct);
            if (eContractRes is null)
                return Result.Failure<EmployerContract>(EContractErrors.FilteredEmployerContractNotFound);
            var eContract = eContractRes.Value!;

            eContract.UpdateCost(eContractModel.CurrencyRate, eContractModel.AdvancePayment, eContractModel.Description);
            await _contractRepo.Update(eContract);
            return eContract;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerContract?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<EmployerContract?>> UpdateProjectOperationFinancialHandler(
    UpdateEContractFinancialModel eContractModel, EmployerContract eContract, CancellationToken ct)
    {
        try
        {
            foreach (var pOperation in eContractModel.ProjectOperations!)
            {
                var eOperation = eContract.EmployerOperations.FirstOrDefault(x => x.Id == pOperation.EOperationId);
                if (eOperation is null)
                    return Result.Failure<EmployerContract>(EContractErrors.EOperationIsNotAssigned);
                eOperation.UpdateCost(pOperation.Price, pOperation.TolerancePercentage, pOperation.IncreaseRate, pOperation.Description);
                eOperation.AddHistory(eOperation.ProjectOperation.Workload, pOperation.Price, pOperation.IncreaseRate, pOperation.Description);
                await _contractOperationRepo.Update(eOperation);
            }
            return eContract;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerContract?>(SharedErrors.UnknownError);
        }
    }
}