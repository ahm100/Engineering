using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Queries.GetRequestMachineryInquiryByRequestId;

public class GetRequestMachineryInquiryByRequestIdQueryHandler : IQueryHandler<GetRequestMachineryInquiryByRequestIdQuery, List<RequestMachineryInquiry>>
{
    private readonly ILogger<GetRequestMachineryInquiryByRequestIdQueryHandler> _logger;
    private readonly IRequestMachineryInquiryRepository _repository;

    public GetRequestMachineryInquiryByRequestIdQueryHandler(ILogger<GetRequestMachineryInquiryByRequestIdQueryHandler> logger, IRequestMachineryInquiryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<RequestMachineryInquiry>?>> Handle(GetRequestMachineryInquiryByRequestIdQuery request, CT ct)
    {
        try
        {
            var entites = await _repository.GetByRequestIdAsync(request.RequestMachineryId, ct);
            return entites.Any() ? entites : Result.Failure<List<RequestMachineryInquiry>>(SharedErrors.ItemNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<List<RequestMachineryInquiry>>(SharedErrors.UnknownError);
        }
    }
}
