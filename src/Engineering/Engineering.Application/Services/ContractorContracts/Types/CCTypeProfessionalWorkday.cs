using Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.CreateContractorContract;
using Engineering.Application.Services.ContractorContracts.Commands.Details.CreateContractorContractDetail;
using Engineering.Application.Services.ContractorContracts.Commands.Details.CreateContractorContractDetailService;
using Engineering.Application.Services.ContractorContracts.Contracts.CreateContractorContract;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetsProjectOperationDetailContractorService;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.ContractorContracts;

public partial class ContractorContractLogic : IContractorContractLogic
{
    private async Task<Result<List<ContractorContract>?>> CreateProfessionalWorkdayContracts(
        Project project,
        long contractorId,
        ContractorContractHeader value,
        List<CreateProfessionalWorkdayCCRequest> personContractors,
        CT ct)
    {
        List<ContractorContract> contractorContracts = new List<ContractorContract>();
        foreach (var item in personContractors)
        {
            var responseCreate = await _mediator.Send(new CreateContractorContractCommand(
                project.CompanyId!.Value, value, ContractorContractType.ProfessionalWorkday, project, item.StartDate, item.EndDate, item.TotalAmount, item.PercentageDoingJobWell, item.DoingJobWellAmount,
                item.PercentageAdvancePayment, item.AdvancePaymentAmount, item.DailyLatenessPenalty, null, null, null, item.DailyBaseHours, item.MonthlyBaseHours, item.Description), ct);
            if (responseCreate.IsFailure)
                return Result.Failure<List<ContractorContract>>(responseCreate.Error!);
            var createHeader = responseCreate.Value!;

            List<ProjectOperationDetailContractorService>? services = null;
            decimal sumVolum = 0;

            if (item.ProjectOperationDetailServiceIds is not null && item.ProjectOperationDetailServiceIds.Count > 0)
            {
                var queryServices = await _mediator.Send(new GetsProjectOperationDetailContractorServiceQuery(
                    null, null, item.ProjectOperationDetailServiceIds,
                    project.Id, contractorId, null, null, project.CompanyId, null, 0, 0), ct);
                if (queryServices.IsFailure)
                    return Result.Failure<List<ContractorContract>>(queryServices.Error!);
                services = queryServices.Value!.Data!.Where(x => x.Status == ContractorServiceStatus.New).ToList();
                sumVolum = services!.Sum(x => x.Volume);
            }

            if (services is null || services.Count == 0)
                return Result.Failure<List<ContractorContract>>(ContractorContractErrors.ServiceInfoIsEmpty);

            var createDetailCommand = await _mediator.Send(new CreateContractorContractDetailCommand(createHeader, null, null, null, sumVolum, null, 1), ct);
            if (createDetailCommand.IsFailure)
                return Result.Failure<List<ContractorContract>>(createDetailCommand.Error!);
            var createDetail = createDetailCommand.Value!;

            foreach (var service in services)
            {
                var commandDetailService = await _mediator.Send(new CreateContractorContractDetailServiceCommand(createDetail, service), ct);
                if (commandDetailService.IsFailure)
                    return Result.Failure<List<ContractorContract>>(commandDetailService.Error!);
            }
        }
        return contractorContracts;
    }

}
