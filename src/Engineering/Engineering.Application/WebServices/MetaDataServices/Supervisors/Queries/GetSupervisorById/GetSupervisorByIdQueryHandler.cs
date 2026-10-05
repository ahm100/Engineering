using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Supervisors.Models.GetSupervisorById;
using SupervisorModel = Engineering.Application.WebServices.MetaDataServices.Supervisors.Models.Supervisor;

namespace Engineering.Application.WebServices.MetaDataServices.Supervisors.Queries.GetSupervisorById;

public class GetSupervisorByIdQueryHandler : IQueryHandler<GetSupervisorByIdQuery, SupervisorModel?>
{
    private readonly ILogger<GetSupervisorByIdQueryHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public GetSupervisorByIdQueryHandler(ILogger<GetSupervisorByIdQueryHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<SupervisorModel?>> Handle(GetSupervisorByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetSupervisorById(request.Adapt<GetSupervisorByIdRequest>(), ct);

            var data = result?.Data!.FirstOrDefault();

            return data ?? Result.Failure<SupervisorModel?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<SupervisorModel?>(SharedErrors.UnknownError);
        }
    }
}