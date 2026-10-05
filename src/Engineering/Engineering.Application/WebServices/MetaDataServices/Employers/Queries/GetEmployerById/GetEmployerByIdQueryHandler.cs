using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Employers.Models.GetEmployerById;
using EmployerModel = Engineering.Application.WebServices.MetaDataServices.Employers.Models.Employer;

namespace Engineering.Application.WebServices.MetaDataServices.Employers.Queries.GetEmployerById;

public class GetEmployerByIdQueryHandler : IQueryHandler<GetEmployerByIdQuery, EmployerModel?>
{
    private readonly ILogger<GetEmployerByIdQueryHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public GetEmployerByIdQueryHandler(ILogger<GetEmployerByIdQueryHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<EmployerModel?>> Handle(GetEmployerByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetEmployerById(request.Adapt<GetEmployerByIdRequest>(), ct);

            var data = result?.Value?.Data?.FirstOrDefault();

            return data ?? Result.Failure<EmployerModel?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerModel?>(SharedErrors.UnknownError);
        }
    }
}