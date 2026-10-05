using Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.CreateContractorContract;
using Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.DeleteContractorContract;
using Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.UpdateContractorContract;
using Engineering.Application.Services.ContractorContracts.Commands.Details.CreateContractorContractDetail;
using Engineering.Application.Services.ContractorContracts.Commands.Details.DeleteContractorContractDetail;
using Engineering.Application.Services.ContractorContracts.Commands.Details.DeleteContractorContractDetailService;
using Engineering.Application.Services.ContractorContracts.Contracts.CreateContractorContract;
using Engineering.Application.Services.ContractorContracts.Contracts.UpdateContractorContract;
using Engineering.Application.Services.ContractorContracts.Queries.GetFixContractorContractByIdIncludeless;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetsProjectOperationDetailContractorService;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.ContractorContracts;

public partial class ContractorContractLogic : IContractorContractLogic
{
    private async Task<Result<List<ContractorContract>?>> CreateFixContractors(
        Project project,
        long contractorId,
        ContractorContractHeader value,
        List<CreateFixCCRequest> fixContractors,
        CT ct)
    {
        List<ContractorContract> contractorContracts = new List<ContractorContract>();
        foreach (var item in fixContractors)
        {
            var responseCreate = await _mediator.Send(new CreateContractorContractCommand(project.CompanyId!.Value, value, ContractorContractType.Fixed, project, item.StartDate, item.EndDate,
                item.TotalAmount, item.PercentageDoingJobWell, item.DoingJobWellAmount, item.PercentageAdvancePayment, item.AdvancePaymentAmount,
                item.DailyLatenessPenalty, 0, 0, 0, 0, 0, item.Description), ct);
            if (responseCreate.IsFailure)
                return Result.Failure<List<ContractorContract>>(responseCreate.Error!);
            var contractorContract = responseCreate.Value!;

            if (item.ServiceIds is not null && item.ServiceIds.Count > 0)
                await AddNewServicesToFixContractorContract(contractorContract, project.Id, contractorId, project.CompanyId, item.ServiceIds, ct);

            if (item.ProjectServiceIds is not null && item.ProjectServiceIds.Count > 0)
                await AddNewProjectServiceToFixContractorContract(contractorContract, project.Id, contractorId, project.CompanyId, item.ProjectServiceIds, ct);

            if (item.ProjectOperationDetailServiceIds is not null && item.ProjectOperationDetailServiceIds.Count > 0)
                await AddNewProjectOperationDetailServiceToFixContractorContract(contractorContract, project.Id, contractorId, project.CompanyId, item.ProjectOperationDetailServiceIds, ct);

            if (item.CreateCostOvers is not null && item.CreateCostOvers.Count > 0)
            {
                var costOvers = await CreateContractorContractDetailCostOvers(contractorContract, null, item.CreateCostOvers, ct);
                if (costOvers.IsFailure)
                    return Result.Failure<List<ContractorContract>>(costOvers.Error!);
            }

            contractorContracts.Add(contractorContract);
        }
        return contractorContracts;
    }

