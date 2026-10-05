using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCCThirdParties;
using Engineering.Application.Services.ContractorMachineries.Queries.GetFltrByContractorIds;
using Engineering.Application.WebServices.MetaDataServices.ThirdPartySkills.Queries.GetThirdPartiesSkills;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetCCThirdParties;

public class GetCCThirdPartiesQueryHandler : IQueryHandler<GetCCThirdPartiesQuery, GetCCThirdPartiesResponse?>
{
    private readonly ILogger<GetCCThirdPartiesQueryHandler> _logger;
    private readonly IContractorContractRepository _repository;
    private readonly IContractorEmployeeRepository _employeeRepo;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;
    private readonly IMediator _mediator;

    public GetCCThirdPartiesQueryHandler(
        ILogger<GetCCThirdPartiesQueryHandler> logger,
        IContractorContractRepository repository,
        IContractorEmployeeRepository employeeRepo,
        IViewThirdPartyRepository thirdPartyRepo,
        IMediator mediator)
    {
        _logger = logger;
        _repository = repository;
        _employeeRepo = employeeRepo;
        _thirdPartyRepo = thirdPartyRepo;
        _mediator = mediator;
    }

    public async Task<Result<GetCCThirdPartiesResponse?>> Handle(GetCCThirdPartiesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCCThirdParties(request.ProjectId, request.ContractorIds, request.PageIndex, request.PageSize, request.CompanyId, ct);
            if (result.RowCount < 1 || result.Data is null)
                return Result.Failure<GetCCThirdPartiesResponse>(ContractorContractErrors.NoThirdPartyFound);
            var contractorIds = result.Data.NullListed(x => x.ContractorId);
            var employees = await _employeeRepo.GetThirdPartiesByContractorId(contractorIds, ct);
            var employeeIds = employees.Listed(x => x.EmployeeId);

            var userIds = contractorIds
                .Concat(employeeIds)
                .Listed(x => x);

            var skills = await _mediator.Send(new GetThirdPartiesSkillsQuery(employeeIds), ct);
            var machineries = await _mediator.Send(new GetFltrByContractorIdsQuery(contractorIds), ct);
            var machineValue = !machineries.IsBad() ? machineries.Value!.Data : null;
            List<GetCCThirdPartiesModel> response = [];
            if (userIds is not null && userIds.Count > 0)
            {
                var users = await _thirdPartyRepo.GetByIds(userIds, ct);
                foreach (var item in employees)
                {
                    var machines = machineValue?.Where(x => x.ContractorId == item.ContractorId);
                    var contract = result.Data.FirstOrDefault(x => x.ContractorId == item.ContractorId);
                    var contractor = users.FirstOrDefault(x => x.Id == item.ContractorId);
                    var thirdParty = users.FirstOrDefault(x => x.Id == item.EmployeeId);
                    string? skill = string.Empty;
                    if (!skills.IsBad())
                    {
                        var thirdPartyskill = skills.Value!.Data!.Where(x => x.ThirdPartyId == item.EmployeeId).Listed(x => x.Skill);
                        skill = thirdPartyskill.JoinListDisc();
                    }

                    response.Add(new GetCCThirdPartiesModel
                    {
                        Id = item.Id,
                        ThirdPartyId = thirdParty?.Id,
                        ThirdParty = thirdParty?.FirstName + " " + thirdParty?.LastName,
                        ContractorId = contractor?.Id,
                        Contractor = contractor?.FirstName + " " + contractor?.LastName,
                        ContractRequestNumber = contract?.ContractRequestNumber,
                        Skill = skill,
                        Machineries = machines?.Select(x => x.MachineryName).JoinList(),
                        StartDate = contract?.StartDate,
                        EndDate = contract?.EndDate
                    });
                }
            }

            return result.Data.Any() ?
                new GetCCThirdPartiesResponse
                (
                    response,
                    result.RowCount
                ) : Result.Failure<GetCCThirdPartiesResponse?>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetCCThirdPartiesResponse?>(SharedErrors.UnknownError);
        }
    }
}
