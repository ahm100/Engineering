using Engineering.Application.Services.EmployerContracts.Contracts.CreateEContract;
using Engineering.Application.Services.EmployerContracts.Contracts.EContractFinancialCover;
using Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContract;
using Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContractFinancial;
using Engineering.Domain.Entities.EmployerContracts;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.EmployerContracts;

public partial class EContractHeaderLogic
{
    private async Task<Result<EmployerContract?>> CreateContract(
        EmployerContractHead head, List<Project> projects, CreateEContractModel item, CT ct)
    {
        var project = projects!.FirstOrDefault(x => x.Id == item.ProjectId);
        if (project is null)
            return Result.Failure<EmployerContract>(EContractErrors.NoContractual!);

        var contract = await CreateContractHandler(head, project!, item, ct);
        if (contract.IsBad()) return contract.Failure<EmployerContract?>()!;

        if (item.CreateDocuments.HasAny())
        {
            var docs = await CreateDocHandler(contract.Value!, item.CreateDocuments!, ct);
            if (docs.IsBad()) return docs.Failure<EmployerContract?>()!;
        }

        var flagedConsiderations = new List<EmployerConsideration>();
        if (item.CreateConsiderations.HasAny())
        {
            var considerations = await CreateConsiderationHandler(contract.Value!, item.CreateConsiderations!, ct);
            if (considerations.IsBad()) return considerations.Failure<EmployerContract>()!;
            flagedConsiderations = considerations.Value!.Where(x => x.FlagId != null).ToList();
        }

        if (item.AssigneOperations.HasAny())
        {
            var ePOperations = await CreateOperationsHandler(contract.Value!,
                flagedConsiderations, item.AssigneOperations!, ct);
            if (ePOperations.IsBad()) return ePOperations.Failure<EmployerContract>()!;
        }

        if (item.CreateOperations.HasAny())
        {
            var pOperations = await CreateNewOperationsHandler(contract.Value!,
                flagedConsiderations, item.CreateOperations!, ct);
            if (pOperations.IsBad()) return pOperations.Failure<EmployerContract>()!;
        }

        return contract;
    }

    private async Task<Result<EmployerContract?>> UpdateContract(
        EmployerContract value, UpdateEContractRequest item, CT ct)
    {
        var pOperations = value.EmployerOperations.Select(x => x.ProjectOperation).ToList();

        var response = await UpdateContractHandler(value!, item, ct);
        if (response.IsBad()) return response.Failure<EmployerContract>()!;
        var entity = response.Value;

        if (item.CreateDocuments.HasAny())
        {
            var docs = await CreateDocHandler(entity!, item.CreateDocuments!, ct);
            if (docs.IsBad()) return docs.Failure<EmployerContract?>()!;
        }

        if (item.UpdateDocuments.HasAny())
        {
            var docs = await UpdateDocHandler(entity!, item.UpdateDocuments!, ct);
            if (docs.IsBad()) return docs.Failure<EmployerContract?>()!;
        }

        if (item.DeleteDocuments.HasAny())
        {
            var docs = await DeleteDocHandler(entity!, item.DeleteDocuments!, ct);
            if (docs.IsBad()) return docs.Failure<EmployerContract?>()!;
        }

        var flagedConsiderations = new List<EmployerConsideration>();
        if (item.CreateConsiderations.HasAny())
        {
            var considerations = await CreateConsiderationHandler(
                entity!, item.CreateConsiderations!, ct);
            if (considerations.IsBad()) return considerations.Failure<EmployerContract>()!;
            flagedConsiderations = considerations.Value!.Where(x => x.FlagId != null).ToList();
        }

        if (item.UpdateConsiderations.HasAny())
        {
            var considerations = await UpdateConsiderationHandler(
                entity!, item.UpdateConsiderations!, ct);
            if (considerations.IsBad()) return considerations.Failure<EmployerContract>()!;
            flagedConsiderations = considerations.Value!.Where(x => x.FlagId != null).ToList();
        }

        if (item.DeleteConsiderations.HasAny())
        {
            var docs = await DeleteConsiderationHandler(
                entity!, item.DeleteConsiderations!, ct);
            if (docs.IsBad()) return docs.Failure<EmployerContract?>()!;
        }

        if (item.DeleteOperations.HasAny())
        {
            var operations = await DeleteOperationsHandler(entity!, item.DeleteOperations!, ct);
            if (operations.IsBad()) return operations.Failure<EmployerContract?>()!;
            pOperations = pOperations
                .Where(x => !item.DeleteOperations!.Contains(x.Id))
                .ToList();
        }

        if (item.UpdateOperations.HasAny())
        {
            var operations = await UpdateOperationsHandler(entity!,
                item.UpdateOperations!, ct);
            if (operations.IsBad()) return operations.Failure<EmployerContract>()!;
            pOperations = pOperations.Concat(operations.Value!).ToList();
            var hasDuplicates = pOperations
                .GroupBy(x => new { x.Id, x.UnitOfMeasurementId, OperationInfoId = x.OperationInfo.Id })
                .Any(g => g.Count() > 1);
            if (hasDuplicates)
                return operations.Failure<EmployerContract>()!;
        }

        if (item.CreateOperations.HasAny())
        {
            var operations = await CreateNewOperationsHandler(entity!,
                flagedConsiderations, item.CreateOperations!, ct);
            if (operations.IsBad()) return operations.Failure<EmployerContract>()!;
            pOperations = pOperations.Concat(operations.Value!).ToList();
            var hasDuplicates = pOperations
                .GroupBy(x => new { x.Id, x.UnitOfMeasurementId, OperationInfoId = x.OperationInfo.Id })
                .Any(g => g.Count() > 1);
            if (hasDuplicates)
                return Result.Failure<EmployerContract>(EContractErrors.DuplicateOperationCombination)!;
        }

        if (item.AssigneOperations.HasAny())
        {
            var assignOPIds = item.AssigneOperations!.Listed(x => x.ProjectOperationId);
            if (pOperations.Any(x => assignOPIds.Contains(x.Id)))
                return Result.Failure<EmployerContract>(EContractErrors.NotFound);

            var ePOperations = await CreateOperationsHandler(entity!,
                flagedConsiderations, item.AssigneOperations!, ct);
            if (ePOperations.IsBad()) return ePOperations.Failure<EmployerContract>()!;
        }

        await _contractRepo.Update(entity!);
        return entity!;
    }

