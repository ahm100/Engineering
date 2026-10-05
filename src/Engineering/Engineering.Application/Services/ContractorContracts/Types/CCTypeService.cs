using Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.CreateContractorContract;
using Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.DeleteContractorContract;
using Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.UpdateContractorContract;
using Engineering.Application.Services.ContractorContracts.Commands.Details.CreateContractorContractDetail;
using Engineering.Application.Services.ContractorContracts.Commands.Details.CreateContractorContractDetailCostOver;
using Engineering.Application.Services.ContractorContracts.Commands.Details.CreateContractorContractDetailPrice;
using Engineering.Application.Services.ContractorContracts.Commands.Details.CreateContractorContractDetailService;
using Engineering.Application.Services.ContractorContracts.Commands.Details.DeleteContractorContractDetail;
using Engineering.Application.Services.ContractorContracts.Commands.Details.DeleteContractorContractDetailCostOver;
using Engineering.Application.Services.ContractorContracts.Commands.Details.DeleteContractorContractDetailPrice;
using Engineering.Application.Services.ContractorContracts.Commands.Details.DeleteContractorContractDetailService;
using Engineering.Application.Services.ContractorContracts.Commands.Details.UpdateContractorContractDetailCostOver;
using Engineering.Application.Services.ContractorContracts.Commands.Details.UpdateContractorContractDetailPrice;
using Engineering.Application.Services.ContractorContracts.Contracts.CreateContractorContract;
using Engineering.Application.Services.ContractorContracts.Contracts.UpdateContractorContract;
using Engineering.Application.Services.ContractorContracts.Queries.GetServiceContractorContractByIdIncludeless;
using Engineering.Application.Services.CostOvers.Queries.GetsCostOversByIds;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetsProjectOperationDetailContractorService;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.ContractorContracts;

public partial class ContractorContractLogic : IContractorContractLogic
{
    private async Task<Result<List<ContractorContract>?>> CreateServiceContracts(
        Project project,
        long contractorId,
        ContractorContractHeader value,
        List<CreateServiceCCRequest> serviceContractors,
        CT ct)
    {
        List<ContractorContract> contractorContracts = new List<ContractorContract>();
        foreach (var item in serviceContractors)
        {
            var responseCreate = await _mediator.Send(new CreateContractorContractCommand(project.CompanyId!.Value, value, ContractorContractType.Service, project, item.StartDate, item.EndDate, item.TotalAmount,
                item.PercentageDoingJobWell, item.DoingJobWellAmount, item.PercentageAdvancePayment, item.AdvancePaymentAmount, item.DailyLatenessPenalty, 0,
                0, 0, 0, 0, item.Description), ct);
            if (responseCreate.IsFailure)
                return Result.Failure<List<ContractorContract>>(responseCreate.Error!);
            var contractorContract = responseCreate.Value!;

            var createDetails = await AddNewServicesToServiceContractor(contractorContract, project.Id, contractorId, project.CompanyId, item.Details, ct);
            if (createDetails.IsFailure)
                return Result.Failure<List<ContractorContract>>(createDetails.Error!);
        }
        return contractorContracts;
    }

