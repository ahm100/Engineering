using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Employees.Models.GetEmployeeById;
using EmployeeModel = Engineering.Application.WebServices.MetaDataServices.Employees.Models.Employee;

namespace Engineering.Application.WebServices.MetaDataServices.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdQueryHandler : IQueryHandler<GetEmployeeByIdQuery, EmployeeModel?>
{
    private readonly ILogger<GetEmployeeByIdQueryHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public GetEmployeeByIdQueryHandler(ILogger<GetEmployeeByIdQueryHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<EmployeeModel?>> Handle(GetEmployeeByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetEmployeeById(request.Adapt<GetEmployeeByIdRequest>(), ct);

            return result?.Value != null ? result.Value?.Data?.FirstOrDefault() : Result.Failure<EmployeeModel?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployeeModel?>(SharedErrors.UnknownError);
        }
    }
}