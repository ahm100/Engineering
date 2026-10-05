using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Consultants.Models.GetConsultantById;
using ConsultantModel = Engineering.Application.WebServices.MetaDataServices.Consultants.Models.Consultant;

namespace Engineering.Application.WebServices.MetaDataServices.Consultants.Queries.GetConsultantById;

public class GetConsultantByIdQueryHandler : IQueryHandler<GetConsultantByIdQuery, ConsultantModel?>
{
    private readonly ILogger<GetConsultantByIdQueryHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public GetConsultantByIdQueryHandler(ILogger<GetConsultantByIdQueryHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<ConsultantModel?>> Handle(GetConsultantByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetConsultantById(request.Adapt<GetConsultantByIdRequest>(), ct);

            var data = result?.Data!.FirstOrDefault();

            return data ?? Result.Failure<ConsultantModel?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ConsultantModel?>(SharedErrors.UnknownError);
        }
    }
}