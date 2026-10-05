using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.EmployerEmployees;
using Engineering.Application.Services.EmployerEmployees.Contracts.GetEmployeesByEmployerId;
using Engineering.Domain.Errors.EmployerEmployees;

namespace Engineering.Application.Services.EmployerEmployees.Queries.GetEmployeesByEmployerId;

public class GetEmployeesByEmployerIdQueryHandler : IQueryHandler<GetEmployeesByEmployerIdQuery, GetEmployeesByEmployerIdResponse?>
{
    private readonly ILogger<GetEmployeesByEmployerIdQuery> _logger;
    private readonly IEmployerEmployeeRepository _repository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;

    public GetEmployeesByEmployerIdQueryHandler(
        ILogger<GetEmployeesByEmployerIdQuery> logger,
        IEmployerEmployeeRepository repository,
        IViewThirdPartyRepository thirdPartyRepo)
    {
        _logger = logger;
        _repository = repository;
        _thirdPartyRepo = thirdPartyRepo;
    }

    public async Task<Result<GetEmployeesByEmployerIdResponse?>> Handle(GetEmployeesByEmployerIdQuery request, CT ct)
    {
        try
        {
            var employer = await _thirdPartyRepo.GetByIds([request.EmployerId], ct);
            if (employer is null)
                return Result.Failure<GetEmployeesByEmployerIdResponse?>(EmployerEmployeeErrors.EmployerNotFound);

            var result = await _repository.GetEmployeesByEmployerId(request.EmployerId, request.PageIndex, request.PageSize, ct);

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
                new GetEmployeesByEmployerIdResponse
                (
                    result.Data,
                    result.RowCount
                ) : Result.Failure<GetEmployeesByEmployerIdResponse>(EmployerEmployeeErrors.EmployeeForEmployerNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetEmployeesByEmployerIdResponse>(SharedErrors.UnknownError);
        }
    }
}