    private async Task<Result<EmployerContract?>> FinancialCreate(
        EmployerContract contract, EContractFinancialCoverModel cover, CT ct)
    {
        if (cover.ProjectOperations.HasAny())
        {
            foreach (var pOperation in cover.ProjectOperations!)
            {
                var eOperation = contract.EmployerOperations.FirstOrDefault(x => x.Id == pOperation.EOperationId);
                if (eOperation is null)
                    return Result.Failure<EmployerContract?>(EContractErrors.EOperationIsNotAssigned)!;

                if (pOperation.DeletedProductGroups.HasAny())
                {
                    var created = await DeleteOperationProductHandler(pOperation.DeletedProductGroups!, eOperation, ct);
                    if (created.IsBad())
                        return created.Failure<EmployerContract>()!;
                }

                if (pOperation.AssignProducts.HasAny())
                {
                    var created = await AssignOperationProductHandler(pOperation, eOperation, ct);
                    if (created.IsBad())
                        return created.Failure<EmployerContract>()!;
                }

                if (pOperation.UpdateAssignProducts.HasAny() || pOperation.UpdateProducts.HasAny())
                {
                    var created = await UpdateOperationProductHandler(pOperation, eOperation, ct);
                    if (created.IsBad())
                        return created.Failure<EmployerContract>()!;
                }

                if (pOperation.CreateProducts.HasAny())
                {
                    var created = await CreateOperationProductHandler(pOperation, eOperation, ct);
                    if (created.IsBad())
                        return created.Failure<EmployerContract>()!;
                }

                if (pOperation.DeletedServices.HasAny())
                {
                    var created = await DeleteOperationServiceHandler(pOperation.DeletedServices!, eOperation, ct);
                    if (created.IsBad())
                        return created.Failure<EmployerContract>()!;
                }

                if (pOperation.UpdateServices.HasAny() || pOperation.UpdateAssignServices.HasAny())
                {
                    var created = await UpdateOperationServiceHandler(pOperation, eOperation, ct);
                    if (created.IsBad())
                        return created.Failure<EmployerContract>()!;
                }

                if (pOperation.AssignServices.HasAny())
                {
                    var created = await AssignOperationServiceHandler(pOperation, eOperation, ct);
                    if (created.IsBad())
                        return created.Failure<EmployerContract>()!;
                }

                if (pOperation.CreateServices.HasAny())
                {
                    var created = await CreateOperationServiceHandler(pOperation, eOperation, ct);
                    if (created.IsBad())
                        return created.Failure<EmployerContract>()!;
                }
            }
        }
        await _contractRepo.Update(contract);
        return contract;
    }

