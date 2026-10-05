using Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.CreateContractorContract;
using Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.DeleteContractorContract;
using Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.SetCCTotalAmount;
using Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.UpdateContractorContract;
using Engineering.Application.Services.ContractorContracts.Commands.Details.CreateContractorContractDetail;
using Engineering.Application.Services.ContractorContracts.Commands.Details.CreateContractorContractDetailService;
using Engineering.Application.Services.ContractorContracts.Commands.Details.DeleteContractorContractDetail;
using Engineering.Application.Services.ContractorContracts.Commands.Details.UpdateContractorContractDetail;
using Engineering.Application.Services.ContractorContracts.Contracts.CreateContractorContract;
using Engineering.Application.Services.ContractorContracts.Contracts.UpdateContractorContract.ItemPriceList;
using Engineering.Application.Services.ContractorContracts.Queries.GetFixContractorContractByIdIncludeless;
using Engineering.Application.Services.ContractorContracts.Queries.GetsRequestedOperationContract;
using Engineering.Application.Services.ProjectOperations.Queries.GetPOsWithoutInclude;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.ContractorContracts;

public partial class ContractorContractLogic : IContractorContractLogic
{
    private async Task<Result<List<ContractorContract>?>> CreateOperationBasedContracts(
    Project project,
    long contractorId,
    ContractorContractHeader value,
    List<CreateItemPriceListCCRequest> operationCCs,
    CT ct)
    {
        if (operationCCs is null ||
        operationCCs.Count == 0 ||
        operationCCs.Any(x => x is null))
            return Result.Failure<List<ContractorContract>>(
                GlobalErrors.ValueIsNull);

        if (operationCCs.Any(x =>
                x.Details is null ||
                x.Details.Count == 0))
            return Result.Failure<List<ContractorContract>>(
                ContractorContractErrors.POIdsIsNull);

        var contractorContracts = new List<ContractorContract>();

        var poIds = operationCCs
            .SelectMany(x => x.Details.Select(p => p.ProjectOperationId))
            .Distinct()
            .ToList();

        if (poIds.Count == 0)
            return Result.Failure<List<ContractorContract>>(
                ContractorContractErrors.POIdsIsNull);
        if (operationCCs
            .SelectMany(x => x.Details)
            .Any(x => x.ContractCoefficient <= 0))
        {
            return Result.Failure<List<ContractorContract>>(
                ContractorContractErrors.InValidContractCoefficient);
        }

        if (operationCCs.Any(x =>
                x.ProjectOperationDetailServiceIds is null ||
                x.ProjectOperationDetailServiceIds.Count == 0))
        {
            return Result.Failure<List<ContractorContract>>(
                ContractorContractErrors.ProjectOperationDetailServiceIdsIsEmpty);
        }

        var poResponse =
            await _mediator.Send(new GetPOsWithoutIncludeQuery(poIds), ct);

        if (poResponse.IsFailure)
            return Result.Failure<List<ContractorContract>>(
                poResponse.Error!);

        var listedPO = poResponse.Value!;

        // IsPriceList validation removed for OperationBased

        if (listedPO.Any(x => x.OperationInfo.BasePrice == 0))
            return Result.Failure<List<ContractorContract>>(
                ContractorContractErrors.BasePriceIsZero);

        foreach (var item in operationCCs)
        {
            var itemPoIds =
                item.Details.Listed(x => x.ProjectOperationId);

            var projectOperations = listedPO
    .Where(x => itemPoIds.Contains(x.Id))
    .ToList();

            var servicesResult =
                await GetOperationBasedServicesForContract(
                    project,
                    contractorId,
                    item.ProjectOperationDetailServiceIds!,
                    itemPoIds.ToHashSet(),
                    ct);

            if (servicesResult.IsFailure)
            {
                return Result.Failure<List<ContractorContract>>(
                    servicesResult.Error!);
            }

            var selectedServices = servicesResult.Value!;

            var workLoadsByProjectOperation =
                selectedServices
                    .GroupBy(x =>
                        x.ProjectOperationDetail.ProjectOperation.Id)
                    .ToDictionary(
                        x => x.Key,
                        x => x.Sum(s => s.Volume));

            if (itemPoIds.Any(poId =>
                    !workLoadsByProjectOperation.ContainsKey(poId)))
            {
                return Result.Failure<List<ContractorContract>>(
                    ContractorContractErrors.InValidProjectOperationServiceId);
            }

            var totalPrice =
                CalculateOperationBasedTotal(
                    projectOperations,
                    item.Details,
                    workLoadsByProjectOperation);

            var responseCreate = await _mediator.Send(
                new CreateContractorContractCommand(
                    project.CompanyId!.Value,
                    value,
                    ContractorContractType.OperationBased,
                    project,
                    item.StartDate,
                    item.EndDate,
                    totalPrice,
                    item.PercentageDoingJobWell,
                    item.DoingJobWellAmount,
                    item.PercentageAdvancePayment,
                    item.AdvancePaymentAmount,
                    item.DailyLatenessPenalty,
                    0,
                    0,
                    0,
                    0,
                    0,
                    item.Description),
                ct);

            if (responseCreate.IsFailure)
                return Result.Failure<List<ContractorContract>>(
                    responseCreate.Error!);

            var contractorContract = responseCreate.Value!;

            var createDetails = await CreateOperationBasedDetails(
                contractorContract,
                projectOperations,
                item.Details,
                workLoadsByProjectOperation,
                ct);

            if (createDetails.IsFailure)
                return Result.Failure<List<ContractorContract>>(
                    createDetails.Error!);

            var linkResult =
                await LinkOperationBasedServicesToContractDetails(
                    createDetails.Value!,
                    selectedServices,
                    ct);

            if (linkResult.IsFailure)
                return Result.Failure<List<ContractorContract>>(
                    linkResult.Error!);

            contractorContracts.Add(contractorContract);
        }

        return contractorContracts;
    }


