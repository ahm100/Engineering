using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.EmployerEmployees;
using Engineering.Application.Services.EmployerEmployees.Contracts.GetFltrEmployees;
using Engineering.Domain.Errors.EmployerEmployees;

namespace Engineering.Application.Services.EmployerEmployees.Queries.GetFltrEmployees;

public class GetFltrEmployeesQueryHandler : IQueryHandler<GetFltrEmployeesQuery, GetFltrEmployeesResponse?>
{
    private readonly ILogger<GetFltrEmployeesQueryHandler> _logger;
    private readonly IEmployerEmployeeRepository _repository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;

    public GetFltrEmployeesQueryHandler(
        ILogger<GetFltrEmployeesQueryHandler> logger,
        IEmployerEmployeeRepository repository,
        IViewThirdPartyRepository thirdPartyRepo)
    {
        _logger = logger;
        _repository = repository;
        _thirdPartyRepo = thirdPartyRepo;
    }

    public async Task<Result<GetFltrEmployeesResponse?>> Handle(GetFltrEmployeesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFltrEmployees(request.PageIndex, request.PageSize, ct);

            var userIds = result.Data.Any() ? result.Data
                .Select(x => x.EmployeeId)
                .Concat(result.Data.Select(x => x.EmployerId))
                .Listed(x => x) : null;

            if (userIds is not null && userIds.Any())
            {
                var thirdParties = await _thirdPartyRepo.GetByIds(userIds, ct);
                foreach (var item in result.Data)
                {
                    var employerThirdParty = thirdParties.FirstOrDefault(x => x.Id == item.EmployerId);
                    item.Employer = employerThirdParty?.FirstName + " " + employerThirdParty?.LastName;
                    var employee = thirdParties.FirstOrDefault(x => x.Id == item.EmployeeId);
                    item.Employee = employee?.FirstName + " " + employee?.LastName;
                }
            }
            return result.Data.Any() ?
                new GetFltrEmployeesResponse
                (
                    result.Data,
                    result.RowCount
                ) : Result.Failure<GetFltrEmployeesResponse>(EmployerEmployeeErrors.EmployeeForEmployerNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetFltrEmployeesResponse>(SharedErrors.UnknownError);
        }
    }
}