    private async Task<Result<List<ContractorContract>?>> UpdateFixContractors(
        long projectId,
        long contractorId,
        ContractorContractHeader value,
        List<UpdateFixCCRequest> fixContractors,
        long companyId,
        CT ct)
    {
        List<ContractorContract> contractorContracts = new List<ContractorContract>();
        foreach (var item in fixContractors)
        {
            if (item.IsDelete == true)
            {
                var deletedCommand = await _mediator.Send(new DeleteContractorContractCommand(item.Id, companyId), ct);
                if (deletedCommand.IsFailure)
                    return Result.Failure<List<ContractorContract>>(deletedCommand.Error!);
            }

            if (item.IsDelete == false)
            {
                var responseGet = await _mediator.Send(new GetFixContractorContractByIdIncludelessQuery(item.Id, companyId), ct);
                if (responseGet.IsFailure)
                    return Result.Failure<List<ContractorContract>>(responseGet.Error!);
                var contractValue = responseGet.Value!;

                var responseUpdate = await _mediator.Send(new UpdateContractorContractCommand(contractValue, item.StartDate, item.EndDate, item.TotalAmount, item.PercentageDoingJobWell,
                    item.DoingJobWellAmount, item.PercentageAdvancePayment, item.AdvancePaymentAmount, item.DailyLatenessPenalty, 0, 0, 0, null, null, item.Description), ct);
                if (responseUpdate.IsFailure)
                    return Result.Failure<List<ContractorContract>>(responseUpdate.Error!);

                if (item.DeleteContractorContractDetailIds is not null && item.DeleteContractorContractDetailIds.Count > 0)
                    foreach (var deteteItem in item.DeleteContractorContractDetailIds)
                    {
                        var responseDetele = await _mediator.Send(new DeleteContractorContractDetailCommand(deteteItem), ct);
                        if (responseDetele.IsFailure)
                            return Result.Failure<List<ContractorContract>>(responseDetele.Error!);
                    }

                if (item.DeleteContractorContractDetailServiceIds is not null && item.DeleteContractorContractDetailServiceIds.Count > 0)
                    foreach (var deteteItem in item.DeleteContractorContractDetailServiceIds)
                    {
                        var responseDetele = await _mediator.Send(new DeleteContractorContractDetailServiceCommand(deteteItem), ct);
                        if (responseDetele.IsFailure)
                            return Result.Failure<List<ContractorContract>>(responseDetele.Error!);
                    }

                var contractorContract = responseUpdate.Value!;
                if (item.NewDetailServiceModels is not null && item.NewDetailServiceModels.Any())
                    foreach (var newService in item.NewDetailServiceModels)
                    {
                        if (newService.ServiceIds is not null && newService.ServiceIds.Count > 0)
                            await AddNewServicesToFixContractorContract(contractorContract, projectId, contractorId, contractorContract.ContractorContractHeader.CompanyId, newService.ServiceIds, ct);

                        if (newService.ProjectServiceIds is not null && newService.ProjectServiceIds.Count > 0)
                            await AddNewProjectServiceToFixContractorContract(contractorContract, projectId, contractorId, contractorContract.ContractorContractHeader.CompanyId, newService.ProjectServiceIds, ct);

                        if (newService.ProjectOperationDetailServiceIds is not null && newService.ProjectOperationDetailServiceIds.Count > 0)
                            await AddNewProjectOperationDetailServiceToFixContractorContract(contractorContract, projectId, contractorId, contractorContract.ContractorContractHeader.CompanyId, newService.ProjectOperationDetailServiceIds, ct);
                    }

                if (contractValue.Details.All(z => z.IsDeleted == true))
                {
                    var deletedCommand = await _mediator.Send(new DeleteContractorContractCommand(contractValue.Id, companyId), ct);
                    if (deletedCommand.IsFailure)
                        return Result.Failure<List<ContractorContract>>(deletedCommand.Error!);
                }

                if (item.UpdateCostOvers is not null && item.UpdateCostOvers.Count > 0)
                {
                    var costOvers = await UpdateContractorContractDetailCostOvers(contractorContract, null, item.UpdateCostOvers, ct);
                    if (costOvers.IsFailure)
                        return Result.Failure<List<ContractorContract>>(costOvers.Error!);
                }

                if (item.DeleteContractorContractDetailCostOverIds is not null && item.DeleteContractorContractDetailCostOverIds.Count > 0)
                {
                    var costOvers = await DeleteContractorContractDetailCostOvers(item.DeleteContractorContractDetailCostOverIds, ct);
                    if (costOvers.IsFailure)
                        return Result.Failure<List<ContractorContract>>(costOvers.Error!);
                }

                if (item.CreateCostOvers is not null && item.CreateCostOvers.Count > 0)
                {
                    var costOvers = await CreateContractorContractDetailCostOvers(contractorContract, null, item.CreateCostOvers, ct);
                    if (costOvers.IsFailure)
                        return Result.Failure<List<ContractorContract>>(costOvers.Error!);
                }
            }
        }
        return contractorContracts;
    }