    private async Task<Result<List<ContractorContract>?>> UpdateOperationBasedContracts(
        Project project,
        long contractorId,
        ContractorContractHeader value,
        List<UpdateItemPriceListCCRequest> itemPriceListContracts,
        CT ct)
    {
        var createPOIds = itemPriceListContracts
        .SelectMany(x => x.Details ?? [])
        .Listed(x => x.ProjectOperationId);

        var updatePOIds = itemPriceListContracts
            .SelectMany(x => x.UpdateDetails ?? [])
            .Listed(x => x.ProjectOperationId);

        var poIds = createPOIds
            .Concat(updatePOIds)
            .Distinct()
            .ToList();

        var listedPO = new List<ProjectOperation>();

        if (poIds.Count > 0)
        {
            var poResponse =
                await _mediator.Send(
                    new GetPOsWithoutIncludeQuery(poIds),
                    ct);

            if (poResponse.IsFailure)
                return Result.Failure<List<ContractorContract>>(
                    poResponse.Error!);

            listedPO = poResponse.Value ?? [];

            if (listedPO.Count != poIds.Count)
                return Result.Failure<List<ContractorContract>>(
                    ContractorContractErrors
                        .InValidProjectOperationServiceId);

            if (listedPO.Any(x =>
                    x.OperationInfo.BasePrice <= 0))
                return Result.Failure<List<ContractorContract>>(
                    ContractorContractErrors.BasePriceIsZero);
        }
        if (listedPO != null && listedPO.Any())
            if (listedPO.Any(x => x.OperationInfo.BasePrice == 0))
                return Result.Failure<List<ContractorContract>>(
                    ContractorContractErrors.BasePriceIsZero);

        List<ContractorContract> contractorContracts = new List<ContractorContract>();
        foreach (var item in itemPriceListContracts)
        {
            var responseGet = await _mediator.Send(new GetFixContractorContractByIdIncludelessQuery(item.Id, project.CompanyId!.Value), ct);
            if (responseGet.IsFailure)
                return Result.Failure<List<ContractorContract>>(responseGet.Error!);
            var contractValue = responseGet.Value!;

            if (contractValue.ContractorContractType != ContractorContractType.OperationBased ||
            contractValue.ContractorContractHeaderId != value.Id ||
            contractValue.ProjectId != project.Id)
                return Result.Failure<List<ContractorContract>>(
                    ContractorContractErrors
                        .InValidContractorContractId);

            if (item.IsDelete == false)
            {
                if (item.DeleteDetails is { Count: > 0 })
                {
                    foreach (var deleteDetailId in item.DeleteDetails)
                    {
                        var detailToDelete =
                            contractValue.Details
                                .FirstOrDefault(x =>
                                    x.Id == deleteDetailId &&
                                    !x.IsDeleted);

                        if (detailToDelete is null)
                            return Result.Failure<List<ContractorContract>>(
                                ContractorContractErrors
                                    .InValidProjectOperationServiceId);

                        var responseDelete =
                            await _mediator.Send(
                                new DeleteContractorContractDetailCommand(
                                    deleteDetailId),
                                ct);

                        if (responseDelete.IsFailure)
                            return Result.Failure<List<ContractorContract>>(
                                responseDelete.Error!);
                    }
                }

                if (item.UpdateDetails is { Count: > 0 })
                {
                    foreach (var update in item.UpdateDetails)
                    {
                        var detail =
                            contractValue.Details
                                .FirstOrDefault(x =>
                                    x.Id == update.Id &&
                                    !x.IsDeleted);

                        if (detail is null)
                            return Result.Failure<List<ContractorContract>>(
                                ContractorContractErrors
                                    .InValidProjectOperationServiceId);

                        if (detail.ProjectOperationId !=
                            update.ProjectOperationId)
                            return Result.Failure<List<ContractorContract>>(
                                ContractorContractErrors
                                    .InValidProjectOperationServiceId);

                        var projectOperation =
                            listedPO.FirstOrDefault(x =>
                                x.Id == update.ProjectOperationId);

                        if (projectOperation is null)
                            return Result.Failure<List<ContractorContract>>(
                                ContractorContractErrors
                                    .InValidProjectOperationServiceId);

                        var updateDetail =
                            await _mediator.Send(
                                new UpdateContractorContractDetailCommand(
                                    detail,
                                    projectOperation,
                                    contractValue.StartDate,
                                    contractValue.EndDate,
                                    null,
                                    null,
                                    update.ContractCoefficient),
                                ct);

                        if (updateDetail.IsFailure)
                            return Result.Failure<List<ContractorContract>>(
                                updateDetail.Error!);
                    }
                }

                if (item.Details is { Count: > 0 })
                {
                    if (item.ProjectOperationDetailServiceIds is null ||
                        item.ProjectOperationDetailServiceIds.Count == 0)
                        return Result.Failure<List<ContractorContract>>(
                             ContractorContractErrors
                            .ProjectOperationDetailServiceIdsIsEmpty);

                    var itemCreatePOIds =
                        item.Details
                            .Listed(x => x.ProjectOperationId);

                    var projectOperations =
                        listedPO
                            .Where(x =>
                                itemCreatePOIds.Contains(x.Id))
                            .ToList();

                    if (projectOperations.Count !=
                        itemCreatePOIds.Count)
                        return Result.Failure<List<ContractorContract>>(
                            ContractorContractErrors
                                .InValidProjectOperationServiceId);

                    var servicesResult =
                        await GetOperationBasedServicesForContract(
                            project,
                            contractorId,
                            item.ProjectOperationDetailServiceIds,
                            itemCreatePOIds.ToHashSet(),
                            ct);

                    if (servicesResult.IsFailure)
                        return Result.Failure<List<ContractorContract>>(
                            servicesResult.Error!);

                    var selectedServices =
                        servicesResult.Value!;

                    var workLoadsByProjectOperation =
                        selectedServices
                            .GroupBy(x =>
                                x.ProjectOperationDetail
                                    .ProjectOperation.Id)
                            .ToDictionary(
                                x => x.Key,
                                x => x.Sum(s => s.Volume));

                    if (itemCreatePOIds.Any(poId =>
                            !workLoadsByProjectOperation
                                .ContainsKey(poId)))
                        return Result.Failure<List<ContractorContract>>(
                            ContractorContractErrors
                                .InValidProjectOperationServiceId);

                    var createDetails =
                        await CreateOperationBasedDetails(
                            contractValue,
                            projectOperations,
                            item.Details,
                            workLoadsByProjectOperation,
                            ct);

                    if (createDetails.IsFailure)
                        return Result.Failure<List<ContractorContract>>(
                            createDetails.Error!);

                    var linkResult =
                        await LinkOperationBasedServicesToContractDetails(
                            createDetails.Value!,
                            selectedServices,
                            ct);

                    if (linkResult.IsFailure)
                        return Result.Failure<List<ContractorContract>>(
                            linkResult.Error!);
                }

                var usedPOs = contractValue.Details.Select(x => x.ProjectOperation).ToList();

                var responseUpdate = await _mediator.Send(new UpdateContractorContractCommand(
                    contractValue, item.StartDate, item.EndDate, contractValue.TotalAmount, item.PercentageDoingJobWell,
                    item.DoingJobWellAmount, item.PercentageAdvancePayment, item.AdvancePaymentAmount,
                    item.DailyLatenessPenalty, 0, 0, 0, null, null, item.Description), ct);
                if (responseUpdate.IsFailure)
                    return Result.Failure<List<ContractorContract>>(responseUpdate.Error!);


                var deletedCommand = await _mediator.Send(new SetCCTotalAmountCommand(responseUpdate.Value!), ct);
                if (deletedCommand.IsFailure)
                    return Result.Failure<List<ContractorContract>>(deletedCommand.Error!);

            }

            if (item.IsDelete == true || contractValue.Details.All(z => z.IsDeleted == true))
            {
                var deletedCommand = await _mediator.Send(new DeleteContractorContractCommand(item.Id, project.CompanyId!.Value), ct);
                if (deletedCommand.IsFailure)
                    return Result.Failure<List<ContractorContract>>(deletedCommand.Error!);
            }

        }
        return contractorContracts;
    }