    private async Task<Result<EmployerContract?>> FinancialUpdate(
        EmployerContract contract, UpdateEContractFinancialModel cover, CT ct)
    {
        if (cover.ProjectOperations.HasAny())
        {
            foreach (var pOperation in cover.ProjectOperations!)
            {
                var eOperation = contract.EmployerOperations.FirstOrDefault(x => x.Id == pOperation.EOperationId);
                if (eOperation is null)
                    return Result.Failure<EmployerContract?>(EContractErrors.EOperationIsNotAssigned)!;

                if (pOperation.DeletedProductGroups != null)
                {
                    var deleted = await DeleteOperationProductHandler(pOperation.DeletedProductGroups, eOperation, ct);
                    if (deleted.IsBad())
                        return deleted.Failure<EmployerContract>()!;
                }

                if (pOperation.UpdateProducts.HasAny() || pOperation.UpdateAssignProducts.HasAny())
                {
                    var created = await UpdateOperationProductHandler(pOperation, eOperation, ct);
                    if (created.IsBad())
                        return created.Failure<EmployerContract>()!;
                }

                if (pOperation.CreateProducts.HasAny() || pOperation.AssignProducts.HasAny())
                {
                    var created = await CreateUpdateOperationProductHandler(pOperation, eOperation, ct);
                    if (created.IsBad())
                        return created.Failure<EmployerContract>()!;
                }

                if (pOperation.DeletedServices != null)
                {
                    var created = await DeleteOperationServiceHandler(pOperation.DeletedServices, eOperation, ct);
                    if (created.IsBad())
                        return created.Failure<EmployerContract>()!;
                }

                if (pOperation.UpdateAssignServices.HasAny() || pOperation.UpdateServices.HasAny())
                {
                    var created = await UpdateAssignOperationServiceHandler(pOperation, eOperation, ct);
                    if (created.IsBad())
                        return created.Failure<EmployerContract>()!;
                }

                if (pOperation.CreateServices.HasAny() || pOperation.AssignServices.HasAny())
                {
                    var created = await CreateUpdateOperationServiceHandler(pOperation, eOperation, ct);
                    if (created.IsBad())
                        return created.Failure<EmployerContract>()!;
                }
            }
        }
        await _contractRepo.Update(contract);
        return contract;
    }

    private async Task<Result<EmployerContract?>> EContractFinancialCover(
        EmployerContractHead eContractHead, EContractFinancialCoverModel request, CT ct)
    {
        var contract = await EContractFinancialCoverHandler(request, eContractHead, ct);
        if (contract.IsBad())
            return contract.Failure<EmployerContract>()!;

        if (request.ProjectOperations.HasAny())
        {
            var created = await CreateProjectOperationFinancialHandler(request, contract.Value!, ct);
            if (created.IsBad())
                return created.Failure<EmployerContract>()!;
            var createdEContract = await FinancialCreate(contract.Value!, request, ct);
            if (createdEContract.IsFailure)
                return createdEContract.Failure<EmployerContract>()!;
        }

        if (request.DeleteCostOvers.HasAny())
        {
            var created = await DeleteCostOverHandler(contract.Value!, request.DeleteCostOvers!, ct);
            if (created.IsFailure)
                return created.Failure<EmployerContract>()!;
        }

        if (request.CostOvers.HasAny())
        {
            var created = await CreateCostOverHandler(contract.Value!, request.CostOvers!, ct);
            if (created.IsFailure)
                return created.Failure<EmployerContract>()!;
        }

        await _contractRepo.Update(contract.Value!);
        return contract;
    }

    private async Task<Result<EmployerContract?>> UpdateEContractFinancial(
        EmployerContractHead eContractHead, UpdateEContractFinancialModel request, CT ct)
    {
        var contract = await UpdateEContractFinancialHandler(request, eContractHead, ct);
        if (contract.IsFailure)
            return contract.Failure<EmployerContract>()!;

        if (request.ProjectOperations.HasAny())
        {
            var created = await UpdateProjectOperationFinancialHandler(request, contract.Value!, ct);
            if (created.IsBad())
                return created.Failure<EmployerContract>()!;
            var createdEContract = await FinancialUpdate(contract.Value!, request, ct);
        }

        if (request.DeleteCostOvers.HasAny())
        {
            var created = await DeleteCostOverHandler(contract.Value!, request.DeleteCostOvers!, ct);
            if (created.IsBad())
                return created.Failure<EmployerContract>()!;
        }

        if (request.CostOvers.HasAny())
        {
            var created = await UpdateCostOverHandler(contract.Value!, request.CostOvers!, ct);
            if (created.IsBad())
                return created.Failure<EmployerContract>()!;
        }

        await _contractRepo.Update(contract.Value!);
        return contract;
    }
}