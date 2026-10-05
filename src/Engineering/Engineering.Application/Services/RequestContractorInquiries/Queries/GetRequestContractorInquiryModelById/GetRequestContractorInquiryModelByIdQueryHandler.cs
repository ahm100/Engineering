using Engineering.Application.Abstractions.Data.RequestContractors;
using Engineering.Application.Services.RequestContractors.Models.GetRequestContractorInquiryById;

namespace Engineering.Application.Services.RequestContractorInquiries.Queries.GetRequestContractorInquiryModelById;

public class GetRequestContractorInquiryModelByIdQueryHandler : IQueryHandler<GetRequestContractorInquiryModelByIdQuery, GetRequestContractorInquiryByIdResponse>
{
    private readonly ILogger<GetRequestContractorInquiryModelByIdQueryHandler> _logger;
    private readonly IRequestContractorInquiryRepository _repository;

    public GetRequestContractorInquiryModelByIdQueryHandler(ILogger<GetRequestContractorInquiryModelByIdQueryHandler> logger, IRequestContractorInquiryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetRequestContractorInquiryByIdResponse?>> Handle(GetRequestContractorInquiryModelByIdQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.GetInquiryModelById(request.RequestContractorInquiryId, ct);
            if (entity is null)
                return Result.Failure<GetRequestContractorInquiryByIdResponse>(RequestContractorInquiryErrors.RequestContractorInquiryNotFound);

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<GetRequestContractorInquiryByIdResponse>(SharedErrors.UnknownError);
        }
    }
}