    private async Task<Result<List<ContractorContractDetail>?>>
    CreateOperationBasedDetails(
        ContractorContract cContract,
        List<ProjectOperation> projectOperations,
        List<CreatePoCCModel> operationCCs,
        IReadOnlyDictionary<long, decimal> workLoadsByProjectOperation,
        CT ct)
    {
        var details = new List<ContractorContractDetail>();

        foreach (var item in operationCCs)
        {
            var pOperation =
                projectOperations.FirstOrDefault(x =>
                    x.Id == item.ProjectOperationId);

            if (pOperation is null)
            {
                return Result.Failure<List<ContractorContractDetail>>(
                    ContractorContractErrors.InValidProjectOperationServiceId);
            }

            if (!workLoadsByProjectOperation.TryGetValue(
                    item.ProjectOperationId,
                    out var workLoad) ||
                workLoad <= 0)
            {
                return Result.Failure<List<ContractorContractDetail>>(
                    ContractorContractErrors.InValidProjectOperationServiceId);
            }

            var createDetailCommand =
                await _mediator.Send(
                    new CreateContractorContractDetailCommand(
                        cContract,
                        pOperation,
                        null,
                        null,
                        workLoad,
                        pOperation.OperationInfo.BasePrice,
                        item.ContractCoefficient),
                    ct);

            if (createDetailCommand.IsFailure)
            {
                return Result.Failure<List<ContractorContractDetail>>(
                    createDetailCommand.Error!);
            }

            details.Add(createDetailCommand.Value!);
        }

        return details;
    }

