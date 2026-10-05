using Engineering.Application.Abstractions.Data.RequestContractors;
using Engineering.Application.Services.RequestContractorInquiries.Models.GetRequestContractorInquiryByRequestId;

namespace Engineering.Application.Services.RequestContractorInquiries.Queries.GetRequestContractorInquiryByRequestId;

public class GetRequestContractorInquiryByRequestIdQueryHandler : IQueryHandler<GetRequestContractorInquiryByRequestIdQuery, DataResult<List<GetRequestContractorInquiryByRequestIdModel>>>
{
    private readonly ILogger<GetRequestContractorInquiryByRequestIdQueryHandler> _logger;
    private readonly IRequestContractorInquiryRepository _repository;

    public GetRequestContractorInquiryByRequestIdQueryHandler(ILogger<GetRequestContractorInquiryByRequestIdQueryHandler> logger, IRequestContractorInquiryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetRequestContractorInquiryByRequestIdModel>>?>> Handle(GetRequestContractorInquiryByRequestIdQuery request, CT ct)
    {
        try
        {
            var entites = await _repository.GetsInquiryByRequestContractorId(request.RequestContractorId, request.CompanyId, request.PageIndex, request.PageSize, ct);
            return entites.Data.Any()
                    ? new DataResult<List<GetRequestContractorInquiryByRequestIdModel>>
                    {
                        Data = entites.Data,
                        RowCount = entites.RowCount
                    } : Result.Failure<DataResult<List<GetRequestContractorInquiryByRequestIdModel>>>(RequestContractorInquiryErrors.RequestContractorInquiriesNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<GetRequestContractorInquiryByRequestIdModel>>>(SharedErrors.UnknownError);
        }
    }
}