    private async Task<Result<List<ContractorContract>?>> UpdateServiceContracts(
        long projectId,
        long contractorId,
        ContractorContractHeader value,
        List<UpdateServiceCCRequest> updateServiceContractors,
        long companyId,
        CT ct)
    {
        List<ContractorContract> contractorContracts = new List<ContractorContract>();
        foreach (var item in updateServiceContractors)
        {
            if (item.IsDelete == true)
            {
                var deletedCommand = await _mediator.Send(new DeleteContractorContractCommand(item.Id, companyId), ct);
                if (deletedCommand.IsFailure)
                    return Result.Failure<List<ContractorContract>>(deletedCommand.Error!);
            }

            if (item.IsDelete == false)
            {
                var responseGet = await _mediator.Send(new GetServiceContractorContractByIdIncludelessQuery(item.Id, companyId), ct);
                if (responseGet.IsFailure)
                    return Result.Failure<List<ContractorContract>>(responseGet.Error!);
                var contractorContract = responseGet.Value!;

                if (item.UpdateDetails is not null)
                {
                    foreach (var detail in item.UpdateDetails)
                    {
                        var allPrices = (detail.UpdatePrices ?? new())
                            .Select(x => new { x.StartDate, x.EndDate })
                            .Concat(
                                (detail.CreatePrices ?? new())
                                .Select(x => new { x.StartDate, x.EndDate })
                            )
                            .ToList();

                        var hasOverlap = allPrices
                            .SelectMany((x, i) => allPrices.Skip(i + 1),
                                (x, y) => new { x, y })
                            .Any(p =>
                                p.x.StartDate <= p.y.EndDate &&
                                p.x.EndDate >= p.y.StartDate);

                        if (hasOverlap)
                            return Result.Failure<List<ContractorContract>>(ContractorContractErrors.TwoPriceHaveOverlap);
                    }
                }

                if (item.CreateDetails is not null)
                {
                    foreach (var detail in item.CreateDetails)
                    {
                        var prices = detail.Prices ?? new();

                        // Optional but IMPORTANT sanity check
                        if (prices.Any(x => x.StartDate >= x.EndDate))
                            throw new Exception("StartDate must be before EndDate.");

                        var hasOverlap = prices
                            .SelectMany((x, i) => prices.Skip(i + 1),
                                (x, y) => new { x, y })
                            .Any(p =>
                                p.x.StartDate <= p.y.EndDate &&
                                p.x.EndDate >= p.y.StartDate);

                        if (hasOverlap)
                            return Result.Failure<List<ContractorContract>>(ContractorContractErrors.TwoPriceHaveOverlap);

                    }
                }

                var updateContractorContract = await _mediator.Send(new UpdateContractorContractCommand(contractorContract, item.StartDate, item.EndDate, item.TotalAmount, item.PercentageDoingJobWell,
                    item.DoingJobWellAmount, item.PercentageAdvancePayment, item.AdvancePaymentAmount, item.DailyLatenessPenalty, 0, 0, 0, null, null, item.Description), ct);
                if (updateContractorContract.IsFailure)
                    return Result.Failure<List<ContractorContract>>(updateContractorContract.Error!);

                if (item.CreateDetails is not null && item.CreateDetails.Count > 0)
                {
                    var createDetails = await AddNewServicesToServiceContractor(
                        contractorContract, projectId, contractorId, contractorContract.ContractorContractHeader.CompanyId, item.CreateDetails, ct);
                    if (createDetails.IsFailure)
                        return Result.Failure<List<ContractorContract>>(createDetails.Error!);
                }

                if (item.UpdateDetails is not null && item.UpdateDetails.Count > 0)
                    foreach (var detail in item.UpdateDetails)
                    {
                        if (detail.IsDelete == true)
                        {
                            var responseDetele = await _mediator.Send(new DeleteContractorContractDetailCommand(detail.ContractorContractDetailId), ct);
                            if (responseDetele.IsFailure)
                                return Result.Failure<List<ContractorContract>>(responseDetele.Error!);
                        }

                        if (detail.IsDelete == false)
                        {
                            if (detail.DeleteContractorContractDetailServiceIds is not null && detail.DeleteContractorContractDetailServiceIds.Count > 0)
                                foreach (var deteteItem in detail.DeleteContractorContractDetailServiceIds)
                                {
                                    var responseDetele = await _mediator.Send(new DeleteContractorContractDetailServiceCommand(deteteItem), ct);
                                    if (responseDetele.IsFailure)
                                        return Result.Failure<List<ContractorContract>>(responseDetele.Error!);
                                }

                            if (detail.DeleteContractorContractDetailPriceIds is not null && detail.DeleteContractorContractDetailPriceIds.Count > 0)
                            {
                                var prices = await DeleteContractorContractDetailPrices(detail.DeleteContractorContractDetailPriceIds, ct);
                                if (prices.IsFailure)
                                    return Result.Failure<List<ContractorContract>>(prices.Error!);
                            }

                            var contractDetail = contractorContract.Details.Where(c => c.Id.Equals(detail.ContractorContractDetailId)).SingleOrDefault();
                            if (contractDetail is null)
                                return Result.Failure<List<ContractorContract>>(ContractorContractDetailErrors.InvalidContractorContractDetailId);

                            if (detail.UpdatePrices is not null && detail.UpdatePrices.Count > 0)
                            {
                                var prices = await UpdateContractorContractDetailPrices(contractorContract, contractDetail, detail.UpdatePrices, ct);
                                if (prices.IsFailure)
                                    return Result.Failure<List<ContractorContract>>(prices.Error!);
                            }

                            if (detail.CreatePrices is not null && detail.CreatePrices.Count > 0)
                            {
                                var prices = await CreateContractorContractDetailPrices(contractorContract, contractDetail, detail.CreatePrices, ct);
                                if (prices.IsFailure)
                                    return Result.Failure<List<ContractorContract>>(prices.Error!);
                            }

                            if (detail.DeleteContractorContractDetailCostOverIds is not null && detail.DeleteContractorContractDetailCostOverIds.Count > 0)
                            {
                                var costOvers = await DeleteContractorContractDetailCostOvers(detail.DeleteContractorContractDetailCostOverIds, ct);
                                if (costOvers.IsFailure)
                                    return Result.Failure<List<ContractorContract>>(costOvers.Error!);
                            }

                            if (detail.UpdateCostOvers is not null && detail.UpdateCostOvers.Count > 0)
                            {
                                var costOvers = await UpdateContractorContractDetailCostOvers(null, contractDetail, detail.UpdateCostOvers, ct);
                                if (costOvers.IsFailure)
                                    return Result.Failure<List<ContractorContract>>(costOvers.Error!);
                            }

                            if (detail.CreateCostOvers is not null && detail.CreateCostOvers.Count > 0)
                            {
                                var costOvers = await CreateContractorContractDetailCostOvers(null, contractDetail, detail.CreateCostOvers, ct);
                                if (costOvers.IsFailure)
                                    return Result.Failure<List<ContractorContract>>(costOvers.Error!);
                            }

                            contractDetail.SetStartDate(detail.StartDate);
                            contractDetail.SetEndDate(detail.EndDate);
                            contractDetail.SetTotalAmountClc();
                            contractDetail.SetTotalCostOveredAmountClc();
                        }
                    }

                contractorContract.SetTotalAmountClc();

                if (contractorContract.Details.All(z => z.IsDeleted == true))
                {
                    var deletedCommand = await _mediator.Send(new DeleteContractorContractCommand(contractorContract.Id, companyId), ct);
                    if (deletedCommand.IsFailure)
                        return Result.Failure<List<ContractorContract>>(deletedCommand.Error!);
                }
            }
        }
        return contractorContracts;
    }

