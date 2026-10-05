using Engineering.Application.Abstractions.Data.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Queries.GetRequestMachineryInquiryOperators;

public class GetRequestMachineryInquiryOperatorsQueryHandler : IQueryHandler<GetRequestMachineryInquiryOperatorsQuery, List<GetRequestMachineryInquiryOperatorsQueryModel>>
{
    private readonly ILogger<GetRequestMachineryInquiryOperatorsQueryHandler> _logger;
    private readonly IRequestMachineryInquiryOperatorRepository _repository;

    public GetRequestMachineryInquiryOperatorsQueryHandler(ILogger<GetRequestMachineryInquiryOperatorsQueryHandler> logger, IRequestMachineryInquiryOperatorRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<GetRequestMachineryInquiryOperatorsQueryModel>?>> Handle(GetRequestMachineryInquiryOperatorsQuery request, CT ct)
    {
        try
        {
            var entities = await _repository.GetFilteredAsync(request.MachineryId, request.MachinertGroupId, ct);

            return entities;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<List<GetRequestMachineryInquiryOperatorsQueryModel>>(SharedErrors.UnknownError);
        }
    }
}
