using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.EmployerContracts;
using Engineering.Application.Abstractions.Data.EmployerEmployees;
using Engineering.Application.Services.EmployerEmployees.Contracts.GetECThirdParties;
using Engineering.Application.WebServices.MetaDataServices.ThirdPartySkills.Queries.GetThirdPartiesSkills;

namespace Engineering.Application.Services.EmployerEmployees.Queries.GetECThirdParties;

public class GetECThirdPartiesQueryHandler : IQueryHandler<GetECThirdPartiesQuery, GetECThirdPartiesResponse?>
{
    private readonly ILogger<GetECThirdPartiesQueryHandler> _logger;
    private readonly IEmployerContractRepository _repository;
    private readonly IEmployerEmployeeRepository _employeeRepo;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;
    private readonly IMediator _mediator;

    public GetECThirdPartiesQueryHandler(
        ILogger<GetECThirdPartiesQueryHandler> logger,
        IEmployerContractRepository repository,
        IEmployerEmployeeRepository employeeRepo,
        IViewThirdPartyRepository thirdPartyRepo,
        IMediator mediator)
    {
        _logger = logger;
        _repository = repository;
        _employeeRepo = employeeRepo;
        _thirdPartyRepo = thirdPartyRepo;
        _mediator = mediator;
    }

    public async Task<Result<GetECThirdPartiesResponse?>> Handle(GetECThirdPartiesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetECThirdParties(request.ProjectId, request.EmployerIds, request.PageIndex, request.PageSize, ct);
            if (result.RowCount < 1 || result.Data is null)
                return Result.Failure<GetECThirdPartiesResponse>(ContractorContractErrors.NoThirdPartyFoundForEc);
            var employerIds = result.Data.NullListed(x => x.EmployerId);
            var employees = await _employeeRepo.GetThirdPartiesByEmployerId(employerIds, ct);
            var employeeIds = employees.Listed(x => x.EmployeeId);

            var userIds = employerIds
                .Concat(employeeIds)
                .Listed(x => x);

            var skills = await _mediator.Send(new GetThirdPartiesSkillsQuery(employeeIds), ct);

            List<GetECThirdPartiesModel> response = [];
            if (userIds is not null && userIds.Count > 0)
            {
                var users = await _thirdPartyRepo.GetByIds(userIds, ct);
                foreach (var item in employees)
                {
                    var contract = result.Data.FirstOrDefault(x => x.EmployerId == item.EmployerId);
                    var employer = users.FirstOrDefault(x => x.Id == item.EmployerId);
                    var thirdParty = users.FirstOrDefault(x => x.Id == item.EmployeeId);
                    string? skill = string.Empty;
                    if (!skills.IsBad())
                    {
                        var thirdPartyskill = skills.Value!.Data!.Where(x => x.ThirdPartyId == item.EmployeeId).Listed(x => x.Skill);
                        skill = thirdPartyskill.JoinListDisc();
                    }

                    response.Add(new GetECThirdPartiesModel
                    {
                        Id = item.Id,
                        ThirdPartyId = thirdParty?.Id,
                        ThirdParty = thirdParty?.FirstName + " " + thirdParty?.LastName,
                        EmployerId = employer.Id,
                        Employer = employer?.FirstName + " " + employer?.LastName,
                        EContractHeadCode = contract?.EContractHeadCode,
                        Skill = skill,
                        StartDate = contract.StartDate,
                        EndDate = contract.EndDate
                    });
                }
            }

            return result.Data.Any() ?
                new GetECThirdPartiesResponse
                (
                    response,
                    result.RowCount
                ) : Result.Failure<GetECThirdPartiesResponse?>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetECThirdPartiesResponse?>(SharedErrors.UnknownError);
        }
    }
}