    private async Task<Result<List<ContractorContractDetail>?>> AddNewServicesToServiceContractor(
        ContractorContract contractorContract,
        long projectId,
        long contractorId,
        long? companyId,
        List<CreateServiceContractorContractModel> details,
        CT ct)
    {
        var newDetails = new List<ContractorContractDetail>();
        foreach (var detail in details)
        {
            List<ProjectOperationDetailContractorService>? services = null;
            decimal sumVolum = 0;
            if (detail.ServiceId is not null && detail.ServiceId > 0)
            {
                var queryServices = await _mediator.Send(new GetsProjectOperationDetailContractorServiceQuery(
                    [detail.ServiceId!.Value], null, null,
                    projectId, contractorId, null, null, companyId, null, 0, 0), ct);
                if (queryServices.IsFailure)
                    return Result.Failure<List<ContractorContractDetail>>(queryServices.Error!);
                services = queryServices.Value!.Data!.Where(x => x.Status == ContractorServiceStatus.New).ToList();
                sumVolum = services!.Sum(x => x.Volume);
            }

            if (detail.ProjectServiceId is not null && detail.ProjectServiceId > 0)
            {
                var queryServices = await _mediator.Send(new GetsProjectOperationDetailContractorServiceQuery(
                    null, [detail.ProjectServiceId!.Value], null,
                    projectId, contractorId, null, null, companyId, null, 0, 0), ct);
                if (queryServices.IsFailure)
                    return Result.Failure<List<ContractorContractDetail>>(queryServices.Error!);
                services = queryServices.Value!.Data!.Where(x => x.Status == ContractorServiceStatus.New).ToList();
                sumVolum = services!.FirstOrDefault()!.ProjectServiceDetail!.ProjectService.Volume;
            }

            if (detail.ProjectOperationDetailServiceIds is not null && detail.ProjectOperationDetailServiceIds.Count > 0)
            {
                var queryServices = await _mediator.Send(new GetsProjectOperationDetailContractorServiceQuery(
                    null, null, detail.ProjectOperationDetailServiceIds,
                    projectId, contractorId, null, null, companyId, null, 0, 0), ct);
                if (queryServices.IsFailure)
                    return Result.Failure<List<ContractorContractDetail>>(queryServices.Error!);
                services = queryServices.Value!.Data!.Where(x => x.Status == ContractorServiceStatus.New).ToList();
                sumVolum = services!.Sum(x => x.Volume);
            }

            if (services is null || services.Count == 0)
                return Result.Failure<List<ContractorContractDetail>>(ContractorContractErrors.ServiceInfoIsEmpty);

            var firstPrice = detail.Prices.FirstOrDefault()!.Price;
            var createDetailCommand = await _mediator.Send(new CreateContractorContractDetailCommand(
                contractorContract, null, contractorContract.StartDate, contractorContract.EndDate, sumVolum, firstPrice, 1), ct);
            if (createDetailCommand.IsFailure)
                return Result.Failure<List<ContractorContractDetail>>(createDetailCommand.Error!);
            var createDetail = createDetailCommand.Value!;

            var prices = await CreateContractorContractDetailPrices(contractorContract, createDetail, detail.Prices, ct);
            if (prices.IsFailure)
                return Result.Failure<List<ContractorContractDetail>>(prices.Error!);

            if (detail.CreateCostOvers is not null && detail.CreateCostOvers.Count > 0)
            {
                var costOvers = await CreateContractorContractDetailCostOvers(null, createDetail, detail.CreateCostOvers, ct);
                if (costOvers.IsFailure)
                    return Result.Failure<List<ContractorContractDetail>>(costOvers.Error!);
            }

            var detialServices = await CreateContractorContractDetailServices(createDetail, services, ct);
            if (detialServices.IsFailure)
                return Result.Failure<List<ContractorContractDetail>>(detialServices.Error!);

            createDetail.SetTotalCostOveredAmountClc();
            newDetails.Add(createDetail);
        }

        return newDetails;
    }

