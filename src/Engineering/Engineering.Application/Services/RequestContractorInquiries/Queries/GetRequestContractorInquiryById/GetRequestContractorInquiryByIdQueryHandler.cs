using Engineering.Application.Abstractions.Data.RequestContractors;
using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Application.Services.RequestContractorInquiries.Queries.GetRequestContractorInquiryById;

public class GetRequestContractorInquiryByIdQueryHandler : IQueryHandler<GetRequestContractorInquiryByIdQuery, RequestContractorInquiry>
{
    private readonly ILogger<GetRequestContractorInquiryByIdQueryHandler> _logger;
    private readonly IRequestContractorInquiryRepository _repository;

    public GetRequestContractorInquiryByIdQueryHandler(ILogger<GetRequestContractorInquiryByIdQueryHandler> logger, IRequestContractorInquiryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestContractorInquiry?>> Handle(GetRequestContractorInquiryByIdQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.RequestContractorInquiryId, ct);
            if (entity is null)
                return Result.Failure<RequestContractorInquiry>(RequestContractorInquiryErrors.RequestContractorInquiryNotFound);

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<RequestContractorInquiry>(SharedErrors.UnknownError);
        }
    }
}