    private decimal CalculateOperationBasedTotal(
    List<ProjectOperation> projectOperations,
    List<CreatePoCCModel> operationCCs,
    IReadOnlyDictionary<long, decimal> workLoadsByProjectOperation)
    {
        ArgumentNullException.ThrowIfNull(projectOperations);
        ArgumentNullException.ThrowIfNull(operationCCs);
        ArgumentNullException.ThrowIfNull(workLoadsByProjectOperation);

        var poDictionary = projectOperations
            .Where(x => x.OperationInfo is not null)
            .ToDictionary(
                x => x.Id,
                x => x.OperationInfo.BasePrice);

        decimal totalPrice = 0;

        foreach (var item in operationCCs)
        {
            if (!poDictionary.TryGetValue(
                    item.ProjectOperationId,
                    out var unitPrice))
            {
                throw new InvalidOperationException(
                    $"شرح عملیات پروژه با شناسه {item.ProjectOperationId} پیدا نشد.");
            }

            if (!workLoadsByProjectOperation.TryGetValue(
                    item.ProjectOperationId,
                    out var workLoad))
            {
                throw new InvalidOperationException(
                    $"حجم قرارداد شرح عملیات پروژه با شناسه {item.ProjectOperationId} پیدا نشد.");
            }

            totalPrice +=
                unitPrice *
                item.ContractCoefficient *
                workLoad;
        }

        return totalPrice;
    }