    private async Task<Result<List<ContractorContractDetailPrice>?>> CreateContractorContractDetailPrices(
        ContractorContract contractorContract,
        ContractorContractDetail contractorContractDetail,
        List<CreateServiceContractorContractPriceModel> prices,
        CT ct)
    {
        var newPrices = new List<ContractorContractDetailPrice>();
        foreach (var price in prices)
        {
            var responseCreate = await _mediator.Send(new CreateContractorContractDetailPriceCommand(contractorContractDetail,
                price.StartDate, price.EndDate, price.Price, contractorContract.ContractorContractHeader.CurrencyId, price.IsActive), ct);
            if (responseCreate.IsFailure)
                return Result.Failure<List<ContractorContractDetailPrice>>(responseCreate.Error!);
            newPrices.Add(responseCreate.Value!);
        }
        return newPrices;
    }

    private async Task<Result<List<ContractorContractDetailPrice>?>> UpdateContractorContractDetailPrices(
        ContractorContract contractorContract,
        ContractorContractDetail contractorContractDetail,
        List<UpdateServiceContractorContractPriceModel> prices,
        CT ct)
    {
        var updatedPrices = new List<ContractorContractDetailPrice>();
        foreach (var price in prices)
        {
            var responseUpdate = await _mediator.Send(new UpdateContractorContractDetailPriceCommand(
                price.ContractorContractDetailPriceId, price.StartDate, price.EndDate, price.Price, contractorContract.ContractorContractHeader.CurrencyId, price.IsActive), ct);
            if (responseUpdate.IsFailure)
                return Result.Failure<List<ContractorContractDetailPrice>>(responseUpdate.Error!);
            updatedPrices.Add(responseUpdate.Value!);
        }
        return updatedPrices;
    }

    private async Task<Result<List<ContractorContractDetailPrice>?>> DeleteContractorContractDetailPrices(
        List<long> prices,
        CT ct)
    {
        var deletedPrices = new List<ContractorContractDetailPrice>();
        foreach (var detailPrice in prices)
        {
            var responseDelete = await _mediator.Send(new DeleteContractorContractDetailPriceCommand(detailPrice), ct);
            if (responseDelete.IsFailure)
                return Result.Failure<List<ContractorContractDetailPrice>>(responseDelete.Error!);
            deletedPrices.Add(responseDelete.Value!);
        }
        return deletedPrices;
    }

