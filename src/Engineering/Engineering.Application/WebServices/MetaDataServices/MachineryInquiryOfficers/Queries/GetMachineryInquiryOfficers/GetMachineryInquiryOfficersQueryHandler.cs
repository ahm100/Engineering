using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.MachineryInquiryOfficers.Models.GetMachineryInquiryOfficers;

namespace Engineering.Application.WebServices.MetaDataServices.MachineryInquiryOfficers.Queries.GetMachineryInquiryOfficers;

public class GetMachineryInquiryOfficersQueryHandler : IQueryHandler<GetMachineryInquiryOfficersQuery, DataResult<List<Officer?>?>?>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<GetMachineryInquiryOfficersQueryHandler> _logger;

    public GetMachineryInquiryOfficersQueryHandler(ILogger<GetMachineryInquiryOfficersQueryHandler> logger, IMetaDataService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }

    public async Task<Result<DataResult<List<Officer?>?>?>> Handle(GetMachineryInquiryOfficersQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetMachineryInquiryOfficers(request.Adapt<GetMachineryInquiryOfficersRequest>(), ct);

            return (result?.Value?.Data?.Any() ?? false) ?
                new DataResult<List<Officer?>?>
                {
                    Data = result.Value!.Data!,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<Officer?>?>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<Officer?>?>>(SharedErrors.UnknownError);
        }
    }
}