    private async Task<Result<ContractorContractDetail?>> AddNewServicesToFixContractorContract(
        ContractorContract contractorContract,
        long projectId,
        long contractorId,
        long? companyId,
        List<long> serviceIds,
        CT ct)
    {
        var queryServices = await _mediator.Send(new GetsProjectOperationDetailContractorServiceQuery(
            serviceIds, null, null, projectId, contractorId, null, null, companyId, null, 0, 0), ct);
        if (queryServices.IsFailure)
            return Result.Failure<ContractorContractDetail>(queryServices.Error!);
        var services = queryServices.Value!.Data!.Where(x => x.Status == ContractorServiceStatus.New).ToList();

        var sumVolum = services!.Sum(x => x.Volume);
        var createDetailCommand = await _mediator.Send(new CreateContractorContractDetailCommand(contractorContract, null, null, null, sumVolum, null, 1), ct);
        if (createDetailCommand.IsFailure)
            return Result.Failure<ContractorContractDetail>(createDetailCommand.Error!);
        var createDetail = createDetailCommand.Value!;

        var detialServices = await CreateContractorContractDetailServices(createDetail, services, ct);
        if (detialServices.IsFailure)
            return Result.Failure<ContractorContractDetail>(detialServices.Error!);

        return createDetail;
    }

    private async Task<Result<ContractorContractDetail?>> AddNewProjectServiceToFixContractorContract(
        ContractorContract contractorContract,
        long projectId,
        long contractorId,
        long? companyId,
        List<long> projectServiceIds,
        CT ct)
    {
        var queryServices = await _mediator.Send(new GetsProjectOperationDetailContractorServiceQuery(
            null, projectServiceIds, null, projectId, contractorId, null, null, companyId, null, 0, 0), ct);
        if (queryServices.IsFailure)
            return Result.Failure<ContractorContractDetail>(queryServices.Error!);
        var services = queryServices.Value!.Data!.Where(x => x.Status == ContractorServiceStatus.New).ToList();

        var sumVolum = services!.FirstOrDefault()!.ProjectServiceDetail!.ProjectService.Volume;
        var createDetailCommand = await _mediator.Send(new CreateContractorContractDetailCommand(contractorContract, null, null, null, sumVolum, null, 1), ct);
        if (createDetailCommand.IsFailure)
            return Result.Failure<ContractorContractDetail>(createDetailCommand.Error!);
        var createDetail = createDetailCommand.Value!;

        var detialServices = await CreateContractorContractDetailServices(createDetail, services, ct);
        if (detialServices.IsFailure)
            return Result.Failure<ContractorContractDetail>(detialServices.Error!);

        return createDetail;
    }

    private async Task<Result<ContractorContractDetail?>> AddNewProjectOperationDetailServiceToFixContractorContract(
        ContractorContract contractorContract,
        long projectId,
        long contractorId,
        long? companyId,
        List<long> projectOperationDetailServiceIds,
        CT ct)
    {
        var queryServices = await _mediator.Send(new GetsProjectOperationDetailContractorServiceQuery(
        null, null, projectOperationDetailServiceIds, projectId, contractorId, null, null, companyId, null, 0, 0), ct);
        if (queryServices.IsFailure)
            return Result.Failure<ContractorContractDetail>(queryServices.Error!);
        var services = queryServices.Value!.Data!.Where(x => x.Status == ContractorServiceStatus.New).ToList();
        if (services is null || services.Count == 0)
            return Result.Failure<ContractorContractDetail>(ContractorContractErrors.ServiceInfoIsEmpty);

        var sumVolum = services!.Sum(x => x.Volume);
        var createDetailCommand = await _mediator.Send(new CreateContractorContractDetailCommand(contractorContract, null, null, null, sumVolum, null, 1), ct);
        if (createDetailCommand.IsFailure)
            return Result.Failure<ContractorContractDetail>(createDetailCommand.Error!);
        var createDetail = createDetailCommand.Value!;

        var detialServices = await CreateContractorContractDetailServices(createDetail, services, ct);
        if (detialServices.IsFailure)
            return Result.Failure<ContractorContractDetail>(detialServices.Error!);

        return createDetail;
    }
}