    private async Task<Result<List<ContractorContractDetailCostOver>?>> CreateContractorContractDetailCostOvers(
        ContractorContract? contractorContract,
        ContractorContractDetail? contractorContractDetail,
        List<CreateContractorContractDetailCostOverModel> costOverItems,
        CT ct)
    {
        var costOverIds = costOverItems.Select(x => x.CostOverId).ToList();
        var responseGets = await _mediator.Send(new GetsCostOversByIdsQuery(costOverIds), ct);
        if (responseGets.IsFailure)
            return Result.Failure<List<ContractorContractDetailCostOver>>(responseGets.Error!);
        var costOvers = responseGets.Value!.Data!;

        var newCostOvers = new List<ContractorContractDetailCostOver>();
        foreach (var item in costOverItems)
        {
            var costOver = costOvers.FirstOrDefault(x => x.Id == item.CostOverId);
            if (costOver is null)
                return Result.Failure<List<ContractorContractDetailCostOver>>(responseGets.Error!);

            var responseCreate = await _mediator.Send(new CreateContractorContractDetailCostOverCommand(contractorContract,
                contractorContractDetail, costOver, item.ContractorId, item.Percentage, item.Description), ct);
            if (responseCreate.IsFailure)
                return Result.Failure<List<ContractorContractDetailCostOver>>(responseCreate.Error!);
            newCostOvers.Add(responseCreate.Value!);
        }
        return newCostOvers;
    }

    private async Task<Result<List<ContractorContractDetailCostOver>?>> UpdateContractorContractDetailCostOvers(
        ContractorContract? contractorContract,
        ContractorContractDetail? contractorContractDetail,
        List<UpdateContractorContractDetailCostOverModel> costOverItems,
        CT ct)
    {
        var costOverIds = costOverItems.Select(x => x.CostOverId).ToList();
        var responseGets = await _mediator.Send(new GetsCostOversByIdsQuery(costOverIds), ct);
        if (responseGets.IsFailure)
            return Result.Failure<List<ContractorContractDetailCostOver>>(responseGets.Error!);
        var costOvers = responseGets.Value!.Data!;

        var updatedCostOvers = new List<ContractorContractDetailCostOver>();
        foreach (var item in costOverItems)
        {
            var costOver = costOvers.FirstOrDefault(x => x.Id == item.CostOverId);
            if (costOver is null)
                return Result.Failure<List<ContractorContractDetailCostOver>>(responseGets.Error!);

            var responseUpdate = await _mediator.Send(new UpdateContractorContractDetailCostOverCommand(
                item.ContractorContractDetailCostOverId, contractorContract, contractorContractDetail, costOver, item.ContractorId, item.Percentage, item.Description), ct);
            if (responseUpdate.IsFailure)
                return Result.Failure<List<ContractorContractDetailCostOver>>(responseUpdate.Error!);
            updatedCostOvers.Add(responseUpdate.Value!);
        }
        return updatedCostOvers;
    }

    private async Task<Result<List<ContractorContractDetailCostOver>?>> DeleteContractorContractDetailCostOvers(
        List<long> ids,
        CT ct)
    {
        var deletedCostOvers = new List<ContractorContractDetailCostOver>();
        foreach (var detailCostOver in ids)
        {
            var responseDelete = await _mediator.Send(new DeleteContractorContractDetailCostOverCommand(detailCostOver), ct);
            if (responseDelete.IsFailure)
                return Result.Failure<List<ContractorContractDetailCostOver>>(responseDelete.Error!);
            deletedCostOvers.Add(responseDelete.Value!);
        }
        return deletedCostOvers;
    }


    private async Task<Result<List<ContractorContractDetailService>?>> CreateContractorContractDetailServices(
        ContractorContractDetail contractorContractDetail,
        List<ProjectOperationDetailContractorService> services,
        CT ct)
    {
        var newServices = new List<ContractorContractDetailService>();
        foreach (var service in services)
        {
            var responseCreate = await _mediator.Send(new CreateContractorContractDetailServiceCommand(contractorContractDetail, service), ct);
            if (responseCreate.IsFailure)
                return Result.Failure<List<ContractorContractDetailService>>(responseCreate.Error!);
            newServices.Add(responseCreate.Value!);
        }
        return newServices;
    }

}