    private async Task<Result<bool>> LinkOperationBasedServicesToContractDetails(
        List<ContractorContractDetail> contractDetails,
        List<ProjectOperationDetailContractorService> services,
        CT ct)
    {
        foreach (var service in services)
        {
            var projectOperationId =
                service.ProjectOperationDetail
                    .ProjectOperation.Id;

            var contractDetail =
                contractDetails.FirstOrDefault(x =>
                    x.ProjectOperationId ==
                    projectOperationId);

            if (contractDetail is null)
            {
                return Result.Failure<bool>(
                    ContractorContractErrors
                        .InValidProjectOperationServiceId);
            }

            var createDetailServiceResponse =
                await _mediator.Send(
                    new CreateContractorContractDetailServiceCommand(
                        contractDetail,
                        service),
                    ct);

            if (createDetailServiceResponse.IsFailure)
            {
                return Result.Failure<bool>(
                    createDetailServiceResponse.Error!);
            }
        }

        return true;
    }

    private async Task<Result<List<ProjectOperationDetailContractorService>?>>
    GetOperationBasedServicesForContract(
        Project project,
        long contractorId,
        List<long> projectOperationDetailServiceIds,
        HashSet<long> allowedProjectOperationIds,
        CT ct)
    {
        var ids = projectOperationDetailServiceIds
            .Distinct()
            .ToList();

        if (ids.Count == 0)
        {
            return Result.Failure<
                List<ProjectOperationDetailContractorService>>(
                ContractorContractErrors
                    .ProjectOperationDetailServiceIdsIsEmpty);
        }

        if (ids.Count != projectOperationDetailServiceIds.Count)
        {
            return Result.Failure<
                List<ProjectOperationDetailContractorService>>(
                ContractorContractErrors
                    .InValidProjectOperationServiceId);
        }

        var servicesResponse = await _mediator.Send(
            new GetsRequestedOperationContractQuery(
                ids,
                project.CompanyId),
            ct);

        if (servicesResponse.IsFailure ||
            servicesResponse.Value?.Data is null)
        {
            return Result.Failure<
                List<ProjectOperationDetailContractorService>>(
                ContractorContractErrors
                    .InValidProjectOperationServiceId);
        }

        var services = servicesResponse.Value.Data;

        if (services.Count != ids.Count)
        {
            return Result.Failure<
                List<ProjectOperationDetailContractorService>>(
                ContractorContractErrors
                    .InValidProjectOperationServiceId);
        }

        if (services.Any(x =>
                x.Type !=
                PODContractorServiceType.OperationBased))
        {
            return Result.Failure<
                List<ProjectOperationDetailContractorService>>(
                ContractorContractErrors
                    .InValidProjectOperationServiceId);
        }

        if (services.Any(x =>
                x.ContractorId != contractorId))
        {
            return Result.Failure<
                List<ProjectOperationDetailContractorService>>(
                ContractorContractErrors
                    .InValidContractorIds);
        }

        if (services.Any(x =>
                x.Status != ContractorServiceStatus.New ||
                x.ContractorContractDetailServices
                    .Any(c => !c.IsDeleted)))
        {
            return Result.Failure<
                List<ProjectOperationDetailContractorService>>(
                ContractorContractErrors
                    .InValidProjectOperationServiceId);
        }

        if (services.Any(x =>
                x.ProjectOperationDetail
                    .ProjectOperation.ProjectId != project.Id))
        {
            return Result.Failure<
                List<ProjectOperationDetailContractorService>>(
                ContractorContractErrors
                    .InValidProjectOperationServiceId);
        }

        if (services.Any(x =>
                !allowedProjectOperationIds.Contains(
                    x.ProjectOperationDetail
                        .ProjectOperation.Id)))
        {
            return Result.Failure<
                List<ProjectOperationDetailContractorService>>(
                ContractorContractErrors
                    .InValidProjectOperationServiceId);
        }

        return services;
    }
}
